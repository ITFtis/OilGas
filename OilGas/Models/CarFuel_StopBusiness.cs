namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;

    public partial class CarFuel_StopBusiness
    {
        [Key]
        [ColumnDef(Display = "查詢年度", EditType = EditType.Select,
            SelectItemsClassNamespace = YearSelectItemsClassImp.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Sortable = true, Visible = false)]
        [NotMapped]
        public string Year_Start { get; set; }

        [ColumnDef(Display = "縣市名稱", Sortable = true)]
        [NotMapped]
        public string workCity { get; set; }

        [ColumnDef(Display = "縣市代碼", Visible = false, Sortable = true)]
        [NotMapped]
        public string CityCode { get; set; }

        [ColumnDef(Display = "縣市排序", Visible = false, Sortable = true)]
        [NotMapped]
        public int Rank { get; set; }

        [ColumnDef(Display = "GSL代碼", Visible = false, Sortable = true)]
        [NotMapped]
        public string GSLCode { get; set; }

        [ColumnDef(Display = "新設", Sortable = true)]
        [NotMapped]
        public int AddBusiness { get; set; }

        [ColumnDef(Display = "暫停營業", Sortable = true)]
        [NotMapped]
        public int StopBusiness { get; set; }

        [ColumnDef(Display = "恢復營業", Sortable = true)]
        [NotMapped]
        public int ReBusiness { get; set; }

        [ColumnDef(Display = "歇業", Sortable = true)]
        [NotMapped]
        public int EndBusiness { get; set; }

        [ColumnDef(Display = "營業", Visible = false, Sortable = true)]
        [NotMapped]
        public int Business { get; set; }

    }

    public class lsYear
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public class YearSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.YearSelectItemsClassImp, OilGas";

        protected static IEnumerable<lsYear> _years;
        protected static IEnumerable<lsYear> YEARS
        {
            get
            {
                _years = DouHelper.Misc.GetCache<IEnumerable<lsYear>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_years == null)
                {
                    int nowYear = DateTime.Now.Year;
                    List<lsYear> lsYear = new List<lsYear>();

                    for (int i = 103; i <= (nowYear - 1911); i++)
                    {
                        lsYear.Add(new lsYear { Text = i.ToString(), Value = i });
                    }

                    _years = lsYear;
                    DouHelper.Misc.AddCache(_years, AssemblyQualifiedName);
                }
                return _years;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            return YEARS.Select(s => new KeyValuePair<string, object>(s.Text, s.Value));
        }
    }
}
