namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemPwdLog")]
    public partial class SystemPwdLog
    {
        [Key]
        public int wIndex { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }

        [StringLength(52)]
        public string Pwd { get; set; }

        public DateTime? ChangPwdDate { get; set; }
    }
}
