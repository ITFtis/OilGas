namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_GasBasicData_Insurance_Temp_Log
    {
        [Key]
        [StringLength(50)]
        public string CaseNo { get; set; }

        [StringLength(50)]
        public string InsuranceCompanyName { get; set; }

        [StringLength(50)]
        public string OtherInsuranceCompanyName { get; set; }

        public DateTime? InsuranceValidateStartDate { get; set; }

        public DateTime? InsuranceValidateEndDate { get; set; }

        [StringLength(50)]
        public string InsuranceNo { get; set; }

        [StringLength(50)]
        public string PipeFileOriginalName { get; set; }

        [StringLength(50)]
        public string PipeFileNewName { get; set; }

        [StringLength(20)]
        public string PipeFileSize { get; set; }

        public DateTime? PipeFileUpLoadDate { get; set; }

        [StringLength(10)]
        public string Responsor { get; set; }

        [StringLength(10)]
        public string IdNumber { get; set; }

        [StringLength(3)]
        public string ContactAddressZip { get; set; }

        [StringLength(50)]
        public string ContactAddress { get; set; }

        [StringLength(30)]
        public string ContactPhone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? Change { get; set; }

        [StringLength(5)]
        public string CreateUser { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(5)]
        public string ModifyUser { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(5)]
        public string DeleteUser { get; set; }

        public DateTime? DeleteTime { get; set; }

        [StringLength(3)]
        public string Act { get; set; }
    }
}
