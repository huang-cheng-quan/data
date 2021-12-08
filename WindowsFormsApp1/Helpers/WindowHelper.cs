using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Camera_Capture_demo.Helpers
{
    public class WindowHelper
    {
        /// <summary>
        /// 打开用户控件子窗口
        /// </summary>
        /// <param name="objFrm"></param>
        /// <param name="p"></param>
        public static void OpenFrom(Control objFrm, Control p)
        {
            objFrm.Height = p.Height;
            objFrm.Width = p.Width;
            objFrm.Dock = DockStyle.Fill;
            p.Controls.Add(objFrm);
        }

        /// <summary>
        /// 关闭子窗口
        /// </summary>
        /// <param name="p"></param>
        public static void CloseFrom(Control p)
        {
            if (p.Controls.Count > 0)
            {
                p.Controls[0].Dispose();
                p.Controls.Clear();
                GC.Collect();
            }
        }
        /// <summary>
        /// 检查窗口是否打开
        /// </summary>
        /// <param name="asFormName"></param>
        /// <returns></returns>
        public static bool CheckFormIsOpen(string asFormName)
        {
            bool bResult = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == asFormName)
                {
                    bResult = true;
                    //if (frm.WindowState == FormWindowState.Minimized)
                    //{
                    //    frm.WindowState = FormWindowState.Maximized;
                    //    //最大化窗口
                    //}
                    frm.WindowState = FormWindowState.Normal;
                    frm.Activate();
                    //置顶窗口，激活
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            return bResult;
        }

        /// <summary>
        /// 打开或激活窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FormName"></param>
        public static  void ShowOrActiveForm<T>(string FormName) where T : Form, new()
        {
            if (!CheckFormIsOpen(FormName))
            {
                Form frm = new T();
                frm.Show();
            }
        }
    }
}
