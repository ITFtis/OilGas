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
        [Display(Name = "�ץ�s��", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }

        [StringLength(30)]
        [ColumnDef(EditType = EditType.Select,
          SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
           SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_InsuranceCompanyName, OilGas",
           SelectSourceModelValueField = "Value",
           SelectSourceModelDisplayField = "Name")]
        [Display(Name = "�O�I���q�W��", Order = 2)]
        public string Insurance_Company { get; set; }

        [StringLength(30)]
        [Display(Name = "�O�I���q�W��(��L)", Order = 3)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Insurance_otherCompany { get; set; }

        [Display(Name = "�O�榳�Ĵ���(�}�l)", Order = 4)]
        public DateTime? Insurance_policy_start { get; set; }

        [Display(Name = "�O�榳�Ĵ���(����)", Order = 5)]
        public DateTime? Insurance_policy_end { get; set; }

        [StringLength(30)]
        [Display(Name = "�O�渹�X", Order = 6)]
        public string Insurance_No { get; set; }

        [StringLength(5)]
        [Display(Name = "�O������", Order = 7)]
        // [ColumnDef(Visible = false, VisibleEdit = true)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"0\":\"���@�N�~�d���O�I\",\"1\":\"�N�~�ìV�d���O�I\"}")]
        public string Insurance_Type { get; set; }



        [Display(Name = "�O������", Order = 7)]
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









        [StringLength(52)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string MemberID { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}