using BaseForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Camera_Capture_demo.Helpers;

namespace Dyestripping.DeviceSettingFrms
{

    public partial class OmronPlcSettingFrm : ZoomForm
    {
        int plcNo;
        public OmronPlcSettingFrm(int plcNo)
        {
            InitializeComponent();
            this.plcNo = plcNo;
            this.Name += plcNo;
            this.Text = $"PLC{plcNo + 1}参数设置";
        }

        private void cboMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboMenu.SelectedIndex)
            {
                case 0:
                    WindowHelper.CloseFrom(panel1);
                    WindowHelper.OpenFrom(new OmronFinsTcp(plcNo), panel1);
                    break;
                default:
                    break;
            }
        }

        private void OmronPlcSettingFrm_Load(object sender, EventArgs e)
        {
            WindowHelper.CloseFrom(panel1);
            WindowHelper.OpenFrom(new OmronFinsTcp(plcNo), panel1);
            //this.IsMdiContainer = true;
        }
    }
}
