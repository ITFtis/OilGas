namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Item_97
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

        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string I00 { get; set; }

        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J00 { get; set; }

        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string K00 { get; set; }

        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string L00 { get; set; }









        [StringLength(1)]
        [Display(Name = "營業室無儲存輕質之揮發性小包裝油品", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A01 { get; set; }

        [StringLength(1)]
        [Display(Name = "站屋、油泵島之地面無油漬", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A02 { get; set; }

        [StringLength(1)]
        [Display(Name = "排水溝無浮油/油漬", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A03 { get; set; }

        [StringLength(1)]
        [Display(Name = "油品儲藏室無其他非油品類可燃物", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A04 { get; set; }

        [StringLength(1)]
        [Display(Name = "油品儲藏室防火門無變形、損壞", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A05 { get; set; }

        [StringLength(1)]
        [Display(Name = "地坪(路面)完好無嚴重龜裂、坑洞", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string A06 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string A07 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string A08 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string A09 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string A10 { get; set; }


        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string A_Notes { get; set; }












        [StringLength(1)]
        [Display(Name = "配電盤操作面板前無堆置雜物且開關箱關閉", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B01 { get; set; }

        [StringLength(1)]
        [Display(Name = "配電盤開關接線端子接觸良好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B02 { get; set; }

        [StringLength(1)]
        [Display(Name = "配電盤各分路絕緣電阻數值符合規定", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B03 { get; set; }

        [StringLength(1)]
        [Display(Name = "無非防爆性電氣設備或器具置於第一種場所或第二種場所範圍內使用", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B04 { get; set; }

        [StringLength(1)]
        [Display(Name = "電氣設備插頭、插座無損壞，線路無鬆散裸露", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B05 { get; set; }

        [StringLength(1)]
        [Display(Name = "電氣設備漏電斷路器試鈕作用正常", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B06 { get; set; }

        [StringLength(1)]
        [Display(Name = "電氣設備接地裝置無脫落", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B07 { get; set; }

        [StringLength(1)]
        [Display(Name = "避雷設備接地裝置無脫落", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B08 { get; set; }

        [StringLength(1)]
        [Display(Name = "緊急照明燈功能正常", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B09 { get; set; }

        [StringLength(1)]
        [Display(Name = "油品儲藏室防爆開關及防爆燈具完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string B10 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string B_Notes { get; set; }













        [StringLength(1)]
        [Display(Name = "卸油口蓋上鎖", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C01 { get; set; }

        [StringLength(1)]
        [Display(Name = "卸油口蓋及墊圈完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C02 { get; set; }

        [StringLength(1)]
        [Display(Name = "卸油口油品類別標識正確完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C03 { get; set; }

        [StringLength(1)]
        [Display(Name = "卸油口盛油盤內無雜物及卸水口關閉", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C04 { get; set; }

        [StringLength(1)]
        [Display(Name = "卸油口靜電接地棒無毀損、鍍銅無斷裂", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C05 { get; set; }

        [StringLength(1)]
        [Display(Name = "儲油槽陰井蓋板完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C06 { get; set; }

        [StringLength(1)]
        [Display(Name = "儲油槽陰井內無積水或積油", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C07 { get; set; }

        [StringLength(1)]
        [Display(Name = "陰井內部及壁板完整", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C08 { get; set; }

        [StringLength(1)]
        [Display(Name = "陰井蓋板油品類別標識正確完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C09 { get; set; }

        [StringLength(1)]
        [Display(Name = "手動量油口蓋墊圈完好、蓋妥(或蓋妥後上鎖)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C10 { get; set; }

        [StringLength(1)]
        [Display(Name = "陰井內油管法蘭及接頭無滲漏", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C11 { get; set; }

        [StringLength(1)]
        [Display(Name = "陰井之油氣濃度未逾25%LEL", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C12 { get; set; }

        [StringLength(1)]
        [Display(Name = "沉油泵馬達等接線盒內無積水、積油", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C13 { get; set; }

        [StringLength(1)]
        [Display(Name = "排氣(通氣)管防雨蓋完整", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string C14 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string C_Notes { get; set; }








        [StringLength(1)]
        [Display(Name = "加油機底座穩固", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D01 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油機底座四周無漏油跡象", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D02 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油槍及橡皮管無滲漏", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D03 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油槍之加滿自動跳停裝置功能良好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D04 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油橡皮管吊管回收裝置功能良好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D05 { get; set; }

        [StringLength(1)]
        [Display(Name = "運轉無異狀", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D06 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油機內無積水、無積油及無油氣，設備接頭無滲漏", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D07 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油機接地裝置無脫落", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D08 { get; set; }

        [StringLength(1)]
        [Display(Name = "傳動皮帶鬆緊度適當(自吸式)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D09 { get; set; }

        [StringLength(1)]
        [Display(Name = "加油機及電氣接線孔配電管接頭、防爆軟管接頭無鬆脫，預留管口密封", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D10 { get; set; }

        [StringLength(1)]
        [Display(Name = "沉油泵加油機緊急遮斷閥位置適當、固定", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string D11 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string D_Notes { get; set; }










        [StringLength(1)]
        [Display(Name = "第一階段油氣回收口快速接頭蓋上鎖", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string E01 { get; set; }

        [StringLength(1)]
        [Display(Name = "第一階段油氣回收口快速接頭護蓋墊圈及操作良好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string E02 { get; set; }

        [StringLength(1)]
        [Display(Name = "油氣回收槍及同軸及管無滲漏", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string E03 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string E_Notes { get; set; }







        [StringLength(1)]
        [Display(Name = "放置定位", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F01 { get; set; }

        [StringLength(1)]
        [Display(Name = "消防設備前無堆置雜物", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F02 { get; set; }

        [StringLength(1)]
        [Display(Name = "滅火器及藥劑未逾有效期限", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F03 { get; set; }

        [StringLength(1)]
        [Display(Name = "滅火器本體無損傷、銹蝕", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F04 { get; set; }

        [StringLength(1)]
        [Display(Name = "噴嘴口無異物堵塞", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F05 { get; set; }

        [StringLength(1)]
        [Display(Name = "噴嘴把手靈活(加壓式滅火器)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F06 { get; set; }

        [StringLength(1)]
        [Display(Name = "橡皮管無損傷，接頭旋緊、無銹蝕/裂痕", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F07 { get; set; }

        [StringLength(1)]
        [Display(Name = "封條完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F08 { get; set; }

        [StringLength(1)]
        [Display(Name = "蓄壓式滅火器壓力指示正常(指針在錄色範圍)", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string F09 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 1)]
        public string F_Notes { get; set; }













        [StringLength(1)]
        [Display(Name = "「熄火加油」及「嚴禁煙火」標誌完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G01 { get; set; }

        [StringLength(1)]
        [Display(Name = "儲油槽之卸油區地面黃線標示清楚", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G02 { get; set; }

        [StringLength(1)]
        [Display(Name = "車輛出入口標誌及標線完好", Order = 1)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string G03 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "兩棚高度標誌完好", Order = 1)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string G04 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "燈柱(框架)及基座完好、無銹損", Order = 1)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string G05 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "接線蓋板完好", Order = 1)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string G06 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 3)]
        public string G_Notes { get; set; }









        [StringLength(1)]
        [Display(Name = "收油口靜電接地電阻符合規定（50Ω以下）", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H01 { get; set; }

        [StringLength(10)]
        [Display(Name = "收油口靜電接地電阻符合規定（50Ω以下）實測", Order = 3)]
        public string H01_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "加油機接地電阻符合規定（25Ω以下）", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H02 { get; set; }


        [StringLength(10)]
        [Display(Name = "加油機接地電阻符合規定（25Ω以下）實測", Order = 3)]
        public string H02_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "配電盤接地電阻符合規定（50Ω以下）", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H03 { get; set; }

        [StringLength(10)]
        [Display(Name = "配電盤接地電阻符合規定（50Ω以下）實測", Order = 3)]
        public string H03_Value { get; set; }

        [StringLength(1)]
        [Display(Name = "避雷針靜電接地電阻符合規定（10Ω以下）", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string H04 { get; set; }


        [StringLength(10)]
        [Display(Name = "避雷針靜電接地電阻符合規定（10Ω以下）實測", Order = 3)]
        public string H04_Value { get; set; }

        //[StringLength(1)]
        //[Display(Name = "空氣壓縮機接地電阻符合規定（50Ω以下）", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string H05 { get; set; }

        //[StringLength(10)]
        //[Display(Name = "空氣壓縮機接地電阻符合規定（50Ω以下）實測", Order = 3)]
        //public string H05_Value { get; set; }


        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 3)]
        public string H_Notes { get; set; }













        [StringLength(1)]
        [Display(Name = "運轉無異狀", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string I01 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "接頭無漏氣", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string I02 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "壓力計壓力指示正常", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string I03 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "曲軸箱之機油油位適當", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string I04 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "空壓機接地裝置無脫落", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string I05 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "螺栓及管路接頭無鬆動", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string I06 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "皮帶鬆緊度適當、無龜裂", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string I07 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "壓力洩放並啟動後制壓閥作動正常", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string I08 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "安全閥之作用正常", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string I09 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "防護網完好", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string I10 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 3)]
        public string I_Notes { get; set; }









        [StringLength(1)]
        [Display(Name = "運轉無異狀", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string J01 { get; set; }

        [StringLength(1)]
        [Display(Name = "漏電斷路器測試正常", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string J02 { get; set; }

        [StringLength(1)]
        [Display(Name = "緊急停止裝置測試正常", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string J03 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J04 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J05 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J06 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J07 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J08 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J09 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J10 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string J11 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 3)]
        public string J_Notes { get; set; }









        [StringLength(1)]
        [Display(Name = "冷卻水、潤滑油及電瓶液位正常", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string K01 { get; set; }

        [StringLength(1)]
        [Display(Name = "燃油量充足", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string K02 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string K03 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string K04 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 3)]
        public string K_Notes { get; set; }






        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string M02 { get; set; }



        [StringLength(1)]
        [Display(Name = "每日盤查儲油槽油品存量，並作成紀錄", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string L01 { get; set; }

        [StringLength(1)]
        [Display(Name = "每月盤查儲油槽存量及檢測漏管油氣濃度並紀錄", Order = 3)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string L02 { get; set; }

        //[StringLength(1)]
        //[Display(Name = "每年應依消防法規定由領有證照之專業人員執行消防檢查，並檢附檢查報告，加油站自行安全檢查負責人應檢查相關紀錄與報告是否保存完好。", Order = 3)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string L03 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 3)]
        public string L_Notes { get; set; }




        //[StringLength(1)]
        //[Display(Name = "「加油站設置管理規則」第29條:「經營加油站業務者，應依加油站營運設備目行安全檢查表自行實施加油站設施每日、每月及每半年檢查紀錄，應與實際相符，請且應保存一年以上。」", Order = 1)]
        //[Required]
        //[ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        //public string M01 { get; set; }

        //[Column(TypeName = "ntext")]
        //[Display(Name = "備註", Order = 1)]
        //public string M_Notes { get; set; }



        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string F10 { get; set; }





        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }









        [Display(Name = "營業站屋-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Count { get; set; }

        [Display(Name = "營業站屋-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Conform { get; set; }

        [Display(Name = "營業站屋-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Doesmeet { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? A_Unable { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? I_Unable { get; set; }


        [Display(Name = "電氣設備-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Count { get; set; }

        [Display(Name = "電氣設備-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Conform { get; set; }

        [Display(Name = "電氣設備-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Doesmeet { get; set; }

        [Display(Name = "儲油設備-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Count { get; set; }

        [Display(Name = "儲油設備-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Conform { get; set; }

        [Display(Name = "儲油設備-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Doesmeet { get; set; }

        [Display(Name = "儲油設備-無法檢查", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? C_Unable { get; set; }

        [Display(Name = "加油設備(加油機)-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Count { get; set; }

        [Display(Name = "加油設備(加油機)-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Conform { get; set; }

        [Display(Name = "加油設備(加油機)-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? D_Doesmeet { get; set; }

        [Display(Name = "油氣回收設備-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Count { get; set; }

        [Display(Name = "油氣回收設備-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Conform { get; set; }

        [Display(Name = "油氣回收設備-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? E_Doesmeet { get; set; }

        [Display(Name = "消防設備(滅火器)-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Count { get; set; }

        [Display(Name = "消防設備(滅火器)-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Conform { get; set; }

        [Display(Name = "消防設備(滅火器)-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? F_Doesmeet { get; set; }


















        [Display(Name = "標示設備-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Count { get; set; }

        [Display(Name = "標示設備-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Conform { get; set; }

        [Display(Name = "標示設備-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? G_Doesmeet { get; set; }

        [Display(Name = "靜電(電氣)接地裝置-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Count { get; set; }

        [Display(Name = "靜電(電氣)接地裝置-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Conform { get; set; }

        [Display(Name = "靜電(電氣)接地裝置-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Doesmeet { get; set; }

        [Display(Name = "靜電(電氣)接地裝置-無法檢查", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? H_Unable { get; set; }

        [Display(Name = "空氣壓縮機-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? I_Count { get; set; }

        [Display(Name = "空氣壓縮機-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? I_Conform { get; set; }

        [Display(Name = "空氣壓縮機-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? I_Doesmeet { get; set; }

        [Display(Name = "洗車機-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? J_Count { get; set; }

        [Display(Name = "洗車機-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? J_Conform { get; set; }

        [Display(Name = "洗車機-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? J_Doesmeet { get; set; }

        [Display(Name = "發電機-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? K_Count { get; set; }

        [Display(Name = "發電機-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? K_Conform { get; set; }

        [Display(Name = "發電機-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? K_Doesmeet { get; set; }

        [Display(Name = "特別規定-未設置", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? L_Count { get; set; }

        [Display(Name = "特別規定-符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? L_Conform { get; set; }

        [Display(Name = "特別規定-不符合", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? L_Doesmeet { get; set; }















        //[Display(Name = "查核編號", Order = 1)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public int? M_Count { get; set; }

        //[Display(Name = "查核編號", Order = 1)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public int? M_Conform { get; set; }

        //[Display(Name = "查核編號", Order = 1)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public int? M_Doesmeet { get; set; }

        [Display(Name = "全部-未設置", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllCount { get; set; }

        [Display(Name = "全部-符合", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllConform { get; set; }

        [Display(Name = "全部-不符合", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllDoesmeet { get; set; }

        [Display(Name = "全部-無法檢查", Order = 5)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? AllUnable { get; set; }



























        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? B_Unable { get; set; }




        //[StringLength(10)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public string D08_Value { get; set; }

        //[StringLength(10)]
        //[ColumnDef(Visible = false, VisibleEdit = false)]
        //public string I05_Value { get; set; }













        [Display(Name = "兩棚高度標誌完好", Order = 2)]        
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public int? G04 { get; set; }


        [Display(Name = "燈柱(框架)及基座完好、無銹損", Order = 2)]        
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public int? G05 { get; set; }


        [Display(Name = "接線蓋板完好", Order = 2)]        
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public int? G06 { get; set; }


        [StringLength(1)]
        [Display(Name = "「加油站設置管理規則」第29條:「經營加油站業務者，應依加油站營運設備目行安全檢查表自行實施加油站設施每日、每月及每半年檢查紀錄，應與實際相符，請且應保存一年以上。」", Order = 4)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"未設置\",\"1\":\"良好\",\"2\":\"不良好\",\"3\":\"無法檢查\"}")]
        public string M01 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "備註", Order = 4)]
        public string M_Notes { get; set; }













        [Display(Name = "查核編號", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Count { get; set; }

        [Display(Name = "查核編號", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Conform { get; set; }

        [Display(Name = "查核編號", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Doesmeet { get; set; }



    }
}
