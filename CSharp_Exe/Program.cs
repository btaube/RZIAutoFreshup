using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using UpdateModul;
using System.Windows.Forms;


namespace UpdateExe
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            Console.WriteLine("Arguments");
            Console.WriteLine(" -language=[language]   (de-DE, en-US)");
            Console.WriteLine(" -guid=[GUID]");
            Console.WriteLine(" -path=[VersionLookupPath]");
            Console.WriteLine(" -pass=[Admin password]");
            Console.WriteLine(" -setpw=[User password]");
            Console.WriteLine(" -seturl=[Repository url]");
            Console.WriteLine(" -setdays=[Days between checks]");
            Console.WriteLine(" -setproxyuse=[1= Use Custom Proxy / 0=Use IE Settings]");
            Console.WriteLine(" -setproxyserver=[Proxy server]");
            Console.WriteLine(" -setproxyport=[Proxy port]");
            Console.WriteLine(" -setproxyusedefcred=[Use default credentials for proxy]");
            Console.WriteLine(" -setproxyuser=[Proxy user]");
            Console.WriteLine(" -setproxypassw=[Proxy password]");
            Console.WriteLine(" -setproxybypassonlan=[Bypass proxy on lan]");
            Console.WriteLine("");

            String paramLanguage = null;
            String paramGUID = null;
            String paramPath = null;
            String[] paramParams = {"", ""};

            Console.WriteLine("Check argument ...");
            try
            {
                foreach (var argument in args)
                {
                    String lowArgument = argument.ToLower();
                    if (lowArgument.StartsWith("-language"))
                    {
                        var arr = argument.Split('=');
                        if (arr.Length == 2)
                        {
                            paramLanguage = argument.Split('=')[1];
                            Console.WriteLine("Language: {0}", paramLanguage);
                        }
                        else
                            throw new System.ArgumentException("Argument has syntax error!", "Language");
                    }
                    else if (lowArgument.StartsWith("-guid"))
                    {
                        var arr = argument.Split('=');
                        if (arr.Length == 2)
                        {
                            paramGUID = argument.Split('=')[1];
                            Console.WriteLine("GUID: {0}", paramGUID);
                        }
                        else
                            throw new System.ArgumentException("Argument has syntax error!", "GUID");
                    }
                    else if (lowArgument.StartsWith("-path"))
                    {
                        var arr = argument.Split('=');
                        if (arr.Length == 2)
                        {
                            paramPath = argument.Split('=')[1];
                            Console.WriteLine("Path: {0}", paramPath);
                        }
                        else
                            throw new System.ArgumentException("Argument has syntax error!", "PATH");
                    }
                }

                // Argument required
                //if (String.IsNullOrEmpty(paramGUID))
                //    throw new System.ArgumentException("Argument is missing!", "GUID");

                // Default english
                if (String.IsNullOrEmpty(paramLanguage))
                {
                    Console.WriteLine("Argument 'Language' is missing! Take default language 'en-US'!");
                    paramLanguage = "en-US";
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
                return -1;
            }


            Console.WriteLine("Start update process ...");
            try
            {
                CUpdateModul um = new CUpdateModul();
                String[] Params = new String[] { "-versionname=2", "-pass=admin_updater", "-ignoreinterval=true", "-timeout=10000" };
                //int returnVal = um.CheckForUpdates(paramLanguage, "ea986764-8407-48aa-8d5a-0cc4d7832604", "", Params);
                //int returnVal = um.CheckForUpdates(paramLanguage, "bb1d828f-2550-4e3d-8d14-875bb8b6bcba", "", Params);

                int returnVal = um.OpenConfig("de-DE", "df2d36ca-2fb9-410b-bc84-323ad2eeb81b", "", Params);
                //int returnVal = um.OpenConfig("de-DE", "b6984695-64a6-453b-bb5d-c7ced6a9ea5d", "", Params);
                //int returnVal = um.OpenConfig("en-GB", "c601c7d2-eea8-444d-b8db-76e935f7473f", "", null);

                return returnVal;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
                return -1;
            }
        }
    }
}
