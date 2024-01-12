using Dou.Misc.Attr;
using Dou.Models.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    /// <summary>
    /// 加油站營運設備自行安全檢查表(無時間限制)
    /// </summary>
    [Table("Check_Basic_NoTime")]
    public class Check_Basic_NoTime
    {
        [Key]
        [StringLength(10)]
        [ColumnDef(EditType = EditType.TextList, SelectGearingWith = "Gas_Name,v",
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_BusinessOrganization, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [Display(Name = "營業主體", Order = 1)]
        public string Business_theme { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        [Display(Name = "其他營業主體")]
        public string otherBusiness_theme { get; set; }


        [Display(Name = "加油(氣)站名稱")]
        [StringLength(70)]
        //[ColumnDef(EditType = EditType.TextList, SelectItemsClassNamespace = OilGas.Models.GasFuleStationNameSelectItemsClassImp.AssemblyQualifiedName)]
        public string Gas_Name { get; set; }

        
        [Display(Name = "檢查日期")]
        [Column(Order = 2)]
        public DateTime? CheckDate { get; set; }

        [StringLength(50)]
        [Display(Name = "地址")]
        [ColumnDef(EditType = EditType.Text)]
        public string Address { get; set; }

        [StringLength(10)]
        [Display(Name = "電話")]
        [ColumnDef(EditType = EditType.Text)]
        public string PhoneNumber { get; set; }

        [Display(Name = "檢查人員")]
        [StringLength(50)]
        public string CheckMan { get; set; }


    }

    public class GasFuleStationNameSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.GasFuleStationNameSelectItemsClassImp, OilGas";

        //取得加油加氣站名稱

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            OilGasModelContextExt _db = new OilGasModelContextExt();
            IModelEntity<CarFuel_BasicData> _CarFuel_BasicData = new ModelEntity<CarFuel_BasicData>(_db);
            IModelEntity<CarGas_BasicData> _CarGas_BasicData = new ModelEntity<CarGas_BasicData>(_db);

            var a = _CarFuel_BasicData.GetAll().Select(x => new GasFuleStationName
            {
                StationName = x.Gas_Name,
                BusinessTheme = x.Business_theme,
                otherBusinessTheme = x.otherBusiness_theme,
                Address = x.Address,

            });

            var b = _CarGas_BasicData.GetAll().Select(x => new GasFuleStationName
            {
                StationName = x.Gas_Name,
                BusinessTheme = x.Business_theme,
                otherBusinessTheme = x.otherBusiness_theme,
                Address = x.Address,
            });

            List<GasFuleStationName> gasFuleStationNames = new List<GasFuleStationName>();
            gasFuleStationNames.AddRange(a);
            gasFuleStationNames.AddRange(b);

            return gasFuleStationNames.Select(z => new KeyValuePair<string, object>(z.StationName, JsonConvert.SerializeObject(new { v = z.BusinessTheme, ov = z.otherBusinessTheme, n = z.StationName, add = z.Address })));


        }

        private class GasFuleStationName
        {
            public string StationName { get; set; }
            public string BusinessTheme { get; set; }
            public string otherBusinessTheme { get; set; }
            public string Address { get; set; }
        }
    }

    
}