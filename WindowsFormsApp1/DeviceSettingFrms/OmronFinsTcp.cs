using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Camera_Capture_demo.Models;
using Camera_Capture_demo.GlobalVariable;
using Camera_Capture_demo.Helpers;
using HslCommunication.Profinet.Omron;
using HslCommunication;

namespace Dyestripping.DeviceSettingFrms
{
    public partial class OmronFinsTcp : UserControl
    {
        OmronPlcFactory omronPlcConfig;
        OmronFinsNet omronInstance;
        int plcNo;
        public OmronFinsTcp(int plcNo)
        {
            InitializeComponent();
            this.plcNo = plcNo;
        }

        private void OmronFinsTcp_Load(object sender, EventArgs e)
        {
            omronPlcConfig = ConfigVars.configInfo.OmronPlcs[plcNo];
            if (omronPlcConfig != null)
            {
                txtIpAddress.Text = omronPlcConfig.IpAddr;
                txtPort.Text = omronPlcConfig.Port.ToString();
                txtPcUnit.Text = omronPlcConfig.SA1.ToString();
                txtPlcUnit.Text = omronPlcConfig.DA1.ToString();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string ipPattern = @"^(([1-9]\d?)|(1\d{2})|(2[01]\d)|(22[0-3]))(\.((1?\d\d?)|(2[04]/d)|(25[0-5]))){3}$";
            if (!Regex.IsMatch(txtIpAddress.Text, ipPattern))
            {
                MessageBox.Show("输入的IP地址格式错误");
                return;
            }
            if (!Regex.IsMatch(txtPort.Text, @"^\d+$"))
            {
                MessageBox.Show("输入的端口号格式错误");
                return;
            }
            if (!Regex.IsMatch(txtPlcUnit.Text, @"^\d+$"))
            {
                MessageBox.Show("输入的PLC单元号格式错误");
                return;
            }
            if (!Regex.IsMatch(txtPlcUnit.Text, @"^\d+$"))
            {
                MessageBox.Show("输入的PC单元号格式错误");
                return;
            }
            omronPlcConfig.IpAddr = txtIpAddress.Text;
            omronPlcConfig.Port = Convert.ToInt32(txtPort.Text);
            omronPlcConfig.SA1 = Convert.ToByte(txtPcUnit.Text);
            omronPlcConfig.DA1 = Convert.ToByte(txtPlcUnit.Text);
            XmlHelper.SerializeToXml<ConfigInfo>(ConfigVars.configInfo);
            MessageBox.Show("参数保存成功");
        }

        private void btnBoolRead_Click(object sender, EventArgs e)
        {
            if (omronInstance == null)
            {
                omronInstance = OmronPlcFactory.GetInstance(plcNo);
            }
            try
            {
                bool[] bools = omronInstance.ReadBool(txtAddress.Text, Convert.ToUInt16(txtLength.Text)).Content;
                foreach (bool b in bools)
                {
                    rtxResultMsg.AppendText(b.ToString() + "\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBoolWrite_Click(object sender, EventArgs e)
        {
            if (omronInstance == null)
            {
                omronInstance = OmronPlcFactory.GetInstance(plcNo);
            }
            try
            {
                omronInstance.Write(txtAddress.Text, Convert.ToBoolean(txtWriteValue.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWordRead_Click(object sender, EventArgs e)
        {
            if (omronInstance == null)
            {
                omronInstance = OmronPlcFactory.GetInstance(plcNo);
            }
            try
            {
                OperateResult<float[]> words =omronInstance.ReadFloat(txtAddress.Text,ushort.Parse(txtLength.Text));

                rtxResultMsg.AppendText(words.Content[0].ToString() + "\n");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWordWrite_Click(object sender, EventArgs e)
        {
            if (omronInstance == null)
            {
                omronInstance = OmronPlcFactory.GetInstance(plcNo);
            }
            try
            {
                omronInstance.Write(txtAddress.Text,float.Parse(txtWriteValue.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtxResultMsg.Text = string.Empty;
        }

        private void btnConn_Click(object sender, EventArgs e)
        {

        }
    }
}
