using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MDT.Console
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (FormLogin frm = new FormLogin())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    FormMain frmMain = new FormMain();
                    frmMain.IsAllowed = frm.IsAllowed;
                    Application.Run(frmMain);
                }
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //FormMain frmMain = new FormMain();
            //frmMain.IsAllowed = true;
            //Application.Run(frmMain);
        }
    }
}
