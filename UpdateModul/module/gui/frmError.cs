using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using UpdateModul.Properties;
using UpdateModul.shared;

namespace UpdateModul
{
    public partial class frmError : Form
    {

        bool _showDetails = false;
        Bitmap _icon;

        public frmError(String errorDescription)
        {
            InitializeComponent();

            this.Height = 200;
            tbDescription.Visible = false;
            lbDescription.Visible = false;
            lbShowDetails.Visible = true;
            lbHideDetails.Visible = false;


            tbDescription.Text = errorDescription;
            this.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_Title1 : translations.frmVersion_Title2;
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
        }

        public static void ShowError(String errorDescription)
        {
            String ErrorText = "";
            if (!CGlobVars.silentModeError)
            {
                frmError dlg = new frmError(errorDescription);
                dlg.SetCorporateDesign(CGlobVars.CurrentGUID, out ErrorText);
                dlg.ShowDialog();
            }
            
        }

        private void frmError_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void lbDetails_Click(object sender, EventArgs e)
        {
            _showDetails = !_showDetails;
            if (_showDetails)
            {
                this.Height = 270;
                tbDescription.Visible = true;
                lbDescription.Visible = true;
                lbShowDetails.Visible = false;
                lbHideDetails.Visible = true;

            }
            else
            {
                this.Height = 200;
                tbDescription.Visible = false;
                lbDescription.Visible = false;
                lbShowDetails.Visible = true;
                lbHideDetails.Visible = false;

            }
        }

        private void lbHideDetails_Click(object sender, EventArgs e)
        {
            _showDetails = !_showDetails;
            if (_showDetails)
            {
                this.Height = 270;
                tbDescription.Visible = true;
                lbDescription.Visible = true;
                lbShowDetails.Visible = false;
                lbHideDetails.Visible = true;

            }
            else
            {
                this.Height = 200;
                tbDescription.Visible = false;
                lbDescription.Visible = false;
                lbShowDetails.Visible = true;
                lbHideDetails.Visible = false;

            }
        }
    }
}
