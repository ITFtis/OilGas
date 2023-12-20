namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Briefing")]
    public partial class Briefing
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string BriefingName { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime BriefingDate { get; set; }

        public DateTime? SignupdDate { get; set; }

        [StringLength(100)]
        public string BriefingLocation { get; set; }

        [StringLength(100)]
        public string BriefingAddr { get; set; }

        public int? BriefingPeople { get; set; }

        [StringLength(1)]
        public string IsHaveDietary_Context { get; set; }

        [StringLength(100)]
        public string File { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        [StringLength(20)]
        public string Keyin_Name { get; set; }

        public DateTime? Keyin_Date { get; set; }

        [StringLength(20)]
        public string Mod_Name { get; set; }

        public DateTime? Mod_Date { get; set; }

        [StringLength(5)]
        public string ZipCode { get; set; }
    }
}
