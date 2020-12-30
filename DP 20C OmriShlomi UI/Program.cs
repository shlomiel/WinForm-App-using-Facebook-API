using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DP_20C_OmriShlomi_UI
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
