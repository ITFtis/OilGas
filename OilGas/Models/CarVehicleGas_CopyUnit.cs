namespace OilGas.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Http.Results;

    public partial class CarVehicleGas_CopyUnit
    {
        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        public int? Rank { get; set; }
    }

    public class CarVehicleGas_CopyUnitSelectItems : Dou.Misc.Attr.SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.CarVehicleGas_CopyUnitSelectItems, OilGas";

        protected static IEnumerable<CarVehicleGas_CopyUnit> _carVehicleGas_CopyUnit;
        internal static IEnumerable<CarVehicleGas_CopyUnit> CarVehicleGas_CopyUnits
        {
            get
            {
                if (_carVehicleGas_CopyUnit == null)
                {
                    using (var db = new OilGasModelContextExt())
                    {
                        //權限查詢 (縣市權限，變動清除catch)
                        var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();

                        _carVehicleGas_CopyUnit = db.CarVehicleGas_CopyUnit.OrderBy(a => a.Rank).ToArray();
                    }
                }
                return _carVehicleGas_CopyUnit;
            }
        }


        public static void Reset()
        {
            _carVehicleGas_CopyUnit = null;
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return CarVehicleGas_CopyUnits.Select(s => new KeyValuePair<string, object>(s.Name, JsonConvert.SerializeObject(new { v = s.Name, s = s.Rank })));            
        }
    }
}
