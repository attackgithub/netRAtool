using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace netRAtoolServer
{
    public partial class Form2 : Form
    {
        private int port;
        private TcpClient client;
        private TcpListener server;
        private NetworkStream mainStream;

        private readonly Thread Listening;
        private readonly Thread GetImage;

        public Form2(int Port)
        {
            port = Port;
            client = new TcpClient();
            Listening = new Thread(StartListening);
            GetImage = new Thread(ReceiveImage);

            InitializeComponent();
        }

        private void StartListening()
        {
            while(!client.Connected)
            {
                server.Start();
                client = server.AcceptTcpClient();
            }
            GetImage.Start();
        }

        private void StopListening()
        {
            server.Stop();
            client = null;
            if (Listening.IsAlive) Listening.Abort();
            if (GetImage.IsAlive) GetImage.Abort();
        }

        private void ReceiveImage()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();

            while(client.Connected)
            {
                mainStream = client.GetStream();
                try
                {
                    pictureBox1.Image = (Image)binFormatter.Deserialize(mainStream);
                }
                catch(Exception)
                {
                    ;
                }
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            server = new TcpListener(IPAddress.Any, port);
            Listening.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            StopListening();
        }
    }
}
