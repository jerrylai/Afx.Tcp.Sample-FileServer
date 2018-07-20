namespace Client.WinMain
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.ucFormTitle1 = new Afx.Forms.Controls.UCFormTitle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Login = new Afx.Forms.Controls.AfxButton();
            this.lb_Msg = new System.Windows.Forms.Label();
            this.pic_Loading = new System.Windows.Forms.PictureBox();
            this.linkLabel_Setting = new System.Windows.Forms.LinkLabel();
            this.txt_Account = new System.Windows.Forms.TextBox();
            this.checkBox_User = new System.Windows.Forms.CheckBox();
            this.txt_Pwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Loading)).BeginInit();
            this.SuspendLayout();
            // 
            // ucFormTitle1
            // 
            this.ucFormTitle1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ucFormTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucFormTitle1.IsShowCloseBox = true;
            this.ucFormTitle1.IsShowMaxBox = false;
            this.ucFormTitle1.IsShowMinBox = false;
            this.ucFormTitle1.Location = new System.Drawing.Point(2, 0);
            this.ucFormTitle1.Margin = new System.Windows.Forms.Padding(0);
            this.ucFormTitle1.Name = "ucFormTitle1";
            this.ucFormTitle1.Size = new System.Drawing.Size(374, 27);
            this.ucFormTitle1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Client.WinMain.Properties.Resources.login_bg;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.btn_Login);
            this.panel1.Controls.Add(this.lb_Msg);
            this.panel1.Controls.Add(this.pic_Loading);
            this.panel1.Controls.Add(this.linkLabel_Setting);
            this.panel1.Controls.Add(this.txt_Account);
            this.panel1.Controls.Add(this.checkBox_User);
            this.panel1.Controls.Add(this.txt_Pwd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 237);
            this.panel1.TabIndex = 1;
            // 
            // btn_Login
            // 
            this.btn_Login.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Login.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(34)));
            this.btn_Login.ForeColor = System.Drawing.Color.White;
            this.btn_Login.Location = new System.Drawing.Point(171, 171);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(166, 32);
            this.btn_Login.TabIndex = 21;
            this.btn_Login.Text = "登  录";
            this.btn_Login.Type = Afx.Forms.Controls.AfxButtonType.Yes;
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // lb_Msg
            // 
            this.lb_Msg.AutoSize = true;
            this.lb_Msg.BackColor = System.Drawing.Color.Transparent;
            this.lb_Msg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Msg.ForeColor = System.Drawing.Color.Red;
            this.lb_Msg.Location = new System.Drawing.Point(168, 213);
            this.lb_Msg.Name = "lb_Msg";
            this.lb_Msg.Size = new System.Drawing.Size(104, 17);
            this.lb_Msg.TabIndex = 20;
            this.lb_Msg.Text = "账号或密码错误！";
            // 
            // pic_Loading
            // 
            this.pic_Loading.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pic_Loading.Image = global::Client.WinMain.Properties.Resources.loadImg;
            this.pic_Loading.Location = new System.Drawing.Point(143, 59);
            this.pic_Loading.Name = "pic_Loading";
            this.pic_Loading.Size = new System.Drawing.Size(100, 93);
            this.pic_Loading.TabIndex = 18;
            this.pic_Loading.TabStop = false;
            this.pic_Loading.Visible = false;
            // 
            // linkLabel_Setting
            // 
            this.linkLabel_Setting.ActiveLinkColor = System.Drawing.Color.Red;
            this.linkLabel_Setting.AutoSize = true;
            this.linkLabel_Setting.BackColor = System.Drawing.Color.White;
            this.linkLabel_Setting.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel_Setting.Location = new System.Drawing.Point(260, 142);
            this.linkLabel_Setting.Name = "linkLabel_Setting";
            this.linkLabel_Setting.Size = new System.Drawing.Size(32, 17);
            this.linkLabel_Setting.TabIndex = 19;
            this.linkLabel_Setting.TabStop = true;
            this.linkLabel_Setting.Text = "设置";
            this.linkLabel_Setting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Setting_LinkClicked);
            // 
            // txt_Account
            // 
            this.txt_Account.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_Account.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txt_Account.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txt_Account.Location = new System.Drawing.Point(171, 59);
            this.txt_Account.MaxLength = 30;
            this.txt_Account.Name = "txt_Account";
            this.txt_Account.Size = new System.Drawing.Size(166, 23);
            this.txt_Account.TabIndex = 15;
            this.txt_Account.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_Account_KeyUp);
            // 
            // checkBox_User
            // 
            this.checkBox_User.AutoSize = true;
            this.checkBox_User.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox_User.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox_User.Location = new System.Drawing.Point(171, 141);
            this.checkBox_User.Name = "checkBox_User";
            this.checkBox_User.Size = new System.Drawing.Size(75, 21);
            this.checkBox_User.TabIndex = 17;
            this.checkBox_User.Text = "记住密码";
            this.checkBox_User.UseVisualStyleBackColor = false;
            // 
            // txt_Pwd
            // 
            this.txt_Pwd.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.txt_Pwd.Location = new System.Drawing.Point(171, 106);
            this.txt_Pwd.MaxLength = 30;
            this.txt_Pwd.Name = "txt_Pwd";
            this.txt_Pwd.Size = new System.Drawing.Size(166, 23);
            this.txt_Pwd.TabIndex = 16;
            this.txt_Pwd.UseSystemPasswordChar = true;
            this.txt_Pwd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txt_Pwd_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "登录";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btn_Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(378, 266);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucFormTitle1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "登录";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Loading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Afx.Forms.Controls.UCFormTitle ucFormTitle1;
        private System.Windows.Forms.Panel panel1;
        private Afx.Forms.Controls.AfxButton btn_Login;
        private System.Windows.Forms.Label lb_Msg;
        private System.Windows.Forms.PictureBox pic_Loading;
        private System.Windows.Forms.LinkLabel linkLabel_Setting;
        private System.Windows.Forms.CheckBox checkBox_User;
        private System.Windows.Forms.TextBox txt_Account;
        private System.Windows.Forms.TextBox txt_Pwd;
        private System.Windows.Forms.Label label1;

    }
}