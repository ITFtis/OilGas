namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PortGas_Ban_Penalty
    {
        public long ID { get; set; }

        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(10)]
        public string BanCaseNo { get; set; }

        [StringLength(8)]
        public string Penalty_1 { get; set; }

        [StringLength(8)]
        public string Penalty_2 { get; set; }

        [StringLength(8)]
        public string Penalty_3 { get; set; }

        [StringLength(8)]
        public string Penalty_Car_1 { get; set; }

        [StringLength(8)]
        public string Penalty_Car_2 { get; set; }

        [StringLength(8)]
        public string Penalty_Car_3 { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }
    }
}
