using Dou.Misc.Attr;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas._applyClass
{
    public class LienceNoYear
    {
    }

    /// <summary>
    /// 取得lienceNo的下拉式年度
    /// </summary>
    public class LienceNoYearSelectItem : SelectItemsClass
    {
       
        public const string AssemblyQualifiedName = "OilGas._applyClass.LienceNoYearSelectItem, OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetLienceNoYear();
        }
    }
}