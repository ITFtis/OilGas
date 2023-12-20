namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfFuel_Land_Log
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(2)]
        public string LandPriority { get; set; }

        public double? LandTotalSquare { get; set; }

        [StringLength(2)]
        public string LandClass { get; set; }

        [StringLength(2)]
        public string LandUsageZone { get; set; }

        [StringLength(10)]
        public string OtherLandUsageZone { get; set; }

        [StringLength(10)]
        public string CreateUserTemp { get; set; }

        public bool? IsConfirm { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(10)]
        public string ModifyUser { get; set; }

        public DateTime? DeleteTime { get; set; }

        [StringLength(10)]
        public string DeleteUser { get; set; }

        [StringLength(60)]
        public string Ip { get; set; }

        [StringLength(10)]
        public string Action { get; set; }

        public int? Change { get; set; }
    }
}
