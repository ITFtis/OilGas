namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Collections;
    using System.Linq;


    [Table("FileDownload")]
    public partial class FileDownload
    {
        [Key]
        [Display(Name = "UUID", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public Guid UUID { get; set; }

        [Display(Name = "檔名", Order = 1)]
        [Required]
        [StringLength(200)]
        public string File_name { get; set; }

        [Display(Name = "檔案敘述", Order = 1)]
        [StringLength(500)]
        public string Detail { get; set; }

        [Display(Name = "檔案分類", Order = 1)]
        [Required]
        [ColumnDef(Filter = true, VisibleEdit = true, Visible = true, EditType = EditType.Select,
 SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
  SelectSourceModelNamespace = "OilGas.Models.FileType, OilGas",
  SelectSourceModelValueField = "Id",
  SelectSourceModelDisplayField = "type")]
        public int type { get; set; }

        [Display(Name = "新增人員", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [StringLength(200)]
        public string AddMemberID { get; set; }

        [Display(Name = "新增時間", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        public DateTime? AddTime { get; set; }

        [Display(Name = "更改人員", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(200)]
        public string UpdateMemberID { get; set; }

        [Display(Name = "更改時間", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? UpdateTime { get; set; }

    }
}
