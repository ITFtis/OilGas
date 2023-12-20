namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_Facility
    {
        [Key]
        [StringLength(80)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        public int? Rank { get; set; }

        public bool? isDisplay { get; set; }
    }
}
