using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Client.IService;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using Client.Common;
using Afx.Forms.Controls;

namespace Client.WinMain
{
    public partial class UpdatePwdForm : AfxBaseForm
    {
        public UpdatePwdForm()
        {
            InitializeComponent();

            this.lb_msg.Text = "";
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string oldpwd = this.txt_OldPwd.Text.Trim();
            string newpwd = this.txt_NewPwd.Text.Trim();
            string newpwd2 = this.txt_NewPwd2.Text.Trim();
            if(string.IsNullOrEmpty(oldpwd))
            {
                this.txt_OldPwd.Focus();
                this.lb_msg.Text = "原密码不能为空！";
                return;
            }

            if (string.IsNullOrEmpty(newpwd))
            {
                this.txt_NewPwd.Focus();
                this.lb_msg.Text = "新密码不能为空！";
                return;
            }

            if (newpwd != newpwd2)
            {
                this.txt_NewPwd2.Focus();
                this.lb_msg.Text = "两次输入新密码不一致！";
                return;
            }

            if (newpwd == oldpwd)
            {
                this.txt_NewPwd2.Focus();
                this.lb_msg.Text = "新旧密码不能相同！";
                return;
            }

            using (var userService = IocUtils.Get<IUserService>(new object[] { MainForm.Current.FileClient }))
            {
                if (userService.UpdatePwd(oldpwd, newpwd))
                {
                    if(ConfigUtils.IsRememberPassword && ConfigUtils.Account.ToLower() == MainForm.Current.FileClient.UserInfo.Account.ToLower())
                    {
                        ConfigUtils.Password = newpwd;
                    }
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    this.lb_msg.Text = "修改密码失败！";
                }
            }
        }

        private void txt_OldPwd_TextChanged(object sender, EventArgs e)
        {
            this.lb_msg.Text = "";
        }
    }
}
