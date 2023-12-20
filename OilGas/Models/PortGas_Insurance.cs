namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class PortGas_Insurance
    {
        [Key]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public long ID { get; set; }

        [StringLength(10)]
        [Display(Name = "案件編號", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }

        [StringLength(30)]
        [ColumnDef(EditType = EditType.Select,
          SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
           SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_InsuranceCompanyName, OilGas",
           SelectSourceModelValueField = "Value",
           SelectSourceModelDisplayField = "Name")]
        [Display(Name = "保險公司名稱", Order = 2)]
        public string Insurance_Company { get; set; }

        [StringLength(30)]
        [Display(Name = "保險公司名稱(其他)", Order = 3)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Insurance_otherCompany { get; set; }

        [Display(Name = "保單有效期限(開始)", Order = 4)]
        public DateTime? Insurance_policy_start { get; set; }

        [Display(Name = "保單有效期限(結束)", Order = 5)]
        public DateTime? Insurance_policy_end { get; set; }

        [StringLength(30)]
        [Display(Name = "保單號碼", Order = 6)]
        public string Insurance_No { get; set; }

        [StringLength(5)]
        [Display(Name = "保單類型", Order = 7)]
        // [ColumnDef(Visible = false, VisibleEdit = true)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"0\":\"公共意外責任保險\",\"1\":\"意外污染責任保險\"}")]
        public string Insurance_Type { get; set; }



        [Display(Name = "保單類型", Order = 7)]
        [StringLength(100)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string Insurance_Type_text
        {
            get
            {
                string Insurance_Type_text = "";
                if (Insurance_Type != null)
                {
                    var Insurance_Type_nomber = Insurance_Type.Split(';');
                    foreach (var i in Insurance_Type_nomber)
                    {
                        var text = "";
                        switch (i)
                        {
                            case "0":
                                text = "，公共意外責任保險";
                                break;
                            case "1":

                                text = "，意外污染責任保險";
                                break;

                        }

                        Insurance_Type_text = Insurance_Type_text + text;
                    }
                }
                if (Insurance_Type_text.Length > 0)
                {
                    return Insurance_Type_text.Substring(1);
                }
                else
                {
                    return Insurance_Type_text;
                }

            }
            set
            {
            }
        }









        [StringLength(52)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string MemberID { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
