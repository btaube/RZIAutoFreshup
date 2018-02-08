using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using UpdateModul.shared;
using System.Threading;
using UpdateModul.Properties;

namespace UpdateModul
{
    public partial class frmMain : Form
    {

        private string m_InitPassw = "";
        private string m_InitCheckDays = "";
        private string m_InitLogLevel = "";
        private string m_InitLookupURL = "";
        private List<string> m_lsInitPR = new List<string>();

        private bool m_HasChangedPassw = false;
        private bool m_HasChangedCheckDays = false;
        private bool m_HasChangedLoglevel = false;
        private bool m_HasChangedLookupURL = false;


        private bool m_InitProxyUseProxy = false;
        private string m_InitProxyServer = "";
        private string m_InitProxyPort = "";
        private bool m_InitProxyUseDefCred = false;
        private string m_InitProxyUser = "";
        private string m_InitProxyPW = "";
        private bool m_InitProxyBypass = false;

        private bool m_HasChangedProxyServer = false;
        private bool m_HasChangedProxyPort = false;
        private bool m_HasChangedProxyUseDefCred = false;
        private bool m_HasChangedProxyUseProxy = false;
        private bool m_HasChangedProxyUser = false;
        private bool m_HasChangedProxyPW = false;
        private bool m_HasChangedProxyBypass = false;

        public bool InitProxyUseProxy
        {
            get
            {
                return m_InitProxyUseProxy;
            }

            set
            {
                m_InitProxyUseProxy = value;
            }
        }

        public frmMain()
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
                }
            }
        }

        public void SetIcon(String GUID, out String ErrorText)
        {
            ErrorText = null;
            XmlNode Node = CVersionLookupHelper.GetNodeByTagName(GUID);
            if (Node != null)
            {
                XmlNode aNode = Node.SelectSingleNode("IconBase64");
                if (aNode != null)
                {
                    Bitmap bitmap = RZITools.GetBitmapFromBase64String(aNode.InnerText, out ErrorText);
                    if (ErrorText != null)
                    {
                        return;
                    }
                    IntPtr icon_handle = bitmap.GetHicon();
                    this.Icon = Icon.FromHandle(icon_handle);

                }
                else
                {
                    Bitmap aIcon = null;
                    if (CGlobVars.VersionDescriptionID == 1)
                    {
                        try
                        {
                            aIcon = new Icon(Resources.RZI, 48, 48).ToBitmap();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            aIcon = Bitmap.FromHicon(new Icon(Resources.RZI, new Size(24, 24)).Handle);
                        }
                    }
                    else
                    {
                        try
                        {
                            aIcon = new Icon(Resources.card1, 48, 48).ToBitmap();
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            aIcon = Bitmap.FromHicon(new Icon(Resources.card1, new Size(24, 24)).Handle);
                        }
                    }
                    if (aIcon != null)
                    {
                        IntPtr icon_handle = aIcon.GetHicon();
                        this.Icon = Icon.FromHandle(icon_handle);
                    }

                }
            }
            else
            {
                Bitmap aIcon = null;
                if (CGlobVars.VersionDescriptionID == 1)
                {
                    try
                    {
                        aIcon = new Icon(Resources.RZI, 48, 48).ToBitmap();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        aIcon = Bitmap.FromHicon(new Icon(Resources.RZI, new Size(24, 24)).Handle);
                    }
                }
                else
                {
                    try
                    {
                        aIcon = new Icon(Resources.card1, 48, 48).ToBitmap();
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        aIcon = Bitmap.FromHicon(new Icon(Resources.card1, new Size(24, 24)).Handle);
                    }
                }
                if (aIcon != null)
                {
                    IntPtr icon_handle = aIcon.GetHicon();
                    this.Icon = Icon.FromHandle(icon_handle);
                }
            }
        }


        private void BdetailSave_Click(object sender, EventArgs e)
        {
            String ErrorText;
            if (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Common//CheckDays", numDetailsCheckDays.Text, out ErrorText))
            {
                LoadDetailsCommon((XmlNode)grpboxDetailCommon.Tag, out ErrorText);
            }
            else
            {
                frmError.ShowError(ErrorText);
            }
        }


        private void LoadTreeview()
        {
            TVmain.BeginUpdate();
            try
            {
                TVmain.Nodes.Clear();
                XmlNode aVersioNode = CVersionConfigHelper.GetNodeByTagName("Settings");
                if (aVersioNode != null)
                {
                    TVmain.Nodes.Add(new TreeNode(CVersionConfigHelper.xmlDoc.DocumentElement.Name));
                    TreeNode tNode = new TreeNode();
                    tNode = TVmain.Nodes[0];
                    tNode.NodeFont = new Font(TVmain.Font, FontStyle.Bold);
                    tNode.Text = translations.frmMain_tvMain_Settings;
                    tNode.ImageIndex = -1;
                    tNode.SelectedImageIndex = -1;
                    tNode.Tag = aVersioNode;

                    AddNode(aVersioNode, tNode);

                    //
                }

            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(translations.frmMain_Dlg_LoadTreeview + xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(translations.frmMain_Dlg_LoadTreeview + ex.Message);
            }
            TVmain.EndUpdate();

            TVmain.Sort();
            TVmain.ExpandAll();
            TVmain.Nodes[0].Text = translations.frmMain_tvMain_Versions;

        }

        private void LoadDetailsCommon(XmlNode aNode, out String ErrorText)
        {
            ErrorText = null;
            if (aNode == null)
            {
                //ErrorText = "";
                return;
            }


            grpboxDetailCommon.Visible = true;
            grpboxDetailCommon.Tag = aNode;
            cbCommonLogLevel.SelectedIndex = 0;
            numDetailsCheckDays.Text = "0";

            foreach (XmlNode itNode in aNode.ChildNodes)
            {
                if ("CheckDays".Equals(itNode.Name))
                {
                    numDetailsCheckDays.Text = itNode.InnerText.Trim();

                }
                else if ("LastChecked".Equals(itNode.Name))
                {
                    try
                    {
                        lDetailsCommonCheckDate.Text = RZITools.UnixTimeToDateTime(Convert.ToInt64(itNode.InnerText.Trim()), out ErrorText).ToString();
                    }
                    catch
                    {
                        lDetailsCommonCheckDate.Text = "n/a";
                    }


                }
                else if ("LogLevel".Equals(itNode.Name))
                {
                    if ("debug".Equals(itNode.InnerText.Trim().ToLower()))
                    {
                        cbCommonLogLevel.SelectedIndex = 1;
                    }
                    else
                    {
                        cbCommonLogLevel.SelectedIndex = 0;
                    }
                }
            }


            m_InitCheckDays = numDetailsCheckDays.Text;
            m_HasChangedCheckDays = false;

            m_InitLogLevel = cbCommonLogLevel.Text;
            m_HasChangedLoglevel = false;

            configureSaveButtonCommon();

        }

        private void LoadDetailsSecurity(XmlNode aNode, out String ErrorText)
        {
            ErrorText = null;
            if (aNode == null)
            {
                ErrorText = "";
                return;
            }

            grpboxDetailSecurity.Visible = true;
            grpboxDetailSecurity.Tag = aNode;

            txDetailsPassword.Text = "";
            txDetailsPasswordConfirm.Text = "";


            foreach (XmlNode itNode in aNode.ChildNodes)
            {
                if ("Password".Equals(itNode.Name))
                {
                    try
                    {
                        //txDetailsPassword.Text = RZITools.Decrypt(itNode.InnerText.Trim());
                    }
                    catch (Exception)
                    {
                        txDetailsPassword.Text = "";
                    }

                }
            }

            m_InitPassw = txDetailsPassword.Text;
            m_HasChangedPassw = false;
            configureSaveButtonSecurity();

        }

        private void LoadDetailsRepository(XmlNode aNode, out String ErrorText)
        {
            ErrorText = null;
            if (aNode == null)
            {
                ErrorText = "";
                return;
            }

            grpboxDetailRepository.Visible = true;
            grpboxDetailRepository.Tag = aNode;

            txDetailsRepositoryRepo.Text = "";

            foreach (XmlNode itNode in aNode.ChildNodes)
            {
                if ("LookupURL".Equals(itNode.Name))
                {
                    txDetailsRepositoryRepo.Text = itNode.InnerText.Trim();
                }
            }

            m_InitLookupURL = txDetailsRepositoryRepo.Text;
            m_HasChangedLookupURL = false;
            configureSaveButtonRepository();

        }

        private void LoadDetailsProxy(XmlNode aNode, out String ErrorText)
        {
            ErrorText = null;
            if (aNode == null)
            {
                return;
            }

            grpboxDetailProxy.Visible = true;
            grpboxDetailProxy.Tag = aNode;

            txProxyServer.Text = "";
            txProxyPort.Text = "";
            txProxyUser.Text = "";
            txProxyPW.Text = "";
            chkbxProxyUseProxy.Checked = false;
            chkbxProxyUseDefCred.Checked = false;
            chkbxProxyBypassServer.Checked = false;

            foreach (XmlNode itNode in aNode.ChildNodes)
            {
                if ("ProxyServer".Equals(itNode.Name))
                {
                    txProxyServer.Text = itNode.InnerText.Trim();
                }
                else if ("ProxyPort".Equals(itNode.Name))
                {
                    txProxyPort.Text = itNode.InnerText.Trim();
                }
                else if ("ProxyUser".Equals(itNode.Name))
                {
                    txProxyUser.Text = itNode.InnerText.Trim();
                }
                else if ("ProxyPassw".Equals(itNode.Name))
                {
                    txProxyPW.Text = RZITools.Decrypt(itNode.InnerText.Trim(), out ErrorText);
                }
                else if ("UseProxy".Equals(itNode.Name))
                {
                    if (itNode.InnerText.Trim() == "1")
                    {
                        chkbxProxyUseProxy.Checked = true;
                    }
                    else
                    {
                        chkbxProxyUseProxy.Checked = false;
                    }
                }
                else if ("UseDefCred".Equals(itNode.Name))
                {
                    if (itNode.InnerText.Trim() == "1")
                    {
                        chkbxProxyUseDefCred.Checked = true;
                    }
                    else
                    {
                        chkbxProxyUseDefCred.Checked = false;
                    }
                }
                else if ("BypassOnLan".Equals(itNode.Name))
                {
                    if (itNode.InnerText.Trim() == "1")
                    {
                        chkbxProxyBypassServer.Checked = true;
                    }
                    else
                    {
                        chkbxProxyBypassServer.Checked = false;
                    }
                }
            }

            m_InitProxyServer = txProxyServer.Text;
            m_InitProxyPort = txProxyPort.Text;
            m_InitProxyUser = txProxyUser.Text;
            m_InitProxyPW = txProxyPW.Text;

            m_InitProxyUseProxy = chkbxProxyUseProxy.Checked;
            m_InitProxyUseDefCred = chkbxProxyUseDefCred.Checked;
            m_InitProxyBypass = chkbxProxyBypassServer.Checked;

            m_HasChangedProxyUseProxy = false;
            m_HasChangedProxyServer = false;
            m_HasChangedProxyPort = false;
            m_HasChangedProxyUseDefCred = false;
            m_HasChangedProxyUser = false;
            m_HasChangedProxyPW = false;
            m_HasChangedProxyBypass = false;

            configureSaveButtonProxy();


            if (chkbxProxyUseProxy.Checked)
            {
                txProxyServer.Enabled = true;
                txProxyPort.Enabled = true;
                chkbxProxyUseDefCred.Enabled = true;
                if (chkbxProxyUseDefCred.Checked)
                {
                    txProxyUser.Enabled = false;
                    txProxyPW.Enabled = false;
                }
                else
                {
                    txProxyUser.Enabled = true;
                    txProxyPW.Enabled = true;
                }
                chkbxProxyBypassServer.Enabled = true;
            }
            else
            {
                txProxyServer.Enabled = false;
                txProxyPort.Enabled = false;
                chkbxProxyUseDefCred.Enabled = false;
                txProxyUser.Enabled = false;
                txProxyPW.Enabled = false;
                chkbxProxyBypassServer.Enabled = false;
            }

        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            //
            //string ErrorText;
            // load xml file
            //if (!CVersionConfigHelper.LoadVersionConfigXML(out ErrorText))
            //{
            //    frmError.ShowError(ErrorText);
            //    return;
            //}

            // add file path to title
            String title = this.Text;
            if (CGlobVars.VersionDescriptionID == 1)
            {
                if (CGlobVars.CurrentLanguage.ToLower().Contains("de"))
                {
                    title = "RZI Updater konfigurieren";
                }
                else
                {
                    title = "RZI Update Configuration";
                }
            }
            else
            {
                if (CGlobVars.CurrentLanguage.ToLower().Contains("de"))
                {
                    title = "CARD/1 Updater konfigurieren";
                }
                else
                {
                    title = "CARD/1 Update Configuration";
                }
            }

            //if (title.IndexOf(" (") > 0)
            //{
            //    title = title.Substring(0, title.IndexOf(" ("));
            //}
            //title = title + " (" + CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML + ")";
            this.Text = title;

            // Load Corporate Identity


            // load treeview
            LoadTreeview();
        }



        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            // Loop through the XML nodes until the leaf is reached.
            // Add the nodes to the TreeView during the looping process.
            if (inXmlNode.HasChildNodes)
            {
                foreach (XmlNode xNode in inXmlNode.ChildNodes)
                {
                    if (xNode.Attributes["name"] != null)
                    {
                        TreeNode newNode = new TreeNode(xNode.Attributes["name"].Value);
                        if ("Common".Equals(xNode.Attributes["name"].Value))
                        {
                            newNode.Text = translations.frmMain_tvMain_Settings_Common;
                        }
                        else if ("Security".Equals(xNode.Attributes["name"].Value))
                        {
                            newNode.Text = translations.frmMain_tvMain_Settings_Security;
                        }
                        else if ("Repository".Equals(xNode.Attributes["name"].Value))
                        {
                            newNode.Text = translations.frmMain_tvMain_Settings_Repository;
                        }
                        newNode.Tag = xNode;
                        newNode.SelectedImageIndex = 1;
                        newNode.ImageIndex = 1;
                        inTreeNode.Nodes.Add(newNode);
                        AddNode(xNode, newNode);
                    }
                }
            }
        }



        private void TVmain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TVmain.SelectedNode = e.Node;
            }
        }

        private void TVmain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string ErrorText = null;

            XmlNode node = (XmlNode)e.Node.Tag;
            if (node == null)
            {
                return;
            }

            grpboxDetailCommon.Visible = false;
            grpboxDetailRepository.Visible = false;
            grpboxDetailSecurity.Visible = false;
            grpboxDetailProxy.Visible = false;

            if ((node.Attributes != null) && (node.Attributes["name"] != null) && ("Common".Equals(node.Attributes["name"].Value)))
            {
                LoadDetailsCommon(node, out ErrorText);

            }
            else if ((node.Attributes != null) && (node.Attributes["name"] != null) && ("Repository".Equals(node.Attributes["name"].Value)))
            {
                LoadDetailsRepository(node, out ErrorText);
            }
            else if ((node.Attributes != null) && (node.Attributes["name"] != null) && ("Security".Equals(node.Attributes["name"].Value)))
            {
                LoadDetailsSecurity(node, out ErrorText);
            }
            else if ((node.Attributes != null) && (node.Attributes["name"] != null) && ("Proxy".Equals(node.Attributes["name"].Value)))
            {
                LoadDetailsProxy(node, out ErrorText);
            }
            if (ErrorText != null)
            {
                frmError.ShowError(ErrorText);
            }

            XmlNode referenceNode = (XmlNode)e.Node.Tag;
            if (referenceNode != null)
            {
                string captionPath = CVersionConfigHelper.GetFullPathByTagName(referenceNode.Name, out ErrorText);
                if (ErrorText != null)
                {
                    frmError.ShowError(ErrorText);
                }
                if (captionPath.StartsWith(" » "))
                {
                    captionPath = captionPath.Substring(3);
                }
                // Ausblenden, nicht mehr anzeigen.
                //GRPBXmain.Text = captionPath;
            }


        }

        private void configureSaveButtonCommon()
        {
            if (m_HasChangedCheckDays || m_HasChangedLoglevel)
            {
                TVmain.Enabled = false;
                bDetailSaveCommon.Enabled = true;
                bDetailCancelCommon.Enabled = true;
            }
            else
            {
                TVmain.Enabled = true;
                bDetailSaveCommon.Enabled = false;
                bDetailCancelCommon.Enabled = false;
            }

        }

        private void configureSaveButtonRepository()
        {
            if (m_HasChangedLookupURL)
            {
                TVmain.Enabled = false;
                bDetailsSaveRepository.Enabled = true;
                bDetailsCancelRepository.Enabled = true;
            }
            else
            {
                TVmain.Enabled = true;
                bDetailsSaveRepository.Enabled = false;
                bDetailsCancelRepository.Enabled = false;
            }

        }

        private void configureSaveButtonSecurity()
        {
            if (m_HasChangedPassw)
            {
                TVmain.Enabled = false;
                if ((txDetailsPassword.Text.Trim().Length > 0) && (txDetailsPassword.Text.Equals(txDetailsPasswordConfirm.Text)))
                {
                    BdetailSaveSecurity.Enabled = true;
                }
                else
                {
                    BdetailSaveSecurity.Enabled = false;
                }
                BdetailCancelSecurity.Enabled = true;
            }
            else
            {
                TVmain.Enabled = true;
                BdetailSaveSecurity.Enabled = false;
                BdetailCancelSecurity.Enabled = false;
            }

        }

        private void configureSaveButtonProxy()
        {
            if (m_HasChangedProxyUseProxy || m_HasChangedProxyServer || m_HasChangedProxyPort || m_HasChangedProxyUseDefCred || m_HasChangedProxyUser || m_HasChangedProxyPW || m_HasChangedProxyBypass)
            {
                TVmain.Enabled = false;
                bProxySave.Enabled = true;
                bProxyCancel.Enabled = true;
                bProxyTest.Enabled = false;
            }
            else
            {
                TVmain.Enabled = true;
                bProxySave.Enabled = false;
                bProxyCancel.Enabled = false;
                bProxyTest.Enabled = true;
            }

        }


        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numDetailsCheckDays_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown o = (NumericUpDown)sender;
            int thisValue = (int)o.Value;
            if (m_InitCheckDays.Trim().Equals(thisValue.ToString()))
            {
                m_HasChangedCheckDays = false;
            }
            else
            {
                m_HasChangedCheckDays = true;
            }
            configureSaveButtonCommon();
        }

        private void bDetailCancelCommon_Click(object sender, EventArgs e)
        {
            string ErrorText;
            LoadDetailsCommon((XmlNode)grpboxDetailCommon.Tag, out ErrorText);
            if (ErrorText != null)
            {
                frmError.ShowError(ErrorText);
            }
        }

        private void BdetailCancelSecurity_Click(object sender, EventArgs e)
        {
            string ErrorText;
            LoadDetailsSecurity((XmlNode)grpboxDetailSecurity.Tag, out ErrorText);
            if (ErrorText != null)
            {
                frmError.ShowError(ErrorText);
            }
        }

        private void bDetailsCancelRepository_Click(object sender, EventArgs e)
        {
            string ErrorText;
            LoadDetailsRepository((XmlNode)grpboxDetailRepository.Tag, out ErrorText);
            if (ErrorText != null)
            {
                frmError.ShowError(ErrorText);
            }
        }

        private void txDetailsRepositoryRepo_TextChanged(object sender, EventArgs e)
        {
            if (m_InitLookupURL.Trim().Equals(txDetailsRepositoryRepo.Text.Trim()))
            {
                m_HasChangedLookupURL = false;
            }
            else
            {
                m_HasChangedLookupURL = true;
            }
            configureSaveButtonRepository();
        }

        private void txDetailsPassword_TextChanged(object sender, EventArgs e)
        {
            if (m_InitPassw.Trim().Equals(txDetailsPassword.Text.Trim()))
            {
                m_HasChangedPassw = false;
            }
            else
            {
                m_HasChangedPassw = true;
            }
            configureSaveButtonSecurity();
        }

        private void numDetailsCheckDays_KeyUp(object sender, KeyEventArgs e)
        {
            if (m_InitCheckDays.Trim().Equals(numDetailsCheckDays.Text.Trim()))
            {
                m_HasChangedCheckDays = false;
            }
            else
            {
                m_HasChangedCheckDays = true;
            }
            configureSaveButtonCommon();
        }



        private void txDetailsPasswordConfirm_TextChanged(object sender, EventArgs e)
        {
            configureSaveButtonSecurity();
        }

        private void bDetailSaveCommon_Click(object sender, EventArgs e)
        {
            string ErrorText;
            if (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Common//CheckDays", numDetailsCheckDays.Text, out ErrorText) &&
                CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Common//LogLevel", cbCommonLogLevel.Text, out ErrorText))
            {
                LoadDetailsCommon((XmlNode)grpboxDetailCommon.Tag, out ErrorText);
            }
            else
            {
                frmError.ShowError(ErrorText);
            }
        }

        private void BdetailSaveSecurity_Click(object sender, EventArgs e)
        {
            string ErrorText;
            if (CVersionConfigHelper.SetInnerTextByPathAsPW("UpdateCheckContent//Security/Password", txDetailsPassword.Text, out ErrorText))
            {
                LoadDetailsSecurity((XmlNode)grpboxDetailSecurity.Tag, out ErrorText);
            }
            else
            {
                frmError.ShowError(ErrorText);
            }
        }

        private void bDetailsSaveRepository_Click(object sender, EventArgs e)
        {
            string ErrorText;
            if (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Repository//LookupURL", txDetailsRepositoryRepo.Text, out ErrorText))
            {
                LoadDetailsRepository((XmlNode)grpboxDetailRepository.Tag, out ErrorText);
            }
            else
            {
                frmError.ShowError(ErrorText);
            }
        }





        private void cbCommonLogLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_InitLogLevel.Trim().Equals(cbCommonLogLevel.Text.Trim()))
            {
                m_HasChangedLoglevel = false;
            }
            else
            {
                m_HasChangedLoglevel = true;
            }
            configureSaveButtonCommon();
        }

        /// <summary>
        /// Decrypts a provided file and loads VersionLookup.xml.
        /// </summary>
        /// <param name="VersionLookupFile"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        private int DecryptAndLoadVersionLookupXML(String VersionLookupFile, bool ShowErrorIfNotExist, out string ErrorText)
        {
            if (!File.Exists(VersionLookupFile))
            {
                if (ShowErrorIfNotExist)
                {
                    frmError.ShowError(translations.CVersionLookupHelper_CannotLoadXML);
                }
                ErrorText = "";
                return CReturnCodes.COULD_NOT_DECRYPT_VERSION_LOOKUP_XML;
            }

            // Decrypt File
            CLog.Info("Starting decryption of downloaded file.");
            if (!RZITools.DecryptFile(VersionLookupFile, CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML, CGlobVars.PASSWORD_TOKEN, out ErrorText))
            {
                if (ShowErrorIfNotExist)
                {
                    frmError.ShowError(ErrorText);
                }
                if (File.Exists(VersionLookupFile))
                {
                    try
                    {
                        File.Delete(VersionLookupFile);
                    } catch (Exception)
                    {

                    }
                }
                if (File.Exists(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML))
                {
                    try
                    {
                        File.Delete(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML);
                    }
                    catch (Exception)
                    {

                    }
                }

                return CReturnCodes.COULD_NOT_DECRYPT_VERSION_LOOKUP_XML;
            }


            // Create delay due to anti virus scans that may lock the file
            // TODO Replace with better way
            Thread.Sleep(500);


            // Load Current Version
            CLog.Info("Loading downloaded VersionLookupXML.");
            if (!CVersionLookupHelper.LoadVersionLookupXML(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML, false))
            {
                CLog.Error("Could not load VersionLookupXML.");
                CLog.Error("Error message: {0}", ErrorText);
                frmError.ShowError(ErrorText);
                return CReturnCodes.COULD_NOT_LOAD_VERSIONLOOKUP_XML;
            }


            // Delete decrypted file
            File.Delete(CGlobVars.wrkDir + "VersionLookup.xml");
            if (File.Exists(CGlobVars.wrkDir + "VersionLookup.xml"))
            {
                CLog.Warning("Could not delete VersionLookup.xml from disk: {0}", CGlobVars.wrkDir + "VersionLookup.xml");
            }
            else
            {
                CLog.Info("Deleted VersionLookup.xml from disk successfully.");
            }

            return CReturnCodes.OK;
        }



        private void bProxyTest_Click(object sender, EventArgs e)
        {
            string ErrorText;
            SaveFileDialog sfdVersionLookup = new SaveFileDialog();
            // image filters
            sfdVersionLookup.FileName = CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML;
            if (sfdVersionLookup.ShowDialog() == DialogResult.OK)
            {
                frmDownload dlgDownload = new frmDownload(CVersionConfigHelper.GetInnerTextByPathAsString("UpdateCheckContent//Repository//LookupURL", out ErrorText), Path.GetDirectoryName(sfdVersionLookup.FileName) + Path.DirectorySeparatorChar);
                if (dlgDownload.ShowDialog() == DialogResult.OK)
                {
                    int returnVal = DecryptAndLoadVersionLookupXML(sfdVersionLookup.FileName, false, out ErrorText);
                    if (returnVal == CReturnCodes.OK)
                    {
                        richTextBox1.Text = "Test okay";
                    }
                    else
                    {
                        if (returnVal == CReturnCodes.COULD_NOT_DECRYPT_VERSION_LOOKUP_XML)
                        {
                            richTextBox1.Text = "Decryption failed. Please check the repository path and make sure that a correct VersionLookup file can be downloaded: \n\n";
                            try
                            {
                                List<string> lines = File.ReadAllLines(sfdVersionLookup.FileName).ToList();
                                foreach (string current in lines)
                                {
                                    richTextBox1.Text += current;
                                }
                            }
                            catch
                            {

                            }

                        }
                        if (returnVal == CReturnCodes.COULD_NOT_LOAD_VERSIONLOOKUP_XML)
                        {
                            richTextBox1.Text = "Loading failed. It seems the file has not been downloaded correctly. Please check your proxy settings and make sure that you have set up the repository path properly: \n\n ";
                            try
                            {
                                List<string> lines = File.ReadAllLines(sfdVersionLookup.FileName).ToList();
                                foreach (string current in lines)
                                {
                                    richTextBox1.Text += current;
                                }
                            }
                            catch
                            {

                            }
                        }
                    }

                }
                else
                {
                    richTextBox1.Text = "Test fehlgeschlagen: " + "\r\n\r\n" + CLog.LastError;
                }
            }
        }


        private void chkbxProxyUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            if (m_InitProxyUseProxy.Equals(chkbxProxyUseProxy.Checked))
            {
                m_HasChangedProxyUseProxy = false;
            }
            else
            {
                m_HasChangedProxyUseProxy = true;
            }
            configureSaveButtonProxy();

            if (chkbxProxyUseProxy.Checked)
            {
                txProxyServer.Enabled = true;
                txProxyPort.Enabled = true;
                chkbxProxyUseDefCred.Enabled = true;
                if (chkbxProxyUseDefCred.Checked)
                {
                    txProxyUser.Enabled = false;
                    txProxyPW.Enabled = false;
                }
                else
                {
                    txProxyUser.Enabled = true;
                    txProxyPW.Enabled = true;
                }
                chkbxProxyBypassServer.Enabled = true;
            }
            else
            {
                txProxyServer.Enabled = false;
                txProxyPort.Enabled = false;
                chkbxProxyUseDefCred.Enabled = false;
                txProxyUser.Enabled = false;
                txProxyPW.Enabled = false;
                chkbxProxyBypassServer.Enabled = false;
            }
        }

        private void chkbxProxyBypassServer_CheckedChanged(object sender, EventArgs e)
        {
            if (m_InitProxyBypass.Equals(chkbxProxyBypassServer.Checked))
            {
                m_HasChangedProxyBypass = false;
            }
            else
            {
                m_HasChangedProxyBypass = true;
            }
            configureSaveButtonProxy();
        }

        private void bProxySave_Click(object sender, EventArgs e)
        {
            string ErrorText;
            if ((CVersionConfigHelper.SetInnerTextByPathAsBool("UpdateCheckContent//Proxy//UseProxy", chkbxProxyUseProxy.Checked, out ErrorText)) &&
                (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Proxy//ProxyServer", txProxyServer.Text.Trim(), out ErrorText)) &&
                (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Proxy//ProxyPort", txProxyPort.Text.Trim(), out ErrorText)) &&
                (CVersionConfigHelper.SetInnerTextByPathAsBool("UpdateCheckContent//Proxy//UseDefCred", chkbxProxyUseDefCred.Checked, out ErrorText)) &&
                (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Proxy//ProxyUser", txProxyUser.Text.Trim(), out ErrorText)) &&
                (CVersionConfigHelper.SetInnerTextByPathAsPW("UpdateCheckContent//Proxy//ProxyPassw", txProxyPW.Text.Trim(), out ErrorText)) &&
                (CVersionConfigHelper.SetInnerTextByPathAsBool("UpdateCheckContent//Proxy//BypassOnLan", chkbxProxyBypassServer.Checked, out ErrorText)))
            {
                LoadDetailsProxy((XmlNode)grpboxDetailProxy.Tag, out ErrorText);
            }
            if (ErrorText != null)
            {
                frmError.ShowError(ErrorText);
            }
        }

        private void chkbxProxyUseDefCred_CheckedChanged(object sender, EventArgs e)
        {
            if (m_InitProxyUseDefCred.Equals(chkbxProxyUseDefCred.Checked))
            {
                m_HasChangedProxyUseDefCred = false;
            }
            else
            {
                m_HasChangedProxyUseDefCred = true;
            }
            configureSaveButtonProxy();

            if (chkbxProxyUseDefCred.Checked)
            {
                txProxyUser.Enabled = false;
                txProxyPW.Enabled = false;
            }
            else
            {
                txProxyUser.Enabled = true;
                txProxyPW.Enabled = true;
            }
        }

        private void bProxyCancel_Click(object sender, EventArgs e)
        {
            string ErrorText;
            LoadDetailsProxy((XmlNode)grpboxDetailProxy.Tag, out ErrorText);
            if (ErrorText != null)
            {
                frmError.ShowError(ErrorText);
            }
        }

        private void txProxyServer_TextChanged(object sender, EventArgs e)
        {
            if (m_InitProxyServer.Trim().Equals(txProxyServer.Text.Trim()))
            {
                m_HasChangedProxyServer = false;
            }
            else
            {
                m_HasChangedProxyServer = true;
            }
            configureSaveButtonProxy();
        }

        private void txProxyUser_TextChanged(object sender, EventArgs e)
        {
            if (m_InitProxyUser.Trim().Equals(txProxyUser.Text.Trim()))
            {
                m_HasChangedProxyUser = false;
            }
            else
            {
                m_HasChangedProxyUser = true;
            }
            configureSaveButtonProxy();
        }

        private void txProxyPW_TextChanged(object sender, EventArgs e)
        {
            if (m_InitProxyPW.Trim().Equals(txProxyPW.Text.Trim()))
            {
                m_HasChangedProxyPW = false;
            }
            else
            {
                m_HasChangedProxyPW = true;
            }
            configureSaveButtonProxy();
        }

        private void txProxyPort_TextChanged(object sender, EventArgs e)
        {
            if (m_InitProxyPort.Trim().Equals(txProxyPort.Text.Trim()))
            {
                m_HasChangedProxyPort = false;
            }
            else
            {
                m_HasChangedProxyPort = true;
            }
            configureSaveButtonProxy();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            TopMost = true;
        }
    }
}
