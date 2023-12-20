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
    using System.Runtime.Remoting.Messaging;
    using System.Web.Mvc;

    public partial class Audit_ReportMissing_statistics
    {
        [Key]
        [ColumnDef(Display = "石油設施類型", Visible = false, Filter = true, EditType = EditType.Select,
            SelectGearingWith = "workYear,CaseType,true",
            SelectItemsClassNamespace = OrganizationCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string CaseType { get; set; }

        [ColumnDef(Display = "查詢年度", Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = AuditReportMissingStatisticsCountyYearsSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string workYear { get; set; }

        [ColumnDef(Display = "營業主體", Visible = false, Filter = true, EditType = EditType.Select,
            SelectItems = "{}")]
        [NotMapped]
        public string Business_theme { get; set; }

        [ColumnDef(Display = "檢查設備名稱", Sortable = true)]
        public string CheckItemTitel { get; set; }

        [ColumnDef(Display = "檢查項目", Sortable = true)]
        public int CheckItemCount { get; set; }

        [ColumnDef(Display = "缺失數(項次)", Sortable = true)]
        public int CheckItemErrCount { get; set; }

    }
}
