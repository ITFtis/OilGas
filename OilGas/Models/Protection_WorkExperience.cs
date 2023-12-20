namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Protection_WorkExperience
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int Id { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? BasicDataId { get; set; }

        [Display(Name = "單位名稱", Order = 1)]
        [StringLength(20)]
        public string UnitName { get; set; }

        [Display(Name = "職務名稱", Order = 1)]
        [StringLength(20)]
        public string WorkTitle { get; set; }

        [Display(Name = "工作時間(起)", Order = 1)]
        public DateTime? WorkStartDate { get; set; }

        [Display(Name = "工作時間(訖)", Order = 1)]
        public DateTime? WorkEndDate { get; set; }

        [Display(Name = "工作內容", Order = 1)]
        [StringLength(200)]
        public string WorkContent { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(10)]
        public string CreateUser { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? CreateTime { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(10)]
        public string ModifyUser { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? ModifyTime { get; set; }
    }
}
