namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RecordLog")]
    public partial class RecordLog
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string CaseNo { get; set; }

        public string recordData { get; set; }

        [StringLength(100)]
        public string File_name { get; set; }

        [StringLength(10)]
        public string Mod_name { get; set; }

        public DateTime? Mod_date { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }
    }
}
