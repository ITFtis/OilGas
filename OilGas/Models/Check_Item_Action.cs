namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using OilGas.Models.BASE;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Item_Action: Check_Item_BASE
    {


        [Display(Name = "��װ��׼лx���n", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{0:\"���]�m\",1:\"�}�n\",2:\"���}�n\",3:\"�L�k�ˬd\"}")]
        public int? G04 { get; set; }


        [Display(Name = "�O�W(�ج[)�ΰ�y���n�B�L�׷l", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{0:\"���]�m\",1:\"�}�n\",2:\"���}�n\",3:\"�L�k�ˬd\"}")]
        public int? G05 { get; set; }


        [Display(Name = "���u�\�O���n", Order = 2)]
        [Required]
        [ColumnDef(EditType = EditType.Radio, SelectItems = "{0:\"���]�m\",1:\"�}�n\",2:\"���}�n\",3:\"�L�k�ˬd\"}")]
        public int? G06 { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int? Frequency { get; set; }

      
    }
}
