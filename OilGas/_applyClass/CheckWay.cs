using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class CheckWay
    {
    }

    /// <summary>
    /// 天氣
    /// </summary>
    public class CheckWaySelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.CheckWaySelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetCheckWay();
        }
    }
}