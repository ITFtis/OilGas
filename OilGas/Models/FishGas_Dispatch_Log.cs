namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FishGas_Dispatch_Log
    {
        public long ID { get; set; }

        [StringLength(10)]
        public string CaseNo { get; set; }

        public DateTime? Dispatch_date { get; set; }

        [StringLength(50)]
        public string DispatchClass { get; set; }

        [StringLength(30)]
        public string Dispatch_No { get; set; }

        [StringLength(30)]
        public string File_name { get; set; }

        [StringLength(30)]
        public string DispatchUnit { get; set; }

        [StringLength(30)]
        public string Shouwen_Units { get; set; }

        [StringLength(100)]
        public string CopyUnit { get; set; }

        [StringLength(250)]
        public string otherCopyUnit { get; set; }

        [StringLength(200)]
        public string Note { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }

        [StringLength(20)]
        public string License_No { get; set; }

        public int? Change { get; set; }
    }
}
