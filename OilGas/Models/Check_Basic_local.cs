namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("Check_Basic_local")]
    public partial class Check_Basic_local
    {
        [Key]
        [Display(Name = "加油站編號", Order = 1)]
        public string CaseNo { get; set; }

        [Display(Name = "加油站名稱", Order = 1)]
        [StringLength(70)]
        public string Gas_Name { get; set; }


        [Display(Name = "檢查人員", Order = 1)]
        public string inspectors { get; set; }


        [Display(Name = "受檢加油站陪檢人員", Order = 1)]
        public string Inspection { get; set; }

        [Display(Name = "受檢加油站陪檢人員職稱", Order = 1)]
        public string Inspection_title { get; set; }


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


        [Display(Name = "檢查日期", Order = 1)]
        public DateTime? CheckDate { get; set; }

        [Display(Name = "下載EXCEL", Order = 1)]
        [ColumnDef( Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string EXCEL
        {
            get
            {
                return CaseNo;
            }
            set
            {
            }
        }
       


        [StringLength(100)]
        [Display(Name = "以下標誌", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"營業主體\",\"1\":\"加油站站名\",\"2\":\"營業時間\",\"3\":\"供油廠商標誌或名稱\"}")]
        public string A01_Options { get; set; }

        [StringLength(1)]
        [Display(Name = "是否於明顯處所標示", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string A01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string A01_Notes { get; set; }




        [StringLength(100)]
        [Display(Name = "下列場所是否設置警戒標誌及標線：(請勾選檢查結果為符合項目)", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"儲油槽區設置「嚴禁煙火」標誌（紅底白字標示板製作）。\",\"1\":\"加油區設置「熄火加油」、「嚴禁煙火」標誌（紅底白字標示板製作）。\",\"2\":\"加油站出入口設方向標誌，並於地面劃白色箭頭標線。\",\"3\":\"卸油區於地面以黃線標示。\",\"4\":\"雨棚設置高度標誌。\"}")]
        public string A02_Options { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string A02_Notes { get; set; }

        [StringLength(100)]
        [Display(Name = "是否標示符合下列規定之售油種類及油品價格：(請勾選檢查結果為符合項目)", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"標示價格為當日各油品之零售價格。\",\"1\":\"標示位置為距臨路地界入口處5公尺範圍內。\",\"2\":\"標示方式為固定式，標示物下緣離地面1公尺以上，且不得有遮蔽物遮蔽，並輔以夜間照明。\",\"3\":\"標示價格之數字規格，每字高17公分以上，寬10公分以上。\"}")]
        public string A03_Options { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string A03_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "加油站經營許可執照登記事項是否與原核准事項相符。", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string B01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B01_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "加油站基地出、入口、自助加油區及面積之平面配置是否與原核准相符。", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string B02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B02_Notes { get; set; }








        [StringLength(1)]
        [Display(Name = "原核准加油機槍數1", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_gun1 { get; set; }


        [Display(Name = "原核准加油機槍數1台數", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B03_gun1_value { get; set; }

        [StringLength(1)]
        [Display(Name = "原核准加油機槍數2", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_gun2 { get; set; }


        [Display(Name = "原核准加油機槍數2台數", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B03_gun2_value { get; set; }

        [StringLength(1)]
        [Display(Name = "原核准加油機槍數3", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_gun3 { get; set; }


        [Display(Name = "原核准加油機槍數3台數", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B03_gun3_value { get; set; }

        [StringLength(1)]
        [Display(Name = "原核准加油機槍數4", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_gun4 { get; set; }


        [Display(Name = "原核准加油機槍數4台數", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B03_gun4_value { get; set; }


        [StringLength(1)]
        [Display(Name = "加油機是否與原核准相符：", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string B03 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B03_Notes { get; set; }












        [StringLength(1)]
        [Display(Name = "原核准地下儲油槽公秉1", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B04_tank1 { get; set; }


        [Display(Name = "原核准地下儲油槽公秉1座數", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B04_tank1_value { get; set; }

        [StringLength(1)]
        [Display(Name = "原核准地下儲油槽公秉2", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B04_tank2 { get; set; }


        [Display(Name = "原核准地下儲油槽公秉2座數", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B04_tank2_value { get; set; }

        [StringLength(1)]
        [Display(Name = "原核准地下儲油槽公秉3", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B04_tank3 { get; set; }

        [Display(Name = "原核准地下儲油槽公秉3座數", Order = 1)]
        [ColumnDef(Visible = false)]
        public int? B04_tank3_value { get; set; }


        [StringLength(1)]
        [Display(Name = "地下儲油槽是否與原核准相符：", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string B04 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B04_Notes { get; set; }






        [StringLength(100)]
        [Display(Name = "以下設置附屬設施是否經申請核准。", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"汽機車簡易保養設施\",\"1\":\"洗車設施\",\"2\":\"簡易排污檢測服務設施\",\"3\":\"銷售汽機車用品設施\",\"4\":\"自動販賣機\",\"5\":\"多媒體事務機\",\"6\":\"接受事業機構委託收費服務設施\"}")]
        public string B05_Options { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string B05 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B05_Notes { get; set; }





        [StringLength(100)]
        [Display(Name = "以下設置兼營項目是否經申請核准。", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"便利商店\",\"1\":\"販售農產品\",\"2\":\"停車場\",\"3\":\"車用液化石油氣\",\"4\":\"代辦汽車定期檢驗\",\"5\":\"汽機車與自行車買賣及租賃\",\"6\":\"經銷公益彩券\",\"7\":\"廣告服務\",\"8\":\"提供場所供設置金融機構營業場所外自動化服務設備\",\"9\":\"接受他人委託代收物品服務\",\"10\":\"從事僅供收受洗滌物場所之洗衣業務\",\"11\":\"提供營業站屋屋頂設置行動電話基地台\",\"12\":\"屋頂供他人設置裝置容量不及五百瓩並利用太陽能之自用發電設備\",\"13\":\"其他經中央主管機關核准之兼營項目\"}")]
        public string B06_Options { get; set; }

        [StringLength(1)]
        [Display(Name = "檢查結果", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string B06 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string B06_Notes { get; set; }


        [StringLength(1)]
        [Display(Name = "是否加入加油站商業同業公會。", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string C01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string C01_Notes { get; set; }


        [StringLength(1)]
        [Display(Name = "是否投保公共意外責任保險及意外污染責任險。", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string D01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string D01_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "保險之保險金額是否達最小保險金額。", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string D02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string D02_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "是否依加油站營運設備自行安全檢查表自行實施加油站設施每日、每月及每半年安全檢查，並製作檢查紀錄。", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string E01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string E01_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "前項安全檢查紀錄是否保存1年以上。", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string E02 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string E02_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "前項安全檢查項目經檢查是否有缺失", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"否\",\"1\":\"是\"}")]
        public string E03 { get; set; }


        [Display(Name = "改善日期。", Order = 1)]
        [ColumnDef(Visible = false)]
        public DateTime? E_date { get; set; }

        [StringLength(1)]
        [Display(Name = "加油站購油之證明文件是否保存1年以上。", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"不符合\",\"1\":\"符合\"}")]
        public string F01 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string F01_Notes { get; set; }



        [StringLength(100)]
        [Display(Name = "現場稽查，是否有發現下列情事", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"對販售之汽油、柴油摻雜或作偽。\",\"1\":\"以流量式加油機以外之器具，販售汽油、柴油。\",\"2\":\"於核准基地範圍外進行汽油、柴油之交付行為或營業。\",\"3\":\"設置未經核准之出、入口，供車輛通行及加油使用。\",\"4\":\"於加油站內等候車道以外之地區，進行加油行為。\",\"5\":\"灌注柴油至內容積總和超過4,000公升油罐車之罐槽體或車輛裝載之油槽（桶）。\",\"6\":\"灌注汽油至油罐車之罐槽體或車輛裝載內容積總和達200公升以上之油槽（桶）。\",\"7\":\"其他對公共安全有影響之虞之行為。\"}")]
        public string G01 { get; set; }

        [StringLength(1)]
        [Display(Name = "現場稽查，是否有特殊用油需求者以桶（槽）加油之販售行為（填否者，下列2款免填）", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"否\",\"1\":\"是\"}")]
        public string G02 { get; set; }

        [StringLength(1)]
        [Display(Name = "汽油數量在10公升以上或柴油數量在500公升以上者，業者是否依中央主管機關規定之登記表登記，並於消費者攜帶之容器上粘貼中央主管機關規定之警語標籤", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"否\",\"1\":\"是\"}")]
        public string G02_1 { get; set; }

        [StringLength(1)]
        [Display(Name = "消費者攜帶之容器是否為玻璃容器、紙質容器或塑膠袋。", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"否\",\"1\":\"是\"}")]
        public string G02_2 { get; set; }

        [StringLength(500)]
        [Display(Name = "備註", Order = 1)]
        [ColumnDef(Visible = false)]
        public string H_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "受檢加油站陪檢人員是否有陳述意見", Order = 1)]
        [ColumnDef(Visible = false, EditType = EditType.Radio, SelectItems = "{\"0\":\"否\",\"1\":\"是\"}")]
        public string I01 { get; set; }

        [StringLength(500)]
        [Display(Name = "陳述意見如下：", Order = 1)]
        [ColumnDef(Visible = false)]
        public string I01_Notes { get; set; }
    }
}
