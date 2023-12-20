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

    public partial class Audit_ReportCheckDistribution
    {
        [Key]
        [ColumnDef(Display = "ホ猳砞琁摸", Visible = false, Filter = true, EditType = EditType.Select,
            SelectGearingWith = "Business_theme,CaseType,true",
            SelectItemsClassNamespace = ReportCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string CaseType { get; set; }

        [ColumnDef(Display = "犁穨砰", Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = CarVehicleGas_BusinessOrganizationV2SelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string Business_theme { get; set; }      

        [ColumnDef(Display = "┮郡カ", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        [NotMapped]
        public string CityCode1 { get; set; }

        [ColumnDef(Display = "", Sortable = true)]
        public string CheckYear { get; set; }

        [ColumnDef(Display = "单A", Sortable = true)]
        public int LevA { get; set; }

        [ColumnDef(Display = "单B", Sortable = true)]
        public int LevB { get; set; }

        [ColumnDef(Display = "单C", Sortable = true)]
        public int LevC { get; set; }

        [ColumnDef(Display = "单D", Sortable = true)]
        public int LevD { get; set; }

        [ColumnDef(Display = "单E", Sortable = true)]
        public int LevE { get; set; }

        [ColumnDef(Display = "单N", Sortable = true)]
        public int LevN { get; set; }

        [ColumnDef(Display = "璸", Sortable = true)]
        public int LevAll { get; set; }
    }
}
