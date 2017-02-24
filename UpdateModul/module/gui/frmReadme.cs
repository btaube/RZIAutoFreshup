using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using UpdateModul;

namespace UpdateModul
{
  public partial class frmReadme : Form
  {
    private String m_DownloadPath;

    public frmReadme(String DownloadPath)
    {
      InitializeComponent();

      m_DownloadPath = DownloadPath;
    }

    private void butOK_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void frmReadme_Load(object sender, EventArgs e)
    {
      try
      {
        WebRequest wr = HttpWebRequest.Create(m_DownloadPath);
        HttpWebResponse ws = (HttpWebResponse)wr.GetResponse();

        Stream str = ws.GetResponseStream();
        byte[] inBuf = new byte[1024];

        long comBytes = ws.ContentLength;
        long comBytesRead = 0;
        int bytesToRead = (int)(((comBytes - comBytesRead) > inBuf.Length) ? inBuf.Length : comBytes - comBytesRead);

        StringBuilder fstr = new StringBuilder();
        while (bytesToRead > 0)
        {
          int n = str.Read(inBuf, 0, bytesToRead);
          if (n == 0)
            break;
          else
            fstr.Append(Encoding.UTF8.GetString(inBuf, 0, n));

          comBytesRead += n;
          bytesToRead = (int)(((comBytes - comBytesRead) > inBuf.Length) ? inBuf.Length : comBytes - comBytesRead);
        }

        str.Close();
        rbReadme.Text = fstr.ToString();
      }
      catch (Exception exc)
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(String.Format(translations.MESSAGE_ERROR_DOWNLOAD, m_DownloadPath, exc.ToString()));
        sb.AppendLine("");
        rbReadme.Text = sb.ToString();
      }
    }

    private void frmReadme_Shown(object sender, EventArgs e)
    {
      BringToFront();
    }
  }
}
