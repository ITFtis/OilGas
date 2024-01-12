using Dou.Controllers;
using Dou.Misc;
using Dou.Misc.Attr;
using Dou.Models.DB;
using Newtonsoft.Json;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarFuel
{
    [Dou.Misc.Attr.MenuDef(Id = "CarFuel_Update", Name = "負責人批次變更", MenuPath = "加油站/A管理專區", Action = "Index", Index = 2, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class CarFuel_UpdateController : APaginationModelController<CarFuel_BasicData>
    {
        // GET: CarFuel_Update
        public ActionResult Index()
        {
            return View();
        }

        protected override IModelEntity<CarFuel_BasicData> GetModelEntity()
        {
            return new ModelEntity<CarFuel_BasicData>(new OilGasModelContextExt());
        }

        protected override IQueryable<CarFuel_BasicData> BeforeIQueryToPagedList(IQueryable<CarFuel_BasicData> iquery, params KeyValueParams[] paras)
        {
            //進入頁面不顯示清單(未使用查詢)
            KeyValueParams filter = paras.FirstOrDefault((KeyValueParams s) => s.key == "filter");
            if (filter == null)
            {
                return new List<CarFuel_BasicData>().AsQueryable();
            }

            return base.BeforeIQueryToPagedList(iquery, paras);
        }

        //批次變更表單
        public ActionResult BatchSave(List<string> CaseNos, vwe_CarFuel_UpdateForm obj)
        {
            try
            {
                string ab = "!23";
            }
            catch (Exception ex)
            {
                return Json(new { result = false, errorMessage = ex.Message });
            }

            return Json(new { result = true });
        }

        public override DataManagerOptions GetDataManagerOptions()
        {
            var opts = base.GetDataManagerOptions();

            //全部欄位排序
            foreach (var field in opts.fields)
                field.filter = false;

            opts.GetFiled("Gas_Name").filter = true;            
            opts.GetFiled("Business_theme").filter = true;            
            opts.GetFiled("Boss").visible = true;
            opts.GetFiled("Recipient_date").visible = false;

            return opts;
        }

        public virtual ActionResult getUpdateForm()
        {
            var opts = Dou.Misc.DataManagerScriptHelper.GetDataManagerOptions<vwe_CarFuel_UpdateForm>();

            //全部欄位排序
            foreach (var field in opts.fields)
                field.sortable = true;

            opts.addable = false;
            //opts.editable = false;
            opts.deleteable = false;

            opts.singleDataEdit = true;
            opts.editformWindowStyle = "showEditformOnly";

            vwe_CarFuel_UpdateForm data = new vwe_CarFuel_UpdateForm();
            opts.datas = new List<vwe_CarFuel_UpdateForm>() { data };

            var jstr = JsonConvert.SerializeObject(opts, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            jstr = jstr.Replace(DataManagerScriptHelper.JavaScriptFunctionStringStart, "(").Replace(DataManagerScriptHelper.JavaScriptFunctionStringEnd, ")");
            return Content(jstr, "application/json");
        }

    }

    public class vwe_CarFuel_UpdateForm
    {
        [Display(Name = "負責人名稱")]
        [ColumnDef(ColSize = 3)]
        public string txt_BossName { get; set; }

        [Display(Name = "負責人身份證字號")]
        [ColumnDef(ColSize = 3)]
        public string txt_ID_No { get; set; }

        ////[Display(Name = "負責人聯絡地址")]
        ///[ColumnDef(ColSize = 3)]
        ////public string xxxxx { get; set; }

        [Display(Name = "負責人聯絡電話")]
        [ColumnDef(ColSize = 3)]
        public string txt_Boss_Tel { get; set; }

        [Display(Name = "電子郵件信箱")]
        [ColumnDef(ColSize = 3)]
        public string txt_Boss_Email { get; set; }

        [Display(Name = "發文日期")]
        [ColumnDef(ColSize = 3)]
        public string txt_Dispatch_date { get; set; }

        //[Display(Name = "發文字號1")]
        //[ColumnDef(ColSize = 3)]
        //public string xxxxxx { get; set; }

        [Display(Name = "發文字號2")]
        [ColumnDef(ColSize = 3)]
        public string txt_Dispatch_No2 { get; set; }

        ////[Display(Name = "發文資料")]
        ///[ColumnDef(ColSize = 3)]
        ////public string xxxxx { get; set; }

        [Display(Name = "受文者單位")]
        [ColumnDef(ColSize = 3)]
        public string txt_Shouwen_Units { get; set; }

        ////[Display(Name = "副本單位")]
        ///[ColumnDef(ColSize = 3)]
        ////public string xxxxx { get; set; }

    }
}