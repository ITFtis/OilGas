namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PortGas_Counseling
    {
        public int id { get; set; }

        [StringLength(10)]
        public string Counseling_No { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Counseling_Date { get; set; }

        [StringLength(25)]
        public string Counseling_Staff { get; set; }

        [StringLength(25)]
        public string Gas_Staff { get; set; }

        [StringLength(25)]
        public string Gas_Tel { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [StringLength(100)]
        public string Achievement_Data { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Isseud_Date { get; set; }

        [StringLength(30)]
        public string Isseud_Class { get; set; }

        [StringLength(30)]
        public string Isseud_No { get; set; }

        [StringLength(100)]
        public string Isseud_Data { get; set; }

        [StringLength(20)]
        public string Isseud_Units { get; set; }

        [StringLength(30)]
        public string Shouwen_Units { get; set; }

        [StringLength(10)]
        public string Copy_Unit { get; set; }

        [StringLength(50)]
        public string CaseNo { get; set; }

        [StringLength(70)]
        public string Gas_Name { get; set; }

        [StringLength(200)]
        public string Gas_Location { get; set; }

        [StringLength(200)]
        public string ApparatusOwner { get; set; }

        [StringLength(200)]
        public string LocationType { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        public int? Change { get; set; }
    }
}
