using DouHelper;
using Microsoft.Ajax.Utilities;
using NPOI.OpenXml4Net.OPC.Internal;
using NPOI.OpenXmlFormats.Dml.Diagram;
using NPOI.OpenXmlFormats.Vml;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.Util;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.PortGas
{
    [Dou.Misc.Attr.MenuDef(Id = "PortGas_BasicData", Name = "現況資料匯出", MenuPath = "航港自用加儲油/E統計報表專區", Action = "Index", Index = 1, Func = Dou.Misc.Attr.FuncEnum.ALL, AllowAnonymous = false)]
    public class PortGas_BasicDataController : Controller
    {
        // GET: PortGas_BasicData
        public ActionResult Index()
        {
            if (!AppConfig.IsDev)
            {
                //非開發階段
                if (Dou.Context.CurrentUserBase == null)
                {
                    return Redirect("~/Home/Index");
                }
            }

            System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();

            //設施地點
            Dou.Models.DB.IModelEntity<PortGas_Code> portGas_Code = new Dou.Models.DB.ModelEntity<PortGas_Code>(dbContext);
            ViewBag.portGas_CodeMaster = portGas_Code.GetAll().Where(a=>a.PortGasCode_Type == "A0").OrderBy(a => a.PortGasCode_No).ToList();
            List<string> strs = new List<string>() { "A1", "A2", "A3" };
            ViewBag.portGas_CodeDetail = portGas_Code.GetAll().Where(a => strs.Contains(a.PortGasCode_Type))
                .OrderBy(a => a.PortGasCode_Type).ThenBy(a => a.PortGasCode_No).ToList();

            return View();
        }

        public ActionResult ExportPortGas_BasicData(ReportData.ViewParams objs)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.航港自用加儲油_E統計報表專區_現況資料);
            string fileTitle = "航港自用加儲油_現況資料";

            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                List<string> titles = new List<string>() { "航港加儲油_現況資料，查詢條件:" };

                //1.取得資料
                System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
                var datas = OilGas.Rpt_PortGas_BasicData.GetAllDatas();
                var n = datas.Count();

                //2.條件篩選 (conditions)                   
                if (objs.conditions != null)
                {
                    var txt_ModifyStartTime = objs.conditions.Where(a => a.Id == "txt_ModifyStartTime").FirstOrDefault();
                    if (txt_ModifyStartTime != null)
                    {
                        DateTime date = DateTime.Parse(txt_ModifyStartTime.Value);
                        datas = datas.Where(a => a.Mod_date >= date);
                        titles.Add("查詢期間(現況資料修改時間)起:" + txt_ModifyStartTime.Value);
                    }
                    var txt_ModifyEndTime = objs.conditions.Where(a => a.Id == "txt_ModifyEndTime").FirstOrDefault();
                    if (txt_ModifyEndTime != null)
                    {
                        DateTime date = DateTime.Parse(txt_ModifyEndTime.Value);
                        datas = datas.Where(a => a.Mod_date <= date);
                        titles.Add("查詢期間(現況資料修改時間)迄:" + txt_ModifyEndTime.Value);
                    }

                    //設施地點類型
                    var ddl_portGas_CodeMaster = objs.conditions.Where(a => a.Id == "ddl_portGas_CodeMaster").FirstOrDefault();
                    if (ddl_portGas_CodeMaster != null)
                    {
                        string str = ddl_portGas_CodeMaster.Value;
                        datas = datas.Where(a => a.設施地點類型 == str);
                        titles.Add("設施地點類型:" + str);
                    }

                    //設施地點
                    var ddl_portGas_CodeDetail = objs.conditions.Where(a => a.Id == "ddl_portGas_CodeDetail").FirstOrDefault();
                    if (ddl_portGas_CodeDetail != null)
                    {
                        string str = ddl_portGas_CodeDetail.Value;
                        datas = datas.Where(a => a.設施地點 == str);
                        titles.Add("設施地點:" + str);
                    }

                    //使用狀況ddl_BigUsage
                    var ddl_BigUsage = objs.conditions.Where(a => a.Id == "ddl_BigUsage").FirstOrDefault();
                    if (ddl_BigUsage != null)
                    {
                        string str = ddl_BigUsage.Value;
                        datas = datas.Where(a => a.UsageState == str);

                        //titles add
                        switch (str)
                        {
                            case "01":
                                titles.Add("使用狀況:" + "申請中");
                                break;
                            case "02":
                                titles.Add("使用狀況:" + "同意設置");
                                break;
                            case "03":
                                titles.Add("使用狀況:" + "同意使用");
                                break;
                            case "04":
                                titles.Add("使用狀況:" + "使用中");
                                break;
                        }                        
                    }

                    var txt_CaseNo = objs.conditions.Where(a => a.Id == "txt_CaseNo").FirstOrDefault();
                    if (txt_CaseNo != null)
                    {
                        string str = txt_CaseNo.Value;
                        datas = datas.Where(a => a.案件編號 == str);
                        titles.Add("案件編號:" + str);
                    }

                    //sqlWhr += sqlHelper.AssembleWhrSQLWithAND(condition.Gas_Name, " ( base.Gas_Name LIKE '%{0}%' or base.Gas_Location like '%{0}%' )");
                    var txt_GasName = objs.conditions.Where(a => a.Id == "txt_GasName").FirstOrDefault();
                    if (txt_GasName != null)
                    {
                        string str = txt_GasName.Value;
                        datas = datas.Where(a => a.設施名稱.IndexOf(str) > -1
                                    || a.設施所在地.IndexOf(str) > -1);
                        titles.Add("設施名稱/所在地:" + str);
                    }                    
                }

                //3.輸出欄位(distinct) (no distinct error:顯示全部欄位)
                bool cbApparatusOwner = false;
                bool cbUsageNames = false;
                bool cbExpiredDate = false;
                bool cbStartDate = false;
                bool cbEndDate = false;
                bool cbLicenseNo = false;
                bool cbBoss = false;
                bool cbBoss_Tel = false;
                bool cbBoss_Email = false;
                bool cbBasicFacilities = false;
                bool cbOtherFacilities = false;
                bool cbSupplyTarget = false;
                bool cbInsurance = false;                

                if (objs.columns != null)
                {
                    cbApparatusOwner = objs.columns.Where(b => b.Id == "cbApparatusOwner").Count() > 0;
                    cbUsageNames = objs.columns.Where(b => b.Id == "cbUsageNames").Count() > 0;
                    cbExpiredDate = objs.columns.Where(b => b.Id == "cbExpiredDate").Count() > 0;
                    cbStartDate = objs.columns.Where(b => b.Id == "cbStartDate").Count() > 0;
                    cbEndDate = objs.columns.Where(b => b.Id == "cbEndDate").Count() > 0;
                    cbLicenseNo = objs.columns.Where(b => b.Id == "cbLicenseNo").Count() > 0;
                    cbBoss = objs.columns.Where(b => b.Id == "cbBoss").Count() > 0;
                    cbBoss_Tel = objs.columns.Where(b => b.Id == "cbBoss_Tel").Count() > 0;
                    cbBoss_Email = objs.columns.Where(b => b.Id == "cbBoss_Email").Count() > 0;
                    cbBasicFacilities = objs.columns.Where(b => b.Id == "cbBasicFacilities").Count() > 0;
                    cbOtherFacilities = objs.columns.Where(b => b.Id == "cbOtherFacilities").Count() > 0;
                    cbSupplyTarget = objs.columns.Where(b => b.Id == "cbSupplyTarget").Count() > 0;
                    cbInsurance = objs.columns.Where(b => b.Id == "cbInsurance").Count() > 0;                    
                }

                var output = datas.Select(a => new
                {
                    a.案件編號,
                    a.設施地點類型,
                    a.設施地點,
                    a.設施名稱,
                    a.設施所在地,
                    a.變更次數,
                    設備使用人 = cbApparatusOwner ? a.設備使用人 : "",
                    目前狀態 = cbUsageNames ? a.目前狀態 : "",
                    使用狀況第一層 = cbUsageNames ? a.使用狀況第一層 : "",
                    使用狀況第二層 = cbUsageNames ? a.使用狀況第二層 : "",
                    使用狀況第三層 = cbUsageNames ? a.使用狀況第三層 : "",
                    使用狀況第四層 = cbUsageNames ? a.使用狀況第四層 : "",
                    收件日期 = cbExpiredDate ? a.收件日期 : "",
                    核准設置日期 = cbStartDate ? a.核准設置日期 : "",
                    結束使用日期 = cbEndDate ? a.結束使用日期 : "",
                    核准設置文號 = cbLicenseNo ? a.核准設置文號 : "",
                    負責人姓名 = cbBoss ? a.負責人姓名 : "",
                    聯絡電話 = cbBoss_Tel ? a.聯絡電話 : "",
                    電子郵件信箱 = cbBoss_Email ? a.電子郵件信箱 : "",
                    基本設施 = cbBasicFacilities ? a.基本設施 : "",
                    其他設施 = cbOtherFacilities ? a.其他設施 : "",
                    供油對象 = cbSupplyTarget ? a.供油對象 : "",
                    保險公司名稱 = cbInsurance ? a.保險公司名稱 : "",
                    保單號碼 = cbInsurance ? a.保單號碼 : "",
                    保單有效期間_起 = cbInsurance ? a.保單有效期間_起 : null,
                    保單有效期間_迄 = cbInsurance ? a.保單有效期間_迄 : null,
                    保險類型 = cbInsurance ? a.保險類型 : "",
                }).Distinct().ToList();

                //4.產出Dynamic資料 (給Excel)
                List<dynamic> list = new List<dynamic>();

                foreach (var data in output)
                {
                    dynamic f = new ExpandoObject();
                    f.案件編號 = data.案件編號;
                    f.設施地點類型 = data.設施地點類型;
                    f.設施地點 = data.設施地點;
                    f.設施名稱 = data.設施名稱;
                    f.設施所在地 = data.設施所在地;

                    //欄位挑選
                    if (objs.columns != null)
                    {
                        if (cbApparatusOwner)
                        {
                            f.設備使用人 = data.設備使用人;
                        }
                        if (cbUsageNames)
                        {                            
                            f.目前狀態 = data.目前狀態;
                            f.使用狀況第一層 = data.使用狀況第一層;
                            f.使用狀況第二層 = data.使用狀況第二層;
                            f.使用狀況第三層 = data.使用狀況第三層;
                            f.使用狀況第四層 = data.使用狀況第四層;
                        }
                        if (cbExpiredDate)
                        {
                            f.收件日期 = data.收件日期;
                        }
                        if (cbStartDate)
                        {
                            f.核准設置日期 = data.核准設置日期;
                        }
                        if (cbEndDate)
                        {
                            f.結束使用日期 = data.結束使用日期;
                        }
                        if (cbLicenseNo)
                        {
                            f.核准設置文號 = data.核准設置文號;
                        }
                        if (cbBoss)
                        {
                            f.負責人姓名 = data.負責人姓名;
                        }
                        if (cbBoss_Tel)
                        {
                            f.聯絡電話 = data.聯絡電話;
                        }
                        if (cbBoss_Email)
                        {
                            f.電子郵件信箱 = data.電子郵件信箱;
                        }
                        if (cbBasicFacilities)
                        {
                            f.基本設施 = data.基本設施;
                        }
                        if (cbOtherFacilities)
                        {
                            f.其他設施 = data.其他設施;
                        }
                        if (cbSupplyTarget)
                        {
                            f.供油對象 = data.供油對象;
                        }
                        if (cbInsurance)
                        {
                            f.保險公司名稱 = data.保險公司名稱;
                        }
                        if (cbInsurance)
                        {
                            f.保單號碼 = data.保單號碼;
                        }
                        if (cbInsurance)
                        {
                            f.保單有效期間_起 = data.保單有效期間_起;
                        }
                        if (cbInsurance)
                        {
                            f.保單有效期間_迄 = data.保單有效期間_迄;
                        }
                        if (cbInsurance)
                        {
                            f.保險類型 = data.保險類型;
                        }
                    }

                    f.變更次數 = data.變更次數;

                    f.SheetName = fileTitle;//sheep.名稱;
                    list.Add(f);
                }

                //查無符合資料表數
                if (list.Count == 0)
                {
                    return Json(new { result = false, errorMessage = "查無符合資料表數" }, JsonRequestBehavior.AllowGet);
                }

                //5.產出excel
                string fileName = OilGas.ExcelSpecHelper.GenerateExcelByLinqF1(fileTitle, titles, list, folder, "N");
                string path = folder + fileName;
                url = OilGas.Cm.PhysicalToUrl(path);
            }
            catch (Exception ex)
            {
                error = ex.Message + ex.StackTrace;
            }

            if (url == "")
            {
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ResetExport()
        {
            try
            {
                Rpt_PortGas_BasicData.ResetGetAllDatas();
                Rpt_PortGas_BasicData.GetAllDatas();
            }
            catch (Exception ex)
            {
                string error = ex.Message + ex.StackTrace;
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
    }
}