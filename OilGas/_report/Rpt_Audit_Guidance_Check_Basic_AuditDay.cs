using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.HSSF.Model;
using NPOI.SS.UserModel;


namespace OilGas
{
    public class Rpt_Audit_Guidance_Check_Basic_AuditDay : ReportClass
    {
        public string Export(string CaseNo, string ext)
        {
            //複製範本
            string sourcePath = HttpContext.Current.Server.MapPath(string.Format(@"~/DocsWeb/範本_日_自行安全檢查表_113年系統.xls"));
            
            string fileName = System.IO.Path.GetFileName(sourcePath) + "_" + DateTime.Now.ToString("yyyy-MM-dd_") + ".xls";            
            string toFolder = FileHelper.GetFileFolder(Code.TempUploadFile.範本_日_自行安全檢查表_113年系統);

            if (!Directory.Exists(toFolder))
            {
                Directory.CreateDirectory(toFolder);
            }

            string toPath = toFolder + fileName;
            File.Copy(sourcePath, toPath, true);

            return "aaaa";
        }
    }
}