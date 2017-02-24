namespace UpdateModul
{
  partial class frmError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmError));
            this.lbCaption = new System.Windows.Forms.Label();
            this.lbNote = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lbShowDetails = new System.Windows.Forms.Label();
            this.lbHideDetails = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
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
            // lbDescription
            // 
            resources.ApplyResources(this.lbDescription, "lbDescription");
            this.lbDescription.Name = "lbDescription";
            // 
            // tbDescription
            // 
            resources.ApplyResources(this.tbDescription, "tbDescription");
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // pbIcon
            // 
            this.pbIcon.Image = global::UpdateModul.Properties.Resources.Security_Shields_Critical_32xLG_color;
            resources.ApplyResources(this.pbIcon, "pbIcon");
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.TabStop = false;
            // 
            // lbShowDetails
            // 
            resources.ApplyResources(this.lbShowDetails, "lbShowDetails");
            this.lbShowDetails.Name = "lbShowDetails";
            this.lbShowDetails.Click += new System.EventHandler(this.lbDetails_Click);
            // 
            // lbHideDetails
            // 
            resources.ApplyResources(this.lbHideDetails, "lbHideDetails");
            this.lbHideDetails.Name = "lbHideDetails";
            this.lbHideDetails.Click += new System.EventHandler(this.lbHideDetails_Click);
            // 
            // frmError
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.Controls.Add(this.lbHideDetails);
            this.Controls.Add(this.lbShowDetails);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.lbNote);
            this.Controls.Add(this.lbCaption);
            this.Controls.Add(this.pbIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmError";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.frmError_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pbIcon;
    private System.Windows.Forms.Label lbCaption;
    private System.Windows.Forms.Label lbNote;
    private System.Windows.Forms.Label lbDescription;
    private System.Windows.Forms.TextBox tbDescription;
    private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbShowDetails;
        private System.Windows.Forms.Label lbHideDetails;
    }
}