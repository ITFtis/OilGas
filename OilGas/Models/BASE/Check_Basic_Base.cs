namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Basic_Base
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        [Display(Name = "查核編號", Order = 1)]
        [ColumnDef(Filter = true)]
        public string CheckNo { get; set; }

        [Display(Name = "查核日期", Order = 1)]
        [ColumnDef(Filter = true,FilterAssign =FilterAssignType.Between)]
        public DateTime? CheckDate { get; set; }







        [Display(Name = "縣市", Order = 1)]
        [ColumnDef(Filter = true, Visible = false, VisibleEdit = false, EditType = EditType.Select,
         SelectItemsClassNamespace = UsercitySelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY
        {
            get
            {
                if (CaseNo!=null&&CaseNo.Length > 6)
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










        [StringLength(50)]
        [Display(Name = "石油設施類型", Order = 0)]
        [Required]
        [ColumnDef(Visible = true, Filter = true, Sortable = true, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"CarFuel_BasicData\":\"汽/機車加油站\",\"FishGas_BasicData\":\"漁船加油站\",\"SelfFuel_Basic\":\"自用加儲油\"}")]
        public string CaseType { get; set; }






        //根據CaseType選項換選項
        //可以選不同加油站類型的加油站
        //要把選項換成可搜尋
        //儲存一次存兩格
        //[StringLength(50)]
        [Display(Name = "石油設施編號", Order = 1)]
        public string CaseNo { get; set; }

        [Display(Name = "石油設施名稱", Order = 1)]
        [StringLength(70)]
        public string Gas_Name { get; set; }


























        [Display(Name = "營業主體", Order = 1)]
        [ColumnDef(Filter = true, VisibleEdit = true, Visible = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_BusinessOrganization, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "ShortName")]
        [StringLength(70)]
        public string Business_theme { get; set; }

        [Display(Name = "石油設施地址", Order = 1)]
        [StringLength(200)]
        public string Addr { get; set; }

        [Display(Name = "通話日期/時間", Order = 1)]
        public DateTime? Talk_time { get; set; }

        [Display(Name = "站方接話人", Order = 1)]
        [StringLength(25)]
        public string Officials { get; set; }

        [Display(Name = "洽電紀錄人", Order = 1)]
        [StringLength(25)]
        public string Record { get; set; }


       


        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? Testing_dates { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Testing_instruments { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Weather { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Tank_total { get; set; }

        [StringLength(25)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Testing_personnel { get; set; }

        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Improve { get; set; }

        [Column(TypeName = "ntext")]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Improve_Notes { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Check_Style { get; set; }



        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Frequency { get; set; }

        [StringLength(2)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Improve_Day { get; set; }

        [StringLength(100)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Check_Data { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? Isseud_Date { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Isseud_Class { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Isseud_No { get; set; }

        [StringLength(100)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Isseud_Data { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Isseud_Units { get; set; }

        [StringLength(70)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Shouwen_Units { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Copy_Unit { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Column(TypeName = "ntext")]
        public string Isseud_Notes { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? TankCount { get; set; }

        [StringLength(3)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string AreaCode { get; set; }

        [StringLength(70)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Business_theme_Name { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string OtherImprove { get; set; }

        [StringLength(20)]
        [ColumnDef(Visible = false)]
        [Display(Name = "傳真號碼", Order = 1)]
        public string FaxNo { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false)]
        [Display(Name = "電子郵件信箱", Order = 1)]
        public string E_mail { get; set; }




        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string CheckTable { get; set; }

        [StringLength(2)]
        [ColumnDef(Visible = false, Filter = true, Sortable = true, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"0\":\"地上\",\"1\":\"地下\"}")]
        [Display(Name = "儲油槽", Order = 1)]
        public string Tank_Well { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? Check1 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? Check2 { get; set; }
    }
}
