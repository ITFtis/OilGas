namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarGas_Ban_Log
    {
        public long ID { get; set; }

        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(10)]
        public string BanCaseNo { get; set; }

        [StringLength(30)]
        public string CompanyType { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(5)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public DateTime? Violation_date { get; set; }

        public long? Fine { get; set; }

        public DateTime? Limit_date { get; set; }

        [StringLength(200)]
        public string note { get; set; }

        [StringLength(30)]
        public string Issued_No { get; set; }

        [StringLength(30)]
        public string File_name { get; set; }

        public DateTime? Delivery_date { get; set; }

        public DateTime? Executive_date { get; set; }

        [StringLength(10)]
        public string Payments { get; set; }

        public long? PayOnce { get; set; }

        public DateTime? PayOnce_date { get; set; }

        public long? Accumulative { get; set; }

        public long? Owed { get; set; }

        public DateTime? Disposal_date { get; set; }

        public DateTime? Payment_deadline { get; set; }

        public DateTime? Petitions_date { get; set; }

        public DateTime? Decision_date { get; set; }

        [StringLength(30)]
        public string Petitions_No { get; set; }

        [StringLength(10)]
        public string Petitions { get; set; }

        public DateTime? Litigation_date { get; set; }

        public DateTime? Judgment_date { get; set; }

        [StringLength(30)]
        public string Verdict_No { get; set; }

        [StringLength(30)]
        public string Verdict { get; set; }

        public DateTime? Create_date { get; set; }

        [StringLength(25)]
        public string Create_name { get; set; }

        public DateTime? Mod_date { get; set; }

        [StringLength(25)]
        public string Mod_name { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }
    }
}
