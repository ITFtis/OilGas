namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class CarVehicleGas_DispatchUnit
    {
        [Key]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Value { get; set; }

        public int? Rank { get; set; }

        static object lockGetAllDatas = new object();
        public static IEnumerable<CarVehicleGas_DispatchUnit> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheTime;

            string key = "OilGas.Models.CarVehicleGas_DispatchUnit";
            var allData = DouHelper.Misc.GetCache<IEnumerable<CarVehicleGas_DispatchUnit>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                {
                    Dou.Models.DB.IModelEntity<CarVehicleGas_DispatchUnit> modle = new Dou.Models.DB.ModelEntity<CarVehicleGas_DispatchUnit>(new OilGasModelContextExt());
                    allData = modle.GetAll().OrderBy(a => a.Rank).ToArray();

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }
    }
}
