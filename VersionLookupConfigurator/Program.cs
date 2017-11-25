using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Configuration;
using System.Windows.Forms;
using UpdateModul.shared;

namespace UpdateModul
{
    static class Program
    {
        //public static object ConfigurationManager { get; private set; }

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            String[] arguments = Environment.GetCommandLineArgs();
            //Set default language
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
            //Check if another language setting has been provided by argument and set it
            SetLanguage(arguments);
            // Check if export is asked for
            int returnValue = 0;
            returnValue = ExportFile(arguments);
            if (returnValue != 0)
            {
                return returnValue;
            }

            SetVersionLookupFile(args);
            SetClient();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            return 0;
        }

        public static void SetClient()
        {
            if (ConfigurationManager.AppSettings["Client"] != null)
            {
                try
                {
                    CGlobVars.Client = ConfigurationManager.AppSettings["Client"].ToString().ToUpper().Replace("-", "");
                }
                catch (Exception ex)
                {

                }

                if (CGlobVars.Client == "")
                {
                    CGlobVars.Client = "RZI";
                }
            }
            else
            {
                CGlobVars.Client = "RZI";
            }
        }

        private static int ExportFile(String[] Params)
        {
            string ErrorText = "";
            foreach (string param in Params)
            {
                if (param.ToLower().Contains("-export="))
                {
                    string sPath = param;
                    if (sPath.Contains("\""))
                    {
                        sPath = sPath.Replace("\"", "");
                    }
                    sPath = sPath.Substring(8);

                    if (Directory.Exists(sPath))
                    {
                        if (RZITools.ProvideEncryptedXMLFile(sPath, out ErrorText))
                        {
                            return 1;
                        }
                        else
                        {
                            return 2;
                        }
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
            return 0;
        }

        private static void SetVersionLookupFile(String[] Params)
        {
            //CLog.Debug("Entered function 'SetSilentMode'");
            //CLog.Debug("Provided 'Params': {0}", Params.Aggregate((a, b) => a + "," + b));
            foreach (string param in Params)
            {
                //CLog.Debug("Processed 'param': {0}", param);
                if (param.ToLower().Contains("-fvl="))
                {
                    //CLog.Debug("Found param '-silent': {0}", param);
                    string Pattern = param;
                    if (Pattern.Contains("\""))
                    {
                        Pattern = Pattern.Replace("\"", "");
                        Pattern = Pattern.Replace("“", "");
                        Pattern = Pattern.Replace("'", "");
                    }
                    Pattern = Pattern.Substring(5);
                    if (Pattern.Length > 0)
                    {
                        CGlobVars.currentlyLoadedFile = Pattern;
                    }
                }
            }
        }

        private static void SetLanguage(String[] Params)
        {
            foreach (string param in Params)
            {
                if (param.ToLower().Contains("-lang="))
                {
                    string sLang = param;
                    if (sLang.Contains("\""))
                    {
                        sLang = sLang.Replace("\"", "");
                    }
                    sLang = sLang.Substring(6);

                    switch (sLang.ToLower())
                    {
                        case "de":
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                            break;
                        case "de-de":
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                            break;
                        case "en":
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
                            break;
                        case "en-gb":
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
                            break;
                        case "en-us":
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
                            break;
                        default:
                            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-DE");
                            break;
                    }
                }
            }
        }
    }
}
