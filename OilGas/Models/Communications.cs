namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Communications
    {
        public int ID { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Area { get; set; }

        [StringLength(3)]
        public string userlv { get; set; }

        [StringLength(50)]
        public string Org { get; set; }

        [StringLength(50)]
        public string Titles { get; set; }

        [StringLength(10)]
        public string IDNO { get; set; }

        [StringLength(25)]
        public string Tel { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
    }
}
