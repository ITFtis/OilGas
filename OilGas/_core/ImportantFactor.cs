using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class ImportantFactor
    {
    }

    public class GetImportantFactorSelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.GetImportantFactorSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetImportantFactor();
        }
    }
}