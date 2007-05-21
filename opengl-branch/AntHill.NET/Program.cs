using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.Platform.Windows;

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
                MainForm mainForm = new MainForm();
                mainForm.Show();
                while (!mainForm.done)
                {                                                      // Loop That Runs While done = false
                    Application.DoEvents();                      
                    mainForm.DrawGLScene();
                    Gdi.SwapBuffers(mainForm.hDC);
                    //Application.Run(new MainForm());
                }
            }
            catch
            {
                MessageBox.Show(Properties.Resources.errorFatal);
                Application.Exit();
            }
        }
    }
}