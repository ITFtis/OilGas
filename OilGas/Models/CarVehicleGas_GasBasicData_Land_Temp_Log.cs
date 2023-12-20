namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_GasBasicData_Land_Temp_Log
    {
        [Key]
        [StringLength(50)]
        public string CaseNo { get; set; }

        [StringLength(5)]
        public string LandPriority { get; set; }

        public double? LandTotalSquare { get; set; }

        [StringLength(50)]
        public string LandClass { get; set; }

        [StringLength(50)]
        public string LandUsageZone { get; set; }

        [StringLength(50)]
        public string OtherLandUsageZone { get; set; }

        [StringLength(5)]
        public string Wnamo { get; set; }

        [StringLength(200)]
        public string Facility { get; set; }

        [StringLength(50)]
        public string OtherFacility { get; set; }

        [StringLength(5)]
        public string SinglePump { get; set; }

        [StringLength(5)]
        public string DualPump { get; set; }

        [StringLength(5)]
        public string FourPump { get; set; }

        [StringLength(5)]
        public string SixPump { get; set; }

        [StringLength(5)]
        public string EightPump { get; set; }

        [StringLength(5)]
        public string TotalPump { get; set; }

        [StringLength(5)]
        public string SelfSinglePump { get; set; }

        [StringLength(5)]
        public string SelfDualPump { get; set; }

        [StringLength(5)]
        public string SelfFourPump { get; set; }

        [StringLength(5)]
        public string SelfSixPump { get; set; }

        [StringLength(5)]
        public string SelfEightPump { get; set; }

        [StringLength(5)]
        public string SelfTotalPump { get; set; }

        public int? Change { get; set; }

        [StringLength(5)]
        public string CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(5)]
        public string ModifyUser { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(5)]
        public string DeleteUser { get; set; }

        public DateTime? DeleteTime { get; set; }

        [StringLength(3)]
        public string Act { get; set; }
    }
}
