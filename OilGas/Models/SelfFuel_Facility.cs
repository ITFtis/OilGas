namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;


    public partial class SelfFuel_Facility
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

        [StringLength(5)]
        [ColumnDef(EditType = EditType.Select, SelectItems = "{\"0\":\"�o��\",\"1\":\"�o�騮\"}")]
        [Display(Name = "�]�I����", Order = 2)]
        public string FacilityType { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Tank { get; set; }


        [Display(Name = "�o�����P��1", Order = 3)]
        [StringLength(20)]
        public string TankCar1 { get; set; }

        [Display(Name = "�o�����P��2", Order = 4)]
        [StringLength(20)]
        public string TankCar2 { get; set; }

        [Display(Name = "�o�����P��3", Order = 5)]
        [StringLength(20)]
        public string TankCar3 { get; set; }

        [Display(Name = "�o�����P��4", Order = 6)]
        [StringLength(20)]
        public string TankCar4 { get; set; }

        [Display(Name = "�o�����P��5", Order = 7)]
        [StringLength(20)]
        public string TankCar5 { get; set; }

        [Display(Name = " �[�o����:��j", Order =8)]
        public int? SinglePump { get; set; }

        [Display(Name = " �[�o����:���j", Order = 9)]
        public int? DualPump { get; set; }

        [Display(Name = " �[�o����:�|�j", Order = 10)]
        public int? FourPump { get; set; }

        [Display(Name = " �[�o����:���j", Order = 11)]
        public int? SixPump { get; set; }

        [Display(Name = " �[�o����:�K�j", Order = 12)]
        public int? EightPump { get; set; }

        [Display(Name = " �[�o����:�@�p", Order = 13)]
        public int? TotalPump { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(10)]
        public string CreateUserTemp { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? IsConfirm { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
