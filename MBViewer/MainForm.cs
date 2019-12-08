using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            timerMain.Interval = Constant.Time;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timerMain.Enabled = !timerMain.Enabled;
            button1.Text = timerMain.Enabled ? "Kayıt başladı" : "Kayıt bitti";
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            timerMain.Enabled = false;
            saveScreen();
            timerMain.Enabled = true;
        }

        private void saveScreen() {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }
                var fileLocation = string.Format(@"D:\tmp\src\screen_{0}.jpg", DateTime.Now.ToString("yyyyMMddHHssfff"));
                bitmap.Save(fileLocation, ImageFormat.Jpeg);
                File.WriteAllText(fileLocation.Replace(".jpg", ".txt"), Constant.PointToString(Constant.GetCursorPosition()));
            }
        }
    }
}
