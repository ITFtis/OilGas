namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_GasBasicData_SaleSoil_Temp_Log
    {
        public int? Id { get; set; }

        [Key]
        [StringLength(50)]
        public string CaseNo { get; set; }

        [StringLength(50)]
        public string TroughNo { get; set; }

        [StringLength(100)]
        public string StoreSoilClass { get; set; }

        [StringLength(50)]
        public string TroughCapacity { get; set; }

        [StringLength(50)]
        public string TroughLocation { get; set; }

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
