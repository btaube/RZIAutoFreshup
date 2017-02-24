using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using UpdateModul.shared;

namespace UpdateModul
{
    class CVersionConfigHelper
    {

        public static XmlDocument xmlDoc;

        public static bool LoadVersionConfigXML(out String ErrorText)
        {
            CLog.Debug("Entered function 'LoadVersionConfigXML'");

            ErrorText = null;

            if (File.Exists(CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML))
            {
                try
                {
                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML);
                    ErrorText = null;
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorText = translations.CVersionLookupHelper_CannotCreateDirectory + "\r\n" + ex.ToString();
                    return false;
                }
            }
            else
            {
                xmlDoc = new XmlDocument();
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = xmlDoc.DocumentElement;
                xmlDoc.InsertBefore(xmlDeclaration, root);

                XmlElement eleLookupContent = xmlDoc.CreateElement(string.Empty, "UpdateCheckContent", string.Empty);
                xmlDoc.AppendChild(eleLookupContent);

                XmlElement eleConfigurationInformation = xmlDoc.CreateElement(string.Empty, "ConfigurationInformation", string.Empty);
                eleLookupContent.AppendChild(eleConfigurationInformation);

                XmlElement eleLastEdited = xmlDoc.CreateElement(string.Empty, "LastEdited", string.Empty);
                XmlText txtLastEdited = xmlDoc.CreateTextNode(RZITools.ConvertToUnixTime(DateTime.Now, out ErrorText).ToString());
                if (ErrorText != null)
                {
                    return false;
                }
                eleLastEdited.AppendChild(txtLastEdited);
                eleConfigurationInformation.AppendChild(eleLastEdited);

                XmlElement eleConfigVersion = xmlDoc.CreateElement(string.Empty, "ConfigVersion", string.Empty);
                XmlText txtConfigVersion = xmlDoc.CreateTextNode("1.0.0.0");
                eleConfigVersion.AppendChild(txtConfigVersion);
                eleConfigurationInformation.AppendChild(eleConfigVersion);

                XmlElement eleVersions = xmlDoc.CreateElement(string.Empty, "Settings", string.Empty);
                eleLookupContent.AppendChild(eleVersions);

                XmlElement eleCommon = xmlDoc.CreateElement(string.Empty, "Common", string.Empty);
                XmlAttribute attrCommon = xmlDoc.CreateAttribute("name");
                attrCommon.Value = "Common";
                eleCommon.Attributes.Append(attrCommon);
                eleVersions.AppendChild(eleCommon);

                XmlElement eleCheckDays = xmlDoc.CreateElement(string.Empty, "CheckDays", string.Empty);
                eleCheckDays.InnerText = "0";
                eleCommon.AppendChild(eleCheckDays);

                XmlElement eleLogLevel = xmlDoc.CreateElement(string.Empty, "LogLevel", string.Empty);
                eleLogLevel.InnerText = "INFO";
                eleCommon.AppendChild(eleLogLevel);

                XmlElement eleLastChecked = xmlDoc.CreateElement(string.Empty, "LastChecked", string.Empty);
                eleCommon.AppendChild(eleLastChecked);

                XmlElement eleRepository = xmlDoc.CreateElement(string.Empty, "Repository", string.Empty);
                XmlAttribute attrRepository = xmlDoc.CreateAttribute("name");
                attrRepository.Value = "Repository";
                eleRepository.Attributes.Append(attrRepository);
                eleVersions.AppendChild(eleRepository);

                XmlElement eleLookupURL = xmlDoc.CreateElement(string.Empty, "LookupURL", string.Empty);
                eleLookupURL.InnerText = @"http://www.ds-punkte.de/VersionLookup.xmlc";
                eleRepository.AppendChild(eleLookupURL);

                XmlElement eleSecurity = xmlDoc.CreateElement(string.Empty, "Security", string.Empty);
                XmlAttribute attrSecurity = xmlDoc.CreateAttribute("name");
                attrSecurity.Value = "Security";
                eleSecurity.Attributes.Append(attrSecurity);
                eleVersions.AppendChild(eleSecurity);

                XmlElement elePassword = xmlDoc.CreateElement(string.Empty, "Password", string.Empty);
                eleSecurity.AppendChild(elePassword);

                //
                XmlElement eleProxy = xmlDoc.CreateElement(string.Empty, "Proxy", string.Empty);
                XmlAttribute attrProxy = xmlDoc.CreateAttribute("name");
                attrProxy.Value = "Proxy";
                eleProxy.Attributes.Append(attrProxy);
                eleVersions.AppendChild(eleProxy);

                XmlElement eleUseProxy = xmlDoc.CreateElement(string.Empty, "UseProxy", string.Empty);
                eleUseProxy.InnerText = "0";
                eleProxy.AppendChild(eleUseProxy);

                XmlElement eleProxyServer = xmlDoc.CreateElement(string.Empty, "ProxyServer", string.Empty);
                eleProxyServer.InnerText = "";
                eleProxy.AppendChild(eleProxyServer);

                XmlElement eleProxyPort = xmlDoc.CreateElement(string.Empty, "ProxyPort", string.Empty);
                eleProxyPort.InnerText = "";
                eleProxy.AppendChild(eleProxyPort);

                XmlElement eleUseDefCred = xmlDoc.CreateElement(string.Empty, "UseDefCred", string.Empty);
                eleUseDefCred.InnerText = "0";
                eleProxy.AppendChild(eleUseDefCred);

                XmlElement eleProxyUser = xmlDoc.CreateElement(string.Empty, "ProxyUser", string.Empty);
                eleProxyUser.InnerText = "";
                eleProxy.AppendChild(eleProxyUser);

                XmlElement eleProxyPassw = xmlDoc.CreateElement(string.Empty, "ProxyPassw", string.Empty);
                eleProxyPassw.InnerText = "";
                eleProxy.AppendChild(eleProxyPassw);

                XmlElement eleBypassOnLan = xmlDoc.CreateElement(string.Empty, "BypassOnLan", string.Empty);
                eleBypassOnLan.InnerText = "0";
                eleProxy.AppendChild(eleBypassOnLan);

                try
                {
                    xmlDoc.Save(CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML);
                    string finalSource = CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML;
                    string finalDest = CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_CONFIG_XML;

                    if (File.Exists(finalDest))
                    {
                        File.Delete(finalDest);
                    }

                    RZITools.EncryptFile(finalSource, finalDest, CGlobVars.PASSWORD_TOKEN, out ErrorText);

                    if (File.Exists(finalSource))
                    {
                        File.Delete(finalSource);
                    }

                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(translations.CVersionLookupHelper_CannotSaveXML + "\r\n" + e.Message);
                    return false;
                }

            }
        }

        public static string GetInnerTextByPathAsString(string Path, out String ErrorText)
        {
            ErrorText = null;
            var aNode = xmlDoc.SelectSingleNode(Path);
            if (aNode == null)
            {
                ErrorText = "Node " + Path + " not found.";
                return null;
            }
            return aNode.InnerText;
        }

        public static bool GetInnerTextByPathAsBool(string Path, out String ErrorText)
        {
            ErrorText = null;
            var aNode = xmlDoc.SelectSingleNode(Path);
            if (aNode == null)
            {
                ErrorText = "Node " + Path + " not found.";
                return false;
            }
            if (aNode.InnerText.Trim() == "1")
            {
                return true;
            } else
            {
                return false;
            }
        }


        /// <summary>
        /// Decrypts a provided file and loads VersionConfig.xml.
        /// </summary>
        /// <param name="VersionConfigFile"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        public static int DecryptAndLoadVersionConfigXML(String VersionConfigFile, out string ErrorText)
        {
            if (File.Exists(VersionConfigFile))
            {
                // Decrypt File
                CLog.Info("Starting decryption of VersionConfig.xmlc.");
                if (!RZITools.DecryptFile(VersionConfigFile, CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML, CGlobVars.PASSWORD_TOKEN, out ErrorText))
                {
                    return CReturnCodes.COULD_NOT_DECRYPT_VERSION_CONFIG_XML;
                }

                // Create delay due to anti virus scans that may lock the file
                // TODO Replace with better way
                Thread.Sleep(500);
            }

            // Load Current Version
            CLog.Info("Loading VersionConfig.xml.");
            if (!CVersionConfigHelper.LoadVersionConfigXML(out ErrorText))
            {
                CLog.Error("Could not load VersionConfig.xml.");
                CLog.Error("Error message: {0}", ErrorText);
                return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
            }


            // Delete decrypted file
            //File.Delete(CGlobVars.wrkDir + "VersionConfig.xml");
            if (File.Exists(CGlobVars.wrkDir + "VersionConfig.xml"))
            {
                CLog.Warning("Could not delete VersionConfig.xml from disk: {0}", CGlobVars.wrkDir + "VersionConfig.xml");
            }
            else
            {
                CLog.Info("Deleted VersionConfig.xml from disk successfully.");
            }

            return CReturnCodes.OK;
        }

        public static string GetInnerTextByPathAsPW(string Path, out String ErrorText)
        {
            ErrorText = null;
            var aNode = xmlDoc.SelectSingleNode(Path);
            if (aNode == null)
            {
                ErrorText = "Node " + Path + " not found.";
                return null;
            }
            return  RZITools.Decrypt(aNode.InnerText, out ErrorText);
        }


        public static bool SetInnerTextByPathAsString(string Path, string Value, out String ErrorText)
        {
            CLog.Debug("Entered function 'SetInnerTextByPathAsString'");
            CLog.Debug("Provided 'Path': {0}", Path);
            CLog.Debug("Provided 'Value': {0}", Value);

            var aNode = xmlDoc.SelectSingleNode(Path);
            if (aNode == null)
            {
                ErrorText = "Node " + Path + " not found.";
                return false;
            }
            aNode.InnerText = Value;
            if (!SaveVersionConfigXML(out ErrorText))
            {
                return false;
            }
            ErrorText = null;
            return true;
        }

        public static bool SetInnerTextByPathAsPW(string Path, string Value, out String ErrorText)
        {
            CLog.Debug("Entered function 'SetInnerTextByPathAsPW'");
            CLog.Debug("Provided 'Path': {0}", Path);
            CLog.Debug("Provided 'Value': {0}", "<hidden password>");

            var aNode = xmlDoc.SelectSingleNode(Path);
            if (aNode == null)
            {
                ErrorText = "Node " + Path + " not found.";
                return false;
            }
            aNode.InnerText = RZITools.Encrypt(Value, out ErrorText);
            if (ErrorText != null)
            {
                return false;
            }
            if (!SaveVersionConfigXML(out ErrorText))
            {
                return false;
            }
            ErrorText = null;
            return true;
        }

        public static bool SetInnerTextByPathAsBool(string Path, bool Value, out String ErrorText)
        {
            CLog.Debug("Entered function 'SetInnerTextByPathAsBool'");
            CLog.Debug("Provided 'Path': {0}", Path);
            CLog.Debug("Provided 'Value': {0}", Value);

            var aNode = xmlDoc.SelectSingleNode(Path);
            if (aNode == null)
            {
                ErrorText = "Node " + Path + " not found.";
                return false;
            }
            aNode.InnerText = Value ? "1" : "0";
            if (!SaveVersionConfigXML(out ErrorText))
            {
                return false;
            }
            ErrorText = null;
            return true;
        }


        public static string GetFullPathByTagName(string TagName, out String ErrorText)
        {

            XmlNode aNode = GetNodeByTagName(TagName);

            StringBuilder builder = new StringBuilder();
            while (aNode != null)
            {
                switch (aNode.NodeType)
                {
                    case XmlNodeType.Attribute:
                        builder.Insert(0, " » @" + aNode.Name);
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
                            builder.Insert(0, " » " + aNode.Attributes["name"].Value);
                        }
                        else
                        {
                            builder.Insert(0, " » " + aNode.Name);
                        }

                        aNode = aNode.ParentNode;
                        break;
                    case XmlNodeType.Document:
                        ErrorText = null;
                        return builder.ToString();
                    default:
                        ErrorText = "Only elements and attributes are supported";
                        return null;
                        //throw new ArgumentException("Only elements and attributes are supported");
                }
            }
            ErrorText = "Node was not in a document";
            return null;
        }



        public static bool SaveVersionConfigXML(out String ErrorText)
        {
            XmlNode aNode = GetNodeByTagName("LastEdited");
            if ((aNode != null) && (aNode.ParentNode != null) && ("ConfigurationInformation".Equals(aNode.ParentNode.Name)))
            {
                aNode.InnerText = RZITools.ConvertToUnixTime(DateTime.Now, out ErrorText).ToString();
                if (ErrorText != null)
                {
                    return false;
                }
            }
            try
            {
                CVersionConfigHelper.xmlDoc.Save(CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML);

                string finalSource = CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML;
                string finalDest = CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_CONFIG_XML;

                if (File.Exists(finalDest))
                {
                    File.Delete(finalDest);
                }

                RZITools.EncryptFile(finalSource, finalDest, CGlobVars.PASSWORD_TOKEN, out ErrorText);

                if (File.Exists(finalSource))
                {
                    File.Delete(finalSource);
                }

                ErrorText = null;
                return true;
            } catch (Exception ex)
            {
                ErrorText = ex.ToString();
                return false;
            }
            
        }

        public static XmlNode GetNodeByTagName(string sTagName)
        {
            XmlNodeList aNodeList = xmlDoc.GetElementsByTagName(sTagName);
            if (aNodeList.Count != 1)
            {
                return null;
            }

            return aNodeList[0];
        }

    }
}
