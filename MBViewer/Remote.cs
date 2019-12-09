using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBViewer
{
    public partial class Remote : Form
    {
        public Remote()
        {
            InitializeComponent();
            timerMain.Interval = Constant.Time;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timerMain.Enabled = !timerMain.Enabled;
            button1.Text = timerMain.Enabled ? "İzleme başladı" : "İzleme bitti";
        }

        private Bitmap Base64StringToBitmap(string base64String)
        {
            Bitmap bmpReturn = null;
            //Convert Base64 string to byte[]
            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }

        private void Player()
        {
            var filePaths = Directory.GetFiles(@"D:\tmp\src", "*.jpg",
                                         SearchOption.AllDirectories).ToList<string>();
            filePaths.Sort();

            string firstText = "Hello";
            string secondText = "World";

            PointF firstLocation = new PointF(10f, 10f);
            PointF secondLocation = new PointF(10f, 50f);

            var src = new Bitmap("99188.png");
            foreach (var fileName in filePaths)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Length > 0)
                {
                    var loc = File.ReadAllText(fileName.Replace(".jpg", ".txt")).Split(',');
                    Image img = Image.FromFile(fileName);

                    int Width = img.Width - pictureBoxMain.Width;
                    int Height = img.Height - pictureBoxMain.Height;

                    using (Graphics graphics = Graphics.FromImage((Bitmap)img))
                    {
                        using (Font arialFont = new Font("Arial", 50))
                        {
                            graphics.DrawString(firstText, arialFont, Brushes.Blue, firstLocation);
                            graphics.DrawString(secondText, arialFont, Brushes.Red, secondLocation);

                            graphics.DrawImage(src, new Rectangle(Int32.Parse(loc[0]), Int32.Parse(loc[1]), 32,32));
                        }
                    }

                    pictureBoxMain.Image = img;
                    System.Threading.Thread.Sleep(Constant.Time);
                    Application.DoEvents();
                }
            }

            foreach (var fileName in filePaths)
            {
                //File.Delete(fileName);
                //File.Delete(fileName.Replace(".jpg",".txt"));
            }
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            timerMain.Enabled = false;
            using (IRedisClient client = new RedisClient())
            {
                var customerClient = client.As<Customer>();
                var customer = customerClient.GetById(Constant.lastCustomerId);
                Console.WriteLine($"Müşteri Id : {customer.Id}");

                pictureBoxMain.Image = Base64StringToBitmap(customer.Data);
            }
            Application.DoEvents();
            timerMain.Enabled = !false;
        }

        private void Remote_Load(object sender, EventArgs e)
        {
        }
    }
}
