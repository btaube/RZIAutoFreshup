using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using UpdateModul.shared;

namespace UpdateModul
{
    public partial class frmMain : Form
    {


        private static Dictionary<string, string> cbPrerequisiteItems;
        private static Dictionary<string, string> cbSearchItems;
        private BindingSource bsPrerequisites = new BindingSource();
        private BindingSource bsSearch = new BindingSource();
        private static int iSearchCnt = 0;


        private ListViewColumnSorter lvwColumnSorter;


        // physical details
        private string sInitInt = "";
        private string sInitExt = "";
        private string sInitMin = "";
        private string sInitRL = "";
        private string sInitDL = "";
        private string sInitGuid = "";
        private List<string> lsInitPR = new List<string>();

        private bool bHasChangedInitInt = false;
        private bool bHasChangedInitExt = false;
        private bool bHasChangedInitMin = false;
        private bool bHasChangedInitRL = false;
        private bool bHasChangedInitDL = false;
        private bool bHasChangedInitGuid = false;
        private bool bHasChangedInitPR = false;

        // product details
        private string sInitIcon = "";
        private string sInitSupportEmail = "";

        private bool bHasChangedInitIcon = false;
        private bool bHasChangedInitSupportEmail = false;
        public frmMain()
        {
            InitializeComponent();
        }

        private void LoadPrerequisites(XmlNode aNode)
        {
            if (cbPrerequisiteItems != null)
            {
                if (cbPrerequisiteItems.Count > 0)
                {
                    (CBprerequisites.DataSource as BindingSource).Clear();
                }
            }

            ///////////////////////////////////
            cbPrerequisiteItems = new Dictionary<string, string>();
            cbPrerequisiteItems.Add("-1", translations.frmMain_Detail_MinVersion_Default);
            XmlNode aPathNode;
            string sPath = "";
            foreach (XmlNode itNode in aNode.ParentNode.ChildNodes)
            {
                aPathNode = itNode;
                while ((itNode.ParentNode != null) && (aPathNode.Name != "Versions"))
                {
                    aPathNode = aPathNode.ParentNode;

                    //sPath = CVersionLookupHelper.GetFullPathByTagName(aPathNode.Name);
                    if (aPathNode.Attributes["nodetype"] != null)
                    {
                        if ("physical".Equals(aPathNode.Attributes["nodetype"].Value))
                        {
                            sPath = aPathNode.Attributes["name"].InnerText;
                            if (!cbPrerequisiteItems.Keys.Contains(aPathNode.Name))
                            {
                                if (sPath.StartsWith(" » "))
                                {
                                    sPath = sPath.Substring(3);
                                }
                                if (!cbPrerequisiteItems.Keys.Contains(aPathNode.Name))
                                {
                                    cbPrerequisiteItems.Add(aPathNode.Name, sPath);
                                }

                            }
                        }
                    }
                }
            }

            foreach (ListViewItem itLV in LVdetailParents.Items)
            {
                if (itLV.Checked)
                {
                    XmlNode aParNode = (XmlNode)itLV.Tag;
                    if (aParNode != null)
                    {

                        foreach (XmlNode itChild in aParNode.ChildNodes)
                        {
                            aPathNode = itChild;
                            while ((aParNode.ParentNode != null) && (aPathNode.Name != "Versions"))
                            {
                                aPathNode = aPathNode.ParentNode;
                                sPath = CVersionLookupHelper.GetFullPathByTagName(aPathNode.Name, true);
                                if (aPathNode.Attributes["nodetype"] != null)
                                {
                                    if ("physical".Equals(aPathNode.Attributes["nodetype"].Value))
                                    {
                                        if (!cbPrerequisiteItems.Keys.Contains(aPathNode.Name))
                                        {
                                            if (sPath.StartsWith(Environment.NewLine + " » "))
                                            {
                                                sPath = sPath.Substring(4);
                                            }
                                            if (!cbPrerequisiteItems.Keys.Contains(aPathNode.Name))
                                            {
                                                cbPrerequisiteItems.Add(aPathNode.Name, sPath);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }

            if (cbPrerequisiteItems.Count > 0)
            {
                bsPrerequisites.DataSource = cbPrerequisiteItems;
                CBprerequisites.DataSource = bsPrerequisites;
                CBprerequisites.DisplayMember = "Text";
                CBprerequisites.ValueMember = "Value";
            }

        }

        private void BdetailSave_Click(object sender, EventArgs e)
        {
            XmlNode aNode = CVersionLookupHelper.GetNodeByTagName(sInitGuid);
            if (aNode == null)
            {
                return;
            }
            TVmain.SelectedNode.Text = TXvnameInt.Text.Trim();
            aNode.Attributes["name"].Value = TXvnameInt.Text.Trim();
            foreach (XmlNode itNode in aNode.ChildNodes)
            {

                if ("DisplayName".Equals(itNode.Name))
                {
                    itNode.InnerText = TXvnameExt.Text.Trim();
                }
                if ("IconBase64".Equals(itNode.Name))
                {
                    itNode.InnerText = RZITools.GetBase64stringFromImage(imgProductIcon.Image);
                }
                if ("SupportEmail".Equals(itNode.Name))
                {
                    itNode.InnerText = txSupportEmail.Text.Trim();
                }
                if ("ParentList".Equals(itNode.Name))
                {
                    itNode.RemoveAll();
                    foreach (ListViewItem itLV in LVdetailParents.Items)
                    {
                        if (itLV.Checked)
                        {
                            XmlNode aParNode = (XmlNode)itLV.Tag;
                            if (aParNode != null)
                            {
                                XmlElement aNewPhysicalTag = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "PhysicalTag", string.Empty);
                                aNewPhysicalTag.InnerText = aParNode.Name;
                                itNode.AppendChild(aNewPhysicalTag);
                            }
                        }

                    }
                }
                if ("MinimumVersion".Equals(itNode.Name))
                {
                    if (CBprerequisites.SelectedIndex <= 0)
                    {
                        itNode.InnerText = "";
                    }
                    else
                    {
                        itNode.InnerText = ((KeyValuePair<string, string>)CBprerequisites.SelectedItem).Key;
                    }
                }
                if ("InstFilesURL".Equals(itNode.Name))
                {
                    itNode.InnerText = TXurlInstFile.Text.Trim();
                }
                if ("ReleaseNotesURL".Equals(itNode.Name))
                {
                    itNode.InnerText = TXurlRN.Text.Trim();
                }

            }
            if (sInitGuid.Trim() != txDisplayGuid.Text.Trim())
            {
                XmlNode renamedNode = renameXMLNode(CVersionLookupHelper.m_xmlDoc, aNode, txDisplayGuid.Text.Trim());
                aNode = CVersionLookupHelper.GetNodeByTagName(txDisplayGuid.Text.Trim());
                SaveFile(CGlobVars.currentlyLoadedFile);
                LoadMainFrm(CGlobVars.currentlyLoadedFile);
                //LoadDetails(renamedNode);
            }
            else
            {
                SaveFile(CGlobVars.currentlyLoadedFile);
                LoadDetails(aNode);
            }



        }

        private XmlNode renameXMLNode(XmlDocument doc, XmlNode oldRoot, string newname)
        {
            //XmlNode aNewXmlNode = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, sGuid, string.Empty);
            XmlNode newRoot = doc.CreateElement(string.Empty, newname, string.Empty);
            XmlAttribute attrNewName = CVersionLookupHelper.m_xmlDoc.CreateAttribute("name");
            attrNewName.Value = oldRoot.Attributes["name"].InnerText;
            newRoot.Attributes.Append(attrNewName);

            XmlAttribute attrNewClient = CVersionLookupHelper.m_xmlDoc.CreateAttribute("nodetype");
            attrNewClient.Value = oldRoot.Attributes["nodetype"].InnerText;
            newRoot.Attributes.Append(attrNewClient);

            foreach (XmlNode childNode in oldRoot.ChildNodes)
            {
                newRoot.AppendChild(childNode.CloneNode(true));
            }
            XmlNode parent = oldRoot.ParentNode;
            parent.AppendChild(newRoot);
            parent.RemoveChild(oldRoot);

            return newRoot;
        }

        private void LoadTreeview()
        {
            lSearchCount.Visible = false;
            TVmain.BeginUpdate();
            try
            {
                TVmain.Nodes.Clear();
                XmlNode aVersioNode = CVersionLookupHelper.GetNodeByTagName("Versions");
                if (aVersioNode != null)
                {
                    TVmain.Nodes.Add(new TreeNode(CVersionLookupHelper.m_xmlDoc.DocumentElement.Name));
                    TreeNode tNode = new TreeNode();
                    tNode = TVmain.Nodes[0];
                    tNode.NodeFont = new Font(TVmain.Font, FontStyle.Bold);
                    tNode.Text = translations.frmMain_tvMain_Versions;
                    tNode.ImageIndex = 4;
                    tNode.SelectedImageIndex = 4;
                    tNode.Tag = aVersioNode;

                    AddNode(aVersioNode, tNode);
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

            TreeNode aNode = TVmain.Nodes[0];
            TVmain.ExpandAll();
            foreach (TreeNode itClientNode in aNode.Nodes)
            {
                foreach (TreeNode itProductNode in itClientNode.Nodes)
                {
                    itProductNode.Collapse();
                }
            }
            TVmain.Sort();
        }


        private void LoadDetails(XmlNode aNode)
        {
            if (aNode == null)
            {
                return;
            }

            bool foundParentBase = false;
            XmlNodeList checkList = CVersionLookupHelper.m_xmlDoc.GetElementsByTagName("ParentList");
            foreach (XmlNode checkNode in checkList)
            {
                if (checkNode.InnerText.Equals(aNode.Name))
                {
                    foundParentBase = true;
                }
            }

            if ((aNode.ChildNodes.Count > 7) || (foundParentBase))
            {
                txDisplayGuid.ContextMenuStrip = null;
            }
            else
            {
                txDisplayGuid.ContextMenuStrip = ctxGUID;
            }

            if (foundParentBase)
            {
                lblParentVersion.Visible = true;
            }
            else
            {
                lblParentVersion.Visible = false;
            }

            //    TreeNode toBeSelected = allNodes.OfType<TreeNode>().FirstOrDefault<TreeNode>(node => (XmlNode)node.Tag == aNode);
            //    if (toBeSelected != null)
            //    {
            //        toBeSelected.EnsureVisible();
            //        TVmain.SelectedNode = toBeSelected;
            //    }

            if ((aNode.ParentNode.Attributes != null) && (aNode.ParentNode.Attributes["nodetype"] != null) && ((aNode.ParentNode.Attributes["nodetype"].Value == "logical") || (aNode.ParentNode.Attributes["nodetype"].Value == "product")))
            {
                //lbDetailParents.Visible = false;
                LVdetailParents.Enabled = true;
            }
            else
            {
                //lbDetailParents.Visible = true;
                LVdetailParents.Enabled = false;
            }

            string sCaptionPath = CVersionLookupHelper.GetFullPathByTagName(aNode.Name, true);
            if (sCaptionPath.StartsWith(Environment.NewLine + " » "))
            {
                sCaptionPath = sCaptionPath.Substring(4);
            }
            //toolTip1 = new ToolTip();
            //toolTip1.RemoveAll();
            toolTip1.SetToolTip(grpbxDetailHint, sCaptionPath);
            toolTip1.SetToolTip(GRPBXmain, sCaptionPath);

            if (aNode.Attributes["name"] != null)
            {
                grpbxDetailHint.Text = aNode.Attributes["name"].Value;
                GRPBXmain.Text = aNode.Attributes["name"].Value;
            }
            else
            {
                GRPBXmain.Text = string.Empty;
            }
            //GRPBXdetail.Text = sCaptionPath;
            //GRPBXmain.Text = sCaptionPath;
            grpbxDetailHint.Visible = true;

            imgProductIcon.Image = null;
            txSupportEmail.Text = "";
            TXvnameExt.Text = "";
            TXurlInstFile.Text = "";
            TXurlRN.Text = "";
            txDisplayGuid.ReadOnly = true;

            LVdetailParents.BeginUpdate();
            LVdetailParents.Items.Clear();
            sInitGuid = aNode.Name;
            txDisplayGuid.Text = aNode.Name;

            txSupportEmail.Width = TXvnameInt.Width;

            TXvnameInt.Text = aNode.Attributes["name"].Value;
            CVersionLookupHelper.ClearCollectedLeafs();
            List<XmlNode> aList = CVersionLookupHelper.GetDownloadLeafs();

            foreach (XmlNode itNode in aNode.ChildNodes)
            {
                if ("IconBase64".Equals(itNode.Name))
                {
                    imgProductIcon.Image = RZITools.GetImageFromBase64string(itNode.InnerText.Trim());
                }
                if ("SupportEmail".Equals(itNode.Name))
                {
                    txSupportEmail.Text = itNode.InnerText.Trim();
                }
                //
                if ("DisplayName".Equals(itNode.Name))
                {
                    TXvnameExt.Text = itNode.InnerText.Trim();
                }
                if ("ParentList".Equals(itNode.Name))
                {
                    if (aList != null)
                    {
                        foreach (XmlNode itDownloadNode in aList)
                        {
                            string sPath = "";

                            XmlNode aPathNode = itDownloadNode;
                            sPath = CVersionLookupHelper.GetFullPathByTagName(aPathNode.Name, true);
                            if (sPath.StartsWith(Environment.NewLine + " » "))
                            {
                                sPath = sPath.Substring(4);
                            }

                            ListViewItem aItem = new ListViewItem(sPath);
                            aItem.Tag = itDownloadNode;

                            XmlNode aCheckNode = (XmlNode)TVmain.SelectedNode.Tag;
                            if (aCheckNode != null)
                            {
                                if (!CVersionLookupHelper.CheckIfNodeIsChildOfAnotherNode(itDownloadNode, aCheckNode.Name))
                                {
                                    if (!itDownloadNode.Name.Equals(aCheckNode.Name))
                                    {
                                        LVdetailParents.Items.Add(aItem);
                                    }
                                }


                            }

                        }
                    }
                    foreach (XmlNode itParents in itNode.ChildNodes)
                    {
                        foreach (ListViewItem itLV in LVdetailParents.Items)
                        {
                            XmlNode lvNode = (XmlNode)itLV.Tag;
                            if (lvNode != null)
                            {
                                if (lvNode.Name.Equals(itParents.InnerText.Trim()))
                                {
                                    itLV.Checked = true;
                                }
                            }
                        }
                    }
                    LoadPrerequisites(aNode);

                }

                if ("MinimumVersion".Equals(itNode.Name))
                {
                    string sTag;
                    if ("".Equals(itNode.InnerText.Trim()))
                    {
                        if (cbPrerequisiteItems.TryGetValue("-1", out sTag))
                        {
                            CBprerequisites.SelectedIndex = CBprerequisites.FindStringExact(sTag);
                        }
                    }
                    else
                    {
                        if (cbPrerequisiteItems.TryGetValue(itNode.InnerText.Trim(), out sTag))
                        {
                            CBprerequisites.SelectedIndex = CBprerequisites.FindStringExact(sTag);
                        }
                    }


                }
                if ("InstFilesURL".Equals(itNode.Name))
                {
                    TXurlInstFile.Text = itNode.InnerText.Trim();
                }
                if ("ReleaseNotesURL".Equals(itNode.Name))
                {
                    TXurlRN.Text = itNode.InnerText.Trim();
                }

            }

            sInitIcon = RZITools.GetBase64stringFromImage(imgProductIcon.Image);
            sInitSupportEmail = txSupportEmail.Text;
            sInitInt = TXvnameInt.Text;
            sInitExt = TXvnameExt.Text;
            sInitMin = CBprerequisites.Text;
            sInitRL = TXurlRN.Text;
            sInitDL = TXurlInstFile.Text;
            //sInitGuid = txDisplayGuid.Text;

            string sChecked = "";
            lsInitPR.Clear();
            foreach (ListViewItem itItem in LVdetailParents.Items)
            {
                if (itItem.Checked)
                {
                    sChecked = "1";
                }
                else
                {
                    sChecked = "0";
                }
                XmlNode aCheckedNode = (XmlNode)itItem.Tag;
                if (aCheckedNode != null)
                {
                    lsInitPR.Add(sChecked + aCheckedNode.Name);
                }
            }
            lsInitPR.Sort();

            bHasChangedInitIcon = false;
            bHasChangedInitSupportEmail = false;
            bHasChangedInitInt = false;
            bHasChangedInitExt = false;
            bHasChangedInitMin = false;
            bHasChangedInitRL = false;
            bHasChangedInitDL = false;
            bHasChangedInitGuid = false;
            bHasChangedInitPR = false;

            LVdetailParents.EndUpdate();

            configureDetailsSaveButton();

        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CGlobVars.currentlyLoadedFile))
            {
                CGlobVars.initDir = SetInitDir("");
                CGlobVars.wrkDir = SetWorkDir(CGlobVars.initDir);
            }
            LoadMainFrm(CGlobVars.currentlyLoadedFile);
        }

        public static string GetCleanFileName(string fileName)
        {
            //CLog.Debug("Entered function 'GetCleanFileName'");
            //CLog.Debug("Provided 'FileName': {0}", fileName);
            var invalidChars = Path.GetInvalidFileNameChars();

            string cleanFileName = new string(fileName
            .Where(x => !invalidChars.Contains(x))
            .ToArray());

            return cleanFileName;
        }

        private string SetInitDir(string InitDir)
        {
            //CLog.Debug("Entered function 'SetInitDir'");
            //CLog.Debug("Provided 'InitDir': {0}", InitDir);
            if ((InitDir == null) || (InitDir.Trim() == ""))
            {
                InitDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                //CLog.Debug("Trying to set init dir in AppData directory: {0}", InitDir);
                if (Directory.Exists(InitDir))
                {
                    //CLog.Debug("Directory already exists.");
                    string folderName = "";
                    if (Application.ProductName != null)
                    {
                        if (CGlobVars.Client == "CARD1")
                        {
                            folderName = "IBuT" + System.IO.Path.DirectorySeparatorChar + GetCleanFileName(Application.ProductName);
                        }
                        else
                        {
                            folderName = "RZI" + System.IO.Path.DirectorySeparatorChar + GetCleanFileName(Application.ProductName);
                        }
                        //CLog.Debug("Trying to set application folder name: {0}", folderName);
                    }
                    else
                    {
                        if (CGlobVars.Client == "CARD1")
                        {
                            folderName = "IBuT" + System.IO.Path.DirectorySeparatorChar + "VersionLookupKonfigurator";
                        }
                        else
                        {
                            folderName = "RZI" + System.IO.Path.DirectorySeparatorChar + "VersionLookupKonfigurator";
                        }
                        //CLog.Debug("Using default application folder name: {0}", folderName);
                    }
                    InitDir = InitDir + System.IO.Path.DirectorySeparatorChar + folderName + System.IO.Path.DirectorySeparatorChar;
                    if (!Directory.Exists(InitDir))
                    {
                        //CLog.Debug("Directory does not exist yet: {0}", InitDir);
                        try
                        {
                            Directory.CreateDirectory(InitDir);
                            //CLog.Debug("Created new init directory successfully.");
                        }
                        catch
                        {
                            InitDir = Path.GetDirectoryName(Application.ExecutablePath);
                            //CLog.Warning("Error creating new init directory, using default directory: {0}", InitDir);
                        }
                    }
                }
                else
                {
                    InitDir = Path.GetDirectoryName(Application.ExecutablePath);
                    //CLog.Warning("AppData directory cannot be found, using default directory: {0}", InitDir);
                }
            }
            return InitDir;
        }

        private string SetWorkDir(string InitDir)
        {
            //CLog.Debug("Entered function 'SetInitDir'");
            //CLog.Debug("Provided 'InitDir': {0}", InitDir);
            if ((InitDir == null) || (InitDir.Trim() == ""))
            {
                InitDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                //CLog.Debug("Trying to set init dir in AppData directory: {0}", InitDir);
                if (Directory.Exists(InitDir))
                {
                    //CLog.Debug("Directory already exists.");
                    string folderName = "";
                    if (Application.ProductName != null)
                    {
                        if (CGlobVars.Client == "CARD1")
                        {
                            folderName = "IBuT" + System.IO.Path.DirectorySeparatorChar + GetCleanFileName(Application.ProductName);
                        }
                        else
                        {
                            folderName = "RZI" + System.IO.Path.DirectorySeparatorChar + GetCleanFileName(Application.ProductName);
                        }
                        //CLog.Debug("Trying to set application folder name: {0}", folderName);
                    }
                    else
                    {
                        if (CGlobVars.Client == "CARD1")
                        {
                            folderName = "IBuT" + System.IO.Path.DirectorySeparatorChar + "VersionLookupKonfigurator";
                        }
                        else
                        {
                            folderName = "RZI" + System.IO.Path.DirectorySeparatorChar + "VersionLookupKonfigurator";
                        }
                        //CLog.Debug("Using default application folder name: {0}", folderName);
                    }
                    InitDir = InitDir + folderName + System.IO.Path.DirectorySeparatorChar;
                    if (!Directory.Exists(InitDir))
                    {
                        //CLog.Debug("Directory does not exist yet: {0}", InitDir);
                        try
                        {
                            Directory.CreateDirectory(InitDir);
                            //CLog.Debug("Created new init directory successfully.");
                        }
                        catch
                        {
                            InitDir = Path.GetDirectoryName(Application.ExecutablePath);
                            //CLog.Warning("Error creating new init directory, using default directory: {0}", InitDir);
                        }
                    }
                }
                else
                {
                    InitDir = Path.GetDirectoryName(Application.ExecutablePath);
                    //CLog.Warning("AppData directory cannot be found, using default directory: {0}", InitDir);
                }
            }



            return InitDir + "wrk" + Path.DirectorySeparatorChar;
        }

        private int LoadMainFrm(string InitFile)
        {
            columnHeader1.Text = translations.frmMain_Detail_Parents_ColumnHeader;
            imgSearchCancel.Image = imgListTV.Images[6];

            TVmain.Enabled = true;
            cbSearch.Enabled = true;
            BdetailSave.Enabled = false;
            BdetailCancel.Enabled = false;




            // load xml file
            if (!String.IsNullOrWhiteSpace(InitFile))
            {
                if (!File.Exists(InitFile))
                {
                    MessageBox.Show("Die Datei konnte nicht geladen werden:" + Environment.NewLine +
                                    InitFile + Environment.NewLine + Environment.NewLine +
                                    "Es wird nun auf das Standard-Verzeichnis zurückgegriffen.", "Pfad ungültig", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    string ErrorText = string.Empty;
                    CGlobVars.initDir = SetInitDir("");
                    CGlobVars.wrkDir = SetWorkDir(CGlobVars.initDir);
                    CVersionLookupHelper.LoadVersionLookupXML(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML, true);
                    InitFile = CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML;
                }
                if (InitFile.EndsWith("xmlc"))
                {
                    string ErrorText = string.Empty;
                    CVersionLookupHelper.DecryptAndLoadVersionLookupXML(InitFile, true, out ErrorText);
                }
                else
                {
                    if (!CVersionLookupHelper.LoadVersionLookupXML(InitFile, true))
                    {
                        return CReturnCodes.COULD_NOT_LOAD_VERSIONLOOKUP_XML;
                    }
                }
                CGlobVars.currentlyLoadedFile = InitFile;
            }
            else
            {
                // Create wrk directory if not exists
                if (!Directory.Exists(CGlobVars.wrkDir))
                {
                    try
                    {
                        Directory.CreateDirectory(CGlobVars.wrkDir);
                    }
                    catch (Exception)
                    {
                        return CReturnCodes.CANNOT_CREATE_WORK_DIRECTORY;
                    }
                }

                string ErrorText = string.Empty;
                CVersionLookupHelper.LoadVersionLookupXML(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML, true);
                CGlobVars.currentlyLoadedFile = CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML;
            }


            // add file path to title
            String sTitle = this.Text;
            if (sTitle.IndexOf(" (") > 0)
            {
                sTitle = sTitle.Substring(0, sTitle.IndexOf(" ("));
            }
            sTitle = sTitle + " (" + CGlobVars.currentlyLoadedFile + ")";
            this.Text = sTitle;

            //
            grpbxDetailHint.Visible = false;

            // load treeview
            LoadTreeview();

            // load legend image indexes
            LoadLegend();

            //apply ListView Column Sorter
            lvwColumnSorter = new ListViewColumnSorter();
            LVdetailParents.ListViewItemSorter = lvwColumnSorter;

            return CReturnCodes.OK;
        }




        private void LoadLegend()
        {
            picLegendClient.Image = imgListTV.Images[0];
            picLegendProduct.Image = imgListTV.Images[1];
            picLegendLogical.Image = imgListTV.Images[2];
            picLegendPhysical.Image = imgListTV.Images[3];
        }

        private void CollectDictionaryEntries(XmlNode inXmlNode)
        {
            XmlNode xNode;
            XmlNodeList nodeList;
            int i;

            // Loop through the XML nodes until the leaf is reached.
            // Add the nodes to the TreeView during the looping process.
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    if ((xNode.Attributes != null) && (xNode.Attributes["name"] != null))
                    {
                        if (!cbSearchItems.Keys.Contains(xNode.Name + "_" + xNode.Attributes["name"].Value))
                        {
                            cbSearchItems.Add(xNode.Name + "_" + xNode.Attributes["name"].Value, xNode.Attributes["name"].Value);
                        }
                        CollectDictionaryEntries(xNode);
                    }
                    else if (xNode.ParentNode.Attributes["nodetype"] != null)
                    {
                        if (!cbSearchItems.Keys.Contains(xNode.ParentNode.Name + "_" + xNode.Name))
                        {
                            cbSearchItems.Add(xNode.ParentNode.Name + "_" + xNode.Name, xNode.InnerText);
                        }
                        CollectDictionaryEntries(xNode);
                    }
                    else
                    {
                        CollectDictionaryEntries(xNode);
                    }
                }
            }
        }

        private void ExpandTreeviewElements(TreeNode aInNode, string sSearchName)
        {

            foreach (TreeNode aChild in aInNode.Nodes)
            {
                if ((sSearchName.Trim().StartsWith("\"")) && sSearchName.Trim().EndsWith("\""))
                {
                    if (aChild.Text.Trim().Equals(sSearchName.Trim().Substring(1, sSearchName.Trim().Length - 2)))
                    {
                        XmlNode aCheckNode = (XmlNode)aChild.Tag;
                        if (aCheckNode != null)
                        {
                            if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("client".Equals(aCheckNode.Attributes["nodetype"].Value)))
                            {
                                aChild.ImageIndex = 8;
                                aChild.SelectedImageIndex = 8;
                            }
                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("product".Equals(aCheckNode.Attributes["nodetype"].Value)))
                            {
                                aChild.ImageIndex = 9;
                                aChild.SelectedImageIndex = 9;
                            }
                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("logical".Equals(aCheckNode.Attributes["nodetype"].Value)))
                            {
                                aChild.ImageIndex = 10;
                                aChild.SelectedImageIndex = 10;
                            }
                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("physical".Equals(aCheckNode.Attributes["nodetype"].Value)))
                            {
                                aChild.ImageIndex = 11;
                                aChild.SelectedImageIndex = 11;
                            }
                            iSearchCnt++;
                            //aChild.Expand();
                            TreeNode aProgress;
                            aProgress = aChild.Parent;
                            while (aProgress != null)
                            {
                                aProgress.Expand();
                                aProgress = aProgress.Parent;
                            }
                        }

                    }
                    else
                    {
                        XmlNode aNode = (XmlNode)aChild.Tag;
                        if (aNode != null)
                        {
                            foreach (XmlNode aChildNode in aNode.ChildNodes)
                            {
                                if (aChildNode.Attributes.Count == 0)
                                {
                                    if (aChildNode.InnerText.Trim().Equals(sSearchName.Trim().Substring(1, sSearchName.Trim().Length - 2)))
                                    {
                                        XmlNode aCheckNode = (XmlNode)aChild.Tag;
                                        if (aCheckNode != null)
                                        {
                                            if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("client".Equals(aCheckNode.Attributes["nodetype"].Value)))
                                            {
                                                aChild.ImageIndex = 8;
                                                aChild.SelectedImageIndex = 8;
                                            }
                                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("product".Equals(aCheckNode.Attributes["nodetype"].Value)))
                                            {
                                                aChild.ImageIndex = 9;
                                                aChild.SelectedImageIndex = 9;
                                            }
                                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("logical".Equals(aCheckNode.Attributes["nodetype"].Value)))
                                            {
                                                aChild.ImageIndex = 10;
                                                aChild.SelectedImageIndex = 10;
                                            }
                                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("physical".Equals(aCheckNode.Attributes["nodetype"].Value)))
                                            {
                                                aChild.ImageIndex = 11;
                                                aChild.SelectedImageIndex = 11;
                                            }
                                        }
                                        iSearchCnt++;
                                        //aChild.Expand();
                                        TreeNode aProgress;
                                        aProgress = aChild.Parent;
                                        while (aProgress != null)
                                        {
                                            aProgress.Expand();
                                            aProgress = aProgress.Parent;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {

                    if (aChild.Text.Trim().ToLower().Contains(sSearchName.Trim().ToLower()))
                    {
                        XmlNode aCheckNode = (XmlNode)aChild.Tag;
                        if (aCheckNode != null)
                        {
                            if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("client".Equals(aCheckNode.Attributes["nodetype"].Value)))
                            {
                                aChild.ImageIndex = 8;
                                aChild.SelectedImageIndex = 8;
                            }
                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("product".Equals(aCheckNode.Attributes["nodetype"].Value)))
                            {
                                aChild.ImageIndex = 9;
                                aChild.SelectedImageIndex = 9;
                            }
                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("logical".Equals(aCheckNode.Attributes["nodetype"].Value)))
                            {
                                aChild.ImageIndex = 10;
                                aChild.SelectedImageIndex = 10;
                            }
                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("physical".Equals(aCheckNode.Attributes["nodetype"].Value)))
                            {
                                aChild.ImageIndex = 11;
                                aChild.SelectedImageIndex = 11;
                            }
                        }
                        iSearchCnt++;
                        //aChild.Expand();
                        TreeNode aProgress;
                        aProgress = aChild.Parent;
                        while (aProgress != null)
                        {
                            aProgress.Expand();
                            aProgress = aProgress.Parent;
                        }
                    }
                    else
                    {
                        XmlNode aNode = (XmlNode)aChild.Tag;
                        if (aNode != null)
                        {
                            foreach (XmlNode aChildNode in aNode.ChildNodes)
                            {
                                if (aChildNode.Attributes.Count == 0)
                                {
                                    if (aChildNode.InnerText.Trim().ToLower().Contains(sSearchName.Trim().ToLower()))
                                    {
                                        XmlNode aCheckNode = (XmlNode)aChild.Tag;
                                        if (aCheckNode != null)
                                        {
                                            if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("client".Equals(aCheckNode.Attributes["nodetype"].Value)))
                                            {
                                                aChild.ImageIndex = 8;
                                                aChild.SelectedImageIndex = 8;
                                            }
                                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("product".Equals(aCheckNode.Attributes["nodetype"].Value)))
                                            {
                                                aChild.ImageIndex = 9;
                                                aChild.SelectedImageIndex = 9;
                                            }
                                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("logical".Equals(aCheckNode.Attributes["nodetype"].Value)))
                                            {
                                                aChild.ImageIndex = 10;
                                                aChild.SelectedImageIndex = 10;
                                            }
                                            else if ((aCheckNode.Attributes != null) && (aCheckNode.Attributes["nodetype"] != null) && ("physical".Equals(aCheckNode.Attributes["nodetype"].Value)))
                                            {
                                                aChild.ImageIndex = 11;
                                                aChild.SelectedImageIndex = 11;
                                            }
                                        }
                                        iSearchCnt++;
                                        //aChild.Expand();
                                        TreeNode aProgress;
                                        aProgress = aChild.Parent;
                                        while (aProgress != null)
                                        {
                                            aProgress.Expand();
                                            aProgress = aProgress.Parent;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }

                ExpandTreeviewElements(aChild, sSearchName);

            }

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
                        TreeNode aNewNode = new TreeNode(xNode.Attributes["name"].Value);
                        if (xNode.Attributes["nodetype"] != null)
                        {
                            aNewNode.Tag = xNode;
                            if (xNode.Attributes["nodetype"].Value.ToLower().Equals("client"))
                            {
                                aNewNode.SelectedImageIndex = 0;
                                aNewNode.ImageIndex = 0;
                            }
                            else if (xNode.Attributes["nodetype"].Value.ToLower().Equals("product"))
                            {
                                aNewNode.SelectedImageIndex = 1;
                                aNewNode.ImageIndex = 1;
                            }
                            else if (xNode.Attributes["nodetype"].Value.ToLower().Equals("logical"))
                            {
                                aNewNode.SelectedImageIndex = 2;
                                aNewNode.ImageIndex = 2;
                            }
                            else if (xNode.Attributes["nodetype"].Value.ToLower().Equals("physical"))
                            {
                                aNewNode.SelectedImageIndex = 3;
                                aNewNode.ImageIndex = 3;
                            }

                        }
                        inTreeNode.Nodes.Add(aNewNode);
                        AddNode(xNode, aNewNode);
                    }
                }
            }
            else
            {
                if (inXmlNode.Attributes["name"] != null)
                {
                    inTreeNode.Text = inXmlNode.Attributes["name"].Value;
                    inTreeNode.Tag = inXmlNode;
                }

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsTVcreateLogical_Click(object sender, EventArgs e)
        {
            contextTV.Tag = "createLogical";
            XmlNode aReferenceNode = (XmlNode)TVmain.SelectedNode.Tag;
            if (aReferenceNode == null)
            {
                MessageBox.Show(translations.frmMain_Dlg_TagNotFound);
                return;
            }
            XmlNode aNode = CVersionLookupHelper.GetNodeByTagName(aReferenceNode.Name);
            if (aNode != null)
            {
                TreeNode aNewNode = new TreeNode(translations.frmMain_tvMain_CreateEntry);
                TVmain.SelectedNode.Nodes.Add(aNewNode);
                aNewNode.ImageIndex = 2;
                aNewNode.SelectedImageIndex = 2;
                TVmain.SelectedNode = aNewNode;

                string sGuid = RZITools.GenerateGUID();
                XmlNode aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                while (aGUIDNode != null)
                {
                    sGuid = RZITools.GenerateGUID();
                    aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                }

                string sContextMode = (String)contextTV.Tag;
                XmlNode aNewXmlNode = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, sGuid, string.Empty);

                XmlAttribute attrNewName = CVersionLookupHelper.m_xmlDoc.CreateAttribute("name");
                attrNewName.Value = TVmain.SelectedNode.Text;
                aNewXmlNode.Attributes.Append(attrNewName);

                XmlAttribute attrNewClient = CVersionLookupHelper.m_xmlDoc.CreateAttribute("nodetype");
                attrNewClient.Value = "logical";
                aNewXmlNode.Attributes.Append(attrNewClient);
                aNode.AppendChild(aNewXmlNode);

                TVmain.SelectedNode.Tag = aNewXmlNode;

                SaveFile(CGlobVars.currentlyLoadedFile);
                //LoadDetails(aNewXmlNode);

                TVmain.LabelEdit = true;
                if (!TVmain.SelectedNode.IsEditing)
                {
                    TVmain.SelectedNode.BeginEdit();
                }
            }
        }

        private void configureTVmainContextMenu(bool bShowClient, bool bShowProduct, bool bShowLogical, bool bShowPhysical, bool bShowEdit, bool bShowDelete, bool bShowCopyPhysical, bool bShowHideDownloads)
        {
            tsTVcreateClient.Image = imgListTV.Images[0];
            tsTVcreateProduct.Image = imgListTV.Images[1];
            tsTVcreateLogical.Image = imgListTV.Images[2];
            tsTVcreatePhysical.Image = imgListTV.Images[3];
            tsTVcopyPhysical.Image = imgListTV.Images[3];
            tsTVeditNode.Image = imgListTV.Images[5];
            tsTVdeleteNode.Image = imgListTV.Images[6];

            tsTVcreateClient.Enabled = bShowClient;
            tsTVcreateProduct.Enabled = bShowProduct;
            tsTVcreateLogical.Enabled = bShowLogical;
            tsTVcreatePhysical.Enabled = bShowPhysical;
            tsTVcopyPhysical.Enabled = bShowCopyPhysical;
            tsTVeditNode.Enabled = bShowEdit;
            tsTVdeleteNode.Enabled = bShowDelete;
            tsTVExpandBranch.Enabled = bShowClient || bShowProduct || bShowLogical || bShowPhysical || bShowEdit || bShowDelete;
            tsTVCollapseBranch.Enabled = bShowClient || bShowProduct || bShowLogical || bShowPhysical || bShowEdit || bShowDelete;
            tsTVhideParentDownloads.Enabled = bShowHideDownloads;
        }


        private void configureDetailsSaveButton()
        {
            if (bHasChangedInitIcon || bHasChangedInitSupportEmail || bHasChangedInitInt || bHasChangedInitExt || bHasChangedInitMin || bHasChangedInitRL || bHasChangedInitDL || bHasChangedInitGuid || bHasChangedInitPR)
            {
                if (TVmain.SelectedNode != null)
                {
                    XmlNode aNode = (XmlNode)TVmain.SelectedNode.Tag;
                    if (aNode != null)
                    {
                        if (aNode.Attributes["nodetype"] != null)
                        {
                            if ("physical".Equals(aNode.Attributes["nodetype"].Value))
                            {
                                TVmain.Enabled = false;
                                cbSearch.Enabled = false;
                            }
                        }
                    }
                }

                if (lblGuidError.Visible || lblGuidAlreadyInUse.Visible)
                {
                    BdetailSave.Enabled = false;
                }
                else
                {
                    BdetailSave.Enabled = true;
                }

                BdetailCancel.Enabled = true;
            }
            else
            {
                TVmain.Enabled = true;
                cbSearch.Enabled = true;
                BdetailSave.Enabled = false;
                BdetailCancel.Enabled = false;
            }

        }

        private void TVmain_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            XmlNode aNode = (XmlNode)e.Node.Tag;
            if (aNode == null)
            {
                return;
            }
            if ("Versions".Equals(aNode.Name))  // Versions
            {
                configureTVmainContextMenu(true, false, false, false, false, false, false, false);
            }
            if ((aNode.Attributes != null) && (aNode.Attributes["nodetype"] != null) && ("client".Equals(aNode.Attributes["nodetype"].Value)))    // Clients
            {
                configureTVmainContextMenu(false, true, false, false, true, true, false, false);
            }
            if ((aNode.Attributes != null) && (aNode.Attributes["nodetype"] != null) && (("product".Equals(aNode.Attributes["nodetype"].Value)) || ("logical".Equals(aNode.Attributes["nodetype"].Value))))                           // Logical
            {
                if (e.Node.Nodes.Count > 0)
                {
                    bool bFoundPhysical = false;
                    foreach (TreeNode itNode in e.Node.Nodes)
                    {
                        XmlNode aSubNode = (XmlNode)itNode.Tag;
                        if (aSubNode != null)
                        {
                            if ((aSubNode.Attributes != null) && (aSubNode.Attributes["nodetype"] != null) && ("logical".Equals(aSubNode.Attributes["nodetype"].Value)))
                            {
                                break;
                            }
                            if ((aSubNode.Attributes != null) && (aSubNode.Attributes["nodetype"] != null) && ("physical".Equals(aSubNode.Attributes["nodetype"].Value)))
                            {
                                break;
                            }
                        }
                    }
                    if ("product".Equals(aNode.Attributes["nodetype"].Value))
                    {
                        if (bFoundPhysical)
                        {
                            configureTVmainContextMenu(false, false, false, false, true, true, false, false);
                        }
                        else
                        {
                            configureTVmainContextMenu(false, false, true, false, true, true, false, false);
                        }
                    }
                    else
                    {
                        if (bFoundPhysical)
                        {
                            configureTVmainContextMenu(false, false, false, false, true, true, false, false);
                        }
                        else
                        {
                            configureTVmainContextMenu(false, false, true, false, true, true, false, false);
                        }
                    }
                }
                else
                {
                    if ("product".Equals(aNode.Attributes["nodetype"].Value))
                    {
                        configureTVmainContextMenu(false, false, true, true, true, true, false, false);
                    }
                    else
                    {
                        configureTVmainContextMenu(false, false, true, true, true, true, false, false);
                    }
                }

            }
            if ((aNode.Attributes != null) && (aNode.Attributes["nodetype"] != null) && ("physical".Equals(aNode.Attributes["nodetype"].Value)))                            // Physical
            {
                XmlNode parent = aNode.ParentNode;
                if ((parent != null) && (parent.Attributes["nodetype"] != null) && ("physical".Equals(parent.Attributes["nodetype"].Value)))
                {
                    if (e.Node.Nodes.Count < 1)
                    {
                        configureTVmainContextMenu(false, false, false, true, false, true, true, true);
                    }
                    else
                    {
                        configureTVmainContextMenu(false, false, false, false, false, true, false, true);
                    }
                }
                else
                {
                    if (e.Node.Nodes.Count < 1)
                    {
                        configureTVmainContextMenu(false, false, false, true, false, true, true, false);
                    }
                    else
                    {
                        configureTVmainContextMenu(false, false, false, false, false, true, false, false);
                    }
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                TVmain.SelectedNode = e.Node;
            }
        }

        private void TVmain_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[] { '@' }) == -1)
                    {
                        // Stop editing without canceling the label change.
                        e.Node.EndEdit(false);

                        //
                        string sContextMode = (String)contextTV.Tag;
                        if (("createClient".Equals(sContextMode)) || ("createProduct".Equals(sContextMode)) || ("createLogical".Equals(sContextMode)) || ("createPhysical".Equals(sContextMode)))
                        {
                            TXvnameInt.Text = e.Label;

                            XmlNode aReferenceNode = (XmlNode)TVmain.SelectedNode.Tag;
                            if (aReferenceNode != null)
                            {
                                aReferenceNode.Attributes["name"].Value = e.Label;
                                SaveFile(CGlobVars.currentlyLoadedFile);
                                TVmain.LabelEdit = false;
                                if ("createPhysical".Equals(sContextMode))
                                {
                                    LoadDetails(aReferenceNode);
                                }

                            }

                            //
                        }
                        else if ("editLogicalPhysical".Equals(sContextMode))
                        {
                            XmlNode aReferenceNode = (XmlNode)TVmain.SelectedNode.Tag;
                            if (aReferenceNode == null)
                            {
                                MessageBox.Show(translations.frmMain_Dlg_TagNotFound);
                                return;
                            }
                            XmlNode aNode = CVersionLookupHelper.GetNodeByTagName(aReferenceNode.Name);
                            if (aNode != null)
                            {
                                aNode.Attributes["name"].Value = e.Label;
                                SaveFile(CGlobVars.currentlyLoadedFile);
                                TVmain.LabelEdit = false;
                                LoadDetails(aNode);
                                //CBproduct_SelectionChangeCommitted(sender, e);
                                //TVmain.Sort();
                            }
                        }
                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and 
                           place the node in edit mode again. */
                        e.CancelEdit = true;
                        MessageBox.Show(translations.frmMain_Dlg_InvalidLabel);
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    /* Cancel the label edit action, inform the user, and 
                       place the node in edit mode again. */
                    e.CancelEdit = true;
                    MessageBox.Show(translations.frmMain_Dlg_EmptyLabel);
                    e.Node.BeginEdit();
                }
            }
        }

        private void tsTVeditNode_Click(object sender, EventArgs e)
        {
            contextTV.Tag = "editLogicalPhysical";
            TVmain.LabelEdit = true;
            if (!TVmain.SelectedNode.IsEditing)
            {
                TVmain.SelectedNode.BeginEdit();
            }
        }

        private void tsTVcreatePhysical_Click(object sender, EventArgs e)
        {
            contextTV.Tag = "createPhysical";
            XmlNode aReferenceNode = (XmlNode)TVmain.SelectedNode.Tag;
            if (aReferenceNode == null)
            {
                MessageBox.Show(translations.frmMain_Dlg_TagNotFound);
                return;
            }
            XmlNode aNode = CVersionLookupHelper.GetNodeByTagName(aReferenceNode.Name);
            if (aNode != null)
            {
                TreeNode aNewNode = new TreeNode(translations.frmMain_tvMain_CreateEntry);
                TVmain.SelectedNode.Nodes.Add(aNewNode);
                aNewNode.ImageIndex = 3;
                aNewNode.SelectedImageIndex = 3;
                TVmain.SelectedNode = aNewNode;

                string sGuid = RZITools.GenerateGUID();
                XmlNode aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                while (aGUIDNode != null)
                {
                    sGuid = RZITools.GenerateGUID();
                    aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                }

                XmlNode aNewXmlNode = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, sGuid, string.Empty);

                XmlAttribute attrNewName = CVersionLookupHelper.m_xmlDoc.CreateAttribute("name");
                attrNewName.Value = TVmain.SelectedNode.Text;
                aNewXmlNode.Attributes.Append(attrNewName);

                XmlAttribute attrNewClient = CVersionLookupHelper.m_xmlDoc.CreateAttribute("nodetype");

                attrNewClient.Value = "physical";

                XmlElement aNewDisplayName = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "DisplayName", string.Empty);
                aNewXmlNode.AppendChild(aNewDisplayName);

                XmlElement eleIconBase64 = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "IconBase64", string.Empty);
                aNewXmlNode.AppendChild(eleIconBase64);

                XmlElement eleSupportEmail = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "SupportEmail", string.Empty);
                aNewXmlNode.AppendChild(eleSupportEmail);

                XmlElement aNewParentList = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "ParentList", string.Empty);
                aNewXmlNode.AppendChild(aNewParentList);

                XmlElement aNewMinimumVersion = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "MinimumVersion", string.Empty);
                aNewXmlNode.AppendChild(aNewMinimumVersion);

                XmlElement aNewInstFilesURL = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "InstFilesURL", string.Empty);
                aNewXmlNode.AppendChild(aNewInstFilesURL);

                XmlElement aNewReleaseNotesURL = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "ReleaseNotesURL", string.Empty);
                aNewXmlNode.AppendChild(aNewReleaseNotesURL);


                aNewXmlNode.Attributes.Append(attrNewClient);
                aNode.AppendChild(aNewXmlNode);

                TVmain.SelectedNode.Tag = aNewXmlNode;

                SaveFile(CGlobVars.currentlyLoadedFile);
                LoadDetails(aNewXmlNode);

                TVmain.LabelEdit = true;
                if (!TVmain.SelectedNode.IsEditing)
                {
                    TVmain.SelectedNode.BeginEdit();
                }
            }
        }

        private void tsTVdeleteNode_Click(object sender, EventArgs e)
        {
            contextTV.Tag = "delete";
            XmlNode aReferenceNode = (XmlNode)TVmain.SelectedNode.Tag;
            if (aReferenceNode == null)
            {
                MessageBox.Show(translations.frmMain_Dlg_TagNotFound);
                return;
            }
            if (aReferenceNode != null)
            {
                string sQuestion = "";
                if ((aReferenceNode.Attributes != null) && (aReferenceNode.Attributes["nodetype"] != null) && ("client".Equals(aReferenceNode.Attributes["nodetype"].Value)))
                {
                    sQuestion = string.Format(translations.frmMain_Dlg_DeleteClient, TVmain.SelectedNode.Text);
                }
                else if ((aReferenceNode.Attributes != null) && (aReferenceNode.Attributes["nodetype"] != null) && ("product".Equals(aReferenceNode.Attributes["nodetype"].Value)))
                {
                    sQuestion = string.Format(translations.frmMain_Dlg_DeleteProduct, TVmain.SelectedNode.Text);
                }
                else if ((aReferenceNode.Attributes != null) && (aReferenceNode.Attributes["nodetype"] != null) && ("logical".Equals(aReferenceNode.Attributes["nodetype"].Value)))
                {
                    sQuestion = string.Format(translations.frmMain_Dlg_DeleteLogical, TVmain.SelectedNode.Text);
                }
                else if ((aReferenceNode.Attributes != null) && (aReferenceNode.Attributes["nodetype"] != null) && ("physical".Equals(aReferenceNode.Attributes["nodetype"].Value)))
                {
                    sQuestion = string.Format(translations.frmMain_Dlg_DeletePhysical, TVmain.SelectedNode.Text);
                }

                if (MessageBox.Show(sQuestion, translations.frmMain_Dlg_Title_Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    aReferenceNode.ParentNode.RemoveChild(aReferenceNode);
                    SaveFile(CGlobVars.currentlyLoadedFile);

                    TVmain.SelectedNode.Remove();
                }
            }
        }

        private void TVmain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            XmlNode aNode = (XmlNode)e.Node.Tag;
            if (aNode == null)
            {
                return;
            }
            if ((aNode.Attributes != null) && (aNode.Attributes["nodetype"] != null) && ("physical".Equals(aNode.Attributes["nodetype"].Value)))
            {
                LoadDetails((XmlNode)e.Node.Tag);
                grpbxDetailHint.Visible = true;
            }
            else
            {
                grpbxDetailHint.Visible = false;
            }

            XmlNode aReferenceNode = (XmlNode)e.Node.Tag;
            if (aReferenceNode != null)
            {
                string sCaptionPath = CVersionLookupHelper.GetFullPathByTagName(aReferenceNode.Name, true);
                if (sCaptionPath.StartsWith(Environment.NewLine + " » "))
                {
                    sCaptionPath = sCaptionPath.Substring(4);
                }
                //toolTip1 = new ToolTip();
                //toolTip1.RemoveAll();
                toolTip1.SetToolTip(GRPBXmain, sCaptionPath);
                toolTip1.SetToolTip(grpbxDetailHint, sCaptionPath);
                if (aReferenceNode.Attributes["name"] != null)
                {
                    GRPBXmain.Text = aReferenceNode.Attributes["name"].Value;
                }
                else
                {
                    GRPBXmain.Text = string.Empty;
                }

            }


        }

        private void tsTVcreateClient_Click(object sender, EventArgs e)
        {
            contextTV.Tag = "createClient";
            XmlNode aReferenceNode = (XmlNode)TVmain.SelectedNode.Tag;
            if (aReferenceNode == null)
            {
                MessageBox.Show(translations.frmMain_Dlg_TagNotFound);
                return;
            }
            XmlNode aNode = CVersionLookupHelper.GetNodeByTagName(aReferenceNode.Name);
            if (aNode != null)
            {
                TreeNode aNewNode = new TreeNode(translations.frmMain_tvMain_CreateEntry);
                TVmain.SelectedNode.Nodes.Add(aNewNode);
                aNewNode.ImageIndex = 0;
                aNewNode.SelectedImageIndex = 0;
                TVmain.SelectedNode = aNewNode;

                string sGuid = RZITools.GenerateGUID();
                XmlNode aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                while (aGUIDNode != null)
                {
                    sGuid = RZITools.GenerateGUID();
                    aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                }

                string sContextMode = (String)contextTV.Tag;
                XmlNode aNewXmlNode = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, sGuid, string.Empty);

                XmlAttribute attrNewName = CVersionLookupHelper.m_xmlDoc.CreateAttribute("name");
                attrNewName.Value = TVmain.SelectedNode.Text;
                aNewXmlNode.Attributes.Append(attrNewName);

                XmlAttribute attrNewClient = CVersionLookupHelper.m_xmlDoc.CreateAttribute("nodetype");
                attrNewClient.Value = "client";
                aNewXmlNode.Attributes.Append(attrNewClient);
                aNode.AppendChild(aNewXmlNode);

                TVmain.SelectedNode.Tag = aNewXmlNode;

                SaveFile(CGlobVars.currentlyLoadedFile);

                TVmain.LabelEdit = true;
                if (!TVmain.SelectedNode.IsEditing)
                {
                    TVmain.SelectedNode.BeginEdit();
                }
            }
        }

        private void tsTVcreateProduct_Click(object sender, EventArgs e)
        {
            contextTV.Tag = "createProduct";
            XmlNode aReferenceNode = (XmlNode)TVmain.SelectedNode.Tag;
            if (aReferenceNode == null)
            {
                MessageBox.Show(translations.frmMain_Dlg_TagNotFound);
                return;
            }
            XmlNode aNode = CVersionLookupHelper.GetNodeByTagName(aReferenceNode.Name);
            if (aNode != null)
            {
                TreeNode aNewNode = new TreeNode(translations.frmMain_tvMain_CreateEntry);
                TVmain.SelectedNode.Nodes.Add(aNewNode);
                aNewNode.ImageIndex = 1;
                aNewNode.SelectedImageIndex = 1;
                TVmain.SelectedNode = aNewNode;

                string sGuid = RZITools.GenerateGUID();
                XmlNode aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                while (aGUIDNode != null)
                {
                    sGuid = RZITools.GenerateGUID();
                    aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                }

                string sContextMode = (String)contextTV.Tag;
                XmlNode aNewXmlNode = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, sGuid, string.Empty);

                XmlAttribute attrNewName = CVersionLookupHelper.m_xmlDoc.CreateAttribute("name");
                attrNewName.Value = TVmain.SelectedNode.Text;
                aNewXmlNode.Attributes.Append(attrNewName);

                XmlAttribute attrNewClient = CVersionLookupHelper.m_xmlDoc.CreateAttribute("nodetype");
                attrNewClient.Value = "product";
                aNewXmlNode.Attributes.Append(attrNewClient);
                aNode.AppendChild(aNewXmlNode);

                TVmain.SelectedNode.Tag = aNewXmlNode;

                SaveFile(CGlobVars.currentlyLoadedFile);
                //LoadDetails(aNewXmlNode);

                TVmain.LabelEdit = true;
                if (!TVmain.SelectedNode.IsEditing)
                {
                    TVmain.SelectedNode.BeginEdit();
                }
            }
        }

        private void LVdetailParents_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            LVdetailParents.Sort();
        }

        private void TXvnameInt_TextChanged(object sender, EventArgs e)
        {
            if (sInitInt.Trim().Equals(TXvnameInt.Text.Trim()))
            {
                bHasChangedInitInt = false;
            }
            else
            {
                bHasChangedInitInt = true;
            }
            configureDetailsSaveButton();

        }

        private void TXvnameExt_TextChanged(object sender, EventArgs e)
        {
            if (sInitExt.Trim().Equals(TXvnameExt.Text.Trim()))
            {
                bHasChangedInitExt = false;
            }
            else
            {
                bHasChangedInitExt = true;
            }
            configureDetailsSaveButton();
        }

        private void CBprerequisites_TextChanged(object sender, EventArgs e)
        {
            if (sInitMin.Trim().Equals(CBprerequisites.Text.Trim()))
            {
                bHasChangedInitMin = false;
            }
            else
            {
                bHasChangedInitMin = true;
            }
            configureDetailsSaveButton();
        }

        private void TXurlRN_TextChanged(object sender, EventArgs e)
        {
            if (sInitRL.Trim().Equals(TXurlRN.Text.Trim()))
            {
                bHasChangedInitRL = false;
            }
            else
            {
                bHasChangedInitRL = true;
            }
            configureDetailsSaveButton();
        }

        private void TXurlInstFile_TextChanged(object sender, EventArgs e)
        {

            if (sInitDL.Trim().Equals(TXurlInstFile.Text.Trim()))
            {
                bHasChangedInitDL = false;
            }
            else
            {
                bHasChangedInitDL = true;
            }
            configureDetailsSaveButton();
        }

        private void LVdetailParents_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            string sChecked = "";
            string sCompareInit = "";
            string sCompareNew = "";
            List<string> lsCompare = new List<string>();
            foreach (ListViewItem itItem in LVdetailParents.Items)
            {
                if (itItem.Checked)
                {
                    sChecked = "1";
                }
                else
                {
                    sChecked = "0";
                }
                XmlNode aNode = (XmlNode)itItem.Tag;
                if (aNode != null)
                {
                    lsCompare.Add(sChecked + aNode.Name);
                }
            }
            lsCompare.Sort();

            foreach (string itStr in lsCompare)
            {
                sCompareNew = sCompareNew + itStr;
            }
            foreach (string itStr in lsInitPR)
            {
                sCompareInit = sCompareInit + itStr;
            }


            if (sCompareInit.Trim().Equals(sCompareNew.Trim()))
            {
                bHasChangedInitPR = false;
            }
            else
            {
                bHasChangedInitPR = true;
            }
            configureDetailsSaveButton();
        }

        private void BdetailCancel_Click(object sender, EventArgs e)
        {
            XmlNode aNode = CVersionLookupHelper.GetNodeByTagName(sInitGuid);
            if (aNode != null)
            {
                LoadDetails(aNode);
            }

        }



        private void CBprerequisites_DrawItem(object sender, DrawItemEventArgs e)
        {

            Font myFont = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            if (e.Index == 1)//We are disabling item based on Index, you can have your logic here
            {
                e.Graphics.DrawString(cbPrerequisiteItems[cbPrerequisiteItems.Keys.ElementAt(e.Index)].ToString(), myFont, Brushes.Black, e.Bounds);
                //e.Graphics.DrawString(CBprerequisites.Items[e.Index].ToString(), myFont, Brushes.LightGray, e.Bounds);
            }
            else if (e.Index == 0)
            {
                e.DrawBackground();
                //e.Graphics.DrawString(CBprerequisites.Items[e.Index].ToString(), myFont, Brushes.Black, e.Bounds);
                e.Graphics.DrawString(cbPrerequisiteItems[cbPrerequisiteItems.Keys.ElementAt(e.Index)].ToString(), myFont, Brushes.Black, e.Bounds);
                e.DrawFocusRectangle();
            }
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void versionLookupxmlBereitstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }



        private void timerSearch_Tick(object sender, EventArgs e)
        {
            if (cbSearch.Text == "")
            {
                LoadTreeview();
                timerSearch.Stop();
            }
            else
            {
                LoadTreeview();
                lSearchCount.Visible = true;
                timerSearch.Stop();
                iSearchCnt = 0;
                ExpandTreeviewElements(TVmain.Nodes[0], cbSearch.Text);
                if (iSearchCnt > 1)
                {
                    lSearchCount.Text = string.Format(translations.frmMain_tvMain_SearchCount_Amount, iSearchCnt);
                }
                else if (iSearchCnt == 1)
                {
                    lSearchCount.Text = translations.frmMain_tvMain_SearchCount_One;
                }
                else
                {
                    lSearchCount.Text = translations.frmMain_tvMain_SearchCount_Zero;
                }
            }


        }




        private void imgSearchCancel_Click(object sender, EventArgs e)
        {
            timerSearch.Stop();
            imgSearchCancel.Visible = false;
            cbSearch.Text = translations.frmMain_tvMain_SearchPattern;
            cbSearch.ForeColor = Color.DarkGray;
            LoadTreeview();
        }

        private void cbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            grpbxDetailHint.Visible = false;
            if (cbSearch.Text.Trim().Length > 0)
            {
                if (cbSearch.Text != translations.frmMain_tvMain_SearchPattern)
                {
                    cbSearch.ForeColor = Color.Black;
                    imgSearchCancel.Visible = true;
                    timerSearch.Stop();
                    timerSearch.Start();
                }
            }
            else
            {
                timerSearch.Stop();
                imgSearchCancel.Visible = false;
                cbSearch.Text = translations.frmMain_tvMain_SearchPattern;
                cbSearch.ForeColor = Color.DarkGray;
                LoadTreeview();
                cbSearch.SelectAll();
            }

        }

        private void provideVersionLookupxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String ErrorText = "";
            RZITools.ProvideEncryptedXMLFile("", out ErrorText);
        }

        private void expandTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TVmain.ExpandAll();
        }

        private void collapseTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode aNode = TVmain.Nodes[0];
            foreach (TreeNode itClientNode in aNode.Nodes)
            {
                foreach (TreeNode itProductNode in itClientNode.Nodes)
                {
                    itProductNode.Collapse();
                }
            }
        }

        private void reloadTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadTreeview();
        }



        private void cbSearch_Enter(object sender, EventArgs e)
        {
            cbSearch.SelectAll();
        }

        private void LvDisplayGuid_Click(object sender, EventArgs e)
        {
            txDisplayGuid.Focus();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (txDisplayGuid.ContainsFocus && e.Control && e.KeyCode == Keys.C)
                Clipboard.SetText(txDisplayGuid.Text);
        }

        private void txProductTitle_TextChanged(object sender, EventArgs e)
        {
            if (sInitSupportEmail.Trim().Equals(txSupportEmail.Text.Trim()))
            {
                bHasChangedInitSupportEmail = false;
            }
            else
            {
                bHasChangedInitSupportEmail = true;
            }
            configureDetailsSaveButton();
        }






        private void loadVersionLookupxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdVersionLookup = new OpenFileDialog();
            string path = string.Empty;
            if (!string.IsNullOrWhiteSpace(CGlobVars.currentlyLoadedFile))
            {
                path = Path.GetDirectoryName(CGlobVars.currentlyLoadedFile);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    ofdVersionLookup.InitialDirectory = path;
                }
            }
            // image filters
            ofdVersionLookup.Filter = "XML Files(*.xml)|*.xml";
            if (ofdVersionLookup.ShowDialog() == DialogResult.OK)
            {
                LoadMainFrm(ofdVersionLookup.FileName);
            }

        }

        private void ExportPIF()
        {
            String ErrorText = "";
            XmlNode aNode = (XmlNode)TVmain.SelectedNode.Tag;
            if (aNode == null)
            {
                return;
            }

            if ((aNode.Attributes != null) && (aNode.Attributes["name"] != null))
            {
                SaveFileDialog sfdVersionLookup = new SaveFileDialog();
                // image filters
                sfdVersionLookup.Filter = "XML Files(*.xml; *.xmlc)|*.xml; *.xmlc";
                sfdVersionLookup.FileName = "PIF_" + RZITools.GetCleanFileName(aNode.Attributes["name"].Value);
                if (sfdVersionLookup.ShowDialog() == DialogResult.OK)
                {
                    XmlDocument xmlExportDoc = new XmlDocument();
                    XmlDeclaration xmlDeclaration = xmlExportDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlElement root = xmlExportDoc.DocumentElement;
                    xmlExportDoc.InsertBefore(xmlDeclaration, root);

                    XmlElement eleLookupContent = xmlExportDoc.CreateElement(string.Empty, "ApplicationContent", string.Empty);
                    xmlExportDoc.AppendChild(eleLookupContent);

                    XmlElement eleConfigurationInformation = xmlExportDoc.CreateElement(string.Empty, "ConfigurationInformation", string.Empty);
                    eleLookupContent.AppendChild(eleConfigurationInformation);

                    XmlElement eleLastEdited = xmlExportDoc.CreateElement(string.Empty, "LastEdited", string.Empty);
                    XmlText txtLastEdited = xmlExportDoc.CreateTextNode(RZITools.ConvertToUnixTime(DateTime.Now, out ErrorText).ToString());
                    eleLastEdited.AppendChild(txtLastEdited);
                    eleConfigurationInformation.AppendChild(eleLastEdited);

                    XmlElement eleContents = xmlExportDoc.CreateElement(string.Empty, "Contents", string.Empty);
                    eleLookupContent.AppendChild(eleContents);

                    XmlElement eleIconBase64 = xmlExportDoc.CreateElement(string.Empty, "IconBase64", string.Empty);
                    eleContents.AppendChild(eleIconBase64);

                    XmlElement eleSupportEmail = xmlExportDoc.CreateElement(string.Empty, "SupportEmail", string.Empty);
                    eleContents.AppendChild(eleSupportEmail);

                    foreach (XmlNode itNode in aNode.ChildNodes)
                    {
                        if ("IconBase64".Equals(itNode.Name))
                        {
                            eleIconBase64.InnerText = itNode.InnerText;
                        }
                        if ("SupportEmail".Equals(itNode.Name))
                        {
                            eleSupportEmail.InnerText = itNode.InnerText;
                        }
                    }


                    try
                    {
                        xmlExportDoc.Save(sfdVersionLookup.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(translations.CVersionLookupHelper_CannotSaveXML + "\r\n" + ex.Message);
                    }
                }
            }
        }

        private void tsTVexportFile_Click(object sender, EventArgs e)
        {

        }

        private void bProductsExport_Click(object sender, EventArgs e)
        {

        }

        private void TXurlRN_Leave(object sender, EventArgs e)
        {
            if (!RZITools.ValidateURL(TXurlRN.Text.Trim()))
            {
                MessageBox.Show(translations.frmMain_Dlg_InvalidURLFormat, translations.frmMain_Dlg_Title_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXurlRN.Focus();
                TXurlRN.SelectAll();
            }
        }

        private void TXurlInstFile_Leave(object sender, EventArgs e)
        {
            if (!RZITools.ValidateURL(TXurlInstFile.Text.Trim()))
            {
                MessageBox.Show(translations.frmMain_Dlg_InvalidURLFormat, translations.frmMain_Dlg_Title_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXurlInstFile.Focus();
                TXurlInstFile.SelectAll();
            }
        }

        private void bProductsChooseItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdIcon = new OpenFileDialog();
            string path = string.Empty;
            if (!string.IsNullOrWhiteSpace(CGlobVars.currentlyLoadedFile))
            {
                path = Path.GetDirectoryName(CGlobVars.currentlyLoadedFile);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    ofdIcon.InitialDirectory = path;
                }
            }
            // image filters
            ofdIcon.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (ofdIcon.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box
                imgProductIcon.Image = new Bitmap(ofdIcon.FileName);

                if (sInitIcon.Trim().Equals(RZITools.GetBase64stringFromImage(imgProductIcon.Image).Trim()))
                {
                    bHasChangedInitIcon = false;
                }
                else
                {
                    bHasChangedInitIcon = true;
                }
                configureDetailsSaveButton();
            }
        }

        private void bProductsDelIcon_Click(object sender, EventArgs e)
        {
            imgProductIcon.Image = null;

            if (sInitIcon.Trim().Equals(RZITools.GetBase64stringFromImage(imgProductIcon.Image).Trim()))
            {
                bHasChangedInitIcon = false;
            }
            else
            {
                bHasChangedInitIcon = true;
            }
            configureDetailsSaveButton();
        }

        private void txProductTitle_Leave(object sender, EventArgs e)
        {
            if (!RZITools.ValidateEmail(txSupportEmail.Text.Trim()))
            {
                MessageBox.Show(translations.frmMain_Dlg_InvalidEmailFormat, translations.frmMain_Dlg_Title_Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txSupportEmail.Focus();
                txSupportEmail.SelectAll();
            }
        }

        private void txSupportEmail_TextChanged(object sender, EventArgs e)
        {
            if (sInitSupportEmail.Trim().Equals(txSupportEmail.Text.Trim()))
            {
                bHasChangedInitSupportEmail = false;
            }
            else
            {
                bHasChangedInitSupportEmail = true;
            }
            configureDetailsSaveButton();
        }

        private void lbDetailParents_Click(object sender, EventArgs e)
        {

        }

        private void contextTV_Opening(object sender, CancelEventArgs e)
        {

        }

        private void loadEncryptedVersionLookupxmlcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdVersionLookup = new OpenFileDialog();
            string path = string.Empty;
            if (!string.IsNullOrWhiteSpace(CGlobVars.currentlyLoadedFile))
            {
                path = Path.GetDirectoryName(CGlobVars.currentlyLoadedFile);
                if (!string.IsNullOrWhiteSpace(path))
                {
                    ofdVersionLookup.InitialDirectory = path;
                }
            }
            // image filters
            ofdVersionLookup.Filter = "XMLC Files(*.xmlc;)|*.xmlc;";
            if (ofdVersionLookup.ShowDialog() == DialogResult.OK)
            {
                string ErrorText = "";
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(ofdVersionLookup.FileName, out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    MessageBox.Show("Die Datei ist in Ordnung.");
                }
            }



        }

        private void tsTVcopyPhysical_Click(object sender, EventArgs e)
        {
            contextTV.Tag = "createPhysical";
            XmlNode aReferenceNode = (XmlNode)TVmain.SelectedNode.Tag;
            if (aReferenceNode == null)
            {
                MessageBox.Show(translations.frmMain_Dlg_TagNotFound);
                return;
            }
            XmlNode aNode = CVersionLookupHelper.GetNodeByTagName(aReferenceNode.Name);
            if (aNode != null)
            {
                TreeNode aNewNode = new TreeNode(aReferenceNode.Attributes["name"].InnerText);
                TVmain.SelectedNode.Nodes.Add(aNewNode);
                aNewNode.ImageIndex = 3;
                aNewNode.SelectedImageIndex = 3;
                TVmain.SelectedNode = aNewNode;

                string sGuid = RZITools.GenerateGUID();
                XmlNode aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                while (aGUIDNode != null)
                {
                    sGuid = RZITools.GenerateGUID();
                    aGUIDNode = CVersionLookupHelper.GetNodeByTagName(sGuid);
                }

                XmlNode aNewXmlNode = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, sGuid, string.Empty);

                XmlAttribute attrNewName = CVersionLookupHelper.m_xmlDoc.CreateAttribute("name");
                attrNewName.Value = TVmain.SelectedNode.Text;
                aNewXmlNode.Attributes.Append(attrNewName);

                XmlAttribute attrNewClient = CVersionLookupHelper.m_xmlDoc.CreateAttribute("nodetype");
                attrNewClient.Value = "physical";
                aNewXmlNode.Attributes.Append(attrNewClient);

                XmlElement aNewDisplayName = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "DisplayName", string.Empty);
                aNewDisplayName.InnerText = aReferenceNode["DisplayName"].InnerText;
                aNewXmlNode.AppendChild(aNewDisplayName);

                XmlElement eleIconBase64 = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "IconBase64", string.Empty);
                eleIconBase64.InnerText = aReferenceNode["IconBase64"].InnerText;
                aNewXmlNode.AppendChild(eleIconBase64);

                XmlElement eleSupportEmail = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "SupportEmail", string.Empty);
                eleSupportEmail.InnerText = aReferenceNode["SupportEmail"].InnerText;
                aNewXmlNode.AppendChild(eleSupportEmail);

                XmlElement aNewParentList = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "ParentList", string.Empty);
                //aNewParentList.InnerText = aReferenceNode["ParentList"].InnerText;
                aNewXmlNode.AppendChild(aNewParentList);

                XmlElement aNewMinimumVersion = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "MinimumVersion", string.Empty);
                aNewMinimumVersion.InnerText = aReferenceNode["MinimumVersion"].InnerText;
                aNewXmlNode.AppendChild(aNewMinimumVersion);

                XmlElement aNewInstFilesURL = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "InstFilesURL", string.Empty);
                aNewInstFilesURL.InnerText = aReferenceNode["InstFilesURL"].InnerText;
                aNewXmlNode.AppendChild(aNewInstFilesURL);

                XmlElement aNewReleaseNotesURL = CVersionLookupHelper.m_xmlDoc.CreateElement(string.Empty, "ReleaseNotesURL", string.Empty);
                aNewReleaseNotesURL.InnerText = aReferenceNode["ReleaseNotesURL"].InnerText;
                aNewXmlNode.AppendChild(aNewReleaseNotesURL);



                aNode.AppendChild(aNewXmlNode);

                TVmain.SelectedNode.Tag = aNewXmlNode;

                SaveFile(CGlobVars.currentlyLoadedFile);

                LoadDetails(aNewXmlNode);

                TVmain.LabelEdit = true;
                if (!TVmain.SelectedNode.IsEditing)
                {
                    TVmain.SelectedNode.BeginEdit();
                }
            }
        }

        private void SaveFile(string filePath)
        {
            if (filePath.EndsWith(".xmlc"))
            {
                //string ErrorText = string.Empty;
                //CVersionLookupHelper.SaveVersionLookupXML(filePath);
                //if (!RZITools.ProvideEncryptedXMLFile(filePath, out ErrorText))
                //{
                //    MessageBox.Show("Fehler beim Schreiben der verschlüsselten Datei:" + Environment.NewLine + ErrorText, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                CVersionLookupHelper.SaveVersionLookupXML(filePath);
            }
        }

        private void tsTVExpandBranch_Click(object sender, EventArgs e)
        {
            if (TVmain.SelectedNode != null)
            {
                TVmain.SelectedNode.ExpandAll();
            }
        }

        private void tsTVCollapseBranch_Click(object sender, EventArgs e)
        {
            if (TVmain.SelectedNode != null)
            {
                TVmain.SelectedNode.Collapse(false);
            }
        }

        private void editGUIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txDisplayGuid.ReadOnly = false;
            txDisplayGuid.Focus();
            txDisplayGuid.SelectAll();
            txDisplayGuid.ContextMenuStrip = null;
        }

        private void txDisplayGuid_TextChanged(object sender, EventArgs e)
        {
            lblGuidError.Visible = false;
            lblGuidAlreadyInUse.Visible = false;
            if ((!string.IsNullOrWhiteSpace(sInitGuid)) && (!sInitGuid.Trim().Equals(txDisplayGuid.Text.Trim())))
            {
                bHasChangedInitGuid = true;
            }
            else
            {
                bHasChangedInitGuid = false;
            }


            bool isLetter = !String.IsNullOrWhiteSpace(txDisplayGuid.Text) && Char.IsLetter(txDisplayGuid.Text[0]);
            if (isLetter)
            {
                if (bHasChangedInitGuid)
                {
                    XmlNodeList nodeList = CVersionLookupHelper.m_xmlDoc.GetElementsByTagName(txDisplayGuid.Text.Trim());
                    if (nodeList.Count > 0)
                    {
                        lblGuidAlreadyInUse.Visible = true;
                    }
                }
            }
            else
            {
                lblGuidError.Visible = true;
            }
            configureDetailsSaveButton();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.PageDown)
            {
                if (TVmain.SelectedNode != null)
                {
                    TVmain.SelectedNode.ExpandAll();
                    return true;
                }
                
            }
            if (keyData == Keys.PageUp)
            {
                if (TVmain.SelectedNode != null)
                {
                    TVmain.SelectedNode.Collapse();
                    return true;
                }
                
            }
            if (keyData == Keys.F4)
            {
                tsTVhideParentDownloads.PerformClick();
                return true;
            }
            if (keyData == Keys.F5)
            {
                LoadTreeview();
                return true;
            }
            if (keyData == Keys.Delete)
            {
                if (TVmain.SelectedNode != null)
                {
                    tsTVdeleteNode.PerformClick();
                    return true;
                }
                
            }
            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void tsTVhideParentDownloads_Click(object sender, EventArgs e)
        {
            TVmain.BeginUpdate();
            List<TreeNode> delList = new List<TreeNode>();
            TreeNode parent = TVmain.SelectedNode.Parent;
            while ((parent != null) && (parent.ImageIndex == 3))
            {
                RemoveNode(parent);
                parent = TVmain.SelectedNode.Parent;
            }
            TVmain.EndUpdate();
        }

        private void RemoveNode(TreeNode nodeToRemove)
        {
            if (nodeToRemove.Nodes.Count > 0)
            {
                List<TreeNode> childNodes = new List<TreeNode>();
                foreach (TreeNode n in nodeToRemove.Nodes)
                {
                    childNodes.Add(n);
                }
                if ((nodeToRemove.Parent != null))
                {
                    foreach (TreeNode child in childNodes)
                    {
                        TreeNode newNode = (TreeNode)child.Clone();
                        nodeToRemove.Parent.Nodes.Add(newNode);
                    }
                    foreach (TreeNode child in childNodes)
                    {
                        child.Remove();
                    }

                }
                else
                {
                    //nodeToRemove.TreeView.Nodes.AddRange(childNodes.ToArray());
                }
            }
            nodeToRemove.Remove();
        }
    }
}
