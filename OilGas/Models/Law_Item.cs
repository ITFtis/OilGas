namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Law_Item
    {
        [Key]
        public int Law_Index { get; set; }

        [StringLength(100)]
        public string Law_name { get; set; }

        public int? Law_num { get; set; }

        [StringLength(500)]
        public string Law_memo { get; set; }

        [StringLength(500)]
        public string Law_http { get; set; }
    }
}
