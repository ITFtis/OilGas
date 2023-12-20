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
        [Display(Name = "課程", Order = 1)]
        public string ClassName { get; set; }

        [Display(Name = "課程內容", Order = 1)]
        [StringLength(500)]
        public string detail { get; set; }


        [Display(Name = "課程時間", Order = 1)]
        public DateTime? time { get; set; }
        

        [Display(Name = "報名網址", Order = 1)]
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

        [Display(Name = "必填欄位", Order = 1)]       
        [ColumnDef(Visible = false, VisibleEdit = true, EditType = EditType.Radio, SelectItems = "{\"IdentityId\":\"身分證\",\"gender\":\"性別\",\"Tel\":\"電話\",\"Mobile\":\"手機\",\"Email\":\"Email\",\"address\":\"地址\",\"birth\":\"生日\",\"Occupation\":\"職業\",\"Hobbies\":\"興趣\"}")]
        public string NoNullcolumn { get; set; }


    }
}
