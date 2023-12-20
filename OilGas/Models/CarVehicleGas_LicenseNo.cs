namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_LicenseNo
    {
        public int ID { get; set; }

        [Required]
        [StringLength(5)]
        public string City { get; set; }

        [StringLength(3)]
        public string CityCode { get; set; }

        [Required]
        [StringLength(3)]
        public string Year { get; set; }

        [StringLength(50)]
        public string LicenseNo { get; set; }

        [StringLength(50)]
        public string DispatchNo { get; set; }

        [StringLength(3)]
        public string Act { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(10)]
        public string Creator { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(5)]
        public string Modifier { get; set; }
    }
}
