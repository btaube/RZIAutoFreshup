namespace UpdateModul
{
    partial class frmMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.GRPBXmain = new System.Windows.Forms.GroupBox();
            this.lSearchCount = new System.Windows.Forms.Label();
            this.imgSearchCancel = new System.Windows.Forms.PictureBox();
            this.cbSearch = new System.Windows.Forms.ComboBox();
            this.TVmain = new System.Windows.Forms.TreeView();
            this.contextTV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsTVcreateClient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTVcreateProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTVcreateLogical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTVcreatePhysical = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsTVeditNode = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTVdeleteNode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.imgListTV = new System.Windows.Forms.ImageList(this.components);
            this.GRPBXdetail = new System.Windows.Forms.GroupBox();
            this.lbDetailParents = new System.Windows.Forms.Label();
            this.bProductsDelIcon = new System.Windows.Forms.Button();
            this.bProductsChooseItem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txSupportEmail = new System.Windows.Forms.TextBox();
            this.imgProductIcon = new System.Windows.Forms.PictureBox();
            this.txDisplayGuid = new System.Windows.Forms.TextBox();
            this.BdetailCancel = new System.Windows.Forms.Button();
            this.LVdetailParents = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BdetailSave = new System.Windows.Forms.Button();
            this.Lvguid = new System.Windows.Forms.Label();
            this.LurlInstFile = new System.Windows.Forms.Label();
            this.LurlRN = new System.Windows.Forms.Label();
            this.Lvprerequisite = new System.Windows.Forms.Label();
            this.Lvparents = new System.Windows.Forms.Label();
            this.TXurlInstFile = new System.Windows.Forms.TextBox();
            this.TXurlRN = new System.Windows.Forms.TextBox();
            this.CBprerequisites = new System.Windows.Forms.ComboBox();
            this.TXvnameExt = new System.Windows.Forms.TextBox();
            this.TXvnameInt = new System.Windows.Forms.TextBox();
            this.LvnameExt = new System.Windows.Forms.Label();
            this.LvnameInt = new System.Windows.Forms.Label();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.programmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadVersionLookupxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.provideVersionLookupxmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.datenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grpbxLegend = new System.Windows.Forms.GroupBox();
            this.lLegendPhysical = new System.Windows.Forms.Label();
            this.lLegendLogical = new System.Windows.Forms.Label();
            this.lLegendProduct = new System.Windows.Forms.Label();
            this.lLegendClient = new System.Windows.Forms.Label();
            this.picLegendProduct = new System.Windows.Forms.PictureBox();
            this.picLegendLogical = new System.Windows.Forms.PictureBox();
            this.picLegendPhysical = new System.Windows.Forms.PictureBox();
            this.picLegendClient = new System.Windows.Forms.PictureBox();
            this.timerSearch = new System.Windows.Forms.Timer(this.components);
            this.loadEncryptedVersionLookupxmlcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GRPBXmain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSearchCancel)).BeginInit();
            this.contextTV.SuspendLayout();
            this.GRPBXdetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProductIcon)).BeginInit();
            this.menuMain.SuspendLayout();
            this.grpbxLegend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLegendProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLegendLogical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLegendPhysical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLegendClient)).BeginInit();
            this.SuspendLayout();
            // 
            // GRPBXmain
            // 
            resources.ApplyResources(this.GRPBXmain, "GRPBXmain");
            this.GRPBXmain.Controls.Add(this.lSearchCount);
            this.GRPBXmain.Controls.Add(this.imgSearchCancel);
            this.GRPBXmain.Controls.Add(this.cbSearch);
            this.GRPBXmain.Controls.Add(this.TVmain);
            this.GRPBXmain.Name = "GRPBXmain";
            this.GRPBXmain.TabStop = false;
            // 
            // lSearchCount
            // 
            resources.ApplyResources(this.lSearchCount, "lSearchCount");
            this.lSearchCount.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lSearchCount.Name = "lSearchCount";
            // 
            // imgSearchCancel
            // 
            resources.ApplyResources(this.imgSearchCancel, "imgSearchCancel");
            this.imgSearchCancel.Name = "imgSearchCancel";
            this.imgSearchCancel.TabStop = false;
            this.imgSearchCancel.Click += new System.EventHandler(this.imgSearchCancel_Click);
            // 
            // cbSearch
            // 
            resources.ApplyResources(this.cbSearch, "cbSearch");
            this.cbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cbSearch.ForeColor = System.Drawing.Color.DarkGray;
            this.cbSearch.FormattingEnabled = true;
            this.cbSearch.Items.AddRange(new object[] {
            resources.GetString("cbSearch.Items"),
            resources.GetString("cbSearch.Items1"),
            resources.GetString("cbSearch.Items2")});
            this.cbSearch.Name = "cbSearch";
            this.cbSearch.Sorted = true;
            this.cbSearch.Enter += new System.EventHandler(this.cbSearch_Enter);
            this.cbSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbSearch_KeyUp);
            // 
            // TVmain
            // 
            resources.ApplyResources(this.TVmain, "TVmain");
            this.TVmain.ContextMenuStrip = this.contextTV;
            this.TVmain.HideSelection = false;
            this.TVmain.ImageList = this.imgListTV;
            this.TVmain.Name = "TVmain";
            this.TVmain.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TVmain_AfterLabelEdit);
            this.TVmain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TVmain_AfterSelect);
            this.TVmain.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TVmain_NodeMouseClick);
            // 
            // contextTV
            // 
            this.contextTV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsTVcreateClient,
            this.tsTVcreateProduct,
            this.tsTVcreateLogical,
            this.tsTVcreatePhysical,
            this.toolStripMenuItem3,
            this.tsTVeditNode,
            this.tsTVdeleteNode,
            this.toolStripMenuItem4});
            this.contextTV.Name = "contextTV";
            resources.ApplyResources(this.contextTV, "contextTV");
            this.contextTV.Opening += new System.ComponentModel.CancelEventHandler(this.contextTV_Opening);
            // 
            // tsTVcreateClient
            // 
            resources.ApplyResources(this.tsTVcreateClient, "tsTVcreateClient");
            this.tsTVcreateClient.Name = "tsTVcreateClient";
            this.tsTVcreateClient.Click += new System.EventHandler(this.tsTVcreateClient_Click);
            // 
            // tsTVcreateProduct
            // 
            resources.ApplyResources(this.tsTVcreateProduct, "tsTVcreateProduct");
            this.tsTVcreateProduct.Name = "tsTVcreateProduct";
            this.tsTVcreateProduct.Click += new System.EventHandler(this.tsTVcreateProduct_Click);
            // 
            // tsTVcreateLogical
            // 
            resources.ApplyResources(this.tsTVcreateLogical, "tsTVcreateLogical");
            this.tsTVcreateLogical.Name = "tsTVcreateLogical";
            this.tsTVcreateLogical.Click += new System.EventHandler(this.tsTVcreateLogical_Click);
            // 
            // tsTVcreatePhysical
            // 
            resources.ApplyResources(this.tsTVcreatePhysical, "tsTVcreatePhysical");
            this.tsTVcreatePhysical.Name = "tsTVcreatePhysical";
            this.tsTVcreatePhysical.Click += new System.EventHandler(this.tsTVcreatePhysical_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // tsTVeditNode
            // 
            resources.ApplyResources(this.tsTVeditNode, "tsTVeditNode");
            this.tsTVeditNode.Name = "tsTVeditNode";
            this.tsTVeditNode.Click += new System.EventHandler(this.tsTVeditNode_Click);
            // 
            // tsTVdeleteNode
            // 
            resources.ApplyResources(this.tsTVdeleteNode, "tsTVdeleteNode");
            this.tsTVdeleteNode.Name = "tsTVdeleteNode";
            this.tsTVdeleteNode.Click += new System.EventHandler(this.tsTVdeleteNode_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // imgListTV
            // 
            this.imgListTV.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListTV.ImageStream")));
            this.imgListTV.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListTV.Images.SetKeyName(0, "industry_16x16.png");
            this.imgListTV.Images.SetKeyName(1, "form_16x16.png");
            this.imgListTV.Images.SetKeyName(2, "folder_16x16.png");
            this.imgListTV.Images.SetKeyName(3, "disc_16x16.png");
            this.imgListTV.Images.SetKeyName(4, "world_16x16.png");
            this.imgListTV.Images.SetKeyName(5, "edit_16x16.png");
            this.imgListTV.Images.SetKeyName(6, "delete_16x16.png");
            this.imgListTV.Images.SetKeyName(7, "search_16x16.png");
            this.imgListTV.Images.SetKeyName(8, "search_industry_16x16.png");
            this.imgListTV.Images.SetKeyName(9, "search_form_16x16.png");
            this.imgListTV.Images.SetKeyName(10, "search_folder_16x16.png");
            this.imgListTV.Images.SetKeyName(11, "search_disc_16x16.png");
            // 
            // GRPBXdetail
            // 
            resources.ApplyResources(this.GRPBXdetail, "GRPBXdetail");
            this.GRPBXdetail.Controls.Add(this.lbDetailParents);
            this.GRPBXdetail.Controls.Add(this.bProductsDelIcon);
            this.GRPBXdetail.Controls.Add(this.bProductsChooseItem);
            this.GRPBXdetail.Controls.Add(this.label2);
            this.GRPBXdetail.Controls.Add(this.label1);
            this.GRPBXdetail.Controls.Add(this.txSupportEmail);
            this.GRPBXdetail.Controls.Add(this.imgProductIcon);
            this.GRPBXdetail.Controls.Add(this.txDisplayGuid);
            this.GRPBXdetail.Controls.Add(this.BdetailCancel);
            this.GRPBXdetail.Controls.Add(this.LVdetailParents);
            this.GRPBXdetail.Controls.Add(this.BdetailSave);
            this.GRPBXdetail.Controls.Add(this.Lvguid);
            this.GRPBXdetail.Controls.Add(this.LurlInstFile);
            this.GRPBXdetail.Controls.Add(this.LurlRN);
            this.GRPBXdetail.Controls.Add(this.Lvprerequisite);
            this.GRPBXdetail.Controls.Add(this.Lvparents);
            this.GRPBXdetail.Controls.Add(this.TXurlInstFile);
            this.GRPBXdetail.Controls.Add(this.TXurlRN);
            this.GRPBXdetail.Controls.Add(this.CBprerequisites);
            this.GRPBXdetail.Controls.Add(this.TXvnameExt);
            this.GRPBXdetail.Controls.Add(this.TXvnameInt);
            this.GRPBXdetail.Controls.Add(this.LvnameExt);
            this.GRPBXdetail.Controls.Add(this.LvnameInt);
            this.GRPBXdetail.Name = "GRPBXdetail";
            this.GRPBXdetail.TabStop = false;
            // 
            // lbDetailParents
            // 
            resources.ApplyResources(this.lbDetailParents, "lbDetailParents");
            this.lbDetailParents.Name = "lbDetailParents";
            // 
            // bProductsDelIcon
            // 
            resources.ApplyResources(this.bProductsDelIcon, "bProductsDelIcon");
            this.bProductsDelIcon.ImageList = this.imgListTV;
            this.bProductsDelIcon.Name = "bProductsDelIcon";
            this.bProductsDelIcon.UseVisualStyleBackColor = true;
            this.bProductsDelIcon.Click += new System.EventHandler(this.bProductsDelIcon_Click);
            // 
            // bProductsChooseItem
            // 
            resources.ApplyResources(this.bProductsChooseItem, "bProductsChooseItem");
            this.bProductsChooseItem.ImageList = this.imgListTV;
            this.bProductsChooseItem.Name = "bProductsChooseItem";
            this.bProductsChooseItem.UseVisualStyleBackColor = true;
            this.bProductsChooseItem.Click += new System.EventHandler(this.bProductsChooseItem_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txSupportEmail
            // 
            resources.ApplyResources(this.txSupportEmail, "txSupportEmail");
            this.txSupportEmail.Name = "txSupportEmail";
            this.txSupportEmail.TextChanged += new System.EventHandler(this.txSupportEmail_TextChanged);
            this.txSupportEmail.Leave += new System.EventHandler(this.txProductTitle_Leave);
            // 
            // imgProductIcon
            // 
            resources.ApplyResources(this.imgProductIcon, "imgProductIcon");
            this.imgProductIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imgProductIcon.Name = "imgProductIcon";
            this.imgProductIcon.TabStop = false;
            // 
            // txDisplayGuid
            // 
            resources.ApplyResources(this.txDisplayGuid, "txDisplayGuid");
            this.txDisplayGuid.Name = "txDisplayGuid";
            this.txDisplayGuid.ReadOnly = true;
            // 
            // BdetailCancel
            // 
            resources.ApplyResources(this.BdetailCancel, "BdetailCancel");
            this.BdetailCancel.Name = "BdetailCancel";
            this.BdetailCancel.UseVisualStyleBackColor = true;
            this.BdetailCancel.Click += new System.EventHandler(this.BdetailCancel_Click);
            // 
            // LVdetailParents
            // 
            resources.ApplyResources(this.LVdetailParents, "LVdetailParents");
            this.LVdetailParents.CheckBoxes = true;
            this.LVdetailParents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.LVdetailParents.FullRowSelect = true;
            this.LVdetailParents.MultiSelect = false;
            this.LVdetailParents.Name = "LVdetailParents";
            this.LVdetailParents.SmallImageList = this.imgListTV;
            this.LVdetailParents.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.LVdetailParents.UseCompatibleStateImageBehavior = false;
            this.LVdetailParents.View = System.Windows.Forms.View.Details;
            this.LVdetailParents.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LVdetailParents_ColumnClick);
            this.LVdetailParents.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LVdetailParents_ItemChecked);
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // BdetailSave
            // 
            resources.ApplyResources(this.BdetailSave, "BdetailSave");
            this.BdetailSave.Name = "BdetailSave";
            this.BdetailSave.UseVisualStyleBackColor = true;
            this.BdetailSave.Click += new System.EventHandler(this.BdetailSave_Click);
            // 
            // Lvguid
            // 
            resources.ApplyResources(this.Lvguid, "Lvguid");
            this.Lvguid.Name = "Lvguid";
            // 
            // LurlInstFile
            // 
            resources.ApplyResources(this.LurlInstFile, "LurlInstFile");
            this.LurlInstFile.Name = "LurlInstFile";
            // 
            // LurlRN
            // 
            resources.ApplyResources(this.LurlRN, "LurlRN");
            this.LurlRN.Name = "LurlRN";
            // 
            // Lvprerequisite
            // 
            resources.ApplyResources(this.Lvprerequisite, "Lvprerequisite");
            this.Lvprerequisite.Name = "Lvprerequisite";
            // 
            // Lvparents
            // 
            resources.ApplyResources(this.Lvparents, "Lvparents");
            this.Lvparents.Name = "Lvparents";
            // 
            // TXurlInstFile
            // 
            resources.ApplyResources(this.TXurlInstFile, "TXurlInstFile");
            this.TXurlInstFile.Name = "TXurlInstFile";
            this.TXurlInstFile.TextChanged += new System.EventHandler(this.TXurlInstFile_TextChanged);
            this.TXurlInstFile.Leave += new System.EventHandler(this.TXurlInstFile_Leave);
            // 
            // TXurlRN
            // 
            resources.ApplyResources(this.TXurlRN, "TXurlRN");
            this.TXurlRN.Name = "TXurlRN";
            this.TXurlRN.TextChanged += new System.EventHandler(this.TXurlRN_TextChanged);
            this.TXurlRN.Leave += new System.EventHandler(this.TXurlRN_Leave);
            // 
            // CBprerequisites
            // 
            resources.ApplyResources(this.CBprerequisites, "CBprerequisites");
            this.CBprerequisites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBprerequisites.FormattingEnabled = true;
            this.CBprerequisites.Items.AddRange(new object[] {
            resources.GetString("CBprerequisites.Items"),
            resources.GetString("CBprerequisites.Items1"),
            resources.GetString("CBprerequisites.Items2")});
            this.CBprerequisites.Name = "CBprerequisites";
            this.CBprerequisites.Sorted = true;
            this.CBprerequisites.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.CBprerequisites_DrawItem);
            this.CBprerequisites.TextChanged += new System.EventHandler(this.CBprerequisites_TextChanged);
            // 
            // TXvnameExt
            // 
            resources.ApplyResources(this.TXvnameExt, "TXvnameExt");
            this.TXvnameExt.Name = "TXvnameExt";
            this.TXvnameExt.TextChanged += new System.EventHandler(this.TXvnameExt_TextChanged);
            // 
            // TXvnameInt
            // 
            resources.ApplyResources(this.TXvnameInt, "TXvnameInt");
            this.TXvnameInt.Name = "TXvnameInt";
            this.TXvnameInt.TextChanged += new System.EventHandler(this.TXvnameInt_TextChanged);
            // 
            // LvnameExt
            // 
            resources.ApplyResources(this.LvnameExt, "LvnameExt");
            this.LvnameExt.Name = "LvnameExt";
            // 
            // LvnameInt
            // 
            resources.ApplyResources(this.LvnameInt, "LvnameInt");
            this.LvnameInt.Name = "LvnameInt";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programmToolStripMenuItem,
            this.datenToolStripMenuItem,
            this.toolStripMenuItem1});
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.Name = "menuMain";
            // 
            // programmToolStripMenuItem
            // 
            this.programmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadVersionLookupxmlToolStripMenuItem,
            this.provideVersionLookupxmlToolStripMenuItem,
            this.toolStripMenuItem2,
            this.beendenToolStripMenuItem,
            this.loadEncryptedVersionLookupxmlcToolStripMenuItem});
            this.programmToolStripMenuItem.Name = "programmToolStripMenuItem";
            resources.ApplyResources(this.programmToolStripMenuItem, "programmToolStripMenuItem");
            // 
            // loadVersionLookupxmlToolStripMenuItem
            // 
            this.loadVersionLookupxmlToolStripMenuItem.Name = "loadVersionLookupxmlToolStripMenuItem";
            resources.ApplyResources(this.loadVersionLookupxmlToolStripMenuItem, "loadVersionLookupxmlToolStripMenuItem");
            this.loadVersionLookupxmlToolStripMenuItem.Click += new System.EventHandler(this.loadVersionLookupxmlToolStripMenuItem_Click);
            // 
            // provideVersionLookupxmlToolStripMenuItem
            // 
            this.provideVersionLookupxmlToolStripMenuItem.Name = "provideVersionLookupxmlToolStripMenuItem";
            resources.ApplyResources(this.provideVersionLookupxmlToolStripMenuItem, "provideVersionLookupxmlToolStripMenuItem");
            this.provideVersionLookupxmlToolStripMenuItem.Click += new System.EventHandler(this.provideVersionLookupxmlToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            resources.ApplyResources(this.beendenToolStripMenuItem, "beendenToolStripMenuItem");
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // datenToolStripMenuItem
            // 
            this.datenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandTreeToolStripMenuItem,
            this.collapseTreeToolStripMenuItem,
            this.reloadTreeToolStripMenuItem});
            this.datenToolStripMenuItem.Name = "datenToolStripMenuItem";
            resources.ApplyResources(this.datenToolStripMenuItem, "datenToolStripMenuItem");
            // 
            // expandTreeToolStripMenuItem
            // 
            this.expandTreeToolStripMenuItem.Name = "expandTreeToolStripMenuItem";
            resources.ApplyResources(this.expandTreeToolStripMenuItem, "expandTreeToolStripMenuItem");
            this.expandTreeToolStripMenuItem.Click += new System.EventHandler(this.expandTreeToolStripMenuItem_Click);
            // 
            // collapseTreeToolStripMenuItem
            // 
            this.collapseTreeToolStripMenuItem.Name = "collapseTreeToolStripMenuItem";
            resources.ApplyResources(this.collapseTreeToolStripMenuItem, "collapseTreeToolStripMenuItem");
            this.collapseTreeToolStripMenuItem.Click += new System.EventHandler(this.collapseTreeToolStripMenuItem_Click);
            // 
            // reloadTreeToolStripMenuItem
            // 
            this.reloadTreeToolStripMenuItem.Name = "reloadTreeToolStripMenuItem";
            resources.ApplyResources(this.reloadTreeToolStripMenuItem, "reloadTreeToolStripMenuItem");
            this.reloadTreeToolStripMenuItem.Click += new System.EventHandler(this.reloadTreeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // grpbxLegend
            // 
            resources.ApplyResources(this.grpbxLegend, "grpbxLegend");
            this.grpbxLegend.Controls.Add(this.lLegendPhysical);
            this.grpbxLegend.Controls.Add(this.lLegendLogical);
            this.grpbxLegend.Controls.Add(this.lLegendProduct);
            this.grpbxLegend.Controls.Add(this.lLegendClient);
            this.grpbxLegend.Controls.Add(this.picLegendProduct);
            this.grpbxLegend.Controls.Add(this.picLegendLogical);
            this.grpbxLegend.Controls.Add(this.picLegendPhysical);
            this.grpbxLegend.Controls.Add(this.picLegendClient);
            this.grpbxLegend.Name = "grpbxLegend";
            this.grpbxLegend.TabStop = false;
            // 
            // lLegendPhysical
            // 
            resources.ApplyResources(this.lLegendPhysical, "lLegendPhysical");
            this.lLegendPhysical.Name = "lLegendPhysical";
            // 
            // lLegendLogical
            // 
            resources.ApplyResources(this.lLegendLogical, "lLegendLogical");
            this.lLegendLogical.Name = "lLegendLogical";
            // 
            // lLegendProduct
            // 
            resources.ApplyResources(this.lLegendProduct, "lLegendProduct");
            this.lLegendProduct.Name = "lLegendProduct";
            // 
            // lLegendClient
            // 
            resources.ApplyResources(this.lLegendClient, "lLegendClient");
            this.lLegendClient.Name = "lLegendClient";
            // 
            // picLegendProduct
            // 
            resources.ApplyResources(this.picLegendProduct, "picLegendProduct");
            this.picLegendProduct.Name = "picLegendProduct";
            this.picLegendProduct.TabStop = false;
            // 
            // picLegendLogical
            // 
            resources.ApplyResources(this.picLegendLogical, "picLegendLogical");
            this.picLegendLogical.Name = "picLegendLogical";
            this.picLegendLogical.TabStop = false;
            // 
            // picLegendPhysical
            // 
            resources.ApplyResources(this.picLegendPhysical, "picLegendPhysical");
            this.picLegendPhysical.Name = "picLegendPhysical";
            this.picLegendPhysical.TabStop = false;
            // 
            // picLegendClient
            // 
            resources.ApplyResources(this.picLegendClient, "picLegendClient");
            this.picLegendClient.Name = "picLegendClient";
            this.picLegendClient.TabStop = false;
            // 
            // timerSearch
            // 
            this.timerSearch.Interval = 1500;
            this.timerSearch.Tick += new System.EventHandler(this.timerSearch_Tick);
            // 
            // loadEncryptedVersionLookupxmlcToolStripMenuItem
            // 
            this.loadEncryptedVersionLookupxmlcToolStripMenuItem.Name = "loadEncryptedVersionLookupxmlcToolStripMenuItem";
            resources.ApplyResources(this.loadEncryptedVersionLookupxmlcToolStripMenuItem, "loadEncryptedVersionLookupxmlcToolStripMenuItem");
            this.loadEncryptedVersionLookupxmlcToolStripMenuItem.Click += new System.EventHandler(this.loadEncryptedVersionLookupxmlcToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpbxLegend);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.GRPBXdetail);
            this.Controls.Add(this.GRPBXmain);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.GRPBXmain.ResumeLayout(false);
            this.GRPBXmain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgSearchCancel)).EndInit();
            this.contextTV.ResumeLayout(false);
            this.GRPBXdetail.ResumeLayout(false);
            this.GRPBXdetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgProductIcon)).EndInit();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.grpbxLegend.ResumeLayout(false);
            this.grpbxLegend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLegendProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLegendLogical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLegendPhysical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLegendClient)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox GRPBXmain;
        private System.Windows.Forms.TreeView TVmain;
        private System.Windows.Forms.GroupBox GRPBXdetail;
        private System.Windows.Forms.Button BdetailSave;
        private System.Windows.Forms.Label Lvguid;
        private System.Windows.Forms.Label LurlInstFile;
        private System.Windows.Forms.Label LurlRN;
        private System.Windows.Forms.Label Lvprerequisite;
        private System.Windows.Forms.Label Lvparents;
        private System.Windows.Forms.TextBox TXurlInstFile;
        private System.Windows.Forms.TextBox TXurlRN;
        private System.Windows.Forms.ComboBox CBprerequisites;
        private System.Windows.Forms.TextBox TXvnameExt;
        private System.Windows.Forms.TextBox TXvnameInt;
        private System.Windows.Forms.Label LvnameExt;
        private System.Windows.Forms.Label LvnameInt;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem programmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ImageList imgListTV;
        private System.Windows.Forms.ContextMenuStrip contextTV;
        private System.Windows.Forms.ToolStripMenuItem tsTVcreateLogical;
        private System.Windows.Forms.ToolStripMenuItem tsTVcreatePhysical;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem tsTVeditNode;
        private System.Windows.Forms.ToolStripMenuItem tsTVdeleteNode;
        private System.Windows.Forms.ToolStripMenuItem tsTVcreateClient;
        private System.Windows.Forms.ToolStripMenuItem tsTVcreateProduct;
        private System.Windows.Forms.GroupBox grpbxLegend;
        private System.Windows.Forms.PictureBox picLegendClient;
        private System.Windows.Forms.PictureBox picLegendProduct;
        private System.Windows.Forms.PictureBox picLegendLogical;
        private System.Windows.Forms.PictureBox picLegendPhysical;
        private System.Windows.Forms.Label lLegendPhysical;
        private System.Windows.Forms.Label lLegendLogical;
        private System.Windows.Forms.Label lLegendProduct;
        private System.Windows.Forms.Label lLegendClient;
        private System.Windows.Forms.ListView LVdetailParents;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button BdetailCancel;
        private System.Windows.Forms.ToolStripMenuItem datenToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbSearch;
        private System.Windows.Forms.Timer timerSearch;
        private System.Windows.Forms.PictureBox imgSearchCancel;
        private System.Windows.Forms.ToolStripMenuItem expandTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseTreeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem provideVersionLookupxmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem reloadTreeToolStripMenuItem;
        private System.Windows.Forms.Label lSearchCount;
        private System.Windows.Forms.TextBox txDisplayGuid;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem loadVersionLookupxmlToolStripMenuItem;
        private System.Windows.Forms.Button bProductsDelIcon;
        private System.Windows.Forms.Button bProductsChooseItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txSupportEmail;
        private System.Windows.Forms.PictureBox imgProductIcon;
        private System.Windows.Forms.Label lbDetailParents;
        private System.Windows.Forms.ToolStripMenuItem loadEncryptedVersionLookupxmlcToolStripMenuItem;
    }
}

