using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OilGas.Controllers
{
    public class DataController : ApiController
    {
        internal const int longcacheduration = 30 * 60 * 1000;
        internal const int shortcacheduration = 5 * 60 * 1000;
        internal const int onemincacheduration = 1 * 60 * 1000;
        static object lockGetCarFuelBasicData = new object();
        static object lockGetCarGasBasicData = new object();
        static object lockGetFishGasBasicData = new object();
        static object lockGetSelfFuelBasicData = new object();
        static object lockGetSelfGasBasicData = new object();

        #region CarFuel 汽機車加油站
        /// <summary>
        /// 取得所有汽機車加油站基本資料
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        [Route("api/CarFuel/Base")]
        public IEnumerable<BasicData> GetCarFuelBasicData()
        {
            int cachetimer = onemincacheduration;
            string key = "OilGas.GetCarFuelBasicData";
            var allDatas = DouHelper.Misc.GetCache<IEnumerable<BasicData>>(cachetimer, key);
            lock (lockGetCarFuelBasicData)
            {
                if (allDatas == null)
                {
                    var dbContext = new OilGasModelContextExt();                   
                    Dou.Models.DB.IModelEntity<CarFuel_BasicData> _CFBD = new Dou.Models.DB.ModelEntity<CarFuel_BasicData>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageStateCode> _USC = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> _CVGBO = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                    var ls_CFBD = _CFBD.GetAll();
                    var ls_USC = _USC.GetAll();
                    var ls_CVGBO = _CVGBO.GetAll();

                    var query = from o in ls_CFBD
                                join p in ls_USC
                                on o.UsageState equals p.Value
                                into groupjoin
                                from a in groupjoin.DefaultIfEmpty()
                                join q in ls_CVGBO
                                on o.Business_theme equals q.Value
                                into groupjoin2
                                from b in groupjoin2.DefaultIfEmpty()
                                select new BasicData
                                {
                                    CaseNo = o.CaseNo,
                                    Gas_Name = string.IsNullOrEmpty(o.Gas_Name) ? "" : o.Gas_Name.Trim(),
                                    Business_theme = string.IsNullOrEmpty(b.Name) ? "" : b.Name.Trim(),
                                    Address = string.IsNullOrEmpty(o.Address) ? "" : o.Address.Trim(),
                                    Recipient_date = o.Recipient_date,
                                    UsageState = string.IsNullOrEmpty(a.Name) ? "" : a.Name.Trim(),
                                    TWD97_X = string.IsNullOrEmpty(o.Longitude_E) ? "" : o.Longitude_E.Trim(),
                                    TWD97_Y = string.IsNullOrEmpty(o.Longitude_N) ? "" : o.Longitude_N.Trim(),
                                };
                    allDatas = query.ToList();
                }
            }
            return allDatas;
        }
        #endregion
        #region CarGas 汽車加氣站
        /// <summary>
        /// 取得所有汽車加氣站基本資料
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        [Route("api/CarGas/Base")]
        public IEnumerable<BasicData> GetCarGasBasicData()
        {
            int cachetimer = onemincacheduration;
            string key = "OilGas.GetCarGasBasicData";
            var allDatas = DouHelper.Misc.GetCache<IEnumerable<BasicData>>(cachetimer, key);
            lock (lockGetCarGasBasicData)
            {
                if (allDatas == null)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<CarGas_BasicData> _CGBD = new Dou.Models.DB.ModelEntity<CarGas_BasicData>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageStateCode> _USC = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> _CVGBO = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                    var ls_CGBD = _CGBD.GetAll().Where(x => !string.IsNullOrEmpty(x.CaseNo));
                    var ls_USC = _USC.GetAll();
                    var ls_CVGBO = _CVGBO.GetAll();

                    var query = from o in ls_CGBD
                                join p in ls_USC
                                on o.UsageState equals p.Value
                                into groupjoin
                                from a in groupjoin.DefaultIfEmpty()
                                join q in ls_CVGBO
                                on o.Business_theme equals q.Value
                                into groupjoin2
                                from b in groupjoin2.DefaultIfEmpty()
                                select new BasicData
                                {
                                    CaseNo = o.CaseNo,
                                    Gas_Name = string.IsNullOrEmpty(o.Gas_Name) ? "" : o.Gas_Name.Trim(),
                                    Business_theme = string.IsNullOrEmpty(b.Name) ? "" : b.Name.Trim(),
                                    Address = string.IsNullOrEmpty(o.Address) ? "" : o.Address.Trim(),
                                    Recipient_date = o.Recipient_date,
                                    UsageState = string.IsNullOrEmpty(a.Name) ? "" : a.Name.Trim(),
                                    TWD97_X = string.IsNullOrEmpty(o.Longitude_E) ? "" : o.Longitude_E.Trim(),
                                    TWD97_Y = string.IsNullOrEmpty(o.Longitude_N) ? "" : o.Longitude_N.Trim(),
                                };
                    allDatas = query.ToList();
                }
            }
            return allDatas;
        }
        #endregion
        #region FishGas 漁船加油站
        /// <summary>
        /// 取得所有漁船加油站基本資料
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        [Route("api/FishGas/Base")]
        public IEnumerable<BasicData> GetFishGasBasicData()
        {
            int cachetimer = onemincacheduration;
            string key = "OilGas.GetFishGasBasicData";
            var allDatas = DouHelper.Misc.GetCache<IEnumerable<BasicData>>(cachetimer, key);
            lock (lockGetFishGasBasicData)
            {
                if (allDatas == null)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<FishGas_BasicData> _FGBD = new Dou.Models.DB.ModelEntity<FishGas_BasicData>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageStateCode> _USC = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> _CVGBO = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                    var ls_FGBD = _FGBD.GetAll();
                    var ls_USC = _USC.GetAll();
                    var ls_CVGBO = _CVGBO.GetAll();

                    var query = from o in ls_FGBD
                                join p in ls_USC
                                on o.UsageState equals p.Value
                                into groupjoin
                                from a in groupjoin.DefaultIfEmpty()
                                join q in ls_CVGBO
                                on o.Business_theme equals q.Value
                                into groupjoin2
                                from b in groupjoin2.DefaultIfEmpty()
                                select new BasicData
                                {
                                    CaseNo = o.CaseNo,
                                    Gas_Name = string.IsNullOrEmpty(o.Gas_Name) ? "" : o.Gas_Name.Trim(),
                                    Business_theme = string.IsNullOrEmpty(b.Name) ? "" : b.Name.Trim(),
                                    Address = string.IsNullOrEmpty(o.Address) ? "" : o.Address.Trim(),
                                    Recipient_date = o.Recipient_date,
                                    UsageState = string.IsNullOrEmpty(a.Name) ? "" : a.Name.Trim(),
                                    TWD97_X = string.IsNullOrEmpty(o.Longitude_E) ? "" : o.Longitude_E.Trim(),
                                    TWD97_Y = string.IsNullOrEmpty(o.Longitude_N) ? "" : o.Longitude_N.Trim(),
                                };
                    allDatas = query.ToList();
                }
            }
            return allDatas;
        }
        #endregion
        #region SelfFuel 自用加儲油站
        /// <summary>
        /// 取得所有自用加儲油站基本資料
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        [Route("api/SelfFuel/Base")]
        public IEnumerable<BasicData> GetSelfFuelBasicData()
        {
            int cachetimer = onemincacheduration;
            string key = "OilGas.GetSelfFuelBasicData";
            var allDatas = DouHelper.Misc.GetCache<IEnumerable<BasicData>>(cachetimer, key);
            lock (lockGetSelfFuelBasicData)
            {
                if (allDatas == null)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<SelfFuel_Basic> _SFBD = new Dou.Models.DB.ModelEntity<SelfFuel_Basic>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageStateCode> _USC = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> _CVGBO = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                    var ls_SFBD = _SFBD.GetAll().Where(x=>!string.IsNullOrEmpty(x.CaseNo));
                    var ls_USC = _USC.GetAll();
                    var ls_CVGBO = _CVGBO.GetAll();

                    var query = from o in ls_SFBD
                                join p in ls_USC
                                on o.UsageState equals p.Value
                                into groupjoin
                                from a in groupjoin.DefaultIfEmpty()
                                select new BasicData
                                {
                                    CaseNo = o.CaseNo,
                                    Gas_Name = string.IsNullOrEmpty(o.FuelName) ? "" : o.FuelName.Trim(),
                                    Business_theme = string.IsNullOrEmpty(o.BusiOrg) ? "" : o.BusiOrg.Trim(),
                                    Address = string.IsNullOrEmpty(o.Address) ? "" : o.Address.Trim(),
                                    Recipient_date = o.ExpiredDate,
                                    UsageState = string.IsNullOrEmpty(a.Name) ? "" : a.Name.Trim(),
                                    TWD97_X = string.IsNullOrEmpty(o.Longitude_E) ? "" : o.Longitude_E.Trim(),
                                    TWD97_Y = string.IsNullOrEmpty(o.Longitude_N) ? "" : o.Longitude_N.Trim(),
                                };
                    allDatas = query.ToList();
                }
            }
            return allDatas;
        }
        #endregion
        #region SelfGas 自用加儲氣站
        /// <summary>
        /// 取得所有自用加儲氣站基本資料
        /// </summary>
        /// <returns></returns>
        // GET api/<controller>
        [Route("api/SelfGas/Base")]
        public IEnumerable<BasicData> GetSelfGasBasicData()
        {
            int cachetimer = onemincacheduration;
            string key = "OilGas.GetSelfFuelBasicData";
            var allDatas = DouHelper.Misc.GetCache<IEnumerable<BasicData>>(cachetimer, key);
            lock (lockGetSelfGasBasicData)
            {
                if (allDatas == null)
                {
                    var dbContext = new OilGasModelContextExt();
                    Dou.Models.DB.IModelEntity<SelfGas_Basic> _SGBD = new Dou.Models.DB.ModelEntity<SelfGas_Basic>(dbContext);
                    Dou.Models.DB.IModelEntity<UsageStateCode> _USC = new Dou.Models.DB.ModelEntity<UsageStateCode>(dbContext);
                    Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> _CVGBO = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
                    var ls_SGBD = _SGBD.GetAll().Where(x => !string.IsNullOrEmpty(x.CaseNo));
                    var ls_USC = _USC.GetAll();
                    var ls_CVGBO = _CVGBO.GetAll();

                    var query = from o in ls_SGBD
                                join p in ls_USC
                                on o.UsageState equals p.Value
                                into groupjoin
                                from a in groupjoin.DefaultIfEmpty()
                                select new BasicData
                                {
                                    CaseNo = o.CaseNo,
                                    Gas_Name = string.IsNullOrEmpty(o.FuelName) ? "" : o.FuelName.Trim(),
                                    Business_theme = string.IsNullOrEmpty(o.BusiOrg) ? "" : o.BusiOrg.Trim(),
                                    Address = string.IsNullOrEmpty(o.Address) ? "" : o.Address.Trim(),
                                    Recipient_date = o.ExpiredDate,
                                    UsageState = string.IsNullOrEmpty(a.Name) ? "" : a.Name.Trim(),
                                    TWD97_X = string.IsNullOrEmpty(o.Longitude_E) ? "" : o.Longitude_E.Trim(),
                                    TWD97_Y = string.IsNullOrEmpty(o.Longitude_N) ? "" : o.Longitude_N.Trim(),
                                };
                    allDatas = query.ToList();
                }
            }
            return allDatas;
        }
        #endregion
    }
}

public class BasicData
{
    public string CaseNo { get; set; }
    public string Gas_Name { get; set; }
    public string Business_theme { get; set; }
    public string Address { get; set; }
    public DateTime? Recipient_date { get; set; }
    public string UsageState { get; set; }
    public string TWD97_X { get; set; }
    public string TWD97_Y { get; set; }
}