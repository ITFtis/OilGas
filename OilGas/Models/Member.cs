namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Member")]
    public partial class Member
    {
        public int id { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }

        [StringLength(52)]
        public string Pass { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(25)]
        public string Tel { get; set; }

        [StringLength(1)]
        public string sex { get; set; }

        [StringLength(200)]
        public string AllowIpRange { get; set; }

        public DateTime? userStartDate { get; set; }

        public DateTime? userEndDate { get; set; }

        [StringLength(2)]
        public string userLv { get; set; }

        [Column(TypeName = "text")]
        public string Power { get; set; }

        [Column(TypeName = "text")]
        public string Range { get; set; }

        public DateTime? FillFormDate { get; set; }

        [StringLength(20)]
        public string ProTitle { get; set; }

        [StringLength(50)]
        public string PassWordTip { get; set; }

        [StringLength(10)]
        public string UnderTaker { get; set; }

        [StringLength(20)]
        public string UnderTakerContactPhone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(250)]
        public string PlanNote { get; set; }

        [StringLength(50)]
        public string AttachFileOriginalName { get; set; }

        [StringLength(50)]
        public string AttachFileNewName { get; set; }

        [StringLength(20)]
        public string AttachFileSize { get; set; }

        public DateTime? AttachFileUpLoadDate { get; set; }

        [StringLength(200)]
        public string NoPassReason { get; set; }

        [StringLength(10)]
        public string Checker { get; set; }

        [StringLength(3)]
        public string State { get; set; }

        public DateTime? ApplyDate { get; set; }

        public DateTime? CheckedDate { get; set; }

        [StringLength(20)]
        public string UnderUnit { get; set; }

        public DateTime? SecondDate { get; set; }

        [StringLength(10)]
        public string Seconder { get; set; }

        [StringLength(1)]
        public string isChangePass { get; set; }

        public bool? isStop { get; set; }
    }
}
