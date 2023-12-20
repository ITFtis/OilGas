namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RP_Check_Hiatus_ByBusiOrgAndCity
    {
        public int Id { get; set; }

        [StringLength(5)]
        public string CityCode { get; set; }

        [StringLength(20)]
        public string BusinessOrgnization { get; set; }

        [StringLength(5)]
        public string BusinessOrgnizationCode { get; set; }

        [StringLength(10)]
        public string CheckYear { get; set; }

        public int? CheckCount { get; set; }

        public int? CheckAllDoesmeet { get; set; }

        public int? CheckNoHiatusCount { get; set; }

        public double? Average { get; set; }
    }
}
