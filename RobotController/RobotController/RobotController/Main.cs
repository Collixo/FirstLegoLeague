using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.IO;
using System.Collections.Generic;

namespace MonoBrickHelloWorld
{
	class MainClass
	{
		// Variables. 
		private const int speed = 25;

		private netWorkingClass networker;
		private timerClass timer;
		private Motor MotorLeft;
		private Motor MotorRight;
		private Motor MotorClaw;

		// Lists for saving the raw commands and the split commands.
		public List<string> cmds; 
		public List<string> actualCmds;


		public static void Main (string[] args)
		{
			MainClass mainClass = new MainClass ();

			// Creating networking class, connecting it and setting the mainclass.
			mainClass.networker = new netWorkingClass ();
			mainClass.networker.connect ();
			mainClass.networker.setMainClass (mainClass);

			// Declaring timer class and initializing it. 
			mainClass.timer = new timerClass();
			mainClass.timer.init ();

			// Initializing lists
			mainClass.cmds = new List<string> ();
			mainClass.actualCmds = new List<string> ();

			// Assigning motors
			mainClass.MotorRight = new Motor (MotorPort.OutA);
			mainClass.MotorLeft = new Motor (MotorPort.OutB);
			mainClass.MotorClaw = new Motor (MotorPort.OutC);



		}

		// Function that will repeat the file. 
		public void repeat()
		{
			// Split commands.
			for (int i = 0; i < cmds.Count; i++) 
			{
				string[] splitComma = cmds [i].Split (',');
				actualCmds.Add (splitComma[0]);	
				actualCmds.Add (splitComma[1]);
			}

			// For loop through the commands.
			for (int i = 0; i < actualCmds.Count; i+=2) 
			{
				// Switch checks what command and takes time out of array.
				switch (actualCmds[i]) 
				{
				case "forward":
					Thread.Sleep (int.Parse(actualCmds[i+1]));
					forwards();
					break;
				case "left":
					Thread.Sleep (int.Parse(actualCmds[i+1]));
					left();
					break;
					case "backward":
 					Thread.Sleep (int.Parse(actualCmds[i+1]));
					backwards();
					break;
				case "right":
 					Thread.Sleep (int.Parse(actualCmds[i+1]));
					right();
					break;
				case "grab":
					Thread.Sleep (int.Parse(actualCmds[i+1]));
					clawGrab();
					break;
				case "release":
 					Thread.Sleep (int.Parse(actualCmds[i+1]));
					clawRelease();
					break;
				case "clawStop":
 					Thread.Sleep (int.Parse(actualCmds[i+1]));
					clawStop();
					break;
				case "stop":
					Thread.Sleep (int.Parse(actualCmds[i+1]));
					stop();	
					break;
				case "endAll":
					Thread.Sleep (int.Parse(actualCmds[i+1]));
					endAll();
					break;
				}	
			}
				
		}
			
		// Movement functions.
		public void forwards()
		{
			timer.stopTimer ();
			timer.startTimer ("forward");
			MotorRight.SetSpeed((sbyte)speed);
			MotorLeft.SetSpeed((sbyte)speed);
		}

		public void backwards()
		{
			timer.stopTimer ();
			timer.startTimer ("backward");
			MotorRight.SetSpeed((sbyte)-speed);
			MotorLeft.SetSpeed((sbyte)-speed);
		}

		public void left()
		{
			timer.stopTimer ();
			timer.startTimer ("left");
			MotorRight.SetSpeed((sbyte)speed);
			MotorLeft.SetSpeed((sbyte)-speed);
		}

		public void right()
		{
			timer.stopTimer ();
			timer.startTimer ("right");
			MotorRight.SetSpeed((sbyte)-speed);
			MotorLeft.SetSpeed((sbyte)speed);
		}

		public void stop()
		{
			timer.stopTimer ();
			timer.startTimer ("stop");
			MotorLeft.Off ();
			MotorRight.Off ();
		}

		public void clawGrab()
		{
			timer.stopTimer ();
			timer.startTimer ("grab");
			MotorClaw.SetSpeed ((sbyte)speed);
		}

		public void clawRelease()
		{
			timer.stopTimer ();
			timer.startTimer ("release");
			MotorClaw.SetSpeed ((sbyte)-speed);
		}

		public void clawStop()
		{
			timer.stopTimer ();
			timer.startTimer ("clawStop");
			MotorClaw.Off();
		}


		// Ends the program and saves generated textfile. 
		public void endAll()
		{
			stop ();
			timer.stopTimer ();
			timer.startTimer ("endAll");
			timer.stopTimer ();
			timer.closeWriter ();
		}

	}




	public class timerClass
	{
		// Variables. 
		private System.Timers.Timer timer;
		private int elapsedTime;
		private int interval = 100;
		public StreamWriter sw;
		private bool firstRun = true;

		private string direction;

		// Initializes the timer class. 
		public void init()
		{
			// Make timers, set interval and initialize StreamWriter with file location. 
			timer = new System.Timers.Timer();
			timer.Interval = interval;
			sw = new StreamWriter(@"/home/root/apps/RobotController/route.txt");

			timer.Elapsed += timerEvent;
		}

		// Starts the timer and sets direction of current command.
		public void startTimer(string dir)
		{
			direction = dir;
			timer.Enabled = true;
		}

		// Stop timer stops the timer, checks if it's the first command, if it is not then stop timer and write direction and time to file. 
		public void stopTimer()
		{
			if (firstRun == false) 
			{
				timer.Enabled = false;
				sw.WriteLine (direction + "," + elapsedTime);
				direction = "";
				elapsedTime = 0;
			} 
			else 
			{
				firstRun = false;
			}
		}

		// Closes the streamwriter and therfore saving the file. 
		public void closeWriter()
		{
			timer.Enabled = false;
			sw.Close ();
		}

		// Timer event (delegate), used to count the interval. 
		public void timerEvent(Object source, System.Timers.ElapsedEventArgs e)
		{
			elapsedTime += interval;
		}
	}




	class netWorkingClass
	{
		// Variables. 
		private const string connectIP = "192.168.43.87";
		private const int connectPort = 1337;

		private Socket sck;

		private MainClass mainClass;

		// Connects the client to the sever. 
		public void connect()
		{
			// Creates the socket. 
			sck = socket ();

			// Tries to connect/ 
			try 
			{
				// Create socket. 
				sck.Connect(new IPEndPoint(IPAddress.Parse(connectIP), connectPort));

				// Make a new thread for the read data function, so it can continuously read data. 
				new Thread(delegate ()
					{
						readData();
					}).Start();
			} 
			catch  
			{
				// If connection fails show disconnect message. 
				LcdConsole.WriteLine ("Failed to connect to remote, \n please run remote first.");
				Thread.Sleep (2500);
			}

		}

		// Sets the reference to the mainclass.
		public void setMainClass(MainClass mainn)
		{
			mainClass = mainn;
		}

		// Reads the data and assigns the data to the correct functionality. 
		private void readData()
		{
			// Infinite loop to continuously read data. 
			while (true) 
			{
				try 
				{
					// Byte array, for the received data. 
					byte[] buffer = new byte[255];


					int rec = sck.Receive(buffer, 0, buffer.Length, 0);

					// Checks if received data is valid. 
					if(rec <= 0)
					{
						throw new SocketException();
					}

					// Minimize array size. 
					Array.Resize(ref buffer, rec);

					// Decode byte array back to string. 
					string data = Encoding.Default.GetString(buffer);
						
					// Switch to check what command was received.
					switch (data) 
					{
					case "forward":
						LcdConsole.WriteLine("forward");
						mainClass.forwards();
						break;
					case "left":
						LcdConsole.WriteLine("left");
						mainClass.left();
						break;
					case "backward":
						LcdConsole.WriteLine("backward");
						mainClass.backwards();
						break;
					case "right":
						LcdConsole.WriteLine("right");
						mainClass.right();
						break;
					case "grab":
						LcdConsole.WriteLine("grab");
						mainClass.clawGrab();
						break;
					case "release":
						LcdConsole.WriteLine("release");
						mainClass.clawRelease();
						break;
					case "clawStop":
						mainClass.clawStop();
						break;
					case "stop":
						mainClass.stop();	
						break;
						case "endAll":
						mainClass.endAll();
						break;
						case "repeat":
						mainClass.repeat();
						break;
						// Default for receiving the repeat data from server, adds this data into a list. 
					default:
						mainClass.cmds.Add(data);
						break;
					}

				} 
				catch 
				{
					// Disconnect message. 
					LcdConsole.WriteLine ("Disconnected from server!");
					sck.Close ();
					break;
				}	
			}
		}


		// Shortcut for creating a socket. 
		Socket socket()
		{
			return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}
	}
}