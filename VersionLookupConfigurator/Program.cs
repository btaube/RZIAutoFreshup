using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UpdateModul;

namespace UpdateModul
{
    static class Program
    {
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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());

            return 0;
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
