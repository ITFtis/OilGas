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
        [Display(Name = "®×¥ó½s¸¹", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }

        [StringLength(10)]
        [Display(Name = "³c°âªo«~ºØÃþ", Order = 2)]
        [ColumnDef(Visible = true, EditType = EditType.Select, SelectItems = "{\"¥ÒºØº®²î¥Îªo\":\"¥ÒºØº®²î¥Îªo\",\"¤AºØº®²î¥Îªo\":\"¤AºØº®²î¥Îªo\",\"¨Tªo\":\"¨Tªo\",\"®ãªo\":\"®ãªo\"}")]

        public string Oil_type { get; set; }

        [StringLength(10)]
        [Display(Name = "ªo¼ÑºØÃþ", Order = 3)]
        [ColumnDef(Visible = true, EditType = EditType.Select, SelectItems = "{\"¥ÒºØº®²î¥Îªo\":\"¥ÒºØº®²î¥Îªo\",\"¤AºØº®²î¥Îªo\":\"¤AºØº®²î¥Îªo\",\"¨Tªo\":\"¨Tªo\",\"®ãªo\":\"®ãªo\"}")]
        public string Tank_type { get; set; }

        [Display(Name = "Àx¼Ñ®e¶q", Order = 4)]
        public int? Tank_type_tank { get; set; }


        [Display(Name = "Àx¼Ñ¼Æ¶q", Order = 5)]
        public int? Tank_type_tank_seat { get; set; }


        [StringLength(52)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string MemberID { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
