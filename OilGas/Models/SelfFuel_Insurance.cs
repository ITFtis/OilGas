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
        [Display(Name = "�ץ�s��", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }


        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        [Display(Name = "�O�渹�X", Order = 6)]
        public string InsuranceNo { get; set; }



        [StringLength(2)]
        [ColumnDef(EditType = EditType.Select,
               SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
                SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_InsuranceCompanyName, OilGas",
                SelectSourceModelValueField = "Value",
                SelectSourceModelDisplayField = "Name")]
        [Display(Name = "�O�I���q�W��", Order = 2)]
        public string InsuranceCompanyName { get; set; }

        [StringLength(20)]
        [Display(Name = "�O�I���q�W��(��L)", Order = 3)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string OtherInsuranceCompanyName { get; set; }

        [Display(Name = "�O�榳�Ĵ���(�}�l)", Order = 4)]
        public DateTime? InsuranceValidateStartDate { get; set; }

        [Display(Name = "�O�榳�Ĵ���(����)", Order = 5)]
        public DateTime? InsuranceValidateEndDate { get; set; }

        [StringLength(10)]
        [Display(Name = "�O������", Order = 7)]
        // [ColumnDef(Visible = false, VisibleEdit = true)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"0\":\"���@�N�~�d���O�I\",\"1\":\"�N�~�ìV�d���O�I\"}")]
        public string InsuranceType { get; set; }



        [Display(Name = "�O������", Order = 7)]
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
                                text = "�A���@�N�~�d���O�I";
                                break;
                            case "1":

                                text = "�A�N�~�ìV�d���O�I";
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
