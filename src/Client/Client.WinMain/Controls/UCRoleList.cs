using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Afx.Forms.Controls;
using Client.Common;
using AfxTcpFileServerSample.Enums;
using AfxTcpFileServerSample.Dto.ProtoBuf;
using Client.IService;
using System.Threading;

namespace Client.WinMain
{
    public partial class UCRoleList : AfxBaseUserControl
    {
        private RoleInfoListParamDto selectParam = new RoleInfoListParamDto();
        public UCRoleList()
        {
            InitializeComponent();

            this.Init();
        }

        private void Init()
        {
            List<AfxDropDownItem> list = new List<AfxDropDownItem>()
            {
                new AfxDropDownItem(){ Text = "全部", Value = RoleType.None},
                new AfxDropDownItem(){ Text = "管理组", Value = RoleType.Admin},
                new AfxDropDownItem(){ Text = "用户组", Value = RoleType.User}
            };
            this.dropDownListRoleType.DataSource = list;
            this.dropDownListRoleType.SelectedItem = list[0];
        }

        private void SetEnabled(bool enabled)
        {
            this.dropDownListRoleType.Enabled = enabled;
            this.txtKeyword.Enabled = enabled;
            this.btnSelect.Enabled = enabled;
            this.listView.Enabled = enabled;
        }

        public void LoadData()
        {
            this.SetEnabled(false);
            ThreadPool.QueueUserWorkItem((o) => {
                using (var client = IocUtils.Get<IRoleService>(new object[] { MainForm.Current.FileClient }))
                {
                    var list = client.GetList(this.selectParam);
                    this.Sync.Post((obj) => {
                        this.BindListView(obj as List<RoleInfoDto>);
                        this.SetEnabled(true);
                    }, list);
                }
            });
        }

        private void BindListView(List<RoleInfoDto> list)
        {
            this.listView.BeginUpdate();
            this.listView.Items.Clear();
            if(list != null)
            {
                foreach(var m in list)
                {
                    ListViewItem item = new ListViewItem(new string[] { m.Name, this.GetRoleTypeText(m.Type), m.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") });
                    item.Tag = m;
                    item.Name = "id_" + m.Id.ToString();
                    this.listView.Items.Add(item);
                }
            }
            this.listView.EndUpdate();
        }

        private string GetRoleTypeText(RoleType type)
        {
            switch(type)
            {
                case RoleType.Admin:
                    return "管理组";
                case RoleType.User:
                    return "用户组";
                default:
                    return "";
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            this.toolStripMenuItemEdit.Enabled = false;
            this.toolStripMenuItemDelete.Enabled = false;
            if(this.listView.SelectedItems.Count > 0)
            {
                ListViewItem item = this.listView.SelectedItems[0];
                RoleInfoDto m = item.Tag as RoleInfoDto;
                if(m.IsSystem == false)
                {
                    this.toolStripMenuItemEdit.Enabled = true;
                    this.toolStripMenuItemDelete.Enabled = true;  
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.selectParam.Type = (RoleType)this.dropDownListRoleType.SelectedValue;
            this.selectParam.Keyword = this.txtKeyword.Text.Trim();
            this.LoadData();
        }

        private int descColumn = 0;
        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.descColumn != e.Column || string.IsNullOrEmpty(this.selectParam.Orderby))
            {
                this.descColumn = e.Column;
                this.selectParam.IsDesc = false;
                switch (this.descColumn)
                {
                    case 0:
                        this.selectParam.Orderby = "Name";
                        break;
                    case 1:
                        this.selectParam.Orderby = "Type";
                        break;
                    case 2:
                        this.selectParam.Orderby = "UpdateTime";
                        break;
                }
            }
            else
            {
                this.selectParam.IsDesc = !this.selectParam.IsDesc;
            }
            
            this.LoadData();
        }
    }
}
