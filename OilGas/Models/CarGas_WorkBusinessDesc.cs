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

    public partial class CarGas_WorkBusinessDesc
    {   
        [Key]
        [ColumnDef(Display = "�[�o���s��", Sortable = true)]
        [NotMapped]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "�[�o���W��", Sortable = true)]
        [NotMapped]
        public string Gas_Name { get; set; }

        [ColumnDef(Display = "��~�D��", Visible = false, Sortable = true)]
        [NotMapped]
        public string Business_theme { get; set; }

        [ColumnDef(Display = "�o����", Visible = false, Sortable = true)]
        [NotMapped]
        public string Dispatch_date { get; set; }

        [ColumnDef(Display = "�o��帹", Visible = false, Sortable = true)]
        [NotMapped]
        public string Dispatch_No { get; set; }

        [ColumnDef(Display = "�o�����O", Sortable = true)]
        [NotMapped]
        public string DispatchName { get; set; }

        [ColumnDef(Display = "�����O", Sortable = true)]
        [NotMapped]
        public string CityName { get; set; }

    }
}
