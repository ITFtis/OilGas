namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PortGas_Insurance_Log
    {
        public long ID { get; set; }

        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(30)]
        public string Insurance_Company { get; set; }

        [StringLength(30)]
        public string Insurance_otherCompany { get; set; }

        public DateTime? Insurance_policy_start { get; set; }

        public DateTime? Insurance_policy_end { get; set; }

        [StringLength(30)]
        public string Insurance_No { get; set; }

        [StringLength(5)]
        public string Insurance_Type { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }

        public int? Change { get; set; }
    }
}
