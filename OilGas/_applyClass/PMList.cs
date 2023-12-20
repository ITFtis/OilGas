using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class PMList
    {
    }

    public class GetPMListCheckTypeTSelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.GetPMListCheckTypeTSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetPMListCheckTypeT();
        }
    }

    public class GetPMListCheckTypeNSelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.GetPMListCheckTypeNSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetPMListCheckTypeN();
        }
    }
}