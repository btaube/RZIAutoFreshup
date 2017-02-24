using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UpdateModul.shared;

namespace UpdateModul
{
    class CBaseVersionLookupHelper
    {

        public static int CheckForNewBaseVersions(XmlNode CurrentVersionNode, String Guid, out String ErrorText)
        {
            CLog.Debug("Enter function 'CheckForNewBaseVersions'");
            CLog.Debug("Provided 'CurrentVersionNode': {0}", CurrentVersionNode);
            CLog.Debug("Provided 'Guid': {0}", Guid);

            ErrorText = null;
            XmlNode LeafNode = CVersionLookupHelper.GetLeaf(CurrentVersionNode);
            if (LeafNode != null)
            {
                List<XmlNode> NewerBaseVersionNodes = new List<XmlNode>();
                NewerBaseVersionNodes = CVersionLookupHelper.GetNewBaseVersionByMaxGuid(LeafNode.Name);
                if (NewerBaseVersionNodes.Count > 0)
                {
                    if (NewerBaseVersionNodes.Count == 1)
                    {
                        // There is a new base version available, show Info
                        CLog.Info("Showing version dialogue due to a single available base version.", NewerBaseVersionNodes.Count);
                        CVersionConfigHelper.SetInnerTextByPathAsString(CGlobVars.UDPATECHECKCONTENT_COMMON_LASTCHECKED, RZITools.ConvertToUnixTime(DateTime.Now, out ErrorText).ToString(), out ErrorText);
                        frmVersion newVersion = new frmVersion(frmVersion.EVersionType.MAIN, Guid, CVersionLookupHelper.GetNodeText(CurrentVersionNode, "DisplayName"),
                                                           NewerBaseVersionNodes[0], null, null, null, null);
                        newVersion.SetCorporateDesign(Guid, out ErrorText);
                        newVersion.ShowDialog();
                        return CReturnCodes.OK;
                    }
                    else if (NewerBaseVersionNodes.Count > 1)
                    {
                        XmlNode NewBaseVersion2Node = null;
                        XmlNode NewBaseVersion3Node = null;
                        XmlNode NewBaseVersion4Node = null;
                        XmlNode NewBaseVersion5Node = null;

                        String NewBaseVersion2 = "";
                        String NewBaseVersion3 = "";
                        String NewBaseVersion4 = "";
                        String NewBaseVersion5 = "";

                        foreach (var version in NewerBaseVersionNodes)
                        {
                            CLog.Debug("Processing newer base versions 'NewerBaseVersionNodes': {0}", version);
                            if ((NewBaseVersion2 == "") && (CVersionLookupHelper.GetNodeText(NewerBaseVersionNodes[0], "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                            {
                                NewBaseVersion2 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                                NewBaseVersion2Node = version;
                            }
                            else if ((NewBaseVersion3 == "") && (CVersionLookupHelper.GetNodeText(NewerBaseVersionNodes[0], "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                            {
                                NewBaseVersion3 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                                NewBaseVersion3Node = version;
                            }
                            else if ((NewBaseVersion4 == "") && (CVersionLookupHelper.GetNodeText(NewerBaseVersionNodes[0], "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                            {
                                NewBaseVersion4 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                                NewBaseVersion4Node = version;
                            }
                            else if ((NewBaseVersion5 == "") && (CVersionLookupHelper.GetNodeText(NewerBaseVersionNodes[0], "DisplayName") != CVersionLookupHelper.GetNodeText(version, "DisplayName")))
                            {
                                NewBaseVersion5 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                                NewBaseVersion5Node = version;
                            }
                            else if ((NewBaseVersion2 != "") && (NewBaseVersion3 != "") && (NewBaseVersion4 != "") && (NewBaseVersion5 != ""))
                            {
                                NewBaseVersion4 = "...";
                                NewBaseVersion4Node = null;
                                NewBaseVersion5 = CVersionLookupHelper.GetNodeText(version, "DisplayName");
                                NewBaseVersion5Node = version;
                            }
                        }

                        // There are several new base versions available, show Info to ask the user to get in contact with the support for further steps
                        CLog.Info("Showing version dialogue due to multiple available base versions.");
                        CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Common//LastChecked", RZITools.ConvertToUnixTime(DateTime.Now, out ErrorText).ToString(), out ErrorText);
                        frmVersion newVersion = new frmVersion(frmVersion.EVersionType.MULTI, Guid, CVersionLookupHelper.GetNodeText(CurrentVersionNode, "DisplayName"),
                                                       NewerBaseVersionNodes[0], NewBaseVersion2Node, NewBaseVersion3Node, NewBaseVersion4Node, NewBaseVersion5Node);

                        newVersion.SetCorporateDesign(Guid, out ErrorText);
                        newVersion.ShowDialog();
                        return CReturnCodes.OK;
                    }
                }
                else
                {
                    CLog.Debug("Returning 'CReturnCodes.WARNING_NO_NEW_BASE_VERSION' due to no newer base version: {0}", CReturnCodes.WARNING_NO_NEW_BASE_VERSION);
                    return CReturnCodes.WARNING_NO_NEW_BASE_VERSION;
                }

            }
            CLog.Debug("Returning 'CReturnCodes.INVALID_LOOKUP_XML_LEAF_NOT_FOUND' due to missing leaf: {0}", CReturnCodes.INVALID_LOOKUP_XML_LEAF_NOT_FOUND);
            return CReturnCodes.INVALID_LOOKUP_XML_LEAF_NOT_FOUND;
        }

    }
}
