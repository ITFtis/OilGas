using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class Check_Item_Report
    {
        #region  前端

        [StringLength(10)]
        public string CheckNo { get; set; }
        [StringLength(1)]

        public DateTime? CheckDate { get; set; }

        public int? CheckYear { get; set; }
        
        public string Business_theme { get; set; }
        public string Business_theme_Name { get; set; }
        public string Gas_Name { get; set; }
        public string A01 { get; set; }
        [StringLength(1)]
        public string A02 { get; set; }
        [StringLength(1)]
        public string A03 { get; set; }
        [StringLength(1)]
        public string A04 { get; set; }
        [StringLength(1)]
        public string A05 { get; set; }
        [StringLength(1)]
        public string A06 { get; set; }
        [StringLength(1)]
        public string A07 { get; set; }
        [StringLength(1)]
        public string A08 { get; set; }
        [StringLength(1)]
        public string A09 { get; set; }
        [StringLength(1)]
        public string A10 { get; set; }
        [StringLength(1)]
        public string A11 { get; set; }
        [StringLength(1)]
        public string A12 { get; set; }
        public string A_Notes { get; set; }
        [StringLength(1)]
        public string B01 { get; set; }
        [StringLength(1)]
        public string B02 { get; set; }
        [StringLength(1)]
        public string B03 { get; set; }
        [StringLength(1)]
        public string B04 { get; set; }
        [StringLength(1)]
        public string B05 { get; set; }
        [StringLength(1)]
        public string B06 { get; set; }
        [StringLength(1)]
        public string B07 { get; set; }
        [StringLength(1)]
        public string B08 { get; set; }
        [StringLength(1)]
        public string B09 { get; set; }
        [StringLength(1)]
        public string B10 { get; set; }
        public string B_Notes { get; set; }
        [StringLength(1)]
        public string C01 { get; set; }
        [StringLength(1)]
        public string C02 { get; set; }
        [StringLength(1)]
        public string C03 { get; set; }
        [StringLength(1)]
        public string C04 { get; set; }
        [StringLength(1)]
        public string C05 { get; set; }
        [StringLength(1)]
        public string C06 { get; set; }
        [StringLength(1)]
        public string C07 { get; set; }
        [StringLength(1)]
        public string C08 { get; set; }
        [StringLength(1)]
        public string C09 { get; set; }
        [StringLength(1)]
        public string C10 { get; set; }
        [StringLength(1)]
        public string C11 { get; set; }
        [StringLength(1)]
        public string C12 { get; set; }
        [StringLength(1)]
        public string C13 { get; set; }
        [StringLength(1)]
        public string C14 { get; set; }
        public string C_Notes { get; set; }
        [StringLength(1)]
        public string D01 { get; set; }
        [StringLength(1)]
        public string D02 { get; set; }
        [StringLength(1)]
        public string D03 { get; set; }
        [StringLength(1)]
        public string D04 { get; set; }
        [StringLength(1)]
        public string D05 { get; set; }
        [StringLength(1)]
        public string D06 { get; set; }
        [StringLength(1)]
        public string D07 { get; set; }
        [StringLength(1)]
        public string D08 { get; set; }
        [StringLength(1)]
        public string D09 { get; set; }
        [StringLength(1)]
        public string D10 { get; set; }
        [StringLength(1)]
        public string D11 { get; set; }
        public string D_Notes { get; set; }
        [StringLength(1)]
        public string E01 { get; set; }
        [StringLength(1)]
        public string E02 { get; set; }
        [StringLength(1)]
        public string E03 { get; set; }
        public string E_Notes { get; set; }
        [StringLength(1)]
        public string F01 { get; set; }
        [StringLength(1)]
        public string F02 { get; set; }
        [StringLength(1)]
        public string F03 { get; set; }
        [StringLength(1)]
        public string F04 { get; set; }
        [StringLength(1)]
        public string F05 { get; set; }
        [StringLength(1)]
        public string F06 { get; set; }
        [StringLength(1)]
        public string F07 { get; set; }
        [StringLength(1)]
        public string F08 { get; set; }
        [StringLength(1)]
        public string F09 { get; set; }
        public string F_Notes { get; set; }
        [StringLength(1)]
        public string G01 { get; set; }
        [StringLength(1)]
        public string G02 { get; set; }
        [StringLength(1)]
        public string G03 { get; set; }
        public string G04 { get; set; }
        public string G05 { get; set; }
        public string G06 { get; set; }
        public string G_Notes { get; set; }
        [StringLength(1)]
        public string H01 { get; set; }
        [StringLength(1)]
        public string H02 { get; set; }
        [StringLength(1)]
        public string H03 { get; set; }
        [StringLength(1)]
        public string H04 { get; set; }
        [StringLength(1)]
        public string H05 { get; set; }
        public string H_Notes { get; set; }
        [StringLength(1)]
        public string I01 { get; set; }
        [StringLength(1)]
        public string I02 { get; set; }

        [StringLength(1)]
        public string I03 { get; set; }

        [StringLength(1)]
        public string I04 { get; set; }

        [StringLength(1)]
        public string I05 { get; set; }

        [StringLength(1)]
        public string I06 { get; set; }

        [StringLength(1)]
        public string I07 { get; set; }

        [StringLength(1)]
        public string I08 { get; set; }

        [StringLength(1)]
        public string I09 { get; set; }

        [StringLength(1)]
        public string I10 { get; set; }
        public string I_Notes { get; set; }
        [StringLength(1)]
        public string J01 { get; set; }
        [StringLength(1)]
        public string J02 { get; set; }
        [StringLength(1)]
        public string J03 { get; set; }
        [StringLength(1)]
        public string J04 { get; set; }
        [StringLength(1)]
        public string J05 { get; set; }
        [StringLength(1)]
        public string J06 { get; set; }
        [StringLength(1)]
        public string J07 { get; set; }
        [StringLength(1)]
        public string J08 { get; set; }
        [StringLength(1)]
        public string J09 { get; set; }
        [StringLength(1)]
        public string J10 { get; set; }
        [StringLength(1)]
        public string J11 { get; set; }
        public string J_Notes { get; set; }
        [StringLength(1)]
        public string K01 { get; set; }
        [StringLength(1)]
        public string K02 { get; set; }
        [StringLength(1)]
        public string K03 { get; set; }
        [StringLength(1)]
        public string K04 { get; set; }
        public string K_Notes { get; set; }
        [StringLength(1)]
        public string L01 { get; set; }
        [StringLength(1)]
        public string L02 { get; set; }
        [StringLength(1)]
        public string L03 { get; set; }
        public string L_Notes { get; set; }
        [StringLength(1)]
        public string M01 { get; set; }
        [StringLength(1)]
        public string M02 { get; set; }
        public string M_Notes { get; set; }
        public int A_Count { get; set; }
        public int A_Conform { get; set; }
        public int? A_Doesmeet { get; set; }
        public int B_Count { get; set; }
        public int B_Conform { get; set; }
        public int? B_Doesmeet { get; set; }
        public int C_Count { get; set; }
        public int C_Conform { get; set; }
        public int? C_Doesmeet { get; set; }
        public int C_Unable { get; set; }
        public int D_Count { get; set; }
        public int D_Conform { get; set; }
        public int? D_Doesmeet { get; set; }
        public int E_Count { get; set; }
        public int E_Conform { get; set; }
        public int? E_Doesmeet { get; set; }
        public int F_Count { get; set; }
        public int F_Conform { get; set; }
        public int? F_Doesmeet { get; set; }
        public int G_Count { get; set; }
        public int G_Conform { get; set; }
        public int? G_Doesmeet { get; set; }
        public int H_Count { get; set; }
        public int H_Conform { get; set; }
        public int? H_Doesmeet { get; set; }
        public int H_Unable { get; set; }
        public int I_Count { get; set; }
        public int I_Conform { get; set; }
        public int? I_Doesmeet { get; set; }
        public int J_Count { get; set; }
        public int J_Conform { get; set; }
        public int? J_Doesmeet { get; set; }
        public int K_Count { get; set; }
        public int K_Conform { get; set; }
        public int? K_Doesmeet { get; set; }
        public int L_Count { get; set; }
        public int L_Conform { get; set; }
        public int? L_Doesmeet { get; set; }
        public int M_Count { get; set; }
        public int M_Conform { get; set; }
        public int? M_Doesmeet { get; set; }
        public int AllCount { get; set; }
        public int AllConform { get; set; }
        public int AllDoesmeet { get; set; }
        public int AllUnable { get; set; }
        [StringLength(10)]
        public string CaseNo { get; set; }
        public int Change { get; set; }
        [StringLength(1)]
        public string I00 { get; set; }
        [StringLength(1)]
        public string J00 { get; set; }
        [StringLength(1)]
        public string K00 { get; set; }
        [StringLength(1)]
        public string L00 { get; set; }
        [StringLength(10)]
        public string H01_Value { get; set; }
        [StringLength(10)]
        public string H02_Value { get; set; }
        [StringLength(10)]
        public string H03_Value { get; set; }
        [StringLength(10)]
        public string H04_Value { get; set; }
        public int A_Unable { get; set; }
        public int B_Unable { get; set; }
        public int I_Unable { get; set; }
        [StringLength(1)]
        public string F10 { get; set; }

        #endregion
    }
}