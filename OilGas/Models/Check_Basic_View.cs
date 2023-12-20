using DouHelper;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OilGas
{
    [Table("Check_Basic_View")]
    public class Check_Basic_View
    {
        public int id { get; set; }
        [StringLength(10)]
        public string CheckNo { get; set; }
        public DateTime CheckDate { get; set; }
        [StringLength(70)]
        public string Gas_Name { get; set; }
        [StringLength(70)]
        public string Business_theme { get; set; }
        [StringLength(20)]
        public string Business_themeS { get; set; }
        [StringLength(70)]
        public string Business_theme_Name { get; set; }
        [StringLength(200)]
        public string Addr { get; set; }
        [StringLength(10)]
        public string Check_Style { get; set; }
        [StringLength(50)]
        public string CaseType { get; set; }
        [StringLength(2)]
        public string Tank_Well { get; set; }
        [StringLength(50)]
        public string CaseNo { get; set; }

        public int? Change { get; set; }

        public int? Frequency { get; set; }
        [StringLength(2)]
        public string Improve_Day { get; set; }
        [StringLength(100)]
        public string Check_Data { get; set; }
        [StringLength(8000)]
        public string AreaCode { get; set; }
        [StringLength(5)]
        public string CityName { get; set; }

        public int? AllConform { get; set; }

        public int? AllCount { get; set; }

        public int? AllDoesmeet { get; set; }

        public int? AllUnable { get; set; }




        static object lockGetAllDatas = new object();
        public static IEnumerable<Check_Basic_View> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheTime;

            string key = "OilGas.Check_Basic_View";
            var allData = DouHelper.Misc.GetCache<IEnumerable<Check_Basic_View>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                {
                    Dou.Models.DB.IModelEntity<Check_Basic_View> modle = new Dou.Models.DB.ModelEntity<Check_Basic_View>(new OilGasModelContextExt());
                    allData = modle.GetAll().ToArray();

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }
    }

    //public class z_Check_Basic_View : Check_Basic_View
    //{
    //    [Display(Name = "年度")]
    //    public int CheckYear
    //    {
    //        get
    //        {
    //            DateTime? date = this.GetFieldValue<Check_Basic_View, DateTime>("CheckDate");
    //            int year = date == null ? 0 : date.Value.Year;

    //            return year;

    //            //return 3;
    //        }
    //    }
    //}
}