using Dou.Controllers;
using Dou.Models.DB;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace OilGas.Controllers.Audit
{
    [Dou.Misc.Attr.MenuDef(Id = "Audit_Guidance_Check_List_local", Name = "地方政府查核結果填報", MenuPath = "查核輔導專區/G查核輔導資料", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class Audit_Guidance_Check_List_localController : APaginationModelController<Check_Basic_local>
    {
        public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        // GET: Audit_Guidance_Check_List_local
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<Check_Basic_local> GetModelEntity()
        {
            return new ModelEntity<Check_Basic_local>(new OilGasModelContextExt());
        }
        protected override void AddDBObject(IModelEntity<Check_Basic_local> dbEntity, IEnumerable<Check_Basic_local> objs)
        {


            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同

            //CaseNo在前端的下拉選單會給CaseNo,Gas_Name  ,所以用","取CaseNo跟Gas_Name
            var CaseNoAndGas_Name = objs.First().CaseNo.Split(',');

            //以防Gas_Name有","  ，所以用迴圈把後面的字直接組起來
            var Gas_Name = "";
            for (int i = 1; i < CaseNoAndGas_Name.Length; i++)
            {
                Gas_Name = Gas_Name + "," + CaseNoAndGas_Name[i];
            }

            objs.First().CaseNo = CaseNoAndGas_Name[0];
            objs.First().Gas_Name = Gas_Name.Substring(1);//拿掉第一個","




            base.AddDBObject(dbEntity, objs);

        }


        protected override void UpdateDBObject(IModelEntity<Check_Basic_local> dbEntity, IEnumerable<Check_Basic_local> objs)
        {



            basic.iscityedit(objs.First().CaseNo);//確定縣市跟帳號縣市相同






            base.UpdateDBObject(dbEntity, objs);

        }

        [HttpPost]
        public string Export(string CaseNo)
        {
            string filePath = FileHelper.GetFileFolder(Code.TempUploadFile.地方政府查核結果填報);
            var file = filePath + "地方政府查核結果填報(範本).xlsx";

            var datas = (from Check_Basic_local in db.Check_Basic_local
                         join b in db.CarFuel_BasicData
                         on Check_Basic_local.CaseNo equals b.CaseNo into groupjoin
                         from CarFuel_BasicData in groupjoin.DefaultIfEmpty()
                         where Check_Basic_local.CaseNo == CaseNo
                         select new { Check_Basic_local, CarFuel_BasicData }).FirstOrDefault();


            // 創建一個新的工作簿（此處以 XLSX 格式為例）
            IWorkbook workbook = new XSSFWorkbook();

            workbook = new XSSFWorkbook(file);


            ISheet sheet = workbook.GetSheetAt(0); // 獲取第一個工作表






            var cityid = "";
            if (CaseNo != null && CaseNo.Length > 6)
            {
                cityid = CaseNo.Substring(4, 2);
            }
            else
            {
                cityid = Dou.Context.CurrentUser<User>().city;
            }
            var city = (from a in db.CityCode
                        where a.GSLCode == cityid
                        select a).FirstOrDefault();




            var Row0 = sheet.GetRow(0);
            Row0.GetCell(0).SetCellValue((city == null || city.CityName == null || city.CityName == "") ? "XXX政府加油站檢查紀錄表" : city.CityName + "政府加油站檢查紀錄表");
            Row0.GetCell(8).SetCellValue("檢查日期:" + DateTime.Now.ToString("yyyy-MM-dd"));



            if (datas.CarFuel_BasicData != null)          
            {
                var Business_theme = (from a in db.CarVehicleGas_BusinessOrganization
                                      where a.Value == datas.CarFuel_BasicData.Business_theme
                                      select a).FirstOrDefault();
                var Row1 = sheet.GetRow(1);
                if (Business_theme != null)
                {
                    Row1.GetCell(2).SetCellValue(Business_theme.Name);
                }
                Row1.GetCell(8).SetCellValue(datas.CarFuel_BasicData.Boss);
            }

          



            var Row2 = sheet.GetRow(2);
            Row2.GetCell(2).SetCellValue(datas.CarFuel_BasicData.Gas_Name);



            var Row3 = sheet.GetRow(3);
            Row3.GetCell(2).SetCellValue(datas.CarFuel_BasicData.Address);
            Row3.GetCell(8).SetCellValue(datas.CarFuel_BasicData.TelNo);



            var A01_Options = "1.是否於明顯處所標示□營業主體、□加油站站名、□營業時間、□供油廠商標誌或名稱。";
            if (datas.Check_Basic_local.A01_Options != null || datas.Check_Basic_local.A01_Options != "") {
                foreach (var data in datas.Check_Basic_local.A01_Options.Split(';'))
                {
                    switch (data)
                    {

                        case "0":
                            A01_Options= A01_Options.Replace("□營業主體", "☑營業主體");
                            break;

                        case "1":
                            A01_Options = A01_Options.Replace("□加油站站名", "☑加油站站名");
                            break;

                        case "2":
                            A01_Options = A01_Options.Replace("□營業時間", "☑營業時間");
                            break;
                        case "3":
                            A01_Options = A01_Options.Replace("□供油廠商標誌或名稱", "☑供油廠商標誌或名稱");
                            break;
                      

                    }

                }
            }

            sheet.GetRow(5).GetCell(1).SetCellValue(A01_Options);




            sheet.GetRow(6).GetCell(6).SetCellValue(datas.Check_Basic_local.A01=="1"?"符合":"不符合");

            sheet.GetRow(6).GetCell(7).SetCellValue(datas.Check_Basic_local.A01_Notes);

            //先設不符合
            sheet.GetRow(8).GetCell(6).SetCellValue("不符合");
            sheet.GetRow(9).GetCell(6).SetCellValue("不符合");
            sheet.GetRow(10).GetCell(6).SetCellValue("不符合");
            sheet.GetRow(11).GetCell(6).SetCellValue("不符合");
            sheet.GetRow(12).GetCell(6).SetCellValue("不符合");

            if (datas.Check_Basic_local.A02_Options != null || datas.Check_Basic_local.A02_Options != "")
            {
                foreach (var data in datas.Check_Basic_local.A02_Options.Split(';'))
                {
                    switch (data)
                    {

                        case "0":
                            sheet.GetRow(8).GetCell(6).SetCellValue("符合");
                            break;

                        case "1":
                            sheet.GetRow(9).GetCell(6).SetCellValue("符合");
                            break;

                        case "2":
                            sheet.GetRow(10).GetCell(6).SetCellValue("符合");
                            break;
                        case "3":
                            sheet.GetRow(11).GetCell(6).SetCellValue("符合");
                            break;
                        case "4":
                            sheet.GetRow(12).GetCell(6).SetCellValue("符合");
                            break;


                    }

                }
            }





            sheet.GetRow(8).GetCell(7).SetCellValue(datas.Check_Basic_local.A02_Notes);

















            //先設不符合
            sheet.GetRow(14).GetCell(6).SetCellValue("不符合");
            sheet.GetRow(15).GetCell(6).SetCellValue("不符合");
            sheet.GetRow(16).GetCell(6).SetCellValue("不符合");
            sheet.GetRow(17).GetCell(6).SetCellValue("不符合");


            if (datas.Check_Basic_local.A02_Options != null || datas.Check_Basic_local.A02_Options != "")
            {
                foreach (var data in datas.Check_Basic_local.A02_Options.Split(';'))
                {
                    switch (data)
                    {

                        case "0":
                            sheet.GetRow(14).GetCell(6).SetCellValue("符合");
                            break;

                        case "1":
                            sheet.GetRow(15).GetCell(6).SetCellValue("符合");
                            break;

                        case "2":
                            sheet.GetRow(16).GetCell(6).SetCellValue("符合");
                            break;
                        case "3":
                            sheet.GetRow(17).GetCell(6).SetCellValue("符合");
                            break;
                    }

                }
            }





            sheet.GetRow(14).GetCell(7).SetCellValue(datas.Check_Basic_local.A02_Notes);









            sheet.GetRow(18).GetCell(6).SetCellValue(datas.Check_Basic_local.B01 == "1" ? "符合" : "不符合");
            sheet.GetRow(18).GetCell(7).SetCellValue(datas.Check_Basic_local.B01_Notes);



            sheet.GetRow(19).GetCell(6).SetCellValue(datas.Check_Basic_local.B02 == "1" ? "符合" : "不符合");
            sheet.GetRow(19).GetCell(7).SetCellValue(datas.Check_Basic_local.B02_Notes);








            var B03_gun = "3.1加油機：_"+ datas.Check_Basic_local.B03_gun1 + "_槍_" + datas.Check_Basic_local.B03_gun1_value + "_台、_" + datas.Check_Basic_local.B03_gun2 + "_槍_" + datas.Check_Basic_local.B03_gun2_value + "_台、_" + datas.Check_Basic_local.B03_gun3 + "_槍_" + datas.Check_Basic_local.B03_gun3_value + "_台、_" + datas.Check_Basic_local.B03_gun4 + "_槍_" + datas.Check_Basic_local.B03_gun4_value + "_台。";
             sheet.GetRow(21).GetCell(1).SetCellValue(B03_gun);

            sheet.GetRow(22).GetCell(6).SetCellValue(datas.Check_Basic_local.B03 == "1" ? "符合" : "不符合");
            sheet.GetRow(22).GetCell(7).SetCellValue(datas.Check_Basic_local.B03_Notes);
















            var B04_tank = "3.1加油機：_" + datas.Check_Basic_local.B04_tank1 + "_公秉_" + datas.Check_Basic_local.B04_tank1_value + "_座、_" + datas.Check_Basic_local.B04_tank2 + "_公秉_" + datas.Check_Basic_local.B04_tank2_value + "_座、_" + datas.Check_Basic_local.B04_tank3 + "_公秉_" + datas.Check_Basic_local.B04_tank3_value + "_座。";
            sheet.GetRow(23).GetCell(1).SetCellValue(B04_tank);

            sheet.GetRow(24).GetCell(6).SetCellValue(datas.Check_Basic_local.B04 == "1" ? "符合" : "不符合");
            sheet.GetRow(24).GetCell(7).SetCellValue(datas.Check_Basic_local.B04_Notes);








            var B05_Options = "3.5設置附屬設施是否經申請核准。□汽機車簡易保養設施 □洗車設施 □簡易排污測服務設施 □銷售汽機車用品設施□自動販賣機 □多媒體事務機 □接受事業機構委託收費服務設施";
            if (datas.Check_Basic_local.B05_Options != null || datas.Check_Basic_local.B05_Options != "")
            {
                foreach (var data in datas.Check_Basic_local.B05_Options.Split(';'))
                {
                    switch (data)
                    {

                        case "0":
                            B05_Options = B05_Options.Replace("□汽機車簡易保養設施", "☑汽機車簡易保養設施");
                            break;

                        case "1":
                            B05_Options = B05_Options.Replace("□洗車設施", "☑洗車設施");
                            break;

                        case "2":
                            B05_Options = B05_Options.Replace("□簡易排污測服務設施", "☑簡易排污測服務設施");
                            break;
                        case "3":
                            B05_Options = B05_Options.Replace("□銷售汽機車用品設施", "☑銷售汽機車用品設施");
                            break;
                        case "4":
                            B05_Options = B05_Options.Replace("□自動販賣機", "☑自動販賣機");
                            break;
                        case "5":
                            B05_Options = B05_Options.Replace("□多媒體事務機", "☑多媒體事務機");
                            break;
                        case "6":
                            B05_Options = B05_Options.Replace("□接受事業機構委託收費服務設施", "☑接受事業機構委託收費服務設施");
                            break;

                    }

                }
            }

            sheet.GetRow(25).GetCell(1).SetCellValue(B05_Options);


            sheet.GetRow(26).GetCell(6).SetCellValue(datas.Check_Basic_local.B05 == "1" ? "符合" : "不符合");
            sheet.GetRow(26).GetCell(7).SetCellValue(datas.Check_Basic_local.B05_Notes);
















            var B06_Options = "4.設置兼營項目是否經申請核准。□便利商店□販售農產品□停車場□車用液化石油氣□代辦汽車定期檢驗□汽機車與自行車買賣及租賃□經銷公益彩券□廣告服務□提供場所供設置金融機構營業場所外自動化服務設備□接受他人委託代收物品服務□從事僅供收受洗滌物場所之洗衣業務□提供營業站屋屋頂設置行動電話基地台□屋頂供他人設置裝置容量不及五百瓩並利用太陽能之自用發電設備□其他經中央主管機關核准之兼營項目";
            if (datas.Check_Basic_local.B06_Options != null || datas.Check_Basic_local.B06_Options != "")
            {
                foreach (var data in datas.Check_Basic_local.B06_Options.Split(';'))
                {
                    switch (data)
                    {

                        case "0":
                            B06_Options = B06_Options.Replace("□便利商店", "☑便利商店");
                            break;

                        case "1":
                            B06_Options = B06_Options.Replace("□販售農產品", "☑販售農產品");
                            break;

                        case "2":
                            B06_Options = B06_Options.Replace("□停車場", "☑停車場");
                            break;
                        case "3":
                            B06_Options = B06_Options.Replace("□車用液化石油氣", "☑車用液化石油氣");
                            break;
                        case "4":
                            B06_Options = B06_Options.Replace("□代辦汽車定期檢驗", "☑代辦汽車定期檢驗");
                            break;
                        case "5":
                            B06_Options = B06_Options.Replace("□汽機車與自行車買賣及租賃", "☑汽機車與自行車買賣及租賃");
                            break;
                        case "6":
                            B06_Options = B06_Options.Replace("□經銷公益彩券", "☑經銷公益彩券");
                            break;
                        case "7":
                            B06_Options = B06_Options.Replace("□廣告服務", "☑廣告服務");
                            break;

                        case "8":
                            B06_Options = B06_Options.Replace("□提供場所供設置金融機構營業場所外自動化服務設備", "☑提供場所供設置金融機構營業場所外自動化服務設備");
                            break;

                        case "9":
                            B06_Options = B06_Options.Replace("□接受他人委託代收物品服務 ", "☑接受他人委託代收物品服務 ");
                            break;
                        case "10":
                            B06_Options = B06_Options.Replace("□從事僅供收受洗滌物場所之洗衣業務", "☑從事僅供收受洗滌物場所之洗衣業務");
                            break;
                        case "11":
                            B06_Options = B06_Options.Replace("□提供營業站屋屋頂設置行動電話基地台", "☑提供營業站屋屋頂設置行動電話基地台");
                            break;
                        case "12":
                            B06_Options = B06_Options.Replace("□屋頂供他人設置裝置容量不及五百瓩並利用太陽能之自用發電設備", "☑屋頂供他人設置裝置容量不及五百瓩並利用太陽能之自用發電設備");
                            break;
                        case "13":
                            B06_Options = B06_Options.Replace("□其他經中央主管機關核准之兼營項目", "☑其他經中央主管機關核准之兼營項目");
                            break;
                    }

                }
            }

            sheet.GetRow(27).GetCell(1).SetCellValue(B06_Options);


            sheet.GetRow(28).GetCell(6).SetCellValue(datas.Check_Basic_local.B06 == "1" ? "符合" : "不符合");
            sheet.GetRow(28).GetCell(7).SetCellValue(datas.Check_Basic_local.B06_Notes);




            sheet.GetRow(29).GetCell(6).SetCellValue(datas.Check_Basic_local.C01 == "1" ? "符合" : "不符合");
            sheet.GetRow(29).GetCell(7).SetCellValue(datas.Check_Basic_local.C01_Notes);



            sheet.GetRow(30).GetCell(6).SetCellValue(datas.Check_Basic_local.D01 == "1" ? "符合" : "不符合");
            sheet.GetRow(30).GetCell(7).SetCellValue(datas.Check_Basic_local.D01_Notes);


            sheet.GetRow(31).GetCell(6).SetCellValue(datas.Check_Basic_local.D02 == "1" ? "符合" : "不符合");
            sheet.GetRow(31).GetCell(7).SetCellValue(datas.Check_Basic_local.D02_Notes);



            sheet.GetRow(32).GetCell(6).SetCellValue(datas.Check_Basic_local.E01 == "1" ? "符合" : "不符合");
            sheet.GetRow(32).GetCell(7).SetCellValue(datas.Check_Basic_local.E01_Notes);

            sheet.GetRow(33).GetCell(6).SetCellValue(datas.Check_Basic_local.E02 == "1" ? "符合" : "不符合");
            sheet.GetRow(33).GetCell(7).SetCellValue(datas.Check_Basic_local.E02_Notes);









            var E_date = datas.Check_Basic_local.E_date.HasValue ? datas.Check_Basic_local.E_date.Value.ToString("yyyy-MM-dd") : "_年_月_日";
            var E03 = datas.Check_Basic_local.E03 == "1" ? "【☑是；□否】" : "【□是；☑否】";
            var E03text = "3.前項安全檢查項目經檢查是否有缺失"+ E03 + "。  填是者，應請業者於"+ E_date + "前完成改善，報府備查。";
            sheet.GetRow(34).GetCell(1).SetCellValue(E03text);




            sheet.GetRow(35).GetCell(6).SetCellValue(datas.Check_Basic_local.F01 == "1" ? "符合" : "不符合");
            sheet.GetRow(35).GetCell(7).SetCellValue(datas.Check_Basic_local.F01_Notes);






            var G01 = (datas.Check_Basic_local.G01!=""|| datas.Check_Basic_local.G01 != null)? "【☑是；□否】" : "【□是；☑否】";
            var G01text = "1.現場稽查，是否有發現下列情事："+ G01;
            sheet.GetRow(36).GetCell(1).SetCellValue(G01text);








             var G01_Options = "□對販售之汽油、柴油摻雜或作偽。□以流量式加油機以外之器具，販售汽油、柴油。□於核准基地範圍外進行汽油、柴油之交付行為或營業。□設置未經核准之出、入口，供車輛通行及加油使用。□於加油站內等候車道以外之地區，進行加油行為。□灌注柴油至內容積總和超過4,000公升油罐車之罐槽體或車輛裝載之油槽(桶)。□灌注汽油至油罐車之罐槽體或車輛裝載內容積總和達200公升以上之油槽(桶)。□其他對公共安全有影響之虞之行為。";
            if (datas.Check_Basic_local.G01 != null || datas.Check_Basic_local.G01 != "")
            {
                foreach (var data in datas.Check_Basic_local.G01.Split(';'))
                {
                    switch (data)
                    {

                        case "0":
                            G01_Options = G01_Options.Replace("□對販售之汽油、柴油摻雜或作偽。", "☑對販售之汽油、柴油摻雜或作偽。");
                            break;

                        case "1":
                            G01_Options = G01_Options.Replace("□以流量式加油機以外之器具，販售汽油、柴油。", "☑以流量式加油機以外之器具，販售汽油、柴油。");
                            break;

                        case "2":
                            G01_Options = G01_Options.Replace("□於核准基地範圍外進行汽油、柴油之交付行為或營業。", "☑於核准基地範圍外進行汽油、柴油之交付行為或營業。");
                            break;
                        case "3":
                            G01_Options = G01_Options.Replace("□設置未經核准之出、入口，供車輛通行及加油使用。", "☑設置未經核准之出、入口，供車輛通行及加油使用。");
                            break;
                        case "4":
                            G01_Options = G01_Options.Replace("□於加油站內等候車道以外之地區，進行加油行為。", "☑於加油站內等候車道以外之地區，進行加油行為。");
                            break;
                        case "5":
                            G01_Options = G01_Options.Replace("□灌注柴油至內容積總和超過4,000公升油罐車之罐槽體或車輛裝載之油槽(桶)。", "☑灌注柴油至內容積總和超過4,000公升油罐車之罐槽體或車輛裝載之油槽(桶)。");
                            break;
                        case "6":
                            G01_Options = G01_Options.Replace("□灌注汽油至油罐車之罐槽體或車輛裝載內容積總和達200公升以上之油槽(桶)。", "☑灌注汽油至油罐車之罐槽體或車輛裝載內容積總和達200公升以上之油槽(桶)。");
                            break;
                        case "7":
                            G01_Options = G01_Options.Replace("□其他對公共安全有影響之虞之行為。", "☑其他對公共安全有影響之虞之行為。");
                            break;

                    }

                }
            }

            sheet.GetRow(37).GetCell(1).SetCellValue(G01_Options);














            var G02 = datas.Check_Basic_local.G02 == "1" ? "【☑是；□否】" : "【□是；☑否】";
            var G02text = "2.現場稽查，是否有特殊用油需求者以桶(槽)加油之販售行為"+ G02 + "。  (填否者，下列2款免填)";
            sheet.GetRow(38).GetCell(1).SetCellValue(G02text);




  
            var G02_1 = datas.Check_Basic_local.G02_1 == "1" ? "【☑是；□否】" : datas.Check_Basic_local.G02_1 == "0" ? "【□是；☑否】" : "【□是；□否】";
            var G02_1text = "(1)汽油數量在10公升以上或柴油數量在500公升以上者，業者是否依中央主管機關規定之登記表登記，並於消費者攜帶之容器上粘貼中央主管機關規定之警語標籤"+ G02_1 + "。";
            sheet.GetRow(39).GetCell(1).SetCellValue(G02_1text);



            var G02_2 = datas.Check_Basic_local.G02_2 == "1" ? "【☑是；□否】" : datas.Check_Basic_local.G02_2 == "0" ? "【□是；☑否】" : "【□是；□否】";
            var G02_2text = "(2)消費者攜帶之容器是否為玻璃容器、紙質容器或塑膠袋。" + G02_2;
            sheet.GetRow(40).GetCell(1).SetCellValue(G02_2text);












            sheet.GetRow(41).GetCell(1).SetCellValue("其他事項：" + datas.Check_Basic_local.H_Notes);

            var CITYdate = (city == null || city.CityName == null || city.CityName == "") ? "XXX" : city.CityName;

            sheet.GetRow(42).GetCell(0).SetCellValue(CITYdate+"政府檢查人員："+ datas.Check_Basic_local.inspectors + "\n簽名：");





            var I01 = datas.Check_Basic_local.I01 == "1" ? "【☑是；□否】" : datas.Check_Basic_local.I01 == "0"? "【□是；☑否】" : "【□是；□否】";

            sheet.GetRow(42).GetCell(4).SetCellValue("受檢加油站陪檢人員：" + datas.Check_Basic_local.Inspection + "職稱：" + datas.Check_Basic_local.Inspection_title + "\n簽名：\n是否有陳述意見：" + I01 + "。陳述意見如下：" + datas.Check_Basic_local.I01_Notes);













            // 將工作簿寫入文件
            FileStream fileStream = new FileStream(filePath + "地方政府查核結果填報.xlsx", FileMode.Create, FileAccess.Write);
            workbook.Write(fileStream);
            workbook.Close();
            return OilGas.Cm.PhysicalToUrl(FileHelper.GetFileFolder(Code.TempUploadFile.地方政府查核結果填報)) + "地方政府查核結果填報.xlsx";
        }
    }
}