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

    public partial class CarFuel_LandData
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
    public class LandUsageZoneCodeSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.LandUsageZoneCodeSelectItemsClassImp, OilGas";

        protected static IEnumerable<LandUsageZoneCode> _luzcs0;
        protected static new IEnumerable<LandUsageZoneCode> LUZCS0
        {
            get
            {
                _luzcs0 = DouHelper.Misc.GetCache<IEnumerable<LandUsageZoneCode>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_luzcs0 == null)
                {
                    _luzcs0 = Rpt_CarFuel_Land.GetAllLandUsageZoneCode().Where(x => x.LandType == 0).Distinct().ToArray();
                    DouHelper.Misc.AddCache(_luzcs0, AssemblyQualifiedName);
                }
                return _luzcs0;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return LUZCS0.Select(s => new KeyValuePair<string, object>(s.Value, s.Name));
        }
    }
    public class _LandUsageZoneCodeSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models._LandUsageZoneCodeSelectItemsClassImp, OilGas";

        protected static IEnumerable<LandUsageZoneCode> _luzcs1;
        protected static new IEnumerable<LandUsageZoneCode> LUZCS1
        {
            get
            {
                _luzcs1 = DouHelper.Misc.GetCache<IEnumerable<LandUsageZoneCode>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_luzcs1 == null)
                {
                    _luzcs1 = Rpt_CarFuel_Land.GetAllLandUsageZoneCode().Where(s => s.LandType == 1).Distinct().ToList();
                    DouHelper.Misc.AddCache(_luzcs1, AssemblyQualifiedName);
                }
                return _luzcs1;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return LUZCS1.Select(s => new KeyValuePair<string, object>(s.Value, s.Name));
        }
    }
    public class LandClassCodeSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.LandClassCodeSelectItemsClassImp, OilGas";

        protected static IEnumerable<LandClassCode> _lccs;
        protected static new IEnumerable<LandClassCode> LCCS
        {
            get
            {
                _lccs = DouHelper.Misc.GetCache<IEnumerable<LandClassCode>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_lccs == null)
                {
                    _lccs = Rpt_CarFuel_Land.GetAllLandClassCode();
                    DouHelper.Misc.AddCache(_lccs, AssemblyQualifiedName);
                }
                return _lccs;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return LCCS.Select(s => new KeyValuePair<string, object>(s.Value, s.Name));
        }
    }
    public class AreaCodeSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.AreaCodeSelectItemsClassImp, OilGas";

        protected static IEnumerable<AreaCode> _acs;
        protected static new IEnumerable<AreaCode> ACS
        {
            get
            {
                _acs = DouHelper.Misc.GetCache<IEnumerable<AreaCode>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_acs == null)
                {
                    _acs = Rpt_CarFuel_Land.GetAllAreaCode();
                    DouHelper.Misc.AddCache(_acs, AssemblyQualifiedName);
                }
                return _acs;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return ACS.Select(s => new KeyValuePair<string, object>(s.AreaCode1, "{\"v\":\"" + s.AreaName + "\",\"CityCode\":\"" + s.CityCode + "\"}"));
        }
    }
}
