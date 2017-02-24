namespace UpdateModul
{
  partial class frmInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfo));
            this.btnOK = new System.Windows.Forms.Button();
            this.lbCaption = new System.Windows.Forms.Label();
            this.lbNote = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lbCaption
            // 
            resources.ApplyResources(this.lbCaption, "lbCaption");
            this.lbCaption.Name = "lbCaption";
            // 
            // lbNote
            // 
            resources.ApplyResources(this.lbNote, "lbNote");
            this.lbNote.Name = "lbNote";
            // 
            // lbVersion
            // 
            resources.ApplyResources(this.lbVersion, "lbVersion");
            this.lbVersion.Name = "lbVersion";
            // 
            // pbIcon
            // 
            this.pbIcon.Image = global::UpdateModul.Properties.Resources.Security_Shields_Complete_and_ok_32xLG_color;
            resources.ApplyResources(this.pbIcon, "pbIcon");
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.TabStop = false;
            // 
            // frmInfo
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbNote);
            this.Controls.Add(this.lbCaption);
            this.Controls.Add(this.pbIcon);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInfo";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.frmInfo_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.PictureBox pbIcon;
    private System.Windows.Forms.Label lbCaption;
    private System.Windows.Forms.Label lbNote;
    private System.Windows.Forms.Label lbVersion;
  }
}