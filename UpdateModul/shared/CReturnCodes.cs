using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateModul
{
    public static class CReturnCodes
    {
        public const int OK = 0;
        // file not found / no create permission
        public const int COULD_NOT_LOAD_VERSIONCONFIG_XML = 101;
        public const int COULD_NOT_LOAD_VERSIONLOOKUP_XML = 102;
        // set config values
        public const int SET_PASSWORD_FAILED = 201;
        public const int SET_LOOKUP_URL_FAILED = 202;
        public const int SET_CHECK_DAYS_FAILED = 203;
        public const int SET_USE_PROXY_FAILED = 204;
        public const int SET_USE_PROXY_SERVER_FAILED = 205;
        public const int SET_USE_PROXY_PORT_FAILED = 206;
        public const int SET_USE_PROXY_USE_DEF_CRED_FAILED = 207;
        public const int SET_USE_PROXY_USER_FAILED = 208;
        public const int SET_USE_PROXY_PASSWORD_FAILED = 209;
        public const int SET_USE_PROXY_BYPASS_ON_LAN_FAILED = 210;
        public const int SET_LANGUAGE_FAILED = 211;
        // download
        public const int COULD_NOT_DOWNLOAD_VERSION_LOOKUP_XML = 301;
        // system critical errors
        public const int DECRYPT_PASSWORD_FAILED = 401;
        public const int COULD_NOT_DECRYPT_VERSION_LOOKUP_XML = 402;
        public const int INVALID_LOOKUP_XML_GUID_NOT_FOUND = 403;
        public const int INVALID_LOOKUP_XML_DISPLAYNAME_NOT_FOUND = 404;
        public const int CANNOT_CREATE_WORK_DIRECTORY = 405;
        public const int COULD_NOT_DECRYPT_VERSION_CONFIG_XML = 406;
        public const int LOOKUP_XML_IS_EMPTY = 407;
        // user-based
        public const int ADMIN_PASSWORD_WRONG = 501;
        public const int PASSWORD_INPUT_CANCELLED = 502;
        public const int ADMIN_PASSWORD_EMPTY = 503;
        public const int CHECKDAYS_INTERVAL_NOT_FINISHED_YET = 504;
        public const int NO_VALID_CHECKDAYS_VALUE_SET = 505;
        public const int NO_CHECK_WANTED = 506;
        // internal
        public const int WARNING_NO_NEW_BASE_VERSION = 901;
        public const int INVALID_LOOKUP_XML_LEAF_NOT_FOUND = 902;
    }
}
