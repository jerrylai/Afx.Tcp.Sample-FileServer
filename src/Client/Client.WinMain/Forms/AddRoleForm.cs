using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Afx.Forms.Controls;
using Client.IService;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using Client.Common;
using AfxTcpFileServerSample.Enums;

namespace Client.WinMain
{
    public partial class AddRoleForm : AfxBaseForm
    {
        public AddRoleForm()
        {
            InitializeComponent();
        }

        private RoleInfoDto roleInfo = null;
        public RoleInfoDto RoleInfo
        {
            get { return this.roleInfo; }
            set
            {
                this.roleInfo = value ?? new RoleInfoDto();
                if(this.roleInfo.AuthList == null) this.roleInfo.AuthList = new List<AuthType>();
                if(this.roleInfo.Id == 0)
                {
                    this.txtName.Clear();
                    this.dropDownListType.SelectedIndex = 1;
                    this.checkBoxRead.Checked = true;
                    this.checkBoxWrite.Checked = true;
                    this.checkBoxSystem.Checked = false;
                }
                else
                {
                    this.txtName.Text = this.roleInfo.Name;
                    this.dropDownListType.SelectedValue = this.roleInfo.Type;
                    this.checkBoxRead.Checked = this.roleInfo.AuthList.Contains(AuthType.ReadFile);
                    this.checkBoxWrite.Checked = this.roleInfo.AuthList.Contains(AuthType.WriteFile);
                    this.checkBoxSystem.Checked = this.roleInfo.AuthList.Contains(AuthType.System);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = this.txtName.Text.Trim();
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            this.lbMsg.Text = "";
        }

        private void dropDownListType_SelectedValueChanged(object sender, EventArgs e)
        {
            this.lbMsg.Text = "";
        }
    }
}
