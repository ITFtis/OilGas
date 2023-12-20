namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FishGas_Dispatch
    {
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public long ID { get; set; }

        [StringLength(10)]
        [Display(Name = "�ץ�s��", Order = 1)]
        [ColumnDef(VisibleEdit = false)]
        public string CaseNo { get; set; }

        [Display(Name = "�o����", Order = 1)]
        public DateTime? Dispatch_date { get; set; }

        [Display(Name = "�o������", Order = 1)]
        [ColumnDef(Sortable = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.DispatchClassCode, OilGas",
          SelectSourceModelValueField = "Value",
          SelectSourceModelDisplayField = "Name")]
        [StringLength(50)]
        public string DispatchClass { get; set; }

        [Display(Name = "�o��r��", Order = 1)]//�o��r��(���)
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(20)]
        public string License_No { get; set; }

        [Display(Name = " ", Order = 1)]//�o��r��(�s��)
        [StringLength(30)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Dispatch_No { get; set; }


        [Display(Name = "�o��r��", Order = 1)]
        [StringLength(50)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string License_Dispatch_No
        {
            get
            {
                if (License_No + Dispatch_No == "")
                {
                    return "-";
                }
                return License_No + Dispatch_No + "��";
            }
            set
            {
            }
        }



        [Display(Name = "�o����", Order = 1)]
        [StringLength(30)]
        public string File_name { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(30)]
        public string DispatchUnit { get; set; }

        [Display(Name = "����̳��", Order = 1)]
        [StringLength(30)]
        public string Shouwen_Units { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"�O�_���F�����ҫO�@��\":\"�O�_���F�����ҫO�@��\",\"�O�����F�����ҫO�@��\":\"�O�����F�����ҫO�@��\",\"�򶩥��F�����ҫO�@��\":\"�򶩥��F�����ҫO�@��\",\"�O�n���F�����ҫO�@��\":\"�O�n���F�����ҫO�@��\",\"�������F�����ҫO�@��\":\"�������F�����ҫO�@��\",\"�s�_���F�����ҫO�@��\":\"�s�_���F�����ҫO�@��\",\"�y�����F�����ҫO�@��\":\"�y�����F�����ҫO�@��\",\"��鿤�F�����ҫO�@��\":\"��鿤�F�����ҫO�@��\",\"�Ÿq���F�����ҫO�@��\":\"�Ÿq���F�����ҫO�@��\",\"�s�˿��F�����ҫO�@��\":\"�s�˿��F�����ҫO�@��\",\"�]�߿��F�����ҫO�@��\":\"�]�߿��F�����ҫO�@��\",\"�n�뿤�F�����ҫO�@��\":\"�n�뿤�F�����ҫO�@��\",\"���ƿ��F�����ҫO�@��\":\"���ƿ��F�����ҫO�@��\",\"�s�˥��F�����ҫO�@��\":\"�s�˥��F�����ҫO�@��\",\"���L���F�����ҫO�@��\":\"���L���F�����ҫO�@��\",\"�Ÿq���F�����ҫO�@��\":\"�Ÿq���F�����ҫO�@��\",\"�̪F���F�����ҫO�@��\":\"�̪F���F�����ҫO�@��\",\"�Ὤ���F�����ҫO�@��\":\"�Ὤ���F�����ҫO�@��\",\"�O�F���F�����ҫO�@��\":\"�O�F���F�����ҫO�@��\",\"�������F�����ҫO�@��\":\"�������F�����ҫO�@��\",\"��򿤬F�����ҫO�@��\":\"��򿤬F�����ҫO�@��\",\"�s�����F�����ҫO�@��\":\"�s�����F�����ҫO�@��\",\"�g�ٳ��෽��\":\"�g�ٳ��෽��\"}")]
        [Display(Name = "�ƥ����", Order = 1)]
        [StringLength(500)]
        public string CopyUnit { get; set; }

        [Display(Name = "��L�ƥ����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(250)]
        public string otherCopyUnit { get; set; }

        [Display(Name = "�Ƶ�", Order = 1)]
        [StringLength(200)]
        public string Note { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(52)]
        public string MemberID { get; set; }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Change { get; set; }
    }
}
