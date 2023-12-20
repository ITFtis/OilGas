namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfFuel_Dispatch
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

        [Display(Name = "�o����", Order = 1)]
        public DateTime? DispatchDate { get; set; }

        [Display(Name = "�o������", Order = 1)]
        [ColumnDef(Sortable = true, EditType = EditType.Select,
 SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
  SelectSourceModelNamespace = "OilGas.Models.DispatchClassCode, OilGas",
  SelectSourceModelValueField = "Value",
  SelectSourceModelDisplayField = "Name")]
        [StringLength(2)]
        public string DispatchClass { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "��L�o������", Order = 1)]
        [StringLength(20)]
        public string OtherDispatchClass { get; set; }

        [Display(Name = "�o��r��", Order = 1)]//�o��r��(���)
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(20)]
        public string License_No { get; set; }


        [Display(Name = " ", Order = 1)]//�o��r��(�s��)
        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string DispatchNo { get; set; }



        [Display(Name = "�o��r��", Order = 1)]
        [StringLength(50)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string License_Dispatch_No
        {
            get
            {
                if (License_No + DispatchNo == "")
                {
                    return "-";
                }
                return License_No + DispatchNo + "��";
            }
            set
            {
            }
        }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(50)]
        public string FileOriginalName { get; set; }



        [Display(Name = "�o����", Order = 1)]
        [StringLength(50)]
        public string FileNewName { get; set; }






        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? FileSize { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? FileUpLoadDate { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(20)]
        public string DispatchUnit { get; set; }

        [Display(Name = "����̳��", Order = 1)]
        [StringLength(20)]
        public string ReceiveUnit { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"�O�_���F�����ҫO�@��\":\"�O�_���F�����ҫO�@��\",\"�O�����F�����ҫO�@��\":\"�O�����F�����ҫO�@��\",\"�򶩥��F�����ҫO�@��\":\"�򶩥��F�����ҫO�@��\",\"�O�n���F�����ҫO�@��\":\"�O�n���F�����ҫO�@��\",\"�������F�����ҫO�@��\":\"�������F�����ҫO�@��\",\"�s�_���F�����ҫO�@��\":\"�s�_���F�����ҫO�@��\",\"�y�����F�����ҫO�@��\":\"�y�����F�����ҫO�@��\",\"��鿤�F�����ҫO�@��\":\"��鿤�F�����ҫO�@��\",\"�Ÿq���F�����ҫO�@��\":\"�Ÿq���F�����ҫO�@��\",\"�s�˿��F�����ҫO�@��\":\"�s�˿��F�����ҫO�@��\",\"�]�߿��F�����ҫO�@��\":\"�]�߿��F�����ҫO�@��\",\"�n�뿤�F�����ҫO�@��\":\"�n�뿤�F�����ҫO�@��\",\"���ƿ��F�����ҫO�@��\":\"���ƿ��F�����ҫO�@��\",\"�s�˥��F�����ҫO�@��\":\"�s�˥��F�����ҫO�@��\",\"���L���F�����ҫO�@��\":\"���L���F�����ҫO�@��\",\"�Ÿq���F�����ҫO�@��\":\"�Ÿq���F�����ҫO�@��\",\"�̪F���F�����ҫO�@��\":\"�̪F���F�����ҫO�@��\",\"�Ὤ���F�����ҫO�@��\":\"�Ὤ���F�����ҫO�@��\",\"�O�F���F�����ҫO�@��\":\"�O�F���F�����ҫO�@��\",\"�������F�����ҫO�@��\":\"�������F�����ҫO�@��\",\"��򿤬F�����ҫO�@��\":\"��򿤬F�����ҫO�@��\",\"�s�����F�����ҫO�@��\":\"�s�����F�����ҫO�@��\",\"�g�ٳ��෽��\":\"�g�ٳ��෽��\"}")]
        [Display(Name = "�ƥ����", Order = 1)]
        [StringLength(500)]
        public string CopyUnit { get; set; }

        [Display(Name = "��L�ƥ����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(250)]
        public string OtherCopyUnit { get; set; }

        [Display(Name = "�Ƶ�", Order = 1)]
        [StringLength(500)]
        public string DispatchNote { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string CreateUserTemp { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public bool? IsConfirm { get; set; }



        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
