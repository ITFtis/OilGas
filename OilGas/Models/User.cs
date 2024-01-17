using Dou.Misc.Attr;
using OilGas.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas.Models
{
	[Table("User")]
	public partial class User : Dou.Models.UserBase { 

    [Display(Name = "縣市")]
    [ColumnDef(VisibleEdit = true, Sortable = true, Filter = true, EditType = EditType.Select, SelectItemsClassNamespace = UsercitySelectItemsClassImp.AssemblyQualifiedName)]
     public string city { set; get; }

        [Display(Name = "職級")]
        [ColumnDef(VisibleEdit = true, Sortable = true, Filter = true, EditType = EditType.Select, SelectItemsClassNamespace = RolesSelectItemsClassImp.AssemblyQualifiedName)]
        public string grade { set; get; }

        //臺東縣
        [Display(Name = "單位名稱")]
        [ColumnDef(VisibleEdit = false, Visible = false)]
        public string OrganizationName
        { 
            get
            {
                var grade = Roles.GetAllDatas().Where(a => a.Value == this.grade).Select(a => a.Name);
                var city = CityCode.GetAllDatas().Where(a => a.GSLCode == this.city).Select(a => a.CityName);

                IEnumerable<string> list = grade.Union(city);
                return string.Join(", ", list);
            }
        }

        //台東縣政府－財政及經濟發展處
        [Display(Name = "單位完整名稱")]
        [ColumnDef(VisibleEdit = false, Visible = false)]
        public string OrganizationFullName
        {
            get
            {
                var pCitys = PowerCitysCodes();
                var citys = Code.ConvertTwCity(pCitys);
                var orgs = CarVehicleGas_DispatchUnit.GetAllDatas().Where(a => pCitys.Any(b => b == a.Value)).Select(a => a.Name);

                return string.Join(", ", orgs);
            }
        }

        [Display(Name = "權限查詢")]
		[ColumnDef(VisibleEdit = false, Visible = false)]
		private List<CityCode> PowerCitys
		{
			get
			{
				List<CityCode> result = new List<CityCode>();

				List<string> list = new List<string>() { "10", "11", "12" };
				var grades = Roles.GetAllDatas().Where(a => list.Any(b => b == a.Value));
				var allCitys = CityCode.GetAllDatas();

                //角色全開(admin)
                var admins = Role.GetAllDatas().Where(a => a.RoleUsers.Any(b => b.RoleId == "admin"));
				if (admins.Where(a => a.RoleUsers.Any(b => b.UserId == this.Id)).Count() > 0)
				{					
					return allCitys.ToList();
				}

                //有職級
                var u_grade = grades.Where(a => a.Value == this.grade);
                if (u_grade.Count() > 0)
				{					
					return allCitys.ToList();
				}

                //單一縣市
                var u_city = allCitys.Where(a => a.GSLCode == this.city);
                if (u_city.Count() > 0)
				{					
					return u_city.ToList();
                }

                //////盡量不要用特殊帳號開權限
                ////if (this.Id == "admin")
                ////{
                ////    //系統管理者固定帳號
                ////    result.AddRange(allCitys.Select(a => a.GSLCode));
                ////}

                return result;
			}
		}

        //縣市GSLCode("18,19"=>"18","19","18,19")
        public List<string> PowerCitysGSLs()
		{
            var list = this.PowerCitys.Select(a => a.GSLCode).ToList();

            var ary = list.Where(a => a.Contains(',')).ToArray();
            foreach (var v in ary)
            {
                foreach (var gsl in v.Split(','))
                {
                    if (!list.Contains(gsl))
                        list.Add(gsl);
                }
            }

            return list;
        }

        //縣市代碼
        public List<string> PowerCitysCodes()
        {
            var list = this.PowerCitys.Select(a => a.CityCode1).ToList();
            list = Code.ConvertTwCity(list);
            return list;
        }

        //縣市名稱
        public List<string> PowerCitysNames()
        {
            return this.PowerCitys.Select(a => a.CityName).ToList();
        }

        ////權限查詢
        //var pCitys1 = Dou.Context.CurrentUser<User>().PowerCitysGSLs();
        //var pCitys2 = Dou.Context.CurrentUser<User>().PowerCitysCodes();
        //var pCitys3 = Dou.Context.CurrentUser<User>().PowerCitysNames();

        ////var pCitys = Dou.Context.CurrentUser<User>().PowerCitys;
        ////var ienumber = iquery.AsEnumerable().Where(a => pCitys.Any(b => b == a.CITY));
    }


    public class UsercitySelectItemsClassImp : Dou.Misc.Attr.SelectItemsClass
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        public const string AssemblyQualifiedName = "OilGas.Models.UsercitySelectItemsClassImp,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {

			var cityCodes = db.CityCode.ToArray();

			//不是ADMIN只能看自己
			if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
			{
				cityCodes = cityCodes.Where(x => x.GSLCode == Dou.Context.CurrentUser<User>().city).ToArray();
			}

			//示範程式 可以用這段取得user的城市下拉清單

			//var cityCodes = CityCode.GetAllDatas(); //db.CityCode.ToArray();

			////權限查詢
			//var pCitys = Dou.Context.CurrentUser<User>().PowerCitys;
			//cityCodes = cityCodes.Where(a => pCitys.Any(b => b == a.GSLCode)).ToArray();


			return cityCodes.Select(s => new KeyValuePair<string, object>(s.GSLCode, "{\"v\":\"" + s.CityName + "\",\"s\":\"" + s.Rank + "\"}"));

			
		}
    }

    public class UsercityCodeSelectItems : Dou.Misc.Attr.SelectItemsClass
    {
        static public OilGasModelContextExt db = new OilGasModelContextExt();
        static public basicController basic = new basicController();
        public const string AssemblyQualifiedName = "OilGas.Models.UsercityCodeSelectItems,OilGas";
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {

            var cityCodes = db.CityCode.ToArray();

            //不是ADMIN只能看自己
            if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
            {
                cityCodes = cityCodes.Where(x => x.GSLCode == Dou.Context.CurrentUser<User>().city).ToArray();
            }

            return cityCodes.OrderBy(a => a.Rank).Select(s => new KeyValuePair<string, object>(s.CityCode1, s.CityName));
        }
    }
}