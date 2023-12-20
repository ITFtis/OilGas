namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GasLawBan")]
    public partial class GasLawBan
    {
        public long ID { get; set; }

        [StringLength(10)]
        public string CaseNo { get; set; }

        public int? Times { get; set; }

        [StringLength(30)]
        public string Reporter { get; set; }

        public DateTime? Report_date { get; set; }

        [StringLength(30)]
        public string Notification_unit { get; set; }

        [StringLength(30)]
        public string Investigation_unit { get; set; }

        public DateTime? Seized_date { get; set; }

        [StringLength(5)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(200)]
        public string AddressNo { get; set; }

        [StringLength(30)]
        public string Organizers { get; set; }

        [StringLength(10)]
        public string Treatment { get; set; }

        public int? Penalty_1 { get; set; }

        public int? Penalty_2 { get; set; }

        public int? Penalty_3 { get; set; }

        public long? Fine { get; set; }

        [StringLength(10)]
        public string Payments { get; set; }

        public long? PayOnce { get; set; }

        public DateTime? PayOnce_date { get; set; }

        public long? Accumulative { get; set; }

        public long? Owed { get; set; }

        public DateTime? Disposal_date { get; set; }

        [StringLength(30)]
        public string Issued_No { get; set; }

        [StringLength(30)]
        public string File_name { get; set; }

        public DateTime? Delivery_date { get; set; }

        public DateTime? Payment_deadline { get; set; }

        public DateTime? Call_date { get; set; }

        public DateTime? Payment_deadline2 { get; set; }

        public DateTime? Executive_date { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(10)]
        public string ID_No { get; set; }

        [StringLength(8)]
        public string Unified_No { get; set; }

        [StringLength(5)]
        public string ZipCode2 { get; set; }

        [StringLength(100)]
        public string Address2 { get; set; }

        public int? Investigation_refueling_gun { get; set; }

        public int? Investigation_petrol_tank_seat { get; set; }

        public long? Investigation_petrol_tank { get; set; }

        public int? Investigation_gas_gun { get; set; }

        public int? Investigation_gas_tank_seat { get; set; }

        public long? Investigation_gas_tank { get; set; }

        public long? Investigation_petrol { get; set; }

        public long? Investigation_diesel { get; set; }

        public long? Investigation_LPG { get; set; }

        public long? Investigation_fuel { get; set; }

        public long? Investigation_Barrel { get; set; }

        public int? Execution_refueling_gun { get; set; }

        public int? Execution_petrol_tank_seat { get; set; }

        public long? Execution_petrol_tank { get; set; }

        public int? Execution_gas_gun { get; set; }

        public int? Execution_gas_tank_seat { get; set; }

        public long? Execution_gas_tank { get; set; }

        public long? Execution_petrol { get; set; }

        public long? Execution_diesel { get; set; }

        public long? Execution_LPG { get; set; }

        public long? Execution_fuel { get; set; }

        public long? Execution_Barrel { get; set; }

        [StringLength(30)]
        public string Engineering_company { get; set; }

        [StringLength(100)]
        public string Notes { get; set; }

        public DateTime? Petitions_date { get; set; }

        public DateTime? Decision_date { get; set; }

        [StringLength(30)]
        public string Petitions_No { get; set; }

        [StringLength(10)]
        public string Petitions { get; set; }

        public DateTime? Litigation_date { get; set; }

        public DateTime? Judgment_date { get; set; }

        [StringLength(30)]
        public string Verdict_No { get; set; }

        [StringLength(30)]
        public string Verdict { get; set; }

        [StringLength(52)]
        public string MemberID { get; set; }

        //µÍ¿¿ƒÊ¶Ï
        public string vs_Seized_date
        {
            get
            {
                if (this.Seized_date == null)
                {
                    return null;
                }
                else
                {
                    return DateFormat.ToDate4((DateTime)this.Seized_date);
                }
            }
        }
    }
}
