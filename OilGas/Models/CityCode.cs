namespace OilGas.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
    using System.Linq;
	using System.Web.Mvc;

    [Table("CityCode")]
    public partial class CityCode
    {
        public CityCode Clone()
        {
            return (CityCode)this.MemberwiseClone();
        }

        [Key]
        [StringLength(5)]
        public string CityName { get; set; }

        [Column("CityCode")]
        [StringLength(50)]
        public string CityCode1 { get; set; }

        public int? Rank { get; set; }

        [StringLength(10)]
        public string GSLCode { get; set; }

        static object lockGetAllDatas = new object();
        public static IEnumerable<CityCode> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheTime;

            string key = "OilGas.Models.CityCode";
            var allData = DouHelper.Misc.GetCache<IEnumerable<CityCode>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                {
                    Dou.Models.DB.IModelEntity<CityCode> modle = new Dou.Models.DB.ModelEntity<CityCode>(new OilGasModelContextExt());
                    allData = modle.GetAll().OrderBy(a => a.Rank).ToArray();
                    
                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }

        public IEnumerable<CityCode> GetUserCityDDL()
        {
            
			Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(new OilGasModelContextExt());

            var city = Dou.Context.CurrentUser<User>().city;

            if(city != "")
            {
				return cityCode.GetAll().Where(a => a.GSLCode == city).OrderBy(a => a.Rank);
			}

			return cityCode.GetAll().Prepend(new CityCode { GSLCode = string.Empty,CityName = "--¥þ°ê--" }).OrderBy(a => a.Rank);

		}
    }
}
