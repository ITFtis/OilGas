namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class SelfFuel_UsageState
    {
        [ColumnDef(Display = "�έp����-���O", EditType = EditType.Select, 
            SelectItems = "{'SelfFuel_Basic':'�̷s�ϥΪ��p�]�w���','SelfFuel_Basic_Log':'�ϥΪ��p�ܧ���{���'}",
            DefaultValue = "SelfFuel_Basic", Required = true, Filter = true, Visible = false)]
        [NotMapped]
        public string CaseType { get; set; }

        [ColumnDef(Display = "�έp�ɶ�", EditType = EditType.Date, Filter = true,
            FilterAssign = FilterAssignType.Between, Visible = false)]
        [NotMapped]
        public DateTime? Mod_date { get; set; }

        [ColumnDef(Display = "�����O", Visible = false, Sortable = true)]
        [NotMapped]
        public string CityName { get; set; }

        [ColumnDef(Display = "�ϥΪ��p(�D)", EditType = EditType.Select, SelectGearingWith = "UsageState,BigUsage"
            , SelectItems = "{'1':'�ӽФ�','2':'�ӽФ��Х���','3':'�ϥΤ�','4':'�ӽФ��Х���'}",
            DefaultValue = "1", Required = true, Filter = true, Visible = false)]
        [NotMapped]
        public string BigUsage { get; set; }

        [ColumnDef(Display = "�ϥΪ��p(�l)", EditType = EditType.Select,
            SelectItemsClassNamespace = UsageStateDetailSelectItems.AssemblyQualifiedName,
            Filter = true, FilterAssign = FilterAssignType.Equal, Visible = false)]
        public string UsageState { get; set; }

        [Key]
        [ColumnDef(Display = "�ץ�s��", EditType = EditType.Text, Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, Sortable = true)]
        [NotMapped]
        public string CaseNo { get; set; }

        [ColumnDef(Display = "�]�I�W��", EditType = EditType.Text, Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, Sortable = true)]
        [NotMapped]
        public string FuelName { get; set; }

        [ColumnDef(Display = "�t�d�H�m�W", EditType = EditType.Text, Filter = true, FilterAssign = FilterAssignType.Contains, Visible = false, Sortable = true)]
        [NotMapped]
        public string Responsor { get; set; }

    }
 
    public class UsageStateDetailSelectItems : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.UsageStateDetailSelectItems, OilGas";

        protected static IEnumerable<UsageStateDetail> _usd;
        protected static new IEnumerable<UsageStateDetail> USD
        {
            get
            {
                _usd = DouHelper.Misc.GetCache<IEnumerable<UsageStateDetail>>(2 * 60 * 1000, AssemblyQualifiedName);
                if (_usd == null)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<UsageStateDetail> UsageStateDetail = new Dou.Models.DB.ModelEntity<UsageStateDetail>(dbContext);
                    _usd = UsageStateDetail.GetAll().ToArray();
                    DouHelper.Misc.AddCache(_usd, AssemblyQualifiedName);
                }
                return _usd;
            }
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {   
            //return USD.Select(s => new KeyValuePair<string, object>(s.UsageStateDetailID, s.Name));
            return USD.Select(s => new KeyValuePair<string, object>(s.UsageStateDetailID, "{\"v\":\"" + s.Name + "\",\"BigUsage\":\"" + s.BigUsageStateID + "\"}"));
        }
    }
}
