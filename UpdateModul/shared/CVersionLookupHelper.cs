using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using UpdateModul.shared;

namespace UpdateModul
{
    class CVersionLookupHelper
    {
        public static XmlDocument m_xmlDoc;
        public static List<XmlNode> m_CollectedBaseVersionList;
        public static List<XmlNode> m_CollectedLeafList;

        

        public static string getVersionLookupPath()
        {
            return CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML;
        }


        public static bool LoadVersionLookupXML(string filePath, bool createNewFileIfNotExists)
        {
            CLog.Debug("Enter function 'LoadVersionLookupXML'");
            CLog.Debug("Provided 'filePath': {0}", filePath);
            CLog.Debug("Provided 'createNewFileIfNotExists': {0}", createNewFileIfNotExists);

            string ErrorText = "";
  
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    } catch (Exception ex)
                    {
                        MessageBox.Show(translations.CVersionLookupHelper_CannotCreateDirectory + "\r\n" + ex.Message);
                        return false;
                    }
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(translations.CVersionLookupHelper_CannotLoadXML + "\r\n" + e.Message);
                return false;
            }

            if (File.Exists(filePath))
            {
                try
                {
                    m_xmlDoc = new XmlDocument();
                    m_xmlDoc.Load(filePath);
                    //m_xmlDoc.Load(@"C:\temp\VersionLookup.xml");
                    CLog.Debug(CGlobVars.VERSION_LOOKUP_XML + " has been successfully loaded.");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(translations.CVersionLookupHelper_CannotLoadXML + "\r\n" + ex.Message);
                    CLog.Debug("Error loading " + CGlobVars.VERSION_LOOKUP_XML + ": { 0}", ex.ToString());
                    return false;
                }
            }
            else
            {
                CLog.Debug("Creating new " + CGlobVars.VERSION_LOOKUP_XML + ".");
                m_xmlDoc = new XmlDocument();
                XmlDeclaration xmlDeclaration = m_xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = m_xmlDoc.DocumentElement;
                m_xmlDoc.InsertBefore(xmlDeclaration, root);

                XmlElement eleLookupContent = m_xmlDoc.CreateElement(string.Empty, "LookupContent", string.Empty);
                m_xmlDoc.AppendChild(eleLookupContent);

                XmlElement eleConfigurationInformation = m_xmlDoc.CreateElement(string.Empty, "ConfigurationInformation", string.Empty);
                eleLookupContent.AppendChild(eleConfigurationInformation);

                XmlElement eleLastEdited = m_xmlDoc.CreateElement(string.Empty, "LastEdited", string.Empty);
                XmlText txtLastEdited = m_xmlDoc.CreateTextNode(RZITools.ConvertToUnixTime(DateTime.Now, out ErrorText).ToString());
                eleLastEdited.AppendChild(txtLastEdited);
                eleConfigurationInformation.AppendChild(eleLastEdited);

                XmlElement eleLookupVersion = m_xmlDoc.CreateElement(string.Empty, "LookupVersion", string.Empty);
                XmlText txtLookupVersion = m_xmlDoc.CreateTextNode("1.0.0.0");
                eleLookupVersion.AppendChild(txtLookupVersion);
                eleConfigurationInformation.AppendChild(eleLookupVersion);

                XmlElement eleLogLevel = m_xmlDoc.CreateElement(string.Empty, "LogLevel", string.Empty);
                XmlText txtLogLevel = m_xmlDoc.CreateTextNode("INFO");
                eleLogLevel.AppendChild(txtLogLevel);
                eleConfigurationInformation.AppendChild(eleLogLevel);

                XmlElement eleVersions = m_xmlDoc.CreateElement(string.Empty, "Versions", string.Empty);
                eleLookupContent.AppendChild(eleVersions);

                try
                {
                    m_xmlDoc.Save(filePath);
                    CLog.Debug(CGlobVars.VERSION_LOOKUP_XML + " has been successfully created.");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(translations.CVersionLookupHelper_CannotSaveXML + "\r\n" + ex.Message);
                    CLog.Debug("Error creating " + CGlobVars.VERSION_LOOKUP_XML + ": { 0}", ex.ToString());
                    return false;
                }

            }
        }


        public static bool SaveVersionLookupXML(string filePath)
        {
            CLog.Debug("Enter function 'SaveVersionLookupXML'");

            String ErrorText = "";
            XmlNode aNode = GetNodeByTagName("LastEdited");
            if ((aNode != null) && (aNode.ParentNode != null) && ("ConfigurationInformation".Equals(aNode.ParentNode.Name)))
            {
                aNode.InnerText = RZITools.ConvertToUnixTime(DateTime.Now, out ErrorText).ToString();
            }
            try
            {
                if ((filePath != null) && (filePath.Trim() == ""))
                {
                    m_xmlDoc.Save(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML);
                } else
                {
                    m_xmlDoc.Save(filePath);
                }
                
                CLog.Debug(CGlobVars.VERSION_LOOKUP_XML + " has been successfully saved.");
                return true;
            }
            catch (Exception ex)
            {
                CLog.Debug("Error saving " + CGlobVars.VERSION_LOOKUP_XML + ": { 0}", ex.ToString());
                return false;
            }

        }

        /// <summary>
        /// Decrypts a provided file and loads VersionLookup.xml.
        /// </summary>
        /// <param name="VersionLookupFile"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        public static int DecryptAndLoadVersionLookupXML(String VersionLookupFile, bool ShowErrorIfNotExist, out string ErrorText)
        {
            CLog.Info("Enter function 'DecryptAndLoadVersionLookupXML'");
            CLog.Debug("Provided 'VersionLookupFile': {0}", VersionLookupFile);
            CLog.Debug("Provided 'ShowErrorIfNotExist': {0}", ShowErrorIfNotExist);

            CLog.Info("Checking if file exists.'");
            if (!File.Exists(VersionLookupFile))
            {
                CLog.Info("File does not exist yet.'");
                if (ShowErrorIfNotExist)
                {
                    CLog.Info("Showing error that file cannot be loaded.'");
                    //frmError.ShowError(translations.CVersionLookupHelper_CannotLoadXML);
                }
                ErrorText = "";
                return CReturnCodes.COULD_NOT_DECRYPT_VERSION_LOOKUP_XML;
            }

            CLog.Info("Checking file size to prevent from loading 0 byte file.");
            var length = new System.IO.FileInfo(VersionLookupFile).Length;
            if (length == 0)
            {
                CLog.Info("File is empty, showing error.");
                ErrorText = "";
                return CReturnCodes.LOOKUP_XML_IS_EMPTY;
            }


            // Decrypt File
            CLog.Info("Starting decryption of downloaded file.");
            if (!RZITools.DecryptFile(VersionLookupFile, CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML, CGlobVars.PASSWORD_TOKEN, out ErrorText))
            {
                if (ShowErrorIfNotExist)
                {
                    //frmError.ShowError(ErrorText);
                }

                if (File.Exists(VersionLookupFile))
                {
                    try
                    {
                        File.Delete(VersionLookupFile);
                    }
                    catch (Exception)
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
                //frmError.ShowError(ErrorText);
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


        public static List<XmlNode> GetDownloadLeafs()
        {
            CLog.Debug("Enter function 'GetDownloadLeafs'");

            XmlNode aVersionNode = GetNodeByTagName("Versions");
            if (aVersionNode == null)
            {
                return null;
            }
            foreach (XmlNode itClientNode in aVersionNode.ChildNodes)
            {
                foreach (XmlNode itProductNode in itClientNode.ChildNodes)
                {
                    CollectLeafNodesByTagName(itProductNode);
                }
            }
            return m_CollectedLeafList;
        }

        public static void CollectLeafNodesByTagName(XmlNode node)
        {
            CLog.Debug("Enter function 'SaveVersionLookupXML'");
            if (node != null)
            {
                CLog.Debug("Provided 'node': {0}", node.OuterXml);
            }
            

            ProcessNode(node);
        }

        public static void ProcessNode(XmlNode node)
        {
            CLog.Debug("Enter function 'SaveVersionLookupXML'");
            if (node != null)
            {
                CLog.Debug("Provided 'node': {0}", node.OuterXml);
            }


            if ((node.ChildNodes.Count == 7) && (!"ParentList".Equals(node.Name)))
            {
                if ("physical".Equals(node.Attributes["nodetype"].Value))
                {
                    m_CollectedLeafList.Add(node);
                }
            }

            foreach (XmlNode child in node.ChildNodes)
            {
                ProcessNode(child);
            }
        }


        public static bool FindAttributeValueForClientName(string sValue)
        {
            XmlNode aNode = GetNodeByTagName("Versions");
            if (aNode != null)
            {
                foreach (XmlNode itNode in aNode.ChildNodes)
                {
                    if (itNode.Attributes["nodetype"] != null)
                    {
                        if (itNode.Attributes["nodetype"].Value.ToLower().Equals("client"))
                        {
                            if (itNode.Attributes["name"].Value.ToLower().Equals(sValue.ToLower()))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static bool FindAttributeValueForProductName(string sValue, string sClientTagName)
        {
            XmlNode aNode = GetNodeByTagName(sClientTagName);
            if (aNode != null)
            {
                foreach (XmlNode itNode in aNode.ChildNodes)
                {
                    if (itNode.Attributes["nodetype"] != null)
                    {
                        if (itNode.Attributes["nodetype"].Value.ToLower().Equals("product"))
                        {
                            if (itNode.Attributes["name"].Value.ToLower().Equals(sValue.ToLower()))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static XmlNode FindNodeForClientAttributeValue(string sValue, string sTarget)
        {
            XmlNode aNode = GetNodeByTagName("Versions");
            foreach (XmlNode itNode in aNode.ChildNodes)
            {
                if (itNode.Attributes["nodetype"] != null)
                {
                    if (itNode.Attributes["nodetype"].Value.ToLower().Equals(sTarget.ToLower()))
                    {
                        if (itNode.Attributes["name"].Value.ToLower().Equals(sValue.ToLower()))
                        {
                            return itNode;
                        }
                    }
                }
            }
            return null;
        }


        public static XmlNode GetLeaf(XmlNode node)
        {
            CLog.Debug("Enter function 'GetLeaf'");
            if (node != null)
            {
                CLog.Debug("Provided 'node': {0}", node.OuterXml);
            }

            if ((node.ChildNodes != null) && (node.ChildNodes.Count == 7))
            {
                CLog.Debug("Current node is leaf, returning node.");
                return node;
            }
            foreach (XmlNode Child in node.ChildNodes)
            {
                CLog.Debug("Processing child node 'Child': {0}", Child);
                XmlNode retNode = GetLeaf(Child);
                if (retNode != null)
                {
                    CLog.Debug("Returning leaf node.");
                    return retNode;
                }
            }
            return null;
        }

        public static List<XmlNode> GetNewBaseVersionByMaxGuid(string Guid)
        {
            CLog.Debug("Enter function 'GetNewBaseVersionByMaxGuid'");
            CLog.Debug("Provided 'Guid': {0}", Guid);

            ClearCollectedBaseVersions();
            XmlNodeList PhysicalTagList = m_xmlDoc.GetElementsByTagName("PhysicalTag");
            foreach (XmlNode Node in PhysicalTagList)
            {
                if (Node != null)
                {
                    CLog.Debug("Processing physical tag node 'Node': {0}", Node.OuterXml);
                }
                if ((Node.InnerText != null) && (Guid.Equals(Node.InnerText)))
                {
                    CLog.Debug("Found matching Guid 'Node.InnerText': {0}", Node.InnerText);
                    m_CollectedBaseVersionList.Add(Node.ParentNode.ParentNode);
                    CLog.Debug("Added base version node to list 'CollectedBaseVersionList': {0}", Node.ParentNode.ParentNode);
                }
            }

            // max 10 versions
            String newGuid = "";
            for (int iCnt = 1; iCnt <= 10; iCnt++)
            {
                for (int iBaseCnt = 0; iBaseCnt < m_CollectedBaseVersionList.Count; iBaseCnt++)
                {
                    CLog.Debug("Processing base version list 'CollectedBaseVersionList': {0}", m_CollectedBaseVersionList[iBaseCnt]);
                    XmlNode leaf = GetLeaf(m_CollectedBaseVersionList[iBaseCnt]);
                    newGuid = leaf.Name;
                    XmlNodeList newList = m_xmlDoc.GetElementsByTagName("PhysicalTag");
                    for (int iListCnt = 0; iListCnt < newList.Count; iListCnt++)
                    {
                        CLog.Debug("Processing physical tag node list 'newList': {0}", newList[iListCnt]);
                        if ((newList[iListCnt].InnerText != null) && (newGuid.Equals(newList[iListCnt].InnerText)))
                        {
                            CLog.Debug("Replacing matching base version due to newer version 'newList[iListCnt].InnerText': {0}", newList[iListCnt].InnerText);
                            m_CollectedBaseVersionList[iBaseCnt] = newList[iListCnt].ParentNode.ParentNode;
                        }
                    }
                }
            }
            CLog.Debug("Returning 'CollectedBaseVersionList': {0}", m_CollectedBaseVersionList);
            return m_CollectedBaseVersionList;
        }


        public static XmlNode GetNodeByTagName(string TagName)
        {
            CLog.Debug("Enter function 'GetNodeByTagName'");
            CLog.Debug("Provided 'TagName': {0}", TagName);
            if (m_xmlDoc == null)
            {
                CLog.Debug("Returning null due to 'm_xmlDoc' is null");
                return null;
            }
            XmlNodeList NodeList = m_xmlDoc.GetElementsByTagName(TagName);
            if (NodeList.Count != 1)
            {
                CLog.Debug("Returning null due to 'NodeList.Count' != 1", NodeList.Count);
                return null;
            }
            CLog.Debug("Returning 'NodeList[0]'", NodeList[0]);
            return NodeList[0];
        }

        public static void ClearCollectedLeafs()
        {
            m_CollectedLeafList = new List<XmlNode>();
            m_CollectedLeafList.Clear();
        }


        public static string GetFullPathByTagName(string sTagName, bool withNewLines)
        {

            XmlNode aNode = GetNodeByTagName(sTagName);
            string spacePlaceHolder = "[$space$]";
            string newLine = (withNewLines ? Environment.NewLine : string.Empty);
            StringBuilder builder = new StringBuilder();
            while (aNode != null)
            {
                switch (aNode.NodeType)
                {
                    case XmlNodeType.Attribute:
                        builder.Insert(0, newLine + spacePlaceHolder + " » @" + aNode.Name);
                        aNode = ((XmlAttribute)aNode).OwnerElement;
                        break;
                    case XmlNodeType.Element:
                        if ((("Versions".Equals(aNode.Name)) || ("LookupContent".Equals(aNode.Name))) && (aNode.Attributes["name"] == null))
                        {
                            aNode = aNode.ParentNode;
                            break;
                        }
                        //int index = FindElementIndex((XmlElement)aNode);
                        //builder.Insert(0, "/" + aNode.Name + "[" + index + "]");
                        if (aNode.Attributes["name"] != null)
                        {
                            builder.Insert(0, newLine + spacePlaceHolder + " » " + aNode.Attributes["name"].Value);
                        }
                        else
                        {
                            builder.Insert(0, newLine + spacePlaceHolder + " » " + aNode.Name);
                        }

                        aNode = aNode.ParentNode;
                        break;
                    case XmlNodeType.Document:
                        string raw = builder.ToString();
                        int numLines = Regex.Matches(raw, Environment.NewLine).Count;
                        string rep = string.Empty;
                        for (int i = 0; i< numLines; i++)
                        {
                            rep = string.Empty;
                            for (int irep = 0; irep < i; irep++)
                            {
                                rep = rep + "  ";
                            }
                            if (raw.IndexOf(spacePlaceHolder) > -1)
                            {
                                raw = raw.Substring(0, raw.IndexOf(spacePlaceHolder)) + rep + raw.Substring(raw.IndexOf(spacePlaceHolder) + spacePlaceHolder.Length);
                            }
                        }

                        return raw;
                    default:
                        throw new ArgumentException("Only elements and attributes are supported");
                }
            }
            throw new ArgumentException("Node was not in a document");
        }


        public static String GetMinimumVersionGUID(XmlNode aNode)
        {
            if (aNode.HasChildNodes)
            {
                foreach (XmlNode aChild in aNode.ChildNodes)
                {
                    if (aChild.Name.Equals("MinimumVersion"))
                    {
                        return aChild.InnerText;
                    }
                }
            }
            return "";
        }



        public static bool CheckIfNodeIsChildOfAnotherNode(XmlNode aNode, String sTagName)
        {
            XmlNode aaa = FindParentNode(aNode, sTagName);
            if (aaa != null)
            {
                return true;
            }
            return false;
        }

        public static XmlNode FindParentNode(XmlNode aNode, String sTagName)
        {
            if (aNode != null)
            {
                if (aNode.Name.Equals(sTagName))
                {
                    return aNode;
                }
                else
                {
                    XmlNode aParentNode = FindParentNode(aNode.ParentNode, sTagName);
                    return aParentNode;
                }

            }
            return null;
        }


        public static void ClearCollectedBaseVersions()
        {
            CLog.Debug("Enter function 'ClearCollectedBaseVersions'");
            m_CollectedBaseVersionList = new List<XmlNode>();
            m_CollectedBaseVersionList.Clear();
            CLog.Debug("Leave function 'ClearCollectedBaseVersions'");
        }


        public static void GetAllPhysicalNodes(XmlNode Parent, List<XmlNode> NodeList)
        {
            CLog.Debug("Enter function 'GetAllPhysicalNodes'");
            if (Parent != null)
            {
                CLog.Debug("Provided 'Parent': {0}", Parent.OuterXml);
            }
            
            CLog.Debug("Provided 'NodeList': {0}", NodeList);
            if (!Parent.HasChildNodes)
            {
                CLog.Debug("Returning null due to missing child nodes.");
                return;
            }

            var list = Parent.SelectNodes("*[@nodetype=\"physical\"]");
            if (list.Count > 0)
            {
                NodeList.Add(list[0]);
                GetAllPhysicalNodes(list[0], NodeList);
            }
        }

        public static String GetNodeText(XmlNode CurrentNode, String SubNodeName)
        {
            CLog.Debug("Enter function 'GetNodeText'");
            if (CurrentNode != null)
            {
                CLog.Debug("Provided 'CurrentNode': {0}", CurrentNode.OuterXml);
            }
            CLog.Debug("Provided 'SubNodeName': {0}", SubNodeName);
            if (CurrentNode != null)
            {
                var SubNode = CurrentNode.SelectSingleNode(SubNodeName);
                if (SubNode == null)
                {
                    CLog.Debug("Returning null due to missing child node: {0}", SubNodeName);
                    return String.Empty;
                }

                CLog.Debug("Returning 'SubNode.InnerText': {0}", SubNode.InnerText);
                return SubNode.InnerText;
            }
            CLog.Debug("Returning emtpy string due to no matching child node.");
            return "";
        }

        

        
    }
}
