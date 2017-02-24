namespace UpdateModul.config.gui
{
    partial class frmPassw
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPassw));
            this.txPassword = new System.Windows.Forms.TextBox();
            this.bOK = new System.Windows.Forms.Button();
            this.lPasswDescription = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txPassword
            // 
            resources.ApplyResources(this.txPassword, "txPassword");
            this.txPassword.Name = "txPassword";
            // 
            // bOK
            // 
            resources.ApplyResources(this.bOK, "bOK");
            this.bOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOK.Name = "bOK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // lPasswDescription
            // 
            resources.ApplyResources(this.lPasswDescription, "lPasswDescription");
            this.lPasswDescription.Name = "lPasswDescription";
            // 
            // bCancel
            // 
            resources.ApplyResources(this.bCancel, "bCancel");
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Name = "bCancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // frmPassw
            // 
            this.AcceptButton = this.bOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.lPasswDescription);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.txPassword);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPassw";
            this.Load += new System.EventHandler(this.frmPassw_Load);
            this.Shown += new System.EventHandler(this.frmPassw_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txPassword;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Label lPasswDescription;
        private System.Windows.Forms.Button bCancel;
    }
}