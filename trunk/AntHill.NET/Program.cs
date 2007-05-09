using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AntHill.NET
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new MainForm());
            }
            catch
            {
                MessageBox.Show(Properties.Resources.errorFatal);
                Application.Exit();
            }
        }
    }
}