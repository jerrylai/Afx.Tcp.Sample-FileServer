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
    public partial class UCFileInfo : AfxBaseUserControl
    {
        public UCFileInfo()
        {
            InitializeComponent();
            this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            this.listView_Local.VirtualMode = true;
            this.listView_Local.FullRowSelect = true;
            this.listView_Local.MultiSelect = false;
            this.listView_Local.RetrieveVirtualItem += listView_Local_RetrieveVirtualItem;

            this.listView_Server.VirtualMode = true;
            this.listView_Server.FullRowSelect = true;
            this.listView_Server.MultiSelect = false;
            this.listView_Server.RetrieveVirtualItem += listView_Server_RetrieveVirtualItem;

            this.ucPageFooter_Server.SetPageSize(new List<int>() { 20, 30, 50, 100 }, 20);
            this.ucPageFooter_Server.SetPageIndex(1, 0);
            this.ucPageFooter_Server.MemberValueChanged += ucPageFooter_Server_MemberValueChanged;

            this.pic_Loading.Visible = false;

            this.dropDownList_LocalDisk.SelectedValueChanged += dropDownList_LocalDisk_SelectedValueChanged;
        }

        private void dropDownList_LocalDisk_SelectedValueChanged(object sender, EventArgs e)
        {
            this.dropDownList_LcoalFileInfoType.SelectedIndex = 0;
            this.txt_LocalKeyword.Clear();
            this.LocalPath = this.dropDownList_LocalDisk.SelectedValue as string;
            this.GetLocalFileInfo();
        }

        private void ucPageFooter_Server_MemberValueChanged(object sender, EventArgs e)
        {
            this.GetServerList();
        }

        private void listView_Server_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < this.serverList.Count)
            {
                var m = this.serverList[e.ItemIndex];
                var arr = new string[] {
                    m.Name,
                    m.Type != FileInfoType.None ? m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    m.Type != FileInfoType.None ? m.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    m.Type == FileInfoType.File ? Utils.ToAutoUnit(m.Length) : ""
                };
                e.Item = new ListViewItem(arr);
                e.Item.Tag = m;
                e.Item.ToolTipText = m.Name;
                if (m.Type == FileInfoType.File) e.Item.ImageIndex = 1;
                else e.Item.ImageIndex = 0;
            }
        }

        private void listView_Local_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (e.ItemIndex < this.localList.Count)
            {
                var m = this.localList[e.ItemIndex];
                var arr = new string[] {
                    m.Name,
                    m.Type != FileInfoType.None ? m.CreationTime.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    m.Type != FileInfoType.None ? m.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") : "",
                    m.Type == FileInfoType.File ? Utils.ToAutoUnit(m.Length) : ""
                };
                e.Item = new ListViewItem(arr);
                e.Item.Tag = m;
                e.Item.ToolTipText = m.Name;
                if (m.Type == FileInfoType.File) e.Item.ImageIndex = 1;
                else e.Item.ImageIndex = 0;
            }
        }

        private void SetEnabled(bool enabled, bool isFile = false)
        {
            this.pic_Loading.Visible = !enabled;

            this.dropDownList_LocalDisk.Enabled = enabled;
            this.dropDownList_LcoalFileInfoType.Enabled = enabled;
            this.txt_LocalKeyword.Enabled = enabled;
            this.btn_LocalSearch.Enabled = enabled;
            this.listView_Local.Enabled = enabled;

            this.dropDownList_ServerFileInfoType.Enabled = enabled;
            this.txt_ServerKeyword.Enabled = enabled;
            this.ucPageFooter_Server.Enabled = enabled;
            this.listView_Server.Enabled = enabled;

            this.btn_Stop.Enabled = false;
            if (isFile) this.btn_Stop.Enabled = !enabled;

            this.btn_UploadAll.Enabled = false;
            this.btn_DownloadAll.Enabled = false;
            if (enabled)
            {
                this.btn_UploadAll.Enabled = this.localList.Count > 0 && this.localList[0].Type != FileInfoType.None
                    || this.localList.Count > 1;
                this.btn_DownloadAll.Enabled = this.serverList.Count > 0 && this.serverList[0].Type != FileInfoType.None
                    || this.serverList.Count > 1;
            }

            this.btn_Upload.Enabled = false;
            this.btn_Download.Enabled = false;
            if (enabled)
            {
                this.btn_Upload.Enabled = this.listView_Local.SelectedIndices.Count > 0
                    && this.localList[this.listView_Local.SelectedIndices[0]].Type != FileInfoType.None;
                this.btn_Download.Enabled = this.listView_Server.SelectedIndices.Count > 0
                    && this.serverList[this.listView_Server.SelectedIndices[0]].Type != FileInfoType.None;
            }
            
            MainForm.Current.SetMenuEnabled(enabled);
            if(!enabled)
            {
                MainForm.Current.SetStatusText("正在加载数据...");
            }
            else
            {
                MainForm.Current.SetStatusText("");
            }
        }

        public void Init()
        {
            if (Directory.Exists(ConfigUtils.LocalPath))
            {
                this.LocalPath = ConfigUtils.LocalPath;
            }
            LoadLocalDisk();
            List<AfxDropDownItem> list = new List<AfxDropDownItem>(0);
            list.Add(new AfxDropDownItem() { Text = "全部", Value = FileInfoType.None });
            list.Add(new AfxDropDownItem() { Text = "目录", Value = FileInfoType.Directory });
            list.Add(new AfxDropDownItem() { Text = "文件", Value = FileInfoType.File });
            this.dropDownList_LcoalFileInfoType.DataSource = list.FindAll(q => true);
            this.dropDownList_LcoalFileInfoType.SelectedItem = list[0];
            this.dropDownList_ServerFileInfoType.DataSource = list.FindAll(q => true);
            this.dropDownList_ServerFileInfoType.SelectedItem = list[0];

            this.GetLocalFileInfo(true);
        }

        private string _localPath;
        public string LocalPath 
        {
            get { return this._localPath; }
            private set
            { 
                this._localPath = value;
                SendOrPostCallback action = (o) =>
                {
                    this.txt_LocalPath.Text = this._localPath;
                };
                if (this.ManagedThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId)
                {
                    action(null);
                }
                else
                {
                    this.Sync.Post(action, null);
                }
            }
        }
        private void LoadLocalDisk()
        {
           var arr = Directory.GetLogicalDrives();
           List<AfxDropDownItem> list = new List<AfxDropDownItem>();
            if(arr != null && arr.Length > 0)
            {
                foreach(var s in arr)
                {
                    list.Add(new AfxDropDownItem() { Text = s, Value = s });
                }
            }

            this.dropDownList_LocalDisk.SelectedValueChanged -= dropDownList_LocalDisk_SelectedValueChanged;
            this.dropDownList_LocalDisk.DataSource = list;
            int i = -1;
            if (list.Count > 0 && !string.IsNullOrEmpty(this.LocalPath))
            {
                string s = this.LocalPath;
                int j = s.IndexOf('\\');
                if(j > 0)
                {
                    s = s.Substring(0, j + 1).ToLower();
                    i = list.FindIndex(q => q.Text.ToLower() == s);
                }
            }
            if (list.Count > 0)
            {
                if (i < 0)
                {
                    i = 0;
                    this.LocalPath = list[i].Value as string; 
                }
                this.dropDownList_LocalDisk.SelectedIndex = i;
            }
            this.dropDownList_LocalDisk.SelectedValueChanged += dropDownList_LocalDisk_SelectedValueChanged;
        }

        private List<FileInfoDto> localList = new List<FileInfoDto>();
        private void GetLocalFileInfo(bool isLoadServer = false)
        {
            this.SetEnabled(false);
            object[] param = new object[] { 
                this.dropDownList_LcoalFileInfoType.SelectedValue,
                this.txt_LocalKeyword.Text.Trim(),
                this.dropDownList_LocalDisk.SelectedValue,
                isLoadServer
            };
            System.Threading.ThreadPool.QueueUserWorkItem((o) =>
            {
                object[] _param = o as object[];
                FileInfoType type = (FileInfoType)_param[0];
                string key = _param[1] as string;
                string selectDisk = _param[2] as string; ;
                List<FileInfoDto> list = new List<FileInfoDto>();
                if (!string.IsNullOrEmpty(this.LocalPath) && Directory.Exists(this.LocalPath))
                {
                    if (this.LocalPath != selectDisk)
                    {
                        list.Insert(0, new FileInfoDto()
                        {
                            Type = FileInfoType.None,
                            Name = "...",
                            Directory = this.LocalPath,
                            Key = Guid.NewGuid().ToString("n")
                        });
                    }

                    if (type == FileInfoType.None || type == FileInfoType.Directory)
                    {
                        string[] arr = Directory.GetDirectories(this.LocalPath);
                        if (arr != null)
                        {
                            foreach (var s in arr)
                            {
                                DirectoryInfo d = new DirectoryInfo(s);
                                if (d.Exists && (d.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                                {
                                    if (string.IsNullOrEmpty(key) || d.Name.Contains(key))
                                    {
                                        list.Add(new FileInfoDto()
                                        {
                                            Type = FileInfoType.Directory,
                                            Name = d.Name,
                                            Directory = this.LocalPath,
                                            CreationTime = d.CreationTime,
                                            LastWriteTime = d.LastWriteTime,
                                            Key = Guid.NewGuid().ToString("n")
                                        });
                                    }
                                }
                            }
                        }
                    }

                    if (type == FileInfoType.None || type == FileInfoType.File)
                    {
                        string[] arr = Directory.GetFiles(this.LocalPath);
                        if (arr != null)
                        {
                            foreach (var s in arr)
                            {
                                FileInfo f = new FileInfo(s);
                                if (f.Exists && (f.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                                {
                                    if (string.IsNullOrEmpty(key) || f.Name.Contains(key))
                                    {
                                        list.Add(new FileInfoDto()
                                        {
                                            Type = FileInfoType.File,
                                            Name = f.Name,
                                            Directory = this.LocalPath,
                                            Length = f.Length,
                                            CreationTime = f.CreationTime,
                                            LastWriteTime = f.LastWriteTime
                                        });
                                    }
                                }
                            }
                        }
                    }

                    list = (from q in list orderby q.Type, q.Name select q).ToList();
                }

                object[] sparam = new object[] { list, _param[3] };
                this.Sync.Post((obj) => {
                    object[] _sparam = obj as object[];
                    var l = this.localList;
                    this.listView_Local.BeginUpdate();
                    this.localList = _sparam[0] as List<FileInfoDto>;
                    this.listView_Local.VirtualListSize = this.localList.Count;
                    this.listView_Local.EndUpdate();
                    l.Clear();
                    if ((bool)_sparam[1])
                    {
                        this.GetServerList();
                    }
                    else
                    {
                        this.SetEnabled(true);
                    }
                }, sparam);
            }, param);
        }
        
        private void btn_LocalSearch_Click(object sender, EventArgs e)
        {
            this.GetLocalFileInfo();
        }
        
        private void listView_Local_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = this.listView_Local.GetItemAt(e.X, e.Y);
            if(item != null && item.Tag != null)
            {
                var m = item.Tag as FileInfoDto;
                if (m.Type == FileInfoType.Directory)
                {
                    this.LocalPath =Utils.Combine(this.LocalPath, m.Name);
                    this.GetLocalFileInfo();
                }
                else if (m.Type == (int)FileInfoType.None)
                {
                    this.LocalPath = Utils.GetParent(this.LocalPath);
                    this.GetLocalFileInfo();
                }
            }
        }

        private FileInfoDto _serverPath = null;
        public FileInfoDto ServerPath
        {
            get { return this._serverPath; }
            private set 
            {
                this._serverPath = value;
                SendOrPostCallback action = (o) =>
                {
                    if (this._serverPath != null)
                        this.txt_ServerPath.Text = Path.Combine(this._serverPath.Directory, this.Name).TrimEnd('\\');
                    else
                        this.txt_ServerPath.Text = "\\";
                };
                if (this.ManagedThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId)
                {
                    action(null);
                }
                else
                {
                    this.Sync.Post(action, null);
                }
            }
        }

        public void SetServerPath(int id)
        {
            using (var fileclient = IocUtils.Get<IFileInfoService>(new object[] { MainForm.Current.FileClient }))
            {
                this.ServerPath = fileclient.Get(id);
            }
        }

        private List<FileInfoDto> serverList = new List<FileInfoDto>();
        private void GetServerList()
        {
            this.SetEnabled(false);
            object[] param = new object[] { 
                this.dropDownList_ServerFileInfoType.SelectedValue,
                this.txt_ServerKeyword.Text.Trim(),
                this.ucPageFooter_Server.PageIndex,
                this.ucPageFooter_Server.PageSize
            };
            System.Threading.ThreadPool.QueueUserWorkItem((o) =>
            {
                object[] _param = o as object[];
                FileInfoType type = (FileInfoType)_param[0];
                string key = _param[1] as string;
                using (var fileclient = IocUtils.Get<IFileInfoService>(new object[] { MainForm.Current.FileClient }))
                {
                    var vm = new FileInfoPageParamDto()
                    {
                        Index = (int)_param[2],
                        Size = (int)_param[3],
                        Type = type,
                        Keyword = key
                    };
                    if (this.ServerPath != null) vm.ParentId = this.ServerPath.Id;
                    var page = fileclient.GetPageList(vm);
                    this.Sync.Post((obj) => {
                        var p = obj as PageListDto<FileInfoDto>;
                        List<FileInfoDto> list = new List<FileInfoDto>();
                        if (this.ServerPath != null)
                        {
                            list.Add(new FileInfoDto()
                            {
                                Type = (int)FileInfoType.None,
                                Name = "...",
                                Directory = this.LocalPath,
                                Key = Guid.NewGuid().ToString("n")
                            });
                        }
                        if (p != null && p.List != null && p.List.Count > 0) list.AddRange(p.List);
                        var l = this.serverList;
                        this.listView_Server.BeginUpdate();
                        this.serverList = list;
                        this.listView_Server.VirtualListSize = this.serverList.Count;
                        this.listView_Server.EndUpdate();
                        l.Clear();
                        if (p != null) this.ucPageFooter_Server.SetPageIndex(p.Index, p.TotalCount);
                        else this.ucPageFooter_Server.SetPageIndex(1, 0);
                        this.SetEnabled(true);
                    }, page);
                }
            }, param);
        }

        private void btn_ServerSearch_Click(object sender, EventArgs e)
        {
            this.ucPageFooter_Server.SetPageIndex(1, 0);
            this.GetServerList();
        }

        private void listView_Server_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var item = this.listView_Server.GetItemAt(e.X, e.Y);
            if (item != null && item.Tag != null)
            {
                var m = item.Tag as FileInfoDto;
                if (m.Type == FileInfoType.Directory)
                {
                    this.ServerPath = m;
                    this.GetServerList();
                }
                else if (m.Type == (int)FileInfoType.None)
                {
                    if (this.ServerPath != null)
                    {
                        using (var fileclient = IocUtils.Get<IFileInfoService>(new object[] { MainForm.Current.FileClient }))
                        {
                            this.ServerPath = fileclient.Get(this.ServerPath.ParentId);
                        }
                    }
                    this.GetServerList();
                }
            }
        }

        private void listView_Local_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var m = e.Item.Tag as FileInfoDto;
            this.btn_Upload.Enabled = e.IsSelected && m != null && m.Type != (int)FileInfoType.None;
        }

        private void listView_Server_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            var m = e.Item.Tag as FileInfoDto;
            this.btn_Download.Enabled = e.IsSelected && m != null && m.Type != (int)FileInfoType.None;
        }

        private bool isStop = false;
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            this.isStop = true;
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            if (this.listView_Server.SelectedIndices.Count > 0 && this.serverList[this.listView_Server.SelectedIndices[0]].Type != (int)FileInfoType.None)
            {
                this.isStop = false;
                this.SetEnabled(false, true);
                var m = this.serverList[this.listView_Server.SelectedIndices[0]];
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    try
                    {
                        this.DownloadLocal(o as FileInfoDto, this.LocalPath);
                        this.Sync.Post((obj) => {
                            this.GetLocalFileInfo();
                            this.SetEnabled(true, false); 
                        }, null);
                    }
                    catch (Exception ex)
                    {
                        this.Sync.Post((obj) =>
                        {
                            this.GetLocalFileInfo();
                            this.SetEnabled(true, false);
                            MainForm.Current.SetStatusText("");
                            MainForm.Current.ShowMessage((obj as Exception).Message);
                        }, ex);
                    }
                }, m);
            }
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            if (this.listView_Local.SelectedIndices.Count > 0 && this.localList[this.listView_Local.SelectedIndices[0]].Type != (int)FileInfoType.None)
            {
                this.isStop = false;
                this.SetEnabled(false, true);
                var m = this.localList[this.listView_Local.SelectedIndices[0]];
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    try
                    {
                        string dir = this.ServerPath != null ? Utils.Combine(this.ServerPath.Directory, this.ServerPath.Name) : "\\";
                        this.UploadServer(o as FileInfoDto, dir);
                        this.Sync.Post((obj) => {
                            this.GetServerList();
                            this.SetEnabled(true, false); 
                        }, null);
                    }
                    catch(Exception ex)
                    {
                        this.Sync.Post((obj) => {
                            this.GetServerList();
                            this.SetEnabled(true, false);
                            MainForm.Current.SetStatusText("");
                            MainForm.Current.ShowMessage((obj as Exception).Message);
                        }, ex);
                    }
                }, m);
            }
        }

        private void btn_DownloadAll_Click(object sender, EventArgs e)
        {
            if (this.serverList.Count > 0 && this.serverList[0].Type != FileInfoType.None || this.serverList.Count > 1)
            {
                this.isStop = false;
                this.SetEnabled(false, true);
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    try
                    {
                        foreach (var m in this.serverList)
                        {
                            if (this.isStop) break;
                            if (m.Type != FileInfoType.None)
                            {
                                this.DownloadLocal(m, this.LocalPath);
                            }
                        }
                        this.Sync.Post((obj) => {
                            this.GetLocalFileInfo();
                            this.SetEnabled(true, false); 
                        }, null);
                    }
                    catch (Exception ex)
                    {
                        this.Sync.Post((obj) =>
                        {
                            this.GetLocalFileInfo();
                            this.SetEnabled(true, false);
                            MainForm.Current.SetStatusText("");
                            MainForm.Current.ShowMessage((obj as Exception).Message);
                        }, ex);
                    }
                });
            }
        }

        private void btn_UploadAll_Click(object sender, EventArgs e)
        {
            if (this.localList.Count > 0 && this.localList[0].Type != (int)FileInfoType.None || this.localList.Count > 1)
            {
                this.isStop = false;
                this.SetEnabled(false, true);
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    try
                    {
                        string dir = this.ServerPath != null ? Utils.Combine(this.ServerPath.Directory, this.ServerPath.Name) : "\\";
                        foreach (var m in this.localList)
                        {
                            if (this.isStop) break;
                            this.UploadServer(m, dir);
                        }
                        this.Sync.Post((obj) => {
                            this.GetServerList();
                            this.SetEnabled(true, false); 
                        }, null);
                    }
                    catch (Exception ex)
                    {
                        this.Sync.Post((obj) =>
                        {
                            this.GetServerList();
                            this.SetEnabled(true, false);
                            MainForm.Current.SetStatusText("");
                            MainForm.Current.ShowMessage((obj as Exception).Message);
                        }, ex);
                    }
                });
            }
        }

        private void UploadServer(FileInfoDto m, string serverDir)
        {
            if (m == null || m.Type == (int)FileInfoType.None) return;

            using (var client = IocUtils.Get<IFileInfoService>(new object[] { MainForm.Current.FileClient }))
            {
                Queue<FileInfoDto> queue = new Queue<FileInfoDto>();
                queue.Enqueue(m);

                string name = "";
                string status = "";
                FilePositionCallback call = (len, pos) =>
                {
                    string s = string.Format("上传文件：{0} ({1}/{2})", name, Utils.ToAutoUnit(pos), Utils.ToAutoUnit(len));
                    if (status != s)
                    {
                        status = s;
                        MainForm.Current.SetStatusText(status);
                    }

                    return !this.isStop;
                };

                string localBaseDir = m.Directory;

                while (queue.Count > 0 && !this.isStop)
                {
                    var item = queue.Dequeue();
                    string loaclFull = Utils.Combine(item.Directory, item.Name);
                    string dir = Utils.Combine(serverDir, Utils.GetRelative(item.Directory, localBaseDir));
                    name = Utils.Combine(dir, item.Name);
                    if (item.Type == FileInfoType.File)
                    {
                        client.SendFile(loaclFull, dir, item.Name, call);
                    }
                    else if (item.Type == FileInfoType.Directory)
                    {
                        MainForm.Current.SetStatusText("上传目录：" + name);
                        client.SendDirectory(loaclFull, dir, item.Name);
                        var arr = Directory.GetFiles(loaclFull);
                        if(arr != null && arr.Length > 0)
                        {
                            foreach(var s in arr)
                            {
                                if (this.isStop) break;
                                FileInfoDto vm = new FileInfoDto()
                                {
                                    Type = FileInfoType.File,
                                    Directory = Utils.GetParent(s),
                                    Name = Utils.GetName(s)
                                };
                                queue.Enqueue(vm);
                            }
                        }

                        arr = Directory.GetDirectories(loaclFull);
                        if (arr != null && arr.Length > 0)
                        {
                            foreach (var s in arr)
                            {
                                if (this.isStop) break;
                                FileInfoDto vm = new FileInfoDto()
                                {
                                    Type = FileInfoType.Directory,
                                    Directory = Utils.GetParent(s),
                                    Name = Utils.GetName(s)
                                };
                                queue.Enqueue(vm);
                            }
                        }
                    }
                }
            }

            MainForm.Current.SetStatusText("");
        }

        private void DownloadLocal(FileInfoDto m, string loaclDir)
        {
            if (m == null || m.Type == (int)FileInfoType.None) return;

            using (var client = IocUtils.Get<IFileInfoService>(new object[] { MainForm.Current.FileClient }))
            {
                Queue<FileInfoDto> queue = new Queue<FileInfoDto>();
                queue.Enqueue(m);
                string relDir = m.Directory;
                while (queue.Count > 0 && !this.isStop)
                {
                    FileInfoDto item = queue.Dequeue();
                    string dir = Utils.Combine(loaclDir, Utils.GetRelative(item.Directory, relDir));
                    string fullname = Utils.Combine(dir, item.Name);
                    string remofull = Utils.Combine(item.Directory, item.Name);
                    if (item.Type == FileInfoType.Directory)
                    {
                        MainForm.Current.SetStatusText("下载目录：" + remofull);
                        if (!Directory.Exists(fullname))
                        {
                            Directory.CreateDirectory(fullname);
                            Directory.SetCreationTime(fullname, item.CreationTime);
                            Directory.SetLastAccessTime(fullname, item.LastWriteTime);
                        }
                        FileInfoPageParamDto param = new FileInfoPageParamDto()
                        {
                            Index = 1,
                            Size = 50,
                            ParentId = item.Id
                        };
                        var page = client.GetPageList(param);
                        int pos = 0;
                        while(page != null && pos < page.TotalCount && !this.isStop)
                        {
                            if (page.List != null && page.List.Count > 0) page.List.ForEach(q => { queue.Enqueue(q); pos++; });
                            param.Index = param.Index + 1;
                            page = client.GetPageList(param);
                        }
                    }
                    else if (item.Type == FileInfoType.File)
                    {
                        string status = "";
                        FilePositionCallback call = (len, pos) =>
                        {
                            string s = string.Format("下载文件： {0} ({1}/{2})", remofull, Utils.ToAutoUnit(pos), Utils.ToAutoUnit(len));
                            if (status != s)
                            {
                                status = s;
                                MainForm.Current.SetStatusText(status);
                            }

                            return !this.isStop;
                        };
                        client.GetFile(item, dir, item.Name, call);
                    }
                }
            }

            MainForm.Current.SetStatusText("");
        }

        private void contextMenuStrip_Local_Opening(object sender, CancelEventArgs e)
        {
            this.toolStripMenuItem_LocalPrve.Enabled = this.localList.Count> 0 && this.localList[0].Type == (int)FileInfoType.None;
            this.toolStripMenuItem_LocalOpen.Enabled = false;
            this.toolStripMenuItem_LocalUpload.Enabled = false;
            this.toolStripMenuItem_LocalDelete.Enabled = false;
            if (this.listView_Local.SelectedIndices.Count > 0)
            {
                var m = this.localList[this.listView_Local.SelectedIndices[0]];
                this.toolStripMenuItem_LocalOpen.Enabled = m.Type == FileInfoType.Directory;
                this.toolStripMenuItem_LocalUpload.Enabled = m.Type != FileInfoType.None;
                this.toolStripMenuItem_LocalDelete.Enabled = m.Type != FileInfoType.None;
            }
        }

        private void contextMenuStrip_Server_Opening(object sender, CancelEventArgs e)
        {
            this.toolStripMenuItem_ServerPrve.Enabled = this.serverList.Count > 0 && this.serverList[0].Type == FileInfoType.None;
            this.toolStripMenuItem_ServerOpen.Enabled = false;
            this.toolStripMenuItem_ServerDownload.Enabled = false;
            this.toolStripMenuItem_ServerDelete.Enabled = false;
            if (this.listView_Server.SelectedIndices.Count > 0)
            {
                var m = this.serverList[this.listView_Server.SelectedIndices[0]];
                this.toolStripMenuItem_ServerOpen.Enabled = m.Type == FileInfoType.Directory;
                this.toolStripMenuItem_ServerDownload.Enabled = m.Type != FileInfoType.None;
                this.toolStripMenuItem_ServerDelete.Enabled = m.Type != FileInfoType.None;
            }
        }

        private void toolStripMenuItem_LocalPrve_Click(object sender, EventArgs e)
        {
            this.LocalPath = Utils.GetParent(this.LocalPath);
            this.GetLocalFileInfo();
        }

        private void toolStripMenuItem_LocalOpen_Click(object sender, EventArgs e)
        {
            if (this.listView_Local.SelectedIndices.Count > 0)
            {
                var m = this.localList[this.listView_Local.SelectedIndices[0]];
                if (m.Type == FileInfoType.Directory)
                {
                    this.LocalPath = Utils.Combine(this.LocalPath, m.Name);
                    this.GetLocalFileInfo();
                }
            }
        }

        private void toolStripMenuItem_LocalUpload_Click(object sender, EventArgs e)
        {
            this.btn_Upload_Click(sender, e);
        }

        private void toolStripMenuItem_LocalDelete_Click(object sender, EventArgs e)
        {
            if (this.listView_Local.SelectedIndices.Count > 0)
            {
                var m = this.localList[this.listView_Local.SelectedIndices[0]];
                if (m.Type == (int)FileInfoType.None) return;
                try
                {
                    string name = Utils.Combine(m.Directory, m.Name);
                    if (m.Type == FileInfoType.File) if (File.Exists(name)) File.Delete(name);
                    if (m.Type == FileInfoType.Directory) if (Directory.Exists(name)) Directory.Delete(name, true);
                    this.GetLocalFileInfo();
                }
                catch (Exception ex)
                {
                    MainForm.Current.ShowMessage(ex.Message);
                }
            }
        }

        private void toolStripMenuItem_ServerPrve_Click(object sender, EventArgs e)
        {
            if (this.ServerPath != null)
            {
                using (var fileclient = IocUtils.Get<IFileInfoService>(new object[] { MainForm.Current.FileClient }))
                {
                    this.ServerPath = fileclient.Get(this.ServerPath.ParentId);
                }
                this.GetServerList();
            }
        }

        private void toolStripMenuItem_ServerOpen_Click(object sender, EventArgs e)
        {
            if (this.listView_Server.SelectedIndices.Count > 0)
            {
                var m = this.serverList[this.listView_Server.SelectedIndices[0]];
                if (m.Type == FileInfoType.Directory)
                {
                    this.ServerPath = m;
                    this.GetServerList();
                }
            }
        }

        private void toolStripMenuItem_ServerDownload_Click(object sender, EventArgs e)
        {
            this.btn_Download_Click(sender, e);
        }

        private void toolStripMenuItem_ServerDelete_Click(object sender, EventArgs e)
        {
            if (this.listView_Server.SelectedIndices.Count > 0)
            {
                var m = this.serverList[this.listView_Server.SelectedIndices[0]];
                using (var client = IocUtils.Get<IFileInfoService>(new object[] { MainForm.Current.FileClient }))
                {
                    if(client.Delete(m.Id))
                    {
                        if(this.ucPageFooter_Server.PageIndex > 1)
                        {
                            int count = (this.ucPageFooter_Server.PageIndex - 1) * this.ucPageFooter_Server.PageSize;
                            if (count >= this.ucPageFooter_Server.TotalCount - 1) 
                                this.ucPageFooter_Server.SetPageIndex(this.ucPageFooter_Server.PageIndex - 1, this.ucPageFooter_Server.TotalCount - 1);
                        }
                        this.GetServerList();
                    }
                }
            }
        }
        
    }
}
