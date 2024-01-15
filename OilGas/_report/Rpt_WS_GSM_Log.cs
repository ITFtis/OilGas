using Dou.Models.DB;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OilGas._report
{
    public class Rpt_WS_GSM_Log:ReportClass
    {
        public string Export()
        {
            try
            {
                //複製範本
                string sourcePath = FileHelper.GetTempleteFolder() + "資料交換紀錄.xlsx";

                string fileName = System.IO.Path.GetFileNameWithoutExtension(sourcePath) + "_" + DateTime.Now.ToString("yyyy-MM-dd_") + ".xlsx";
                string toFolder = FileHelper.GetFileFolder(Code.TempUploadFile.範本_資料交換紀錄);

                if (!Directory.Exists(toFolder))
                {
                    Directory.CreateDirectory(toFolder);
                }

                string toPath = toFolder + fileName;
                File.Copy(sourcePath, toPath, true);

                //取得資料
                IModelEntity<WS_GSM_Log> model = new ModelEntity<WS_GSM_Log>(new OilGasModelContextExt());
                var data = model.GetAll().OrderByDescending(x => x.Sys_date).ToList();
                

                //編輯範本檔
                XSSFWorkbook workbook = null;
                XSSFSheet sheet = null;
                FileStream xlsFile = new FileStream(toPath, FileMode.Open, FileAccess.ReadWrite);
                workbook = new XSSFWorkbook(xlsFile);
                xlsFile.Close();
                sheet = (XSSFSheet)workbook.GetSheetAt(0);
                workbook.SetSheetName(workbook.GetSheetIndex(sheet), "資料交換紀錄");

                IRow row;

                //編輯主體
                for (var i = 0; i < data.Count; i++)
                {
                    row = sheet.GetRow(i + 3);
                    var c1 = row.Cells[0];
                    var c2 = row.Cells[1];

                    c1.SetCellValue(data[i].DataCount);
                    c2.SetCellValue(data[i].ViewDate);
                    
                }

                xlsFile = new FileStream(toPath, FileMode.Create, FileAccess.Write);
                workbook.Write(xlsFile);
                xlsFile.Close();
                workbook.Close();

                return OilGas.Cm.PhysicalToUrl(toPath);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return "";
            }
        }
    }
}