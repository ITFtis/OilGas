namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SelfFuel_Ban_UploadFile
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string FileUploadId { get; set; }

        [StringLength(50)]
        public string FileOriginalName { get; set; }

        [StringLength(50)]
        public string FileNewName { get; set; }

        public int? FileSize { get; set; }

        public DateTime? FileUpLoadDate { get; set; }
    }
}
