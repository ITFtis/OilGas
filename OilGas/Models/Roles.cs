namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class Roles
    {
        [Key]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Value { get; set; }

        public int? Rank { get; set; }

        static object lockGetAllDatas = new object();
        public static IEnumerable<Roles> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheTime;

            string key = "OilGas.Models.Roles";
            var allData = DouHelper.Misc.GetCache<IEnumerable<Roles>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                {
                    Dou.Models.DB.IModelEntity<Roles> modle = new Dou.Models.DB.ModelEntity<Roles>(new OilGasModelContextExt());
                    allData = modle.GetAll().OrderBy(a => a.Rank).ToArray();

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }
    }

    public class RolesSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.RolesSelectItemsClassImp, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            List<string> codes = new List<string> { "10", "11" };
            var roles = Roles.GetAllDatas().Where(a => codes.Contains(a.Value));
            return roles.Select(s => new KeyValuePair<string, object>(s.Value, s.Name));
        }


    }    
}
