using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool boni;
            Mutex mu = new Mutex(false, Application.ProductName, out boni);
           
            if (boni)
            {
                Task.Run(() =>
                {
                    StartLoadFrm f = new StartLoadFrm();
                    f.Show();
                    while (true)
                    {
                        if (MainForm.IsinitOK)
                        {
                            f.Close();
                        }
                    }
                    
                   
                   
                });
                Application.Run(new MainForm()); 
            }
            else
            {
                MessageBox.Show("重复打开程序，请关闭");
            }
           
        }
    }
}
