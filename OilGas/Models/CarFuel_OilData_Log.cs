namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarFuel_OilData_Log
    {
        public long ID { get; set; }

        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(10)]
        public string SaleSoilClass { get; set; }

        [StringLength(30)]
        public string Tank_no { get; set; }

        [StringLength(10)]
        public string Tank_place_type { get; set; }

        public int? Tank_type_tank { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }

        public int? Change { get; set; }

        public int? Tank_type_tank_seat { get; set; }
    }
}
