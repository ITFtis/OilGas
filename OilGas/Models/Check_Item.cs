namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using OilGas.Models.BASE;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Check_Item : Check_Item_BASE
    {


        [StringLength(1)]
        [Display(Name = "��װ��׼лx���n", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string G04 { get; set; }

        [StringLength(1)]
        [Display(Name = "�O�W(�ج[)�ΰ�y���n�B�L�׷l", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string G05 { get; set; }

        [StringLength(1)]
        [Display(Name = "���u�\�O���n", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string G06 { get; set; }


        [StringLength(1)]
        [Display(Name = "�u�[�o���]�m�޲z�W�h�v��29��:�u�g��[�o���~�Ȫ̡A���̥[�o����B�]�ƥئ�w���ˬd��ۦ��I�[�o���]�I�C��B�C��ΨC�b�~�ˬd�����A���P��ڬ۲šA�ХB���O�s�@�~�H�W�C�v", Order = 4)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{\"0\":\"���]�m\",\"1\":\"�}�n\",\"2\":\"���}�n\",\"3\":\"�L�k�ˬd\"}")]
        public string M01 { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "�Ƶ�", Order = 4)]
        public string M_Notes { get; set; }













        [Display(Name = "�d�ֽs��", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Count { get; set; }

        [Display(Name = "�d�ֽs��", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Conform { get; set; }

        [Display(Name = "�d�ֽs��", Order = 4)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? M_Doesmeet { get; set; }
        
        
    }
}
