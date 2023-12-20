namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_GasBasicData_ColumnName
    {
        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

        [StringLength(3)]
        public string Type { get; set; }

        public int? Rank { get; set; }
    }
}
