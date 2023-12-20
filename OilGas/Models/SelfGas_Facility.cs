namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfGas_Facility
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }

        public int? SinglePump { get; set; }

        public int? DualPump { get; set; }

        public int? FourPump { get; set; }

        public int? SixPump { get; set; }

        public int? EightPump { get; set; }

        public int? TotalPump { get; set; }

        [StringLength(10)]
        public string CreateUserTemp { get; set; }

        public bool? IsConfirm { get; set; }

        public int? Change { get; set; }
    }
}
