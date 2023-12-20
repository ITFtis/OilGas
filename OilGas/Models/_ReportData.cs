using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    public class ReportData
    {
        //人員編號, 人員姓名, 所屬專案, 專案名稱, 簽核主管, 主管姓名, 管理公司, 所屬部門代碼, 加班日期, 加班單號, 加班處理方案, 加班開始日期, 加班開始時間, 加班結束日期, 加班結束時間, A1, B1, A2, B2, B3, B4, 理論時數合計, 時數合計, 加班費時數合計, 補休時數合計, 已補休時數, 已折現時數, 剩餘可補休時數, 加班原因, 單位, 備註
        public class ViewParams
        {
            /// <summary>
            /// 多條件
            /// </summary>
            public List<FilterValue> conditions { get; set; }

            /// <summary>
            /// 多欄位
            /// </summary>
            public List<FilterValue> columns { get; set; }
        }

        public class FilterValue
        {
            public string Id { get; set; }
            public string Value { get; set; }
        }
    }
}