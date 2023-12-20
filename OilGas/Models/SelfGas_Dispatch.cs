namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfGas_Dispatch
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }

        public DateTime? DispatchDate { get; set; }

        [StringLength(2)]
        public string DispatchClass { get; set; }

        [StringLength(20)]
        public string OtherDispatchClass { get; set; }

        [StringLength(30)]
        public string DispatchNo { get; set; }

        [StringLength(50)]
        public string FileOriginalName { get; set; }

        [StringLength(50)]
        public string FileNewName { get; set; }

        public int? FileSize { get; set; }

        public DateTime? FileUpLoadDate { get; set; }

        [StringLength(20)]
        public string DispatchUnit { get; set; }

        [StringLength(20)]
        public string ReceiveUnit { get; set; }

        [StringLength(100)]
        public string CopyUnit { get; set; }

        [StringLength(250)]
        public string OtherCopyUnit { get; set; }

        [StringLength(500)]
        public string DispatchNote { get; set; }

        [StringLength(10)]
        public string CreateUserTemp { get; set; }

        public bool? IsConfirm { get; set; }

        [StringLength(20)]
        public string License_No { get; set; }

        public int? Change { get; set; }
    }
}
