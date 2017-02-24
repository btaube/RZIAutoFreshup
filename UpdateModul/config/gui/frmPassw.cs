using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using UpdateModul;
using UpdateModul.Properties;
using UpdateModul.shared;

namespace UpdateModul.config.gui
{
    public partial class frmPassw : Form
    {

        Bitmap _icon;

        public frmPassw()
        {
            InitializeComponent();
        }
        
        public void SetCorporateDesign(string GUID, out String ErrorText)
        {
            ErrorText = null;
            XmlNode GuidNode = CVersionLookupHelper.GetNodeByTagName(GUID);
            if (GuidNode != null)
            {
                XmlNode aNode = GuidNode.SelectSingleNode("IconBase64");
                if (aNode != null)
                {
                    Bitmap bitmap = RZITools.GetBitmapFromBase64String(aNode.InnerText, out ErrorText);
                    if (ErrorText != null)
                    {
                        return;
                    }
                    IntPtr icon_handle = bitmap.GetHicon();
                    Icon icon = Icon.FromHandle(icon_handle);
                    this.Icon = icon;
                    _icon = new Icon(icon, 48, 48).ToBitmap();
                }
                else
                {
                    if (CGlobVars.VersionDescriptionID == 1)
                    {
                        try
                        {
                            _icon = new Icon(Resources.RZI, 48, 48).ToBitmap();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            _icon = Bitmap.FromHicon(new Icon(Resources.RZI, new Size(24, 24)).Handle);
                        }
                    }
                    else
                    {
                        try
                        {
                            _icon = new Icon(Resources.card1, 48, 48).ToBitmap();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            _icon = Bitmap.FromHicon(new Icon(Resources.card1, new Size(24, 24)).Handle);
                        }
                    }
                    IntPtr icon_handle = _icon.GetHicon();
                    Icon icon = Icon.FromHandle(icon_handle);
                    this.Icon = icon;

                }
            } else
            {
                if (CGlobVars.VersionDescriptionID == 1)
                {
                    try
                    {
                        _icon = new Icon(Resources.RZI, 48, 48).ToBitmap();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        _icon = Bitmap.FromHicon(new Icon(Resources.RZI, new Size(24, 24)).Handle);
                    }
                }
                else
                {
                    try
                    {
                        _icon = new Icon(Resources.card1, 48, 48).ToBitmap();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        _icon = Bitmap.FromHicon(new Icon(Resources.card1, new Size(24, 24)).Handle);
                    }
                }
                IntPtr icon_handle = _icon.GetHicon();
                Icon icon = Icon.FromHandle(icon_handle);
                this.Icon = icon;
            }
        }
        

        private Boolean CheckPasswords(out String ErrorText)
        {
            int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
            if (retVal != CReturnCodes.OK)
            {
                frmError.ShowError(ErrorText);
                return false;
            }
            XmlNode aPWNode = CVersionConfigHelper.GetNodeByTagName("Password");
            string sPW = "";
            if (aPWNode != null)
            {
                sPW = RZITools.Decrypt(aPWNode.InnerText, out ErrorText);
                if (ErrorText != null)
                {
                    return false;
                }

            }

            //edit: if custom password has been set, the default password may not be valid anymore
            if (sPW == "")
            {
                if ((txPassword.Text.Trim().Length > 0) && ((txPassword.Text.Equals(CGlobVars.DEFAULT_PASSWORD)) || (txPassword.Text.Equals(CGlobVars.SUPER_ADMIN_PASSWORD))))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } else if ((txPassword.Text.Trim().Length > 0) && ((txPassword.Text.Equals(CGlobVars.SUPER_ADMIN_PASSWORD)) || (txPassword.Text.Equals(sPW))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            String ErrorText;
            if (CheckPasswords(out ErrorText))
            {
                this.DialogResult = DialogResult.OK;
            } else
            {
                MessageBox.Show(translations.frmMain_Dlg_Password_Retry, translations.frmMain_Dlg_Title_Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txPassword.Focus();
                txPassword.SelectAll();
                this.DialogResult = DialogResult.None;
            }
        }

        private void frmPassw_Shown(object sender, EventArgs e)
        {
            BringToFront();
            Focus();
            Activate();
        }

        private void frmPassw_Load(object sender, EventArgs e)
        {

        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
