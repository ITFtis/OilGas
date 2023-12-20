namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsageStateCode")]
    public partial class UsageStateCode
    {

        [StringLength(50)]
        public string Name { get; set; }


        [StringLength(50)]
        public string ShortName { get; set; }

        [Key]
        [StringLength(10)]
        public string Value { get; set; }


        public byte Rank { get; set; }

 
        [StringLength(50)]
        public string DispatchClassCode { get; set; }


        [StringLength(3)]
        public string Type { get; set; }
    }
}
