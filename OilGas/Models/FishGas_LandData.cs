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
        [ColumnDef(Display = "�d�ߴ���-���O", EditType = EditType.Select, SelectItems = "{'0':'�{�p��ƭק�ɶ�','1':'�ܧ���{��Ʈɶ�'}"
            , Filter = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string DateType { get; set; }

        [ColumnDef(EditType = EditType.Date, Display = "�d�ߴ���", Filter = true, FilterAssign = FilterAssignType.Between, Visible = false
            , ColSize = 3)]
        [NotMapped]
        public DateTime ModDate { get; set; }

        [ColumnDef(Display = "�g�a���-�����p�e��", EditType = EditType.Select,
            SelectItemsClassNamespace = LandUsageZoneCodeSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string LandUsageZoneCode0 { get; set; }

        [ColumnDef(Display = "�g�a���-�D�����p�e��-�g�a�ϥΤ���", EditType = EditType.Select,
            SelectItemsClassNamespace = _LandUsageZoneCodeSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string LandUsageZoneCode1 { get; set; }

        [ColumnDef(Display = "�g�a���-�D�����p�e��-�Φa���O", EditType = EditType.Select,
            SelectItemsClassNamespace = LandClassCodeSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string LandClassCode { get; set; }

        [ColumnDef(Display = "����", EditType = EditType.Select, SelectGearingWith = "AreaCode,CityCode",
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string CityCode { get; set; }

        [ColumnDef(Display = "�m����", EditType = EditType.Select,
            SelectItemsClassNamespace = AreaCodeSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false, ColSize = 3)]
        [NotMapped]
        public string AreaCode { get; set; }
    }
}
