using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using UpdateModul.shared;

namespace UpdateModul
{
    class RZITools
    {
        public static bool DownloadComplete = false;
        public static string DownloadFilePath = "";
        public static int DownloadPercent = 0;

        public static FileStream _fstr;
        public static MemoryStream _ms;
        public static FileStream _file;

        public delegate void DelDownloadStatus(long currentBytes, long totalBytes);


        private static void browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            Uri uri = e.Url;
        }


        public static string GetFinalRedirectedUrl(string url)
        {
            string result = string.Empty;

            Uri Uris = new Uri(url);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Uris);
            //req3.Proxy = proxy;
            req.Method = "HEAD";
            req.AllowAutoRedirect = false;

            HttpWebResponse myResp = (HttpWebResponse)req.GetResponse();
            if (myResp.StatusCode == HttpStatusCode.Redirect)
            {
                string temp = myResp.GetResponseHeader("Location");
                //Recursive call
                result = GetFinalRedirectedUrl(temp);
            }
            else
            {
                result = url;
            }

            return result;
        }

        /// <summary>
        /// Downloads file from WebServer
        /// </summary>
        /// <param name="downloadUrl">URL to download from</param>
        /// <param name="workingDirectory">Work dir to save file to hdd</param>
        /// <param name="downloadStatus">Delegated Download status, i.e. for progress bars</param>
        /// <param name="errorText">errorText if error occurs</param>
        /// <returns></returns>
        public static bool DownloadFile(string downloadUrl, string workingDirectory, DelDownloadStatus downloadStatus, out string errorText)
        {
            CLog.Debug("Entered function 'DownloadFile'");
            CLog.Debug("Provided 'downloadUrl': {0}", downloadUrl);
            CLog.Debug("Provided 'workingDirectory': {0}", workingDirectory);
            try
            {
                //string fileName = ExtractFileNameFromUri(downloadUrl, out errorText);
                //if (errorText != null)
                //{
                //    return false;
                //}

                WebRequest wr = RZITools.CreateWebRequestWithProxySettings(downloadUrl, out errorText);
                if (errorText != null)
                {
                    return false;
                }

                //string urlToNavigate = GetFinalRedirectedUrl(downloadUrl);


                HttpWebResponse ws = (HttpWebResponse)wr.GetResponse();
                Uri uiui = ws.ResponseUri;

                wr.Timeout = CGlobVars.timeout;

                long comBytes = ws.ContentLength;
                long comBytesRead = 0;

                if (File.Exists(workingDirectory + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML))
                {
                    try
                    {
                        File.Delete(workingDirectory + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML);
                    } catch (Exception exDel)
                    {
                        CLog.Warning("Cannot delete existing " + CGlobVars.ENCRYPTED_VERSION_CONFIG_XML + ": {0}", exDel.Message);
                    }
                    try
                    {
                        if (File.Exists(workingDirectory + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML))
                        {
                            File.Move(workingDirectory + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML, workingDirectory + "_" + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML);
                        }
                    }
                    catch (Exception exDel)
                    {
                        CLog.Warning("Cannot move existing " + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML + ": {0}", exDel.Message);
                    }

                }

                using (Stream str = ws.GetResponseStream())
                {
                    byte[] inBuf = new byte[1024];

                    int bytesToRead = (int)(((comBytes - comBytesRead) > inBuf.Length) ? inBuf.Length : comBytes - comBytesRead);

                    if (bytesToRead > 0)
                    {
                        using (_fstr = new FileStream(workingDirectory + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML, FileMode.Create, FileAccess.Write))
                        {
                            while (bytesToRead > 0)
                            {
                                int n = str.Read(inBuf, 0, bytesToRead);
                                if (n == 0)
                                    break;
                                else
                                    _fstr.Write(inBuf, 0, n);

                                comBytesRead += n;

                                if (downloadStatus != null)
                                    downloadStatus(comBytesRead, comBytes);

                                bytesToRead = (int)(((comBytes - comBytesRead) > inBuf.Length) ? inBuf.Length : comBytes - comBytesRead);
                            }
                        }


                        if (downloadStatus != null)
                            downloadStatus(comBytesRead, comBytes);
                    } else
                    {

                        const int readSize = 256;
                        byte[] buffer = new byte[readSize];

                        using (_ms = new MemoryStream())
                        {
                            comBytes = ws.ContentLength;
                            comBytesRead = 0;
                            int count = ws.GetResponseStream().Read(buffer, 0, readSize);
                            while (count > 0)
                            {
                                _ms.Write(buffer, 0, count);
                                count = ws.GetResponseStream().Read(buffer, 0, readSize);
                                comBytesRead += count;
                                if (downloadStatus != null)
                                    downloadStatus(comBytesRead, comBytes);

                            }

                            String aaa = Encoding.ASCII.GetString(_ms.ToArray());


                            if (downloadStatus != null)
                                downloadStatus(comBytesRead, comBytes);

                            using (_file = new FileStream(workingDirectory + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML, FileMode.Create, System.IO.FileAccess.Write))
                            {
                                byte[] bytes = new byte[_ms.Length];
                                _ms.Read(bytes, 0, (int)_ms.Length);
                                _file.Write(bytes, 0, bytes.Length);
                                _ms.Close();
                                _file.Close();
                            }

                            ws.GetResponseStream().Close();

                            

                        }

                        
                    }


                    
                }
            }
            catch (Exception exc)
            {
                errorText = exc.ToString();
                CLog.Error("Could not download VersionLookupXML.");
                CLog.Error("Error message: {0}", errorText);
                return false;
            }

            errorText = null;
            return true;
        }

        /// <summary>
        /// Creates WebRequest with proxy settings from config file
        /// </summary>
        /// <param name="downloadPath">URL to download from</param>
        /// <param name="errorText">errorText if error occurs</param>

        /// <returns>WebRequest configured with proxy settings</returns>
        public static WebRequest CreateWebRequestWithProxySettings(string downloadPath, out string errorText)
        {
            CLog.Debug("Entered function 'CreateWebRequestWithProxySettings'");
            CLog.Debug("Provided 'downloadPath': {0}", downloadPath);

            bool useCustomProxy = CVersionConfigHelper.GetInnerTextByPathAsBool("UpdateCheckContent//Proxy//UseProxy", out errorText);
            bool useDefCred = CVersionConfigHelper.GetInnerTextByPathAsBool("UpdateCheckContent//Proxy//UseDefCred", out errorText);
            bool bypassOnLan = CVersionConfigHelper.GetInnerTextByPathAsBool("UpdateCheckContent//Proxy//BypassOnLan", out errorText);
            string proxyServer = CVersionConfigHelper.GetInnerTextByPathAsString("UpdateCheckContent//Proxy//ProxyServer", out errorText);
            string proxyPort = CVersionConfigHelper.GetInnerTextByPathAsString("UpdateCheckContent//Proxy//ProxyPort", out errorText);
            string proxyUser = CVersionConfigHelper.GetInnerTextByPathAsString("UpdateCheckContent//Proxy//ProxyUser", out errorText);
            string proxyPW = CVersionConfigHelper.GetInnerTextByPathAsPW("UpdateCheckContent//Proxy//ProxyPassw", out errorText);

            HttpWebRequest wr = (HttpWebRequest)HttpWebRequest.Create(downloadPath);
            wr.AllowAutoRedirect = true;
            wr.MaximumAutomaticRedirections = 100;
            wr.Method = "GET";

            try
            {
                IWebProxy proxy = wr.Proxy;
                CLog.Debug("Checking configuration settings.");
                if (useCustomProxy)
                {
                    if ((proxyServer != null) && (proxyServer.Trim() != ""))
                    {
                        CLog.Debug("Configuring proxy settings.");
                        string proxyAddress = "";
                        WebProxy newProxy = new WebProxy();
                        if (!proxyServer.StartsWith("http"))
                        {
                            proxyServer = "http://" + proxyServer;
                        }
                        if ((proxyPort == null) || (proxyPort.Trim() == ""))
                        {
                            proxyPort = "8080";
                        }
                        proxyAddress = proxyServer + ":" + proxyPort;
                        Uri newUri = new Uri(proxyAddress);
                        newProxy.Address = newUri;
                        if (useDefCred)
                        {
                            newProxy.UseDefaultCredentials = true;
                        }
                        else
                        {
                            newProxy.UseDefaultCredentials = false;
                            if ((proxyUser != null) && (proxyUser.Trim() != "") && ((proxyPW == null) || (proxyPW.Trim() == "")))
                            {
                                newProxy.Credentials = new NetworkCredential(proxyUser, "");
                            }
                            if ((proxyUser != null) && (proxyUser.Trim() != "") && ((proxyPW != null) && (proxyPW.Trim() != "")))
                            {
                                newProxy.Credentials = new NetworkCredential(proxyUser, proxyPW);
                            }
                        }
                        if (bypassOnLan)
                        {
                            newProxy.BypassProxyOnLocal = true;
                        }
                        else
                        {
                            newProxy.BypassProxyOnLocal = false;
                        }

                        wr.Proxy = newProxy;
                    }
                    else
                    {
                        CLog.Debug("No proxy server or proxy port has been configured. Skipping proxy configuration.");
                    }
                    
                }

            }
            catch (Exception e)
            {
                errorText = e.ToString();
            }
            errorText = null;
            return wr;
        }


        /// <summary>
        /// Generates a GUID that starts with a letter
        /// </summary>
        /// <returns>GUID</returns>
        public static string GenerateGUID()
        {
            int i = 0;
            string resultGUID = "";
            while (true)
            {
                i++;
                resultGUID = Guid.NewGuid().ToString();
                if (char.IsLetter(resultGUID.FirstOrDefault()))
                {
                    return resultGUID;
                }
                if (i >= 10000)
                {
                    // very bad luck, try again 
                    return null;
                }
            }
        }


        /// <summary>
        /// Extracts file name from provided URI.
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="errorText"></param>
        /// <returns>File name</returns>
        public static string ExtractFileNameFromUri(string uri, out string errorText)
        {
            CLog.Debug("Entered function 'ExtractFileNameFromUri'");
            CLog.Debug("Provided 'uri': {0}", uri);
            errorText = null;
            try
            {
                string filename = "";
                filename = Path.GetFileName(Uri.UnescapeDataString(uri).Replace(@"\", @"/"));
                CLog.Debug("Returning file name: {0}", filename);
                return filename;
            }
            catch (Exception ex)
            {
                errorText = ex.ToString();
                CLog.Error("Error during extraction: {0}", errorText);
                return "";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64string"></param>
        /// <param name="errorText"></param>
        /// <returns>Bitmap</returns>
        public static Bitmap GetBitmapFromBase64String(string base64string, out string errorText)
        {
            CLog.Debug("Entered function 'GetBitmapFromBase64string'");
            CLog.Debug("Provided 'base64string': {0}", base64string);
            try
            {
                byte[] buffer = Convert.FromBase64String(base64string);

                if ((buffer != null) && (base64string != null) && (base64string.Length > 0))
                {
                    ImageConverter ic = new ImageConverter();
                    errorText = null;
                    return ic.ConvertFrom(buffer) as Bitmap;
                }
                else
                {
                    errorText = null;
                    return null;
                }
            }
            catch (Exception ex)
            {
                errorText = ex.ToString();
                CLog.Error("Error during conversion: {0}", errorText);
                return null;
            }
        }


        /// <summary>
        /// Encrypts a given string.
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="errorText"></param>
        /// <returns>Encrypted string</returns>
        public static string Encrypt(string clearText, out string errorText)
        {
            CLog.Debug("Entered function 'Encrypt'");
            CLog.Debug("Provided 'clearText': {0}", "<hidden>");
            try
            {
                string EncryptionKey = CGlobVars.PASSWORD_TOKEN;
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new
                        Rfc2898DeriveBytes(EncryptionKey, new byte[]
                        { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                errorText = null;
                return clearText;
            }
            catch (Exception ex)
            {
                errorText = ex.ToString();
                CLog.Error("Error during encryption: {0}", errorText);
                return null;
            }
        }


        /// <summary>
        /// Decrypts provided string.
        /// </summary>
        /// <param name="cipherText">Encrypted text</param>
        /// <param name="errorText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, out string errorText)
        {
            CLog.Debug("Entered function 'Decrypt'");
            CLog.Debug("Provided 'cipherText': {0}", cipherText);
            try
            {
                string EncryptionKey = CGlobVars.PASSWORD_TOKEN;
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new
                        Rfc2898DeriveBytes(EncryptionKey, new byte[]
                        { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                errorText = null;
                return cipherText;
            }
            catch (Exception ex)
            {
                errorText = ex.ToString();
                CLog.Error("Error during decryption: {0}", errorText);
                return null;
            }
        }


        /// <summary>
        /// Encrypts a file.
        /// </summary>
        /// <param name="inputFilename">Provided file</param>
        /// <param name="outputFilename">Desired destination for encrypted file</param>
        /// <param name="key">Encryption key</param>
        /// <param name="errorText"></param>
        public static void EncryptFile(string inputFilename, string outputFilename, string key, out string errorText)
        {
            CLog.Debug("Entered function 'EncryptFile'");
            CLog.Debug("Provided 'inputFilename': {0}", inputFilename);
            CLog.Debug("Provided 'outputFilename': {0}", outputFilename);
            try
            {
               FileStream fsInput = new FileStream(inputFilename,
               FileMode.Open,
               FileAccess.Read);

                FileStream fsEncrypted = new FileStream(outputFilename,
                   FileMode.Create,
                   FileAccess.Write);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);
                ICryptoTransform desencrypt = des.CreateEncryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted,
                   desencrypt,
                   CryptoStreamMode.Write);

                byte[] bytearrayinput = new byte[fsInput.Length];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Close();
                fsInput.Close();
                fsEncrypted.Close();
                errorText = null;
            }
            catch (Exception ex)
            {
                errorText = ex.ToString();
                CLog.Error("Error during file encryption: {0}", errorText);
            }
        }


        /// <summary>
        /// Decrypts a file.
        /// </summary>
        /// <param name="inputFilename">Provided file</param>
        /// <param name="outputFilename">Desired destination for decrypted file</param>
        /// <param name="Key">Decryption key</param>
        /// <param name="errorText"></param>
        /// <returns>true / false</returns>
        public static bool DecryptFile(string inputFilename, string outputFilename, string key, out string errorText)
        {
            CLog.Debug("Entered function 'DecryptFile'");
            CLog.Debug("Provided 'inputFilename': {0}", inputFilename);
            CLog.Debug("Provided 'outputFilename': {0}", outputFilename);
            //FileStream fsread = null;
            try
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    //A 64 bit key and IV is required for this provider.
                    //Set secret key For DES algorithm.
                    des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                    //Set initialization vector.
                    des.IV = ASCIIEncoding.ASCII.GetBytes(key);

                    //Create a file stream to read the encrypted file back.
                    using (FileStream fsread = new FileStream(inputFilename,
                       FileMode.Open,
                       FileAccess.Read))
                    {
                        //Create a DES decryptor from the DES instance.
                        ICryptoTransform desdecrypt = des.CreateDecryptor();
                        //Create crypto stream set to read and do a 
                        //DES decryption transform on incoming bytes.
                        using (CryptoStream cryptostreamDecr = new CryptoStream(fsread,
                           desdecrypt,
                           CryptoStreamMode.Read))
                        {
                            //Print the contents of the decrypted file.
                            using (StreamWriter fsDecrypted = new StreamWriter(outputFilename))
                            {
                                fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
                                fsDecrypted.Flush();
                                fsDecrypted.Close();
                                fsread.Flush();
                                fsread.Close();
                                errorText = null;
                                return true;
                            }
                                
                        }
                            
                    }
                        
                }
                    
            }
            catch (Exception ex)
            {
                errorText = ex.ToString();
                CLog.Error("Error during file decryption: {0}", errorText);
                
                return false;
            }
        }


        /// <summary>
        /// Converts datetime to unix time
        /// </summary>
        /// <param name="datetime">Provided DateTime</param>
        /// <param name="errorText"></param>
        /// <returns>Converted unix time or 0 in case of exception</returns>
        public static long ConvertToUnixTime(DateTime datetime, out string errorText)
        {
            CLog.Debug("Entered function 'ConvertToUnixTime'");
            CLog.Debug("Provided 'datetime': {0}", datetime.ToString());
            try
            {
                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                errorText = null;
                return (long)(datetime - time).TotalSeconds;
            }
            catch (Exception ex)
            {
                errorText = ex.ToString();
                CLog.Error("Error during time conversion: {0}", errorText);
                return 0;
            }
        }


        /// <summary>
        /// Converts unix time to datetime
        /// </summary>
        /// <param name="unixtime">Provided unix time</param>
        /// <param name="errorText"></param>
        /// <returns>DateTime</returns>
        public static DateTime UnixTimeToDateTime(long unixtime, out string errorText)
        {
            CLog.Debug("Entered function 'UnixTimeToDateTime'");
            CLog.Debug("Provided 'unixtime': {0}", unixtime);
            try
            {
                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                errorText = null;
                return time.AddSeconds(unixtime);
            }
            catch (Exception ex)
            {
                errorText = ex.ToString();
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }


        /// <summary>
        /// Checks if provided email is correct.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>true / false</returns>
        public static bool ValidateEmail(string emailAddress)
        {
            CLog.Debug("Entered function 'ValidateEmail'");
            CLog.Debug("Provided 'emailAddress': {0}", emailAddress);
            bool result;

            if ((emailAddress != null) && (emailAddress != ""))
            {

                try
                {
                    var test = new MailAddress(emailAddress);
                    CLog.Debug("Email was correct.");
                    result = true;
                }
                catch (FormatException ex)
                {
                    result = false;
                    CLog.Warning("Email was wrong: {0}", ex.ToString());
                }
            }
            else
            {
                result = true;
            }
            return result;
        }


        /// <summary>
        /// Checks if provided URL is correct.
        /// </summary>
        /// <param name="urlText"></param>
        /// <returns></returns>
        public static bool ValidateURL(string urlText)
        {
            CLog.Debug("Entered function 'ValidateURL'");
            CLog.Debug("Provided 'urlText': {0}", urlText);
            bool result;

            if ((urlText != null) && (urlText != ""))
            {
                try
                {
                    Uri check = new Uri(urlText);
                    CLog.Debug("URL was correct.");
                    result = true;
                }
                catch (UriFormatException ex)
                {
                    CLog.Warning("URL was wrong: {0}", ex.ToString());
                    result = false;
                }
            }
            else
            {
                result = true;
            }


            return result;
        }


        /// <summary>
        /// Converts Image to base64
        /// </summary>
        /// <param name="image"></param>
        /// <returns>base64 string</returns>
        public static string GetBase64stringFromImage(Image image)
        {
            CLog.Debug("Entered function 'GetBase64stringFromImage'");
            CLog.Debug("Provided 'image': {0}", image);
            if (image != null)
            {
                ImageConverter ic = new ImageConverter();
                byte[] buffer = (byte[])ic.ConvertTo(image, typeof(byte[]));
                return Convert.ToBase64String(
                    buffer,
                    Base64FormattingOptions.InsertLineBreaks);
            }
            else
                return "";
        }


        /// <summary>
        /// Removes invalid chars from provided file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Clean file name</returns>
        public static string GetCleanFileName(string fileName)
        {
            CLog.Debug("Entered function 'GetCleanFileName'");
            CLog.Debug("Provided 'FileName': {0}", fileName);
            var invalidChars = Path.GetInvalidFileNameChars();

            string cleanFileName = new string(fileName
            .Where(x => !invalidChars.Contains(x))
            .ToArray());

            return cleanFileName;
        }


        /// <summary>
        /// Converts base64 string to image
        /// </summary>
        /// <param name="base64string"></param>
        /// <returns>Image</returns>
        public static Image GetImageFromBase64string(string base64string)
        {
            byte[] buffer = Convert.FromBase64String(base64string);

            if ((buffer != null) && (base64string != null) && (base64string.Length > 0))
            {
                ImageConverter ic = new ImageConverter();
                return ic.ConvertFrom(buffer) as Image;
            }
            else
                return null;
        }


        /// <summary>
        /// Provides encrypted LookupVersion.xml to desired path.
        /// </summary>
        /// <param name="exportPath"></param>
        /// <returns>true / false</returns>
        public static bool ProvideEncryptedXMLFile(string exportPath, out string errorText)
        {
            bool doExport = false;
            string refinedPath = "";
            if (exportPath == "")
            {
                FolderBrowserDialog objDialog = new FolderBrowserDialog();
                objDialog.Description = translations.frmMain_Dlg_PathToExport_Title;
                objDialog.SelectedPath = @"C:\";
                DialogResult objResult = objDialog.ShowDialog();
                if (objResult == DialogResult.OK)
                {
                    doExport = true;
                    refinedPath = objDialog.SelectedPath.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
                }
            }
            else
            {
                doExport = true;
                refinedPath = exportPath.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            }

            if (doExport)
            {
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                path = path.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar + "wrk" + Path.DirectorySeparatorChar;

                string savePath = refinedPath;
                try
                {
                    if (File.Exists(savePath + CGlobVars.VERSION_LOOKUP_XML))
                    {
                        File.Delete(savePath + CGlobVars.VERSION_LOOKUP_XML);
                    }
                    File.Copy(path + CGlobVars.VERSION_LOOKUP_XML, savePath + CGlobVars.VERSION_LOOKUP_XML, true);

                    string finalSource = savePath + CGlobVars.VERSION_LOOKUP_XML;
                    string finalDest = savePath + CGlobVars.ENCRYPTED_VERSION_LOOKUP_XML;

                    EncryptFile(finalSource, finalDest, CGlobVars.PASSWORD_TOKEN, out errorText);


                    if (File.Exists(savePath + CGlobVars.VERSION_LOOKUP_XML))
                    {
                        File.Delete(savePath + CGlobVars.VERSION_LOOKUP_XML);
                    }
                    if (savePath != "")
                    {
                        if (MessageBox.Show(translations.frmMain_Dlg_EncryptFile_Success, translations.frmMain_Dlg_Title_Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Process.Start(savePath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (savePath == "")
                    {
                        MessageBox.Show(translations.frmMain_Dlg_EncryptFile_Error + "\r\n" + ex.Message, translations.frmMain_Dlg_Title_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    errorText = ex.ToString();
                    return false;
                }
            }
            errorText = "";
            return true;
        }

    }
}
