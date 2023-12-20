namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_GasBasicData_GasBasicData_Temp
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string CaseNo { get; set; }

        public DateTime? ReceiveDate { get; set; }

        [StringLength(50)]
        public string Undertaker { get; set; }

        [StringLength(20)]
        public string GasType { get; set; }

        [StringLength(50)]
        public string GasName { get; set; }

        [StringLength(50)]
        public string SoilServer { get; set; }

        [StringLength(50)]
        public string OtherSoilServer { get; set; }

        [StringLength(50)]
        public string BusinessOrganization { get; set; }

        [StringLength(50)]
        public string OtherBusinessOrganization { get; set; }

        public DateTime? OperationDate { get; set; }

        public DateTime? BuildStartDate { get; set; }

        public DateTime? BuildEndDate { get; set; }

        public int? BuildCount { get; set; }

        public DateTime? BuildExtensionDate { get; set; }

        [StringLength(10)]
        public string BusinessState { get; set; }

        public int? BusinessCount { get; set; }

        public DateTime? BusinessExtensionDate { get; set; }

        [StringLength(50)]
        public string LicenseNo { get; set; }

        public DateTime? LicenseDate { get; set; }

        [StringLength(10)]
        public string StationLeader { get; set; }

        [StringLength(20)]
        public string StationPhoneNo { get; set; }

        [StringLength(3)]
        public string GasAddressZip { get; set; }

        [StringLength(100)]
        public string GasAddress { get; set; }

        [StringLength(3)]
        public string ParcelNumberZip { get; set; }

        [StringLength(50)]
        public string ParcelNumberAddress { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Change { get; set; }

        [StringLength(5)]
        public string CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(5)]
        public string ModifyUser { get; set; }

        public DateTime? ModifyTime { get; set; }

        public DateTime? BusinessPauseDate { get; set; }

        [StringLength(5)]
        public string LicenseNoA { get; set; }

        [StringLength(5)]
        public string LicenseNoB { get; set; }
    }
}
