namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Gas_Total_Temp
    {
        public int Id { get; set; }

        [StringLength(1)]
        public string area { get; set; }

        [StringLength(20)]
        public string value { get; set; }

        public int? Total { get; set; }
    }
}
