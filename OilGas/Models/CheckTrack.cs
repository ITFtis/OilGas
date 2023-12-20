namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CheckTrack")]
    public partial class CheckTrack
    {
        public int ID { get; set; }

        [StringLength(10)]
        public string CaseNo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CheckDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Talk_time { get; set; }

        [StringLength(25)]
        public string Officials { get; set; }

        [StringLength(25)]
        public string Record { get; set; }

        [StringLength(2000)]
        public string TrackContents { get; set; }
    }
}
