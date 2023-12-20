namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class news
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int news_id { get; set; }

        [StringLength(100)]
        [Display(Name = "�D��", Order = 1)]
        [ColumnDef(EditType =EditType.TextArea)]
        [Required]
        public string news_sub { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string news_user { get; set; }

        [StringLength(2)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string news_userLv { get; set; }

        [Display(Name = "�o���ɶ�", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        public DateTime? news_date { get; set; }

        [Column(TypeName = "text")]
        [Display(Name = "�ԲӤ��e", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.TextArea)]
        public string news_txt { get; set; }

        [StringLength(50)]
        [Display(Name = "���[�ɮ�", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string news_file { get; set; }
    }
}
