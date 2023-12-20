using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.Info
{
    [Dou.Misc.Attr.MenuDef(Id = "SalesAnalysis", Name = "銷售分析表產出", MenuPath = "資訊查詢/I檔案下載", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class SalesAnalysisController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public string Sendupload(HttpPostedFileBase fileCPC, HttpPostedFileBase fileFPG)
        {
            Stream fileFPGfileStream = fileFPG.InputStream;
            Stream fileCPCfileStream = fileCPC.InputStream;

            //讀取EXCEL
            var FPG = fileFPGToStatisticsexcel(fileFPGfileStream, Path.GetExtension(fileFPG.FileName).ToLower());
            var CPC = fileFPGToStatisticsexcel(fileCPCfileStream, Path.GetExtension(fileCPC.FileName).ToLower());


            //組成銷售統計表
            var AllExcel = (from a in FPG.statisticsexcel
                            join b in CPC.statisticsexcel on a.city.Replace("　", "").Replace(" ", "") equals b.city.Replace("　", "").Replace(" ", "")
                            select new statisticsexcel
                            {
                                city = a.city,
                                station = (float.Parse(a.station.Replace(",", "")) + float.Parse(b.station.Replace(",", ""))).ToString(),
                                oil = (float.Parse(a.oil.Replace(",", "")) + float.Parse(b.oil.Replace(",", ""))).ToString(),
                                diesel = (float.Parse(a.diesel.Replace(",", "")) + float.Parse(b.diesel.Replace(",", ""))).ToString(),
                                sum = (float.Parse(a.sum.Replace(",", "")) + float.Parse(b.sum.Replace(",", ""))).ToString(),
                                statistics = ((float.Parse(a.statistics.Replace(",", "")) * float.Parse(a.station.Replace(",", "")) + float.Parse(b.statistics.Replace(",", "")) * float.Parse(b.station.Replace(",", ""))) / (float.Parse(a.station.Replace(",", "")) + float.Parse(b.station.Replace(",", "")))).ToString(),
                            }).ToList<statisticsexcel>();

            //組成銷售分析表
            var AllExcel2 = (from a in FPG.statisticsexcel2
                             join b in CPC.statisticsexcel2 on a.city.Replace("　", "").Replace(" ", "") equals b.city.Replace("　", "").Replace(" ", "")
                             select new statisticsexcel2
                             {
                                 city = a.city,
                                 under5 = (float.Parse(a.under5.Replace(",", "")) + float.Parse(b.under5.Replace(",", ""))).ToString(),
                                 to10 = (float.Parse(a.to10.Replace(",", "")) + float.Parse(b.to10.Replace(",", ""))).ToString(),
                                 to15 = (float.Parse(a.to15.Replace(",", "")) + float.Parse(b.to15.Replace(",", ""))).ToString(),
                                 to20 = (float.Parse(a.to20.Replace(",", "")) + float.Parse(b.to20.Replace(",", ""))).ToString(),
                                 to25 = (float.Parse(a.to25.Replace(",", "")) + float.Parse(b.to25.Replace(",", ""))).ToString(),
                                 to30 = (float.Parse(a.to30.Replace(",", "")) + float.Parse(b.to30.Replace(",", ""))).ToString(),
                                 to40 = (float.Parse(a.to40.Replace(",", "")) + float.Parse(b.to40.Replace(",", ""))).ToString(),
                                 over40 = (float.Parse(a.over40.Replace(",", "")) + float.Parse(b.over40.Replace(",", ""))).ToString(),
                                 sum = (float.Parse(a.sum.Replace(",", "")) + float.Parse(b.sum.Replace(",", ""))).ToString()

                             }).ToList<statisticsexcel2>();

            //產出新的excel
            CreateNewExcelFile(AllExcel, AllExcel2);

            return OilGas.Cm.PhysicalToUrl(FileHelper.GetFileFolder(Code.TempUploadFile.銷售分析表產出)) + "各縣市加油站汽柴油銷售分析表.xlsx";
        }

        //讀取EXCEL
        public excel fileFPGToStatisticsexcel(Stream fileFPG, string fileExtension)
        {
            IWorkbook workbook;


            // 根據文件擴展名創建對應的工作簿對象
            if (fileExtension == ".xls")
            {
                workbook = new HSSFWorkbook(fileFPG);
            }
            else if (fileExtension == ".xlsx")
            {
                workbook = new XSSFWorkbook(fileFPG);
            }
            else
            {
                return null;
            }







            // 讀取工作簿中的第一個工作表(銷售統計表)
            ISheet sheet = workbook.GetSheetAt(0);
            var statisticsexcelList = excelList(sheet);//將銷售統計表組成list







            // 讀取工作簿中的第二個工作表(銷售分析表)
            ISheet sheet2 = workbook.GetSheetAt(1);
            var statisticsexcelList2 = excelList2(sheet2);//將銷售分析表組成list



            workbook.Close();
            excel list = new excel()
            {
                statisticsexcel = statisticsexcelList,
                statisticsexcel2 = statisticsexcelList2
            };

            return list;
        }

        //產出新的excel
        public void CreateNewExcelFile(List<statisticsexcel> List, List<statisticsexcel2> List2)
        {
            // 創建一個新的工作簿（此處以 XLSX 格式為例）
            IWorkbook workbook = new XSSFWorkbook();

            // 在工作簿中創建一個新的工作表(銷售統計表)
            ISheet sheet = workbook.CreateSheet("銷售統計表");
            fillsheet(sheet, List);// 填充銷售統計表

            // 在工作簿中創建一個新的工作表(銷售分析表)
            ISheet sheet2 = workbook.CreateSheet("銷售分析表");
            fillsheet2(sheet2, List2);// 填充銷售分析表


            string filePath = FileHelper.GetFileFolder(Code.TempUploadFile.銷售分析表產出);
            // 將工作簿寫入文件
            FileStream fileStream = new FileStream(filePath + "各縣市加油站汽柴油銷售分析表.xlsx", FileMode.Create, FileAccess.Write);
            workbook.Write(fileStream);

        }



        // 填充銷售分析表
        public void fillsheet2(ISheet sheet, List<statisticsexcel2> List)
        {
            // 創建一些行和單元格並填充數據
            for (int rownum = 0; rownum < 29; rownum++)
            {
                if (rownum == 0)
                {
                    IRow row = sheet.CreateRow(rownum);
                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue("【附表2】");

                }
                else if (rownum == 1)
                {
                    IRow row = sheet.CreateRow(rownum);
                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue("XX年XX月份各縣市汽車加油站汽、柴油銷售量分析表");

                }
                else if (rownum == 2)
                {
                    IRow row = sheet.CreateRow(rownum);
                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue("( 單位：站 )");
                }
                else if (rownum == 3)
                {
                    IRow row = sheet.CreateRow(rownum);
                    row.CreateCell(0).SetCellValue("縣市別");
                    row.CreateCell(1).SetCellValue("銷售量：( 公秉/日)");
                }
                else if (rownum == 4)
                {
                    IRow row = sheet.CreateRow(rownum);
                    row.CreateCell(1).SetCellValue("5以下");
                    row.CreateCell(2).SetCellValue("5.1~10");
                    row.CreateCell(3).SetCellValue("10.1~15");
                    row.CreateCell(4).SetCellValue("15.1~20");
                    row.CreateCell(5).SetCellValue("20.1~25");
                    row.CreateCell(6).SetCellValue("25.1~30");
                    row.CreateCell(7).SetCellValue("30.1~40");
                    row.CreateCell(8).SetCellValue("40.1以上");
                    row.CreateCell(9).SetCellValue("合　計");
                }
                else if (rownum == 28)
                {
                    IRow row = sheet.CreateRow(rownum);
                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue("資料來源:台灣中油股份有限公司及台塑石化股份有限公司");
                }
                else
                {
                    IRow row = sheet.CreateRow(rownum);
                    for (int cellnum = 0; cellnum < 10; cellnum++)
                    {

                        switch (cellnum)
                        {
                            case 0:
                                ICell cell0 = row.CreateCell(cellnum);
                                cell0.SetCellValue(List[rownum - 5].city);//前面有5格
                                break;
                            case 1:
                                ICell cell1 = row.CreateCell(cellnum);
                                cell1.SetCellValue(List[rownum - 5].under5);
                                break;
                            case 2:
                                ICell cell2 = row.CreateCell(cellnum);
                                cell2.SetCellValue(List[rownum - 5].to10);
                                break;
                            case 3:
                                ICell cell3 = row.CreateCell(cellnum);
                                cell3.SetCellValue(List[rownum - 5].to15);
                                break;
                            case 4:
                                ICell cell4 = row.CreateCell(cellnum);
                                cell4.SetCellValue(List[rownum - 5].to20);
                                break;
                            case 5:
                                ICell cell5 = row.CreateCell(cellnum);
                                cell5.SetCellValue(List[rownum - 5].to25);
                                break;

                            case 6:
                                ICell cell6 = row.CreateCell(cellnum);
                                cell6.SetCellValue(List[rownum - 5].to30);
                                break;
                            case 7:
                                ICell cell7 = row.CreateCell(cellnum);
                                cell7.SetCellValue(List[rownum - 5].to40);
                                break;
                            case 8:
                                ICell cell8 = row.CreateCell(cellnum);
                                cell8.SetCellValue(List[rownum - 5].over40);
                                break;
                            case 9:
                                ICell cell9 = row.CreateCell(cellnum);
                                cell9.SetCellValue(List[rownum - 5].sum);
                                break;

                        };

                    }
                }
            }



            // 合併單元格（合併第一行的前三列）
            // CellRangeAddress 參數：起始行號，結束行號，起始列號，結束列號
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, 5));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, 9));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 4, 0, 0));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 1, 9));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(28, 28, 0, 9));
        }

        // 填充銷售統計表
        public void fillsheet(ISheet sheet, List<statisticsexcel> List)
        {
            // 創建一些行和單元格並填充數據
            for (int rownum = 0; rownum < 28; rownum++)
            {
                if (rownum == 0)
                {
                    IRow row = sheet.CreateRow(rownum);
                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue("【附表1】");

                }
                else if (rownum == 1)
                {
                    IRow row = sheet.CreateRow(rownum);
                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue("XX年XX月份各縣市汽車加油站汽、柴油銷售量統計表");

                }
                else if (rownum == 2)
                {
                    IRow row = sheet.CreateRow(rownum);
                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue("( 單位：公秉 )");
                }
                else if (rownum == 3)
                {
                    IRow row = sheet.CreateRow(rownum);
                    row.CreateCell(0).SetCellValue("縣市別");
                    row.CreateCell(1).SetCellValue("站　數");
                    row.CreateCell(2).SetCellValue("汽　油");
                    row.CreateCell(3).SetCellValue("柴　油");
                    row.CreateCell(4).SetCellValue("合　計");
                    row.CreateCell(5).SetCellValue("公秉／日‧站");
                }
                else if (rownum == 27)
                {
                    IRow row = sheet.CreateRow(rownum);
                    ICell cell = row.CreateCell(0);
                    cell.SetCellValue("資料來源: 台灣中油股份有限公司及台塑石化股份有限公司");
                }
                else
                {
                    IRow row = sheet.CreateRow(rownum);
                    for (int cellnum = 0; cellnum < 6; cellnum++)
                    {

                        switch (cellnum)
                        {
                            case 0:
                                ICell cell0 = row.CreateCell(cellnum);
                                cell0.SetCellValue(List[rownum - 4].city);//前面有4格
                                break;
                            case 1:
                                ICell cell1 = row.CreateCell(cellnum);
                                cell1.SetCellValue(List[rownum - 4].station);
                                break;
                            case 2:
                                ICell cell2 = row.CreateCell(cellnum);
                                cell2.SetCellValue(List[rownum - 4].oil);
                                break;
                            case 3:
                                ICell cell3 = row.CreateCell(cellnum);
                                cell3.SetCellValue(List[rownum - 4].diesel);
                                break;
                            case 4:
                                ICell cell4 = row.CreateCell(cellnum);
                                cell4.SetCellValue(List[rownum - 4].sum);
                                break;
                            case 5:
                                ICell cell5 = row.CreateCell(cellnum);
                                cell5.SetCellValue(List[rownum - 4].statistics);
                                break;
                        };

                    }
                }
            }



            // 合併單元格（合併第一行的前三列）
            // CellRangeAddress 參數：起始行號，結束行號，起始列號，結束列號
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, 5));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, 5));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(27, 27, 0, 5));
        }
       

        //將銷售統計表組成list
        public List<statisticsexcel> excelList(ISheet sheet)
        {
            List<statisticsexcel> statisticsexcelList = new List<statisticsexcel>();
            statisticsexcel statisticsexcel = new statisticsexcel();
            // 遍歷工作表中的行
            //for (int row = 4; row <= sheet.LastRowNum; row++)
            for (int row = 4; row < 27; row++)
            {
                if (sheet.GetRow(row) != null) // null 是因為可能有空行
                {
                    //重製statisticsexcel
                    statisticsexcel = new statisticsexcel();
                    // 遍歷行中的單元格
                    for (int cell = 0; cell < sheet.GetRow(row).LastCellNum; cell++)
                    {

                        // 讀取這個單元格的內容
                        var celldata = sheet.GetRow(row).GetCell(cell);
                        string value = "0";



                        if (celldata != null && celldata.CellType == CellType.Formula)
                        {
                            // 處理公式
                            switch (celldata.CachedFormulaResultType)
                            {
                                case CellType.Numeric:
                                    value = celldata.NumericCellValue.ToString();
                                    break;

                            }
                        }
                        else
                        {
                            value = celldata != null ? celldata.ToString() : "0";
                        }











                        switch (cell)
                        {
                            case 0:
                                statisticsexcel.city = value;
                                break;
                            case 1:
                                statisticsexcel.station = value;
                                break;
                            case 2:
                                statisticsexcel.oil = value;
                                break;
                            case 3:
                                statisticsexcel.diesel = value;
                                break;
                            case 4:
                                statisticsexcel.sum = value;
                                break;
                            case 5:
                                statisticsexcel.statistics = value;
                                break;

                        };


                    }
                    statisticsexcelList.Add(statisticsexcel);
                }
            }



            return statisticsexcelList;


        }


        //將銷售分析表組成list
        public List<statisticsexcel2> excelList2(ISheet sheet2)
        {
            List<statisticsexcel2> statisticsexcelList2 = new List<statisticsexcel2>();
            statisticsexcel2 statisticsexcel2 = new statisticsexcel2();
            // 遍歷工作表中的行
            for (int row = 5; row < 28; row++)
            {
                if (sheet2.GetRow(row) != null) // null 是因為可能有空行
                {
                    //重製statisticsexcel
                    statisticsexcel2 = new statisticsexcel2();
                    // 遍歷行中的單元格
                    for (int cell = 0; cell < sheet2.GetRow(row).LastCellNum; cell++)
                    {

                        // 讀取這個單元格的內容
                        var celldata = sheet2.GetRow(row).GetCell(cell);
                        string value = "0";



                        if (celldata != null && celldata.CellType == CellType.Formula)
                        {
                            // 處理公式
                            switch (celldata.CachedFormulaResultType)
                            {
                                case CellType.Numeric:
                                    value = celldata.NumericCellValue.ToString();
                                    break;

                            }
                        }
                        else
                        {
                            value = celldata != null ? celldata.ToString() : "0";
                        }



                        switch (cell)
                        {
                            case 0:
                                statisticsexcel2.city = value;
                                break;
                            case 1:
                                statisticsexcel2.under5 = value;
                                break;
                            case 2:
                                statisticsexcel2.to10 = value;
                                break;
                            case 3:
                                statisticsexcel2.to15 = value;
                                break;
                            case 4:
                                statisticsexcel2.to20 = value;
                                break;
                            case 5:
                                statisticsexcel2.to25 = value;
                                break;
                            case 6:
                                statisticsexcel2.to30 = value;
                                break;
                            case 7:
                                statisticsexcel2.to40 = value;
                                break;
                            case 8:
                                statisticsexcel2.over40 = value;
                                break;
                            case 9:
                                statisticsexcel2.sum = value;
                                break;

                        };


                    }
                    statisticsexcelList2.Add(statisticsexcel2);
                }
            }
            return statisticsexcelList2;
        }
      


        public class excel
        {
            public List<statisticsexcel> statisticsexcel { get; set; }
            public List<statisticsexcel2> statisticsexcel2 { get; set; }
        }




        public class statisticsexcel2
        {
            public string city { get; set; }
            public string under5 { get; set; }
            public string to10 { get; set; }
            public string to15 { get; set; }
            public string to20 { get; set; }
            public string to25 { get; set; }
            public string to30 { get; set; }
            public string to40 { get; set; }
            public string over40 { get; set; }
            public string sum { get; set; }
        }





        public class statisticsexcel
        {
            public string city { get; set; }
            public string station { get; set; }
            public string oil { get; set; }
            public string diesel { get; set; }
            public string sum { get; set; }
            public string statistics { get; set; }
        }
    }
}