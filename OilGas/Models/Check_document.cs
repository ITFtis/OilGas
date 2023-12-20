namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Check_document
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int id { get; set; }


        [StringLength(10)]
        [Display(Name = "�d�ֽs��", Order = 1)]
        [ColumnDef(Filter =true)]
        public string CheckNo { get; set; }

        [Display(Name = "�d�֤��", Order = 1)]
        public DateTime? CheckDate { get; set; }

        [Display(Name = "�Ƭd���", Order = 1)]
        public DateTime? CheckDate_Action { get; set; }

        [StringLength(70)]
        [Display(Name = "�۪o�]�I�W��", Order = 1)]
        [ColumnDef(Filter = true)]
        public string Gas_Name { get; set; }

        [StringLength(70)]
        [Display(Name = "��~�D��", Order = 1)]
        public string Business_theme { get; set; }

        [StringLength(200)]
        [Display(Name = "�۪o�]�I�a�}", Order = 1)]
        public string Addr { get; set; }

        [Display(Name = "�۪o�]�I�d�ֳ��i���", Order = 1)]
        public DateTime? Gas_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "�۪o�]�I�d�ֳ��i", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_File { get; set; }



        [StringLength(30)]
        [Display(Name = "�d�ֳ��i�o��r��", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_Isseud_No { get; set; }

        [Display(Name = "�d�ֳ��i�o����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        [StringLength(20)]
        public string Gas_Isseud_Units { get; set; }

        [StringLength(70)]
        [Display(Name = "�d�ֳ��i������", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"�O�_���F��-���~�o�i��\":\"�O�_���F��-���~�o�i��\",\"�s�_���F��-�g�ٵo�i��\":\"�s�_���F��-�g�ٵo�i��\",\"�򶩥��F��-�u�ȳB\":\"�򶩥��F��-�u�ȳB\",\"��饫�F��-�g�ٵo�i��\":\"��饫�F��-�g�ٵo�i��\",\"�s�˥��F��-���~�o�i�B\":\"�s�˥��F��-���~�o�i�B\",\"�s�˿��F��-��ڲ��~�o�i�B\":\"�s�˿��F��-��ڲ��~�o�i�B\",\"�y�����F��-�u�ӮȹC�B\":\"�y�����F��-�u�ӮȹC�B\",\"�Ὤ���F��-�[���B\":\"�Ὤ���F��-�[���B\",\"�s�����F��-�س]��\":\"�s�����F��-�س]��\",\"�O�����F��-�g�ٵo�i��\":\"�O�����F��-�g�ٵo�i��\",\"�]�߿��F��-�u�ӵo�i�B\":\"�]�߿��F��-�u�ӵo�i�B\",\"�n�뿤�F��-�س]�B\":\"�n�뿤�F��-�س]�B\",\"���ƿ��F��-�س]�B\":\"���ƿ��F��-�س]�B\",\"���L���F��-�س]�B\":\"���L���F��-�س]�B\",\"�O�n���F��-�g�ٵo�i��\":\"�O�n���F��-�g�ٵo�i��\",\"�������F��-�g�ٵo�i��\":\"�������F��-�g�ٵo�i��\",\"�Ÿq���F��-�س]�B\":\"�Ÿq���F��-�س]�B\",\"�Ÿq���F��-�g�ٵo�i�B\":\"�Ÿq���F��-�g�ٵo�i�B\",\"�̪F���F��-���m�o�i�B\":\"�̪F���F��-���m�o�i�B\",\"�O�F���F��-�]�F�P�g�ٵo�i�B\":\"�O�F���F��-�]�F�P�g�ٵo�i�B\",\"�������F��-�س]��\":\"�������F��-�س]��\",\"��򿤬F��-�س]�B\":\"��򿤬F��-�س]�B\"}")]
        public string Gas_Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "�d�ֳ��i�ƥ����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas_Copy_Unit { get; set; }





        [Display(Name = "�a��F���˰e�ˬd���i���", Order = 1)]
        public DateTime? EPB_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "�a��F���˰e�ˬd���i", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB_File { get; set; }

        [Display(Name = "�۪o�]�I�Ƭd���i���", Order = 1)]
        public DateTime? GAS2_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "�۪o�]�I�Ƭd���i", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string GAS2_File { get; set; }

        [StringLength(30)]
        [Display(Name = "�Ƭd���i�o��r��", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas2_Isseud_No { get; set; }

        [StringLength(20)]
        [Display(Name = "�Ƭd���i�o����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas2_Isseud_Units { get; set; }

        [StringLength(70)]
        [Display(Name = "�Ƭd���i������", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"�O�_���F��-���~�o�i��\":\"�O�_���F��-���~�o�i��\",\"�s�_���F��-�g�ٵo�i��\":\"�s�_���F��-�g�ٵo�i��\",\"�򶩥��F��-�u�ȳB\":\"�򶩥��F��-�u�ȳB\",\"��饫�F��-�g�ٵo�i��\":\"��饫�F��-�g�ٵo�i��\",\"�s�˥��F��-���~�o�i�B\":\"�s�˥��F��-���~�o�i�B\",\"�s�˿��F��-��ڲ��~�o�i�B\":\"�s�˿��F��-��ڲ��~�o�i�B\",\"�y�����F��-�u�ӮȹC�B\":\"�y�����F��-�u�ӮȹC�B\",\"�Ὤ���F��-�[���B\":\"�Ὤ���F��-�[���B\",\"�s�����F��-�س]��\":\"�s�����F��-�س]��\",\"�O�����F��-�g�ٵo�i��\":\"�O�����F��-�g�ٵo�i��\",\"�]�߿��F��-�u�ӵo�i�B\":\"�]�߿��F��-�u�ӵo�i�B\",\"�n�뿤�F��-�س]�B\":\"�n�뿤�F��-�س]�B\",\"���ƿ��F��-�س]�B\":\"���ƿ��F��-�س]�B\",\"���L���F��-�س]�B\":\"���L���F��-�س]�B\",\"�O�n���F��-�g�ٵo�i��\":\"�O�n���F��-�g�ٵo�i��\",\"�������F��-�g�ٵo�i��\":\"�������F��-�g�ٵo�i��\",\"�Ÿq���F��-�س]�B\":\"�Ÿq���F��-�س]�B\",\"�Ÿq���F��-�g�ٵo�i�B\":\"�Ÿq���F��-�g�ٵo�i�B\",\"�̪F���F��-���m�o�i�B\":\"�̪F���F��-���m�o�i�B\",\"�O�F���F��-�]�F�P�g�ٵo�i�B\":\"�O�F���F��-�]�F�P�g�ٵo�i�B\",\"�������F��-�س]��\":\"�������F��-�س]��\",\"��򿤬F��-�س]�B\":\"��򿤬F��-�س]�B\"}")]
        public string Gas2_Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "�Ƭd���i�ƥ����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas2_Copy_Unit { get; set; }









        [Display(Name = "�a��F���P�N���ʥ��ﵽ���G���", Order = 1)]
        public DateTime? EPB2_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "�a��F���P�N���ʥ��ﵽ���G", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB2_File { get; set; }

        [Display(Name = "�Ƭd�������", Order = 1)]
        public DateTime? GASend_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "�Ƭd����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string GASend_File { get; set; }

        [Display(Name = "�s�ʥ����", Order = 1)]
        public DateTime? ZERO_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "�s�ʥ�", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string ZERO_File { get; set; }



































        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "�[�o���޳N�Ը߻��ɳ��i���", Order = 1)]
        public DateTime? GAS3_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "�[�o���޳N�Ը߻��ɳ��i", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string GAS3_File { get; set; }

        [StringLength(30)]
        [Display(Name = "�[�o���޳N�Ը߻��ɳ��i�o��r��", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas3_Isseud_No { get; set; }

        [StringLength(20)]
        [Display(Name = "�[�o���޳N�Ը߻��ɳ��i�o����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas3_Isseud_Units { get; set; }

        [StringLength(70)]
        [Display(Name = "�[�o���޳N�Ը߻��ɳ��i������", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"�O�_���F��-���~�o�i��\":\"�O�_���F��-���~�o�i��\",\"�s�_���F��-�g�ٵo�i��\":\"�s�_���F��-�g�ٵo�i��\",\"�򶩥��F��-�u�ȳB\":\"�򶩥��F��-�u�ȳB\",\"��饫�F��-�g�ٵo�i��\":\"��饫�F��-�g�ٵo�i��\",\"�s�˥��F��-���~�o�i�B\":\"�s�˥��F��-���~�o�i�B\",\"�s�˿��F��-��ڲ��~�o�i�B\":\"�s�˿��F��-��ڲ��~�o�i�B\",\"�y�����F��-�u�ӮȹC�B\":\"�y�����F��-�u�ӮȹC�B\",\"�Ὤ���F��-�[���B\":\"�Ὤ���F��-�[���B\",\"�s�����F��-�س]��\":\"�s�����F��-�س]��\",\"�O�����F��-�g�ٵo�i��\":\"�O�����F��-�g�ٵo�i��\",\"�]�߿��F��-�u�ӵo�i�B\":\"�]�߿��F��-�u�ӵo�i�B\",\"�n�뿤�F��-�س]�B\":\"�n�뿤�F��-�س]�B\",\"���ƿ��F��-�س]�B\":\"���ƿ��F��-�س]�B\",\"���L���F��-�س]�B\":\"���L���F��-�س]�B\",\"�O�n���F��-�g�ٵo�i��\":\"�O�n���F��-�g�ٵo�i��\",\"�������F��-�g�ٵo�i��\":\"�������F��-�g�ٵo�i��\",\"�Ÿq���F��-�س]�B\":\"�Ÿq���F��-�س]�B\",\"�Ÿq���F��-�g�ٵo�i�B\":\"�Ÿq���F��-�g�ٵo�i�B\",\"�̪F���F��-���m�o�i�B\":\"�̪F���F��-���m�o�i�B\",\"�O�F���F��-�]�F�P�g�ٵo�i�B\":\"�O�F���F��-�]�F�P�g�ٵo�i�B\",\"�������F��-�س]��\":\"�������F��-�س]��\",\"��򿤬F��-�س]�B\":\"��򿤬F��-�س]�B\"}")]
        public string Gas3_Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "�[�o���޳N�Ը߻��ɳ��i�ƥ����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string Gas3_Copy_Unit { get; set; }











        [ColumnDef(Visible = false, VisibleEdit = true)]
        [Display(Name = "�a��F���P�N���i�ﵽ���", Order = 1)]
        public DateTime? EPB3_Date { get; set; }

        [StringLength(100)]
        [Display(Name = "�a��F���P�N���i�ﵽ", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB3_File { get; set; }

        [StringLength(30)]
        [Display(Name = "�a��F���P�N���i�ﵽ�o��r��", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB3_Isseud_No { get; set; }

        [StringLength(20)]
        [Display(Name = "�a��F���P�N���i�ﵽ�o����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB3_Isseud_Units { get; set; }

        [StringLength(70)]
        [Display(Name = "�a��F���P�N���i�ﵽ������", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"�O�_���F��-���~�o�i��\":\"�O�_���F��-���~�o�i��\",\"�s�_���F��-�g�ٵo�i��\":\"�s�_���F��-�g�ٵo�i��\",\"�򶩥��F��-�u�ȳB\":\"�򶩥��F��-�u�ȳB\",\"��饫�F��-�g�ٵo�i��\":\"��饫�F��-�g�ٵo�i��\",\"�s�˥��F��-���~�o�i�B\":\"�s�˥��F��-���~�o�i�B\",\"�s�˿��F��-��ڲ��~�o�i�B\":\"�s�˿��F��-��ڲ��~�o�i�B\",\"�y�����F��-�u�ӮȹC�B\":\"�y�����F��-�u�ӮȹC�B\",\"�Ὤ���F��-�[���B\":\"�Ὤ���F��-�[���B\",\"�s�����F��-�س]��\":\"�s�����F��-�س]��\",\"�O�����F��-�g�ٵo�i��\":\"�O�����F��-�g�ٵo�i��\",\"�]�߿��F��-�u�ӵo�i�B\":\"�]�߿��F��-�u�ӵo�i�B\",\"�n�뿤�F��-�س]�B\":\"�n�뿤�F��-�س]�B\",\"���ƿ��F��-�س]�B\":\"���ƿ��F��-�س]�B\",\"���L���F��-�س]�B\":\"���L���F��-�س]�B\",\"�O�n���F��-�g�ٵo�i��\":\"�O�n���F��-�g�ٵo�i��\",\"�������F��-�g�ٵo�i��\":\"�������F��-�g�ٵo�i��\",\"�Ÿq���F��-�س]�B\":\"�Ÿq���F��-�س]�B\",\"�Ÿq���F��-�g�ٵo�i�B\":\"�Ÿq���F��-�g�ٵo�i�B\",\"�̪F���F��-���m�o�i�B\":\"�̪F���F��-���m�o�i�B\",\"�O�F���F��-�]�F�P�g�ٵo�i�B\":\"�O�F���F��-�]�F�P�g�ٵo�i�B\",\"�������F��-�س]��\":\"�������F��-�س]��\",\"��򿤬F��-�س]�B\":\"��򿤬F��-�س]�B\"}")]
        public string EPB3_Shouwen_Units { get; set; }

        [StringLength(10)]
        [Display(Name = "�a��F���P�N���i�ﵽ�ƥ����", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string EPB3_Copy_Unit { get; set; }
    }
}
