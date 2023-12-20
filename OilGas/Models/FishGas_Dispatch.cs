namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FishGas_Dispatch
    {
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public long ID { get; set; }

        [StringLength(10)]
        [Display(Name = "案件編號", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }

        [Display(Name = "發文日期", Order = 1)]
        public DateTime? Dispatch_date { get; set; }

        [Display(Name = "發文類型", Order = 1)]
        [ColumnDef(Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.DispatchClassCode, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [StringLength(50)]
        public string DispatchClass { get; set; }

        [Display(Name = "發文字號", Order = 1)]//發文字號(單位)
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(20)]
        public string License_No { get; set; }

        [Display(Name = " ", Order = 1)]//發文字號(編號)
        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Dispatch_No { get; set; }


        [Display(Name = "發文字號", Order = 1)]
        [StringLength(50)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string License_Dispatch_No
        {
            get
            {
                if (License_No + Dispatch_No == "")
                {
                    return "-";
                }
                return License_No + Dispatch_No + "號";
            }
            set
            {
            }
        }



        [Display(Name = "發文資料", Order = 1)]
        [StringLength(30)]
        public string File_name { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(30)]
        public string DispatchUnit { get; set; }

        [Display(Name = "受文者單位", Order = 1)]
        [StringLength(30)]
        public string Shouwen_Units { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"臺北市政府環境保護局\":\"臺北市政府環境保護局\",\"臺中市政府環境保護局\":\"臺中市政府環境保護局\",\"基隆市政府環境保護局\":\"基隆市政府環境保護局\",\"臺南市政府環境保護局\":\"臺南市政府環境保護局\",\"高雄市政府環境保護局\":\"高雄市政府環境保護局\",\"新北市政府環境保護局\":\"新北市政府環境保護局\",\"宜蘭縣政府環境保護局\":\"宜蘭縣政府環境保護局\",\"桃園縣政府環境保護局\":\"桃園縣政府環境保護局\",\"嘉義市政府環境保護局\":\"嘉義市政府環境保護局\",\"新竹縣政府環境保護局\":\"新竹縣政府環境保護局\",\"苗栗縣政府環境保護局\":\"苗栗縣政府環境保護局\",\"南投縣政府環境保護局\":\"南投縣政府環境保護局\",\"彰化縣政府環境保護局\":\"彰化縣政府環境保護局\",\"新竹市政府環境保護局\":\"新竹市政府環境保護局\",\"雲林縣政府環境保護局\":\"雲林縣政府環境保護局\",\"嘉義縣政府環境保護局\":\"嘉義縣政府環境保護局\",\"屏東縣政府環境保護局\":\"屏東縣政府環境保護局\",\"花蓮縣政府環境保護局\":\"花蓮縣政府環境保護局\",\"臺東縣政府環境保護局\":\"臺東縣政府環境保護局\",\"金門縣政府環境保護局\":\"金門縣政府環境保護局\",\"澎湖縣政府環境保護局\":\"澎湖縣政府環境保護局\",\"連江縣政府環境保護局\":\"連江縣政府環境保護局\",\"經濟部能源局\":\"經濟部能源局\"}")]
        [Display(Name = "副本單位", Order = 1)]
        [StringLength(500)]
        public string CopyUnit { get; set; }

        [Display(Name = "其他副本單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(250)]
        public string otherCopyUnit { get; set; }

        [Display(Name = "備註", Order = 1)]
        [StringLength(200)]
        public string Note { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(52)]
        public string MemberID { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
