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
    public partial class UCUserList : AfxBaseUserControl
    {
        private UserInfoPageParamDto selectParam = new UserInfoPageParamDto();
        public UCUserList()
        {
            InitializeComponent();

            this.Init();
        }

        private void Init()
        {
            this.ucPageFooter.SetPageSize(new List<int>() { 15, 20, 30, 50, 100 }, 15);
            this.selectParam.Index = 1;
            this.selectParam.Size = this.ucPageFooter.PageSize;
        }

        private void SetEnabled(bool enabled)
        {
            this.txtKeyword.Enabled = enabled;
            this.btnSelect.Enabled = enabled;
            this.listView.Enabled = enabled;
            this.ucPageFooter.Enabled = enabled;
        }

        public void LoadData()
        {
            this.SetEnabled(false);
            ThreadPool.QueueUserWorkItem((o) =>
            {
                using (var client = IocUtils.Get<IUserService>(new object[] { MainForm.Current.FileClient }))
                {
                    var page = client.GetPageList(this.selectParam);
                    this.Sync.Post((obj) =>
                    {
                        this.BindListView(obj as PageListDto<UserInfoDto>);
                        this.SetEnabled(true);
                    }, page);
                }
            });
        }

        private void BindListView(PageListDto<UserInfoDto> page)
        {
            this.ucPageFooter.SetPageIndex(page.Index, page.TotalCount);
            this.listView.BeginUpdate();
            this.listView.Items.Clear();
            if (page.List != null)
            {
                foreach (var m in page.List)
                {
                    var arr = new string[] { m.Name, m.Account, m.RoleName,  m.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") };
                    ListViewItem item = new ListViewItem(arr);
                    item.Tag = m;
                    item.Name = "id_" + m.Id.ToString();
                    this.listView.Items.Add(item);
                }
            }
            this.listView.EndUpdate();
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            this.toolStripMenuItemDelete.Enabled = false;
            if (this.listView.SelectedItems.Count > 0)
            {
                ListViewItem item = this.listView.SelectedItems[0];
                UserInfoDto m = item.Tag as UserInfoDto;
                if (m.IsSystem == false)
                {
                    this.toolStripMenuItemDelete.Enabled = true;
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.selectParam.Keyword = this.txtKeyword.Text.Trim();
            this.selectParam.Index = 1;
            this.selectParam.Size = this.ucPageFooter.PageSize;
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
                        this.selectParam.Orderby = "Account";
                        break;
                    case 2:
                        this.selectParam.Orderby = "RoleName";
                        break;
                    case 3:
                        this.selectParam.Orderby = "UpdateTime";
                        break;
                }
            }
            else
            {
                this.selectParam.IsDesc = !this.selectParam.IsDesc;
            }

            this.selectParam.Index = 1;
            this.selectParam.Size = this.ucPageFooter.PageSize;
            this.LoadData();
        }

        private void ucPageFooter_MemberValueChanged(object sender, EventArgs e)
        {
            this.selectParam.Index = this.ucPageFooter.PageIndex;
            this.selectParam.Size = this.ucPageFooter.PageSize;
            this.LoadData();
        }
    }
}
