namespace Client.WinMain
{
    partial class UCFileInfo
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFileInfo));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_LocalPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dropDownList_LcoalFileInfoType = new Afx.Forms.Controls.AfxDropDownList();
            this.dropDownList_LocalDisk = new Afx.Forms.Controls.AfxDropDownList();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_LocalSearch = new Afx.Forms.Controls.AfxButton();
            this.txt_LocalKeyword = new System.Windows.Forms.TextBox();
            this.listView_Local = new System.Windows.Forms.ListView();
            this.col_local_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_local_CreateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_local_LastWriteTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_local_Length = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_Local = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_LocalPrve = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_LocalOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_LocalUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_LocalDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_UploadAll = new System.Windows.Forms.Button();
            this.btn_DownloadAll = new System.Windows.Forms.Button();
            this.btn_Upload = new System.Windows.Forms.Button();
            this.btn_Download = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txt_ServerPath = new System.Windows.Forms.TextBox();
            this.dropDownList_ServerFileInfoType = new Afx.Forms.Controls.AfxDropDownList();
            this.label6 = new System.Windows.Forms.Label();
            this.ucPageFooter_Server = new Afx.Forms.Controls.UCPageFooter();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listView_Server = new System.Windows.Forms.ListView();
            this.columnHeader_serverName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_serverCreateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_serverLastTIme = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_serverLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_Server = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_ServerPrve = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ServerOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ServerDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_ServerDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_ServerSearch = new Afx.Forms.Controls.AfxButton();
            this.txt_ServerKeyword = new System.Windows.Forms.TextBox();
            this.pic_Loading = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip_Local.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuStrip_Server.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Loading)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(980, 428);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_LocalPath);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dropDownList_LcoalFileInfoType);
            this.panel1.Controls.Add(this.dropDownList_LocalDisk);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_LocalSearch);
            this.panel1.Controls.Add(this.txt_LocalKeyword);
            this.panel1.Controls.Add(this.listView_Local);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 428);
            this.panel1.TabIndex = 0;
            // 
            // txt_LocalPath
            // 
            this.txt_LocalPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_LocalPath.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_LocalPath.Location = new System.Drawing.Point(75, 40);
            this.txt_LocalPath.Name = "txt_LocalPath";
            this.txt_LocalPath.ReadOnly = true;
            this.txt_LocalPath.Size = new System.Drawing.Size(376, 22);
            this.txt_LocalPath.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(3, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "当前目录：";
            // 
            // dropDownList_LcoalFileInfoType
            // 
            this.dropDownList_LcoalFileInfoType.DisplayMember = "Text";
            this.dropDownList_LcoalFileInfoType.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.dropDownList_LcoalFileInfoType.FormattingEnabled = true;
            this.dropDownList_LcoalFileInfoType.Location = new System.Drawing.Point(149, 10);
            this.dropDownList_LcoalFileInfoType.Name = "dropDownList_LcoalFileInfoType";
            this.dropDownList_LcoalFileInfoType.Size = new System.Drawing.Size(72, 24);
            this.dropDownList_LcoalFileInfoType.TabIndex = 11;
            this.dropDownList_LcoalFileInfoType.ValueMember = "Value";
            // 
            // dropDownList_LocalDisk
            // 
            this.dropDownList_LocalDisk.DisplayMember = "Text";
            this.dropDownList_LocalDisk.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.dropDownList_LocalDisk.FormattingEnabled = true;
            this.dropDownList_LocalDisk.Location = new System.Drawing.Point(52, 10);
            this.dropDownList_LocalDisk.Name = "dropDownList_LocalDisk";
            this.dropDownList_LocalDisk.Size = new System.Drawing.Size(41, 24);
            this.dropDownList_LocalDisk.TabIndex = 10;
            this.dropDownList_LocalDisk.ValueMember = "Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(3, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "磁盘：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(226, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(101, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "类型：";
            // 
            // btn_LocalSearch
            // 
            this.btn_LocalSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LocalSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_LocalSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LocalSearch.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_LocalSearch.ForeColor = System.Drawing.Color.White;
            this.btn_LocalSearch.Location = new System.Drawing.Point(380, 9);
            this.btn_LocalSearch.Name = "btn_LocalSearch";
            this.btn_LocalSearch.Size = new System.Drawing.Size(75, 25);
            this.btn_LocalSearch.TabIndex = 13;
            this.btn_LocalSearch.Text = "查 询";
            this.btn_LocalSearch.Type = Afx.Forms.Controls.AfxButtonType.OK;
            this.btn_LocalSearch.UseVisualStyleBackColor = true;
            this.btn_LocalSearch.Click += new System.EventHandler(this.btn_LocalSearch_Click);
            // 
            // txt_LocalKeyword
            // 
            this.txt_LocalKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_LocalKeyword.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_LocalKeyword.Location = new System.Drawing.Point(274, 11);
            this.txt_LocalKeyword.Name = "txt_LocalKeyword";
            this.txt_LocalKeyword.Size = new System.Drawing.Size(97, 22);
            this.txt_LocalKeyword.TabIndex = 12;
            // 
            // listView_Local
            // 
            this.listView_Local.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Local.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_Local.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col_local_Name,
            this.col_local_CreateTime,
            this.col_local_LastWriteTime,
            this.col_local_Length});
            this.listView_Local.ContextMenuStrip = this.contextMenuStrip_Local;
            this.listView_Local.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView_Local.FullRowSelect = true;
            this.listView_Local.GridLines = true;
            this.listView_Local.HideSelection = false;
            this.listView_Local.Location = new System.Drawing.Point(-1, 67);
            this.listView_Local.Margin = new System.Windows.Forms.Padding(0);
            this.listView_Local.Name = "listView_Local";
            this.listView_Local.ShowItemToolTips = true;
            this.listView_Local.Size = new System.Drawing.Size(467, 362);
            this.listView_Local.SmallImageList = this.imageList1;
            this.listView_Local.TabIndex = 2;
            this.listView_Local.UseCompatibleStateImageBehavior = false;
            this.listView_Local.View = System.Windows.Forms.View.Details;
            this.listView_Local.VirtualMode = true;
            this.listView_Local.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_Local_ItemSelectionChanged);
            this.listView_Local.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Local_MouseDoubleClick);
            // 
            // col_local_Name
            // 
            this.col_local_Name.Text = "名 称";
            this.col_local_Name.Width = 130;
            // 
            // col_local_CreateTime
            // 
            this.col_local_CreateTime.Text = "创建时间";
            this.col_local_CreateTime.Width = 120;
            // 
            // col_local_LastWriteTime
            // 
            this.col_local_LastWriteTime.Text = "修改时间";
            this.col_local_LastWriteTime.Width = 120;
            // 
            // col_local_Length
            // 
            this.col_local_Length.Text = "大 小";
            this.col_local_Length.Width = 80;
            // 
            // contextMenuStrip_Local
            // 
            this.contextMenuStrip_Local.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_LocalPrve,
            this.toolStripMenuItem_LocalOpen,
            this.toolStripMenuItem_LocalUpload,
            this.toolStripMenuItem_LocalDelete});
            this.contextMenuStrip_Local.Name = "contextMenuStrip_Local";
            this.contextMenuStrip_Local.Size = new System.Drawing.Size(113, 92);
            this.contextMenuStrip_Local.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Local_Opening);
            // 
            // toolStripMenuItem_LocalPrve
            // 
            this.toolStripMenuItem_LocalPrve.Name = "toolStripMenuItem_LocalPrve";
            this.toolStripMenuItem_LocalPrve.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem_LocalPrve.Text = "上一层";
            this.toolStripMenuItem_LocalPrve.Click += new System.EventHandler(this.toolStripMenuItem_LocalPrve_Click);
            // 
            // toolStripMenuItem_LocalOpen
            // 
            this.toolStripMenuItem_LocalOpen.Name = "toolStripMenuItem_LocalOpen";
            this.toolStripMenuItem_LocalOpen.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem_LocalOpen.Text = "打 开";
            this.toolStripMenuItem_LocalOpen.Click += new System.EventHandler(this.toolStripMenuItem_LocalOpen_Click);
            // 
            // toolStripMenuItem_LocalUpload
            // 
            this.toolStripMenuItem_LocalUpload.Name = "toolStripMenuItem_LocalUpload";
            this.toolStripMenuItem_LocalUpload.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem_LocalUpload.Text = "上 传";
            this.toolStripMenuItem_LocalUpload.Click += new System.EventHandler(this.toolStripMenuItem_LocalUpload_Click);
            // 
            // toolStripMenuItem_LocalDelete
            // 
            this.toolStripMenuItem_LocalDelete.Name = "toolStripMenuItem_LocalDelete";
            this.toolStripMenuItem_LocalDelete.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem_LocalDelete.Text = "删 除";
            this.toolStripMenuItem_LocalDelete.Click += new System.EventHandler(this.toolStripMenuItem_LocalDelete_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "dir.png");
            this.imageList1.Images.SetKeyName(1, "file.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Stop);
            this.panel2.Controls.Add(this.btn_UploadAll);
            this.panel2.Controls.Add(this.btn_DownloadAll);
            this.panel2.Controls.Add(this.btn_Upload);
            this.panel2.Controls.Add(this.btn_Download);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(465, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(50, 428);
            this.panel2.TabIndex = 1;
            // 
            // btn_Stop
            // 
            this.btn_Stop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Stop.BackColor = System.Drawing.Color.Red;
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Stop.Font = new System.Drawing.Font("微软雅黑", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Stop.ForeColor = System.Drawing.Color.Black;
            this.btn_Stop.Location = new System.Drawing.Point(6, 114);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(37, 25);
            this.btn_Stop.TabIndex = 20;
            this.btn_Stop.Text = " ▉▉";
            this.btn_Stop.UseVisualStyleBackColor = false;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_UploadAll
            // 
            this.btn_UploadAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_UploadAll.BackColor = System.Drawing.Color.DarkMagenta;
            this.btn_UploadAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_UploadAll.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_UploadAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_UploadAll.Location = new System.Drawing.Point(7, 281);
            this.btn_UploadAll.Name = "btn_UploadAll";
            this.btn_UploadAll.Size = new System.Drawing.Size(37, 25);
            this.btn_UploadAll.TabIndex = 24;
            this.btn_UploadAll.Text = ">>";
            this.btn_UploadAll.UseVisualStyleBackColor = false;
            this.btn_UploadAll.Click += new System.EventHandler(this.btn_UploadAll_Click);
            // 
            // btn_DownloadAll
            // 
            this.btn_DownloadAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_DownloadAll.BackColor = System.Drawing.Color.Fuchsia;
            this.btn_DownloadAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DownloadAll.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DownloadAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_DownloadAll.Location = new System.Drawing.Point(7, 238);
            this.btn_DownloadAll.Name = "btn_DownloadAll";
            this.btn_DownloadAll.Size = new System.Drawing.Size(37, 25);
            this.btn_DownloadAll.TabIndex = 23;
            this.btn_DownloadAll.Text = "<<";
            this.btn_DownloadAll.UseVisualStyleBackColor = false;
            this.btn_DownloadAll.Click += new System.EventHandler(this.btn_DownloadAll_Click);
            // 
            // btn_Upload
            // 
            this.btn_Upload.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Upload.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_Upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Upload.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.btn_Upload.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Upload.Location = new System.Drawing.Point(7, 197);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(37, 25);
            this.btn_Upload.TabIndex = 22;
            this.btn_Upload.Text = ">";
            this.btn_Upload.UseVisualStyleBackColor = false;
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btn_Download.BackColor = System.Drawing.Color.SpringGreen;
            this.btn_Download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Download.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Download.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Download.Location = new System.Drawing.Point(6, 155);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(37, 25);
            this.btn_Download.TabIndex = 21;
            this.btn_Download.Text = "＜";
            this.btn_Download.UseVisualStyleBackColor = false;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txt_ServerPath);
            this.panel3.Controls.Add(this.dropDownList_ServerFileInfoType);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.ucPageFooter_Server);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.listView_Server);
            this.panel3.Controls.Add(this.btn_ServerSearch);
            this.panel3.Controls.Add(this.txt_ServerKeyword);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(515, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(465, 428);
            this.panel3.TabIndex = 2;
            // 
            // txt_ServerPath
            // 
            this.txt_ServerPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_ServerPath.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ServerPath.Location = new System.Drawing.Point(77, 40);
            this.txt_ServerPath.Name = "txt_ServerPath";
            this.txt_ServerPath.ReadOnly = true;
            this.txt_ServerPath.Size = new System.Drawing.Size(374, 22);
            this.txt_ServerPath.TabIndex = 18;
            this.txt_ServerPath.Text = "\\";
            // 
            // dropDownList_ServerFileInfoType
            // 
            this.dropDownList_ServerFileInfoType.DisplayMember = "Text";
            this.dropDownList_ServerFileInfoType.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.dropDownList_ServerFileInfoType.FormattingEnabled = true;
            this.dropDownList_ServerFileInfoType.Location = new System.Drawing.Point(53, 10);
            this.dropDownList_ServerFileInfoType.Name = "dropDownList_ServerFileInfoType";
            this.dropDownList_ServerFileInfoType.Size = new System.Drawing.Size(72, 24);
            this.dropDownList_ServerFileInfoType.TabIndex = 14;
            this.dropDownList_ServerFileInfoType.ValueMember = "Value";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(3, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "当前目录：";
            // 
            // ucPageFooter_Server
            // 
            this.ucPageFooter_Server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucPageFooter_Server.Location = new System.Drawing.Point(0, 397);
            this.ucPageFooter_Server.Margin = new System.Windows.Forms.Padding(0);
            this.ucPageFooter_Server.Name = "ucPageFooter_Server";
            this.ucPageFooter_Server.Size = new System.Drawing.Size(462, 30);
            this.ucPageFooter_Server.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(137, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "类型：";
            // 
            // listView_Server
            // 
            this.listView_Server.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_Server.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_Server.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_serverName,
            this.columnHeader_serverCreateTime,
            this.columnHeader_serverLastTIme,
            this.columnHeader_serverLength});
            this.listView_Server.ContextMenuStrip = this.contextMenuStrip_Server;
            this.listView_Server.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView_Server.FullRowSelect = true;
            this.listView_Server.GridLines = true;
            this.listView_Server.HideSelection = false;
            this.listView_Server.Location = new System.Drawing.Point(-1, 67);
            this.listView_Server.Name = "listView_Server";
            this.listView_Server.ShowItemToolTips = true;
            this.listView_Server.Size = new System.Drawing.Size(467, 329);
            this.listView_Server.SmallImageList = this.imageList1;
            this.listView_Server.TabIndex = 9;
            this.listView_Server.UseCompatibleStateImageBehavior = false;
            this.listView_Server.View = System.Windows.Forms.View.Details;
            this.listView_Server.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView_Server_ItemSelectionChanged);
            this.listView_Server.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Server_MouseDoubleClick);
            // 
            // columnHeader_serverName
            // 
            this.columnHeader_serverName.Text = "名 称";
            this.columnHeader_serverName.Width = 130;
            // 
            // columnHeader_serverCreateTime
            // 
            this.columnHeader_serverCreateTime.Text = "创建时间";
            this.columnHeader_serverCreateTime.Width = 120;
            // 
            // columnHeader_serverLastTIme
            // 
            this.columnHeader_serverLastTIme.Text = "修改时间";
            this.columnHeader_serverLastTIme.Width = 120;
            // 
            // columnHeader_serverLength
            // 
            this.columnHeader_serverLength.Text = "大 小";
            this.columnHeader_serverLength.Width = 80;
            // 
            // contextMenuStrip_Server
            // 
            this.contextMenuStrip_Server.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ServerPrve,
            this.toolStripMenuItem_ServerOpen,
            this.toolStripMenuItem_ServerDownload,
            this.toolStripMenuItem_ServerDelete});
            this.contextMenuStrip_Server.Name = "contextMenuStrip_Local";
            this.contextMenuStrip_Server.Size = new System.Drawing.Size(113, 92);
            this.contextMenuStrip_Server.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Server_Opening);
            // 
            // toolStripMenuItem_ServerPrve
            // 
            this.toolStripMenuItem_ServerPrve.Name = "toolStripMenuItem_ServerPrve";
            this.toolStripMenuItem_ServerPrve.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem_ServerPrve.Text = "上一层";
            this.toolStripMenuItem_ServerPrve.Click += new System.EventHandler(this.toolStripMenuItem_ServerPrve_Click);
            // 
            // toolStripMenuItem_ServerOpen
            // 
            this.toolStripMenuItem_ServerOpen.Name = "toolStripMenuItem_ServerOpen";
            this.toolStripMenuItem_ServerOpen.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem_ServerOpen.Text = "打 开";
            this.toolStripMenuItem_ServerOpen.Click += new System.EventHandler(this.toolStripMenuItem_ServerOpen_Click);
            // 
            // toolStripMenuItem_ServerDownload
            // 
            this.toolStripMenuItem_ServerDownload.Name = "toolStripMenuItem_ServerDownload";
            this.toolStripMenuItem_ServerDownload.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem_ServerDownload.Text = "下 载";
            this.toolStripMenuItem_ServerDownload.Click += new System.EventHandler(this.toolStripMenuItem_ServerDownload_Click);
            // 
            // toolStripMenuItem_ServerDelete
            // 
            this.toolStripMenuItem_ServerDelete.Name = "toolStripMenuItem_ServerDelete";
            this.toolStripMenuItem_ServerDelete.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem_ServerDelete.Text = "删 除";
            this.toolStripMenuItem_ServerDelete.Click += new System.EventHandler(this.toolStripMenuItem_ServerDelete_Click);
            // 
            // btn_ServerSearch
            // 
            this.btn_ServerSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ServerSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_ServerSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ServerSearch.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ServerSearch.ForeColor = System.Drawing.Color.White;
            this.btn_ServerSearch.Location = new System.Drawing.Point(353, 7);
            this.btn_ServerSearch.Name = "btn_ServerSearch";
            this.btn_ServerSearch.Size = new System.Drawing.Size(75, 25);
            this.btn_ServerSearch.TabIndex = 16;
            this.btn_ServerSearch.Text = "查 询";
            this.btn_ServerSearch.Type = Afx.Forms.Controls.AfxButtonType.OK;
            this.btn_ServerSearch.UseVisualStyleBackColor = true;
            this.btn_ServerSearch.Click += new System.EventHandler(this.btn_ServerSearch_Click);
            // 
            // txt_ServerKeyword
            // 
            this.txt_ServerKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_ServerKeyword.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ServerKeyword.Location = new System.Drawing.Point(187, 10);
            this.txt_ServerKeyword.Name = "txt_ServerKeyword";
            this.txt_ServerKeyword.Size = new System.Drawing.Size(160, 22);
            this.txt_ServerKeyword.TabIndex = 15;
            // 
            // pic_Loading
            // 
            this.pic_Loading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pic_Loading.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pic_Loading.Image = global::Client.WinMain.Properties.Resources.loadImg;
            this.pic_Loading.Location = new System.Drawing.Point(440, 166);
            this.pic_Loading.Name = "pic_Loading";
            this.pic_Loading.Size = new System.Drawing.Size(100, 93);
            this.pic_Loading.TabIndex = 19;
            this.pic_Loading.TabStop = false;
            this.pic_Loading.Visible = false;
            // 
            // UCFileInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pic_Loading);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UCFileInfo";
            this.Size = new System.Drawing.Size(980, 428);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip_Local.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.contextMenuStrip_Server.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Loading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView listView_Local;
        private System.Windows.Forms.ColumnHeader col_local_Name;
        private System.Windows.Forms.ColumnHeader col_local_Length;
        private System.Windows.Forms.ColumnHeader col_local_CreateTime;
        private System.Windows.Forms.ColumnHeader col_local_LastWriteTime;
        private Afx.Forms.Controls.AfxButton btn_LocalSearch;
        private System.Windows.Forms.TextBox txt_LocalKeyword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listView_Server;
        private System.Windows.Forms.ColumnHeader columnHeader_serverName;
        private System.Windows.Forms.ColumnHeader columnHeader_serverLength;
        private System.Windows.Forms.ColumnHeader columnHeader_serverCreateTime;
        private System.Windows.Forms.ColumnHeader columnHeader_serverLastTIme;
        private Afx.Forms.Controls.AfxButton btn_ServerSearch;
        private System.Windows.Forms.TextBox txt_ServerKeyword;
        private System.Windows.Forms.Button btn_UploadAll;
        private System.Windows.Forms.Button btn_DownloadAll;
        private System.Windows.Forms.Button btn_Upload;
        private System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Label label5;
        private Afx.Forms.Controls.UCPageFooter ucPageFooter_Server;
        private Afx.Forms.Controls.AfxDropDownList dropDownList_LcoalFileInfoType;
        private Afx.Forms.Controls.AfxDropDownList dropDownList_LocalDisk;
        private Afx.Forms.Controls.AfxDropDownList dropDownList_ServerFileInfoType;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pic_Loading;
        private System.Windows.Forms.TextBox txt_LocalPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_ServerPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Local;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_LocalPrve;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_LocalOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_LocalUpload;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_LocalDelete;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Server;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ServerPrve;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ServerOpen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ServerDownload;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ServerDelete;




    }
}
