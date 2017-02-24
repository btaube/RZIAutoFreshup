namespace UpdateModul
{
  partial class frmReadme
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReadme));
      this.butOK = new System.Windows.Forms.Button();
      this.rbReadme = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // butOK
      // 
      resources.ApplyResources(this.butOK, "butOK");
      this.butOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.butOK.Name = "butOK";
      this.butOK.UseVisualStyleBackColor = true;
      this.butOK.Click += new System.EventHandler(this.butOK_Click);
      // 
      // rbReadme
      // 
      resources.ApplyResources(this.rbReadme, "rbReadme");
      this.rbReadme.Name = "rbReadme";
      this.rbReadme.ReadOnly = true;
      // 
      // frmReadme
      // 
      this.AcceptButton = this.butOK;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.butOK;
      this.Controls.Add(this.rbReadme);
      this.Controls.Add(this.butOK);
      this.MinimizeBox = false;
      this.Name = "frmReadme";
      this.ShowIcon = false;
      this.TopMost = true;
      this.Load += new System.EventHandler(this.frmReadme_Load);
      this.Shown += new System.EventHandler(this.frmReadme_Shown);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button butOK;
    private System.Windows.Forms.RichTextBox rbReadme;
  }
}