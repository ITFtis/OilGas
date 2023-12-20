namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sign")]
    public partial class Sign
    {
        [Key]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "UUID", Order = 1)]
        public Guid SignId { get; set; }

        
        [Display(Name = "�ҵ{", Order = 1)]
        [ColumnDef(Filter = true, EditType = EditType.Select,
         SelectSourceDbContextNamespace = "OilGas.Models.OilGasModelContextExt, OilGas",
          SelectSourceModelNamespace = "OilGas.Models.Lesson, OilGas",
          SelectSourceModelValueField = "LessonID",
          SelectSourceModelDisplayField = "ClassName")]
        [Required]
        public Guid LessonID { get; set; }

        [Display(Name = "�m�W", Order = 1)]
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

      

        [Display(Name = "������", Order = 1)]
        [StringLength(10)]    
        public string IdentityId { get; set; }


        [Display(Name = "�ʧO", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Select, SelectItems = "{\"1\":\"�k\",\"2\":\"�k\",\"0\":\"��L\"}")]
        public int? gender { get; set; }








        [StringLength(20)]
        [Display(Name = "�q��", Order = 1)]
        public string Tel { get; set; }

        [Display(Name = "���", Order = 1)]
        [StringLength(10)]
        public string Mobile { get; set; }

        [Display(Name = "Email", Order = 1)]
        [StringLength(50)]
        public string Email { get; set; }


        [Display(Name = "�a�}", Order = 1)]
        [StringLength(200)]
        public string address { get; set; }

        [Display(Name = "�ͤ�", Order = 1)]
        [StringLength(15)]
        public string birth { get; set; }

        [Display(Name = "¾�~", Order = 1)]
        [StringLength(50)]
        public string Occupation { get; set; }

        [Display(Name = "����", Order = 1)]
        [StringLength(200)]
        public string Hobbies { get; set; }

       






    }
}
