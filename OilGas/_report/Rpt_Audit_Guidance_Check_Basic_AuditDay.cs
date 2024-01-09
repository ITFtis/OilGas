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
using NPOI.XSSF.UserModel;


namespace OilGas
{
    public class Rpt_Audit_Guidance_Check_Basic_AuditDay : ReportClass
    {
        public string Export(string CaseNo)
        {
            try
            {
                //複製範本
                string sourcePath = FileHelper.GetTempleteFolder() + "範本_日_自行安全檢查表_113年系統.xlsx";

                string fileName = System.IO.Path.GetFileNameWithoutExtension(sourcePath) + "_" + DateTime.Now.ToString("yyyy-MM-dd_") + ".xlsx";
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
                XSSFWorkbook workbook = null;
                XSSFSheet sheet = null;
                FileStream xlsFile = new FileStream(toPath, FileMode.Open, FileAccess.ReadWrite);
                workbook = new XSSFWorkbook(xlsFile);
                xlsFile.Close();
                sheet = (XSSFSheet)workbook.GetSheetAt(0);
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

                //A02
                c = sheet.GetRow(4).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.A02, "□")));
                sheet.GetRow(4).GetCell(7).SetCellValue(v.A02_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.A02_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(4).GetCell(8).SetCellValue(v.A02_Improve);
                sheet.GetRow(4).GetCell(9).SetCellValue(v.A02_Note);

                //A03
                c = sheet.GetRow(5).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.A03, "□")));
                sheet.GetRow(5).GetCell(7).SetCellValue(v.A03_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.A03_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(5).GetCell(8).SetCellValue(v.A03_Improve);
                sheet.GetRow(5).GetCell(9).SetCellValue(v.A03_Note);

                //A04
                c = sheet.GetRow(6).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.A04, "□")));
                sheet.GetRow(6).GetCell(7).SetCellValue(v.A04_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.A04_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(6).GetCell(8).SetCellValue(v.A04_Improve);
                sheet.GetRow(6).GetCell(9).SetCellValue(v.A04_Note);

                //A05
                c = sheet.GetRow(7).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.A05, "□")));
                sheet.GetRow(7).GetCell(7).SetCellValue(v.A05_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.A05_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(7).GetCell(8).SetCellValue(v.A05_Improve);
                sheet.GetRow(7).GetCell(9).SetCellValue(v.A05_Note);

                //A06
                c = sheet.GetRow(8).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.A06, "□")));
                sheet.GetRow(8).GetCell(7).SetCellValue(v.A06_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.A06_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(8).GetCell(8).SetCellValue(v.A06_Improve);
                sheet.GetRow(8).GetCell(9).SetCellValue(v.A06_Note);

                //B01
                c = sheet.GetRow(9).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.B01, "□")));
                sheet.GetRow(9).GetCell(7).SetCellValue(v.B01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.B01_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(9).GetCell(8).SetCellValue(v.B01_Improve);
                sheet.GetRow(9).GetCell(9).SetCellValue(v.B01_Note);

                //B02
                c = sheet.GetRow(10).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.B02, "□")));
                sheet.GetRow(10).GetCell(7).SetCellValue(v.B02_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.B02_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(10).GetCell(8).SetCellValue(v.B02_Improve);
                sheet.GetRow(10).GetCell(9).SetCellValue(v.B02_Note);

                //C01
                c = sheet.GetRow(11).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.C01, "□")));
                sheet.GetRow(11).GetCell(7).SetCellValue(v.C01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.C01_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(11).GetCell(8).SetCellValue(v.C01_Improve);
                sheet.GetRow(11).GetCell(9).SetCellValue(v.C01_Note);

                //C02
                c = sheet.GetRow(12).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.C02, "□")));
                sheet.GetRow(12).GetCell(7).SetCellValue(v.C02_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.C02_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(12).GetCell(8).SetCellValue(v.C02_Improve);
                sheet.GetRow(12).GetCell(9).SetCellValue(v.C02_Note);

                //C03
                c = sheet.GetRow(13).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.C03, "□")));
                sheet.GetRow(13).GetCell(7).SetCellValue(v.C03_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.C03_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(13).GetCell(8).SetCellValue(v.C03_Improve);
                sheet.GetRow(13).GetCell(9).SetCellValue(v.C03_Note);

                //D01
                c = sheet.GetRow(14).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.D01, "□")));
                sheet.GetRow(14).GetCell(7).SetCellValue(v.D01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.D01_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(14).GetCell(8).SetCellValue(v.D01_Improve);
                sheet.GetRow(14).GetCell(9).SetCellValue(v.D01_Note);

                //D02
                c = sheet.GetRow(15).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.D02, "□")));
                sheet.GetRow(15).GetCell(7).SetCellValue(v.D02_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.D02_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(15).GetCell(8).SetCellValue(v.D02_Improve);
                sheet.GetRow(15).GetCell(9).SetCellValue(v.D02_Note);

                //D03
                c = sheet.GetRow(16).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.D03, "□")));
                sheet.GetRow(16).GetCell(7).SetCellValue(v.D03_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.D03_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(16).GetCell(8).SetCellValue(v.D03_Improve);
                sheet.GetRow(16).GetCell(9).SetCellValue(v.D03_Note);

                //D04
                c = sheet.GetRow(17).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.D04, "□")));
                sheet.GetRow(17).GetCell(7).SetCellValue(v.D04_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.D04_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(17).GetCell(8).SetCellValue(v.D04_Improve);
                sheet.GetRow(17).GetCell(9).SetCellValue(v.D04_Note);

                //E01
                c = sheet.GetRow(18).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.E01, "□")));
                sheet.GetRow(18).GetCell(7).SetCellValue(v.E01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.E01_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(18).GetCell(8).SetCellValue(v.E01_Improve);
                sheet.GetRow(18).GetCell(9).SetCellValue(v.E01_Note);

                //E02
                c = sheet.GetRow(19).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.E02, "□")));
                sheet.GetRow(19).GetCell(7).SetCellValue(v.E02_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.E02_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(19).GetCell(8).SetCellValue(v.E02_Improve);
                sheet.GetRow(19).GetCell(9).SetCellValue(v.E02_Note);

                //E03
                c = sheet.GetRow(20).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.E03, "□")));
                sheet.GetRow(20).GetCell(7).SetCellValue(v.E03_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.E03_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(20).GetCell(8).SetCellValue(v.E03_Improve);
                sheet.GetRow(20).GetCell(9).SetCellValue(v.E03_Note);

                //F01
                c = sheet.GetRow(21).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.F01, "□")));
                sheet.GetRow(21).GetCell(7).SetCellValue(v.F01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.F01_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(21).GetCell(8).SetCellValue(v.F01_Improve);
                sheet.GetRow(21).GetCell(9).SetCellValue(v.F01_Note);

                //F02
                c = sheet.GetRow(22).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.F02, "□")));
                sheet.GetRow(22).GetCell(7).SetCellValue(v.F02_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.F02_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(22).GetCell(8).SetCellValue(v.F02_Improve);
                sheet.GetRow(22).GetCell(9).SetCellValue(v.F02_Note);

                //F03
                c = sheet.GetRow(23).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.F03, "□")));
                sheet.GetRow(23).GetCell(7).SetCellValue(v.F03_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.F03_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(23).GetCell(8).SetCellValue(v.F03_Improve);
                sheet.GetRow(23).GetCell(9).SetCellValue(v.F03_Note);

                //G01
                c = sheet.GetRow(24).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.G01, "□")));
                sheet.GetRow(24).GetCell(7).SetCellValue(v.G01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.G01_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(24).GetCell(8).SetCellValue(v.G01_Improve);
                sheet.GetRow(24).GetCell(9).SetCellValue(v.G01_Note);

                //H01
                c = sheet.GetRow(25).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.H01, "□")));
                sheet.GetRow(25).GetCell(7).SetCellValue(v.H01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.H01_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(25).GetCell(8).SetCellValue(v.H01_Improve);
                sheet.GetRow(25).GetCell(9).SetCellValue(v.H01_Note);

                //H02
                c = sheet.GetRow(26).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.H02, "□")));
                sheet.GetRow(26).GetCell(7).SetCellValue(v.H02_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.H02_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(26).GetCell(8).SetCellValue(v.H02_Improve);
                sheet.GetRow(26).GetCell(9).SetCellValue(v.H02_Note);

                //H03
                c = sheet.GetRow(27).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.H03, "□")));
                sheet.GetRow(27).GetCell(7).SetCellValue(v.H03_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.H03_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(27).GetCell(8).SetCellValue(v.H03_Improve);
                sheet.GetRow(27).GetCell(9).SetCellValue(v.H03_Note);

                //H04
                c = sheet.GetRow(28).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.H04, "□")));
                sheet.GetRow(28).GetCell(7).SetCellValue(v.H04_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.H04_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(28).GetCell(8).SetCellValue(v.H04_Improve);
                sheet.GetRow(28).GetCell(9).SetCellValue(v.H04_Note);

                //I01
                c = sheet.GetRow(29).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.I01, "□")));
                sheet.GetRow(29).GetCell(7).SetCellValue(v.I01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.I01_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(29).GetCell(8).SetCellValue(v.I01_Improve);
                sheet.GetRow(29).GetCell(9).SetCellValue(v.I01_Note);

                //xxxx
                c = sheet.GetRow(30).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.I02, "□")));
                sheet.GetRow(30).GetCell(7).SetCellValue(v.I02_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.I02_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(30).GetCell(8).SetCellValue(v.I02_Improve);
                sheet.GetRow(30).GetCell(9).SetCellValue(v.I02_Note);

                //J01
                c = sheet.GetRow(31).GetCell(2);
                c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.J01, "□")));
                sheet.GetRow(31).GetCell(7).SetCellValue(v.J01_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.J01_Way.ToString()).FirstOrDefault().Value.ToString());
                sheet.GetRow(31).GetCell(8).SetCellValue(v.J01_Improve);
                sheet.GetRow(31).GetCell(9).SetCellValue(v.J01_Note);

                //資料列Row高度調整
                for (int i = 3; i < 32; i++)
                {
                    IRow row = sheet.GetRow(i);

                    int c8 = (row.GetCell(8).ToString().Length / 6) + 1;    //改善情形(6字換列)
                    int c9 = (row.GetCell(9).ToString().Length / 4) + 1;    //備註(4字換列)
                    List<int> lens = new List<int> { c8, c9 };

                    //2列以上處理
                    if (lens.Max() > 1)
                    {
                        row.HeightInPoints = 16 * lens.Max();
                    }
                }

                //////xxxx
                ////c = sheet.GetRow(xxxx).GetCell(2);
                ////c.SetCellValue(c.StringCellValue.Replace("□", Code.GetCheckF1(v.XXXXX, "□")));
                ////sheet.GetRow(xxxx).GetCell(7).SetCellValue(v.xxxx_Way == null ? "" : Code.GetCheckWay().Where(a => a.Key == v.xxxx_Way.ToString()).FirstOrDefault().Value.ToString());
                ////sheet.GetRow(xxxx).GetCell(8).SetCellValue(v.xxxx_Improve);
                ////sheet.GetRow(xxxx).GetCell(9).SetCellValue(v.xxxx_Note);

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