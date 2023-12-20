namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using static OilGas.Models.LocationSelectItemsClassImp;

    public partial class FishGas_LandData
    {
        [ColumnDef(Display = "查詢期間-類別", EditType = EditType.Select, SelectItems = "{'0':'現況資料修改時間','1':'變更歷程資料時間'}"
            , Filter = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string DateType { get; set; }

        [ColumnDef(EditType = EditType.Date, Display = "查詢期間", Filter = true, FilterAssign = FilterAssignType.Between, Visible = false
            , ColSize = 3)]
        [NotMapped]
        public DateTime ModDate { get; set; }

        [ColumnDef(Display = "土地資料-都市計畫區", EditType = EditType.Select,
            SelectItemsClassNamespace = LandUsageZoneCodeSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string LandUsageZoneCode0 { get; set; }

        [ColumnDef(Display = "土地資料-非都市計畫區-土地使用分區", EditType = EditType.Select,
            SelectItemsClassNamespace = _LandUsageZoneCodeSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string LandUsageZoneCode1 { get; set; }

        [ColumnDef(Display = "土地資料-非都市計畫區-用地類別", EditType = EditType.Select,
            SelectItemsClassNamespace = LandClassCodeSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string LandClassCode { get; set; }

        [ColumnDef(Display = "縣市", EditType = EditType.Select, SelectGearingWith = "AreaCode,CityCode",
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string CityCode { get; set; }

        [ColumnDef(Display = "鄉鎮市區", EditType = EditType.Select,
            SelectItemsClassNamespace = AreaCodeSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string AreaCode { get; set; }
    }
}
