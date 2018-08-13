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
using Afx.Ioc;
using AfxTcpFileServerSample.Enums;
using Client.Common;
using Client.IService;

namespace Client.WinMain
{
    public partial class MainForm : AfxBaseForm
    {
        public IFileClient FileClient { get; private set; }
        public static MainForm Current { get; private set; }
        public MainForm()
        {
            InitializeComponent();

            Current = this;
            this.FileClient = IocUtils.Get<IFileClient>();
            this.Login();

            try { File.WriteAllText(WinAPIs.GetHandleFile(), this.Handle.ToString(), Encoding.UTF8); }
            catch { }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WinAPIs.WM_OPEN_FORM)
            {
                ThreadPool.QueueUserWorkItem((obj) => {
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
                this.FileClient.Close();
                Application.Exit();
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if(this.FileClient.IsLogin)
            {
                ConfigUtils.LocalPath = this.ucFileInfo.LocalPath;
                if (this.ucFileInfo.ServerPath != null) ConfigUtils.ServerPathId = this.ucFileInfo.ServerPath.Id;
                else ConfigUtils.ServerPathId = 0;
            }
            this.FileClient.Dispose();
            base.OnClosed(e);
        }

        private void Login()
        {
            using(var frm = new LoginForm())
            {
                this.FileClient.ClosedCallback = null;
                if(frm.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                {
                    this.FileClient.Close();
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    this.FileClient.ClosedCallback = (c, ex) =>
                    {
                        this.Sync.Post((obj) =>
                        {
                            ConfigUtils.LocalPath = this.ucFileInfo.LocalPath;
                            if (this.ucFileInfo.ServerPath != null) ConfigUtils.ServerPathId = this.ucFileInfo.ServerPath.Id;
                            else ConfigUtils.ServerPathId = 0;
                            if (this.ShowYesNo("已断线，是否重新登录？") == System.Windows.Forms.DialogResult.Yes)
                            {
                                this.Login();
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }, null);
                    };
                    this.LoadUserInfo();
                    this.ucFileInfo.SetServerPath(ConfigUtils.ServerPathId);
                    this.ucFileInfo.Init();
                }
            }
        }

        public void SetStatusText(string txt)
        {
            if(this.ManagedThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId)
            {
                this.toolStripStatusLabel_Status.Text = txt;
            }
            else
            {
                this.Sync.Send((o) => { this.toolStripStatusLabel_Status.Text = (o as string) ?? ""; }, txt);
            }
        }

        public void SetMenuEnabled(bool enabled)
        {
            if (this.ManagedThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId)
            {
                this.menuStrip1.Enabled = enabled;
            }
            else
            {
                this.Sync.Send((o) => { this.menuStrip1.Enabled = (bool)o;  }, enabled);
            }
        }
        
        private void LoadUserInfo()
        {
            this.lb_FormTitle.Text = string.Format("文件管理系统 - {0}({1})", this.FileClient.UserInfo.Name, this.FileClient.UserInfo.Account);
            bool isSys = this.FileClient.UserInfo.RoleAuth.Contains(AuthType.System);
            this.TSMI_UserInfo.Visible = isSys;
            this.TSMI_RoleInfo.Visible = isSys;
            this.TSMI_SysConfig.Visible = false;// isSys;
            this.TSMI_UpdateInfo.Visible = false;// isSys;

        }

        private void toolStripMenuItem_UpdatePwd_Click(object sender, EventArgs e)
        {
            using(var frm = new UpdatePwdForm())
            {
                frm.ShowDialog(this);
            }
        }

        private UCRoleList ucRoleList = null;
        private void TSMI_RoleInfo_Click(object sender, EventArgs e)
        {
            if(this.ucRoleList == null)
            {
                this.ucRoleList = new UCRoleList();
                this.ucRoleList.Dock = DockStyle.Fill;
                this.panel_Body.Controls.Add(this.ucRoleList);
                this.ucRoleList.LoadData();
            }
            this.ucRoleList.BringToFront();
        }

        private void TSMI_FileInfo_Click(object sender, EventArgs e)
        {
            this.ucFileInfo.BringToFront();
        }

        private UCUserList ucUserList = null;
        private void TSMI_UserInfo_Click(object sender, EventArgs e)
        {
            if (this.ucUserList == null)
            {
                this.ucUserList = new UCUserList();
                this.ucUserList.Dock = DockStyle.Fill;
                this.panel_Body.Controls.Add(this.ucUserList);
                this.ucUserList.LoadData();
            }
            this.ucUserList.BringToFront();
        }
    }
}
