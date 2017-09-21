
using System;
using MonoBrickFirmware;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Movement;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace MonoBrickHelloWorld
{
	class MainClass
	{
		private netWorkingClass networker;

		public static void Main (string[] args)
		{
			MainClass mainClass = new MainClass ();

			mainClass.networker = new netWorkingClass ();
			mainClass.networker.connect ();

		}
			
	}






























	class netWorkingClass
	{

		private const string connectIP = "192.168.43.87";
		private const int connectPort = 1337;

		private Socket sck;

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
						break;
					case "left":
						LcdConsole.WriteLine("left");
						break;
					case "backward":
						LcdConsole.WriteLine("backward");
						break;
					case "right":
						LcdConsole.WriteLine("right");
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

