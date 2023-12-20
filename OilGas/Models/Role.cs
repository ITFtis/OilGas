using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
    [Table("Role")]
    public partial class Role : Dou.Models.RoleBase 
    {
		static object lockGetAllDatas = new object();
		public static IEnumerable<Role> GetAllDatas(int cachetimer = 0)
		{
			if (cachetimer == 0) cachetimer = Constant.cacheTime;

			string key = "OilGas.Models.Role";
			var allData = DouHelper.Misc.GetCache<IEnumerable<Role>>(cachetimer, key);
			lock (lockGetAllDatas)
			{
				if (allData == null)
				{
					Dou.Models.DB.IModelEntity<Role> modle = new Dou.Models.DB.ModelEntity<Role>(new OilGasModelContextExt());
					allData = modle.GetAll().ToArray();

					DouHelper.Misc.AddCache(allData, key);
				}
			}

			return allData;
		}
	}



}