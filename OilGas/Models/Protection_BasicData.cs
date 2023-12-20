namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Protection_BasicData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public int BasicDataId { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string IdNo { get; set; }

        [StringLength(10)]
        [ColumnDef(Filter =true)]
        [Display(Name = "姓名", Order = 1)]
        public string Investigator { get; set; }

        [StringLength(30)]
        [Display(Name = "同意書簽核檔", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string FileName { get; set; }

        [StringLength(7)]
        [Display(Name = "郵遞區號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string ContactAddressZip { get; set; }

        [StringLength(100)]
        [Display(Name = "聯絡地址", Order = 1)]
        public string ContactAddress { get; set; }

        [StringLength(20)]
        [Display(Name = "聯絡電話", Order = 1)]
        public string ContactPhone { get; set; }





        [StringLength(50)]
        [Display(Name = "電子郵件信箱", Order = 1)]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "畢業學校", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string GraduateSchool { get; set; }




        [StringLength(50)]
        [Display(Name = "畢業科系", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string GraduateDepartment { get; set; }

        [StringLength(50)]
        [Display(Name = "最高學歷", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string HighestEducation { get; set; }

        [StringLength(250)]
        [Display(Name = "證照類別", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true,EditType =EditType.TextArea)]
        public string Certification { get; set; }

        [StringLength(50)]
        [Display(Name = "目前任職單位名稱", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string UnitName { get; set; }

        [StringLength(50)]
        [Display(Name = "目前任職聯絡電話", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string UnitPhone { get; set; }

        [StringLength(3)]
        [Display(Name = "目前任職郵遞區號", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string UnitContactAddressZip { get; set; }

        [StringLength(100)]
        [Display(Name = "目前任職聯絡地址", Order = 1)]
        [ColumnDef(Visible = false, VisibleEdit = true)]
        public string UnitContactAddress { get; set; }

        [StringLength(50)]
        [ColumnDef(Visible = false, VisibleEdit = false)]
        public string CreateUser { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? CreateTime { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(50)]
        public string ModifyUser { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? ModifyTime { get; set; }


    }
}
