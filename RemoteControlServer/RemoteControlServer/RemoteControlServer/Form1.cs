using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace RemoteControlServer
{
    public partial class RemoteControlServer : Form
    {
        private Socket sck, acc;
        private bool goingForward = false, goingBackward = false, goingLeft = false, goingRight = false;

        public RemoteControlServer()
        {
            InitializeComponent();

            sck = socket();
            sck.Bind(new IPEndPoint(0, 1337));
            sck.Listen(0);

            new Thread(delegate () 
            {
                acc = sck.Accept();
                MessageBox.Show("Robot connected.");
                sck.Close();

                while (true)
                {
                    try
                    {
                        byte[] buffer = new byte[255];
                        int rec = acc.Receive(buffer, 0, buffer.Length, 0);

                        if(rec <= 0)
                        {
                            throw new SocketException();
                        }

                        Array.Resize(ref buffer, rec);

                    }
                    catch 
                    {
                        MessageBox.Show("Server: Client disconnected.");
                        acc.Close();
                        break;
                    }
                }
            }).Start();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.W:
                    acc.Send(getBytes("forward"),0, getBytes("forward").Length, 0);

                    if (goingForward == false)
                    {
                        goingForward = true;
                        forwardIndi.Visible = true;
                    }
                    else
                    {
                        goingForward = false;
                        forwardIndi.Visible = false;
                    }

                    break;
                case Keys.A:
                    acc.Send(getBytes("left"), 0, getBytes("left").Length, 0);
                    if (goingLeft == false)
                    {
                        goingLeft = true;
                        leftIndi.Visible = true;
                    }
                    else
                    {
                        goingLeft = false;
                        leftIndi.Visible = false;
                    }
                    break;
                case Keys.S:
                    acc.Send(getBytes("backward"), 0, getBytes("backward").Length, 0);
                    if (goingBackward == false)
                    {
                        goingBackward = true;
                        backwardIndi.Visible = true;
                    }
                    else
                    {
                        goingBackward = false;
                        backwardIndi.Visible = false;
                    }
                    break;
                case Keys.D:
                    acc.Send(getBytes("right"), 0, getBytes("right").Length, 0);
                    if (goingRight == false)
                    {
                        goingRight = true;
                        rightIndi.Visible = true;
                    }
                    else
                    {
                        goingRight = false;
                        rightIndi.Visible = false;
                    }
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void RemoteControlServer_Load(object sender, EventArgs e)
        {

        }

        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        byte[] getBytes(string message)
        {
            return Encoding.Default.GetBytes(message);
        }
    }
}
