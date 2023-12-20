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

    public partial class vw_CarFuel_CaseError1
    {
        [Key]
        [ColumnDef(Display = "����", Sortable = true)]
        public Int64 CaseIndex { get; set; }

        [ColumnDef(Display = "�ץ�s��", Sortable = true)]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "�۪o�]�I", Sortable = true)]
        public string Gas_Name { get; set; }

        [ColumnDef(Display = "��~�D��", Sortable = true)]
        public string Business_theme { get; set; }

        [ColumnDef(Display = "�۪o�]�I�a�}", Sortable = true)]
        public string Address { get; set; }

        [ColumnDef(Display = "���ʤ��", Sortable = true)]
        public string Mod_date { get; set; }

        [ColumnDef(Display = "�̫���B���A", Sortable = true)]
        public string UsageStateName { get; set; }

        [ColumnDef(Display = "�̫�o����", Sortable = true)]
        public string Last_Dispatch_date { get; set; }

        [ColumnDef(Display = "�̫�o�媬�p", Sortable = true)]
        public string Last_DispatchName { get; set; }

    }
}
