namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PortGas_UsageState
    {
        [Key]
        [StringLength(5)]
        public string UsageState_TypeID { get; set; }

        public int? UsageState_TypeRank { get; set; }

        [StringLength(50)]
        public string UsageState_TypeName { get; set; }

        [StringLength(50)]
        public string UsageState_Type_ParentType { get; set; }

        [StringLength(5)]
        public string UsageState_Type_ParentID { get; set; }

        [StringLength(5)]
        public string UsageState_Type_NextID { get; set; }

        [StringLength(5)]
        public string UsageState1 { get; set; }

        [StringLength(5)]
        public string UsageState2 { get; set; }

        [StringLength(5)]
        public string UsageState3 { get; set; }

        [StringLength(5)]
        public string UsageState4 { get; set; }

        [StringLength(5)]
        public string UsageState5 { get; set; }
    }
}
