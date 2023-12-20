namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Check_Consolidated_Comments_Action
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }

        [StringLength(10)]
        [Display(Name = "查核編號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string CheckNo { get; set; }

        [StringLength(50)]
        [Display(Name = "案件編號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string CaseNo { get; set; }


        [Display(Name = "縣市", Order = 1)]
        [ColumnDef(Filter = true, Visible = false, VisibleEdit = false, EditType = EditType.Select,
      SelectItemsClassNamespace = UsercitySelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY
        {
            get
            {
                if (CaseNo != null && CaseNo.Length > 6)
                {
                    return CaseNo.Substring(4, 2);
                }
                else
                {
                    return CaseNo;
                }
            }
            set
            {
            }
        }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }

        [StringLength(50)]
        [Display(Name = "目的事業主管機關人員(職業)", Order = 1)]
        public string Fac_Titles { get; set; }

        [StringLength(25)]
        [Display(Name = "目的事業主管機關人員(姓名)", Order = 1)]
        public string Fac_Name { get; set; }

        [StringLength(50)]
        [Display(Name = "加油站人員(職業)", Order = 1)]
        public string Gas_Titles { get; set; }

        [StringLength(25)]
        [Display(Name = "加油站人員(姓名)", Order = 1)]
        public string Gas_Name { get; set; }

        [StringLength(50)]
        [Display(Name = "會同機關(構)人員(職業)", Order = 1)]
        public string Organ_Titles { get; set; }

        [StringLength(25)]
        [Display(Name = "會同機關(構)人員(姓名)", Order = 1)]
        public string Organ_Name { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Frequency { get; set; }
    }
}
