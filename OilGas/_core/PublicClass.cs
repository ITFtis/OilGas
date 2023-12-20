using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// PublicClass 共用到的函式或方法
/// </summary>


namespace OilGas
{
    public class PublicClass
    {

        public const string GlobalSystemName = "中華民國環境工程學會";    //系統名稱
        public const string GlobalVision = "1.0.0";  //版本
        public const string DevelopCompany = "環資國際有限公司"; //研發公司
        
        #region <隱碼攻擊參數過濾>
        /// <summary>
        ///  SQL Injection Filter
        /// </summary>
        /// <param name="SQLValue">SQL參數</param>
        /// <returns>不含隱碼攻擊參數</returns>
        public static string ValueFilter(string SQLValue)
        {
            if (SQLValue != null)
            {
                SQLValue = Regex.Replace(SQLValue, @"\b(exec(ute)?|select|update|insert|delete|drop|create)\b|[;']|(-{2})|(/\*.*\*/)", string.Empty, RegexOptions.IgnoreCase);
            }
            else
            {
                SQLValue = "";
            }
            return SQLValue;
        }

        #endregion

        #region <自動產生編號>
        /// <summary>
        ///  自動產生編號
        /// </summary>
        /// <returns>PYYY</returns>
        //public static string CaseNoid()
        //{
        //    UserBasicInfo.UserBasicInfo user = new UserBasicInfo.UserBasicInfo();
        //    DateTime objDate = DateTime.Now;
        //    int objYearTemp = objDate.Year - 1911;
        //    String objYear = objYearTemp.ToString();

        //    if (objYear.Length < 3)
        //    {
        //        objYear = "0" + objYear;
        //    }
        //    if (objYear.Length < 2)
        //        objYear = "00" + objYear;

        //    return "P" + objYear + user.UserLv + user.UserLv;
        //}
        #endregion

        #region <讀取ClientIP>
        /// <summary>
        ///  讀取ClientIP
        /// </summary>
        /// <returns>192.168.10.1</returns>
        public string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
        #endregion

        #region <控制項繫節資料>
        /// <summary>
        /// 控制項繫節資料
        /// </summary>
        public void ControlBoundData(ListControl listCtl,DataTable dt,string text,string value)
        {
            listCtl.DataSource = dt;
            listCtl.DataTextField = text;
            listCtl.DataValueField = value;
            listCtl.DataBind();
        }
        /// <summary>
        /// 控制項繫節資料
        /// </summary>
        /// <param name="listCtl">繫結資料的控制項</param>
        /// <param name="dt">資料來源</param>
        public void ControlBoundData(ListControl listCtl, DataTable dt)
        {
            ControlBoundData(listCtl, dt, "");
        }
        /// <summary>
        /// 控制項繫節資料
        /// </summary>
        /// <param name="listCtl">繫結資料的控制項</param>
        /// <param name="dt">資料來源</param>
        /// <param name="value">第一項文字</param>
        public void ControlBoundData(ListControl listCtl, DataTable dt, string value)
        {
            listCtl.AppendDataBoundItems = true;

            ListItem item = new ListItem(value,string.Empty);
            listCtl.Items.Add(item);

            listCtl.DataSource = dt;
            listCtl.DataTextField = dt.Columns[0].ToString();
            listCtl.DataValueField = dt.Columns[1].ToString();
            listCtl.DataBind();

        }
        #endregion

        #region <設定控制項為ReadOnly>
        /// <summary>
        /// 設定控制項為ReadOnly
        /// </summary>
        public void ControlToReadOnly(WebControl webCtl)
        {
            webCtl.Attributes.Add("ReadOnly","true");
        }
        #endregion

        #region 蒐集勾選的核取方塊Value值
        /// <summary>
        /// 蒐集勾選的核取方塊Value值
        /// </summary>
        public string[] GetCheckBoxValue(ListControl listCtl)
        {
            List<string> columnList = new List<string>();

            foreach (ListItem Item in listCtl.Items)
            {
                if (Item.Selected)
                    columnList.Add(Item.Value);
            }
            string[] colAry = columnList.ToArray();
            return colAry;
        }
		#endregion	
		#region 蒐集勾選的核取方塊Value值(組成字串)
		/// <summary>
		/// 蒐集勾選的核取方塊Value值(組成字串)
		/// </summary>
		public string JoinCheckBoxValue(ListControl listCtl,string sign)
		{
			List<string> columnList = new List<string>();

			foreach (ListItem Item in listCtl.Items)
			{
				if (Item.Selected)
					columnList.Add(Item.Value);
			}
			string[] colAry = columnList.ToArray();
			return string.Join(sign, colAry);
		}
        #endregion

        #region 蒐集勾選的核取方塊Text值
        /// <summary>
        /// 蒐集勾選的核取方塊Text值
        /// </summary>
        public string[] GetCheckBoxText(ListControl listCtl)
        {
            List<string> columnList = new List<string>();

            foreach (ListItem Item in listCtl.Items)
            {
                if (Item.Selected)
                    columnList.Add(Item.Text);
            }
            string[] colAry = columnList.ToArray();
            return colAry;
        }
		#endregion
		#region 蒐集勾選的核取方塊Text值(組成字串)
		/// <summary>
		/// 蒐集勾選的核取方塊Text值
		/// </summary>
		public string JoinCheckBoxText(ListControl listCtl, string sign)
		{
			List<string> columnList = new List<string>();

			foreach (ListItem Item in listCtl.Items)
			{
				if (Item.Selected)
                    columnList.Add(Item.Text);
			}
			string[] colAry = columnList.ToArray();
			return string.Join(sign,colAry);
		}
        #endregion

		#region 還原CheckBoxList值
		/// <summary>
		/// 蒐集勾選的核取方塊Text值
		/// </summary>
		public void CheckBoxListSelected(CheckBoxList ckbl,string value ,char sign)
		{
			string[] arrValue = value.Split(sign);
			foreach (string val in arrValue)
			{
				foreach (ListItem item in ckbl.Items)
				{
					if (val == item.Value)
					{
						item.Selected = true;
						break;
					}
				}
			}
		}
		#endregion

        #region 組SQL Where條件(AND)
        /// <summary>
        /// 組SQL Where條件(AND)
        /// </summary>
        public string AssembleWhrSQL(string value, string sql)
        {
            value = PublicClass.ValueFilter(value);
            return (value != "") ? string.Format(sql + " AND ", value) : "";
        }
        #endregion

        #region <組SQL Where條件(OR)>
        /// <summary>
        /// 組SQL Where條件(OR)
        /// </summary>
        public string AssembleWhrSQLWithOR(string value, string sql)
        {
            value = PublicClass.ValueFilter(value);
            return (value != "") ? string.Format(sql + " OR ", value) : "";
        }
        #endregion

        #region 組SQL Where條件(AND)
        /// <summary>
        /// 組SQL Where條件(AND)
        /// </summary>
        public string AssembleWhrSQLWithAND(string value, string sql)
        {
            value = PublicClass.ValueFilter(value);
            return (value != "") ? string.Format(sql + " AND ", value) : "";
        }
        
        #endregion

        #region <去AND字串>
        /// <summary>
        /// 去AND字串
        /// </summary>
        public string ToWhr(string strWhr)
        {
            if (strWhr.Length > 0)
                strWhr = "WHERE " + strWhr.Substring(0, strWhr.Length - 4);
            return strWhr;
        }
        #endregion

        #region <去AND字串>
        /// <summary>
        /// 去AND字串
        /// </summary>
        public string ToWhrEatAND(string strWhr)
        {
            if (strWhr.Length > 0)
                strWhr = "WHERE " + strWhr.Substring(0, strWhr.Length - 4);
            return strWhr;
        }
        #endregion

        #region <去OR字串>
        /// <summary>
        /// 去OR字串
        /// </summary>
        public string ToWhrEatOR(string strWhr)
        {
            if (strWhr.Length > 0)
                strWhr = "WHERE " + strWhr.Substring(0, strWhr.Length - 3);
            return strWhr;
        }
        #endregion

        #region <取得使用者的IP位置>
        public string GetIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
        #endregion

        #region < -> GetRequestIP 取得使用者的網路位置 ,考慮 Proxy >
        /// --------------------------------------------------
        /// <summary>
        /// 取得使用者的網路位置(IP)
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------
        public string GetRequestIP()
        {
            // ##################################################
            HttpContext Ohttpd = HttpContext.Current;
            ///加入 Proxy 的因素考量
            string tmp = string.Format("{0}", Ohttpd.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);

            if (tmp == "")
                tmp = Ohttpd.Request.UserHostAddress.Trim();

            //218.210.33.170, 192.168.10.99
            string[] ip_list = tmp.Split(',');

            if (ip_list.Length > 0)
            {
                return ip_list[ip_list.Length - 1].Trim();
            }
            else
            {
                return "";
            }
            // ##################################################
        }
        #endregion
        
    }
}
