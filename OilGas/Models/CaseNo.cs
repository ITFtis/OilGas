namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CaseNo")]
    public partial class CaseNo
    {
        [Key]
        [Column("CaseNo")]
        [StringLength(10)]
        public string CaseNo1 { get; set; }

        [StringLength(30)]
        public string SystemName { get; set; }

        public DateTime? Create_date { get; set; }

        internal static string Substring(int v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
