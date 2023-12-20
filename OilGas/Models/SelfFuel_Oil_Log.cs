namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfFuel_Oil_Log
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(2)]
        public string SoilClass { get; set; }

        public double? TroughCapacity { get; set; }

        public int? Ground { get; set; }

        public int? UnderGround { get; set; }

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
