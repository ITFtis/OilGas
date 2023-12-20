namespace OilGas.Models
{
    using Dou.Misc.Attr;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class CarVehicleGas_BusinessOrganization
    {
        [Key]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Value { get; set; }

        public int? Rank { get; set; }

        public bool? IsEnable { get; set; }

        [StringLength(4)]
        public string ShortName { get; set; }

        [StringLength(2)]
        public string OldCode { get; set; }


        static object lockGetAllDatas = new object();
        public static IEnumerable<CarVehicleGas_BusinessOrganization> GetAllDatas(int cachetimer = 0)
        {
            if (cachetimer == 0) cachetimer = Constant.cacheTime;

            string key = "OilGas.Models.CarVehicleGas_BusinessOrganization";
            var allData = DouHelper.Misc.GetCache<IEnumerable<CarVehicleGas_BusinessOrganization>>(cachetimer, key);
            lock (lockGetAllDatas)
            {
                if (allData == null)
                {
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> modle = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(new OilGasModelContextExt());
                    allData = modle.GetAll().OrderBy(a => a.Rank).ToArray();

                    DouHelper.Misc.AddCache(allData, key);
                }
            }

            return allData;
        }

    }

    public class CarVehicleGas_BusinessOrganizationSelectItemsClassImp : SelectItemsClass
    {
        public const string AssemblyQualifiedName = "OilGas.Models.CarVehicleGas_BusinessOrganizationSelectItemsClassImp, OilGas";

        static IEnumerable<CarVehicleGas_BusinessOrganization> _buss;
        static IEnumerable<CarVehicleGas_BusinessOrganization> BUSS
        {
            get
            {
                if (_buss == null || _buss.Count() == 0)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> model = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                    _buss = model.GetAll().Where(a => a.IsEnable == true).OrderBy(a => a.Rank).ToArray();
                }
                return _buss;
            }
        }


        public static void Reset()
        {
            _buss = null;
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            
            return BUSS.Select(s => new KeyValuePair<string, object>(s.Value, JsonConvert.SerializeObject(new { v = s.Name, s = s.Rank })));
        }
    }

    //view
    public partial class CarVehicleGas_BusinessOrganizationV
    {
        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string CaseType { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string Value { get; set; }

        public int? Rank { get; set; }

        public bool? IsEnable { get; set; }

        [StringLength(4)]
        public string ShortName { get; set; }

        [StringLength(2)]
        public string OldCode { get; set; }
    }

    //view(客製化)
    public class CarVehicleGas_BusinessOrganizationVSelectItemsClassImp : SelectItemsClass
    {        
        public const string AssemblyQualifiedName = "OilGas.Models.CarVehicleGas_BusinessOrganizationVSelectItemsClassImp, OilGas";

        static IEnumerable<CarVehicleGas_BusinessOrganizationV> _buss;
        public static IEnumerable<CarVehicleGas_BusinessOrganizationV> BUSS
        {
            get
            {
                if (_buss == null || _buss.Count() == 0)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganizationV> model = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganizationV>(dbContext);

                    var datas = model.GetAll().Where(a => a.CaseType == "CarFuel_BasicData").Select(a => new {
                        CaseType = a.CaseType, 
                        a.Name, a.Value, a.Rank
                    }).Concat(model.GetAll().Where(a => a.CaseType == "FishGas_BasicData").Select(a => new {
                        CaseType = a.CaseType,
                        a.Name, a.Value, a.Rank
                    })).Concat(model.GetAll().Where(a => a.CaseType == "SelfFuel_Basic").Select(a => new {
                        CaseType = a.CaseType + "_Up",
                        a.Name, a.Value, a.Rank
                    })).Concat(model.GetAll().Where(a => a.CaseType == "SelfFuel_Basic").Select(a => new {
                        CaseType = a.CaseType + "_Down",
                        a.Name, a.Value, a.Rank
                    })).ToArray();

                    _buss = datas.Select(a => new CarVehicleGas_BusinessOrganizationV
                    {
                        CaseType = a.CaseType,
                        Name = a.Name,
                        Value = a.Value,
                        Rank = a.Rank,
                    }).OrderBy(a => a.Rank);
                }

                return _buss;
            }
        }


        public static void Reset()
        {
            _buss = null;
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var result = BUSS.Select(s => new KeyValuePair<string, object>(s.Value + "_" + s.CaseType.ToString(), "{\"v\":\"" + s.Name + "\",\"CaseType\":\"" + s.CaseType + "\"}"));
            return result;
        }
    }    

    //view(客製化)
    public class CarVehicleGas_BusinessOrganizationV2SelectItemsClassImp : SelectItemsClass
    {        
        public const string AssemblyQualifiedName = "OilGas.Models.CarVehicleGas_BusinessOrganizationV2SelectItemsClassImp, OilGas";

        static IEnumerable<CarVehicleGas_BusinessOrganizationV> _buss;
        public static IEnumerable<CarVehicleGas_BusinessOrganizationV> BUSS
        {
            get
            {
                if (_buss == null || _buss.Count() == 0)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganizationV> model = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganizationV>(dbContext);

                    var datas = model.GetAll().Where(a => a.CaseType == "CarFuel_BasicData").Select(a => new {
                        CaseType = a.CaseType, 
                        a.Name, a.Value, a.Rank
                    }).Concat(model.GetAll().Where(a => a.CaseType == "FishGas_BasicData").Select(a => new {
                        CaseType = a.CaseType,
                        a.Name, a.Value, a.Rank
                    })).Concat(model.GetAll().Where(a => a.CaseType == "SelfFuel_Basic").Select(a => new {
                        CaseType = a.CaseType,
                        a.Name, a.Value, a.Rank
                    })).ToArray();

                    _buss = datas.Select(a => new CarVehicleGas_BusinessOrganizationV
                    {
                        CaseType = a.CaseType,
                        Name = a.Name,
                        Value = a.Value,
                        Rank = a.Rank,
                    }).OrderBy(a => a.Rank);
                }

                return _buss;
            }
        }


        public static void Reset()
        {
            _buss = null;
        }
        public override IEnumerable<KeyValuePair<string, object>> GetSelectItems()
        {
            var result = BUSS.Select(s => new KeyValuePair<string, object>(s.Value + "_" + s.CaseType.ToString(), "{\"v\":\"" + s.Name + "\",\"CaseType\":\"" + s.CaseType + "\"}"));
            return result;
        }
    }    
}
