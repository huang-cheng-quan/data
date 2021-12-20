using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.Helpers;
using Camera_Capture_demo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camera_Capture_demo.VisionFrms
{
    public partial class ToolOffsetSettingFrm : Form
    {
        ToolInfos toolInfos;
        public ToolOffsetSettingFrm()
        {
            InitializeComponent();
            toolInfos = ConfigVars.configInfo.ToolInfos;
        }

        private void ToolOffsetSettingFrm_Load(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is NumericUpDown)
                {
                    (control as NumericUpDown).DecimalPlaces = 2;
                    (control as NumericUpDown).Increment = 0.1M;
                }
            }
            nudXoffset1.Value = Convert.ToDecimal(toolInfos.Xoffset1);
            nudYoffset1.Value = Convert.ToDecimal(toolInfos.Yoffset1);
            nudXoffset2.Value = Convert.ToDecimal(toolInfos.Xoffset2);
            nudYoffset2.Value = Convert.ToDecimal(toolInfos.Yoffset2);
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            toolInfos.Xoffset1 = Convert.ToSingle(nudXoffset1.Value);
            toolInfos.Yoffset1 = Convert.ToSingle(nudYoffset1.Value);
            toolInfos.Xoffset2 = Convert.ToSingle(nudXoffset2.Value);
            toolInfos.Yoffset2 = Convert.ToSingle(nudYoffset2.Value);
            
            XmlHelper.SerializeToXml<ConfigInfo>(ConfigVars.configInfo);
            MessageBox.Show("参数保存成功");
        }
    }
}
