namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfFuel_Dispatch
    {
        [Key]
        [Column(Order = 0)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        [Display(Name = "案件編號", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }

        [Display(Name = "發文日期", Order = 1)]
        public DateTime? DispatchDate { get; set; }

        [Display(Name = "發文類型", Order = 1)]
        [ColumnDef(Sortable = true, EditType = EditType.Select,
 SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
  SelectSourceModelNamespace = "OilGas.Models.DispatchClassCode, OilGas",
  SelectSourceModelValueField = "Value",
  SelectSourceModelDisplayField = "Name")]
        [StringLength(2)]
        public string DispatchClass { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "其他發文類型", Order = 1)]
        [StringLength(20)]
        public string OtherDispatchClass { get; set; }

        [Display(Name = "發文字號", Order = 1)]//發文字號(單位)
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(20)]
        public string License_No { get; set; }


        [Display(Name = " ", Order = 1)]//發文字號(編號)
        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string DispatchNo { get; set; }



        [Display(Name = "發文字號", Order = 1)]
        [StringLength(50)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string License_Dispatch_No
        {
            get
            {
                if (License_No + DispatchNo == "")
                {
                    return "-";
                }
                return License_No + DispatchNo + "號";
            }
            set
            {
            }
        }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(50)]
        public string FileOriginalName { get; set; }



        [Display(Name = "發文資料", Order = 1)]
        [StringLength(50)]
        public string FileNewName { get; set; }






        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? FileSize { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? FileUpLoadDate { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(20)]
        public string DispatchUnit { get; set; }

        [Display(Name = "受文者單位", Order = 1)]
        [StringLength(20)]
        public string ReceiveUnit { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"臺北市政府環境保護局\":\"臺北市政府環境保護局\",\"臺中市政府環境保護局\":\"臺中市政府環境保護局\",\"基隆市政府環境保護局\":\"基隆市政府環境保護局\",\"臺南市政府環境保護局\":\"臺南市政府環境保護局\",\"高雄市政府環境保護局\":\"高雄市政府環境保護局\",\"新北市政府環境保護局\":\"新北市政府環境保護局\",\"宜蘭縣政府環境保護局\":\"宜蘭縣政府環境保護局\",\"桃園縣政府環境保護局\":\"桃園縣政府環境保護局\",\"嘉義市政府環境保護局\":\"嘉義市政府環境保護局\",\"新竹縣政府環境保護局\":\"新竹縣政府環境保護局\",\"苗栗縣政府環境保護局\":\"苗栗縣政府環境保護局\",\"南投縣政府環境保護局\":\"南投縣政府環境保護局\",\"彰化縣政府環境保護局\":\"彰化縣政府環境保護局\",\"新竹市政府環境保護局\":\"新竹市政府環境保護局\",\"雲林縣政府環境保護局\":\"雲林縣政府環境保護局\",\"嘉義縣政府環境保護局\":\"嘉義縣政府環境保護局\",\"屏東縣政府環境保護局\":\"屏東縣政府環境保護局\",\"花蓮縣政府環境保護局\":\"花蓮縣政府環境保護局\",\"臺東縣政府環境保護局\":\"臺東縣政府環境保護局\",\"金門縣政府環境保護局\":\"金門縣政府環境保護局\",\"澎湖縣政府環境保護局\":\"澎湖縣政府環境保護局\",\"連江縣政府環境保護局\":\"連江縣政府環境保護局\",\"經濟部能源局\":\"經濟部能源局\"}")]
        [Display(Name = "副本單位", Order = 1)]
        [StringLength(500)]
        public string CopyUnit { get; set; }

        [Display(Name = "其他副本單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(250)]
        public string OtherCopyUnit { get; set; }

        [Display(Name = "備註", Order = 1)]
        [StringLength(500)]
        public string DispatchNote { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string CreateUserTemp { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? IsConfirm { get; set; }



        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
