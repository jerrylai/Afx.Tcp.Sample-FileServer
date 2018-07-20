namespace Client.WinMain
{
    partial class AddRoleForm
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
            this.ucFormTitle = new Afx.Forms.Controls.UCFormTitle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbFormTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.dropDownListType = new Afx.Forms.Controls.AfxDropDownList();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxRead = new System.Windows.Forms.CheckBox();
            this.checkBoxWrite = new System.Windows.Forms.CheckBox();
            this.checkBoxSystem = new System.Windows.Forms.CheckBox();
            this.btnSave = new Afx.Forms.Controls.AfxButton();
            this.btnClose = new Afx.Forms.Controls.AfxButton();
            this.lbMsg = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ucFormTitle
            // 
            this.ucFormTitle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ucFormTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucFormTitle.IsShowCloseBox = true;
            this.ucFormTitle.IsShowMaxBox = false;
            this.ucFormTitle.IsShowMinBox = false;
            this.ucFormTitle.Location = new System.Drawing.Point(2, 0);
            this.ucFormTitle.Margin = new System.Windows.Forms.Padding(0);
            this.ucFormTitle.Name = "ucFormTitle";
            this.ucFormTitle.Size = new System.Drawing.Size(300, 27);
            this.ucFormTitle.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.dropDownListType);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 205);
            this.panel1.TabIndex = 1;
            // 
            // lbFormTitle
            // 
            this.lbFormTitle.AutoSize = true;
            this.lbFormTitle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFormTitle.Location = new System.Drawing.Point(6, 5);
            this.lbFormTitle.Name = "lbFormTitle";
            this.lbFormTitle.Size = new System.Drawing.Size(32, 17);
            this.lbFormTitle.TabIndex = 2;
            this.lbFormTitle.Text = "添加";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(13, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "类型：";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(63, 19);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(211, 23);
            this.txtName.TabIndex = 2;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // dropDownListType
            // 
            this.dropDownListType.DisplayMember = "Text";
            this.dropDownListType.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dropDownListType.FormattingEnabled = true;
            this.dropDownListType.Location = new System.Drawing.Point(63, 50);
            this.dropDownListType.Name = "dropDownListType";
            this.dropDownListType.Size = new System.Drawing.Size(211, 24);
            this.dropDownListType.TabIndex = 3;
            this.dropDownListType.ValueMember = "Value";
            this.dropDownListType.SelectedValueChanged += new System.EventHandler(this.dropDownListType_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxSystem);
            this.groupBox1.Controls.Add(this.checkBoxWrite);
            this.groupBox1.Controls.Add(this.checkBoxRead);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(16, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 56);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "权限";
            // 
            // checkBoxRead
            // 
            this.checkBoxRead.AutoSize = true;
            this.checkBoxRead.Location = new System.Drawing.Point(10, 25);
            this.checkBoxRead.Name = "checkBoxRead";
            this.checkBoxRead.Size = new System.Drawing.Size(51, 21);
            this.checkBoxRead.TabIndex = 0;
            this.checkBoxRead.Text = "读取";
            this.checkBoxRead.UseVisualStyleBackColor = true;
            // 
            // checkBoxWrite
            // 
            this.checkBoxWrite.AutoSize = true;
            this.checkBoxWrite.Location = new System.Drawing.Point(95, 25);
            this.checkBoxWrite.Name = "checkBoxWrite";
            this.checkBoxWrite.Size = new System.Drawing.Size(51, 21);
            this.checkBoxWrite.TabIndex = 1;
            this.checkBoxWrite.Text = "写入";
            this.checkBoxWrite.UseVisualStyleBackColor = true;
            // 
            // checkBoxSystem
            // 
            this.checkBoxSystem.AutoSize = true;
            this.checkBoxSystem.Location = new System.Drawing.Point(172, 25);
            this.checkBoxSystem.Name = "checkBoxSystem";
            this.checkBoxSystem.Size = new System.Drawing.Size(75, 21);
            this.checkBoxSystem.TabIndex = 2;
            this.checkBoxSystem.Text = "系统管理";
            this.checkBoxSystem.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(49, 157);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保 存";
            this.btnSave.Type = Afx.Forms.Controls.AfxButtonType.OK;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkSalmon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(166, 157);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "取 消";
            this.btnClose.Type = Afx.Forms.Controls.AfxButtonType.Cancel;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbMsg
            // 
            this.lbMsg.AutoSize = true;
            this.lbMsg.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbMsg.ForeColor = System.Drawing.Color.Red;
            this.lbMsg.Location = new System.Drawing.Point(33, 185);
            this.lbMsg.Name = "lbMsg";
            this.lbMsg.Size = new System.Drawing.Size(68, 16);
            this.lbMsg.TabIndex = 7;
            this.lbMsg.Text = "1111111111";
            // 
            // AddRoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(304, 234);
            this.Controls.Add(this.lbFormTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucFormTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddRoleForm";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddRoleForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Afx.Forms.Controls.UCFormTitle ucFormTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbFormTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxSystem;
        private System.Windows.Forms.CheckBox checkBoxWrite;
        private System.Windows.Forms.CheckBox checkBoxRead;
        private Afx.Forms.Controls.AfxDropDownList dropDownListType;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Afx.Forms.Controls.AfxButton btnClose;
        private Afx.Forms.Controls.AfxButton btnSave;
        private System.Windows.Forms.Label lbMsg;
    }
}