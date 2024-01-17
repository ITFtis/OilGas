namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;

    public partial class Audit_CounselingReportAFMissing
    {       
        [ColumnDef(Display = "�Ҧb����", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        [NotMapped]
        public string CityCode1 { get; set; }

        [ColumnDef(Display = "��~�D��", Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = CarVehicleGas_BusinessOrganizationV3SelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string Business_theme { get; set; }
        [Key]
        [ColumnDef(Display = "�d�֦���", Sortable = true)]
        public int Sum_A_CheckCount { get; set; }

        [ColumnDef(Display = "�d�֯ʥ���", Sortable = true)]
        public int Sum_A_CheckDoesmeet { get; set; }

        [ColumnDef(Display = "�����ʥ���", Sortable = true)]
        public string Sum_A_AvgDoesmeet { get; set; }

        [ColumnDef(Display = "�d�֦���", Sortable = true)]
        public int Sum_F_CheckCount { get; set; }

        [ColumnDef(Display = "�d�֯ʥ���", Sortable = true)]
        public int Sum_F_CheckDoesmeet { get; set; }

        [ColumnDef(Display = "�����ʥ���", Sortable = true)]
        public string Sum_F_AvgDoesmeet { get; set; }

        [ColumnDef(Display = "�d�֦���", Sortable = true)]
        public int Sum_N_CheckCount { get; set; }

        [ColumnDef(Display = "�d�֯ʥ���", Sortable = true)]
        public int Sum_N_CheckDoesmeet { get; set; }

        [ColumnDef(Display = "�����ʥ���", Sortable = true)]
        public string Sum_N_AvgDoesmeet { get; set; }
    }
}
