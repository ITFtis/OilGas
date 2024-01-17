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
    public class Rpt_Audit_Guidance_Check_Basic_Audit_NoTime : ReportClass
    {
        public string Export(string caseNo)
        {
            try
            {
                //複製範本
                string sourcePath = FileHelper.GetTempleteFolder() + "自行填報系統.xlsx";

                string fileName = System.IO.Path.GetFileNameWithoutExtension(sourcePath) + "_" + DateTime.Now.ToString("yyyy-MM-dd_") + ".xlsx";
                string toFolder = FileHelper.GetFileFolder(Code.TempUploadFile.範本_自行填報系統);

                if (!Directory.Exists(toFolder))
                {
                    Directory.CreateDirectory(toFolder);
                }

                string toPath = toFolder + fileName;
                File.Copy(sourcePath, toPath, true);

                var _db = new OilGasModelContextExt();

                //報表
                IModelEntity<Check_Basic_NoTime> model = new ModelEntity<Check_Basic_NoTime>(_db);

                var data = model.Get(x => x.Check_Number == caseNo);

                if (data == null)
                {
                    _errorMessage = "查無此案件編號:" + caseNo;
                    return "";
                }

                //編輯範本
                XSSFWorkbook workbook = null;
                XSSFSheet sheet = null;
                FileStream xlsFile = new FileStream(toPath, FileMode.Open, FileAccess.ReadWrite);
                workbook = new XSSFWorkbook(xlsFile);
                xlsFile.Close();
                sheet = (XSSFSheet)workbook.GetSheetAt(0);
                workbook.SetSheetName(workbook.GetSheetIndex(sheet), "自行填報系統");

                //標頭
                var time = DateTime.Now;
                var taiwanCalender = new System.Globalization.TaiwanCalendar();
                var year = taiwanCalender.GetYear(time).ToString();

                ICell c;
                c = sheet.GetRow(0).GetCell(0);
                c.SetCellValue(c.StringCellValue.Replace("112", year));

                //基本資料列
                c = sheet.GetRow(1).GetCell(2);
                c.SetCellValue(data.Check_Number);

                c = sheet.GetRow(1).GetCell(4);
                var filledDate = data.CheckDate?.ToString("yyyy年MM月dd日");
                c.SetCellValue(filledDate);

                var bt = getBusinessTheme(data.otherBusiness_theme, data.Business_theme);
                c = sheet.GetRow(2).GetCell(2);
                c.SetCellValue(bt);

                c = sheet.GetRow(2).GetCell(4);
                c.SetCellValue(data.Gas_Name);

                c = sheet.GetRow(3).GetCell(2);
                c.SetCellValue(data.CheckMan);

                c = sheet.GetRow(3).GetCell(4);
                c.SetCellValue(data.PhoneNumber);

                c = sheet.GetRow(4).GetCell(2);
                c.SetCellValue(data.Address);

                //主內容區
                IEnumerable<KeyValuePair<string,int>> sections = new List<KeyValuePair<string, int>>()
                {
                    new KeyValuePair<string, int>("A",6),
                    new KeyValuePair<string, int>("B",12),
                    new KeyValuePair<string, int>("C",22),
                    new KeyValuePair<string, int>("D",36),
                    new KeyValuePair<string, int>("E",47),
                    new KeyValuePair<string, int>("F",50),
                    new KeyValuePair<string, int>("G",59),
                    new KeyValuePair<string, int>("H",65),
                    new KeyValuePair<string, int>("I",70),
                    new KeyValuePair<string, int>("J",80),
                    new KeyValuePair<string, int>("K",83),
                    new KeyValuePair<string, int>("L",85),
                };
                foreach (var section in sections)
                {
                    var sData = GetSectionData(data, section.Key);

                    c = setCellValue(sheet, c, sData, section.Value);
                }

                ////A區
                //var AData = GetSectionData(data,"A");
                
                //c = setCellValue(sheet, c, AData,6);


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

        private string getBusinessTheme(string ot, string bt)
        {
            var dbContext = new OilGasModelContextExt();
            Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> model = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
            var allBT = model.GetAll().Where(a => a.IsEnable == true).OrderBy(a => a.Rank).ToArray();

            if (string.IsNullOrEmpty(ot))
            {
                return allBT.Where(x => x.Value == bt.Trim()).FirstOrDefault().Name.ToString();
            }

            return ot;
        }

        private ResultData GetSectionData(Check_Basic_NoTime data,string target)
        {
            var result = new ResultData();
            

            if(target == "A")
            {
                List<string> checkResult = new List<string> 
                { data.A01, data.A02, data.A03, data.A04, data.A05, data.A06 };
                List<string> note = new List<string>
                {
                    data.A01_Note, data.A02_Note, data.A03_Note, data.A04_Note, data.A05_Note, data.A06_Note
                };
                result.CheckValue = checkResult;
                result.Note = note;
            }

            if(target == "B")
            {
                List<string> checkResult = new List<string>
                {
                    data.B01, data.B02, data.B03, data.B04, data.B05, data.B06, data.B07,
                    data.B08, data.B09,data.B10
                };
                List<string> note = new List<string>()
                {
                    data.B01_Note,data.B02_Note, data.B03_Note,data.B04_Note, data.B05_Note, data.B06_Note,
                    data.B07_Note,data.B08_Note, data.B09_Note, data.B10_Note
                };

                result.CheckValue = checkResult;
                result.Note = note;
            }

            if(target == "C")
            {
                List<string> checkResult = new List<string>
                {
                    data.C01, data.C02, data.C03, data.C04, data.C05, data.C06, data.C07,
                    data.C08, data.C09,data.C10,data.C11, data.C12, data.C13, data.C14
                };

                List<string> note = new List<string>
                {
                    data.C01_Note, data.C02_Note, data.C03_Note, data.C04_Note, data.C05_Note, data.C06_Note,
                    data.C07_Note, data.C08_Note, data.C09_Note, data.C10_Note, data.C11_Note, data.C12_Note,
                    data.C13_Note, data.C14_Note
                };
                result.CheckValue = checkResult;
                result.Note = note;
            }

            if(target == "D")
            {
                List<string> r = new List<string>
                {
                    data.D01, data.D02, data.D03, data.D04, data.D05, data.D06, data.D07,
                    data.D08, data.D09,data.D10, data.D11
                };
                List<string> n = new List<string>
                {
                    data.D01_Note, data.D02_Note, data.D03_Note, data.D04_Note, data.D05_Note, data.D06_Note,
                    data.D07_Note, data.D08_Note, data.D09_Note, data.D10_Note, data.D11_Note
                };

                result.CheckValue = r;
                result.Note = n;
            }

            if(target == "E")
            {
                List<string> r = new List<string>
                {
                    data.E01, data.E02, data.E03
                };
                List<string> n = new List<string>
                {
                    data.E01_Note, data.E02_Note, data.E03
                };
                result.CheckValue = r;
                result.Note = n;
            }

            if(target == "F")
            {
                List<string> r = new List<string>
                {
                    data.F01, data.F02, data.F03,data.F04, data.F05, data.F06, data.F07, data.F08, data.F09,
                };
                List<string> n = new List<string>
                {
                    data.F01_Note, data.F02_Note, data.F03_Note, data.F04_Note, data.F05_Note,data.F06_Note,
                    data.F07_Note,data.F08_Note,data.F09_Note
                };

                result.CheckValue = r;
                result.Note = n;
            }

            if(target == "G")
            {
                List<string> r = new List<string>
                {
                    data.G01, data.G02, data.G03, data.G04, data.G05, data.G06
                };
                List<string> n = new List<string>
                {
                    data.G01_Note, data.G02_Note, data.G03_Note, data.G04_Note, data.G05_Note, data.G06_Note
                };

                result.CheckValue= r;
                result.Note = n;
            }

            if(target == "H")
            {
                List<string> r = new List<string>
                {
                    data.H01,data.H02, data.H03, data.H04,data.H05
                };
                List<int> i = new List<int>
                {
                    data.H01_Value, data.H02_Value, data.H03_Value, data.H04_Value, data.H05_Value
                };
                List<string> n = new List<string>
                {
                    data.H01_Note, data.H02_Note, data.H03_Note, data.H04_Note, data.H05_Note
                };

                result.CheckValue = r;
                result.Note = n;
                result.trueValue = i;
            }

            if(target == "I")
            {
                List<string> r = new List<string>
                {
                    data.I01,data.I02, data.I03, data.I04, data.I05,data.I06,data.I07,data.I08,data.I09,
                };
                List<string> n = new List<string>
                {
                    data.I01_Note, data.I02_Note, data.I03_Note, data.I04_Note, data.I05_Note, data.I06_Note, data.I07_Note,data.I08_Note, data.I09_Note,
                };

                result.CheckValue = r;
                result.Note = n;
            }

            if(target == "J")
            {
                List<string> r = new List<string>
                {
                    data.J01,data.J02, data.J03
                };
                List<string> n = new List<string>
                {
                    data.J01_Note, data.J02_Note, data.J03_Note
                };

                result.CheckValue = r;
                result.Note = n;
            }

            if(target == "K")
            {
                List<string> r = new List<string>
                {
                    data.K01, data.K02
                };
                List<string> n = new List<string>
                {
                    data.K01_Note, data.k02_Note
                };

                result.CheckValue = r;
                result.Note = n;
            }

            if(target == "L")
            {
                List<string> r = new List<string>
                {
                    data.L01, data.L02, data.L03
                };
                List<string> n = new List<string>
                {
                    data.L01_Note, data.L02_Note, data.L03_Note
                };

                result.CheckValue = r;
                result.Note = n;
            }



            return result;
            
        }

        /// <summary>
        /// 把資料寫到儲存格中
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="c"></param>
        /// <param name="data"></param>
        /// <param name="startCellIndex">起始儲存格index</param>
        /// <returns></returns>
        private static ICell setCellValue(XSSFSheet sheet, ICell c, ResultData data,int startCellIndex)
        {
            var end = data.CheckValue.Count;
            if(startCellIndex == 65)
            {
                for (var i = 0; i < end; i++)
                {
                    c = sheet.GetRow(i + startCellIndex).GetCell(1);
                    c.SetCellValue(c.StringCellValue.Replace("X", data.trueValue[i].ToString()));

                    c = sheet.GetRow(i + startCellIndex).GetCell(2);
                    c.SetCellValue(Code.GetCheckResultStr(data.CheckValue[i]));
                    
                    c = sheet.GetRow(i + startCellIndex).GetCell(3);
                    c.SetCellValue(data.Note[i]);

                }
            }
            else
            {
                for (var i = 0; i < end; i++)
                {
                    c = sheet.GetRow(i + startCellIndex).GetCell(3);
                    c.SetCellValue(Code.GetCheckResultStr(data.CheckValue[i]));

                    c = sheet.GetRow(i + startCellIndex).GetCell(4);
                    c.SetCellValue(data.Note[i]);

                }
            }
            

            return c;
        }
    }

    internal class ResultData
    {
        public ResultData()
        {
        }
        public List<string> CheckValue { get; set; }
        public List<string> Note { get; set; }
        public List<int> trueValue { get; set; }
    }
}