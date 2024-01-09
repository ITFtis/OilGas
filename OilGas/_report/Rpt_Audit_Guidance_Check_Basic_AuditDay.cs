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
using OilGas.Models;
using System.Reflection;
using System.Text;


namespace OilGas
{
    public class Rpt_Audit_Guidance_Check_Basic_AuditDay : ReportClass
    {
        public string Export(string CaseNo)
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

            var dbContext = new OilGasModelContextExt();
            //日報表
            Dou.Models.DB.IModelEntity<Check_Basic_AuditDay> check_Basic_AuditDay = new Dou.Models.DB.ModelEntity<Check_Basic_AuditDay>(dbContext);
            //加油站
            Dou.Models.DB.IModelEntity<CarFuel_BasicData> carFuel_BasicData = new Dou.Models.DB.ModelEntity<CarFuel_BasicData>(dbContext);

            //只有一筆，方便就不寫join(basic)
            var v = check_Basic_AuditDay.GetAll().Where(a => a.CaseNo == CaseNo).FirstOrDefault();
            var basic = carFuel_BasicData.GetAll().Where(a => a.CaseNo == CaseNo).FirstOrDefault();

            if (v == null)
            {
                _errorMessage = "查無此案件編號:" + CaseNo;
                return "";
            }

            //編輯範本檔
            HSSFWorkbook workbook = null;
            HSSFSheet sheet = null;
            FileStream xlsFile = new FileStream(toPath, FileMode.Open, FileAccess.ReadWrite);
            workbook = new HSSFWorkbook(xlsFile);
            xlsFile.Close();
            sheet = (HSSFSheet)workbook.GetSheetAt(0);
            workbook.SetSheetName(workbook.GetSheetIndex(sheet), "日_自行安全檢查表");

            //Header Gas_Name            
            ICell c;
            c = sheet.GetRow(0).GetCell(0);
            c.SetCellValue(c.StringCellValue.Replace("Gas_Name", basic.Gas_Name));
            
            //Header
            if (v.CheckDate != null)
            {
                c = sheet.GetRow(1).GetCell(0);
                c.SetCellValue(c.StringCellValue
                    .Replace("yyyy", ((DateTime)v.CheckDate).Year.ToString())
                    .Replace("mm", ((DateTime)v.CheckDate).Month.ToString())
                    .Replace("dd", ((DateTime)v.CheckDate).Day.ToString()));
                c = sheet.GetRow(1).GetCell(4);
                //c.SetCellValue(c.StringCellValue.Replace("day", ((DateTime)v.CheckDate).DayOfWeek.ToString()));
                c.SetCellValue(c.StringCellValue.Replace("星期 day", DateFormat.ToDate13(v.CheckDate)));
            }
            c = sheet.GetRow(1).GetCell(6);
            var weather = Code.GetWeather().Where(a => a.Key.Contains(v.Weather.ToString()));
            c.SetCellValue(c.StringCellValue.Replace("Weather", weather.Count() == 0 ? "" : weather.FirstOrDefault().Value.ToString()));
            c = sheet.GetRow(1).GetCell(7);
            c.SetCellValue(c.StringCellValue.Replace("CheckMan", v.CheckMan));
            
            //A01
            c = sheet.GetRow(3).GetCell(2);
            c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.A01, "□")));
            sheet.GetRow(3).GetCell(7).SetCellValue(v.A01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.A01_Way.ToString()).FirstOrDefault().Value.ToString());
            sheet.GetRow(3).GetCell(8).SetCellValue(v.A01_Improve);
            sheet.GetRow(3).GetCell(9).SetCellValue(v.A01_Note);

            

            xlsFile = new FileStream(toPath, FileMode.Open, FileAccess.ReadWrite);
            workbook.Write(xlsFile);
            xlsFile.Close();
            workbook.Close();

            return OilGas.Cm.PhysicalToUrl(toPath);
        }
    }
}