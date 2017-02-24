using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace UpdateModul
{
    class CSubVersionLookupHelper
    {

        public static int CheckForNewSubVersions(XmlNode CurrentVersionNode, String Guid, out String ErrorText)
        {
            CLog.Debug("Enter function 'CheckForNewBaseVersions'");
            CLog.Debug("Provided 'CurrentVersionNode': {0}", CurrentVersionNode);
            CLog.Debug("Provided 'Guid': {0}", Guid);

            ErrorText = null;
            List<XmlNode> NewerVersionNodes = new List<XmlNode>();
            CVersionLookupHelper.GetAllPhysicalNodes(CurrentVersionNode, NewerVersionNodes);

            if (NewerVersionNodes.Count == 0)
            {
                // No newer version available
                CLog.Info("Showing info dialogue that the current version is the actual version.");
                CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Common//LastChecked", RZITools.ConvertToUnixTime(DateTime.Now, out ErrorText).ToString(), out ErrorText);
                frmInfo.ShowInfo(CVersionLookupHelper.GetNodeText(CurrentVersionNode, "DisplayName"));
                return CReturnCodes.OK;
            }
            else
            {
                // There is at least 1 newer version
                // If multiple versions share a minimumVersion, just keep the highest one and remove the other versions.
                string minVer = string.Empty;
                for (int iDel = NewerVersionNodes.Count - 1; iDel >= 0; iDel--)
                {
                    if (string.IsNullOrEmpty(minVer))
                    {
                        minVer = NewerVersionNodes[iDel].SelectSingleNode("MinimumVersion").InnerText;
                    }
                    else
                    {
                        string comp = NewerVersionNodes[iDel].SelectSingleNode("MinimumVersion").InnerText;
                        if (!string.IsNullOrEmpty(comp))
                        {
                            if (comp == minVer)
                            {
                                NewerVersionNodes.RemoveAt(iDel);
                            }
                            else
                            {
                                minVer = comp;
                            }
                        }
                    }
                }


                // Check dependencies of each newer version (i.e. if we have 7.0 and we find 7.4, first we have to check if 7.4 needs at least 7.3 to be installed.
                // In this case we have to provide 7.3 as the next version to install.. and so on.
                XmlNode NewVersionNode = null;
                XmlNode NewVersion2Node = null;
                XmlNode NewVersion3Node = null;
                XmlNode NewVersion4Node = null;
                XmlNode NewVersion5Node = null;

                String NewVersion2 = "";
                String NewVersion3 = "";
                String NewVersion4 = "";
                String NewVersion5 = "";

                foreach (var version in NewerVersionNodes)
                {
                    CLog.Debug("Processing 'version' of 'NewerVersionNodes': {0}", version);
                    var minimumVersionNode = version.SelectSingleNode("MinimumVersion");
                    if (minimumVersionNode != null)
                    {
                        CLog.Debug("Found required version constraint: {0}", minimumVersionNode);
                        if (!String.IsNullOrEmpty(minimumVersionNode.InnerText))
                        {
                            // Find next required version

                            var first = NewerVersionNodes.FirstOrDefault(n => n.Name == minimumVersionNode.InnerText);
                            if (first != null)
                            {
                                NewVersionNode = first;
                                break;
                            }
                            // else -> required version is older than current version ... continue
                        }

                    }

                    NewVersionNode = version;
                }

                // Collect must-have versions
                foreach (var version in NewerVersionNodes)
                {
                    CLog.Debug("Processing required version nodes: {0}", version);
                    if ((NewVersion2 == "") && (!String.IsNullOrEmpty(CVersionLookupHelper.GetNodeText(version, "MinimumVersion"))) && (CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                    {
                        NewVersion2Node = CVersionLookupHelper.GetNodeByTagName(CVersionLookupHelper.GetNodeText(version, "MinimumVersion"));
                        NewVersion2 = CVersionLookupHelper.GetNodeText(NewVersion2Node, "DisplayName");
                        if (NewVersion2 == CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName"))
                        {
                            NewVersion2Node = null;
                            NewVersion2 = "";
                        }
                    }
                    else if ((NewVersion3 == "") && (!String.IsNullOrEmpty(CVersionLookupHelper.GetNodeText(version, "MinimumVersion"))) && (CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                    {
                        NewVersion3Node = CVersionLookupHelper.GetNodeByTagName(CVersionLookupHelper.GetNodeText(version, "MinimumVersion"));
                        NewVersion3 = CVersionLookupHelper.GetNodeText(NewVersion3Node, "DisplayName");
                        if (NewVersion3 == CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName"))
                        {
                            NewVersion3Node = null;
                            NewVersion3 = "";
                        }
                    }
                    else if ((NewVersion4 == "") && (!String.IsNullOrEmpty(CVersionLookupHelper.GetNodeText(version, "MinimumVersion"))) && (CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                    {
                        NewVersion4Node = CVersionLookupHelper.GetNodeByTagName(CVersionLookupHelper.GetNodeText(version, "MinimumVersion"));
                        NewVersion4 = CVersionLookupHelper.GetNodeText(NewVersion4Node, "DisplayName");
                        if (NewVersion4 == CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName"))
                        {
                            NewVersion4Node = null;
                            NewVersion4 = "";
                        }
                    }
                    else if ((NewVersion5 == "") && (!String.IsNullOrEmpty(CVersionLookupHelper.GetNodeText(version, "MinimumVersion"))) && (CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                    {
                        NewVersion5Node = CVersionLookupHelper.GetNodeByTagName(CVersionLookupHelper.GetNodeText(version, "MinimumVersion"));
                        NewVersion5 = CVersionLookupHelper.GetNodeText(NewVersion5Node, "DisplayName");
                        if (NewVersion5 == CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName"))
                        {
                            NewVersion5Node = null;
                            NewVersion5 = "";
                        }
                    }
                    else if ((NewVersion2 != "") && (NewVersion3 != "") && (NewVersion4 != "") && (NewVersion5 != ""))
                    {
                        NewVersion4 = "...";
                        NewVersion4Node = null;

                        XmlNode BackUpVersion5Node = NewVersion5Node;
                        String BackUpNewVersion5 = NewVersion5;

                        NewVersion5Node = CVersionLookupHelper.GetNodeByTagName(CVersionLookupHelper.GetNodeText(version, "MinimumVersion"));
                        NewVersion5 = CVersionLookupHelper.GetNodeText(NewVersion5Node, "DisplayName");
                        if (NewVersion5 == CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName"))
                        {
                            NewVersion5Node = BackUpVersion5Node;
                            NewVersion5 = BackUpNewVersion5;
                        }
                        else
                        {
                            NewVersion4 = "...";
                        }
                    }
                }

                // Collect additional versions
                CLog.Debug("Collection additional new versions.");
                NewerVersionNodes = new List<XmlNode>();
                if (NewVersion5Node != null)
                {
                    CVersionLookupHelper.GetAllPhysicalNodes(NewVersion5Node, NewerVersionNodes);
                }
                else if (NewVersion4Node != null)
                {
                    CVersionLookupHelper.GetAllPhysicalNodes(NewVersion4Node, NewerVersionNodes);
                }
                else if (NewVersion3Node != null)
                {
                    CVersionLookupHelper.GetAllPhysicalNodes(NewVersion3Node, NewerVersionNodes);
                }
                else if (NewVersion2Node != null)
                {
                    CVersionLookupHelper.GetAllPhysicalNodes(NewVersion2Node, NewerVersionNodes);
                }
                else
                {
                    CVersionLookupHelper.GetAllPhysicalNodes(NewVersionNode, NewerVersionNodes);
                }

                // Set up version objects for dialogue
                if (NewerVersionNodes.Count != 0)
                {
                    // There is at least 1 newer version
                    // If multiple versions share a minimumVersion, just keep the highest one and remove the other versions.
                    string minVerFillUp = string.Empty;
                    for (int iDel = NewerVersionNodes.Count - 1; iDel >= 0; iDel--)
                    {
                        if (string.IsNullOrEmpty(minVerFillUp))
                        {
                            minVerFillUp = NewerVersionNodes[iDel].SelectSingleNode("MinimumVersion").InnerText;
                        }
                        else
                        {
                            string comp = NewerVersionNodes[iDel].SelectSingleNode("MinimumVersion").InnerText;
                            if (!string.IsNullOrEmpty(comp))
                            {
                                if (comp == minVerFillUp)
                                {
                                    NewerVersionNodes.RemoveAt(iDel);
                                }
                                else
                                {
                                    minVerFillUp = comp;
                                }
                            }
                        }
                    }

                    foreach (var version in NewerVersionNodes)
                    {
                        CLog.Debug("Processing additional newer version nodes: {0}", version);
                        if ((NewVersion2 == "") && (CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                        {
                            NewVersion2 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                            NewVersion2Node = version;
                        }
                        else if ((NewVersion3 == "") && (CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                        {
                            NewVersion3 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                            NewVersion3Node = version;
                        }
                        else if ((NewVersion4 == "") && (CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                        {
                            NewVersion4 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                            NewVersion4Node = version;
                        }
                        else if ((NewVersion5 == "") && (CVersionLookupHelper.GetNodeText(NewVersionNode, "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                        {
                            NewVersion5 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                            NewVersion5Node = version;
                        }
                        else if ((NewVersion2 != "") && (NewVersion3 != "") && (NewVersion4 != "") && (NewVersion5 != ""))
                        {
                            NewVersion4 = "...";
                            NewVersion4Node = null;

                            NewVersion5 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                            NewVersion5Node = version;
                        }
                    }
                }
                // Show new version dialogue
                CLog.Info("Showing version dialogue due to available sub versions.");
                CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Common//LastChecked", RZITools.ConvertToUnixTime(DateTime.Now, out ErrorText).ToString(), out ErrorText);
                frmVersion newVersion = new frmVersion(frmVersion.EVersionType.SUB, Guid, CVersionLookupHelper.GetNodeText(CurrentVersionNode, "DisplayName"),
                                                       NewVersionNode, NewVersion2Node, NewVersion3Node, NewVersion4Node, NewVersion5Node);
                newVersion.SetCorporateDesign(Guid, out ErrorText);
                newVersion.ShowDialog();

                return CReturnCodes.OK;
            }
        }

    }
}
