namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Test_Group
    {
        [Key]
        [StringLength(10)]
        public string GroupCode { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public bool? IsEnable { get; set; }

        public int? Rank { get; set; }
    }
}
