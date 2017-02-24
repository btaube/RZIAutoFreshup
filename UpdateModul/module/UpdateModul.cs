using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using UpdateModul.config.gui;
using UpdateModul.shared;

namespace UpdateModul
{
    /// <summary>
    /// Interface for update modul
    /// </summary>
    /// 

    public class CUMWrapper
    {
        private string _LanguageCode = "";
        private string _Guid = "";
        private string _InitDir = "";
        private String[] _Params;


        /// <summary>
        /// Function for the UM wrapper, called by C++ applications
        /// </summary>
        /// <param name="LanguageCode">Language code for message box (en-US / de-DE)</param>
        /// <param name="Guid">GUID of the current version</param>
        /// <param name="InitDir">Root dir for further path findings, i.e. for work dir</param>
        /// <param name="Params">String to provide further parameters like settings</param>
        /// 
        public CUMWrapper(string LanguageCode, string Guid, string InitDir, string Params)
        {
            _LanguageCode = LanguageCode;
            _Guid = Guid;
            _InitDir = InitDir;
            _Params = Params.Split(',');
        }

        /// <summary>
        /// Function for the UM wrapper, called by C++ applications
        /// </summary>
        /// <param name="LanguageCode">Language code for message box (en-US / de-DE)</param>
        /// <param name="Guid">GUID of the current version</param>
        /// <param name="InitDir">Root dir for further path findings, i.e. for work dir</param>
        /// 
        public CUMWrapper(string LanguageCode, string Guid, string InitDir)
        {
            _LanguageCode = LanguageCode;
            _Guid = Guid;
            _InitDir = InitDir;
        }

        /// <summary>
        /// Function for the UM wrapper, called by C++ applications
        /// </summary>
        /// <param name="LanguageCode">Language code for message box (en-US / de-DE)</param>
        /// <param name="Guid">GUID of the current version</param>
        /// 
        public CUMWrapper(string LanguageCode, string Guid)
        {
            _LanguageCode = LanguageCode;
            _Guid = Guid;
        }

        /// <summary>
        /// Check for new updates
        /// </summary>
        /// <param name="LanguageCode">Language code for message box (en-US / de-DE)</param>
        /// <param name="Guid">GUID of the current version</param>
        /// <param name="InitDir">Root dir for further path findings, i.e. for work dir</param>
        /// <param name="Params">String to provide further parameters like settings</param>
        /// <returns>0 = no error occured</returns>
        /// 
        public int CheckForUpdates()
        {
            CUpdateModul um = new CUpdateModul();
            return um.CheckForUpdates(_LanguageCode, _Guid, _InitDir, _Params);
            //System.Windows.Forms.MessageBox.Show(_message, "System.Windows.Forms.MessageBox");
        }

        /// <summary>
        /// Opens configuration dialogue
        /// </summary>
        /// <param name="LanguageCode">Language code for message box (en-US / de-DE)</param>
        /// <param name="Guid">GUID of the current version</param>
        /// <param name="InitDir">Root dir for further path findings, i.e. for work dir</param>
        /// <param name="Params">String to provide further parameters like settings</param>
        /// <returns>0 = no error occured</returns>
        /// 
        public int OpenConfig()
        {
            CUpdateModul um = new CUpdateModul();
            return um.OpenConfig(_LanguageCode, _Guid, _InitDir, _Params);
            //System.Windows.Forms.MessageBox.Show(_message, "System.Windows.Forms.MessageBox");
        }
    }



    public interface IUpdateModul
    {
        /// <summary>
        /// Check for new updates
        /// </summary>
        /// <param name="LanguageCode">Language code for message box (en-US / de-DE)</param>
        /// <param name="Guid">GUID of the current version</param>
        /// <param name="InitDir">Root dir for further path findings, i.e. for work dir</param>
        /// <param name="Params">String to provide further parameters like settings</param>
        /// <returns>0 = no error occured</returns>
        /// 
        int CheckForUpdates(String LanguageCode, String Guid, String InitDir, String[] Params);

        /// <summary>
        /// Opens configuration dialogue
        /// </summary>
        /// <param name="LanguageCode">Language code for message box (en-US / de-DE)</param>
        /// <param name="Guid">GUID of the current version</param>
        /// <param name="InitDir">Root dir for further path findings, i.e. for work dir</param>
        /// <param name="Params">String to provide further parameters like settings</param>
        /// <returns>0 = no error occured</returns>
        /// 
        int OpenConfig(String LanguageCode, String Guid, String InitDir, String[] Params);
    }


    /// <summary>
    /// Implementation of interface for update modul
    /// </summary>
    public class CUpdateModul : IUpdateModul
    {
        /// <summary>
        /// Checks if a specific param value exists in the array and returns its value.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="paramName"></param>
        /// <returns>Value or null</returns>
        private static string GetParamValue(String[] Params, string paramName)
        {
            CLog.Debug("Entered function 'GetParamValue'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));
            CLog.Debug("Provided 'paramName': {0}", paramName);

            String SearchString = ("-" + paramName + "=").ToLower();

            foreach (var param in Params)
            {
                CLog.Debug("Processed 'param': {0}", param);
                if (param.ToLower().StartsWith(SearchString))
                {
                    CLog.Debug("Found 'param': {0}", param);
                    CLog.Debug("Return value for function 'GetParamValue': {0}", param.Substring(SearchString.Length).Replace("\"", ""));
                    return param.Substring(SearchString.Length).Replace("\"", "");
                }

            }

            CLog.Debug("Return value for function 'GetParamValue': {0}", "<null>");
            return null;
        }


        /// <summary>
        /// Sets the language of the UM.
        /// </summary>
        /// <param name="Params"></param>
        private static void SetLanguage(String[] Params)
        {
            CLog.Debug("Entered function 'SetLanguage'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));
            foreach (string param in Params)
            {
                CLog.Debug("Processed 'param': {0}", param);
                if (param.ToLower().Contains("-lang="))
                {
                    CLog.Debug("Found param '-lang': {0}", param);
                    string sLang = param;
                    if (sLang.Contains("\""))
                    {
                        sLang = sLang.Replace("\"", "");
                    }
                    sLang = sLang.Substring(6);

                    switch (sLang.ToLower())
                    {
                        case "de":
                            CLog.Debug("Set culture to: {0}", "de-DE");
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                            break;
                        case "de-de":
                            CLog.Debug("Set culture to: {0}", "de-DE");
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                            break;
                        case "en":
                            CLog.Debug("Set culture to: {0}", "en-GB");
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
                            break;
                        case "en-gb":
                            CLog.Debug("Set culture to: {0}", "en-GB");
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
                            break;
                        case "en-us":
                            CLog.Debug("Set culture to: {0}", "en-GB");
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
                            break;
                        default:
                            CLog.Debug("Set default culture to: {0}", "de-DE");
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a version naming pattern (freshup / version) has been provided.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnCodes</returns>
        private static void SetVersionNamingPattern(String[] Params)
        {
            CLog.Debug("Entered function 'SetVersionNamingPattern'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));
            foreach (string param in Params)
            {
                CLog.Debug("Processed 'param': {0}", param);
                if (param.ToLower().Contains("-versionname="))
                {
                    CLog.Debug("Found param '-versionname': {0}", param);
                    string VersionPattern = param;
                    if (VersionPattern.Contains("\""))
                    {
                        VersionPattern = VersionPattern.Replace("\"", "");
                    }
                    VersionPattern = VersionPattern.Substring(13);
                    if ("1".Equals(VersionPattern))
                    {
                        CGlobVars.VersionDescriptionID = 1;
                    }
                    else
                    {
                        CGlobVars.VersionDescriptionID = 2;
                    }

                }
            }
        }

        /// <summary>
        /// Checks if a silent parameter has been provided.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnCodes</returns>
        private static void SetSilentMode(String[] Params)
        {
            CLog.Debug("Entered function 'SetSilentMode'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));
            foreach (string param in Params)
            {
                CLog.Debug("Processed 'param': {0}", param);
                if (param.ToLower().Contains("-silent="))
                {
                    CLog.Debug("Found param '-silent': {0}", param);
                    string SilentPattern = param;
                    if (SilentPattern.Contains("\""))
                    {
                        SilentPattern = SilentPattern.Replace("\"", "");
                    }
                    SilentPattern = SilentPattern.Substring(8);
                    if (SilentPattern.ToLower().Contains("error"))
                    {
                        CGlobVars.silentModeError = true;
                    }
                    if (SilentPattern.ToLower().Contains("noversion"))
                    {
                        CGlobVars.silentModeNoNewVersion = true;
                    }

                }
            }
        }


        /// <summary>
        /// Checks if an ignoreInterval parameter has been provided.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnCodes</returns>
        private static void SetIgnoreInterval(String[] Params)
        {
            CLog.Debug("Entered function 'SetIgnoreInterval'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));
            foreach (string param in Params)
            {
                CLog.Debug("Processed 'param': {0}", param);
                if (param.ToLower().Contains("-ignoreinterval="))
                {
                    CLog.Debug("Found param '-ignoreinterval': {0}", param);
                    string IgnorePattern = param;
                    if (IgnorePattern.Contains("\""))
                    {
                        IgnorePattern = IgnorePattern.Replace("\"", "");
                    }
                    IgnorePattern = IgnorePattern.Substring(16);
                    if (IgnorePattern.ToLower().Contains("true"))
                    {
                        CGlobVars.ignoreCheckInterval = true;
                    }

                }
            }
        }

        /// <summary>
        /// Checks if a timeout parameter has been provided.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnCodes</returns>
        private static void SetTimeout(String[] Params)
        {
            CLog.Debug("Entered function 'SetTimeout'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));
            foreach (string param in Params)
            {
                CLog.Debug("Processed 'param': {0}", param);
                if (param.ToLower().Contains("-timeout="))
                {
                    CLog.Debug("Found param '-timeout': {0}", param);
                    string TimeoutPattern = param;
                    if (TimeoutPattern.Contains("\""))
                    {
                        TimeoutPattern = TimeoutPattern.Replace("\"", "");
                    }
                    TimeoutPattern = TimeoutPattern.Substring(9);
                    try
                    {
                        CGlobVars.timeout = Convert.ToInt32(TimeoutPattern);
                    }
                    catch (Exception ex)
                    {
                        CGlobVars.timeout = 30000;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a default or custom password has been provided and, if so, grants admin status.
        /// Redesigned: Check custom password -> check if default password has been provided -> check if super password has been provided
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnCodes</returns>
        private static int CheckIfIsAdmin(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'CheckIfIsAdmin'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));
            // search for admin token first
            bool bIsAdmin = false;
            String ParamValue = GetParamValue(Params, "pass");
            if (ParamValue == null)
            {
                ParamValue = string.Empty;
            }

            // Check if custom password has been set:
            CLog.Debug("Checking if custom password has been set.");
            CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
            if (ErrorText != null)
                return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;

            XmlNode aPWNode = CVersionConfigHelper.GetNodeByTagName("Password");
            string sPW = "";
            if (aPWNode != null)
            {
                sPW = RZITools.Decrypt(aPWNode.InnerText, out ErrorText);
                if (ErrorText != null)
                    return CReturnCodes.DECRYPT_PASSWORD_FAILED;


                if (sPW == "")
                {
                    if ((ParamValue.Trim().Length > 0) && ((ParamValue.Equals(CGlobVars.DEFAULT_PASSWORD)) || (ParamValue.Equals(CGlobVars.SUPER_ADMIN_PASSWORD))))
                    {
                        CLog.Debug("Default password is provided and correct, granting Admin status.");
                        bIsAdmin = true;
                    }
                    else
                    {
                        CLog.Debug("Default password is provided and not correct.");
                    }
                }
                else if ((ParamValue.Trim().Length > 0) && (ParamValue.Equals(sPW)))
                {
                    CLog.Debug("Custom password is provided and correct, granting Admin status.");
                    bIsAdmin = true;
                }
                else
                {
                    CLog.Debug("Provided password is not correct.");
                    return CReturnCodes.ADMIN_PASSWORD_WRONG;
                }

            }

            if (!bIsAdmin)
            {
                // Check if default password has been provided
                if (!String.IsNullOrEmpty(ParamValue))
                {
                    if (CGlobVars.DEFAULT_PASSWORD.Equals(ParamValue))
                    {
                        CLog.Debug("Default password is provided and correct, granting Admin status.");
                        bIsAdmin = true;
                    }
                    else if (CGlobVars.SUPER_ADMIN_PASSWORD.Equals(ParamValue))
                    {
                        CLog.Debug("Super password is provided and correct, granting Admin status.");
                        bIsAdmin = true;
                    }
                    else
                    {
                        ErrorText = null;
                        return CReturnCodes.ADMIN_PASSWORD_WRONG;
                    }

                }
                else
                {
                    CLog.Debug("Default Password has not been provided.");
                    ErrorText = null;
                    return CReturnCodes.ADMIN_PASSWORD_EMPTY;
                }
            }

            ErrorText = null;
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets new password in configuration file.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnCode</returns>
        private static int AdminSetNewPW(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetNewPW'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "setpw");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    CLog.Debug("VersionConfigXML has been successfully loaded.");
                    if (CVersionConfigHelper.SetInnerTextByPathAsPW("UpdateCheckContent//Security//Password", ParamValue, out ErrorText))
                    {
                        CLog.Debug("Admin password has been successfully saved.");
                        return CReturnCodes.OK;
                    }
                    else
                    {
                        CLog.Debug("Admin password has not been successfully saved.");
                        return CReturnCodes.SET_PASSWORD_FAILED;
                    }
                }
                else
                {
                    CLog.Debug("VersionConfigXML has not been successfully loaded.");
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
                }
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets new repository url for VersionLookup.xml
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnCode</returns>
        private static int AdminSetNewLookupURL(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetNewLookupURL'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "seturl");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    if (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Repository//LookupURL", ParamValue, out ErrorText))
                    {
                        CLog.Debug("Lookup url has been successfully saved.");
                        return CReturnCodes.OK;
                    }
                    else
                    {
                        CLog.Debug("Lookup url has been successfully saved.");
                        return CReturnCodes.SET_LOOKUP_URL_FAILED;
                    }
                }
                else
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets interval between 2 update checks
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        private static int AdminSetNewCheckDaysInterval(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetNewCheckDaysInterval'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "setdays");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    CLog.Debug("VersionConfigXML has been successfully loaded.");
                    if (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Repository//CheckDays", ParamValue, out ErrorText))
                    {
                        CLog.Debug("CheckDays has been successfully saved.");
                        return CReturnCodes.OK;
                    }
                    else
                    {
                        CLog.Debug("CheckDays has not been successfully saved.");
                        return CReturnCodes.SET_CHECK_DAYS_FAILED;
                    }
                }
                else
                {
                    CLog.Debug("VersionConfigXML has not been successfully loaded.");
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
                }
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets proxy usage in configuration file.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        private static int AdminSetProxyProxyUse(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetProxyProxyUse'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "setproxyuse");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    CLog.Debug("VersionConfigXML has been successfully loaded.");
                    bool bTrue = ParamValue == "1" || ParamValue.ToLower() == "true" || ParamValue.ToLower() == "wahr";

                    if (CVersionConfigHelper.SetInnerTextByPathAsBool("UpdateCheckContent//Proxy//UseProxy", bTrue, out ErrorText))
                    {
                        CLog.Debug("UseProxy has been successfully saved.");
                        return CReturnCodes.OK;
                    }

                    else
                    {
                        CLog.Debug("UseProxy has not been successfully saved.");
                        return CReturnCodes.SET_USE_PROXY_FAILED;
                    }

                }
                else
                {
                    CLog.Debug("VersionConfigXML has not been successfully loaded.");
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
                }
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets proxy server in configuration file.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        private static int AdminSetProxyServer(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetProxyServer'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "setproxyserver");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    CLog.Debug("VersionConfigXML has been successfully loaded.");
                    if (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Proxy//ProxyServer", ParamValue, out ErrorText))
                    {
                        CLog.Debug("ProxyServer has been successfully saved.");
                        return CReturnCodes.OK;
                    }

                    else
                    {
                        CLog.Debug("ProxyServer has not been successfully saved.");
                        return CReturnCodes.SET_USE_PROXY_SERVER_FAILED;
                    }

                }
                else
                {
                    CLog.Debug("VersionConfigXML has not been successfully loaded.");
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
                }
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets proxy port in configuration file.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        private static int AdminSetProxyPort(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetProxyPort'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "setproxyport");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    CLog.Debug("VersionConfigXML has been successfully loaded.");
                    if (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Proxy//ProxyPort", ParamValue, out ErrorText))
                    {
                        CLog.Debug("ProxyPort has been successfully saved.");
                        return CReturnCodes.OK;
                    }
                    else
                    {
                        CLog.Debug("ProxyPort has not been successfully saved.");
                        return CReturnCodes.SET_USE_PROXY_PORT_FAILED;
                    }
                }
                else
                {
                    CLog.Debug("VersionConfigXML has not been successfully loaded.");
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
                }
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets proxy usage of default credentials in configuration file.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        private static int AdminSetProxyUseDefCred(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetProxyUseDefCred'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "setproxyusedefcred");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    CLog.Debug("VersionConfigXML has been successfully loaded.");
                    bool bTrue = ParamValue == "1" || ParamValue.ToLower() == "true" || ParamValue.ToLower() == "wahr";

                    if (CVersionConfigHelper.SetInnerTextByPathAsBool("UpdateCheckContent//Proxy//UseDefCred", bTrue, out ErrorText))
                    {
                        CLog.Debug("UseDefCred has been successfully saved.");
                        return CReturnCodes.OK;
                    }
                    else
                    {
                        CLog.Debug("UseDefCred has not been successfully saved.");
                        return CReturnCodes.SET_USE_PROXY_USE_DEF_CRED_FAILED;
                    }
                }
                else
                {
                    CLog.Debug("VersionConfigXML has not been successfully loaded.");
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
                }
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets proxy user in configuration file.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        private static int AdminSetProxUser(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetProxUser'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "setproxyuser");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    CLog.Debug("VersionConfigXML has been successfully loaded.");
                    if (CVersionConfigHelper.SetInnerTextByPathAsString("UpdateCheckContent//Proxy//ProxyUser", ParamValue, out ErrorText))
                    {
                        CLog.Debug("ProxyUser has been successfully saved.");
                        return CReturnCodes.OK;
                    }
                    else
                    {
                        CLog.Debug("ProxyUser has not been successfully saved.");
                        return CReturnCodes.SET_USE_PROXY_USER_FAILED;
                    }
                }
                else
                {
                    CLog.Debug("VersionConfigXML has not been successfully loaded.");
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
                }
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets proxy passw in configuration file.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        private static int AdminSetProxPassw(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetProxPassw'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "setproxypassw");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    CLog.Debug("VersionConfigXML has been successfully loaded.");
                    if (CVersionConfigHelper.SetInnerTextByPathAsPW("UpdateCheckContent//Proxy//ProxyPassw", ParamValue, out ErrorText))
                    {
                        CLog.Debug("ProxyPassw has been successfully saved.");
                        return CReturnCodes.OK;
                    }
                    else
                    {
                        CLog.Debug("ProxyPassw has not been successfully saved.");
                        return CReturnCodes.SET_USE_PROXY_PASSWORD_FAILED;
                    }
                }
                else
                {
                    CLog.Debug("VersionConfigXML has not been successfully loaded.");
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
                }
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Sets proxy bylass for lan connections in configuration file.
        /// </summary>
        /// <param name="Params"></param>
        /// <param name="ErrorText"></param>
        /// <returns>ReturnValue</returns>
        private static int AdminSetProxBypassOnLan(String[] Params, out String ErrorText)
        {
            CLog.Debug("Entered function 'AdminSetNewLookupURL'");
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            ErrorText = null;
            String ParamValue = GetParamValue(Params, "setproxybypassonlan");
            if (ParamValue != null)
            {
                CLog.Debug("Parameter has been found.");
                int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
                if (retVal == CReturnCodes.OK)
                {
                    CLog.Debug("VersionConfigXML has been successfully loaded.");
                    bool bTrue = ParamValue == "1" || ParamValue.ToLower() == "true" || ParamValue.ToLower() == "wahr";

                    if (CVersionConfigHelper.SetInnerTextByPathAsBool("UpdateCheckContent//Proxy//BypassOnLan", bTrue, out ErrorText))
                    {
                        CLog.Debug("BypassOnLan has been successfully saved.");
                        return CReturnCodes.OK;
                    }
                    else
                    {
                        CLog.Debug("BypassOnLan has not been successfully saved.");
                        return CReturnCodes.SET_USE_PROXY_BYPASS_ON_LAN_FAILED;
                    }
                }
                else
                {
                    CLog.Debug("VersionConfigXML has not been successfully loaded.");
                    return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
                }
            }
            return CReturnCodes.OK;
        }


        /// <summary>
        /// Opens configuration dialogue.
        /// </summary>
        /// <param name="LanguageCode"></param>
        /// <param name="Guid"></param>
        /// <param name="InitDir"></param>
        /// <param name="Params"></param>
        /// <returns>ReturnCode</returns>
        public int OpenConfig(String LanguageCode, String Guid, String InitDir, String[] Params)
        {
            if ((Params == null) || (Params.Count() <= 0))
            {
                Params = new List<string> { "," }.ToArray();
            }

            // Reset parameters to default
            ResetParametersToDefault();

            //
            CGlobVars.CurrentGUID = Guid;

            //
            String ErrorText = null;
            bool bIsAdmin = false;

            //Set default language
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");

            // Set application language
            try
            {
                Application.CurrentCulture = new System.Globalization.CultureInfo(LanguageCode);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(LanguageCode);
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(LanguageCode);
            }
            catch (Exception exc)
            {
                frmError.ShowError(exc.ToString());
                return CReturnCodes.SET_LANGUAGE_FAILED;
            }



            // Set InitDir to application dir if it has not been provided by application yet
            InitDir = SetInitDir(InitDir);


            // Set wrk directory
            if (InitDir.EndsWith(@"\"))
            {
                CGlobVars.wrkDir = InitDir + @"wrk\";
            }
            else
            {
                CGlobVars.wrkDir = InitDir + @"\wrk\";
            }


            // Create wrk directory if not exists
            if (!Directory.Exists(CGlobVars.wrkDir))
            {
                try
                {
                    Directory.CreateDirectory(CGlobVars.wrkDir);
                }
                catch (Exception ex)
                {
                    return CReturnCodes.CANNOT_CREATE_WORK_DIRECTORY;
                }
            }

            // Init Logging
            CLog.Init(CGlobVars.wrkDir + CGlobVars.LOG_FILE_NAME, "Info", 10 * 1024 /*10 MB*/);
            CLog.Info("######################################################");

            // Check files
            CheckFiles();

            // Load Current Version
            // Decrypt file and load xml, if a file already exists.
            // So we can try to get our current version and extract the corporate identity icon for the forms.
            CLog.Info("Loading local VersionLookupXML.");
            int retValue = CVersionLookupHelper.DecryptAndLoadVersionLookupXML(CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML, false, out ErrorText);
            if (retValue != CReturnCodes.OK)
            {
                // If no VersionLookup.xml could have been loaded, its not critical
                // In this case it means that no icon can be loaded for the forms
                CLog.Warning("Could not load VersionLookupXML.");
            }

            // Load config gile
            CLog.Info("Loading VersionConfigXML.");
            int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
            if (retVal != CReturnCodes.OK)
            {
                CLog.Error("Could not load VersionConfigXML.");
                CLog.Error("Error message: {0}", ErrorText);
                return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
            }

            //

            // Set log level
            bool useDefaultLoglevel = false;
            CGlobVars.logLevel = CVersionConfigHelper.GetInnerTextByPathAsString("UpdateCheckContent//Common//LogLevel", out ErrorText);
            if (ErrorText != null)
            {
                useDefaultLoglevel = true;
                CGlobVars.logLevel = "DEBUG";
            }
            if (useDefaultLoglevel)
            {
                CLog.Init(CGlobVars.wrkDir + CGlobVars.LOG_FILE_NAME, "DEBUG", 10 * 1024 /*10 MB*/);
                CLog.Info("Using default log level: {0}", "DEBUG");
            }
            else
            {
                CLog.Init(CGlobVars.wrkDir + CGlobVars.LOG_FILE_NAME, CGlobVars.logLevel, 10 * 1024 /*10 MB*/);
                CLog.Info("Using log level: {0}", CGlobVars.logLevel);
            }
            CLog.Info("Enter function 'OpenConfig'");
            CLog.Debug("Provided 'LanguageCode': {0}", LanguageCode);
            CLog.Debug("Provided 'Guid': {0}", Guid);
            CLog.Debug("Provided 'InitDir': {0}", InitDir);
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));

            CLog.Info("InitDir: {0}", InitDir);
            CLog.Info("Work directory: {0}", CGlobVars.wrkDir);

            //// Load Current Version
            //CLog.Info("Loading VersionLookupXML.");
            ////if (!CVersionLookupHelper.LoadVersionLookupXML(out ErrorText))
            //if (!CVersionLookupHelper.LoadVersionLookupXML(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML, false))
            //{
            //    // If no VersionLookup.xml could have been loaded, its not critical
            //    // In this case it means that no icon can be loaded for the form
            //    CLog.Warning("Could not load VersionLookupXML.");
            //    CLog.Warning("Error message: {0}", ErrorText);
            //}

            // Process parameters
            if ((Params != null) && (Params.Length > 1))
            {
                CLog.Info("Processing provided parameters.");

                //Check if another language setting has been provided by argument and set it
                CLog.Info("Checking for language parameter.");
                SetLanguage(Params);
                // Set application language
                try
                {
                    Application.CurrentCulture = new System.Globalization.CultureInfo(LanguageCode);
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(LanguageCode);
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(LanguageCode);
                }
                catch (Exception exc)
                {
                    frmError.ShowError(exc.ToString());
                    return CReturnCodes.SET_LANGUAGE_FAILED;
                }

                CGlobVars.CurrentLanguage = LanguageCode;

                // Check if admin password is provided
                CLog.Info("Checking for admin password parameter.");
                int iReturnCode = CheckIfIsAdmin(Params, out ErrorText);
                // If no password or wrong password has been provided, exit with return code
                if ((iReturnCode == CReturnCodes.ADMIN_PASSWORD_WRONG) || (iReturnCode == CReturnCodes.ADMIN_PASSWORD_EMPTY))
                {
                    CLog.Info("No admin status has been granted, showing login dialogue.");
                    // no admin parameter provided, so open password prompt
                    frmPassw passwFrm = new frmPassw();
                    passwFrm.SetCorporateDesign(Guid, out ErrorText);
                    // check if entered pw is correct and open form
                    if (passwFrm.ShowDialog() == DialogResult.OK)
                    {
                        bIsAdmin = true;
                        CLog.Info("Entered correct admin password, showing config dialogue.");
                    }
                    else
                    {
                        CLog.Info("Password dialogue has been closed by user.");
                        return CReturnCodes.PASSWORD_INPUT_CANCELLED;
                    }
                    //return iReturnCode;
                }
                else
                {
                    // Admin status granted, setting values if provided
                    bIsAdmin = true;
                }

            }
            else
            {
                CLog.Info("No parameters have been provided.");
            }

            // Set options and open config dialogue when admin status granted
            if (bIsAdmin)
            {
                // Set options
                if ((Params != null) && (Params.Length > 1))
                {
                    // Init return value
                    int returnValue = CReturnCodes.OK;

                    // Set new custom admin password if provided
                    CLog.Info("Checking for new password parameter.");
                    returnValue = AdminSetNewPW(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                    CLog.Info("Checking for new lookup url parameter.");
                    returnValue = AdminSetNewLookupURL(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                    CLog.Info("Checking for new checkdays parameter.");
                    returnValue = AdminSetNewCheckDaysInterval(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                    CLog.Info("Checking for new useproxy parameter.");
                    returnValue = AdminSetProxyProxyUse(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                    CLog.Info("Checking for new proxy server parameter.");
                    returnValue = AdminSetProxyServer(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                    CLog.Info("Checking for new proxy port parameter.");
                    returnValue = AdminSetProxyPort(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                    CLog.Info("Checking for new usedefcred parameter.");
                    returnValue = AdminSetProxyUseDefCred(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                    CLog.Info("Checking for new proxy user parameter.");
                    returnValue = AdminSetProxUser(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                    CLog.Info("Checking for new proxy password parameter.");
                    returnValue = AdminSetProxPassw(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                    CLog.Info("Checking for new bypassonlan parameter.");
                    returnValue = AdminSetProxBypassOnLan(Params, out ErrorText);
                    if (returnValue != CReturnCodes.OK)
                    {
                        frmError.ShowError(ErrorText);
                        return returnValue;
                    }
                }


                // Show config dialogue
                CLog.Info("Admin has been status granted, showing config dialogue.");
                frmMain mainFrm = new frmMain();
                mainFrm.SetIcon(Guid, out ErrorText);
                mainFrm.ShowDialog();
                mainFrm.Dispose();
                return CReturnCodes.OK;
            }
            else
            {
                CLog.Info("No admin status has been granted, showing login dialogue.");
                // no admin parameter provided, so open password prompt
                frmPassw passwFrm = new frmPassw();
                passwFrm.SetCorporateDesign(Guid, out ErrorText);
                // check if entered pw is correct and open form
                if (passwFrm.ShowDialog() == DialogResult.OK)
                {
                    CLog.Info("Entered correct admin password, showing config dialogue.");
                    frmMain mainFrm = new frmMain();
                    mainFrm.SetIcon(Guid, out ErrorText);
                    mainFrm.ShowDialog();
                    mainFrm.Dispose();
                    return CReturnCodes.OK;
                }
                else
                {
                    CLog.Info("Password dialogue has been closed by user.");
                    return CReturnCodes.PASSWORD_INPUT_CANCELLED;
                }
            }
        }


        /// <summary>
        /// Checks for updates.
        /// </summary>
        /// <param name="LanguageCode"></param>
        /// <param name="Guid"></param>
        /// <param name="InitDir"></param>
        /// <param name="Params"></param>
        /// <returns>ReturnCode</returns>
        public int CheckForUpdates(String LanguageCode, String Guid, String InitDir, String[] Params)
        {
            // Set up Params if empty
            if ((Params == null) || (Params.Count() <= 0))
            {
                Params = new List<string> { "," }.ToArray();
            }

            // Reset parameters to default
            ResetParametersToDefault();

            //
            CGlobVars.CurrentGUID = Guid;

            //
            String ErrorText;


            // Set application language
            try
            {
                Application.CurrentCulture = new System.Globalization.CultureInfo(LanguageCode);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(LanguageCode);
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(LanguageCode);
            }
            catch (Exception exc)
            {
                frmError.ShowError(exc.ToString());
                return CReturnCodes.SET_LANGUAGE_FAILED;
            }


            // Set InitDir to application dir if it has not been provided by application yet
            InitDir = SetInitDir(InitDir);


            // Set wrk directory
            if (InitDir.EndsWith(@"\"))
            {
                CGlobVars.wrkDir = InitDir + @"wrk\";
            }
            else
            {
                CGlobVars.wrkDir = InitDir + @"\wrk\";
            }


            // Create wrk directory if not exists
            if (!Directory.Exists(CGlobVars.wrkDir))
            {
                try
                {
                    Directory.CreateDirectory(CGlobVars.wrkDir);
                }
                catch (Exception ex)
                {
                    return CReturnCodes.CANNOT_CREATE_WORK_DIRECTORY;
                }
            }


            // Init Logging
            CLog.Init(CGlobVars.wrkDir + CGlobVars.LOG_FILE_NAME, "Info", 10 * 1024 /*10 MB*/);
            CLog.Info("######################################################");

            // Check files
            CheckFiles();

            // Load config gile
            CLog.Info("Loading VersionConfigXML.");
            int retVal = CVersionConfigHelper.DecryptAndLoadVersionConfigXML(CGlobVars.wrkDir + "VersionConfig.xmlc", out ErrorText);
            if (retVal != CReturnCodes.OK)
            {
                CLog.Error("Could not load VersionConfigXML.");
                CLog.Error("Error message: {0}", ErrorText);
                return CReturnCodes.COULD_NOT_LOAD_VERSIONCONFIG_XML;
            }


            // Set log level
            bool useDefaultLoglevel = false;
            CGlobVars.logLevel = CVersionConfigHelper.GetInnerTextByPathAsString("UpdateCheckContent//Common//LogLevel", out ErrorText);
            if (ErrorText != null)
            {
                useDefaultLoglevel = true;
                CGlobVars.logLevel = "DEBUG";
            }
            if (useDefaultLoglevel)
            {
                CLog.Init(CGlobVars.wrkDir + CGlobVars.LOG_FILE_NAME, "DEBUG", 10 * 1024 /*10 MB*/);
                CLog.Info("Using default log level: {0}", "DEBUG");
            }
            else
            {
                CLog.Init(CGlobVars.wrkDir + CGlobVars.LOG_FILE_NAME, CGlobVars.logLevel, 10 * 1024 /*10 MB*/);
                CLog.Info("Using log level: {0}", CGlobVars.logLevel);
            }


            //
            CLog.Info("Enter function 'CheckForUpdates'");
            CLog.Debug("Provided 'LanguageCode': {0}", LanguageCode);
            CLog.Debug("Provided 'Guid': {0}", Guid);
            CLog.Debug("Provided 'InitDir': {0}", InitDir);
            CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));


            // Process parameters
            if ((Params != null) && (Params.Length > 0))
            {
                CLog.Info("Processing provided parameters.");

                //Check if another language setting has been provided by argument and set it
                CLog.Info("Checking for language parameter.");
                SetLanguage(Params);
                // Set application language
                try
                {
                    Application.CurrentCulture = new System.Globalization.CultureInfo(LanguageCode);
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(LanguageCode);
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(LanguageCode);
                }
                catch (Exception exc)
                {
                    frmError.ShowError(exc.ToString());
                    return CReturnCodes.SET_LANGUAGE_FAILED;
                }

                CGlobVars.CurrentLanguage = LanguageCode;

                //Check if silent parameter has been provided
                CLog.Info("Checking for silent mode.");
                SetSilentMode(Params);

                //Check if ignoreInterval parameter has been provided
                CLog.Info("Checking for ignoreInterval mode.");
                SetIgnoreInterval(Params);

                //Check if timeout parameter has been provided
                CLog.Info("Checking for timeout parameter.");
                SetTimeout(Params);

                //Check if version naming patterns have been provided
                CLog.Info("Checking for version pattern parameter.");
                SetVersionNamingPattern(Params);

            }
            else
            {
                CLog.Info("No parameters have been provided.");
            }

            if (!CGlobVars.ignoreCheckInterval)
            {
                // Check checkdays interval 
                String LastChecked = CVersionConfigHelper.GetInnerTextByPathAsString("UpdateCheckContent//Common//LastChecked", out ErrorText);
                String checkDaysInterval = CVersionConfigHelper.GetInnerTextByPathAsString("UpdateCheckContent//Common//CheckDays", out ErrorText);
                int checkDaysNumber;
                if (!int.TryParse(checkDaysInterval, out checkDaysNumber))
                {
                    CLog.Warning("Checkdays parameter has not been set up properly. Falling back to default value 0: {0}", checkDaysInterval);
                    checkDaysNumber = 0;
                }
                // If -1 then no need to check
                if (checkDaysNumber == -1)
                {
                    return CReturnCodes.NO_CHECK_WANTED;
                }
                else
                {
                    if ((LastChecked != null) && (LastChecked != ""))
                    {
                        DateTime dateLastChecked = RZITools.UnixTimeToDateTime(long.Parse(LastChecked), out ErrorText);
                        DateTime dateNow = DateTime.Now;
                        // If last checked day is higher than current day minus checkdays interval, then return
                        if (dateNow.AddDays(checkDaysNumber * -1) < (dateLastChecked))
                        {
                            CLog.Info("Leaving UM due to interval not yet passed.");
                            return CReturnCodes.CHECKDAYS_INTERVAL_NOT_FINISHED_YET;
                        }
                    }
                }
            }


            //
            String LookupUrl = CVersionConfigHelper.GetInnerTextByPathAsString("UpdateCheckContent//Repository//LookupURL", out ErrorText);

            // Load Current Version
            // Decrypt file and load xml, if a file already exists.
            // So we can try to get our current version and extract the corporate identity icon for the forms.
            CLog.Info("Loading local VersionLookupXML.");
            int returnValue = CVersionLookupHelper.DecryptAndLoadVersionLookupXML(CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML, false, out ErrorText);
            if (returnValue != CReturnCodes.OK)
            {
                // If no VersionLookup.xml could have been loaded, its not critical
                // In this case it means that no icon can be loaded for the forms
                CLog.Warning("Could not load VersionLookupXML.");
            }


            //Download encrypted VersionLookup.xmlc from web server
            CLog.Info("Downloading encrypted VersionLookupXML.");
            String VersionLookupFile = "";

            CLog.Info("Showing download dialogue.");
            frmDownload dlgDownload = new frmDownload(LookupUrl, CGlobVars.wrkDir);
            if (dlgDownload.ShowDialog() == DialogResult.OK)
            {
                VersionLookupFile = CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML;
            }
            else
            {
                frmError.ShowError(dlgDownload.m_ErrorText);
                return CReturnCodes.COULD_NOT_DOWNLOAD_VERSION_LOOKUP_XML;
            }


            // Decrypt file and load xml
            //String VersionLookupFile = @"C:\temp\VersionLookup.xmlc";
            returnValue = CVersionLookupHelper.DecryptAndLoadVersionLookupXML(VersionLookupFile, true, out ErrorText);
            if (returnValue != CReturnCodes.OK)
            {
                CLog.Error("Could not load VersionLookupXML.");
                CLog.Error("Error message: {0}", ErrorText);
                return returnValue;
            }

            // Check if our current guid exists in VersionLookup.xml
            CLog.Info("Checking Guid.");
            var CurrentVersionNode = CVersionLookupHelper.GetNodeByTagName(Guid);
            if (CurrentVersionNode == null)
            {
                CLog.Error("Could not find provided Guid: {0}", Guid);
                CLog.Error("Error message: {0}", translations.INVALID_VERSION_FILE);
                frmError.ShowError(String.Format(translations.INVALID_VERSION_FILE, "GUID"));
                return CReturnCodes.INVALID_LOOKUP_XML_GUID_NOT_FOUND;
            }


            // Get our version information
            String CurrentVersionString;
            CLog.Info("Collecting current version information.");
            var DisplayNameNode = CurrentVersionNode.SelectSingleNode("DisplayName");
            if (DisplayNameNode == null)
            {
                CLog.Error("Could not find current version information");
                CLog.Error("Error message: {0}", translations.INVALID_VERSION_FILE);
                frmError.ShowError(String.Format(translations.INVALID_VERSION_FILE, "DisplayName"));
                return CReturnCodes.INVALID_LOOKUP_XML_DISPLAYNAME_NOT_FOUND;
            }
            CurrentVersionString = DisplayNameNode.InnerText;


            // Check for new base version first 
            CLog.Info("Checking for available base versions.");
            int iReturnCode = CBaseVersionLookupHelper.CheckForNewBaseVersions(CurrentVersionNode, Guid, out ErrorText);
            if (iReturnCode == CReturnCodes.OK)
            {
                CLog.Info("Leaving UM without any errors.");
                return CReturnCodes.OK;
            }
            else if (ErrorText != null)
            {
                frmError.ShowError(ErrorText);
                return iReturnCode;
            }

            // No new base version available, continue with sprint versions aka sub versions
            // If no new base version has been found, we will now check for new sprint versions:
            // Find newer versions than ours
            CLog.Info("Checking for available sub versions.");
            iReturnCode = CSubVersionLookupHelper.CheckForNewSubVersions(CurrentVersionNode, Guid, out ErrorText);
            if (iReturnCode != CReturnCodes.OK)
            {
                frmError.ShowError(ErrorText);
                return iReturnCode;
            }
            CLog.Info("Leaving UM without any errors.");
            return CReturnCodes.OK; // No Error occured
        }




        /// <summary>
        /// Resets parameters that can be provided to make sure that parameters will be refreshed
        /// In case of a single instantiation this will ensure that changed parameters between 2 function calls will be recognized.
        /// </summary>
        private void ResetParametersToDefault()
        {
            CGlobVars.wrkDir = "";
            CGlobVars.logLevel = "INFO";
            CGlobVars.silentModeError = false;
            CGlobVars.silentModeNoNewVersion = false;
            CGlobVars.ignoreCheckInterval = false;
            CGlobVars.timeout = 30000;
        }

        /// <summary>
        /// Checks the config and lookup files and deletes them if they are empty
        /// </summary>
        private void CheckFiles()
        {
            CLog.Info("Checking file sizes to prevent from loading 0 byte file.");
            // Check if VersionLookup.xmlc is empty
            if (File.Exists(CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML))
            {
                long len = new System.IO.FileInfo(CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML).Length;
                if (len == 0)
                {
                    CLog.Info("VersionLookup.xmlc file is empty, deleting it.");
                    File.Delete(CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML);
                }
            }

            // Check if VersionLookup.xml is empty
            if (File.Exists(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML))
            {
                long len = new System.IO.FileInfo(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML).Length;
                if (len == 0)
                {
                    CLog.Info("VersionLookup.xml file is empty, deleting it.");
                    File.Delete(CGlobVars.wrkDir + CGlobVars.VERSION_LOOKUP_XML);
                }
            }
            // Check if VersionConfig.xmlc is empty
            if (File.Exists(CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_CONFIG_XML))
            {
                long len = new System.IO.FileInfo(CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_CONFIG_XML).Length;
                if (len == 0)
                {
                    CLog.Info("VersionConfig.xmlc file is empty, deleting it.");
                    File.Delete(CGlobVars.wrkDir + CGlobVars.ENCRYPTED_VERSION_CONFIG_XML);
                }
            }
            // Check if VersionConfig.xml is empty
            if (File.Exists(CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML))
            {
                long len = new System.IO.FileInfo(CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML).Length;
                if (len == 0)
                {
                    CLog.Info("VersionConfig.xml file is empty, deleting it.");
                    File.Delete(CGlobVars.wrkDir + CGlobVars.VERSION_CONFIG_XML);
                }
            }
        }

        /// <summary>
        /// Sets init dir to define where the wrk directory needs to be looked/created for.
        /// Init dir priority:
        /// -> Custom provided Init dir
        /// -> User ApplicationData (Roaming)
        /// -> Directory of executing program / assembly
        /// </summary>
        /// <param name="InitDir">Contains a provided dir or is empty</param>
        /// <returns>Init directory</returns>
        private string SetInitDir(string InitDir)
        {
            CLog.Debug("Entered function 'SetInitDir'");
            CLog.Debug("Provided 'InitDir': {0}", InitDir);
            if ((InitDir == null) || (InitDir.Trim() == ""))
            {
                InitDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                CLog.Debug("Trying to set init dir in AppData directory: {0}", InitDir);
                if (Directory.Exists(InitDir))
                {
                    CLog.Debug("Directory already exists.");
                    string folderName = "";
                    if (Application.ProductName != null)
                    {
                        folderName = RZITools.GetCleanFileName(Application.ProductName);
                        CLog.Debug("Trying to set application folder name: {0}", folderName);
                    }
                    else
                    {
                        folderName = "UpdateModule";
                        CLog.Debug("Using default application folder name: {0}", folderName);
                    }
                    InitDir = InitDir + System.IO.Path.DirectorySeparatorChar + folderName + System.IO.Path.DirectorySeparatorChar;
                    if (!Directory.Exists(InitDir))
                    {
                        CLog.Debug("Directory does not exist yet: {0}", InitDir);
                        try
                        {
                            Directory.CreateDirectory(InitDir);
                            CLog.Debug("Created new init directory successfully.");
                        }
                        catch
                        {
                            InitDir = Path.GetDirectoryName(Application.ExecutablePath);
                            CLog.Warning("Error creating new init directory, using default directory: {0}", InitDir);
                        }
                    }
                }
                else
                {
                    InitDir = Path.GetDirectoryName(Application.ExecutablePath);
                    CLog.Warning("AppData directory cannot be found, using default directory: {0}", InitDir);
                }
            }
            return InitDir;
        }
    }
}
