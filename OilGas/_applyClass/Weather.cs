using Dou.Misc.Attr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class Weather
    {
    }

    /// <summary>
    /// 天氣
    /// </summary>
    public class WeatherSelectItems : SelectItemsClass
    {        
        public const string AssemblyQualifiedName = "OilGas.WeatherSelectItems, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return Code.GetWeather();
        }
    }
}