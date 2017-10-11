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
using System.IO;

namespace RemoteControlServer
{
    public partial class RemoteControlServer : Form
    {
        private Socket sck, acc;
        private bool goingForward = false, goingBackward = false, goingLeft = false, goingRight = false, grabbing = false;
        private bool releasing = false;
        private bool repeating = false;
        public string fileLocation = @"route.txt";
        private StreamReader sr;

        private List<string> cmds;

        private string fileText;

        public RemoteControlServer()
        {
            InitializeComponent();



            sck = socket();
            sck.Bind(new IPEndPoint(0, 1337));
            sck.Listen(0);


            if (File.Exists(@fileLocation))
            {
                repeatButton.Visible = true;
                sr = new StreamReader(fileLocation);

                fileText = sr.ReadToEnd();

                string[] lines = fileText.Split('\n');
                cmds = new List<string>();

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] currentCmd = lines[i].Split(',');

                    cmds.Add(currentCmd[0]);
                    cmds.Add(currentCmd[1]);
                }
            }
            else
            {
                repeatLabel.Visible = true;
            }


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

                        if (rec <= 0)
                        {
                            throw new SocketException();
                        }

                        Array.Resize(ref buffer, rec);
                        acc.Send(getBytes("init"), 0, getBytes("init").Length, 0);
                    }
                    catch
                    {
                        MessageBox.Show("Server: Client disconnected.");
                        acc.Close();
                        Application.Exit();
                        break;
                    }
                }
            }).Start();
        }

        private void initButton_Click(object sender, EventArgs e)
        {
            acc.Send(getBytes("init"), 0, getBytes("init").Length, 0);
        }

        private void repeatButton_Click(object sender, EventArgs e)
        {
            repeating = true;

            for (int i = 0; i < cmds.Count; i += 2)
            {
                acc.Send(getBytes(cmds[i]), 0, getBytes(cmds[i]).Length, 0);
                Thread.Sleep(int.Parse(cmds[i + 1]));
                //acc.Send(getBytes(cmds[i]), 0, getBytes(cmds[i]).Length, 0);
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (repeating == false)
            {
                repeatButton.Enabled = false;
                switch (keyData)
                {
                    case Keys.W:
                        if (goingBackward == false)
                        {
                            acc.Send(getBytes("forward"), 0, getBytes("forward").Length, 0);
                            if (goingForward == false && goingBackward == false)
                            {
                                goingForward = true;
                                forwardIndi.Visible = true;
                            }
                            else if (goingForward)
                            {
                                goingForward = false;
                                forwardIndi.Visible = false;
                            }
                        }
                        else
                        {
                            goingBackward = false;
                            goingLeft = false;
                            goingRight = false;
                            backwardIndi.Visible = false;
                            leftIndi.Visible = false;
                            rightIndi.Visible = false;
                            acc.Send(getBytes("forward"), 0, getBytes("forward").Length, 0);
                            goingForward = true;
                            forwardIndi.Visible = true;

                        }
                        break;
                    case Keys.A:
                        if (goingRight == false)
                        {
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
                        }
                        else
                        {
                            goingRight = false;
                            goingForward = false;
                            goingBackward = false;
                            rightIndi.Visible = false;
                            forwardIndi.Visible = false;
                            backwardIndi.Visible = false;

                            acc.Send(getBytes("left"), 0, getBytes("left").Length, 0);
                            goingLeft = true;
                            leftIndi.Visible = true;
                        }
                        break;
                    case Keys.S:
                        if (goingForward == false)
                        {
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
                        }
                        else
                        {
                            goingForward = false;
                            goingRight = false;
                            goingLeft = false;
                            forwardIndi.Visible = false;
                            rightIndi.Visible = false;
                            leftIndi.Visible = false;

                            acc.Send(getBytes("backward"), 0, getBytes("backward").Length, 0);
                            goingBackward = true;
                            backwardIndi.Visible = true;
                        }
                        break;
                    case Keys.D:
                        if (goingLeft == false)
                        {
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
                        }
                        else
                        {
                            goingLeft = false;
                            goingForward = false;
                            goingBackward = false;
                            leftIndi.Visible = false;
                            forwardIndi.Visible = false;
                            backwardIndi.Visible = false;
                            acc.Send(getBytes("right"), 0, getBytes("right").Length, 0);
                            goingRight = true;
                            rightIndi.Visible = true;
                        }
                        break;
                    case Keys.Q:
                        if (releasing == false)
                        {
                            acc.Send(getBytes("grab"), 0, getBytes("grab").Length, 0);
                            if (grabbing == false)
                            {
                                grabbing = true;
                                grabbingLabel.Visible = true;
                            }
                            else
                            {
                                grabbing = false;
                                grabbingLabel.Visible = false;
                            }
                        }
                        else
                        {
                            releasing = false;
                            releasingLabel.Visible = false;
                            acc.Send(getBytes("grab"), 0, getBytes("grab").Length, 0);
                            grabbing = true;
                            grabbingLabel.Visible = true;
                        }
                        break;
                    case Keys.E:
                        if (grabbing == false)
                        {
                            acc.Send(getBytes("release"), 0, getBytes("release").Length, 0);
                            if (releasing == false)
                            {
                                releasing = true;
                                releasingLabel.Visible = true;
                            }
                            else
                            {
                                releasing = false;
                                releasingLabel.Visible = false;
                            }
                        }
                        else
                        {
                            grabbing = false;
                            grabbingLabel.Visible = false;
                            acc.Send(getBytes("release"), 0, getBytes("release").Length, 0);
                            releasing = true;
                            releasingLabel.Visible = true;

                        }
                        break;
                    case Keys.Space:
                        acc.Send(getBytes("stop"), 0, getBytes("stop").Length, 0);

                        goingForward = false;
                        forwardIndi.Visible = false;

                        goingBackward = false;
                        backwardIndi.Visible = false;

                        goingLeft = false;
                        leftIndi.Visible = false;

                        goingRight = false;
                        rightIndi.Visible = false;
                        break;
                    case Keys.F:
                        acc.Send(getBytes("clawStop"), 0, getBytes("clawStop").Length, 0);
                        grabbing = false;
                        grabbingLabel.Visible = false;

                        releasing = false;
                        releasingLabel.Visible = false;
                        break;
                    case Keys.Escape:
                        acc.Send(getBytes("endAll"), 0, getBytes("endAll").Length, 0);
                        break;
                }
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
