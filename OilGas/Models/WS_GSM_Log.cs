using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    public class WS_GSM_Log
    {
        [Key]
        [StringLength(50)]
        [Display(Name = "交換情形")]
        public string DataCount { get; set; }

        
        [ColumnDef(Visible = false)]
        public DateTime Sys_date { get; set; }

        [Display(Name = "交換日期時間")]
        public string ViewDate
        {
            get
            {
                return convertDate(Sys_date);
            }
        }

        private string convertDate(DateTime sys_date)
        {
            CultureInfo cultureTw = new CultureInfo("zh-TW");
            return sys_date.ToString("yyyy/MM/dd tt hh:mm:ss", CultureInfo.CreateSpecificCulture("zh-TW"));
        }
    }
}