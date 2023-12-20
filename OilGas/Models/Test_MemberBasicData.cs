namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Test_MemberBasicData
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Account { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string PassWord { get; set; }
    }
}
