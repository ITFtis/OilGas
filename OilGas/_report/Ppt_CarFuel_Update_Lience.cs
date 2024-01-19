using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OilGas._report
{
    public class Ppt_CarFuel_Update_Lience : ReportClass
    {
        public string Export(IQueryable<Models.WS_GSM> query)
        {
            try
            {
                //複製範本
                string sourcePath = FileHelper.GetTempleteFolder() + "未對應清單.xlsx";

                string fileName = System.IO.Path.GetFileNameWithoutExtension(sourcePath) + "_" + DateTime.Now.ToString("yyyy-MM-dd_") + ".xlsx";
                string toFolder = FileHelper.GetFileFolder(Code.TempUploadFile.範本_未對應清單);

                if (!Directory.Exists(toFolder))
                {
                    Directory.CreateDirectory(toFolder);
                }

                string toPath = toFolder + fileName;
                File.Copy(sourcePath, toPath, true);

                var data = query.ToList();

                //編輯範本檔
                XSSFWorkbook workbook = null;
                XSSFSheet sheet = null;
                FileStream xlsFile = new FileStream(toPath, FileMode.Open, FileAccess.ReadWrite);
                workbook = new XSSFWorkbook(xlsFile);
                xlsFile.Close();
                sheet = (XSSFSheet)workbook.GetSheetAt(0);
                workbook.SetSheetName(workbook.GetSheetIndex(sheet), "未對應清單");

                IRow row;

                //編輯主體
                for (var i = 0; i < data.Count; i++)
                {
                    row = sheet.GetRow(i + 3);
                    var c1 = row.Cells[0];
                    var c2 = row.Cells[1];
                    var c3 = row.Cells[2];
                    var c4 = row.Cells[3];

                    c1.SetCellValue(data[i].gsm_id);
                    c2.SetCellValue(data[i].gsm_name);
                    c3.SetCellValue(data[i].gsm_field03);
                    c4.SetCellValue(data[i].Situation);
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