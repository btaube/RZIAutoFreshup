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
            this.TVmain = new System.Windows.Forms.TreeView();
            this.imgListTV = new System.Windows.Forms.ImageList(this.components);
            this.grpboxDetailSecurity = new System.Windows.Forms.GroupBox();
            this.BdetailCancelSecurity = new System.Windows.Forms.Button();
            this.BdetailSaveSecurity = new System.Windows.Forms.Button();
            this.txDetailsPasswordConfirm = new System.Windows.Forms.TextBox();
            this.txDetailsPassword = new System.Windows.Forms.TextBox();
            this.lDetailsPasswordConfirm = new System.Windows.Forms.Label();
            this.lDetailsPassword = new System.Windows.Forms.Label();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.programmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.grpboxDetailCommon = new System.Windows.Forms.GroupBox();
            this.lDetailsCommonCheckDate = new System.Windows.Forms.Label();
            this.cbCommonLogLevel = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lDetailsLastChecked = new System.Windows.Forms.Label();
            this.numDetailsCheckDays = new System.Windows.Forms.NumericUpDown();
            this.bDetailCancelCommon = new System.Windows.Forms.Button();
            this.bDetailSaveCommon = new System.Windows.Forms.Button();
            this.lDetailCheckDays = new System.Windows.Forms.Label();
            this.grpboxDetailRepository = new System.Windows.Forms.GroupBox();
            this.bDetailsCancelRepository = new System.Windows.Forms.Button();
            this.bDetailsSaveRepository = new System.Windows.Forms.Button();
            this.txDetailsRepositoryRepo = new System.Windows.Forms.TextBox();
            this.lDetailsRepositoryRepo = new System.Windows.Forms.Label();
            this.grpboxDetailProxy = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.bProxyTest = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.chkbxProxyBypassServer = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txProxyPW = new System.Windows.Forms.TextBox();
            this.txProxyUser = new System.Windows.Forms.TextBox();
            this.chkbxProxyUseDefCred = new System.Windows.Forms.CheckBox();
            this.chkbxProxyUseProxy = new System.Windows.Forms.CheckBox();
            this.txProxyPort = new System.Windows.Forms.TextBox();
            this.txProxyServer = new System.Windows.Forms.TextBox();
            this.bProxyCancel = new System.Windows.Forms.Button();
            this.bProxySave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.GRPBXmain.SuspendLayout();
            this.grpboxDetailSecurity.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.grpboxDetailCommon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDetailsCheckDays)).BeginInit();
            this.grpboxDetailRepository.SuspendLayout();
            this.grpboxDetailProxy.SuspendLayout();
            this.SuspendLayout();
            // 
            // GRPBXmain
            // 
            resources.ApplyResources(this.GRPBXmain, "GRPBXmain");
            this.GRPBXmain.Controls.Add(this.TVmain);
            this.GRPBXmain.Name = "GRPBXmain";
            this.GRPBXmain.TabStop = false;
            // 
            // TVmain
            // 
            resources.ApplyResources(this.TVmain, "TVmain");
            this.TVmain.HideSelection = false;
            this.TVmain.ImageList = this.imgListTV;
            this.TVmain.Name = "TVmain";
            this.TVmain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TVmain_AfterSelect);
            this.TVmain.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TVmain_NodeMouseClick);
            // 
            // imgListTV
            // 
            this.imgListTV.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListTV.ImageStream")));
            this.imgListTV.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListTV.Images.SetKeyName(0, "");
            this.imgListTV.Images.SetKeyName(1, "cog_16x16.png");
            // 
            // grpboxDetailSecurity
            // 
            resources.ApplyResources(this.grpboxDetailSecurity, "grpboxDetailSecurity");
            this.grpboxDetailSecurity.Controls.Add(this.BdetailCancelSecurity);
            this.grpboxDetailSecurity.Controls.Add(this.BdetailSaveSecurity);
            this.grpboxDetailSecurity.Controls.Add(this.txDetailsPasswordConfirm);
            this.grpboxDetailSecurity.Controls.Add(this.txDetailsPassword);
            this.grpboxDetailSecurity.Controls.Add(this.lDetailsPasswordConfirm);
            this.grpboxDetailSecurity.Controls.Add(this.lDetailsPassword);
            this.grpboxDetailSecurity.Name = "grpboxDetailSecurity";
            this.grpboxDetailSecurity.TabStop = false;
            // 
            // BdetailCancelSecurity
            // 
            resources.ApplyResources(this.BdetailCancelSecurity, "BdetailCancelSecurity");
            this.BdetailCancelSecurity.Name = "BdetailCancelSecurity";
            this.BdetailCancelSecurity.UseVisualStyleBackColor = true;
            this.BdetailCancelSecurity.Click += new System.EventHandler(this.BdetailCancelSecurity_Click);
            // 
            // BdetailSaveSecurity
            // 
            resources.ApplyResources(this.BdetailSaveSecurity, "BdetailSaveSecurity");
            this.BdetailSaveSecurity.Name = "BdetailSaveSecurity";
            this.BdetailSaveSecurity.UseVisualStyleBackColor = true;
            this.BdetailSaveSecurity.Click += new System.EventHandler(this.BdetailSaveSecurity_Click);
            // 
            // txDetailsPasswordConfirm
            // 
            resources.ApplyResources(this.txDetailsPasswordConfirm, "txDetailsPasswordConfirm");
            this.txDetailsPasswordConfirm.Name = "txDetailsPasswordConfirm";
            this.txDetailsPasswordConfirm.TextChanged += new System.EventHandler(this.txDetailsPasswordConfirm_TextChanged);
            // 
            // txDetailsPassword
            // 
            resources.ApplyResources(this.txDetailsPassword, "txDetailsPassword");
            this.txDetailsPassword.Name = "txDetailsPassword";
            this.txDetailsPassword.TextChanged += new System.EventHandler(this.txDetailsPassword_TextChanged);
            // 
            // lDetailsPasswordConfirm
            // 
            resources.ApplyResources(this.lDetailsPasswordConfirm, "lDetailsPasswordConfirm");
            this.lDetailsPasswordConfirm.Name = "lDetailsPasswordConfirm";
            // 
            // lDetailsPassword
            // 
            resources.ApplyResources(this.lDetailsPassword, "lDetailsPassword");
            this.lDetailsPassword.Name = "lDetailsPassword";
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programmToolStripMenuItem,
            this.toolStripMenuItem1});
            resources.ApplyResources(this.menuMain, "menuMain");
            this.menuMain.Name = "menuMain";
            // 
            // programmToolStripMenuItem
            // 
            this.programmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beendenToolStripMenuItem});
            this.programmToolStripMenuItem.Name = "programmToolStripMenuItem";
            resources.ApplyResources(this.programmToolStripMenuItem, "programmToolStripMenuItem");
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            resources.ApplyResources(this.beendenToolStripMenuItem, "beendenToolStripMenuItem");
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // grpboxDetailCommon
            // 
            resources.ApplyResources(this.grpboxDetailCommon, "grpboxDetailCommon");
            this.grpboxDetailCommon.Controls.Add(this.lDetailsCommonCheckDate);
            this.grpboxDetailCommon.Controls.Add(this.cbCommonLogLevel);
            this.grpboxDetailCommon.Controls.Add(this.label8);
            this.grpboxDetailCommon.Controls.Add(this.lDetailsLastChecked);
            this.grpboxDetailCommon.Controls.Add(this.numDetailsCheckDays);
            this.grpboxDetailCommon.Controls.Add(this.bDetailCancelCommon);
            this.grpboxDetailCommon.Controls.Add(this.bDetailSaveCommon);
            this.grpboxDetailCommon.Controls.Add(this.lDetailCheckDays);
            this.grpboxDetailCommon.Name = "grpboxDetailCommon";
            this.grpboxDetailCommon.TabStop = false;
            // 
            // lDetailsCommonCheckDate
            // 
            resources.ApplyResources(this.lDetailsCommonCheckDate, "lDetailsCommonCheckDate");
            this.lDetailsCommonCheckDate.Name = "lDetailsCommonCheckDate";
            // 
            // cbCommonLogLevel
            // 
            this.cbCommonLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCommonLogLevel.FormattingEnabled = true;
            this.cbCommonLogLevel.Items.AddRange(new object[] {
            resources.GetString("cbCommonLogLevel.Items"),
            resources.GetString("cbCommonLogLevel.Items1")});
            resources.ApplyResources(this.cbCommonLogLevel, "cbCommonLogLevel");
            this.cbCommonLogLevel.Name = "cbCommonLogLevel";
            this.cbCommonLogLevel.SelectedIndexChanged += new System.EventHandler(this.cbCommonLogLevel_SelectedIndexChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // lDetailsLastChecked
            // 
            resources.ApplyResources(this.lDetailsLastChecked, "lDetailsLastChecked");
            this.lDetailsLastChecked.Name = "lDetailsLastChecked";
            // 
            // numDetailsCheckDays
            // 
            resources.ApplyResources(this.numDetailsCheckDays, "numDetailsCheckDays");
            this.numDetailsCheckDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numDetailsCheckDays.Name = "numDetailsCheckDays";
            this.numDetailsCheckDays.ValueChanged += new System.EventHandler(this.numDetailsCheckDays_ValueChanged);
            this.numDetailsCheckDays.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numDetailsCheckDays_KeyUp);
            // 
            // bDetailCancelCommon
            // 
            resources.ApplyResources(this.bDetailCancelCommon, "bDetailCancelCommon");
            this.bDetailCancelCommon.Name = "bDetailCancelCommon";
            this.bDetailCancelCommon.UseVisualStyleBackColor = true;
            this.bDetailCancelCommon.Click += new System.EventHandler(this.bDetailCancelCommon_Click);
            // 
            // bDetailSaveCommon
            // 
            resources.ApplyResources(this.bDetailSaveCommon, "bDetailSaveCommon");
            this.bDetailSaveCommon.Name = "bDetailSaveCommon";
            this.bDetailSaveCommon.UseVisualStyleBackColor = true;
            this.bDetailSaveCommon.Click += new System.EventHandler(this.bDetailSaveCommon_Click);
            // 
            // lDetailCheckDays
            // 
            resources.ApplyResources(this.lDetailCheckDays, "lDetailCheckDays");
            this.lDetailCheckDays.Name = "lDetailCheckDays";
            // 
            // grpboxDetailRepository
            // 
            resources.ApplyResources(this.grpboxDetailRepository, "grpboxDetailRepository");
            this.grpboxDetailRepository.Controls.Add(this.bDetailsCancelRepository);
            this.grpboxDetailRepository.Controls.Add(this.bDetailsSaveRepository);
            this.grpboxDetailRepository.Controls.Add(this.txDetailsRepositoryRepo);
            this.grpboxDetailRepository.Controls.Add(this.lDetailsRepositoryRepo);
            this.grpboxDetailRepository.Name = "grpboxDetailRepository";
            this.grpboxDetailRepository.TabStop = false;
            // 
            // bDetailsCancelRepository
            // 
            resources.ApplyResources(this.bDetailsCancelRepository, "bDetailsCancelRepository");
            this.bDetailsCancelRepository.Name = "bDetailsCancelRepository";
            this.bDetailsCancelRepository.UseVisualStyleBackColor = true;
            this.bDetailsCancelRepository.Click += new System.EventHandler(this.bDetailsCancelRepository_Click);
            // 
            // bDetailsSaveRepository
            // 
            resources.ApplyResources(this.bDetailsSaveRepository, "bDetailsSaveRepository");
            this.bDetailsSaveRepository.Name = "bDetailsSaveRepository";
            this.bDetailsSaveRepository.UseVisualStyleBackColor = true;
            this.bDetailsSaveRepository.Click += new System.EventHandler(this.bDetailsSaveRepository_Click);
            // 
            // txDetailsRepositoryRepo
            // 
            resources.ApplyResources(this.txDetailsRepositoryRepo, "txDetailsRepositoryRepo");
            this.txDetailsRepositoryRepo.Name = "txDetailsRepositoryRepo";
            this.txDetailsRepositoryRepo.TextChanged += new System.EventHandler(this.txDetailsRepositoryRepo_TextChanged);
            // 
            // lDetailsRepositoryRepo
            // 
            resources.ApplyResources(this.lDetailsRepositoryRepo, "lDetailsRepositoryRepo");
            this.lDetailsRepositoryRepo.Name = "lDetailsRepositoryRepo";
            // 
            // grpboxDetailProxy
            // 
            resources.ApplyResources(this.grpboxDetailProxy, "grpboxDetailProxy");
            this.grpboxDetailProxy.Controls.Add(this.richTextBox1);
            this.grpboxDetailProxy.Controls.Add(this.bProxyTest);
            this.grpboxDetailProxy.Controls.Add(this.label7);
            this.grpboxDetailProxy.Controls.Add(this.chkbxProxyBypassServer);
            this.grpboxDetailProxy.Controls.Add(this.label6);
            this.grpboxDetailProxy.Controls.Add(this.label5);
            this.grpboxDetailProxy.Controls.Add(this.label4);
            this.grpboxDetailProxy.Controls.Add(this.label3);
            this.grpboxDetailProxy.Controls.Add(this.label1);
            this.grpboxDetailProxy.Controls.Add(this.txProxyPW);
            this.grpboxDetailProxy.Controls.Add(this.txProxyUser);
            this.grpboxDetailProxy.Controls.Add(this.chkbxProxyUseDefCred);
            this.grpboxDetailProxy.Controls.Add(this.chkbxProxyUseProxy);
            this.grpboxDetailProxy.Controls.Add(this.txProxyPort);
            this.grpboxDetailProxy.Controls.Add(this.txProxyServer);
            this.grpboxDetailProxy.Controls.Add(this.bProxyCancel);
            this.grpboxDetailProxy.Controls.Add(this.bProxySave);
            this.grpboxDetailProxy.Controls.Add(this.label2);
            this.grpboxDetailProxy.Name = "grpboxDetailProxy";
            this.grpboxDetailProxy.TabStop = false;
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            // 
            // bProxyTest
            // 
            resources.ApplyResources(this.bProxyTest, "bProxyTest");
            this.bProxyTest.Name = "bProxyTest";
            this.bProxyTest.UseVisualStyleBackColor = true;
            this.bProxyTest.Click += new System.EventHandler(this.bProxyTest_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // chkbxProxyBypassServer
            // 
            resources.ApplyResources(this.chkbxProxyBypassServer, "chkbxProxyBypassServer");
            this.chkbxProxyBypassServer.Name = "chkbxProxyBypassServer";
            this.chkbxProxyBypassServer.UseVisualStyleBackColor = true;
            this.chkbxProxyBypassServer.CheckedChanged += new System.EventHandler(this.chkbxProxyBypassServer_CheckedChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txProxyPW
            // 
            resources.ApplyResources(this.txProxyPW, "txProxyPW");
            this.txProxyPW.Name = "txProxyPW";
            this.txProxyPW.TextChanged += new System.EventHandler(this.txProxyPW_TextChanged);
            // 
            // txProxyUser
            // 
            resources.ApplyResources(this.txProxyUser, "txProxyUser");
            this.txProxyUser.Name = "txProxyUser";
            this.txProxyUser.TextChanged += new System.EventHandler(this.txProxyUser_TextChanged);
            // 
            // chkbxProxyUseDefCred
            // 
            resources.ApplyResources(this.chkbxProxyUseDefCred, "chkbxProxyUseDefCred");
            this.chkbxProxyUseDefCred.Name = "chkbxProxyUseDefCred";
            this.chkbxProxyUseDefCred.UseVisualStyleBackColor = true;
            this.chkbxProxyUseDefCred.CheckedChanged += new System.EventHandler(this.chkbxProxyUseDefCred_CheckedChanged);
            // 
            // chkbxProxyUseProxy
            // 
            resources.ApplyResources(this.chkbxProxyUseProxy, "chkbxProxyUseProxy");
            this.chkbxProxyUseProxy.Name = "chkbxProxyUseProxy";
            this.chkbxProxyUseProxy.UseVisualStyleBackColor = true;
            this.chkbxProxyUseProxy.CheckedChanged += new System.EventHandler(this.chkbxProxyUseProxy_CheckedChanged);
            // 
            // txProxyPort
            // 
            resources.ApplyResources(this.txProxyPort, "txProxyPort");
            this.txProxyPort.Name = "txProxyPort";
            this.txProxyPort.TextChanged += new System.EventHandler(this.txProxyPort_TextChanged);
            // 
            // txProxyServer
            // 
            resources.ApplyResources(this.txProxyServer, "txProxyServer");
            this.txProxyServer.Name = "txProxyServer";
            this.txProxyServer.TextChanged += new System.EventHandler(this.txProxyServer_TextChanged);
            // 
            // bProxyCancel
            // 
            resources.ApplyResources(this.bProxyCancel, "bProxyCancel");
            this.bProxyCancel.Name = "bProxyCancel";
            this.bProxyCancel.UseVisualStyleBackColor = true;
            this.bProxyCancel.Click += new System.EventHandler(this.bProxyCancel_Click);
            // 
            // bProxySave
            // 
            resources.ApplyResources(this.bProxySave, "bProxySave");
            this.bProxySave.Name = "bProxySave";
            this.bProxySave.UseVisualStyleBackColor = true;
            this.bProxySave.Click += new System.EventHandler(this.bProxySave_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpboxDetailCommon);
            this.Controls.Add(this.grpboxDetailProxy);
            this.Controls.Add(this.grpboxDetailRepository);
            this.Controls.Add(this.grpboxDetailSecurity);
            this.Controls.Add(this.GRPBXmain);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.GRPBXmain.ResumeLayout(false);
            this.grpboxDetailSecurity.ResumeLayout(false);
            this.grpboxDetailSecurity.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.grpboxDetailCommon.ResumeLayout(false);
            this.grpboxDetailCommon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDetailsCheckDays)).EndInit();
            this.grpboxDetailRepository.ResumeLayout(false);
            this.grpboxDetailRepository.PerformLayout();
            this.grpboxDetailProxy.ResumeLayout(false);
            this.grpboxDetailProxy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox GRPBXmain;
        private System.Windows.Forms.TreeView TVmain;
        private System.Windows.Forms.GroupBox grpboxDetailSecurity;
        private System.Windows.Forms.Button BdetailSaveSecurity;
        private System.Windows.Forms.TextBox txDetailsPassword;
        private System.Windows.Forms.Label lDetailsPassword;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label lDetailsPasswordConfirm;
        private System.Windows.Forms.TextBox txDetailsPasswordConfirm;
        private System.Windows.Forms.GroupBox grpboxDetailCommon;
        private System.Windows.Forms.NumericUpDown numDetailsCheckDays;
        private System.Windows.Forms.Button bDetailCancelCommon;
        private System.Windows.Forms.Button bDetailSaveCommon;
        private System.Windows.Forms.Label lDetailCheckDays;
        private System.Windows.Forms.Label lDetailsLastChecked;
        private System.Windows.Forms.GroupBox grpboxDetailRepository;
        private System.Windows.Forms.Button bDetailsCancelRepository;
        private System.Windows.Forms.Button bDetailsSaveRepository;
        private System.Windows.Forms.TextBox txDetailsRepositoryRepo;
        private System.Windows.Forms.Label lDetailsRepositoryRepo;
        private System.Windows.Forms.Button BdetailCancelSecurity;
        private System.Windows.Forms.ToolStripMenuItem programmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.ImageList imgListTV;
        private System.Windows.Forms.ComboBox cbCommonLogLevel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lDetailsCommonCheckDate;
        private System.Windows.Forms.GroupBox grpboxDetailProxy;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button bProxyTest;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkbxProxyBypassServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txProxyPW;
        private System.Windows.Forms.TextBox txProxyUser;
        private System.Windows.Forms.CheckBox chkbxProxyUseDefCred;
        private System.Windows.Forms.CheckBox chkbxProxyUseProxy;
        private System.Windows.Forms.TextBox txProxyPort;
        private System.Windows.Forms.TextBox txProxyServer;
        private System.Windows.Forms.Button bProxyCancel;
        private System.Windows.Forms.Button bProxySave;
        private System.Windows.Forms.Label label2;
    }
}

