using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class OrganizationCaseType
    {
    }

    public class ReportCaseTypeSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.ReportCaseTypeSelectItemsClassImp, OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetCaseType();
        }
    }

    public class OrganizationCaseTypeSelectItemsClassImp : SelectItemsClass
    {        
        public const string AssemblyQualifiedName = "OilGas.OrganizationCaseTypeSelectItemsClassImp, OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetCaseType2();
        }
    }
}