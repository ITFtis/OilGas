using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class CheckYear
    {
    }

    /// <summary>
    /// N年 + 年度加油站查核專案
    /// </summary>
    public class CheckYearSelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.CheckYearSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetCheckYear(95);
        }
    }

    /// <summary>
    /// N年
    /// </summary>
    public class CheckYear2SelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.CheckYear2SelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetCheckYear2(95);
        }
    }

    /// <summary>
    /// 石油設施查核名單篩選 查核年度
    /// </summary>
    public class EndCheckYaerSelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.EndCheckYaerSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetEndCheckYaer();
        }
    }

    /// <summary>
    /// 分級管理及重要因子交叉分析清單報表 預計查核年度
    /// </summary>
    public class ImportantCheckYaerSelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.ImportantCheckYaerSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetImportantCheckYaer();
        }
    }
}