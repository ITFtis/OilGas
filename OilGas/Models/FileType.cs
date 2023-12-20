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


    [Table("FileType")]
    public partial class FileType
    {
        [Key]
        [Display(Name = "ID", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int Id { get; set; }

        [Display(Name = "檔案分類", Order = 1)]
        [Required]
        [StringLength(50)]
        public string type { get; set; }

        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        [StringLength(200)]
        public string Remark { get; set; }

       
    }
}
