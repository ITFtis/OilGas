namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_PdfFile
    {
        [StringLength(10)]
        public string CheckNo { get; set; }

        [StringLength(300)]
        public string File { get; set; }

        public bool? IsAction { get; set; }

        public int id { get; set; }

        [StringLength(25)]
        public string File_Class { get; set; }
    }
}
