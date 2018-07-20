using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Afx.Forms.Controls;
using Client.Common;

namespace Client.WinMain
{
    public partial class SettingForm : AfxBaseForm
    {
        public SettingForm()
        {
            InitializeComponent();

            this.LoadServer();
        }

        public void LoadServer()
        {
            this.txt_Server.Text = ConfigUtils.ServerAddress;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string txt = this.txt_Server.Text.Trim();
            if(string.IsNullOrEmpty(txt))
            {
                this.ShowMessage("服务器地址不能为空！");
                return;
            }

            ConfigUtils.ServerAddress = txt;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
