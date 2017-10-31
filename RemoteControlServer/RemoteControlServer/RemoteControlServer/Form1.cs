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
        // Variables
        private Socket sck, acc;
        private bool goingForward = false, goingBackward = false, goingLeft = false, goingRight = false, grabbing = false;
        private bool releasing = false;
        private bool repeating = false;
        public string fileLocation = @"route.txt";
        private StreamReader sr;

        private List<string> cmds;
        string[] lines;

        private string fileText;

        public RemoteControlServer()
        {
            InitializeComponent();
            
            // Create server socket            
            sck = socket();
            sck.Bind(new IPEndPoint(0, 1337));
            sck.Listen(0);

            // Check if file exist
            if (File.Exists(@fileLocation))
            {
                // Initialize the streamreader
                sr = new StreamReader(fileLocation);
                
                // make button visible
                initButton.Visible = true;

                // Reads file and places it in a variable
                fileText = sr.ReadToEnd();

                cmds = new List<string>();

            }
            else
            {
                // If file is not found, button won't be visible.
                initButton.Visible = false;
            }

            // New thread for sending data whilst controlling
            new Thread(delegate ()
            {
                // Accept connection from client.
                acc = sck.Accept();
                MessageBox.Show("Robot connected.");
                // Close old socket. 
                sck.Close();

                // Continuously check for received data. 
                while (true)
                {
                    try
                    {
                        // Byte array for received data.
                        byte[] buffer = new byte[255];
                       
                        int rec = acc.Receive(buffer, 0, buffer.Length, 0);

                        // Check if still connected. 
                        if (rec <= 0)
                        {
                            throw new SocketException();
                        }

                        // Resize the array.
                        Array.Resize(ref buffer, rec);
                    }
                    catch
                    {
                        // If disconnected show message and close sockets. 
                        MessageBox.Show("Server: Client disconnected.");
                        acc.Close();
                        Application.Exit();
                        break;
                    }
                }
            }).Start();
        }

        // When init button is pressed. 
        private void initButton_Click(object sender, EventArgs e)
        {
            // Assign seperators. 
            string[] stringSeparators = new string[] { "\r\n" };
            // Split file on seperators. 
            string[] splittedFile = fileText.Split(stringSeparators, StringSplitOptions.None);

            // Loop through all commands from file and send them seperatly.
            for (int i = 0; i < splittedFile.Length; i++)
            {
                send(splittedFile[i]);

                // Stop the thread, so client has time to process command. 
                Thread.Sleep(250);
            }

            // Send repeat so client knows to repeat. 
            send("repeat");

            MessageBox.Show("Sending.");
            
        }

        private void repeatButton_Click(object sender, EventArgs e)
        {   
            //repeating = true;

            //for (int i = 0; i < cmds.Count; i += 2)
            //{
            //    acc.Send(getBytes(cmds[i]), 0, getBytes(cmds[i]).Length, 0);
            //    Thread.Sleep(int.Parse(cmds[i + 1]));
            //    //acc.Send(getBytes(cmds[i]), 0, getBytes(cmds[i]).Length, 0);
            //}
        }

        // Function to check what key is pressed. 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if robot is not already repeating. 
            if (repeating == false)
            {
                // Switch to check 
                switch (keyData)
                {
                    case Keys.W:
                        // Check if already going backwards, sets arrows on winform. 
                        if (goingBackward == false)
                        {
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
                            goingForward = true;
                            forwardIndi.Visible = true;

                        }
                        send("forward");
                        break;
                    case Keys.A:
                        if (goingRight == false)
                        {
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

                            goingLeft = true;
                            leftIndi.Visible = true;
                        }
                        send("left");
                        break;
                    case Keys.S:
                        if (goingForward == false)
                        {
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

                            goingBackward = true;
                            backwardIndi.Visible = true;
                        }
                        send("backward");
                        break;
                    case Keys.D:
                        if (goingLeft == false)
                        {
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
                            
                            goingRight = true;
                            rightIndi.Visible = true;
                        }
                        send("right");
                        break;
                    case Keys.Q:
                        if (releasing == false)
                        {
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
                            
                            grabbing = true;
                            grabbingLabel.Visible = true;
                        }
                        send("grab");
                        break;
                    case Keys.E:
                        if (grabbing == false)
                        {
                            send("release");
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
                            releasing = true;
                            releasingLabel.Visible = true;
                        }
                        break;
                    case Keys.Space:
                        send("stop");
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
                        send("clawStop");
                        grabbing = false;
                        grabbingLabel.Visible = false;

                        releasing = false;
                        releasingLabel.Visible = false;
                        break;
                    case Keys.Escape:
                        send("endAll");
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void RemoteControlServer_Load(object sender, EventArgs e)
        {

        }
    
        // Quick socket creation function.
        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        // Shortcut for sending data to client. 
        private void send(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            acc.Send(bytes, 0, bytes.Length, 0);    
        }
    }
}
