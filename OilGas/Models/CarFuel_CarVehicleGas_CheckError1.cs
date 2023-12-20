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

    public partial class vw_CarFuel_CarVehicleGas_CheckError1
    {
        [Key]
        [ColumnDef(Display = "����", Sortable = true)]
        public Int64 ItemIndex { get; set; }

        [ColumnDef(Display = "�d�ֽs��", Sortable = true)]
        public string CheckNo { get; set; }

        [ColumnDef(Display = "�d�֤��", Sortable = true)]
        public string CheckDate { get; set; }

        [ColumnDef(Display = "�]�I�׸�", Sortable = true)]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "�W��", Sortable = true)]
        public string Check_Gas_Name { get; set; }

        [ColumnDef(Display = "��~�D��", Sortable = true)]
        public string Check_Business { get; set; }

        [ColumnDef(Display = "�a�}", Sortable = true)]
        public string Check_Addr { get; set; }

        [ColumnDef(Display = "�W��", Sortable = true)]
        public string Case_Gas_Name { get; set; }

        [ColumnDef(Display = "��~�D��", Sortable = true)]
        public string Case_Business { get; set; }

        [ColumnDef(Display = "�a�}", Sortable = true)]
        public string Case_Addr { get; set; }

        [ColumnDef(Display = "", Sortable = true)]
        public string Case_UsageState { get; set; }

    }
}
