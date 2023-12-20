namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("changebusiness")]
    public partial class changebusiness
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(2)]
        public string seq { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }

        [StringLength(50)]
        public string Gas_Name { get; set; }

        public DateTime? apply_date { get; set; }

        [StringLength(11)]
        public string prolong_no { get; set; }

        [StringLength(20)]
        public string Business_theme { get; set; }

        [StringLength(200)]
        public string otherBusiness_theme { get; set; }

        [StringLength(30)]
        public string boss_name { get; set; }

        [StringLength(10)]
        public string boss_idno { get; set; }

        [StringLength(10)]
        public string ZipCode { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public decimal? apply_area { get; set; }

        [StringLength(200)]
        public string AncillaryFacility { get; set; }

        [StringLength(200)]
        public string Facility { get; set; }

        public int? single_firearm { get; set; }

        public int? two_firearm { get; set; }

        public int? four_firearm { get; set; }

        public int? six_firearm { get; set; }

        public int? eight_firearm { get; set; }

        public int? others { get; set; }

        [StringLength(1)]
        public string sell_oil_cat { get; set; }

        public int? capacity_bing { get; set; }

        public int? capacity_seat { get; set; }

        public int? oil98_bing { get; set; }

        public int? oil98_seat { get; set; }

        public int? oil982_bing { get; set; }

        public int? oil982_seat { get; set; }

        public int? oil951_bing { get; set; }

        public int? oil951_seat { get; set; }

        public int? oil952_bing { get; set; }

        public int? oil952_seat { get; set; }

        public int? oil92_bing { get; set; }

        public int? oil92_seat { get; set; }

        public int? oil922_bing { get; set; }

        public int? oil922_seat { get; set; }

        public int? oildiesel_bing { get; set; }

        public int? oildiesel_seat { get; set; }

        public int? oilcoal_bing { get; set; }

        public int? oilcoal_seat { get; set; }
    }
}
