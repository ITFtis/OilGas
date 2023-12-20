namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Tank_well
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "�d�ֽs��", Order = 1)]
        public string CheckNo { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "�[�o���s��", Order = 1)]
        public string CaseNo { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }






        [StringLength(10)]
        [Display(Name = "�x�ѽs��", Order = 1)]
        public string TankNo { get; set; }

        [StringLength(10)]
        [ColumnDef(EditType = EditType.Select, SelectItems = "{\"1\":\"92\",\"2\":\"95\",\"3\":\"98\",\"4\":\"��o\",\"5\":\"�s��T�o\",\"6\":\"�Һغ���Ϊo\",\"7\":\"�A�غ���Ϊo\"}")]
        [Display(Name = "�o�~", Order = 1)]
        public string Oil { get; set; }

        [StringLength(10)]
        [ColumnDef(EditType = EditType.Select, SelectItems = "{\"1\":\"�J�o��\",\"2\":\"�X�o��\",\"3\":\"�H�u�qE11\",\"4\":\"�X/�J�o��\",\"5\":\"��L\"}")]
        [Display(Name = "������m", Order = 1)]
        public string Well_Place { get; set; }

        [StringLength(50)]
        [ColumnDef(EditType = EditType.Select, SelectItems = "{\"1\":\"���z��\",\"2\":\"PID/FID\"}")]
        [Display(Name = "�˴�����", Order = 1)]
        public string Testing_instruments { get; set; }

        [Display(Name = "�˴���(%LEL)", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public decimal? Detection { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "�˴���PID(PPMV)", Order = 1)]
        public decimal? PID { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "�˴���FID(PPMV)", Order = 1)]
        public decimal? FID { get; set; }




        [Display(Name = "�˴���", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string CITY
        {
            get
            {

                if (Testing_instruments == "1")
                {
                    return Detection+"%LEL";
                }
                else
                {
                    return PID+"PID(PPMN)/"+ FID+"FID(PPMV)";
                }

               
            }
            set
            {
            }
        }






        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 1)]
        public string Notes { get; set; }



        [StringLength(1)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string SlotNo { get; set; }


    }
}
