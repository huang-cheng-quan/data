using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.Helpers;
using Camera_Capture_demo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.TimeDelete
{
    public partial class SetTimeDelete : Form
    {
        public SetTimeDelete()
        {
            InitializeComponent();
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
            ConfigVars.configInfo.Save_Time_Period.OriginalImagesSaveTime = int.Parse(txt_setOriginalSaveTime.Text);
            ConfigVars.configInfo.Save_Time_Period.CompressionImagesSaveTime = int.Parse(txt_setCompressSaveTime.Text);
        
            XmlHelper.SerializeToXml(ConfigVars.configInfo);
        }
    }
}
