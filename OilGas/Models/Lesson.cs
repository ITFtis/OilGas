namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lesson")]
    public partial class Lesson
    {
        [Key]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        [Display(Name = "UUID", Order = 1)]
        public Guid LessonID { get; set; }

        [StringLength(50)]
        [Display(Name = "�ҵ{", Order = 1)]
        public string ClassName { get; set; }

        [Display(Name = "�ҵ{���e", Order = 1)]
        [StringLength(500)]
        public string detail { get; set; }


        [Display(Name = "�ҵ{�ɶ�", Order = 1)]
        public DateTime? time { get; set; }
        

        [Display(Name = "���W���}", Order = 1)]
        [ColumnDef(Visible = true, VisibleEdit = false)]
        [NotMapped]
        public string URL
        {
            get
            {
                return "SignAdd?id=" + LessonID.ToString();
            }
            set
            {
            }
        }

        [Display(Name = "�������", Order = 1)]       
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"IdentityId\":\"������\",\"gender\":\"�ʧO\",\"Tel\":\"�q��\",\"Mobile\":\"���\",\"Email\":\"Email\",\"address\":\"�a�}\",\"birth\":\"�ͤ�\",\"Occupation\":\"¾�~\",\"Hobbies\":\"����\"}")]
        public string NoNullcolumn { get; set; }


    }
}
