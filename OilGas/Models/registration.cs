namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("registration")]
    public partial class registration
    {
        public int id { get; set; }

        public int? BriefingID { get; set; }

        [StringLength(50)]
        public string GasName { get; set; }

        [StringLength(15)]
        public string GasTel { get; set; }

        [StringLength(20)]
        public string Business_theme { get; set; }

        [StringLength(50)]
        public string Business_theme_other { get; set; }

        [StringLength(15)]
        public string GasFAX { get; set; }

        [StringLength(100)]
        public string AddrNo { get; set; }

        [StringLength(20)]
        public string Participants { get; set; }

        [StringLength(20)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(1)]
        public string meals { get; set; }

        [StringLength(5)]
        public string ZipCode { get; set; }
    }
}
