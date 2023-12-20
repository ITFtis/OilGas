namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FishGas_OilData
    {
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public long ID { get; set; }

        [StringLength(10)]
        [Display(Name = "�ץ�s��", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }

        [StringLength(10)]
        [Display(Name = "�c��o�~����", Order = 2)]
        [ColumnDef(Visible = true, EditType = EditType.Select, SelectItems = "{\"�Һغ���Ϊo\":\"�Һغ���Ϊo\",\"�A�غ���Ϊo\":\"�A�غ���Ϊo\",\"�T�o\":\"�T�o\",\"��o\":\"��o\"}")]

        public string Oil_type { get; set; }

        [StringLength(10)]
        [Display(Name = "�o�Ѻ���", Order = 3)]
        [ColumnDef(Visible = true, EditType = EditType.Select, SelectItems = "{\"�Һغ���Ϊo\":\"�Һغ���Ϊo\",\"�A�غ���Ϊo\":\"�A�غ���Ϊo\",\"�T�o\":\"�T�o\",\"��o\":\"��o\"}")]
        public string Tank_type { get; set; }

        [Display(Name = "�x�Ѯe�q", Order = 4)]
        public int? Tank_type_tank { get; set; }


        [Display(Name = "�x�Ѽƶq", Order = 5)]
        public int? Tank_type_tank_seat { get; set; }


        [StringLength(52)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string MemberID { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
