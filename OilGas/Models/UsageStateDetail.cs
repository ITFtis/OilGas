namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsageStateDetail")]
    public partial class UsageStateDetail
    {
        [Key]
        public string UsageStateDetailID { get; set; }

        [StringLength(10)]
        public string BigUsageStateID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(10)]
        public string UsageState { get; set; }

        [StringLength(10)]
        public string UsageState_Second { get; set; }

        [StringLength(10)]
        public string UsageState_Third { get; set; }

        [StringLength(10)]
        public string UsageState_Fourth { get; set; }
    }
}
