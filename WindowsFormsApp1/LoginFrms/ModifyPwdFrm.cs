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
    public partial class ModifyPwdFrm : Form
    {
        int level;
        string oldPwd;
        public ModifyPwdFrm(int level)
        {
            InitializeComponent();
            this.level = level;
            switch (level)
            {
                case 0:
                    lblUserName.Text = "操作人员";
                    oldPwd = ConfigVars.configInfo.UserInfos.OperatorPwd;
                    break;
                case 1:
                    lblUserName.Text = "管理人员";
                    oldPwd = ConfigVars.configInfo.UserInfos.AdministratorPwd;
                    break;
                case 2:
                    lblUserName.Text = "开发人员";
                    break;
                default:
                    break;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (level == 2)
            {
                MessageBox.Show("开发人员登录密码不可修改");
                return;
            }
            if (txtOldPwd.Text != oldPwd)
            {
                MessageBox.Show("旧密码错误");
                return;
            }
            if (txtNewPwd.Text == string.Empty)
            {
                MessageBox.Show("新密码不可为空");
                return;
            }
            if (txtNewPwd.Text != txtVerifyPwd.Text)
            {
                MessageBox.Show("两次输入的新密码不一致");
                return;
            }
            switch (level)
            {
                case 0:
                    ConfigVars.configInfo.UserInfos.OperatorPwd = oldPwd;
                    break;
                case 1:
                    ConfigVars.configInfo.UserInfos.AdministratorPwd = oldPwd;
                    break;
                default:
                    break;
            }
            XmlHelper.SerializeToXml(ConfigVars.configInfo);
            MessageBox.Show("密码修改成功");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
