using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using Dou.Misc.Attr;
using Microsoft.Ajax.Utilities;
using NPOI.SS.Formula.Functions;
using OilGas;
using OilGas.Controllers;
using OilGas.Models;

/// <summary>統計報表 相關函式</summary>
public class StatisticReportFunc
{
	/// <summary>累計 的 前綴詞</summary>
	public const string GrandTotal_Prefix = "Grand_";

	/// <summary>查詢結果表頭樣版</summary>
	public string resTblHeaderTemplate;

	#region NameItem [class:名稱項目]
	/// <summary>名稱項目</summary>
	public class NameItem
	{
		/// <summary>項目名稱</summary>
		public string Name;
		/// <summary>項目代碼</summary>
		public string Code;

		/// <summary>名稱項目</summary>
		/// <param name="name">項目名稱</param>
		/// <param name="code">項目代碼</param>
		public NameItem(object name, object code)
		{
			if (name != null) Name = name.ToString();
			if (code != null) Code = code.ToString();
		}
	}
	#endregion NameItem [class:名稱項目]

	#region getCityList [取得 縣市別 項目]
	/// <summary>取得 縣市別 項目</summary>
	/// <returns></returns>
	public static List<NameItem> getCityList()
	{
        basicController basic = new basicController();
        System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
		List<CityCode> citys = new List<CityCode>();
		List<NameItem> CityList = new List<NameItem>();
		Dou.Models.DB.IModelEntity<CityCode> cityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
		var allcityCode = cityCode.GetAll().ToArray();
        //不是ADMIN只能看自己
        if (!Dou.Context.CurrentIsAdminUser && !basic.Permissions("admin"))
        {
            allcityCode = allcityCode.Where(x => x.GSLCode == Dou.Context.CurrentUser<User>().city).ToArray();
        }

        foreach (var item in allcityCode.OrderBy(x => x.Rank).ToList())
		{
			string[] strs = item.GSLCode.Split(',');
			if (strs.Count() > 1)
			{
				CityCode city = item;
				foreach (string str in strs)
				{
					city = item.Clone();

					city.GSLCode = str;
					citys.Add(city);
					CityList.Add(new NameItem(city.CityName, city.CityCode1));
				}
			}
			else
			{
				citys.Add(item);
				CityList.Add(new NameItem(item.CityName, item.CityCode1));
			}
		}

        return CityList.DistinctBy(x => new { x.Name, x.Code }).ToList();
	}
	#endregion

	#region GetDBTableName [依 子系統類別 傳回 資料表名稱]
	/// <summary>依 子系統類別 傳回 資料表名稱</summary>
	/// <param name="type">子系統類別</param>
	/// <returns></returns>
	public static string GetDBTableName(OilGas.Code.SubSystemType type)
	{
		switch (type)
		{
			case OilGas.Code.SubSystemType.CarFuel: return "CarFuel_BasicData";
			case OilGas.Code.SubSystemType.CarGas: return "CarGas_BasicData";
			case OilGas.Code.SubSystemType.FishGas: return "FishGas_BasicData";
			default: return string.Empty;
		}
	}
	#endregion

	#region GetSystemTitle [依 子系統類別 傳回 類別名稱]
	/// <summary>依 子系統類別 傳回 類別名稱</summary>
	/// <param name="type">子系統類別</param>
	/// <returns></returns>
	public static string GetSystemTitle(OilGas.Code.SubSystemType type)
	{
		switch (type)
		{
			case OilGas.Code.SubSystemType.CarFuel: return "汽、機車加油站";
			case OilGas.Code.SubSystemType.CarGas: return "汽車加氣站";
			case OilGas.Code.SubSystemType.FishGas: return "漁船加油站";
			default: return string.Empty;
		}
	}
	#endregion

	#region >> 年度成長分析 報表 相關
	#region GetReportDateMinYearAndMaxYear [取得 指定年度區間 的 最小、最大 有資料年度]
	/// <summary>取得 指定年度區間 的 最小、最大 有資料年度</summary>
	/// <param name="type">子系統類別</param>
	/// <param name="qSY">查詢 開始 年度</param>
	/// <param name="qEY">查詢 終止 年度</param>
	/// <param name="minY">最小有資料年度</param>
	/// <param name="maxY">最大有資料年度</param>
	public static void GetReportDateMinYearAndMaxYear(OilGas.Code.SubSystemType type, string qSY, string qEY, out int minY, out int maxY)
	{
		if (!int.TryParse(qSY, out minY)) minY = DateTime.Today.Year - 5;
		//else minY += 1911;
		if (!int.TryParse(qEY, out maxY)) maxY = DateTime.Today.Year;
		//else maxY += 1911;
	}
	#endregion GetReportDateMinYearAndMaxYear [取得 指定年度區間 的 最小、最大 有資料年度]

	#region GetResultsTblHtml [取得 特定年度 查詢結果 HTML Table]
	/// <summary>取得 特定年度 查詢結果 HTML Table</summary>
	/// <param name="type">子系統類別</param>
	/// <param name="year">查詢年度</param>
	/// <param name="qryCityCode">查詢縣市代碼，空值則不限縣市</param>
	/// <returns></returns>
	public string GetResultsTblHtml(OilGas.Code.SubSystemType type, int year, string qryCityCode)
	{
		List<NameItem> BusOrgList = OilGas.Code.getBusOrgList();
        #region 設定 查詢結果表頭樣版 => resTblHeaderTemplate
        if (string.IsNullOrEmpty(resTblHeaderTemplate))
		{
			StringBuilder sbHeader = new StringBuilder();

			#region 設定 表頭 1
			sbHeader.AppendFormat(@"
			<tr class=""gv_thead""><th rowspan=""3"">縣市<br />別</th><th colspan=""{0}"">{{0}}年度{{1}}成長分析</th></tr>"
				, (BusOrgList.Count * 2) + 2);
			#endregion

			#region 設定 表頭 2
			sbHeader.Append(@"
			<tr class=""trH2"">");
			foreach (NameItem ni in BusOrgList)
				sbHeader.AppendFormat(@"<th colspan=""2"">{0}</th>", ni.Name);
			sbHeader.Append(@"<th colspan=""2"">總計</th>
			</tr>");
			#endregion

			#region 設定 表頭 3
			string _th3Txt = "<th>本<br />年<br />度</th><th>累<br />計</th>";
			sbHeader.Append(@"
			<tr>");
			for (int i = 0; i < BusOrgList.Count; i++)
				sbHeader.Append(_th3Txt);
			sbHeader.AppendFormat(@"{0}
			</tr>", _th3Txt);
			#endregion

			resTblHeaderTemplate = sbHeader.ToString();
		}
		#endregion 設定 查詢結果表頭樣版

		#region 設定 查詢結果內容 => sbBody
		StringBuilder sbBody = new StringBuilder();
		Dictionary<string, int> GrowData = getGrowData(type, year, qryCityCode);
        List<NameItem> CityList = getCityList();

		string _tdCss = "gv_tbody", _key, _gkey;
		foreach (NameItem niC in CityList)
		{
			sbBody.AppendFormat(@"
			<tr class=""trR""><td class=""{0} td1"">{1}</td>"
				, _tdCss, niC.Name);
			foreach (NameItem niO in BusOrgList)
			{
				_key = niC.Code + "_" + niO.Code;
				_gkey = GrandTotal_Prefix + _key;
				sbBody.AppendFormat(@"<td class=""{0}"">{1}</td><td class=""{0}"">{2}</td>"
					, _tdCss
					, GrowData.ContainsKey(_key) ? GrowData[_key] : 0
					, GrowData.ContainsKey(_gkey) ? GrowData[_gkey] : 0
					);
			}
			_key = niC.Code + "_ALL";
			_gkey = GrandTotal_Prefix + _key;
			sbBody.AppendFormat(@"<td class=""{0}"">{1}</td><td class=""{0}"">{2}</td></tr>"
				, _tdCss
				, GrowData.ContainsKey(_key) ? GrowData[_key] : 0
				, GrowData.ContainsKey(_gkey) ? GrowData[_gkey] : 0
				);
		}

		sbBody.AppendFormat(@"
			<tr class=""trR""><td class=""{0} td1"">{1}</td>"
			, _tdCss, "總計");
		foreach (NameItem niO in BusOrgList)
		{
			_key = "ALL_" + niO.Code;
			_gkey = GrandTotal_Prefix + _key;
			sbBody.AppendFormat(@"<td class=""{0}"">{1}</td><td class=""{0}"">{2}</td>"
				, _tdCss
				, GrowData.ContainsKey(_key) ? GrowData[_key] : 0
				, GrowData.ContainsKey(_gkey) ? GrowData[_gkey] : 0
				);
		}
		_key = "ALL_ALL";
		_gkey = GrandTotal_Prefix + _key;
		sbBody.AppendFormat(@"<td class=""{0}"">{1}</td><td class=""{0}"">{2}</td></tr>"
			, _tdCss
			, GrowData.ContainsKey(_key) ? GrowData[_key] : 0
			, GrowData.ContainsKey(_gkey) ? GrowData[_gkey] : 0
			);
		#endregion 設定 查詢結果內容 => sbBody

		return string.Format(@"
	<table class=""formx2 tbl1"">
		<thead>
{0}
		</thead>
		<tbody>
{1}
		</tbody>
	</table>"
			, string.Format(resTblHeaderTemplate, year - 1911, GetSystemTitle(type))
			, sbBody.ToString()
			);
	}
	#endregion GetResultsTblHtml [取得 特定年度 查詢結果 HTML Table]

	#region getGrowData [取得特定年度成長資料]
	/// <summary>取得特定年度成長資料</summary>
	/// <param name="type">子系統類別</param>
	/// <param name="year">查詢年度</param>
	/// <param name="qryCityCode">查詢縣市代碼，空值則不限縣市</param>
	public static Dictionary<string, int> getGrowData(OilGas.Code.SubSystemType type, int year, string qryCityCode)
	{
		Dictionary<string, int> GrowData = new Dictionary<string, int>();
		string _TableName = GetDBTableName(type);  //CarGas_BasicData
		//權限查詢
        var pCitys = Dou.Context.CurrentUser<User>().PowerCitysCodes();
		if (pCitys.Count > 0)
		{
            foreach (string s in pCitys)
            {
                qryCityCode += string.Format("'{0}',", s);
            }
			qryCityCode = qryCityCode.Substring(0, qryCityCode.Length - 1);
        }

        string _cityWhr = string.IsNullOrEmpty(qryCityCode) ? string.Empty : " AND b.CityCode in (" + qryCityCode + ")";
		string _grandTotalSql = string.Format(@"
SELECT b.CityCode, c.Value, COUNT(*) [Cnt]
FROM {0} a
JOIN CityCode b ON b.GSLCode LIKE '%' + SUBSTRING(a.CaseNo, 5, 2) + '%'
JOIN CarVehicleGas_BusinessOrganization c ON c.Value = a.Business_theme
WHERE YEAR(a.Report_date) <= {1}{2}
GROUP BY b.CityCode, c.Value"
			, _TableName, year, _cityWhr);

		//System.Data.Entity.DbContext dbContext = new OilGasModelContextExt();
		//Dou.Models.DB.IModelEntity<CarGas_BasicData> CarGas_BasicData = new Dou.Models.DB.ModelEntity<CarGas_BasicData>(dbContext);
		//var cdata = CarGas_BasicData.GetAll().ToArray();
		//Dou.Models.DB.IModelEntity<CityCode> CityCode = new Dou.Models.DB.ModelEntity<CityCode>(dbContext);
		//var citydata = CityCode.GetAll().OrderBy(a => a.Rank).ToArray();
		//Dou.Models.DB.IModelEntity<CarVehicleGas_BusinessOrganization> BusOrg = new Dou.Models.DB.ModelEntity<CarVehicleGas_BusinessOrganization>(dbContext);
		//var busorgdata = BusOrg.GetAll().OrderBy(a => a.Rank).ToArray();
		//var query1 = (from a in cdata
		//			  join c in citydata on a.CaseNo.Substring(5, 2) equals c.GSLCode
		//			  join d in busorgdata on a.Business_theme equals d.Value
		//			  where int.Parse(a.Report_date.Value.ToString("yyyy")) <= year
		//			  select new
		//			  {
		//				  CityCode = c.CityCode1,
		//				  Value = d.Value,
		//			  });
		//var query1 = (from a in cdata
		//                    from b in citydata
		//                   from c in busorgdata
		//			 where int.Parse(a.Report_date.Value.Year.ToString()) <= 1980 &&
		//			 b.GSLCode.Contains(a.CaseNo.Substring(5, 2)) && a.Business_theme == c.Value

		//                   select new
		//			 {
		//				 CityCode = b.CityCode1,
		//				 Value = c.Value,
		//			 }).ToArray();
		// var gbquery = (from el in query1
		//group el by new { el.CityCode, el.Value } into g
		//select new 
		//                  {
		//                      year = g.Key,
		//                      counts = g.Count()
		//                  }).ToArray();



		BindGrowData(_grandTotalSql, GrandTotal_Prefix, ref GrowData);

		string _sql = string.Format(@"
SELECT b.CityCode, c.Value, COUNT(*) [Cnt]
FROM {0} a
JOIN CityCode b ON b.GSLCode LIKE '%' + SUBSTRING(a.CaseNo, 5, 2) + '%'
JOIN CarVehicleGas_BusinessOrganization c ON c.Value = a.Business_theme
WHERE YEAR(a.Report_date) = {1}{2}
GROUP BY b.CityCode, c.Value"
			, _TableName, year, _cityWhr);
		BindGrowData(_sql, string.Empty, ref GrowData);

		return GrowData;
	}
	#endregion getGrowData [取得特定年度成長資料]

	#region BindGrowData [繫結 成長資料]
	/// <summary>繫結 成長資料</summary>
	/// <param name="sql">繫結語法</param>
	/// <param name="prefix">前綴詞</param>
	/// <param name="growData">成長資料</param>
	public static void BindGrowData(string sql, string prefix, ref Dictionary<string, int> growData)
	{
		string allAllKey = prefix + "ALL_ALL";
		Dictionary<string, int> GrowData = new Dictionary<string, int>();

		DataTable dt = StatisticReportFunc.getDataTable(sql);
		if (dt.Rows.Count > 0)
			growData.Add(allAllKey, 0); //總總計

		foreach (DataRow dr in dt.Rows)
		{
			string cCode = dr["CityCode"].ToString();
			string oCode = dr["Value"].ToString();
			int val = Convert.ToInt32(dr["Cnt"]);

			string unitKey = string.Format("{0}{1}_{2}", prefix, cCode, oCode);
			string allCityKey = string.Format("{0}{1}_ALL", prefix, cCode);
			string allOrgKey = string.Format("{0}ALL_{1}", prefix, oCode);

			growData.Add(unitKey, val);
			growData[allAllKey] += val;

			//縣市總計加總
			if (growData.ContainsKey(allCityKey))
				growData[allCityKey] += val;
			else
				growData.Add(allCityKey, val);

			//營業主體總計加總
			if (growData.ContainsKey(allOrgKey))
				growData[allOrgKey] += val;
			else
				growData.Add(allOrgKey, val);
		}
	}
	#endregion BindGrowData [繫結 成長資料]

	#region GetGrowExcelFile [下載 成長資料 Excel 檔]
	/// <summary>下載 成長資料 Excel 檔</summary>
	/// <param name="sst">子系統類別</param>
	/// <param name="results">查詢結果 (HTML Table)</param>
	public static void GetGrowExcelFile(OilGas.Code.SubSystemType sst, string results)
	{
		string title = string.Format("年度{0}成長分析", StatisticReportFunc.GetSystemTitle(sst));
		string FileName = System.Web.HttpUtility.UrlEncode(title, System.Text.Encoding.UTF8);

		System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
		response.Clear();
		response.ContentType = "application/vnd.ms-excel";
		response.AppendHeader("Content-Disposition", "attachment;filename=" + FileName + ".xls");   //另定檔名用

		using (System.IO.StringWriter sw = new System.IO.StringWriter())
		{
			System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

			response.Write(string.Format(@"
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />
<style type=""text/css"">
td {{ mso-number-format:""\@""; }}
.gv_thead {{ background-color:#91b44a; }}
.gv_thead, .trH2 {{ height:30px; }}
table.formx2 th {{ line-height:normal; }}
table.tbl1 {{ margin-bottom:20px; }}
table.tbl1 td {{ line-height:normal; }}
table.tbl1 {{ border-collapse:collapse; width:100%; }}
.trH2 th, .td1 {{ white-space:nowrap; }}
.trR {{ height:26px; }}
</style>
</head>
{0}
</html>"
				, results
					.Replace("<table class=\"formx2 tbl1\">", "<table border=\"1\" class=\"formx2 tbl1\">")
					.Replace("</table>", "</table><table></table>")
				));
		}
		response.End();
	}
	#endregion GetGrowExcelFile [下載 成長資料 Excel 檔]
	#endregion >> 年度成長分析 報表 相關

	#region "下載EXCEL"
	/// <summary>
	/// 下載EXCEL(帶入合併儲存格之標頭)
	/// </summary>
	/// <param name="dt">資料來源DataTable</param>
	/// <param name="report_name">報表名稱</param>
	/// <param name="qry">查詢條件</param>
	/// <param name="field">欄位名稱1維陣列[(資料來源欄位英文名稱)]例:{{"ColumnNameA"}}</param>
	/// <param name="titles">帶入合併儲存格之欄位名稱標頭格式</param>
	/// <param name="total">總計</param>
	/// <param name="file_name">檔案名稱</param>
	public static string DownLoad_Excel(ref DataTable dt, string report_name, string qry, string[] field, string titles, string total, string file_name)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		string FileName = string.Empty;

		#region 判定用戶端瀏覽器版本決定檔名編碼方式
		//if (HttpContext.Current.Request.Browser.Browser == "IE")
		//{
		//    FileName = System.Web.HttpUtility.UrlEncode(file_name + ".xls", System.Text.Encoding.UTF8);
		//}
		//else
		//{
		//    FileName = file_name + ".xls";
		//}
		FileName = HttpUtility.UrlEncode(file_name, System.Text.Encoding.UTF8) + ".xls";
		#endregion
		//HttpContext oContext = HttpContext.Current;
		//HttpResponse oResponse = HttpContext.Current.Response;

		//oResponse.Clear();
		//oResponse.AddHeader("content-disposition", "attachment;filename=" + FileName);
		//oResponse.ContentType = "application/vnd.ms-excel";
		sb.Append("<html><head>" +
				  "<meta http-equiv=\"Content-Type\" content=\"application/vnd.ms-excel; charset=utf-8\">" +
				  "</head><body>");
		sb.Append("<table style=\"border-collapse:collapse;\" border=\"1\">");

		sb.Append("<tr><td colspan=\"" + field.GetLength(0).ToString() + "\">" + report_name + "</td></tr>");
		if (report_name != "各年度查核結果等級分布統計表")
			sb.Append("<tr><td colspan=\"" + field.GetLength(0).ToString() + "\">" + "查詢條件：" + qry + "</td></tr>");
		//sb.Append("<tr><td colspan=\"" + field.GetLength(0).ToString() + "\">" + "製表時間：" + DateTime.Now + "</td></tr>");

		sb.Append(titles);

		for (int i = 0; i < dt.Rows.Count; i++)
		{

			sb.Append("<tr>");
			for (int j = 0; j < field.GetLength(0); j++)
			{
				sb.Append("<td>" +
					System.Web.HttpUtility.HtmlEncode(string.Format("{0}", dt.Rows[i][field[j]])) +
					"</td>");
			}
			sb.Append("</tr>");
		}
		if (total != "")
		{
			sb.Append(total);
		}
		sb.Append("</table></body></html>");
		return sb.ToString();
		//oResponse.Write(sb.ToString());
		//oResponse.End();
	}
	#endregion

	#region >> 申請設置案件同意認定／籌建到期報表 相關
	#region GetConsentOrExpirationSql [取得 申請設置案件同意認定／籌建到期報表 查詢語法]
	/// <summary>取得 申請設置案件同意認定／籌建到期報表 查詢語法</summary>
	/// <param name="sst">子系統類別</param>
	/// <param name="GSLCode">加油站縣市代碼</param>
	/// <param name="usageState">營運狀況</param>
	/// <param name="expireDateStart">到期日期-起</param>
	/// <param name="expireDateEnd">到期日期-迄</param>
	/// <returns></returns>
	public static string GetConsentOrExpirationSql(OilGas.Code.SubSystemType sst, string GSLCode, string usageState, string expireDateStart, string expireDateEnd)
	{
		#region 設定 縣市 查詢條件 => _cityWhr
		string _cityWhr = string.IsNullOrEmpty(GSLCode)
			? string.Empty
			: string.Format(" AND b.GSLCode = '{0}'", GSLCode);
		#endregion

		#region 設定 營運狀況 查詢條件 => _stateWhr
		string _stateWhr;
		switch (usageState)
		{
			case "1": _stateWhr = " AND a.UsageState = '2'"; break;
			case "2": _stateWhr = " AND a.UsageState IN ('8', '9')"; break;
			default: _stateWhr = string.Empty; break;
		}
		#endregion

		#region 設定 到期日期 查詢條件 => _expireWhr
		string _expireWhr = "";
		bool hasEDS = !string.IsNullOrEmpty(expireDateStart);
		bool hasEDE = !string.IsNullOrEmpty(expireDateEnd);
		if (hasEDS && hasEDE)
		{
			_expireWhr = string.Format(" AND (COALESCE(Build_Deadline,AgreeDate,ExtensionDateEnd1) BETWEEN '{0}' AND '{1}')", expireDateStart, expireDateEnd);
		}
		else if (hasEDS)
		{
			_expireWhr = string.Format(" AND COALESCE(Build_Deadline,AgreeDate,ExtensionDateEnd1) >= '{0}'", expireDateStart);
		}
		else if (hasEDE)
		{
			_expireWhr = string.Format(" AND COALESCE(Build_Deadline,AgreeDate,ExtensionDateEnd1) <= '{0}'", expireDateEnd);
		}
		#endregion

		string _sqlTemplate;
		switch (sst)
		{
			case OilGas.Code.SubSystemType.CarFuel:
				_sqlTemplate = @"
SELECT a.CaseNo, b.CityName, CONVERT(CHAR(10), a.Recipient_date, 111) as Recipient_date
	, a.Gas_Name, REPLACE(REPLACE(c.Name,'申請設置-',''),'經營管理-','') as Name_Operations, CONVERT(CHAR(10), d.Dispatch_date, 111) as Dispatch_date
    , CONVERT(CHAR(10), COALESCE(Build_Deadline,AgreeDate,ExtensionDateEnd1) , 111) as Build_Deadline
	, Name_LandUse = (CASE a.LandType WHEN 0 THEN '都市計畫地區' ELSE '非都市計畫地區' END) + ISNULL('-' + e.Name, '')
    , case lcc.Name when '其他' then lcc.Name + a.OtherLandClass else lcc.Name End as Name_LandType
FROM {0} a
JOIN CityCode b ON b.GSLCode LIKE '%' + SUBSTRING(a.CaseNo, 5, 2) + '%'
LEFT JOIN UsageStateCode c ON c.Value = a.UsageState
LEFT JOIN CarFuel_Dispatch d ON d.CaseNo = a.CaseNo and DispatchClass = UsageState
LEFT JOIN LandUsageZoneCode e ON e.Value = a.LandUsageZone
LEFT JOIN LandClassCode lcc on lcc.Value = a.LandClass
WHERE ((UsageState = '8' and GetDate() > Build_Deadline) or (UsageState = '9' and getDate() > ExtensionDateEnd1) or (UsageState= '2' and getDate() > AgreeDate)){1}{2}{3}
";
				break;

			case OilGas.Code.SubSystemType.CarGas:
				_sqlTemplate = @"
SELECT a.CaseNo, b.CityName, CONVERT(CHAR(10), a.Recipient_date, 111) as Recipient_date
	, a.Gas_Name, REPLACE(REPLACE(c.Name,'申請設置-',''),'經營管理-','') as Name_Operations, CONVERT(CHAR(10), d.Dispatch_date, 111) as Dispatch_date
	, CONVERT(CHAR(10), COALESCE(Build_Deadline,ExtensionDateEnd1) , 111) as Build_Deadline
    , e.Name as Name_LandUse
    , case lcc.Name when '其他' then lcc.Name + a.OtherLandClass else lcc.Name End Name_LandType
FROM {0} a
JOIN CityCode b ON b.GSLCode LIKE '%' + SUBSTRING(a.CaseNo, 5, 2) + '%'
LEFT JOIN UsageStateCode c ON c.Value = a.UsageState
LEFT JOIN CarGas_Dispatch d ON d.CaseNo = a.CaseNo and DispatchClass = UsageState
LEFT JOIN LandUsageZoneCode e ON e.Value = a.LandUsageZone
LEFT JOIN LandClassCode lcc on lcc.Value = a.LandClass
WHERE ((UsageState = '8' and GetDate() > Build_Deadline) or (UsageState = '9' and getDate() > ExtensionDateEnd1) or (UsageState= '2' and getDate() > AgreeDate)){1}{2}{3}
";
				break;

			case OilGas.Code.SubSystemType.FishGas:
				_sqlTemplate = @"
SELECT a.CaseNo, b.CityName, CONVERT(CHAR(10), a.Recipient_date, 111) as Recipient_date
	, a.Gas_Name, REPLACE(REPLACE(c.Name,'申請設置-',''),'經營管理-','') as Name_Operations, CONVERT(CHAR(10), d.Dispatch_date, 111) as Dispatch_date
	, CONVERT(CHAR(10), COALESCE(Build_Deadline,AgreeDate,ExtensionDateEnd1) , 111) as Build_Deadline
    , e.Name as Name_LandUse
    , case lcc.Name when '其他' then lcc.Name + a.OtherLandClass else lcc.Name End as Name_LandType
FROM {0} a
JOIN CityCode b ON b.GSLCode LIKE '%' + SUBSTRING(a.CaseNo, 5, 2) + '%'
LEFT JOIN UsageStateCode c ON c.Value = a.UsageState
LEFT JOIN FishGas_Dispatch d ON d.CaseNo = a.CaseNo and DispatchClass = UsageState
LEFT JOIN LandUsageZoneCode e ON e.Value = a.LandUsageZone
LEFT JOIN LandClassCode lcc on lcc.Value = a.LandClass
WHERE ((UsageState = '8' and GetDate() > Build_Deadline) or (UsageState = '9' and getDate() > ExtensionDateEnd1) or (UsageState= '2' and getDate() > AgreeDate)){1}{2}{3}
";
				break;
			default:
				_sqlTemplate = @"
SELECT a.CaseNo, b.CityName, CONVERT(CHAR(10), a.Recipient_date, 111) as Recipient_date
	, a.Gas_Name, REPLACE(REPLACE(c.Name,'申請設置-',''),'經營管理-','') as Name_Operations, CONVERT(CHAR(10), d.Dispatch_date, 111) as Dispatch_date
    , CONVERT(CHAR(10), COALESCE(Build_Deadline,AgreeDate,ExtensionDateEnd1) , 111) as Build_Deadline
	, Name_LandUse = (CASE a.LandType WHEN 0 THEN '都市計畫地區' ELSE '非都市計畫地區' END) + ISNULL('-' + e.Name, '')
    , case lcc.Name when '其他' then lcc.Name + a.OtherLandClass else lcc.Name End as Name_LandType
FROM {0} a
JOIN CityCode b ON b.GSLCode LIKE '%' + SUBSTRING(a.CaseNo, 5, 2) + '%'
LEFT JOIN UsageStateCode c ON c.Value = a.UsageState
LEFT JOIN CarFuel_Dispatch d ON d.CaseNo = a.CaseNo and DispatchClass = UsageState
LEFT JOIN LandUsageZoneCode e ON e.Value = a.LandUsageZone
LEFT JOIN LandClassCode lcc on lcc.Value = a.LandClass
WHERE ((UsageState = '8' and GetDate() > Build_Deadline) or (UsageState = '9' and getDate() > ExtensionDateEnd1) or (UsageState= '2' and getDate() > AgreeDate)){1}{2}{3}
";
				break;
		}

		string _sql = string.Format(_sqlTemplate
			, StatisticReportFunc.GetDBTableName(sst)
			, _cityWhr
			, _stateWhr
			, _expireWhr
			);
		return _sql;
	}
	#endregion GetConsentOrExpirationSql [取得 申請設置案件同意認定／籌建到期報表 查詢語法]
	#endregion >> 申請設置案件同意認定／籌建到期報表 相關

	#region getDataTable [取得 DataTable]
	/// <summary>取得 DataTable</summary>
	/// <param name="sql">查詢語法</param>
	/// <returns></returns>
	public static DataTable getDataTable(string sql)
	{
		var connstring = ConfigurationManager.ConnectionStrings["OilGasModelContextExt"].ConnectionString;
		SqlConnection conn = new SqlConnection(connstring);
		SqlCommand cmd = new SqlCommand(sql, conn);
		conn.Open();
		SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		DataTable dt = new DataTable();
		dt.Load(dr); ;
		return dt;
	}
	#endregion

	#region LIST轉換DataTable [取得 DataTable]
	/// <summary>
	/// LIST轉換DataTable
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="data"></param>
	/// <returns></returns>
	public static DataTable ConvertToDataTable<T>(IList<T> data)
	{
		PropertyDescriptorCollection properties =
		   TypeDescriptor.GetProperties(typeof(T));
		DataTable table = new DataTable();
		foreach (PropertyDescriptor prop in properties)
			table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
		foreach (T item in data)
		{
			DataRow row = table.NewRow();
			foreach (PropertyDescriptor prop in properties)
				row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
			table.Rows.Add(row);
		}
		return table;

	}
	#endregion

	#region DataTable轉換LIST [取得 LIST]
	/// <summary>
	/// DataTable轉換LIST
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="data"></param>
	/// <returns></returns>
	public static List<T> ConvertToList<T>(DataTable dt)
	{
		var columnNames = dt.Columns.Cast<DataColumn>()
				.Select(c => c.ColumnName)
				.ToList();
		var properties = typeof(T).GetProperties();
		return dt.AsEnumerable().Select(row =>
		{
			var objT = Activator.CreateInstance<T>();
			foreach (var pro in properties)
			{
				if (columnNames.Contains(pro.Name))
				{
					PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
					//pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
					pro.SetValue(objT, (row[pro.Name] == DBNull.Value ? null : ChangeType(row[pro.Name], pI.PropertyType)), null);
				}
			}
			return objT;
		}).ToList();
	}

	//轉換為可為 Null 的型別
	public static object ChangeType(object value, Type conversion)
	{
		var t = conversion;

		if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
		{
			if (value == null)
			{
				return null;
			}

			t = Nullable.GetUnderlyingType(t);
		}

		return Convert.ChangeType(value, t);
	}
    #endregion
}
/// <summary>
/// 明細用參數
/// </summary>
public class workParas
{
    public string workCity { get; set; }
    public string workYear { get; set; }
    public string workType { get; set; }
    public string titleName { get; set; }
}
public class YearlyData
{
    public string year { get; set; }
    public int counts { get; set; }
}
public class YearlyGrowData
{
    public string year { get; set; }
    public double rate { get; set; }
}