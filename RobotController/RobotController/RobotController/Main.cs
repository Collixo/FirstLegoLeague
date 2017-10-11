
using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.IO;
namespace MonoBrickHelloWorld
{
	class MainClass
	{
		private netWorkingClass networker;
		private repeaterClass repeater;
		private Motor MotorLeft;
		private Motor MotorRight;
		private Motor MotorClaw;
		private int speed = 35;


		public static void Main (string[] args)
		{
			MainClass mainClass = new MainClass ();
			mainClass.networker = new netWorkingClass ();
			mainClass.networker.connect ();
			mainClass.networker.setMainClass (mainClass);
			mainClass.repeater = new repeaterClass ();
			mainClass.repeater.init ();


			mainClass.MotorRight = new Motor (MotorPort.OutA);
			mainClass.MotorLeft = new Motor (MotorPort.OutB);
			mainClass.MotorClaw = new Motor (MotorPort.OutC);

		}
			
		public void forwards()
		{
			repeater.stopTimer ();
			repeater.startTimer ("forward");
			MotorRight.SetSpeed((sbyte)speed);
			MotorLeft.SetSpeed((sbyte)speed);

		}

		public void backwards()
		{
			repeater.stopTimer ();
			repeater.startTimer ("backward");
			MotorRight.SetSpeed((sbyte)-speed);
			MotorLeft.SetSpeed((sbyte)-speed);
		}

		public void left()
		{
			repeater.stopTimer ();
			repeater.startTimer ("left");
			MotorRight.SetSpeed((sbyte)speed);
			MotorLeft.SetSpeed((sbyte)-speed);
		}

		public void right()
		{
			repeater.stopTimer ();
			repeater.startTimer ("right");
			MotorRight.SetSpeed((sbyte)-speed);
			MotorLeft.SetSpeed((sbyte)speed);
		}

		public void stop()
		{
			MotorLeft.Off ();
			MotorRight.Off ();
		}

		public void clawGrab()
		{
			repeater.stopTimer ();
			repeater.startTimer ("grab");
			MotorClaw.SetSpeed ((sbyte)speed);
		}

		public void clawRelease()
		{
			repeater.stopTimer ();
			repeater.startTimer ("release");
			MotorClaw.SetSpeed ((sbyte)-speed);
		}

		public void clawStop()
		{
			MotorClaw.Off();
		}

		public void endAll()
		{
			stop ();
			repeater.stopTimer ();
			repeater.closeWriter ();
		}

	}




	public class repeaterClass
	{
		private System.Timers.Timer timer;
		private int elapsedTime;
		private int interval = 100;
		public StreamWriter sw;
		private bool firstRun = true;

		private string direction;

		public void init()
		{
			timer = new System.Timers.Timer();
			timer.Interval = interval;
			sw = new StreamWriter(@"/home/root/apps/RobotController/route.txt");

			timer.Elapsed += timerEvent;
		}

		public void startTimer(string dir)
		{
			direction = dir;
			timer.Enabled = true;
		}

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

		public void closeWriter()
		{
			timer.Enabled = false;
			sw.Close ();
		}

		public void timerEvent(Object source, System.Timers.ElapsedEventArgs e)
		{
			elapsedTime += interval;
		}
	}




	class netWorkingClass
	{

		private const string connectIP = "192.168.43.87";
		private const int connectPort = 1337;

		private Socket sck;

		private MainClass mainClass;

		public void connect()
		{
			sck = socket ();
			try 
			{
				sck.Connect(new IPEndPoint(IPAddress.Parse(connectIP), connectPort));

				new Thread(delegate ()
					{
						readData();
					}).Start();
			} 
			catch  
			{
				LcdConsole.WriteLine ("Failed to connect to remote, \n please run remote first.");
				Thread.Sleep (2500);
			}

		}

		public void setMainClass(MainClass mainn)
		{
			mainClass = mainn;
		}

		private void readData()
		{
			while (true) 
			{
				try 
				{
					byte[] buffer = new byte[255];

					int rec = sck.Receive(buffer, 0, buffer.Length, 0);

					if(rec <= 0)
					{
						throw new SocketException();
					}

					Array.Resize(ref buffer, rec);

					string data = Encoding.Default.GetString(buffer);

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
						LcdConsole.WriteLine(data);
						break;
					case "stop":
						mainClass.stop();	
						break;
						case "endAll":
						mainClass.endAll();
						LcdConsole.WriteLine("END END END END");
						break;
						default:
						LcdConsole.WriteLine(data);
						break;
					}

				} 
				catch 
				{
					LcdConsole.WriteLine ("Disconnected from server!");
					sck.Close ();
					break;
				}	
			}
		}


		Socket socket()
		{
			return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}


	}
}

