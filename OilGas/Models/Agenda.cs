namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Agenda
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string BriefingName { get; set; }

        public DateTime? BriefingDate { get; set; }

        [StringLength(4)]
        public string strDate { get; set; }

        [StringLength(4)]
        public string EndDate { get; set; }

        [StringLength(50)]
        public string Course_content { get; set; }

        [StringLength(50)]
        public string Lecturer { get; set; }
    }
}
