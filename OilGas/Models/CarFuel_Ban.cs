namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class CarFuel_Ban
    {
        [ColumnDef(Visible = false)]
        public long ID { get; set; }

        [Display(Name = "縣市", Order = 1)]
        [ColumnDef(Filter = true, Visible = false, VisibleEdit = false, EditType = EditType.Select,
        SelectItemsClassNamespace = UsercitySelectItemsClassImp.AssemblyQualifiedName)]
        [StringLength(10)]
        [NotMapped]
        public string CITY
        {
            get
            {
                if (CaseNo != null && CaseNo.Length > 6)
                {
                    return CaseNo.Substring(4, 2);
                }
                else
                {
                    return CaseNo;
                }
            }
            set
            {
            }
        }

        [StringLength(10)]
        [ColumnDef(Display = "案件編號", Index = 2,Filter = true,FilterAssign = FilterAssignType.Contains)]
        public string CaseNo { get; set; }

        [StringLength(10)]
        [ColumnDef(Display = "違規案件編號", Index = 1, Filter = true, FilterAssign = FilterAssignType.Contains)]
        public string BanCaseNo { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        public string CompanyType { get; set; }

        [StringLength(30)]
        [ColumnDef(Display = "站名", Filter = true)]
        public string Name { 
            get
            {              
                return CBData.Gas_Name;
            }  
        }

        [StringLength(5)]
        [ColumnDef(Visible = false)]
        public string ZipCode { get; set; }

        [StringLength(100)]
        [ColumnDef(Display = "地址", Visible = false)]
        public string Address {  get; set; }

        [ColumnDef(Display = "地址")]
        public string TrueAddress { get 
            {
                return string.IsNullOrWhiteSpace(this.Address) ? CBData.Address : this.Address;
            } 
        }

        [ColumnDef(Display = "違規日期", Visible = false, Filter = true, FilterAssign = FilterAssignType.Between)]
        public DateTime? Violation_date { get; set; }

        [ColumnDef(Display = "違規日期")]
        public string FormatViolation_Date
        {
            get
            {
                return this.Violation_date.Value.ToString("yyyy/MM/dd");
            }
        }

        [ColumnDef(Visible = false)]
        public long? Fine { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Limit_date { get; set; }

        [StringLength(200)]
        [ColumnDef(Visible = false)]
        public string note { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        public string Issued_No { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        public string File_name { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Delivery_date { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Executive_date { get; set; }

        [StringLength(10)]
        [ColumnDef(Visible = false)]
        public string Payments { get; set; }

        [ColumnDef(Visible = false)]
        public long? PayOnce { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? PayOnce_date { get; set; }

        [ColumnDef(Visible = false)]
        public long? Accumulative { get; set; }

        [ColumnDef(Visible = false)]
        public long? Owed { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Disposal_date { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Payment_deadline { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Petitions_date { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Decision_date { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(30)]
        public string Petitions_No { get; set; }

        [ColumnDef(Visible = false)]
        [StringLength(10)]
        public string Petitions { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Litigation_date { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Judgment_date { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        public string Verdict_No { get; set; }

        [StringLength(30)]
        [ColumnDef(Visible = false)]
        public string Verdict { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Create_date { get; set; }

        [StringLength(25)]
        [ColumnDef(Visible = false)]
        public string Create_name { get; set; }

        [ColumnDef(Visible = false)]
        public DateTime? Mod_date { get; set; }

        [StringLength(25)]
        [ColumnDef(Visible = false)]
        public string Mod_name { get; set; }

        [StringLength(52)]
        [ColumnDef(Visible = false)]
        public string MemberID { get; set; }

        private CarFuel_BasicData CBData { get
            {
                return CarFuel_BasicData.GetAllCarFuel_BasicData().Where(x => x.CaseNo == this.CaseNo).FirstOrDefault();
            }
        }
    }
}
