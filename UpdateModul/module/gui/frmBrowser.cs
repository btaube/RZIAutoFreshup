using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using UpdateModul;


namespace UpdateModul
{


    


    //ToDo
    //http://www.journeyintocode.com/2013/08/c-webbrowser-control-proxy.html



    public partial class frmBrowser : Form
    {
        private String m_DownloadPath;
        private String m_Guid;

        [STAThread]
        private void runBrowserThread(Uri url)
        {
            var th = new Thread(() => {
                var br = new WebBrowser();
                br.Parent = pnlBrowser;
                br.Visible = true;
                br.Dock = DockStyle.Fill;
                br.Height = 500;
                br.Width = 500;
                br.DocumentCompleted += browser_DocumentCompleted;
                br.Navigate(url);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var br = sender as WebBrowser;
            if (br.Url == e.Url)
            {
                Console.WriteLine("Natigated to {0}", e.Url);
                CLog.Debug("4444444444442222222222222sdfsdfsdfdsfsdf");
                Application.ExitThread();   // Stops the thread
                CLog.Debug("33333333333222222222222sdfsdfsdfdsfsdf");
            }
        }


        public frmBrowser(String Guid, String DownloadPath)
        {
 
            InitializeComponent();

            m_DownloadPath = DownloadPath;
            m_Guid = Guid;

            CLog.Debug("111111111111111111111111sdfsdfsdfdsfsdf");
            //runBrowserThread(new Uri(m_DownloadPath));
            CLog.Debug("222222222222222222222222sdfsdfsdfdsfsdf");
            //m_DownloadPath = "http://de.download.nvidia.com/solaris/319.72/NVIDIA-Solaris-x86-319.72.run";
            //m_DownloadPath = "http://www.google.de";


        }

        

        private void SetCanGoBack(bool canGoBack)
        {
            this.InvokeOnUiThreadIfRequired(() => backButton.Enabled = canGoBack);
        }

        private void SetCanGoForward(bool canGoForward)
        {
            this.InvokeOnUiThreadIfRequired(() => forwardButton.Enabled = canGoForward);
        }

        private void SetIsLoading(bool isLoading)
        {
            goButton.Text = isLoading ?
                "Stop" :
                "Go";
            goButton.Image = isLoading ?
                Properties.Resources.nav_plain_red :
                Properties.Resources.nav_plain_green;

            HandleToolStripLayout();
        }
        
        private void HandleToolStripLayout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            var width = toolStrip1.Width;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != urlTextBox)
                {
                    width -= item.Width - item.Margin.Horizontal;
                }
            }
            urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
        }



        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                webBrowser1.Navigate(url);
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

        private void butOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmBrowser_Load(object sender, EventArgs e)
        {
            String ErrorText;
            try
            {
                webBrowser1.Navigate("about:blank");
                urlTextBox.Text = m_DownloadPath;
                //m_DownloadPath = "http://www.ds-punkte.de/test.zip";
                webBrowser1.Navigate(m_DownloadPath);
            }
            catch (Exception ex)
            {
                ErrorText = ex.ToString();
            }
        }

        private void frmBrowser_Shown(object sender, EventArgs e)
        {
            String ErrorText;
            SetCorporateDesign(m_Guid, out ErrorText);
            RZITools.DownloadPercent = 0;

            BringToFront();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void urlTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            LoadUrl(urlTextBox.Text);
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            LoadUrl(urlTextBox.Text);
        }

        private void webBrowser1_FileDownload(object sender, EventArgs e)
        {
            
        }
    }
}
