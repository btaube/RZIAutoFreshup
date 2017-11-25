using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateModul.shared
{
    public static class CGlobVars
    {
        public const string DEFAULT_PASSWORD = "admin_updater";
        public const string SUPER_ADMIN_PASSWORD = "cD7§)/:#?.";
        public const string PASSWORD_TOKEN = "VLookup7";
        public const string LOG_FILE_NAME = "UMLog.log";
        public const string VERSION_LOOKUP_XML = "VersionLookup.xml";
        public const string ENCRYPTED_VERSION_LOOKUP_XML = "VersionLookup.xmlc";
        public const string ENCRYPTED_VERSION_CONFIG_XML = "VersionConfig.xmlc";
        public const string PROXYTEST_ENCRYPTED_VERSION_LOOKUP_XML = "PROXYTEST_VersionLookup.xmlc";
        public const string VERSION_CONFIG_XML = "VersionConfig.xml";

        // XML paths VersionConfig
        public const string UDPATECHECKCONTENT_COMMON_LASTCHECKED = "UpdateCheckContent//Common//LastChecked";

        // XML paths VersionLookup


        // XML attributes VersionLookup

        public static string CurrentGUID = "";
        public static string CurrentLanguage = "";

        public static string Client = "";
        public static string initDir = "";
        public static string wrkDir = "";

        public static string currentlyLoadedFile = "";

        public static string logLevel;
        
        public static int VersionDescriptionID = 1; // 1 = rzi; 2 = card/1

        public static bool silentModeError = false;
        public static bool silentModeNoNewVersion = false;

        public static bool ignoreCheckInterval = false;

        public static int timeout = 30000;
    }
}
