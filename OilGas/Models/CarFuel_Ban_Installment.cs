namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarFuel_Ban_Installment
    {
        public long ID { get; set; }

        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(10)]
        public string BanCaseNo { get; set; }

        public long? Installment { get; set; }

        public DateTime? Installment_date { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }
    }
}
