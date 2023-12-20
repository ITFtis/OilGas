namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PortGas_Code
    {
        [Key]
        public int PortGasCode_Index { get; set; }

        [StringLength(50)]
        public string PortGasCode_Type { get; set; }

        [StringLength(10)]
        public string PortGasCode_No { get; set; }

        [StringLength(100)]
        public string PortGasCode_Name { get; set; }

        [StringLength(400)]
        public string PortGasCode_Memo { get; set; }
    }
}
