using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Afx.Forms.Controls;
using Client.Common;
using Client.IService;

namespace Client.WinMain
{
    public partial class LoginForm : AfxBaseForm
    {
        private IFileClient client;
        public LoginForm()
        {
            InitializeComponent();

            this.client = MainForm.Current.FileClient;
            try { File.WriteAllText(WinAPIs.GetHandleFile(), this.Handle.ToString(), Encoding.UTF8); }
            catch { }

            this.lb_Msg.Text = "";

            this.SetEnabled(true);

            if(ConfigUtils.IsRememberPassword)
            {
                this.txt_Account.Text = ConfigUtils.Account;
                this.txt_Pwd.Text = ConfigUtils.Password;
                this.checkBox_User.Checked = true;
            }
            
        }

        private void SetEnabled(bool enabled)
        {
            this.txt_Account.Enabled = enabled;
            this.txt_Pwd.Enabled = enabled;
            this.checkBox_User.Enabled = enabled;
            this.linkLabel_Setting.Enabled = enabled;
            this.btn_Login.Enabled = enabled;
            this.pic_Loading.Visible = !enabled;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WinAPIs.WM_OPEN_FORM)
            {
                ThreadPool.QueueUserWorkItem((obj) =>
                {
                    Thread.Sleep(300);
                    this.Sync.Post((o) =>
                    {
                        this.BringToFront();
                        this.Activate();
                    }, null);
                });
            }
            else if (m.Msg == WinAPIs.WM_CLOSE_SYSTEM)
            {
                Application.Exit();
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void txt_Account_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                this.txt_Pwd.Focus();
            }
        }

        private void txt_Pwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                this.btn_Login.Focus();
            }
        }

        private string account = null;
        private string pwd = null;
        private void btn_Login_Click(object sender, EventArgs e)
        {
            this.account = this.txt_Account.Text.Trim();
            this.pwd = this.txt_Pwd.Text.Trim();

            if (string.IsNullOrEmpty(this.account))
            {
                this.txt_Account.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.pwd))
            {
                this.txt_Pwd.Focus();
                return;
            }

            this.SetEnabled(false);
            ThreadPool.QueueUserWorkItem((o) =>
            {
                    if(!this.client.IsConnected && !this.client.Connect(ConfigUtils.ServerAddress, 10000))
                    {
                        this.Sync.Post((obj) => 
                        {
                            this.SetEnabled(true);
                            this.lb_Msg.Text = "无法连接服务器!";
                        }, null);
                        return;
                    }

                if (this.client.Login(this.account, this.pwd))
                {
                    this.Sync.Post((obj) =>
                    {
                        if(this.checkBox_User.Checked)
                        {
                            ConfigUtils.IsRememberPassword = true;
                            ConfigUtils.Account = this.account;
                            ConfigUtils.Password = this.pwd;
                        }
                        else
                        {
                            ConfigUtils.IsRememberPassword = false;
                            ConfigUtils.Account = "";
                            ConfigUtils.Password = "";
                        }
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }, null);
                }
                else
                {
                    this.client.Close();
                    this.Sync.Post((obj) =>
                    {
                        this.SetEnabled(true);
                        this.lb_Msg.Text = "账号或密码错误!";
                    }, null);
                }
            });
        }

        private void linkLabel_Setting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using(var frm = new SettingForm())
            {
                frm.ShowDialog(this);
            }
        }
    }
}
