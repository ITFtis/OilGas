using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;

namespace OilGas
{
    /// <summary>
    /// 基本款Excel
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 產生Excel
        /// </summary>
        /// <param name="fileTitle">檔名開頭(報表統計_......)</param>
        /// <param name="list">多個Sheet資料</param>
        /// <param name="savePath">儲存路徑</param>
        /// <param name="autoSizeColumn">(Y/N)自動調整儲存格長度(效能影響-資料量多)</param>
        /// <returns>Excel檔名</returns>
        public static string GenerateExcelByLinq(string fileTitle, List<dynamic> list, string savePath, string autoSizeColumn = "Y")
        {
            string fileName = "";

            if (list.Count == 0)
            {
                return "ExcelListCount_0";
            }

            HSSFWorkbook workbook = new HSSFWorkbook();

            //sheet區分：所屬部門代碼
            var sheets = list.GroupBy(x => x.SheetName);
            foreach (var sheet in sheets)
            {
                string sheetName = sheet.Key;

                List<string> headerName = new List<string>();
                foreach (var row in sheet)
                {
                    foreach (var v in row)
                    {
                        string key = v.Key.ToString();
                        if (key != "SheetName")
                            headerName.Add(key);
                    }
                    break;
                }

                HSSFSheet mySheet1 = (HSSFSheet)workbook.CreateSheet(sheetName);
                HSSFRow rowHeader = (HSSFRow)mySheet1.CreateRow(0);
                //建立 Header
                for (int j = 0; j < headerName.Count; j++)
                {
                    rowHeader.CreateCell(j).SetCellValue(headerName[j]);
                }
                //建立內容
                foreach (var row in sheet)
                {
                    int index = mySheet1.LastRowNum;

                    HSSFRow rowItem = (HSSFRow)mySheet1.CreateRow(index + 1);

                    foreach (var v in row)
                    {
                        string key = v.Key.ToString();
                        if (key == "SheetName")
                            continue;

                        int l = rowItem.Cells.Count;
                        object value = v.Value;

                        Type t;
                        if (value == null)
                        {
                            value = "";
                            t = typeof(string);
                        }
                        else
                        {
                            t = value.GetType();
                        }
                        
                        if (t == typeof(string))
                        {
                            rowItem.CreateCell(l).SetCellValue(value.ToString());
                        }
                        else if (t == typeof(DateTime))
                        {
                            rowItem.CreateCell(l).SetCellValue(value == null ? String.Empty : OilGas.DateFormat.ToDate1(value.ToString()));
                        }
                        else if (t == typeof(int))
                        {
                            rowItem.CreateCell(l).SetCellValue(value == null ? 0 : int.Parse(value.ToString()));
                        }
                        else if (t == typeof(double))
                        {
                            rowItem.CreateCell(l).SetCellValue(value == null ? 0 : double.Parse(value.ToString()));
                        }
                        else
                        {
                            rowItem.CreateCell(l).SetCellValue(value.ToString());
                        }
                    }


                }

                if (autoSizeColumn == "Y")
                {
                    if (mySheet1.LastRowNum > 0)
                    {
                        //有資料列
                        int columnCount = ((ICollection<KeyValuePair<string, Object>>)sheet.First()).Count;

                        for (int j = 0; j < columnCount; j++)
                        {
                            mySheet1.AutoSizeColumn(j);
                        }
                    }
                }

            }

            //匯出
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
            string filePathName = "";
            int i = 0;
            bool exist = false;

            fileName = fileTitle + "_" + DateTime.Now.ToString("yyyy-MM-dd_") + Guid.NewGuid() + ".xls";
            filePathName = savePath + @"\" + fileName;

            FileStream file = new FileStream(filePathName, FileMode.Create);
            workbook.Write(file);
            file.Close();
            workbook = null;
            return fileName;
        }
    }
}