using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using OilGas;
using OilGas.Models;
using NPOI.SS.Formula.Functions;
using NPOI.XSSF.Streaming.Values;

namespace Cienve_Web
{
    /// <summary>
    /// DBGasManagement 的摘要描述
    /// </summary>
    public class DBGasManagement
    {
        System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
        public DBGasManagement()
	    {
		    //
		    // TODO: 在此加入建構函式的程式碼
		    //
	    }
        #region <Property>
        public string CaseNo { set; get; }
        public string ReceiveDate { set; get; }
        public string Undertaker { set; get; }
        public string GasType { set; get; }
        public string GasName { set; get; }
        public string SoilServer { set; get; }
        public string BusinessOrganization { set; get; }
        public string OtherBusinessOrganizatio { set; get; }
        public string OperationDate { set; get; }
        public string BuildStartDate { set; get; }
        public string BuildEndDate { set; get; }
        public string BuildCount { set; get; }
        public string BuildExtensionDate { set; get; }
        public string BusinessState { set; get; }
        public string BusinessPauseDate { set; get; }
        public string BusinessCount { set; get; }
        public string BusinessExtensionDate { set; get; }
        public string LicenseNo { set; get; }
        public string LicenseNoA { set; get;}
        public string LicenseNoB { set; get; }
        public string LicenseDate { set; get; }
        public string StationLeader { set; get; }
        public string StationPhoneNo { set; get; }
        public string GasAddressZip { set; get; }
        public string GasAddress { set; get; }
        public string ParcelNumberZip { set; get; }
        public string ParcelNumberAddress { set; get; }
        public string InsuranceCompanyName { set; get; }
        public string OtherInsuranceCompanyName { set; get; }
        public string InsuranceValidateStartDate { set; get; }
        public string InsuranceValidateEndDate { set; get; }
        public string InsuranceNo { set; get; }
        public string PipeFileOriginalName { set; get; }
        public string PipeFileNewName { set; get; }
        public string PipeFileSize { set; get; }
        public string PipeFileUpLoadDate { set; get; }
        public string Responsor { set; get; }
        public string IdNumber { set; get; }
        public string ContactAddressZip { set; get; }
        public string ContactAddress { set; get; }
        public string ContactPhone { set; get; }
        public string Email { set; get; }
        public string LandPriority { set; get; }
        public string LandTotalSquare { set; get; }
        public string LandClass { set; get; }
        public string LandUsageZone { set; get; }
        public string OtherLandUsageZone { set; get; }
        public string Wnamo { set; get; }
        public string Facility { set; get; }
        public string OtherFacility { set; get; }
        public string SinglePump { set; get; }
        public string DualPump { set; get; }
        public string FourPump { set; get; }
        public string SixPump { set; get; }
        public string EightPump { set; get; }
        public string TotalPump { set; get; }
        public string SelfSinglePump { set; get; }
        public string SelfDualPump { set; get; }
        public string SelfFourPump { set; get; }
        public string SelfSixPump { set; get; }
        public string SelfEightPump { set; get; }
        public string SelfTotalPump { set; get; }
        public string DispatchDate { set; get; }
        public string DispatchClass { set; get; }
        public string OtherDispatchClass { set; get; }
        public string DispatchNo { set; get; }
        public string DispatchNoA { set; get; }
        public string DispatchFileOriginalName { set; get; }
        public string DispatchFileNewName { set; get; }
        public string DispatchFileSize { set; get; }
        public string DispatchFileUpLoadDate { set; get; }
        public string DispatchUnit { set; get; }
        public string ReceiveUnit { set; get; }
        public string CopyUnit { set; get; }
        public string Note { set; get; }
        public string TroughNo { set; get; }
        public string StoreSoilClass { set; get; }
        public string TroughCapacity { set; get; }
        public string TroughLocation { set; get; }
        public string OtherSoilServer { set; get; }
        public string OtherBusinessOrganization { set; get; }
        #endregion

        #region <查詢營業主體資料>
        //public DataTable GetBusinessOrganization()
        //{
        //    string sqlstr = string.Format(@"
        //                    SELECT Name,Value
        //                    FROM CarVehicleGas_BusinessOrganization
        //                    WHERE IsEnable = 'True'
        //                    ORDER BY RANK                           
        //                    ");
        //    DataTable dt = Mei.DataAccess.ExecuteDataTable(sqlstr, Mei.DataAccess.ConnectionType._CienveGSLConnection);
        //    return dt;
        //}

        //public string GetBusinessOrganization(string _value)
        //{
        //    string sqlstr = string.Format(@"
        //                    SELECT Name,Value
        //                    FROM CarVehicleGas_BusinessOrganization
        //                    WHERE IsEnable = 'True' and Value = '{0}'
        //                    ORDER BY RANK                           
        //                    ",_value);
        //    DataTable dt = Mei.DataAccess.ExecuteDataTable(sqlstr, Mei.DataAccess.ConnectionType._CienveGSLConnection);
        //    string name = "";
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        name = dt.Rows[0]["Name"].ToString();
        //    }
        //    return name;
        //}
        #endregion

        #region <查詢用地類別>
        public DataTable GetLandClass()
        {          
            Dou.Models.DB.IModelEntity<LandClassCode> LandClassCode = new Dou.Models.DB.ModelEntity<LandClassCode>(dbContext);
            var landdata = LandClassCode.GetAll().OrderBy(x=>x.Rank).Select(n => new
            {
                Name = n.Name,
                Value = n.Value
            }).ToArray();
            return ArrayToDataTable(landdata);

            //string sqlstr = string.Format(@"
            //                SELECT Name,Value
            //                FROM LandClassCode
            //                ORDER BY RANK
            //                ");
            //DataTable dt = Mei.DataAccess.ExecuteDataTable(sqlstr, Mei.DataAccess.ConnectionType._CienveGSLConnection);
            //return dt;
        }

        //查詢用地類別 - 單筆
        public DataTable GetLandClass(string _LandType)
        {
            Dou.Models.DB.IModelEntity<LandClassCode> LandClassCode = new Dou.Models.DB.ModelEntity<LandClassCode>(dbContext);
            var landdata = LandClassCode.GetAll().Where(s => s.LandType == int.Parse(_LandType) as int?)
                .OrderBy(x => x.Rank).Select(n => new
                {
                    Name = n.Name,
                    Value = n.Value
                }).ToArray();
            return ArrayToDataTable(landdata);

            //string sqlstr = string.Format(@"
            //                SELECT Name,Value
            //                FROM LandClassCode
            //                WHERE LandType = '{0}'
            //                ORDER BY RANK
            //                ", _LandType);
            //DataTable dt = Mei.DataAccess.ExecuteDataTable(sqlstr, Mei.DataAccess.ConnectionType._CienveGSLConnection);
            //return dt;
        }

        //查詢用地類別 - for報表
        public DataTable GetLandClassForStatistic(string _LandType)
        {
            Dou.Models.DB.IModelEntity<LandClassCode> LandClassCode = new Dou.Models.DB.ModelEntity<LandClassCode>(dbContext);
            var landdata = LandClassCode.GetAll().Where(s => s.LandType == (int.Parse(_LandType) as int?) & int.Parse(s.Value) > 1)
                .OrderBy(x => x.Rank).Select(n => new
                {
                    Name = n.Name,
                    Value = n.Value
                }).ToArray();
            return ArrayToDataTable(landdata);
            //string sqlstr = string.Format(@"
            //                SELECT Name,Value
            //                FROM LandClassCode
            //                WHERE LandType = '{0}' and Value > 1 --不顯示 － 
            //                ORDER BY RANK
            //                ", _LandType);
            //DataTable dt = Mei.DataAccess.ExecuteDataTable(sqlstr, Mei.DataAccess.ConnectionType._CienveGSLConnection);
            //return dt;
        }
        #endregion

        #region <查詢土地使用分區>
        public DataTable GetUsageZone()
        {
            Dou.Models.DB.IModelEntity<LandUsageZoneCode> LandUsageZoneCode = new Dou.Models.DB.ModelEntity<LandUsageZoneCode>(dbContext);
            var landusagedata = LandUsageZoneCode.GetAll().OrderBy(x => x.Rank).Select(n => new
            {
                Name = n.Name,
                Value = n.Value
            }).ToArray();
            return ArrayToDataTable(landusagedata);
            //string sqlstr = string.Format(@"
            //                SELECT Name,Value
            //                FROM LandUsageZoneCode
            //                ORDER BY RANK
            //                ");
            //DataTable dt = Mei.DataAccess.ExecuteDataTable(sqlstr, Mei.DataAccess.ConnectionType._CienveGSLConnection);
            //return dt;
        }

        //查詢土地使用分區 - 單筆
        public DataTable GetUsageZone(string _LandType)
        {
            Dou.Models.DB.IModelEntity<LandUsageZoneCode> LandUsageZoneCode = new Dou.Models.DB.ModelEntity<LandUsageZoneCode>(dbContext);
            var landusagedata = LandUsageZoneCode.GetAll().Where(s => s.LandType == (int.Parse(_LandType) as int?))
                .OrderBy(x => x.Rank).Select(n => new
                {
                    Name = n.Name,
                    Value = n.Value
                })
                .ToArray();
            return ArrayToDataTable(landusagedata);
            //string sqlstr = string.Format(@"
            //                SELECT Name,Value
            //                FROM LandUsageZoneCode
            //                WHERE LandType = '{0}'
            //                ORDER BY RANK
            //                ", _LandType);
            //DataTable dt = Mei.DataAccess.ExecuteDataTable(sqlstr, Mei.DataAccess.ConnectionType._CienveGSLConnection);
            //return dt;
        }
        #endregion

        public static System.Data.DataTable ArrayToDataTable(Array array, bool headerQ = true)
        {
            if (array == null || array.GetLength(1) == 0 || array.GetLength(0) == 0) return null;
            System.Data.DataTable dt = new System.Data.DataTable();
            int dataRowStart = headerQ ? 1 : 0;

            // create columns
            for (int i = 1; i <= array.GetLength(1); i++)
            {
                var column = new DataColumn();
                string value = array.GetValue(1, i) is System.String
                    ? array.GetValue(1, i).ToString() : "Column" + i.ToString();

                column.ColumnName = value;
                dt.Columns.Add(column);
            }
            if (array.GetLength(0) == dataRowStart) return dt;  //array has no data

            //Note:  the array is 1-indexed (not 0-indexed)
            for (int i = dataRowStart + 1; i <= array.GetLength(0); i++)
            {
                // create a DataRow using .NewRow()
                DataRow row = dt.NewRow();

                // iterate over all columns to fill the row
                for (int j = 1; j <= array.GetLength(1); j++)
                {
                    row[j - 1] = array.GetValue(i, j);
                }

                // add the current row to the DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}