namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("CheckItemList")]
    public partial class CheckItemList
    {
        [Key]
        public int CheckItemList_Index { get; set; }

        [StringLength(50)]
        public string CheckItemTable { get; set; }

        [StringLength(255)]
        public string CheckItemTitel { get; set; }

        [StringLength(10)]
        public string CheckItemTitelNo { get; set; }

        [StringLength(20)]
        public string CheckItemTitelSum { get; set; }

        [StringLength(255)]
        public string CheckItemDesc { get; set; }

        [StringLength(10)]
        public string CheckItemDescNo { get; set; }

        static object lockGetAllDatas = new object();
        public static IEnumerable<CheckItemList> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheTime;

            string key = "OilGas.Models.CheckItemList";
            var allData = DouHelper.Misc.GetCache<IEnumerable<CheckItemList>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                {
                    Dou.Models.DB.IModelEntity<CheckItemList> modle = new Dou.Models.DB.ModelEntity<CheckItemList>(new OilGasModelContextExt());
                    allData = modle.GetAll().ToArray();

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }
    }

    public class CheckItemListSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.CheckItemListSelectItemsClassImp, OilGas";

        static IEnumerable<CheckItemList> _checks;
        public static IEnumerable<CheckItemList> Checks
        {
            get
            {
                if (_checks == null || _checks.Count() == 0)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<CheckItemList> model = new Dou.Models.DB.ModelEntity<CheckItemList>(dbContext);

                    _checks = model.GetAll().Where(a => a.CheckItemTable == "Check_Item")
                                .Distinct().OrderBy(a => a.CheckItemTitelSum).ThenBy(a => a.CheckItemTitel)
                                .ToArray();
                }

                return _checks;
            }
        }

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var result = Checks.Select(s => new KeyValuePair<string, object>(s.CheckItemTitelSum, "{\"v\":\"" + s.CheckItemTitel + "\",\"s\":\"" + s.CheckItemTitelSum + "\"}"));
            return result;
        }
    }

    public class CheckItemListDetailSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.CheckItemListDetailSelectItemsClassImp, OilGas";

        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var result = CheckItemListSelectItemsClassImp.Checks.Select(s => new KeyValuePair<string, object>(s.CheckItemDescNo, "{\"v\":\"" + s.CheckItemDesc + "\",\"s\":\"" + s.CheckItemDescNo + "\",\"CheckItemTitel\":\"" + s.CheckItemTitelSum + "\"}"));
            return result;
        }
    }
}
