namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;


    [Table("Lazybag")]
    public partial class Lazybag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int s_index { get; set; }

        [StringLength(200)]
        [Display(Name = "���D�W��", Order = 1)]
        public string s_titel { get; set; }

        [StringLength(100)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "�o����", Order = 1)]
        public string s_postdept { get; set; }

        [Display(Name = "�o����", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public DateTime? s_postdate { get; set; }

        [StringLength(200)]
        [Display(Name = "�Ϥ��ɦ�m", Order = 1)]
        public string s_pic { get; set; }

        [StringLength(200)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "�����W�s��", Order = 1)]
        public string s_http { get; set; }

        [StringLength(1024)]
        [Display(Name = "�Ƶ�����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string s_memo { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string s_end_date { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string s_end_post { get; set; }
    }
}
