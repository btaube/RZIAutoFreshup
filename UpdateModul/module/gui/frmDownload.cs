using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using UpdateModul.Properties;
using UpdateModul.shared;

namespace UpdateModul
{
    public partial class frmDownload : Form
    {
        private String m_DownloadUrl;
        private String m_WorkingDirectory;
        private DialogResult m_DialogResult;

        public String m_ErrorText { get; private set; }

        Bitmap _icon;

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

        public void SetText()
        {
            if (CGlobVars.VersionDescriptionID == 1)
            {
                lLookingForUpdates.Text = translations.frmDownload_LookingForVersions1;
            }
            else
            {
                lLookingForUpdates.Text = translations.frmDownload_LookingForVersions2;
            }
        }

        

        public frmDownload(string DownloadUrl, String WorkingDirectory)
        {
            string ErrorText = "";
            InitializeComponent();
            SetText();
            SetCorporateDesign(CGlobVars.CurrentGUID, out ErrorText);
            pbIcon.Image = _icon;

            m_DownloadUrl = DownloadUrl;
            m_WorkingDirectory = WorkingDirectory;

            if ((DownloadUrl != null) && (DownloadUrl != ""))
            {
                labDownloadLocation.Text = Path.GetDirectoryName(m_DownloadUrl);
                labFile.Text = Path.GetFileName(m_DownloadUrl);
            }
            this.Text = (CGlobVars.VersionDescriptionID == 1) ? translations.frmVersion_Title1 : translations.frmVersion_Title2;
        }

        public void updateStatus(int value)
        {
            pbStatus.Value = value;
        }

        private Thread m_DownloadThread;
        private void frmDownload_Load(object sender, EventArgs e)
        {
            if ((CGlobVars.silentModeNoNewVersion) || (CGlobVars.silentModeError))
            {
                this.Opacity = 0;
            }
            if ((m_DownloadUrl != null) && (m_DownloadUrl != ""))
            {
                m_DownloadThread = new Thread(delegate ()
               {
                   try
                   {
                       String ErrorText;
                       if (RZITools.DownloadFile(m_DownloadUrl, m_WorkingDirectory,
                               (CurrentBytes, CompleteBytes) =>
                               {
                                   this.Invoke(new Action(delegate
                                   {
                                       if (CompleteBytes != pbStatus.Maximum)
                                       {
                                           if (CompleteBytes == -1)
                                           {
                                               pbStatus.Maximum = 100;
                                               pbStatus.Minimum = 1;
                                           }
                                           else
                                           {
                                               pbStatus.Maximum = (int)CompleteBytes;
                                               pbStatus.Minimum = 1;
                                           }

                                       }

                                       if (CompleteBytes == -1)
                                       {
                                           pbStatus.Value = 1;
                                           labStatus.Text = String.Format("{0} / {1} KB", CurrentBytes / 1000, 0);
                                       }
                                       else
                                       {
                                           pbStatus.Value = (int)CurrentBytes;
                                           labStatus.Text = String.Format("{0} / {1} KB", CurrentBytes / 1000, CompleteBytes / 1000);
                                       }


                                       pbStatus.Invalidate();
                                       labStatus.Invalidate();
                                   }));
                               },
                               out ErrorText))
                       {
                           m_ErrorText = String.Empty;
                           m_DialogResult = DialogResult.OK;
                       }
                       else
                       {
                           m_ErrorText = ErrorText;
                           m_DialogResult = DialogResult.Cancel;
                       }
                   }
                   catch (ThreadAbortException)
                   {
                       m_ErrorText = translations.DOWNLOAD_CANCELLED;
                       m_DialogResult = DialogResult.Cancel;
                   }
                   catch (Exception exc)
                   {
                       m_ErrorText = exc.Message;
                       m_DialogResult = DialogResult.Cancel;
                   }
                   finally
                   {
                       this.Invoke(new Action(delegate
                       {
                           try
                           {
                               DialogResult = m_DialogResult;
                               Close();
                           }
                           catch
                           {
                           }
                       }));
                   }


               });
                m_DownloadThread.Start();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_ErrorText = translations.DOWNLOAD_CANCELLED;
            if (m_DownloadThread != null)
            {
                m_DownloadThread.Abort();
            }
            if (RZITools._file != null)
            {
                try
                {
                    RZITools._file.Close();
                    RZITools._file.Dispose();
                } catch (Exception file)
                {

                }
            }
            if (RZITools._fstr != null)
            {
                try
                {
                    RZITools._fstr.Close();
                    RZITools._fstr.Dispose();
                }
                catch (Exception fstr)
                {

                }
            }
            if (RZITools._ms != null)
            {
                try
                {
                    RZITools._ms.Close();
                    RZITools._ms.Dispose();
                }
                catch (Exception ms)
                {

                }
            }
        }

        private void frmDownload_Shown(object sender, EventArgs e)
        {
            if ((CGlobVars.silentModeNoNewVersion) || (CGlobVars.silentModeError))
            {
                this.Opacity = 0;
                return;
            }
            BringToFront();
        }
    }
}
