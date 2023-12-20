namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;





    public partial class Law_Data
    {
    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int LawData_Index { get; set; }

        [StringLength(100)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string LawData_Titel { get; set; }


        [StringLength(20)]
        [Display(Name = "檔案名稱", Order = 1)]
        public string LawData_FileName { get; set; }

        [StringLength(200)]
        [Display(Name = "摘要", Order = 1)]
        public string LawData_DataName { get; set; }

        [StringLength(100)]
        [Display(Name = "函釋文號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string LawData_WorkNo { get; set; }

        [StringLength(30)]
        [Display(Name = "發文日期", Order = 1)]
        public string LawData_WorkDate { get; set; }



        [StringLength(400)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "函釋附件", Order = 1)]
        public string LawData_DownLad { get; set; }








      
        [ColumnDef(Visible = false, VisibleEdit = true, Filter = true, FilterAssign = FilterAssignType.Contains)]
        [StringLength(100)]
        [Display(Name = "關鍵字", Order = 1)]
        public string LawData_KeyWord { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(4000)]
        [Display(Name = "明細說明", Order = 1)]
        public string LawData_Desc { get; set; }




        //為了搜尋的虛擬欄位
        [Display(Name = "法規", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false, Filter = true, EditType = EditType.Select,
            SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
             SelectGearingWith = "LawMath_LawItemNo,Parent,true",
             SelectSourceModelNamespace = "OilGas.Models.Law_Item, OilGas",
             SelectSourceModelValueField = "Law_name",
             SelectSourceModelDisplayField = "Law_name")]
        [NotMapped]
        public string LawMath_LawItem { get; set; }


        //為了搜尋的虛擬欄位
        [Display(Name = "條文", Order = 1)]
        [NotMapped]
        [ColumnDef(Visible = false, VisibleEdit = false, Filter = true, EditType = EditType.Select,
             SelectItemsClassNamespace = LawMath_LawItemNoSelectItemsClassImp.AssemblyQualifiedName)]
        public string LawMath_LawItemNo { get; set; }
      

    }
}
