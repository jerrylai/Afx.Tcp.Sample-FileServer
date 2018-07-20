namespace Client.WinMain
{
    partial class AddUserForm
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
            this.lbFormTitle = new System.Windows.Forms.Label();
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
            this.ucFormTitle1.Size = new System.Drawing.Size(315, 27);
            this.ucFormTitle1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(315, 166);
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
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(319, 195);
            this.Controls.Add(this.lbFormTitle);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ucFormTitle1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddUserForm";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.ShowInTaskbar = false;
            this.Text = "AddUserForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Afx.Forms.Controls.UCFormTitle ucFormTitle1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbFormTitle;
    }
}