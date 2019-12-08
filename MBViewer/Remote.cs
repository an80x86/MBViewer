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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Player();
            button1.Enabled = !false;
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
    }
}
