namespace UpdateModul
{
  partial class frmDownload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownload));
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.labStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labDownloadLocation = new System.Windows.Forms.Label();
            this.labFile = new System.Windows.Forms.Label();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.lLookingForUpdates = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbStatus
            // 
            resources.ApplyResources(this.pbStatus, "pbStatus");
            this.pbStatus.Name = "pbStatus";
            // 
            // labStatus
            // 
            resources.ApplyResources(this.labStatus, "labStatus");
            this.labStatus.Name = "labStatus";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labDownloadLocation
            // 
            resources.ApplyResources(this.labDownloadLocation, "labDownloadLocation");
            this.labDownloadLocation.Name = "labDownloadLocation";
            // 
            // labFile
            // 
            resources.ApplyResources(this.labFile, "labFile");
            this.labFile.Name = "labFile";
            // 
            // pbIcon
            // 
            resources.ApplyResources(this.pbIcon, "pbIcon");
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.TabStop = false;
            // 
            // lLookingForUpdates
            // 
            resources.ApplyResources(this.lLookingForUpdates, "lLookingForUpdates");
            this.lLookingForUpdates.Name = "lLookingForUpdates";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // frmDownload
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lLookingForUpdates);
            this.Controls.Add(this.pbIcon);
            this.Controls.Add(this.labFile);
            this.Controls.Add(this.labDownloadLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labStatus);
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDownload";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmDownload_Load);
            this.Shown += new System.EventHandler(this.frmDownload_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.ProgressBar pbStatus;
    private System.Windows.Forms.Label labStatus;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label labDownloadLocation;
    private System.Windows.Forms.Label labFile;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Label lLookingForUpdates;
        private System.Windows.Forms.Label label3;
    }
}