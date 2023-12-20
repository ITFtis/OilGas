namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_File
    {
        public int id { get; set; }

        [StringLength(100)]
        public string File { get; set; }

        [StringLength(25)]
        public string File_Class { get; set; }

        [StringLength(30)]
        public string File_Name { get; set; }

        public byte? File_Size { get; set; }

        [StringLength(10)]
        public string CheckNo { get; set; }
    }
}
