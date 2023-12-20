namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemUsageRate")]
    public partial class SystemUsageRate
    {
        public int ID { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }

        [StringLength(52)]
        public string Pwd { get; set; }

        [StringLength(5)]
        public string Organization { get; set; }

        [StringLength(2)]
        public string UserLv { get; set; }

        [StringLength(200)]
        public string LoginIP { get; set; }

        public DateTime? LoginDate { get; set; }

        [StringLength(10)]
        public string LoginType { get; set; }
    }
}
