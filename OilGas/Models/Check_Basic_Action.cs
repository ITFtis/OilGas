namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Check_Basic_Action: Check_Basic_Base
    {

        [Display(Name = "¬d®Öµ²ªG", Order = 2)]
        [ColumnDef(VisibleEdit = false)]
        [NotMapped]
        public string URL
        {
            get
            {
                if (CaseType == "SelfFuel_Basic")
                {
                    if (Tank_Well == "0")
                    {
                        return "Check_Action_UP_" + CaseType + "?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
                    }
                    else
                    {
                        return "Check_Action_DOWN_" + CaseType + "?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
                    }
                }
                return "Check_Action_" + CaseType + "?CheckNo=" + CheckNo + "&CaseNo=" + CaseNo;
            }
            set
            {
            }
        }


        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(10)]
        public string ReviewNo { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? ReviewDate { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        public DateTime? Review_Talk_time { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(25)]
        public string Review_Officials { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(25)]
        public string Review_Record { get; set; }

        [ColumnDef(Visible = false, VisibleEdit = false)]
        [StringLength(50)]
        public string LicenseNo { get; set; }

     
    }
}
