using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UpdateModul
{
    public static class CLog
    {

        public static string LastError;
        public static string LastEntry;
        private static String m_FileName;
        private static int m_iMaxSizeByte;
        private static string m_Loglevel;
        private static object m_Lock = new object();

        public static void Init(string FileName, string LogLevel, int iMaxSizeKb)
        {
            m_FileName = FileName;
            m_iMaxSizeByte = iMaxSizeKb * 1024;
            m_Loglevel = LogLevel;

            string directoryName = Path.GetDirectoryName(FileName);
            if (!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
        }

        public static void Info(string formatStr, params object[] obj)
        {
            string type = "I";
            LastEntry = String.Format(formatStr, obj);
            LogFinal(type, formatStr, obj);
        }

        public static void Debug(string formatStr, params object[] obj)
        {
            string type = "D";
            LastEntry = String.Format(formatStr, obj);
            LogFinal(type, formatStr, obj);
        }

        public static void Error(string formatStr, params object[] obj)
        {
            string type = "E";
            LastError = String.Format(formatStr, obj);
            LogFinal(type, formatStr, obj);
        }

        public static void Exception(Exception ex)
        {
            string type = "X";
            LastError = ex.ToString();
            LogFinal(type, ex.ToString());
        }

        public static void Warning(string formatStr, params object[] obj)
        {
            string type = "W";
            LastEntry = String.Format(formatStr, obj);
            LogFinal(type, formatStr, obj);
        }

        private static void LogFinal(string type, string formatStr, params object[] obj)
        {
            if (m_Loglevel != null)
            {

                if (m_Loglevel.Equals("INFO"))
                {
                    // No log entry, if debug action, but global log level has been set to INFO
                    if ("D".Equals(type))
                    {
                        return;
                    }
                }

                DateTime dt = DateTime.Now;
                StringBuilder sb = new StringBuilder(512);

                sb.AppendFormat("{0} {1}^{2:000}: ", dt.ToString("yyyy-MM-dd HH:mm:ss.fff"), type, System.Threading.Thread.CurrentThread.ManagedThreadId);
                if (obj == null)
                {
                    sb.Append(formatStr);
                }
                else
                {
                    sb.AppendFormat(formatStr, obj);
                }

                string str = sb.ToString();

                lock (m_Lock)
                {
                    try
                    {
                        using (StreamWriter sw = File.AppendText(m_FileName))
                        {
                            sw.WriteLine(str);
                        }


                        FileInfo fileInfo = new FileInfo(m_FileName);
                        if (fileInfo.Length > m_iMaxSizeByte)
                        {
                            File.Move(m_FileName, m_FileName + dt.ToString("yyMMddHHmmss"));
                            // TODO : CleanUp / Archive etc.
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
