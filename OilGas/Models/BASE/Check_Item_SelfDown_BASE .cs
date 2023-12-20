namespace OilGas.Models.BASE
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Item_SelfDown_BASE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }

        [StringLength(10)]
        [Display(Name = "查核編號", Order = 1)]
        public string CheckNo { get; set; }


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


        [Display(Name = "查核時間", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? CheckDate { get; set; }

        [StringLength(50)]
        [Display(Name = "加油站編號", Order = 1)]
        public string CaseNo { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }












        [StringLength(1)]
        [Display(Name = "陰井之油氣濃度未逾30% LEL", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A01 { get; set; }

        [StringLength(1)]
        [Display(Name = "量油口蓋及墊圈完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A02 { get; set; }

        [StringLength(1)]
        [Display(Name = "陰井內收油管法蘭及接頭無滲漏", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A03 { get; set; }

        [StringLength(1)]
        [Display(Name = "通氣(排氣)管口安全網無銹破無阻塞", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A04 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string A_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "沉油泵與油管接頭無滲漏﹝沉油式﹞", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B01 { get; set; }

        [StringLength(1)]
        [Display(Name = "吸油管閘閥及接頭無滲漏﹝自吸式﹞", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B02 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油設備防爆配管良好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B03 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string B_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "手提乾粉滅火器檢查紀錄卡紀錄完整", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C01 { get; set; }

        [StringLength(1)]
        [Display(Name = "滅火器本體無損傷、銹蝕", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C02 { get; set; }

        [StringLength(1)]
        [Display(Name = "噴嘴口無堵塞", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C03 { get; set; }

        [StringLength(1)]
        [Display(Name = "噴嘴把手靈活(加壓式)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C04 { get; set; }

        [StringLength(1)]
        [Display(Name = "橡皮管無損傷、龜裂", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C05 { get; set; }

        [StringLength(1)]
        [Display(Name = "橡皮管接頭旋緊、無鏽蝕、束圈無龜裂", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C06 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string C_Notes { get; set; }

        [StringLength(1)]
        [Display(Name = "各電氣設備插頭、插座無損壞，線路無鬆裸露", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D01 { get; set; }

        [StringLength(1)]
        [Display(Name = "各電氣設備漏電斷路器試鈕作用正常", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D02 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string D_Notes { get; set; }



        [StringLength(1)]
        [Display(Name = "收油口靜電接地裝置完整", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H01 { get; set; }

        [StringLength(1)]
        [Display(Name = "電氣設備接地裝置完整、無脫落", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H02 { get; set; }

        [StringLength(1)]
        [Display(Name = "卸油口接地電阻符合規定〔50Ω以下〕", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H03 { get; set; }


        [StringLength(10)]
        [Display(Name = "卸油口接地電阻符合規定〔50Ω以下〕實測", Order = 1)]
        public string H03_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "加油機接地電阻符合規定〔25Ω以下〕", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H04 { get; set; }

        [StringLength(10)]
        [Display(Name = "加油機接地電阻符合規定〔25Ω以下〕實測", Order = 1)]
        public string H04_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "配電盤接地電阻符合規定〔50Ω以下〕", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H05 { get; set; }

        [StringLength(10)]
        [Display(Name = "配電盤接地電阻符合規定〔50Ω以下〕實測", Order = 1)]
        public string H05_Value { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string H_Notes { get; set; }







        [StringLength(1)]
        [Display(Name = "儲油槽區「嚴禁煙火」及加油區「熄火加油」、「嚴禁煙火」之警戒標誌完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G01 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string G_Notes { get; set; }


































        [Display(Name = "儲油設備-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Count { get; set; }

        [Display(Name = "儲油設備-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Conform { get; set; }

        [Display(Name = "儲油設備-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Doesmeet { get; set; }

        [Display(Name = "儲油設備-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Unable { get; set; }

        [Display(Name = "加油設備(加油機)-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Count { get; set; }

        [Display(Name = "加油設備(加油機)-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Conform { get; set; }

        [Display(Name = "加油設備(加油機)-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Doesmeet { get; set; }

        [Display(Name = "加油設備(加油機)-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Unable { get; set; }

        [Display(Name = "消防設備(滅火器)-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Count { get; set; }

        [Display(Name = "消防設備(滅火器)-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Conform { get; set; }

        [Display(Name = "消防設備(滅火器)-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Doesmeet { get; set; }

        [Display(Name = "消防設備(滅火器)-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Unable { get; set; }

        [Display(Name = "電氣設備-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Count { get; set; }

        [Display(Name = "電氣設備-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Conform { get; set; }

        [Display(Name = "電氣設備-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Doesmeet { get; set; }

        [Display(Name = "電氣設備-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Unable { get; set; }


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


        [Display(Name = "電氣設備-檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Count { get; set; }

        [Display(Name = "電氣設備-符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Conform { get; set; }

        [Display(Name = "電氣設備-不符合項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Doesmeet { get; set; }

        [Display(Name = "電氣設備-無法檢查項目", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Unable { get; set; }

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
