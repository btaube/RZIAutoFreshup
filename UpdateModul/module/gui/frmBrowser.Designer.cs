namespace UpdateModul
{
  partial class frmBrowser
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBrowser));
            this.butOK = new System.Windows.Forms.Button();
            this.pnlBrowser = new System.Windows.Forms.Panel();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.forwardButton = new System.Windows.Forms.ToolStripButton();
            this.urlTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.goButton = new System.Windows.Forms.ToolStripButton();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.pnlBrowser.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butOK
            // 
            resources.ApplyResources(this.butOK, "butOK");
            this.butOK.Name = "butOK";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // pnlBrowser
            // 
            resources.ApplyResources(this.pnlBrowser, "pnlBrowser");
            this.pnlBrowser.Controls.Add(this.webBrowser1);
            this.pnlBrowser.Name = "pnlBrowser";
            // 
            // pbStatus
            // 
            resources.ApplyResources(this.pbStatus, "pbStatus");
            this.pbStatus.Name = "pbStatus";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.forwardButton,
            this.urlTextBox,
            this.goButton});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Stretch = true;
            // 
            // backButton
            // 
            resources.ApplyResources(this.backButton, "backButton");
            this.backButton.Image = global::UpdateModul.Properties.Resources.nav_left_green;
            this.backButton.Name = "backButton";
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // forwardButton
            // 
            resources.ApplyResources(this.forwardButton, "forwardButton");
            this.forwardButton.Image = global::UpdateModul.Properties.Resources.nav_right_green;
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // urlTextBox
            // 
            resources.ApplyResources(this.urlTextBox, "urlTextBox");
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.urlTextBox_KeyUp);
            // 
            // goButton
            // 
            this.goButton.Image = global::UpdateModul.Properties.Resources.nav_plain_green;
            resources.ApplyResources(this.goButton, "goButton");
            this.goButton.Name = "goButton";
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // webBrowser1
            // 
            resources.ApplyResources(this.webBrowser1, "webBrowser1");
            this.webBrowser1.Name = "webBrowser1";
            // 
            // frmBrowser
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.pnlBrowser);
            this.Controls.Add(this.butOK);
            this.Name = "frmBrowser";
            this.Load += new System.EventHandler(this.frmBrowser_Load);
            this.Shown += new System.EventHandler(this.frmBrowser_Shown);
            this.pnlBrowser.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Panel pnlBrowser;
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton backButton;
        private System.Windows.Forms.ToolStripButton forwardButton;
        private System.Windows.Forms.ToolStripTextBox urlTextBox;
        private System.Windows.Forms.ToolStripButton goButton;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}