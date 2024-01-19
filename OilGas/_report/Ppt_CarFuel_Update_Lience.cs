using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OilGas.Controllers.CarFuel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Xceed.Words.NET;

namespace OilGas._report
{
    public class Ppt_CarFuel_Update_Lience : ReportClass
    {
        public string Export(CarFuel_Update_Lience query)
        {
            try
            {
                //複製範本
                string sourcePath = FileHelper.GetTempleteFolder() + "證照套印.docx";

                string fileName = query.Gas_Name +  System.IO.Path.GetFileNameWithoutExtension(sourcePath) + "_" + DateTime.Now.ToString("yyyy-MM-dd_") + ".docx";
                string toFolder = FileHelper.GetFileFolder(Code.TempUploadFile.範本_證件套印);

                if (!Directory.Exists(toFolder))
                {
                    Directory.CreateDirectory(toFolder);
                }

                string toPath = toFolder + fileName;
                //File.Copy(sourcePath, toPath, true);

                //編輯word範本檔
                var culture = new CultureInfo("zh-TW");
                culture.DateTimeFormat.Calendar = new System.Globalization.TaiwanCalendar();
                var date = query.ReportDate.Value.ToString("yyy/MM/dd",culture);
                var l = date.Split(new char[] { '/' });
                var year = l[0];
                var month = l[1];
                var day = l[2];

                Dictionary<string, string> doc = new Dictionary<string, string>()
                    {
                        {"Lience_No",query.LienceNo},
                        {"GasName", query.Gas_Name },
                        {"BT",query.Business_Theme },
                        {"MA", query.Boss },
                        {"Address", query.Address },
                        {"Type", query.SellType },
                        {"YY", year },
                        {"MM", month },
                        {"DD", day}
                    };

                using (DocX document = DocX.Load(sourcePath))
                {
                    foreach(var k in doc)
                    {
                        document.ReplaceText("[$" + k.Key + "$]", k.Value);
                    }

                    document.SaveAs(toPath);
                }

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