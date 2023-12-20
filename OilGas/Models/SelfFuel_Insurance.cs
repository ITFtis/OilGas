namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;


    public partial class SelfFuel_Insurance
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


        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        [Display(Name = "保單號碼", Order = 6)]
        public string InsuranceNo { get; set; }



        [StringLength(2)]
        [ColumnDef(EditType = EditType.Select,
               SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
                SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_InsuranceCompanyName, OilGas",
                SelectSourceModelValueField = "Value",
                SelectSourceModelDisplayField = "Name")]
        [Display(Name = "保險公司名稱", Order = 2)]
        public string InsuranceCompanyName { get; set; }

        [StringLength(20)]
        [Display(Name = "保險公司名稱(其他)", Order = 3)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string OtherInsuranceCompanyName { get; set; }

        [Display(Name = "保單有效期限(開始)", Order = 4)]
        public DateTime? InsuranceValidateStartDate { get; set; }

        [Display(Name = "保單有效期限(結束)", Order = 5)]
        public DateTime? InsuranceValidateEndDate { get; set; }

        [StringLength(10)]
        [Display(Name = "保單類型", Order = 7)]
        // [ColumnDef(Visible = false, VisibleEdit = true)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"0\":\"公共意外責任保險\",\"1\":\"意外污染責任保險\"}")]
        public string InsuranceType { get; set; }



        [Display(Name = "保單類型", Order = 7)]
        [StringLength(100)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string Insurance_Type_text
        {
            get
            {
                string Insurance_Type_text = "";
                if (InsuranceType != null)
                {
                    var Insurance_Type_nomber = InsuranceType.Split(';');
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















        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string CreateUserTemp { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? IsConfirm { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
