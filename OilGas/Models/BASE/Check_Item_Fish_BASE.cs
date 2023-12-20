namespace OilGas.Models.BASE
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Item_Fish_BASE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }

        [StringLength(10)]
        [Display(Name = "查核編號", Order = 1)]
        public string CheckNo { get; set; }

        [Display(Name = "查核時間", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? CheckDate { get; set; }


        [StringLength(50)]
        [Display(Name = "加油站編號", Order = 1)]
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




















        [StringLength(1)]
        [Display(Name = "緊急照明燈功能正常", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A01 { get; set; }

        [StringLength(1)]
        [Display(Name = "營業站屋內放置使用油品安全資料表", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A02 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string A_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "配電盤操作面板前無堆置雜物且開關箱關閉", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B01 { get; set; }

        [StringLength(1)]
        [Display(Name = "電氣設備插頭、插座無損壞，線路無鬆散裸露", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B02 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string B_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "擋油堤完好無裂縫（地上儲油槽）", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C01 { get; set; }

        [StringLength(1)]
        [Display(Name = "卸油泵浦接頭無滲漏（地上儲油槽）", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C02 { get; set; }

        [StringLength(1)]
        [Display(Name = "各陰井之油氣濃度未逾30% LEL（地下儲油槽）", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C03 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string C_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "加油碼頭區域四周無漏油跡象", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D01 { get; set; }

        [StringLength(1)]
        [Display(Name = "地坪(路面)完好無嚴重龜裂、坑洞", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D02 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油槍、橡皮管及各接頭處無滲漏", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D03 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string D_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "流量計接頭無漏油跡象", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string E01 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string E_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "滅火器放置於固定且便於取用之明顯場所", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F01 { get; set; }

        [StringLength(1)]
        [Display(Name = "滅火器之安全插梢固定無脫落", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F02 { get; set; }

        [StringLength(1)]
        [Display(Name = "噴嘴無變形或破損(如：老化、龜裂)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F03 { get; set; }

        [StringLength(1)]
        [Display(Name = "壓力指示針在綠色範圍內", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F04 { get; set; }

        [StringLength(1)]
        [Display(Name = "滅火器前方無阻礙物(如：堆置雜物)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F05 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string F_Notes { get; set; }

























        [StringLength(1)]
        [Display(Name = "明顯處所標示「熄火加油」及「嚴禁煙火」標誌完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G01 { get; set; }

        [StringLength(1)]
        [Display(Name = "卸油區地面黃線標示清楚", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G02 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油碼頭標示「漁船加油時應將引擎熄火，且禁止使用明火」", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G03 { get; set; }

        [StringLength(1)]
        [Display(Name = "儲油區出入口標示「油槽區重地，非工作人員請勿進出」（地上型儲油槽）", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G04 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string G_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "收油口靜電接地電阻符合規定〔50Ω以下〕", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H01 { get; set; }

        [StringLength(10)]
        [Display(Name = "收油口靜電接地電阻符合規定（50Ω以下）實測", Order = 1)]
        public string H01_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "配電盤接地電阻符合規定 〔50Ω以下〕", Order = 1)]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H02 { get; set; }

        [StringLength(10)]
        [Display(Name = "配電盤接地電阻符合規定 〔50Ω以下〕實測", Order = 1)]
        public string H02_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "避雷針接地電阻符合規定〔10Ω以下〕", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H03 { get; set; }

        [StringLength(10)]
        [Display(Name = "避雷針接地電阻符合規定〔10Ω以下〕實測", Order = 1)]
        public string H03_Value { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string H_Notes { get; set; }







        [Display(Name = "營業站屋-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Count { get; set; }

        [Display(Name = "營業站屋-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Conform { get; set; }

        [Display(Name = "營業站屋-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Doesmeet { get; set; }

        [Display(Name = "營業站屋-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Unable { get; set; }

        [Display(Name = "電氣設備-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Count { get; set; }

        [Display(Name = "電氣設備-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Conform { get; set; }

        [Display(Name = "電氣設備-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Doesmeet { get; set; }

        [Display(Name = "電氣設備-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Unable { get; set; }

        [Display(Name = "儲油設備-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Count { get; set; }

        [Display(Name = "儲油設備-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Conform { get; set; }

        [Display(Name = "儲油設備-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Doesmeet { get; set; }

        [Display(Name = "儲油設備-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Unable { get; set; }

        [Display(Name = "加油設備-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Count { get; set; }

        [Display(Name = "加油設備-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Conform { get; set; }

        [Display(Name = "加油設備-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Doesmeet { get; set; }

        [Display(Name = "加油設備-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Unable { get; set; }

        [Display(Name = "流量計-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Count { get; set; }

        [Display(Name = "流量計-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Conform { get; set; }

        [Display(Name = "流量計-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Doesmeet { get; set; }

        [Display(Name = "流量計-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Unable { get; set; }

        [Display(Name = "消防設備-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Count { get; set; }

        [Display(Name = "消防設備-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Conform { get; set; }

        [Display(Name = "消防設備-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Doesmeet { get; set; }

        [Display(Name = "消防設備-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Unable { get; set; }

        [Display(Name = "標示設備-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Count { get; set; }

        [Display(Name = "標示設備-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Conform { get; set; }

        [Display(Name = "標示設備-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Doesmeet { get; set; }

        [Display(Name = "標示設備-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Unable { get; set; }

        [Display(Name = "靜電/漏電接地裝置-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Count { get; set; }

        [Display(Name = "靜電/漏電接地裝置-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Conform { get; set; }

        [Display(Name = "靜電/漏電接地裝置-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Doesmeet { get; set; }

        [Display(Name = "靜電/漏電接地裝置-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Unable { get; set; }

        [Display(Name = "合計-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllCount { get; set; }

        [Display(Name = "合計-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllConform { get; set; }

        [Display(Name = "合計-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllDoesmeet { get; set; }

        [Display(Name = "合計-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllUnable { get; set; }

    }
}
