namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Check_Counseling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }

        [StringLength(10)]
        [Display(Name = "輔導編號", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Counseling_No { get; set; }

        [StringLength(50)]
        [Display(Name = "石油設施類型", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"CarFuel_BasicData\":\"汽/機車加油站\",\"CarGas_BasicData\":\"汽車加氣站\",\"FishGas_BasicData\":\"漁船加油站\",\"SelfFuel_Basic\":\"自用加儲油\",\"SelfGas_Basic\":\"自用加儲氣\"}")]
        public string CaseType { get; set; }


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


        [StringLength(70)]
        [Display(Name = "石油設施名稱", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Gas_Name { get; set; }

        [StringLength(50)]
        [Display(Name = "油氣設施編號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string CaseNo { get; set; }


        [Display(Name = "輔導日期", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public DateTime? Counseling_Date { get; set; }




        
        [StringLength(70)]
        [Display(Name = "營業主體", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = false, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_BusinessOrganization, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        public string Business_theme { get; set; }

        [StringLength(70)]
        [Display(Name = "營業主體名稱", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Business_theme_Name { get; set; }

        [StringLength(200)]
        [Display(Name = "油氣設施地址", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Addr { get; set; }

        [Display(Name = "通話時間", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public DateTime? Talk_time { get; set; }

        [StringLength(25)]
        [Display(Name = "洽電紀錄人", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Officials { get; set; }

        [StringLength(25)]
        [Display(Name = "站方接話人", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Record { get; set; }


        [StringLength(20)]
        [Display(Name = "傳真號碼", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string FaxNo { get; set; }


        [StringLength(50)]
        [Display(Name = "電子郵件信箱", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string E_mail { get; set; }





        [Display(Name = "輔導人員姓名", Order = 1)]
        [StringLength(25)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Counseling_Staff { get; set; }


        [Display(Name = "石油設施站方人員姓名", Order = 1)]
        [StringLength(25)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_Staff { get; set; }

        [Display(Name = "連絡電話", Order = 1)]
        [StringLength(25)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_Tel { get; set; }







        [Display(Name = "輔導原因(篩選)", Order = 1)]
        [Column(TypeName = "ntext")]
        [ColumnDef(Visible = false, VisibleEdit = true,EditType =EditType.Radio, SelectItems = "{\"污染控制場址\":\"污染控制場址\",\"污染整治場址\":\"污染整治場址\",\"土污法第7條第5項限期改善\":\"土污法第7條第5項限期改善\",\"污染潛勢(A級)\":\"污染潛勢(A級)\",\"污染潛勢(B1級)\":\"污染潛勢(B1級)\",\"污染潛勢(B2級)\":\"污染潛勢(B2級)\",\"污染潛勢(C級)\":\"污染潛勢(C級)\",\"現場查核PID/FID>500ppmV或LEL>25%\":\"現場查核PID/FID>500ppmV或LEL>25%\",\"環保主管機關查證有土壤及地下水污染疑慮\":\"環保主管機關查證有土壤及地下水污染疑慮\",\"其他\":\"其他\"}")]
        public string Notes { get; set; }


        [StringLength(500)]
        [Display(Name = "現勘觀察情形", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType= EditType.TextArea)]
        public string LiveSituation { get; set; }

        [StringLength(500)]
        [Display(Name = "問題分析", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.TextArea)]
        public string ProblemAnalysis { get; set; }

        [StringLength(500)]
        [Display(Name = "輔導建議", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.TextArea)]
        public string CounselingAdvice { get; set; }

        [Display(Name = "輔導報告上傳", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [NotMapped]
        public string File_Name { get; set; }





        [Display(Name = "發文日期", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public DateTime? Isseud_Date { get; set; }

        [Display(Name = "發文類型", Order = 1)]
        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Isseud_Class { get; set; }


        [StringLength(30)]
        [Display(Name = "發文字號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Isseud_No { get; set; }

        [StringLength(100)]
        [Display(Name = "發文資料", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Isseud_Data { get; set; }

        [StringLength(20)]
        [Display(Name = "發文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Isseud_Units { get; set; }

        [StringLength(30)]
        [Display(Name = "受文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "副本單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Copy_Unit { get; set; }


































        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(100)]
        public string Achievement_Data { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(70)]
        public string Unit { get; set; }



        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(3)]
        public string AreaCode { get; set; }



    }
}
