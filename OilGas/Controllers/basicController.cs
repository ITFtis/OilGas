using Dou.Controllers;
using Dou.Misc.Attr;
using Newtonsoft.Json;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
using Dou.Models;
using NPOI.SS.Formula.Functions;

namespace OilGas.Controllers
{
    public class basicController : Controller
    {
        //確認帳號權限
        public bool Permissions( string RoleId)
        {
            bool Permissions = false;
            var RoleUsers = Dou.Context.CurrentUser<User>().RoleUsers;
            var RoleUsersgrade = Dou.Context.CurrentUser<User>().grade;

            foreach (RoleUser i in RoleUsers)
            {
                if (i.RoleId == RoleId)
                {
                    Permissions = true;
                    break;
                }
            }

            if(RoleUsersgrade=="10"|| RoleUsersgrade == "11"|| RoleUsersgrade == "12")
            {
                Permissions = true;
            }


            return Permissions;
        }


        //確認帳號縣市是否可以新刪修資料
        public void iscityedit(string CaseNo) 
        {
            if (!Dou.Context.CurrentIsAdminUser&&!Permissions("admin"))
            {
                var CITYdata = Dou.Context.CurrentUser<User>().city.Split(',');

                List<string> formats = new List<string>();
                foreach(var v in CITYdata)
                {
                    formats.Add("oooo" + v + "oo....");
                }
                string errorAlert = "案件編號格式錯誤，格式：" + string.Join("或", formats);

                if (CaseNo.Length < 6)
                {
                    throw new Exception(errorAlert);                    
                }

                var city =CaseNo.Substring(4, 2);                
                if (!CITYdata.Any(x => city.Contains(x)))
                {
                    throw new Exception(errorAlert);
                }
            }

        }





        //上傳檔案
        public string upload(HttpPostedFileBase file, string Old_File_name, string filepath)
        {
            string extension = Path.GetExtension(file.FileName);
            string filename = Path.GetFileName(file.FileName);
            //只收txt、doc、docx、pdf
            if (extension == ".txt" | extension == ".doc" | extension == ".docx" | extension == ".pdf" | extension == ".jpg" | extension == ".png")
            {
              
                string savePath = ConfigurationManager.AppSettings["uploadfilepath"] + filepath + "\\" + filename;

            
                if (System.IO.File.Exists(savePath))
                {
                    if (Old_File_name == filename)
                    {

                        string directoryPath = Path.GetDirectoryName(savePath);
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }
                        file.SaveAs(savePath);//如果相同檔名就取代過去

                        return filename;//回傳檔名給下一個funtion用
                    }
                    else
                    {
                        return "false";
                    }
                }
                else
                {


                    string directoryPath = Path.GetDirectoryName(savePath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    file.SaveAs(savePath);


                    if (Old_File_name == "NULL")//原本沒檔案就不用刪了
                    {
                    }
                    else
                    {
                        try
                        {
                            System.IO.File.Delete(ConfigurationManager.AppSettings["uploadfilepath"] + filepath + "\\" + Old_File_name);//刪除舊檔案
                        }
                        catch
                        {

                        }
                    }




                    return filename;//回傳檔名給下一個funtion用
                }


            }

            return "false";


        }





        //比較兩個時間是否相同(差60秒當他相同)
        public bool timecompare(DateTime? t1, DateTime? t2)
        {
            DateTime time1 = t1.HasValue ? Convert.ToDateTime(t1) : Convert.ToDateTime("1901/1/1 15:12:12");
            DateTime time2 = t2.HasValue ? Convert.ToDateTime(t2) : Convert.ToDateTime("1901/1/1 15:12:12");

            var compareSeconds = new TimeSpan(time1.Ticks - time2.Ticks).TotalSeconds;
            if (compareSeconds > 60 || compareSeconds < -60)
            {
                return false;
            }
            else
            {
                return true;
            }

        }






        //取得filter資料
        public string getfilter(KeyValueParams[] paras, string filterobj)
        {
            var filter = paras.Where(X => X.key == "filter");

            if (filter.Count() > 0)
            {
                KeyValueParams[] Jsonfilter = JsonConvert.DeserializeObject<KeyValueParams[]>(filter.First().value.ToString());//反序列化
                return Jsonfilter.Where(X => X.key == filterobj).First().value.ToString();
            }

            return "";

        }






        //修改營運狀況
        public string changeUsageState<T>(IEnumerable<T> objs) where T : UsageStatebasic
        {


            var _UsageState1 = objs.First().UsageState1??"999";
            var _UsageState2 = objs.First().UsageState2 ?? "999";
            var _UsageState3 = objs.First().UsageState3 ?? "999";
            var _UsageState4 = objs.First().UsageState4 ?? "999";
            var _UsageState5 = objs.First().UsageState5 ?? "999";
            var _UsageState6 = objs.First().UsageState6 ?? "999";
            var _UsageState7 = objs.First().UsageState7 ?? "999";


            switch (_UsageState1)
            {
                #region 0 申請設置
                case "0": //申請設置
                    switch (_UsageState2)
                    {
                        case "0": //申請中  
                            switch (_UsageState3)
                            {
                                case "0": //申請中變更
                                    return "1";
                                case "1": //同意認定
                                    switch (_UsageState4)
                                    {
                                        case "0": //同意認定失效
                                            return "3";
                                        case "1": //同意認定變更
                                            return "4";
                                        case "2": //同意籌建

                                            switch (_UsageState5)
                                            {
                                                case "17": //同意籌建變更
                                                    return "15";
                                                case "18": //同意籌建失效
                                                    return "14";
                                                case "19": //核發經營許可執照
                                                    return "17";

                                                case "20": //申請核發經營許可執照展延
                                                    switch (_UsageState6)
                                                    {
                                                        case "10": //同意
                                                            return "19";
                                                        case "5": //核照展延失效
                                                            return "20";
                                                        case "6": //核駁
                                                            return "21";
                                                        case "7": //廢止
                                                            return "22";
                                                        case "8": //撤案
                                                            return "23";
                                                        case "999":
                                                            return "19";
                                                    }
                                                    break;
                                                case "21": //核駁
                                                    return "24";
                                                case "22": //廢止
                                                    return "25";
                                                case "23": //撤案
                                                    return "26";
                                                case "999":
                                                    return "8";
                                            }
                                            break;
                                        case "44": //申請同意籌建展延
                                            switch (_UsageState5)
                                            {
                                                case "26": //同意籌建展延
                                                    return "9";
                                                case "27": //同意籌建展延失效
                                                    return "10";
                                                case "28": //核駁
                                                    return "11";
                                                case "29": //廢止
                                                    return "12";
                                                case "30": //撤案
                                                    return "13";
                                                case "999":
                                                    return "9";
                                            }
                                            break;
                                        case "3": //核駁
                                            return "5";
                                        case "4": //廢止
                                            return "6";
                                        case "5": //撤案
                                            return "7";
                                        case "999":
                                            return "2";
                                    }
                                    break;
                                case "2": //同意籌建
                                    switch (_UsageState4)
                                    {
                                        case "6": //同意籌建變更
                                            return "15";
                                        case "7": //同意籌建失效
                                            return "14";
                                        case "8": //核發經營許可執照
                                            return "17";

                                        case "9": //申請核發經營許可執照展延
                                            switch (_UsageState5)
                                            {
                                                case "32": //同意
                                                    return "19";
                                                case "2": //核照展延失效
                                                    return "20";
                                                case "3": //核駁
                                                    return "21";
                                                case "4": //廢止
                                                    return "22";
                                                case "5": //撤案
                                                    return "23";
                                                case "999":
                                                    return "19";
                                            }
                                            break;
                                        case "10": //核駁
                                            return "24";
                                        case "11": //廢止
                                            return "25";
                                        case "12": //撤案
                                            return "26";
                                        case "999":
                                            return "8";
                                    }
                                    break;
                                case "3": //申請同意籌建展延
                                    switch (_UsageState4)
                                    {
                                        case "43": //同意籌建展延
                                            return "9";
                                        case "13": //同意籌建展延失效
                                            return "10";
                                        case "14": //核駁
                                            return "11";
                                        case "15": //廢止
                                            return "12";
                                        case "16": //撤案
                                            return "13";
                                        case "999":
                                            return "9";
                                    }
                                    break;
                                case "4": //核駁
                                    return "27";
                                case "5": //廢止
                                    return "28";
                                case "6": //撤案
                                    return "29";
                                case "999":
                                    return "0";

                            }
                            break;

                    }
                    break;
                #endregion

                #region 1 經營管理
                case "1": //經營管理
                    switch (_UsageState3)
                    {
                        case "7": //申請開業展延
                            switch (_UsageState4)
                            {
                                case "17": //同意籌建展延失效
                                    return "32";
                                case "18": //核駁
                                    return "34";
                                case "19": //廢止
                                    return "35";
                                case "20": //撤案
                                    return "36";
                                case "999":
                                    return "31";
                            }
                            break;
                        case "8": //營業中變更
                            switch (_UsageState4)
                            {
                                case "46": //同意營業中變更
                                    return "75";
                                case "47": //核駁
                                    return "76";
                                case "48": //廢止
                                    return "77";
                                case "49": //撤案
                                    return "78";
                                case "999":
                                    return "75";
                            }
                            break;
                        case "9": //申請暫停營業
                            switch (_UsageState4)
                            {
                                case "26": //暫停營業展延
                                    switch (_UsageState5)
                                    {
                                        case "24": //同意
                                            return "47";
                                        case "25": //核駁
                                            return "73";
                                        case "999":
                                            return "47";
                                    }
                                    break;
                                case "27": //同意暫停營業
                                    return "45";
                                case "28": //撤案
                                    return "49";
                                case "42": //核駁
                                    return "48";
                                case "999":
                                    return "44";
                            }
                            break;
                        case "10": //暫停營業中變更
                            switch (_UsageState4)
                            {
                                case "50": //同意暫停營業中變更
                                    return "79";
                                case "51": //核駁
                                    return "80";
                                case "52": //廢止
                                    return "81";
                                case "53": //撤案
                                    return "82";
                                case "999":
                                    return "79";
                            }
                            break;
                        case "11": //申請復業
                            switch (_UsageState4)
                            {
                                case "34": //同意復業
                                    switch (_UsageState5)
                                    {
                                        case "16": //同意復業撤案
                                            return "60";
                                        case "999":
                                            return "56";
                                    }
                                    break;
                                case "35": //撤案
                                    return "60";
                                case "40": //申請復業展延
                                    switch (_UsageState5)
                                    {
                                        case "15": //同意復業失效
                                            return "57";
                                        case "999":
                                            return "59";
                                    }
                                    break;
                                case "41": //失效
                                    return "58";
                                case "999":
                                    return "55";
                            }
                            break;
                        case "12": //申請歇業
                            switch (_UsageState4)
                            {
                                case "36": //同意歇業
                                    switch (_UsageState5)
                                    {
                                        case "14": //已歇業
                                            switch (_UsageState6)
                                            {
                                                case "0": //繳銷經營許可執照
                                                    return "63";
                                                case "1": //註銷經營許可執照
                                                    return "64";
                                                case "2": //法拍
                                                    switch (_UsageState7)
                                                    {
                                                        case "0": //換發經營許可執照
                                                            return "68";
                                                    }
                                                    break;
                                                case "999":
                                                    return "62";
                                            }
                                            break;
                                        case "999":
                                            return "62";
                                    }
                                    break;
                                case "37": //核駁
                                    return "65";
                                case "38": //廢止
                                    return "66";
                                case "39": //撤案
                                    return "67";
                                case "45": //失效
                                    return "71";
                                case "999":
                                    return "61";
                            }
                            break;
                        case "19": //撤案
                            return "70";
                        case "20": //廢止經營許可執照
                            return "74";
                        case "21": //核照逾期開業失效
                            return "72";
                        case "999"://已開業-----                   
                            return "30";

                    }
                    break;
                    #endregion
            }




            return "無";//沒填



        }

        public ActionResult getAreaCode(string GSLCode = "")
        {
            //代碼-鄉鎮
            Dou.Models.DB.IModelEntity<AreaCode> model = new Dou.Models.DB.ModelEntity<AreaCode>(new OilGasModelContextExt());
            var query = model.GetAll();

            if (GSLCode != "")
            {
                List<string> GSLCodes = GSLCode.Split(',').ToList();
                query = query.Where(a => GSLCodes.Any(b => a.GSLCode == b));
            }

            query.OrderBy(a => a.Rank);

            return Json(new { result = true, roads = query.ToList() }, JsonRequestBehavior.AllowGet);            
        }

        //自用加儲油-使用狀況Detail(1.申請中,2.申請中－失效,3.使用中,4.使用中－失效)
        public ActionResult getDDLUsageStateDetail(string kind = "")
        {
            var details = WebUI.GetDDLUsageStateDetail(kind);

            return Json(new { result = true, details = details }, JsonRequestBehavior.AllowGet);
        }

        public partial class UsageStatebasic
        {

            [StringLength(10)]
            [Required]
            [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select,
         SelectGearingWith = "UsageState2,Parent,true",
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.UsageStateCode1, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
            [Display(Name = "營運狀況(選單1)", Order = 18)]
            public string UsageState1 { get; set; }

            [StringLength(10)]
            [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState3,Parent,true",
             SelectItemsClassNamespace = UsageState2SelectItemsClassImp.AssemblyQualifiedName)]
            [Display(Name = "營運狀況(選單2)", Order = 19)]
            public string UsageState2 { get; set; }

            [StringLength(10)]
            [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState4,Parent,true",
              SelectItemsClassNamespace = UsageState3SelectItemsClassImp.AssemblyQualifiedName)]
            [Display(Name = "營運狀況(選單3)", Order = 20)]
            public string UsageState3 { get; set; }

            [StringLength(10)]
            [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState5,Parent,true",
             SelectItemsClassNamespace = UsageState4SelectItemsClassImp.AssemblyQualifiedName)]
            [Display(Name = "營運狀況(選單4)", Order = 21)]
            public string UsageState4 { get; set; }

            [StringLength(10)]
            [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState6,Parent,true",
             SelectItemsClassNamespace = UsageState5SelectItemsClassImp.AssemblyQualifiedName)]
            [Display(Name = "營運狀況(選單5)", Order = 22)]
            public string UsageState5 { get; set; }

            [StringLength(10)]
            [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select, SelectGearingWith = "UsageState7,Parent,true",
             SelectItemsClassNamespace = UsageState6SelectItemsClassImp.AssemblyQualifiedName)]
            [Display(Name = "營運狀況(選單6)", Order = 23)]
            public string UsageState6 { get; set; }

            [StringLength(10)]
            [ColumnDef(Visible = false, Sortable = true, EditType = EditType.Select,
             SelectItemsClassNamespace = UsageState7SelectItemsClassImp.AssemblyQualifiedName)]
            [Display(Name = "營運狀況(選單7)", Order = 24)]
            public string UsageState7{ get; set; }

        }
     
    }
}