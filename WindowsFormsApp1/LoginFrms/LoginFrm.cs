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
using System.Windows.Forms;

namespace Camera_Capture_demo.LoginFrms
{
    public partial class LoginFrm : Form
    {
        int level;
        static int selectedIndex = 0;
        public LoginFrm(int level)
        {
            InitializeComponent();
            this.level = level;
        }

        private void llblModifyPwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ModifyPwdFrm frm = new ModifyPwdFrm(cboUser.SelectedIndex);
            frm.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool flag = false;
            switch (cboUser.SelectedIndex)
            {
                case 0:
                    //操作人员
                    flag = (txtPassword.Text == ConfigVars.configInfo.UserInfos.OperatorPwd);
                    break;
                case 1:
                    //管理人员
                    flag = (txtPassword.Text == ConfigVars.configInfo.UserInfos.AdministratorPwd);
                    break;
                case 2:
                    //开发人员
                    flag = (txtPassword.Text == "332400");
                    break;
                default:
                    break;
            }
            if (!flag)
            {
                MessageBox.Show("密码输入错误");
                this.DialogResult = DialogResult.No;
                return;
            }
            if (cboUser.SelectedIndex < level)
            {
                MessageBox.Show("用户登录权限不足");
                this.DialogResult = DialogResult.No;
                return;
            }
            this.DialogResult = DialogResult.OK;
            selectedIndex = cboUser.SelectedIndex;
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            cboUser.SelectedIndex = selectedIndex;
            ConfigVars.configInfo = XmlHelper.DeserializeFromXml<ConfigInfo>();
           
            if (ConfigVars.configInfo.UserInfos == null)
            {
                ConfigVars.configInfo.UserInfos = new UserInfos()
                {
                    OperatorPwd = "1",
                    AdministratorPwd = "1",
                    DeveloperPwd = "1"
                };
            }
            XmlHelper.SerializeToXml(ConfigVars.configInfo);
        }
    }
}
