namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Tank_well_Action
    {
        public int id { get; set; }

        [StringLength(10)]
        public string CheckNo { get; set; }

        [StringLength(50)]
        public string CaseNo { get; set; }

        public int? Change { get; set; }

        [StringLength(10)]
        public string TankNo { get; set; }

        public decimal? Detection { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public int? Frequency { get; set; }

        [StringLength(50)]
        public string Testing_instruments { get; set; }

        public decimal? PID { get; set; }

        public decimal? FID { get; set; }

        [StringLength(10)]
        public string Oil { get; set; }

        [StringLength(1)]
        public string SlotNo { get; set; }

        [StringLength(10)]
        public string Well_Place { get; set; }
    }
}
