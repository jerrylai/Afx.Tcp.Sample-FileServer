namespace Client.WinMain
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ucFormTitle1 = new Afx.Forms.Controls.UCFormTitle();
            this.lb_FormTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_Body = new System.Windows.Forms.Panel();
            this.ucFileInfo = new Client.WinMain.UCFileInfo();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TSMI_FileInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_RoleInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_UserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_SysConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_UpdateInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_UpdatePwd = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel_Body.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucFormTitle1
            // 
            this.ucFormTitle1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ucFormTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucFormTitle1.IsShowCloseBox = true;
            this.ucFormTitle1.IsShowMaxBox = true;
            this.ucFormTitle1.IsShowMinBox = true;
            this.ucFormTitle1.Location = new System.Drawing.Point(2, 0);
            this.ucFormTitle1.Margin = new System.Windows.Forms.Padding(0);
            this.ucFormTitle1.Name = "ucFormTitle1";
            this.ucFormTitle1.Size = new System.Drawing.Size(996, 27);
            this.ucFormTitle1.TabIndex = 0;
            // 
            // lb_FormTitle
            // 
            this.lb_FormTitle.AutoSize = true;
            this.lb_FormTitle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lb_FormTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_FormTitle.Location = new System.Drawing.Point(9, 6);
            this.lb_FormTitle.Name = "lb_FormTitle";
            this.lb_FormTitle.Size = new System.Drawing.Size(80, 17);
            this.lb_FormTitle.TabIndex = 1;
            this.lb_FormTitle.Text = "文件管理系统";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.panel_Body);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(996, 571);
            this.panel1.TabIndex = 2;
            // 
            // panel_Body
            // 
            this.panel_Body.BackColor = System.Drawing.Color.White;
            this.panel_Body.Controls.Add(this.ucFileInfo);
            this.panel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Body.Location = new System.Drawing.Point(0, 25);
            this.panel_Body.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(996, 524);
            this.panel_Body.TabIndex = 2;
            // 
            // ucFileInfo
            // 
            this.ucFileInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFileInfo.Location = new System.Drawing.Point(0, 0);
            this.ucFileInfo.Name = "ucFileInfo";
            this.ucFileInfo.Size = new System.Drawing.Size(996, 524);
            this.ucFileInfo.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 549);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(996, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_Status
            // 
            this.toolStripStatusLabel_Status.Name = "toolStripStatusLabel_Status";
            this.toolStripStatusLabel_Status.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel_Status.Text = "状态";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_FileInfo,
            this.TSMI_RoleInfo,
            this.TSMI_UserInfo,
            this.TSMI_SysConfig,
            this.TSMI_UpdateInfo,
            this.toolStripMenuItem_UpdatePwd});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(996, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip";
            // 
            // TSMI_FileInfo
            // 
            this.TSMI_FileInfo.Name = "TSMI_FileInfo";
            this.TSMI_FileInfo.Size = new System.Drawing.Size(68, 21);
            this.TSMI_FileInfo.Text = "文件管理";
            this.TSMI_FileInfo.Click += new System.EventHandler(this.TSMI_FileInfo_Click);
            // 
            // TSMI_RoleInfo
            // 
            this.TSMI_RoleInfo.Name = "TSMI_RoleInfo";
            this.TSMI_RoleInfo.Size = new System.Drawing.Size(68, 21);
            this.TSMI_RoleInfo.Text = "角色管理";
            this.TSMI_RoleInfo.Click += new System.EventHandler(this.TSMI_RoleInfo_Click);
            // 
            // TSMI_UserInfo
            // 
            this.TSMI_UserInfo.Name = "TSMI_UserInfo";
            this.TSMI_UserInfo.Size = new System.Drawing.Size(68, 21);
            this.TSMI_UserInfo.Text = "用户管理";
            this.TSMI_UserInfo.Click += new System.EventHandler(this.TSMI_UserInfo_Click);
            // 
            // TSMI_SysConfig
            // 
            this.TSMI_SysConfig.Name = "TSMI_SysConfig";
            this.TSMI_SysConfig.Size = new System.Drawing.Size(68, 21);
            this.TSMI_SysConfig.Text = "系统设置";
            // 
            // TSMI_UpdateInfo
            // 
            this.TSMI_UpdateInfo.Name = "TSMI_UpdateInfo";
            this.TSMI_UpdateInfo.Size = new System.Drawing.Size(68, 21);
            this.TSMI_UpdateInfo.Text = "升级设置";
            // 
            // toolStripMenuItem_UpdatePwd
            // 
            this.toolStripMenuItem_UpdatePwd.Name = "toolStripMenuItem_UpdatePwd";
            this.toolStripMenuItem_UpdatePwd.Size = new System.Drawing.Size(68, 21);
            this.toolStripMenuItem_UpdatePwd.Text = "修改密码";
            this.toolStripMenuItem_UpdatePwd.Click += new System.EventHandler(this.toolStripMenuItem_UpdatePwd_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lb_FormTitle);
            this.Controls.Add(this.ucFormTitle1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsCustomWindow = true;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件管理系统";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_Body.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Afx.Forms.Controls.UCFormTitle ucFormTitle1;
        private System.Windows.Forms.Label lb_FormTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMI_FileInfo;
        private System.Windows.Forms.ToolStripMenuItem TSMI_UserInfo;
        private System.Windows.Forms.ToolStripMenuItem TSMI_RoleInfo;
        private System.Windows.Forms.ToolStripMenuItem TSMI_SysConfig;
        private System.Windows.Forms.ToolStripMenuItem TSMI_UpdateInfo;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Status;
        private System.Windows.Forms.Panel panel_Body;
        private UCFileInfo ucFileInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_UpdatePwd;

    }
}

