namespace Client.WinMain
{
    partial class UpdatePwdForm
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
            this.ucFormTitle1 = new Afx.Forms.Controls.UCFormTitle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.lb_msg = new System.Windows.Forms.Label();
            this.txt_NewPwd2 = new System.Windows.Forms.TextBox();
            this.txt_NewPwd = new System.Windows.Forms.TextBox();
            this.txt_OldPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            this.ucFormTitle1.Name = "ucFormTitle1";
            this.ucFormTitle1.Size = new System.Drawing.Size(289, 27);
            this.ucFormTitle1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btn_Save);
            this.panel1.Controls.Add(this.lb_msg);
            this.panel1.Controls.Add(this.txt_NewPwd2);
            this.panel1.Controls.Add(this.txt_NewPwd);
            this.panel1.Controls.Add(this.txt_OldPwd);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 167);
            this.panel1.TabIndex = 1;
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Save.Location = new System.Drawing.Point(106, 136);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "保 存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // lb_msg
            // 
            this.lb_msg.AutoSize = true;
            this.lb_msg.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_msg.ForeColor = System.Drawing.Color.Red;
            this.lb_msg.Location = new System.Drawing.Point(92, 113);
            this.lb_msg.Name = "lb_msg";
            this.lb_msg.Size = new System.Drawing.Size(85, 16);
            this.lb_msg.TabIndex = 6;
            this.lb_msg.Text = "修改密码失败！";
            // 
            // txt_NewPwd2
            // 
            this.txt_NewPwd2.Location = new System.Drawing.Point(94, 86);
            this.txt_NewPwd2.Name = "txt_NewPwd2";
            this.txt_NewPwd2.Size = new System.Drawing.Size(139, 21);
            this.txt_NewPwd2.TabIndex = 5;
            this.txt_NewPwd2.UseSystemPasswordChar = true;
            this.txt_NewPwd2.TextChanged += new System.EventHandler(this.txt_OldPwd_TextChanged);
            // 
            // txt_NewPwd
            // 
            this.txt_NewPwd.Location = new System.Drawing.Point(94, 53);
            this.txt_NewPwd.Name = "txt_NewPwd";
            this.txt_NewPwd.Size = new System.Drawing.Size(139, 21);
            this.txt_NewPwd.TabIndex = 4;
            this.txt_NewPwd.UseSystemPasswordChar = true;
            this.txt_NewPwd.TextChanged += new System.EventHandler(this.txt_OldPwd_TextChanged);
            // 
            // txt_OldPwd
            // 
            this.txt_OldPwd.Location = new System.Drawing.Point(94, 19);
            this.txt_OldPwd.Name = "txt_OldPwd";
            this.txt_OldPwd.Size = new System.Drawing.Size(139, 21);
            this.txt_OldPwd.TabIndex = 3;
            this.txt_OldPwd.UseSystemPasswordChar = true;
            this.txt_OldPwd.TextChanged += new System.EventHandler(this.txt_OldPwd_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(20, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "确认密码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(32, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "新密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "原密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "修改密码";
            // 
            // UpdatePwdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(293, 196);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucFormTitle1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdatePwdForm";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UpdatePwdForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Afx.Forms.Controls.UCFormTitle ucFormTitle1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label lb_msg;
        private System.Windows.Forms.TextBox txt_NewPwd2;
        private System.Windows.Forms.TextBox txt_NewPwd;
        private System.Windows.Forms.TextBox txt_OldPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}