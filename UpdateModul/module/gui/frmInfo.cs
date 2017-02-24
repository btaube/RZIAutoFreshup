using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using UpdateModul.Properties;
using UpdateModul.shared;

namespace UpdateModul
{
    public partial class frmInfo : Form
    {
        public frmInfo(String CurrentVersion)
        {
            InitializeComponent();
            String ErrorText = "";
            SetCorporateDesign(CGlobVars.CurrentGUID, out ErrorText);
            lbCaption.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmInfo_NoVersion1 : translations.frmInfo_NoVersion2;
            lbVersion.Text = CurrentVersion;
            this.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_Title1 : translations.frmVersion_Title2;
        }

        static Bitmap _icon;


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
                    pbIcon.Image = icon.ToBitmap();
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
                    pbIcon.Image = icon.ToBitmap();

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
        

        public static void ShowInfo(String CurrentVersion)
        {
            
            if (!CGlobVars.silentModeNoNewVersion)
            {
                frmInfo dlg = new frmInfo(CurrentVersion);
                dlg.ShowDialog();
            }
            
        }

        private void frmInfo_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }
    }
}
