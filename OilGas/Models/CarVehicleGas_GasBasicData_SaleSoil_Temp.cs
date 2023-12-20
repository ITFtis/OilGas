namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_GasBasicData_SaleSoil_Temp
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 0)]
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

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Change { get; set; }

        [StringLength(5)]
        public string CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(5)]
        public string ModifyUser { get; set; }

        public DateTime? ModifyTime { get; set; }
    }
}
