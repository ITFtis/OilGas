using Dou.Controllers;
using Dou.Models.DB;
using Microsoft.Ajax.Utilities;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Admin
{
    [Dou.Misc.Attr.MenuDef(Id = "PublicFacility", Name = "臺灣地區公共設施匯入", MenuPath = "系統管理專區/H臺灣地區公共設施匯入", Action = "Index", Index = 3, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class PublicFacilityController : Controller
    {
        private OilGasModelContextExt _db = new OilGasModelContextExt();
        // GET: PublicFacility
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public JsonResult Sendupload(HttpPostedFileBase filePF)
        {
            //NPOI解析檔案
            if (filePF != null)
            {
                Stream stream = filePF.InputStream;
                DataTable dataTable = new DataTable();
                IWorkbook wb;
                ISheet sheet;
                IRow headerRow;
                int cellCount;
                int sheetCount;

                try
                {
                    if (filePF.FileName.ToLower().EndsWith("xlsx"))
                    {
                        wb = new XSSFWorkbook(stream);
                    }
                    else
                    {
                        wb = new HSSFWorkbook(stream);
                    }

                    sheetCount = wb.NumberOfSheets;

                    //抓第一個頁籤
                    sheet = wb.GetSheetAt(0);
                    

                    //取第一頁籤第一列
                    headerRow = sheet.GetRow(0);

                    //計算共有多少欄位
                    cellCount = headerRow.LastCellNum;

                    //把excel的欄位塞到datatable當欄位
                    for(int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        dataTable.Columns.Add(new DataColumn(headerRow.GetCell(i).StringCellValue));
                    }

                    //略過第0列 開始處理內容
                    for(int i = sheet.FirstRowNum + 1;i <= sheet.LastRowNum; i++)
                    {
                        //取得目前的row
                        IRow row = sheet.GetRow(i);

                        DataRow dataRow = dataTable.NewRow();
                        ICell cell;

                        for(int k = row.FirstCellNum; k < row.LastCellNum; k++)
                        {
                            cell = row.GetCell(k);

                            dataRow[k] = cell;
                        }
                        //datatable 加入row
                        dataTable.Rows.Add(dataRow);
                    }

                    InsertIntoDB(dataTable);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sheet = null;
                    wb = null;
                    stream.Dispose();
                    stream.Close();
                }
            }
            else
            {
                return Json("未上傳檔案");
            }
            return Json("ok");
        }

        private void InsertIntoDB(DataTable dataTable)
        {
            //寫入DB
            foreach(DataRow dataRow in dataTable.Rows)
            {
                var pf = convertToPF(dataRow);

                try
                {
                    Dou.Models.DB.IModelEntity<PublicFacility> publicFacility = new Dou.Models.DB.ModelEntity<PublicFacility>(_db);
                    publicFacility.Add(pf);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                
            }
        }

        private PublicFacility convertToPF(DataRow dataRow)
        {
            var pf = new PublicFacility()
            {
                Place_name = convertValue(dataRow["Place_name"]),
                Chinese_phonetic = convertValue(dataRow["Chinese_phonetic"]),
                Common_phonetic = convertValue(dataRow["Common_phonetic"]),
                Another_name = convertValue(dataRow["Another_name"]),
                County = convertValue(dataRow["County"]),
                Town = convertValue(dataRow["Town"]),
                Village =   convertValue(dataRow["Village"]),
                Place_mean = convertValue(dataRow["Place_mean"]),
                Year_f = convertValue(dataRow["Year_f"]),
                Year_l = convertValue(dataRow["Year_l"]),
                Place_type = convertValue(dataRow["Place_type"]),
                Language = convertValue(dataRow["Language"]),
                Denominate = convertValue(dataRow["Denominate"]),
                Place_describe = convertValue(dataRow["Place_describe"]),
                History_describe = convertValue(dataRow["History_describe"]),
                Place_content = convertValue(dataRow["Place_content"]),
                Map_ref = convertValue(dataRow["Map_ref"]),
                X = convertValue(dataRow["X"]),
                Y = convertValue(dataRow["Y"]),
            };
            return pf;
            
        }

        private string convertValue(object v)
        {
            return string.IsNullOrEmpty(v.ToString()) ? null : v.ToString();
        }

        
    }
}