using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using UpdateModul;
using UpdateModul.Properties;
using UpdateModul.shared;

namespace UpdateModul
{
    public partial class frmVersion : Form
    {
        public enum EVersionType { MAIN, SUB, MULTI }

        private String m_Guid;
        private String m_NewVersion;
        private String m_DownloadLink;
        private String m_ReleaseNotesLink;
        private String m_SupportEmail;
        private String m_NewVersion2;
        private String m_NewVersion3;
        private String m_NewVersion4;
        private String m_NewVersion5;
        private String m_ReleaseNotesLink2;
        private String m_ReleaseNotesLink3;
        private String m_ReleaseNotesLink4;
        private String m_ReleaseNotesLink5;
        private String m_DownloadLink2;
        private String m_DownloadLink3;
        private String m_DownloadLink4;
        private String m_DownloadLink5;
        private String m_CurrentVersion;

        Bitmap _icon;


        //, String NewVersion2, String NewVersion3, String NewVersion4, String NewVersion5, String DownloadLink, String ReleaseNotesLink, String ReleaseNotesLink2, String ReleaseNotesLink3, String ReleaseNotesLink4, String ReleaseNotesLink5, String SupportEmail, String CurrentVersion
        public frmVersion(EVersionType versionType, String Guid, String CurrentVersion, XmlNode NewVersion, XmlNode NewVersion2, XmlNode NewVersion3, XmlNode NewVersion4, XmlNode NewVersion5)
        {
            InitializeComponent();
            String ErrorText = "";
            SetIcon(NewVersion, out ErrorText);

            this.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_Title1 : translations.frmVersion_Title2;

            m_NewVersion = CVersionLookupHelper.GetNodeText(NewVersion, "DisplayName");
            m_DownloadLink = CVersionLookupHelper.GetNodeText(NewVersion, "InstFilesURL");
            m_ReleaseNotesLink = CVersionLookupHelper.GetNodeText(NewVersion, "ReleaseNotesURL");
            m_SupportEmail = CVersionLookupHelper.GetNodeText(NewVersion, "SupportEmail");

            m_Guid = Guid;
            m_CurrentVersion = CurrentVersion;

            m_NewVersion2 = CVersionLookupHelper.GetNodeText(NewVersion2, "DisplayName");
            m_NewVersion3 = CVersionLookupHelper.GetNodeText(NewVersion3, "DisplayName");
            m_NewVersion4 = CVersionLookupHelper.GetNodeText(NewVersion4, "DisplayName");
            m_NewVersion5 = CVersionLookupHelper.GetNodeText(NewVersion5, "DisplayName");
            if ((m_NewVersion4 == "") && (m_NewVersion5 != ""))
            {
                m_NewVersion4 = "...";
            }

            m_ReleaseNotesLink2 = CVersionLookupHelper.GetNodeText(NewVersion2, "ReleaseNotesURL"); 
            m_ReleaseNotesLink3 = CVersionLookupHelper.GetNodeText(NewVersion3, "ReleaseNotesURL");
            m_ReleaseNotesLink4 = CVersionLookupHelper.GetNodeText(NewVersion4, "ReleaseNotesURL");
            m_ReleaseNotesLink5 = CVersionLookupHelper.GetNodeText(NewVersion5, "ReleaseNotesURL");

            m_DownloadLink2 = CVersionLookupHelper.GetNodeText(NewVersion2, "InstFilesURL");
            m_DownloadLink3 = CVersionLookupHelper.GetNodeText(NewVersion3, "InstFilesURL");
            m_DownloadLink4 = CVersionLookupHelper.GetNodeText(NewVersion4, "InstFilesURL");
            m_DownloadLink5 = CVersionLookupHelper.GetNodeText(NewVersion5, "InstFilesURL");

            ConfigureLabels(m_NewVersion, m_NewVersion2, m_NewVersion3, m_NewVersion4, m_NewVersion5);
            ConfigureReleaseNotesLinks(m_ReleaseNotesLink, m_ReleaseNotesLink2, m_ReleaseNotesLink3, m_ReleaseNotesLink4, m_ReleaseNotesLink5);
            ConfigureDownloadLinks(m_DownloadLink, m_DownloadLink2, m_DownloadLink3, m_DownloadLink4, m_DownloadLink5);


            lbVersion.Text = m_NewVersion;

            if (versionType == EVersionType.MAIN) {
                lbCaption.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_BaseVersion1 : translations.frmVersion_BaseVersion2;
                // 08.02.2017: Download links for base versions now should not be hidden anymore...
                //llDownload.Visible = false;
                llNote.Links.Clear();
                if (llNote.Text.IndexOf("Vertrieb") > -1)
                {
                    llNote.Links.Add(llNote.Text.IndexOf("Vertrieb"), 8, "mailto:" + m_SupportEmail + "?subject=Installation%20Version%28" + m_CurrentVersion + "%20%2D%2D%3E%20" + m_NewVersion + "%29");
                } else if (llNote.Text.IndexOf("marketing") > -1)
                {
                    llNote.Links.Add(llNote.Text.IndexOf("marketing"), 9, "mailto:" + m_SupportEmail + "?subject=Installation%20Version%28" + m_CurrentVersion + "%20%2D%2D%3E%20" + m_NewVersion + "%29");
                }
                llNote.Visible = true;
                this.Height = 240;

                SizeF sizeMain = lbVersion.CreateGraphics().MeasureString(lbVersion.Text, lbVersion.Font);
                int MaxTextWidthMain = Convert.ToInt32(sizeMain.Width);
                llReleaseNotes.Location = new Point(lbVersion.Left + 32 + MaxTextWidthMain, 95);
                llDownload.Location = new Point(llReleaseNotes.Left + 64, 95);

                this.Width = llDownload.Left + 192;
                this.CenterToScreen();
                
            } else if (versionType == EVersionType.MULTI)
            {
                lbCaption.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_MultipleBaseVersions1 : translations.frmVersion_MultipleBaseVersions2;
                lbFurtherVersions.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_FurtherMultipleBase1 : translations.frmVersion_FurtherMultipleBase2;
                // 08.02.2017: Download links for base versions now should not be hidden anymore...
                //llDownload.Visible = false;
                llNote.Links.Clear();
                if (llNote.Text.IndexOf("Vertrieb") > -1)
                {
                    llNote.Links.Add(llNote.Text.IndexOf("Vertrieb"), 8, "mailto:" + m_SupportEmail + "?subject=Installation%20Version%28" + m_CurrentVersion + "%20%2D%2D%3E%20" + m_NewVersion + "%29");
                }
                else if (llNote.Text.IndexOf("marketing") > -1)
                {
                    llNote.Links.Add(llNote.Text.IndexOf("marketing"), 9, "mailto:" + m_SupportEmail + "?subject=Installation%20Version%28" + m_CurrentVersion + "%20%2D%2D%3E%20" + m_NewVersion + "%29");
                }
                llNote.Visible = true;
                lbVersion2.Text = m_NewVersion2;
                lbVersion3.Text = m_NewVersion3;
                lbVersion4.Text = m_NewVersion4;
                lbVersion5.Text = m_NewVersion5;

                if ((m_NewVersion2 == "") && (m_NewVersion3 == "") && (m_NewVersion4 == "") && (m_NewVersion5 == ""))
                {
                    lbCaption.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_SingleVersion1 : translations.frmVersion_SingleVersion2;
                    this.Height = 240;

                    SizeF sizeMain = lbVersion.CreateGraphics().MeasureString(lbVersion.Text, lbVersion.Font);
                    int MaxTextWidthMain = Convert.ToInt32(sizeMain.Width);
                    llReleaseNotes.Location = new Point(lbVersion.Left + 32 + MaxTextWidthMain, 95);
                    llDownload.Location = new Point(llReleaseNotes.Left + 64, 95);

                    this.Width = llDownload.Left + 192;
                    this.CenterToScreen();

                }
                else
                {
                    lbCaption.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_MultipleBaseVersions1 : translations.frmVersion_MultipleBaseVersions2;
                    lbFurtherVersions.Visible = true;
                    this.Height = 342;

                    SizeF sizeMulti = lbVersion.CreateGraphics().MeasureString(lbVersion.Text, lbVersion.Font);
                    SizeF size2Multi = lbVersion2.CreateGraphics().MeasureString(lbVersion2.Text, lbVersion2.Font);
                    SizeF size3Multi = lbVersion3.CreateGraphics().MeasureString(lbVersion3.Text, lbVersion3.Font);
                    SizeF size4Multi = lbVersion4.CreateGraphics().MeasureString(lbVersion4.Text, lbVersion4.Font);
                    SizeF size5Multi = lbVersion5.CreateGraphics().MeasureString(lbVersion5.Text, lbVersion5.Font);

                    int MaxTextWidthMulti = Convert.ToInt32(sizeMulti.Width);
                    if (size2Multi.Width > sizeMulti.Width)
                    {
                        MaxTextWidthMulti = Convert.ToInt32(size2Multi.Width);
                    }
                    if (size3Multi.Width > size2Multi.Width)
                    {
                        MaxTextWidthMulti = Convert.ToInt32(size3Multi.Width);
                    }
                    if (size4Multi.Width > size3Multi.Width)
                    {
                        MaxTextWidthMulti = Convert.ToInt32(size4Multi.Width);
                    }
                    if (size5Multi.Width > size4Multi.Width)
                    {
                        MaxTextWidthMulti = Convert.ToInt32(size5Multi.Width);
                    }

                    llReleaseNotes.Location = new Point(lbVersion.Left + 32 + MaxTextWidthMulti, 95);
                    llReleaseNotes2.Location = new Point(lbVersion2.Left + 32 + MaxTextWidthMulti, 146);
                    llReleaseNotes3.Location = new Point(lbVersion3.Left + 32 + MaxTextWidthMulti, 164);
                    llReleaseNotes4.Location = new Point(lbVersion4.Left + 32 + MaxTextWidthMulti, 182);
                    llReleaseNotes5.Location = new Point(lbVersion5.Left + 32 + MaxTextWidthMulti, 200);

                    llDownload.Location = new Point(llReleaseNotes.Left + 64, 95);
                    llDownload2.Location = new Point(llReleaseNotes2.Left + 64, 146);
                    llDownload3.Location = new Point(llReleaseNotes3.Left + 64, 164);
                    llDownload4.Location = new Point(llReleaseNotes4.Left + 64, 182);
                    llDownload5.Location = new Point(llReleaseNotes5.Left + 64, 200);

                    this.Width = llDownload.Left + 192;
                    this.CenterToScreen();
                }
            } else
            {
                llNote.Visible = false;
                lbVersion2.Text = m_NewVersion2;
                lbVersion3.Text = m_NewVersion3;
                lbVersion4.Text = m_NewVersion4;
                lbVersion5.Text = m_NewVersion5;


                if ((m_NewVersion2 == "") && (m_NewVersion3 == "") && (m_NewVersion4 == "") && (m_NewVersion5 == ""))
                {
                    lbCaption.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_SingleVersion1 : translations.frmVersion_SingleVersion2;
                    this.Height = 240;
                }
                else
                {
                    lbCaption.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_MultipleVersions1 : translations.frmVersion_MultipleVersions2;
                    lbFurtherVersions.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_FurtherMultipleBase1 : translations.frmVersion_FurtherMultipleBase2;
                    lbFurtherVersions.Visible = true;
                    this.Height = 342;
                }

            }
            SizeF size = lbVersion.CreateGraphics().MeasureString(lbVersion.Text, lbVersion.Font);
            SizeF size2 = lbVersion2.CreateGraphics().MeasureString(lbVersion2.Text, lbVersion2.Font);
            SizeF size3 = lbVersion3.CreateGraphics().MeasureString(lbVersion3.Text, lbVersion3.Font);
            SizeF size4 = lbVersion4.CreateGraphics().MeasureString(lbVersion4.Text, lbVersion4.Font);
            SizeF size5 = lbVersion5.CreateGraphics().MeasureString(lbVersion5.Text, lbVersion5.Font);

            int MaxTextWidth = Convert.ToInt32(size.Width);
            if (size2.Width > size.Width)
            {
                MaxTextWidth = Convert.ToInt32(size2.Width);
            }
            if (size3.Width > size2.Width)
            {
                MaxTextWidth = Convert.ToInt32(size3.Width);
            }
            if (size4.Width > size3.Width)
            {
                MaxTextWidth = Convert.ToInt32(size4.Width);
            }
            if (size5.Width > size4.Width)
            {
                MaxTextWidth = Convert.ToInt32(size5.Width);
            }

            llReleaseNotes.Location = new Point(lbVersion.Left + 32 + MaxTextWidth, 95);
            llReleaseNotes2.Location = new Point(lbVersion2.Left + 32 + MaxTextWidth, 146);
            llReleaseNotes3.Location = new Point(lbVersion3.Left + 32 + MaxTextWidth, 164);
            llReleaseNotes4.Location = new Point(lbVersion4.Left + 32 + MaxTextWidth, 182);
            llReleaseNotes5.Location = new Point(lbVersion5.Left + 32 + MaxTextWidth, 200);

            llDownload.Location = new Point(llReleaseNotes.Left + 64, 95);
            llDownload2.Location = new Point(llReleaseNotes2.Left + 64, 146);
            llDownload3.Location = new Point(llReleaseNotes3.Left + 64, 164);
            llDownload4.Location = new Point(llReleaseNotes4.Left + 64, 182);
            llDownload5.Location = new Point(llReleaseNotes5.Left + 64, 200);

            this.Width = llDownload.Left + 192;
            this.CenterToScreen();
        }

        private void ConfigureLabels(string NewVersion, string NewVersion2, string NewVersion3, string NewVersion4, string NewVersion5)
        {
            if (NewVersion != "")
            {
                lbVersion.Visible = true;
            }

            if (NewVersion2 != "")
            {
                lbVersion2.Visible = true;
            }

            if (NewVersion3 != "")
            {
                lbVersion3.Visible = true;
            }

            if (NewVersion4 != "")
            {
                lbVersion4.Visible = true;
            }

            if (NewVersion5 != "")
            {
                lbVersion5.Visible = true;
            }
        }

        private void ConfigureReleaseNotesLinks(string ReleaseNotesLink, string ReleaseNotesLink2, string ReleaseNotesLink3, string ReleaseNotesLink4, string ReleaseNotesLink5)
        {
            if (ReleaseNotesLink != "")
            {
                lbVersion.Visible = true;
                llReleaseNotes.Visible = true;
                llReleaseNotes.Links.Add(0, 16, ReleaseNotesLink);
            }

            if (ReleaseNotesLink2 != "")
            {
                lbVersion2.Visible = true;
                llReleaseNotes2.Visible = true;
                llReleaseNotes2.Links.Add(0, 16, ReleaseNotesLink2);
            }

            if (ReleaseNotesLink3 != "")
            {
                lbVersion3.Visible = true;
                llReleaseNotes3.Visible = true;
                llReleaseNotes3.Links.Add(0, 16, ReleaseNotesLink3);
            }

            if (ReleaseNotesLink4 != "")
            {
                if (lbVersion4.Text != "...")
                {
                    lbVersion4.Visible = true;
                    llReleaseNotes4.Visible = true;
                    llReleaseNotes4.Links.Add(0, 20, m_ReleaseNotesLink4);
                }
            }

            if (ReleaseNotesLink5 != "")
            {
                lbVersion5.Visible = true;
                llReleaseNotes5.Visible = true;
                llReleaseNotes5.Links.Add(0, 16, ReleaseNotesLink5);
            }
        }

        private void ConfigureDownloadLinks(string DownloadLink, string DownloadLink2, string DownloadLink3, string DownloadLink4, string DownloadLink5)
        {
            if (DownloadLink != "")
            {
                lbVersion.Visible = true;
                llDownload.Visible = true;
                llDownload.Links.Add(0, 16, DownloadLink);
            }

            if (DownloadLink2 != "")
            {
                lbVersion2.Visible = true;
                llDownload2.Visible = true;
                llDownload2.Links.Add(0, 16, DownloadLink2);
            }

            if (DownloadLink3 != "")
            {
                lbVersion3.Visible = true;
                llDownload3.Visible = true;
                llDownload3.Links.Add(0, 16, DownloadLink3);
            }

            if (DownloadLink4 != "")
            {
                lbVersion4.Visible = true;
                llDownload4.Visible = true;
                llDownload4.Links.Add(0, 16, DownloadLink4);
            }

            if (DownloadLink5 != "")
            {
                lbVersion5.Visible = true;
                llDownload5.Visible = true;
                llDownload5.Links.Add(0, 16, DownloadLink5);
            }
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
                    if (!string.IsNullOrEmpty(aNode.InnerText))
                    {
                        Bitmap bitmap = RZITools.GetBitmapFromBase64String(aNode.InnerText, out ErrorText);
                        if (ErrorText != null)
                        {
                            return;
                        }
                        IntPtr icon_handle = bitmap.GetHicon();
                        Icon icon = Icon.FromHandle(icon_handle);
                        this.Icon = icon;
                    }
                }
            }
        }

        public void SetIcon(XmlNode GuidNode, out String ErrorText)
        {
            ErrorText = null;
            if (GuidNode != null)
            {
                XmlNode aNode = GuidNode.SelectSingleNode("IconBase64");
                if (aNode != null)
                {
                    if (!string.IsNullOrEmpty(aNode.InnerText))
                    {
                        Bitmap bitmap = RZITools.GetBitmapFromBase64String(aNode.InnerText, out ErrorText);
                        if (ErrorText != null)
                        {
                            return;
                        }
                        IntPtr icon_handle = bitmap.GetHicon();
                        Icon icon = Icon.FromHandle(icon_handle);
                        pbIcon.Image = icon.ToBitmap();
                        _icon = new Icon(icon, 48, 48).ToBitmap();
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

        private void llReleaseNotes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked release notes #1 link: {0}", m_ReleaseNotesLink);
            OpenBrowser(m_Guid, m_ReleaseNotesLink);
        }

        private void frmVersion_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void llDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked download #1 link: {0}", m_DownloadLink);
            OpenBrowser(m_Guid, m_DownloadLink);
        }

        

        private void llNote_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llNote.Links[llNote.Links.IndexOf(e.Link)].Visited = true;
            string target = e.Link.LinkData as string;
            CLog.Info("User clicked email link: {0}", target);
            System.Diagnostics.Process.Start(target);

        }

        private void OpenBrowser(String Guid, String URL)
        {


            try
            {
                System.Diagnostics.Process.Start(URL);
            }
            catch (Exception ex)
            {
                frmError.ShowError(ex.ToString());
            }

            /*

            try
            {
                frmBrowser browser = new frmBrowser(Guid, URL);
                browser.ShowDialog();
                browser.Dispose();
            }
            catch (Exception ex)
            {
                frmError.ShowError(ex.ToString());
            }

            */
        }

        private void llDownload2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked download #2 link: {0}", m_DownloadLink2);
            OpenBrowser(m_Guid, m_DownloadLink2);
        }

        private void llDownload3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked download #3 link: {0}", m_DownloadLink3);
            OpenBrowser(m_Guid, m_DownloadLink3);
        }

        private void llDownload4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked download #4 link: {0}", m_DownloadLink4);
            OpenBrowser(m_Guid, m_DownloadLink4);
        }

        private void llDownload5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked download #5 link: {0}", m_DownloadLink5);
            OpenBrowser(m_Guid, m_DownloadLink5);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CLog.Info("User clicked OK button, dialogue will be closed.");
        }

        private void llReleaseNotes2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked release notes #2 link: {0}", m_DownloadLink2);
            OpenBrowser(m_Guid, m_ReleaseNotesLink2);
        }

        private void llReleaseNotes3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked release notes #3 link: {0}", m_DownloadLink3);
            OpenBrowser(m_Guid, m_ReleaseNotesLink3);
        }

        private void llReleaseNotes4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked release notes #4 link: {0}", m_DownloadLink4);
            OpenBrowser(m_Guid, m_ReleaseNotesLink4);
        }

        private void llReleaseNotes5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CLog.Info("User clicked release notes #5 link: {0}", m_DownloadLink5);
            OpenBrowser(m_Guid, m_ReleaseNotesLink5);
        }
    }
}
