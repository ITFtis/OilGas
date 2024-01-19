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

    public partial class Info_Dashboard
    {
        [Key]
        [ColumnDef(Display = "石油設施類型", Visible = false, Filter = true, EditType = EditType.Select,
            SelectItemsClassNamespace = ReportCaseTypeSelectItemsClassImp.AssemblyQualifiedName)]
        [NotMapped]
        public string CaseType { get; set; }

        [Display(Name = "年度查詢")]
        [ColumnDef(Visible = false, EditType = EditType.Select,
            Filter = true, SelectItemsClassNamespace = OilGas.CheckYearSelectItems.AssemblyQualifiedName)]
        [NotMapped]
        public int CheckYear { get; }

        [ColumnDef(Display = "縣市", Visible = false, VisibleEdit = false, EditType = EditType.Select, Filter = true,
            SelectItemsClassNamespace = UsercityCodeSelectItems.AssemblyQualifiedName)]
        [NotMapped]
        public string CityCode1 { get; set; }


        [ColumnDef(Display = "已查核家數", Visible = true, VisibleEdit = false)]
        public int Sum_Total {  get; set; }

    }
}
