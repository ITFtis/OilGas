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
        [Display(Name = "�ɮצW��", Order = 1)]
        public string LawData_FileName { get; set; }

        [StringLength(200)]
        [Display(Name = "�K�n", Order = 1)]
        public string LawData_DataName { get; set; }

        [StringLength(100)]
        [Display(Name = "�����帹", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string LawData_WorkNo { get; set; }

        [StringLength(30)]
        [Display(Name = "�o����", Order = 1)]
        public string LawData_WorkDate { get; set; }



        [StringLength(400)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "��������", Order = 1)]
        public string LawData_DownLad { get; set; }








      
        [ColumnDef(Visible = false, VisibleEdit = true, Filter = true, FilterAssign = FilterAssignType.Contains)]
        [StringLength(100)]
        [Display(Name = "����r", Order = 1)]
        public string LawData_KeyWord { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(4000)]
        [Display(Name = "���ӻ���", Order = 1)]
        public string LawData_Desc { get; set; }




        //���F�j�M���������
        [Display(Name = "�k�W", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false, Filter = true, EditType = EditType.Select,
            SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
             SelectGearingWith = "LawMath_LawItemNo,Parent,true",
             SelectSourceModelNamespace = "OilGas.Models.Law_Item, OilGas",
             SelectSourceModelValueField = "Law_name",
             SelectSourceModelDisplayField = "Law_name")]
        [NotMapped]
        public string LawMath_LawItem { get; set; }


        //���F�j�M���������
        [Display(Name = "����", Order = 1)]
        [NotMapped]
        [ColumnDef(Visible = false, VisibleEdit = false, Filter = true, EditType = EditType.Select,
             SelectItemsClassNamespace = LawMath_LawItemNoSelectItemsClassImp.AssemblyQualifiedName)]
        public string LawMath_LawItemNo { get; set; }
      

    }
}
