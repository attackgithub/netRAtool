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

        private readonly TcpClient client = new TcpClient();
        private NetworkStream mainStream;
        private int portNumber;
        
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

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnectClick_Click(object sender, EventArgs e)
        {
            portNumber = int.Parse(textBox2.Text);

            if(btnConnectClick.Text.StartsWith("Disconnect"))
            {
                btnConnectClick.Text = "Connect";
                timer1.Stop();
                btnRemoteDesktop.Text = "Share Desktop";
                client.Close();
            }
            else
            {
                try
                {
                    client.Connect(textBox1.Text, portNumber);
                    btnConnectClick.Text = "Disconnect";
                    MessageBox.Show("Connected to server!");
                }
                catch(Exception)
                {
                    MessageBox.Show("Failed to connect...");
                }
            }
        }

        private void btnRemoteDesktop_Click(object sender, EventArgs e)
        {
            if(btnRemoteDesktop.Text.StartsWith("Share"))
            {
                timer1.Start();
                btnRemoteDesktop.Text = "Stop Remote";
            }
            else
            {
                timer1.Stop();
                btnRemoteDesktop.Text = "Share Desktop";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendDesktopImage();
        }
    }
}
