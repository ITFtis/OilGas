namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Check_document
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }


        [StringLength(10)]
        [Display(Name = "查核編號", Order = 1)]
        [ColumnDef(Filter =true)]
        public string CheckNo { get; set; }

        [Display(Name = "查核日期", Order = 1)]
        public DateTime? CheckDate { get; set; }

        [Display(Name = "複查日期", Order = 1)]
        public DateTime? CheckDate_Action { get; set; }

        [StringLength(70)]
        [Display(Name = "石油設施名稱", Order = 1)]
        [ColumnDef(Filter = true)]
        public string Gas_Name { get; set; }

        [StringLength(70)]
        [Display(Name = "營業主體", Order = 1)]
        public string Business_theme { get; set; }

        [StringLength(200)]
        [Display(Name = "石油設施地址", Order = 1)]
        public string Addr { get; set; }

        [Display(Name = "石油設施查核報告日期", Order = 1)]
        public DateTime? Gas_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "石油設施查核報告", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_File { get; set; }



        [StringLength(30)]
        [Display(Name = "查核報告發文字號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_Isseud_No { get; set; }

        [Display(Name = "查核報告發文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(20)]
        public string Gas_Isseud_Units { get; set; }

        [StringLength(70)]
        [Display(Name = "查核報告受文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"臺北市政府-產業發展局\":\"臺北市政府-產業發展局\",\"新北市政府-經濟發展局\":\"新北市政府-經濟發展局\",\"基隆市政府-工務處\":\"基隆市政府-工務處\",\"桃園市政府-經濟發展局\":\"桃園市政府-經濟發展局\",\"新竹市政府-產業發展處\":\"新竹市政府-產業發展處\",\"新竹縣政府-國際產業發展處\":\"新竹縣政府-國際產業發展處\",\"宜蘭縣政府-工商旅遊處\":\"宜蘭縣政府-工商旅遊處\",\"花蓮縣政府-觀光處\":\"花蓮縣政府-觀光處\",\"連江縣政府-建設局\":\"連江縣政府-建設局\",\"臺中市政府-經濟發展局\":\"臺中市政府-經濟發展局\",\"苗栗縣政府-工商發展處\":\"苗栗縣政府-工商發展處\",\"南投縣政府-建設處\":\"南投縣政府-建設處\",\"彰化縣政府-建設處\":\"彰化縣政府-建設處\",\"雲林縣政府-建設處\":\"雲林縣政府-建設處\",\"臺南市政府-經濟發展局\":\"臺南市政府-經濟發展局\",\"高雄市政府-經濟發展局\":\"高雄市政府-經濟發展局\",\"嘉義市政府-建設處\":\"嘉義市政府-建設處\",\"嘉義縣政府-經濟發展處\":\"嘉義縣政府-經濟發展處\",\"屏東縣政府-城鄉發展處\":\"屏東縣政府-城鄉發展處\",\"臺東縣政府-財政與經濟發展處\":\"臺東縣政府-財政與經濟發展處\",\"金門縣政府-建設局\":\"金門縣政府-建設局\",\"澎湖縣政府-建設處\":\"澎湖縣政府-建設處\"}")]
        public string Gas_Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "查核報告副本單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_Copy_Unit { get; set; }





        [Display(Name = "地方政府檢送檢查報告日期", Order = 1)]
        public DateTime? EPB_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "地方政府檢送檢查報告", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB_File { get; set; }

        [Display(Name = "石油設施複查報告日期", Order = 1)]
        public DateTime? GAS2_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "石油設施複查報告", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string GAS2_File { get; set; }

        [StringLength(30)]
        [Display(Name = "複查報告發文字號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas2_Isseud_No { get; set; }

        [StringLength(20)]
        [Display(Name = "複查報告發文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas2_Isseud_Units { get; set; }

        [StringLength(70)]
        [Display(Name = "複查報告受文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"臺北市政府-產業發展局\":\"臺北市政府-產業發展局\",\"新北市政府-經濟發展局\":\"新北市政府-經濟發展局\",\"基隆市政府-工務處\":\"基隆市政府-工務處\",\"桃園市政府-經濟發展局\":\"桃園市政府-經濟發展局\",\"新竹市政府-產業發展處\":\"新竹市政府-產業發展處\",\"新竹縣政府-國際產業發展處\":\"新竹縣政府-國際產業發展處\",\"宜蘭縣政府-工商旅遊處\":\"宜蘭縣政府-工商旅遊處\",\"花蓮縣政府-觀光處\":\"花蓮縣政府-觀光處\",\"連江縣政府-建設局\":\"連江縣政府-建設局\",\"臺中市政府-經濟發展局\":\"臺中市政府-經濟發展局\",\"苗栗縣政府-工商發展處\":\"苗栗縣政府-工商發展處\",\"南投縣政府-建設處\":\"南投縣政府-建設處\",\"彰化縣政府-建設處\":\"彰化縣政府-建設處\",\"雲林縣政府-建設處\":\"雲林縣政府-建設處\",\"臺南市政府-經濟發展局\":\"臺南市政府-經濟發展局\",\"高雄市政府-經濟發展局\":\"高雄市政府-經濟發展局\",\"嘉義市政府-建設處\":\"嘉義市政府-建設處\",\"嘉義縣政府-經濟發展處\":\"嘉義縣政府-經濟發展處\",\"屏東縣政府-城鄉發展處\":\"屏東縣政府-城鄉發展處\",\"臺東縣政府-財政與經濟發展處\":\"臺東縣政府-財政與經濟發展處\",\"金門縣政府-建設局\":\"金門縣政府-建設局\",\"澎湖縣政府-建設處\":\"澎湖縣政府-建設處\"}")]
        public string Gas2_Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "複查報告副本單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas2_Copy_Unit { get; set; }









        [Display(Name = "地方政府同意之缺失改善結果日期", Order = 1)]
        public DateTime? EPB2_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "地方政府同意之缺失改善結果", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB2_File { get; set; }

        [Display(Name = "複查完成日期", Order = 1)]
        public DateTime? GASend_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "複查完成", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string GASend_File { get; set; }

        [Display(Name = "零缺失日期", Order = 1)]
        public DateTime? ZERO_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "零缺失", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string ZERO_File { get; set; }



































        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "加油站技術諮詢輔導報告日期", Order = 1)]
        public DateTime? GAS3_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "加油站技術諮詢輔導報告", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string GAS3_File { get; set; }

        [StringLength(30)]
        [Display(Name = "加油站技術諮詢輔導報告發文字號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas3_Isseud_No { get; set; }

        [StringLength(20)]
        [Display(Name = "加油站技術諮詢輔導報告發文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas3_Isseud_Units { get; set; }

        [StringLength(70)]
        [Display(Name = "加油站技術諮詢輔導報告受文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"臺北市政府-產業發展局\":\"臺北市政府-產業發展局\",\"新北市政府-經濟發展局\":\"新北市政府-經濟發展局\",\"基隆市政府-工務處\":\"基隆市政府-工務處\",\"桃園市政府-經濟發展局\":\"桃園市政府-經濟發展局\",\"新竹市政府-產業發展處\":\"新竹市政府-產業發展處\",\"新竹縣政府-國際產業發展處\":\"新竹縣政府-國際產業發展處\",\"宜蘭縣政府-工商旅遊處\":\"宜蘭縣政府-工商旅遊處\",\"花蓮縣政府-觀光處\":\"花蓮縣政府-觀光處\",\"連江縣政府-建設局\":\"連江縣政府-建設局\",\"臺中市政府-經濟發展局\":\"臺中市政府-經濟發展局\",\"苗栗縣政府-工商發展處\":\"苗栗縣政府-工商發展處\",\"南投縣政府-建設處\":\"南投縣政府-建設處\",\"彰化縣政府-建設處\":\"彰化縣政府-建設處\",\"雲林縣政府-建設處\":\"雲林縣政府-建設處\",\"臺南市政府-經濟發展局\":\"臺南市政府-經濟發展局\",\"高雄市政府-經濟發展局\":\"高雄市政府-經濟發展局\",\"嘉義市政府-建設處\":\"嘉義市政府-建設處\",\"嘉義縣政府-經濟發展處\":\"嘉義縣政府-經濟發展處\",\"屏東縣政府-城鄉發展處\":\"屏東縣政府-城鄉發展處\",\"臺東縣政府-財政與經濟發展處\":\"臺東縣政府-財政與經濟發展處\",\"金門縣政府-建設局\":\"金門縣政府-建設局\",\"澎湖縣政府-建設處\":\"澎湖縣政府-建設處\"}")]
        public string Gas3_Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "加油站技術諮詢輔導報告副本單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas3_Copy_Unit { get; set; }











        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "地方政府同意延展改善日期", Order = 1)]
        public DateTime? EPB3_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "地方政府同意延展改善", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB3_File { get; set; }

        [StringLength(30)]
        [Display(Name = "地方政府同意延展改善發文字號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB3_Isseud_No { get; set; }

        [StringLength(20)]
        [Display(Name = "地方政府同意延展改善發文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB3_Isseud_Units { get; set; }

        [StringLength(70)]
        [Display(Name = "地方政府同意延展改善受文單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"臺北市政府-產業發展局\":\"臺北市政府-產業發展局\",\"新北市政府-經濟發展局\":\"新北市政府-經濟發展局\",\"基隆市政府-工務處\":\"基隆市政府-工務處\",\"桃園市政府-經濟發展局\":\"桃園市政府-經濟發展局\",\"新竹市政府-產業發展處\":\"新竹市政府-產業發展處\",\"新竹縣政府-國際產業發展處\":\"新竹縣政府-國際產業發展處\",\"宜蘭縣政府-工商旅遊處\":\"宜蘭縣政府-工商旅遊處\",\"花蓮縣政府-觀光處\":\"花蓮縣政府-觀光處\",\"連江縣政府-建設局\":\"連江縣政府-建設局\",\"臺中市政府-經濟發展局\":\"臺中市政府-經濟發展局\",\"苗栗縣政府-工商發展處\":\"苗栗縣政府-工商發展處\",\"南投縣政府-建設處\":\"南投縣政府-建設處\",\"彰化縣政府-建設處\":\"彰化縣政府-建設處\",\"雲林縣政府-建設處\":\"雲林縣政府-建設處\",\"臺南市政府-經濟發展局\":\"臺南市政府-經濟發展局\",\"高雄市政府-經濟發展局\":\"高雄市政府-經濟發展局\",\"嘉義市政府-建設處\":\"嘉義市政府-建設處\",\"嘉義縣政府-經濟發展處\":\"嘉義縣政府-經濟發展處\",\"屏東縣政府-城鄉發展處\":\"屏東縣政府-城鄉發展處\",\"臺東縣政府-財政與經濟發展處\":\"臺東縣政府-財政與經濟發展處\",\"金門縣政府-建設局\":\"金門縣政府-建設局\",\"澎湖縣政府-建設處\":\"澎湖縣政府-建設處\"}")]
        public string EPB3_Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "地方政府同意延展改善副本單位", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB3_Copy_Unit { get; set; }
    }
}
