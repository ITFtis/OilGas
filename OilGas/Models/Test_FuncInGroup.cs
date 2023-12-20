namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Test_FuncInGroup
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string GroupCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string FuncCode { get; set; }

        [StringLength(50)]
        public string FuncName { get; set; }

        public bool? IsEnable { get; set; }

        public int? Rank { get; set; }
    }
}
