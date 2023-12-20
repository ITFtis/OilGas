namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CarVehicleGas_GasBasicData_Dispatch_Temp
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string CaseNo { get; set; }

        public DateTime? DispatchDate { get; set; }

        [StringLength(20)]
        public string DispatchClass { get; set; }

        [StringLength(10)]
        public string OtherDispatchClass { get; set; }

        [StringLength(20)]
        public string DispatchNo { get; set; }

        [StringLength(50)]
        public string DispatchFileOriginalName { get; set; }

        [StringLength(50)]
        public string DispatchFileNewName { get; set; }

        [StringLength(20)]
        public string DispatchFileSize { get; set; }

        public DateTime? DispatchFileUpLoadDate { get; set; }

        [StringLength(30)]
        public string DispatchUnit { get; set; }

        [StringLength(30)]
        public string ReceiveUnit { get; set; }

        [StringLength(30)]
        public string CopyUnit { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

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

        [StringLength(10)]
        public string DispatchNoA { get; set; }
    }
}
