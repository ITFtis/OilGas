namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Check_Counseling
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }

        [StringLength(10)]
        [Display(Name = "���ɽs��", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Counseling_No { get; set; }

        [StringLength(50)]
        [Display(Name = "�۪o�]�I����", Order = 1)]
        [Required]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"CarFuel_BasicData\":\"�T/�����[�o��\",\"CarGas_BasicData\":\"�T���[��\",\"FishGas_BasicData\":\"����[�o��\",\"SelfFuel_Basic\":\"�ۥΥ[�x�o\",\"SelfGas_Basic\":\"�ۥΥ[�x��\"}")]
        public string CaseType { get; set; }


        [Display(Name = "����", Order = 1)]
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


        [StringLength(70)]
        [Display(Name = "�۪o�]�I�W��", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Gas_Name { get; set; }

        [StringLength(50)]
        [Display(Name = "�o��]�I�s��", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string CaseNo { get; set; }


        [Display(Name = "���ɤ��", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public DateTime? Counseling_Date { get; set; }




        
        [StringLength(70)]
        [Display(Name = "��~�D��", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = false, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.CarVehicleGas_BusinessOrganization, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        public string Business_theme { get; set; }

        [StringLength(70)]
        [Display(Name = "��~�D��W��", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string Business_theme_Name { get; set; }

        [StringLength(200)]
        [Display(Name = "�o��]�I�a�}", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Addr { get; set; }

        [Display(Name = "�q�ܮɶ�", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public DateTime? Talk_time { get; set; }

        [StringLength(25)]
        [Display(Name = "���q�����H", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Officials { get; set; }

        [StringLength(25)]
        [Display(Name = "���豵�ܤH", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = true)]
        public string Record { get; set; }


        [StringLength(20)]
        [Display(Name = "�ǯu���X", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string FaxNo { get; set; }


        [StringLength(50)]
        [Display(Name = "�q�l�l��H�c", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string E_mail { get; set; }





        [Display(Name = "���ɤH���m�W", Order = 1)]
        [StringLength(25)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Counseling_Staff { get; set; }


        [Display(Name = "�۪o�]�I����H���m�W", Order = 1)]
        [StringLength(25)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_Staff { get; set; }

        [Display(Name = "�s���q��", Order = 1)]
        [StringLength(25)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_Tel { get; set; }







        [Display(Name = "���ɭ�](�z��)", Order = 1)]
        [Column(TypeName = "ntext")]
        [ColumnDef(Visible = false, VisibleEdit = true,EditType =EditType.Radio, SelectItems = "{\"�ìV������}\":\"�ìV������}\",\"�ìV��v���}\":\"�ìV��v���}\",\"�g�êk��7����5�������ﵽ\":\"�g�êk��7����5�������ﵽ\",\"�ìV���(A��)\":\"�ìV���(A��)\",\"�ìV���(B1��)\":\"�ìV���(B1��)\",\"�ìV���(B2��)\":\"�ìV���(B2��)\",\"�ìV���(C��)\":\"�ìV���(C��)\",\"�{���d��PID/FID>500ppmV��LEL>25%\":\"�{���d��PID/FID>500ppmV��LEL>25%\",\"���O�D�޾����d�Ҧ��g�[�Φa�U���ìV�ü{\":\"���O�D�޾����d�Ҧ��g�[�Φa�U���ìV�ü{\",\"��L\":\"��L\"}")]
        public string Notes { get; set; }


        [StringLength(500)]
        [Display(Name = "�{���[���", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType= EditType.TextArea)]
        public string LiveSituation { get; set; }

        [StringLength(500)]
        [Display(Name = "���D���R", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.TextArea)]
        public string ProblemAnalysis { get; set; }

        [StringLength(500)]
        [Display(Name = "���ɫ�ĳ", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.TextArea)]
        public string CounselingAdvice { get; set; }

        [Display(Name = "���ɳ��i�W��", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [NotMapped]
        public string File_Name { get; set; }





        [Display(Name = "�o����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public DateTime? Isseud_Date { get; set; }

        [Display(Name = "�o������", Order = 1)]
        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Isseud_Class { get; set; }


        [StringLength(30)]
        [Display(Name = "�o��r��", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Isseud_No { get; set; }

        [StringLength(100)]
        [Display(Name = "�o����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Isseud_Data { get; set; }

        [StringLength(20)]
        [Display(Name = "�o����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Isseud_Units { get; set; }

        [StringLength(30)]
        [Display(Name = "������", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "�ƥ����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Copy_Unit { get; set; }


































        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(100)]
        public string Achievement_Data { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(70)]
        public string Unit { get; set; }



        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(3)]
        public string AreaCode { get; set; }



    }
}
