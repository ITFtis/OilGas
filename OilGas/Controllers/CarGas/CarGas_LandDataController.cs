using Dou.Controllers;
using OilGas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilGas.Controllers.CarGas
{
    [Dou.Misc.Attr.MenuDef(Id = "CarGas_LandData", Name = "已開業汽車加氣站分析統計報表", MenuPath = "加氣站/B統計報表專區", Action = "Index", Index = 7, Func = Dou.Misc.Attr.FuncEnum.None, AllowAnonymous = false)]
    public class CarGas_LandDataController : AGenericModelController<CarGas_LandData>
    {
        // GET: CarGas_LandData
        public ActionResult Index()
        {
            return View();
        }

        protected override Dou.Models.DB.IModelEntity<OilGas.Models.CarGas_LandData> GetModelEntity()
        {
            return new Dou.Models.DB.ModelEntity<OilGas.Models.CarGas_LandData>(new OilGasModelContextExt());
        }

        string _ddl_City = "";
        string _ModDate_Start_Between_ = "";
        string _ModDate_End_Between_ = "";
        List<string> _LandUsageZoneCode0 = null;
        List<string> _LandUsageZoneCode1 = null;
        List<string> _LandClassCode = null;
        string _CityCode = "";
        string _AreaCode = "";
        string _GSLCode = "";

        public ActionResult ExportCarGasLandDataView(params KeyValueParams[] paras)
        {
            string error = "";
            string url = "";
            string folder = FileHelper.GetFileFolder(Code.TempUploadFile.汽車加氣站_B統計報表專區_已開業汽車加氣站分析統計報表);
            string fileTitle = "汽車加氣站_統計報表專區_已開業汽車加氣站分析統計報表";
            List<string> titles = new List<string>() { "汽車加氣站_已開業汽車加氣站分析統計報表，查詢條件:" };

            _ddl_City = KeyValue.GetFilterParaValue(paras, "DateType");
            _ModDate_Start_Between_ = KeyValue.GetFilterParaValue(paras, "ModDate-Start-Between_");
            _ModDate_End_Between_ = KeyValue.GetFilterParaValue(paras, "ModDate-End-Between_");
            _LandUsageZoneCode0 = KeyValue.GetFilterParaValue(paras, "LandUsageZoneCode0").Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            _LandUsageZoneCode1 = KeyValue.GetFilterParaValue(paras, "LandUsageZoneCode1").Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            _LandClassCode = KeyValue.GetFilterParaValue(paras, "LandClassCode").Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            _CityCode = KeyValue.GetFilterParaValue(paras, "CityCode");
            _AreaCode = KeyValue.GetFilterParaValue(paras, "AreaCode");
            _GSLCode = _CityCode != "" ? Rpt_CarFuel_Land.GetGSLCodeByCityCode(_CityCode).First().GSLCode.ToString() : "";
            var ltrResults = getExcel();

            if (ltrResults == "")
            {
                return Json(new { result = false, errorMessage = "查無資料" }, JsonRequestBehavior.AllowGet); ;
            }
            //5.產出excel
            string fileName = OilGas.ExcelSpecHelper.GenerateExcelGrow(fileTitle, folder, ltrResults, "N");
            string path = folder + fileName;
            url = OilGas.Cm.PhysicalToUrl(path);

            if (url == "")
            {
                return Json(new { result = false, errorMessage = error }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = true, url = url }, JsonRequestBehavior.AllowGet);
            }
        }

        protected string getLevelOneRowSpan()
        {
            if (_LandUsageZoneCode1.Count() > 0 && _LandClassCode.Count() > 0)
                return "rowspan=2";
            return string.Empty;
        }

        protected string getExcel()
        {
            var getLUZC = Rpt_CarFuel_Land.GetAllLandUsageZoneCode();
            var getLCC = Rpt_CarFuel_Land.GetAllLandClassCode();
            var citydata = Rpt_CarFuel_Land.GetAllCityCode();
            var areadata = Rpt_CarFuel_Land.GetAllAreaCode();
            DataTable dt;
            dt = this.getData();

            #region 製作Excel Html
            if (dt != null && dt.Rows.Count > 0)
            {
                string cityname = _CityCode == "" ? "" : citydata.Where(s => s.CityCode1 == _CityCode).First().CityName.ToString();
                string areaname = _AreaCode == "" ? "" : areadata.Where(s => s.AreaCode1 == _AreaCode).First().AreaName.ToString();
                //報表名稱
                string ReportName = "已開業汽車加氣站分析統計報表";
                //加總
                string Total = "", strTotal = "";
                int sumA = 0, sumB = 0, sumC = 0;
                //查詢條件
                string QryString = string.Format(@"縣市別：{0},鄉鎮市區：{1}<br/>
                                               查詢期間：{2} <br/>"
                                                , cityname, areaname, string.Format("{0}~{1}", _ModDate_Start_Between_, _ModDate_End_Between_));

                //標頭
                string strSpan = "", strTitleLevelOne = "", strTitleLevelTwo = "", strTitleLevelThree = "", strTitleLevelFour = "";

                #region 都市計畫區
                if (_LandUsageZoneCode0.Count() > 0)
                {
                    strSpan = "rowspan=3";

                    int count = 0;
                    for (int i = 0; i < _LandUsageZoneCode0.Count(); i++)
                    {
                        string strVaule = _LandUsageZoneCode0[i];
                        string strText = getLUZC.Where(x => x.Value == strVaule).First().Name.ToString();
                        strTitleLevelThree += string.Format("<td colspan=2>{0}</td>", strText);
                        count++;
                    }
                    strTitleLevelThree += "<td rowspan=2>小計(A)</td>";
                    count++;

                    for (int i = 0; i < _LandUsageZoneCode0.Count(); i++)
                    {
                        strTitleLevelFour += "<td>中油</td><td>非中油</td>";
                    }
                    strTitleLevelOne += string.Format("<td colspan={0} {1}>都市計畫區</td>"
                                                        , count * 2 - 1
                                                        , getLevelOneRowSpan());

                    #region 處理加總
                    int[] array = new int[_LandUsageZoneCode0.Count()];
                    int[] array2 = new int[_LandUsageZoneCode0.Count()];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < _LandUsageZoneCode0.Count(); j++)
                            {
                                string strVaule = _LandUsageZoneCode0[j];
                                string strText = getLUZC.Where(x => x.Value == strVaule).First().Name.ToString();
                                array[j] += Convert.ToInt32(dt.Rows[i][strText + "_中油A"].ToString().Trim());
                                array2[j] += Convert.ToInt32(dt.Rows[i][strText + "_非中油A"].ToString().Trim());
                            }
                            sumA += Convert.ToInt32(dt.Rows[i]["小計A"].ToString().Trim());
                        }
                    }

                    for (int i = 0; i < array.Length; i++)
                    {
                        strTotal += "<td>" + array[i] + "</td><td>" + array2[i] + "</td>";
                    }
                    strTotal += "<td>" + sumA + "</td>";
                    #endregion
                }
                #endregion

                #region 非都市計畫區
                if (_LandUsageZoneCode1.Count() > 0 || _LandClassCode.Count() > 0)
                {
                    strSpan = "rowspan=3";
                    int count1 = 0;
                    int count2 = 0;
                    //土地使用分區
                    if (_LandUsageZoneCode1.Count() > 0)
                    {
                        for (int i = 0; i < _LandUsageZoneCode1.Count(); i++)
                        {
                            string strVaule = _LandUsageZoneCode1[i];
                            string strText = getLUZC.Where(x => x.Value == strVaule).First().Name.ToString();
                            strTitleLevelThree += string.Format("<td colspan=2>{0}</td>", strText);
                            count1++;
                        }
                        strTitleLevelThree += "<td rowspan=2>小計(B)</td>";
                        count1++;

                        for (int i = 0; i < _LandUsageZoneCode1.Count(); i++)
                        {
                            strTitleLevelFour += "<td>中油</td><td>非中油</td>";
                        }

                        #region 處理加總
                        int[] array = new int[_LandUsageZoneCode1.Count()];
                        int[] array2 = new int[_LandUsageZoneCode1.Count()];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                for (int j = 0; j < _LandUsageZoneCode1.Count(); j++)
                                {
                                    string strVaule = _LandUsageZoneCode1[j];
                                    string strText = getLUZC.Where(x => x.Value == strVaule).First().Name.ToString();
                                    array[j] += Convert.ToInt32(dt.Rows[i][strText + "_中油B"].ToString().Trim());
                                    array2[j] += Convert.ToInt32(dt.Rows[i][strText + "_非中油B"].ToString().Trim());

                                }
                                sumB += Convert.ToInt32(dt.Rows[i]["小計B"].ToString().Trim());
                            }
                        }

                        for (int i = 0; i < array.Length; i++)
                        {
                            strTotal += "<td>" + array[i] + "</td><td>" + array2[i] + "</td>";
                        }
                        strTotal += "<td>" + sumB + "</td>";
                        #endregion
                    }

                    //用地類別
                    if (_LandClassCode.Count() > 0)
                    {
                        for (int i = 0; i < _LandClassCode.Count(); i++)
                        {
                            string strVaule = _LandClassCode[i];
                            string strText = getLCC.Where(x => x.Value == strVaule).First().Name.ToString();
                            strTitleLevelThree += string.Format("<td colspan=2>{0}</td>", strText);
                            count2++;
                        }
                        strTitleLevelThree += "<td rowspan=2>小計(C)</td>";
                        count2++;

                        for (int i = 0; i < _LandClassCode.Count(); i++)
                        {
                            strTitleLevelFour += "<td>中油</td><td>非中油</td>";
                        }

                        #region 處理加總
                        int[] array = new int[_LandClassCode.Count()];
                        int[] array2 = new int[_LandClassCode.Count()];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                for (int j = 0; j < _LandClassCode.Count(); j++)
                                {
                                    string strVaule = _LandClassCode[j];
                                    string strText = getLCC.Where(x => x.Value == strVaule).First().Name.ToString();
                                    array[j] += Convert.ToInt32(dt.Rows[i][strText + "_中油C"].ToString().Trim());
                                    array2[j] += Convert.ToInt32(dt.Rows[i][strText + "_非中油C"].ToString().Trim());
                                }
                                sumC += Convert.ToInt32(dt.Rows[i]["小計C"].ToString().Trim());
                            }
                        }

                        for (int i = 0; i < array.Length; i++)
                        {
                            strTotal += "<td>" + array[i] + "</td><td>" + array2[i] + "</td>";
                        }
                        strTotal += "<td>" + sumC + "</td>";
                        #endregion
                    }

                    if (_LandUsageZoneCode1.Count() > 0 && _LandClassCode.Count() > 0)
                    {
                        strSpan = "rowspan=4";
                        strTitleLevelTwo = string.Format("<tr><td colspan={0}>土地使用分區</td><td colspan={1}>用地類別</td></tr>", count1 * 2 - 1, count2 * 2 - 1);
                        strTitleLevelOne += string.Format("<td colspan={0}>非都市計畫區</td>", (count1 * 2 - 1) + (count2 * 2 - 1));
                    }
                    else
                    {
                        if (_LandUsageZoneCode1.Count() > 0)
                            strTitleLevelOne += string.Format("<td colspan={0}>非都市計畫區</td>", (count1 * 2 - 1));
                        if (_LandClassCode.Count() > 0)
                            strTitleLevelOne += string.Format("<td colspan={0}>非都市計畫區</td>", (count2 * 2 - 1));
                    }

                    strTitleLevelOne += string.Format("<td {0}>總計</td>", strSpan);

                }
                #endregion

                //合計
                int sum = sumA + sumC;
                Total = "<tr><td>合計</td>";
                Total += strTotal;
                Total += "<td>" + sum + "</td></tr>";

                string Title = string.Format(@"<tr>
                                                 <td {0}>縣市別</td>
                                                 {1}
                                               </tr>
                                               {2}
                                               <tr>{3}</tr>
                                               <tr>{4}</tr>"
                                               , strSpan
                                               , strTitleLevelOne
                                               , strTitleLevelTwo
                                               , strTitleLevelThree
                                               , strTitleLevelFour);
                //欄位
                string[] arrField = new string[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    arrField[i] = dt.Columns[i].ColumnName;
                }

                return StatisticReportFunc.DownLoad_Excel(ref dt, ReportName, QryString, arrField, Title, Total, ReportName + DateTime.Now.ToString("yyyyMMddhhmmss"));
            }
            else
            {
                return "";
            }
            #endregion 製作Excel Html
        }

        private DataTable getData()
        {
            DataTable Alldt = null;
            if (_CityCode == "")
            {
                Alldt = getData("", "");
                //for (int i = 0; i < ddl_City.Items.Count; i++)
                //{
                //    if (Alldt == null)
                //    {
                //        Alldt = getData(ddl_City.Items[i].Value.Trim(), "");
                //    }
                //    else
                //    {
                //        Alldt.Merge(getData(ddl_City.Items[i].Value.Trim(), ""));
                //    }
                //}
            }
            else
            {
                if (_AreaCode == "")
                {
                    var lstAreaCode = Rpt_CarFuel_Land.GetAllAreaCode().Where(x => x.CityCode == _CityCode).ToList();
                    for (int i = 0; i < lstAreaCode.Count; i++)
                    {
                        if (Alldt == null)
                        {
                            Alldt = getData(_GSLCode, lstAreaCode[i].AreaCode1);
                        }
                        else
                        {
                            Alldt.Merge(getData(_GSLCode, lstAreaCode[i].AreaCode1));
                        }
                    }
                }
                else
                {
                    Alldt = getData(_GSLCode, _AreaCode);
                }
            }
            return Alldt;
        }

        private DataTable getData(string workCityStr, string workTownStr)
        {
            var getLUZC = Rpt_CarFuel_Land.GetAllLandUsageZoneCode();
            var getLCC = Rpt_CarFuel_Land.GetAllLandClassCode();
            string strSumField = "", strField = "", strIsLog = "", strWhere = "";
            string strSubtotalA = "", strSubtotalB = "", strSubtotalC = "", strTotal = "";
            #region 查詢條件
            if (_ddl_City == "1")
            {
                strIsLog = "_Log";
            }

            if (_ModDate_Start_Between_ != "")
            {
                strWhere += string.Format(" and cb.Mod_Date >='{0}'", _ModDate_Start_Between_);
            }

            if (_ModDate_End_Between_ != "")
            {
                strWhere += string.Format(" and cb.Mod_Date <='{0}'", _ModDate_End_Between_);
            }

            if (workCityStr != "")
            {
                strWhere += string.Format(" and RIGHT(LEFT(cb.CaseNo,6),2)  in ('{0}')", workCityStr.Replace(",", "','"));
            }

            if (workTownStr != "")
            {
                strWhere += string.Format(" and cb.ZipCode ='{0}'", workTownStr);
            }
            #endregion

            #region 顯示欄位
            #region 都市計畫區
            if (_LandUsageZoneCode0.Count() > 0)
            {
                for (int i = 0; i < _LandUsageZoneCode0.Count(); i++)
                {
                    string strVaule = _LandUsageZoneCode0[i];
                    string strText = getLUZC.Where(x => x.Value == strVaule).First().Name.ToString();
                    strField += string.Format(@",case when cb.LandUsageZone = '{0}' and Business_theme = '6' then 1 else 0 end  [{1}_中油A]
                                                ,case when cb.LandUsageZone = '{0}' and Business_theme <> '6' then 1 else 0 end  [{1}_非中油A]"
                                 , strVaule
                                 , strText);
                    strSumField += string.Format(@",SUM([{0}_中油A]) [{0}_中油A]
                                                   ,SUM([{0}_非中油A]) [{0}_非中油A]"
                                                        , strText);
                    strSubtotalA += string.Format(@"+[{0}_中油A]+[{0}_非中油A]", strText);
                }
                strTotal += strSubtotalA;
                strSubtotalA = strSubtotalA.Substring(1);
                strSubtotalA = ",SUM(" + strSubtotalA + ") [小計A]";
                strSumField += strSubtotalA;
            }
            #endregion

            #region 非都市計畫區
            if (_LandUsageZoneCode1.Count() > 0 || _LandClassCode.Count() > 0)
            {
                //土地使用分區
                if (_LandUsageZoneCode1.Count() > 0)
                {
                    for (int i = 0; i < _LandUsageZoneCode1.Count(); i++)
                    {
                        string strVaule = _LandUsageZoneCode1[i];
                        string strText = getLUZC.Where(x => x.Value == strVaule).First().Name.ToString();

                        strField += string.Format(@",case when cb.LandUsageZone = '{0}' and Business_theme = '6' then 1 else 0 end  [{1}_中油B]
                                                    ,case when cb.LandUsageZone = '{0}' and Business_theme <> '6' then 1 else 0 end  [{1}_非中油B]"
                                    , strVaule
                                    , strText);
                        strSumField += string.Format(@",SUM([{0}_中油B]) [{0}_中油B]
                                                       ,SUM([{0}_非中油B]) [{0}_非中油B]"
                                                            , strText);
                        strSubtotalB += string.Format(@"+[{0}_中油B]+[{0}_非中油B]", strText);
                    }
                    //strTotal += strSubtotalB; //同時有分區和類別的時候，會重複加總
                    strSubtotalB = strSubtotalB.Substring(1);
                    strSubtotalB = ",SUM(" + strSubtotalB + ") [小計B]";
                    strSumField += strSubtotalB;
                }
                //用地類別
                if (_LandClassCode.Count() > 0)
                {
                    for (int i = 0; i < _LandClassCode.Count(); i++)
                    {
                        string strVaule = _LandClassCode[i];
                        string strText = getLCC.Where(x => x.Value == strVaule).First().Name.ToString();

                        strField += string.Format(@",case when cb.LandClass = '{0}' and Business_theme = '6' then 1 else 0 end  [{1}_中油C]
                                                    ,case when cb.LandClass = '{0}' and Business_theme <> '6' then 1 else 0 end  [{1}_非中油C]"
                                    , strVaule
                                    , strText);
                        strSumField += string.Format(@",SUM([{0}_中油C]) [{0}_中油C]
                                                       ,SUM([{0}_非中油C]) [{0}_非中油C]"
                                                            , strText);
                        strSubtotalC += string.Format(@"+[{0}_中油C]+[{0}_非中油C]", strText);
                    }
                    strTotal += strSubtotalC;
                    strSubtotalC = strSubtotalC.Substring(1);
                    strSubtotalC = ",SUM(" + strSubtotalC + ") [小計C]";
                    strSumField += strSubtotalC;
                }
                if (strTotal.Length > 0)
                {
                    strTotal = strTotal.Substring(1);
                    strTotal = ",SUM(" + strTotal + ") [總計AC]";
                }
                strSumField += strTotal;
            }
            #endregion
            #endregion
            Rpt_CarFuel_Land.ResetGetAllLandUsageZoneCode();
            Rpt_CarFuel_Land.ResetGetAllLandClassCode();
            if (workCityStr == "")
            {
                string strSQL = string.Format(@"
                Select [縣市別] {0}
                From
                (
                    Select CityName [縣市別], cc.rank[CityRank] {1}
                    From CarGas_BasicData{2}
                        cb with(nolock)
                    Left Join UsageStateCode u On cb.UsageState = u.Value
                    Right Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(cb.CaseNo, 5, 2) + '%'
                    Where u.Type = '已開業' {3}
                ) A
                Group by [縣市別], CityRank
                Order by CityRank"
                   , strSumField
                   , strField
                   , strIsLog
                   , strWhere);
                //Response.Write(strSQL);
                DataTable dt = StatisticReportFunc.getDataTable(strSQL);
                return dt;
            }
            else
            {
                string strSQL = string.Format(@"
            Select [鄉鎮市區] {0}
            From
            (
                Select ac.AreaName [鄉鎮市區], cc.rank[CityRank] {1}
                From CarGas_BasicData{2}
                     cb with(nolock)
                Left Join UsageStateCode u On cb.UsageState = u.Value
                Right Join CityCode cc with(nolock) On GSLCode LIKE '%' + SUBSTRING(cb.CaseNo, 5, 2) + '%'
                Left Join AreaCode ac with(nolock) on cb.ZipCode=ac.AreaCode
                Where u.Type = '已開業' {3}
            ) A
            Group by [鄉鎮市區], CityRank
            Order by CityRank"
                , strSumField
                , strField
                , strIsLog
                , strWhere);
                //Response.Write(strSQL);
                DataTable dt = StatisticReportFunc.getDataTable(strSQL);
                return dt;
            }
        }
    }
}