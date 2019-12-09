using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] param)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (param.Length>0 && param[0].ToLower().Equals("server"))
            {
                Application.Run(new MainForm());
            }
            else
            {
                Application.Run(new Remote());
            }
        }
    }
}
