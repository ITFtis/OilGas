using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace OilGas
{
    /// <summary>
    /// SQLHelper 的摘要描述
    /// </summary>
    public class SQLHelper
    {
	    public SQLHelper()
	    {
		    //
		    // TODO: 在此加入建構函式的程式碼
		    //
	    }
        /// <summary>
        /// 取得Table的所有欄位名稱
        /// </summary>
        /// <param name="tableName">資料表名稱</param>
        /// <returns>Table物件</returns>
        //public DataTable GetTableColumnName(string tableName)
        //{
        //    string sql = string.Format(@"
        //                 SELECT name FROM syscolumns 
        //                 WHERE id=object_id('{0}') order by colorder "
        //                 , tableName);
        //    DataTable dt = Mei.DataAccess.ExecuteDataTable(sql,Mei.DataAccess.ConnectionType._CienveGSLConnection);
        //    return dt;
        //}
        /// <summary>
        /// 將Table的所有欄位名稱組成字串
        /// </summary>
        /// <param name="sqlKeyWord">SQL關鍵字(SELECT或UPDATE)</param>
        /// <param name="dtColumnName">Table名稱</param>
        /// <returns>字串</returns>
        public string AssembleAllcolumnName(string sqlKeyWord,DataTable dtColumnName)
        {
            string strColumnName = "";
            sqlKeyWord = sqlKeyWord.ToLower();
            switch(sqlKeyWord)
            {
                case "select" :
                    for (int i = 0; i < dtColumnName.Rows.Count; i++ )
                        strColumnName += (i != dtColumnName.Rows.Count - 1) ? dtColumnName.Rows[i][0].ToString() + ","
                                                                            : dtColumnName.Rows[i][0].ToString();
                    break;
                case "update" :
                    for (int i = 0; i < dtColumnName.Rows.Count; i++)
                        strColumnName += (i != dtColumnName.Rows.Count - 1) ? dtColumnName.Rows[i][0].ToString() + "=" + "'{" + i + "}'" + ","
                                                                           : dtColumnName.Rows[i][0].ToString() + "=" + "'{" + i + "}'";
                    break;
                case "insert" :
                    for (int i = 0; i < dtColumnName.Rows.Count; i++ )
                        strColumnName += (i != dtColumnName.Rows.Count - 1) ? "'{" + i + "}'" + ","
                                                                            : "'{" + i + "}'";
                    break;
            }
            return strColumnName;
        }
        /// <summary>
        /// 組SQL Where條件(AND)
        /// </summary>
        public static string AssembleWhrSQLWithAND(string value, string sql)
        {
            value = PublicClass.ValueFilter(value);
            return (value != "") ? string.Format(" AND " + sql, value) : "";
        }
        /// <summary>
        /// 組SQL Where條件(OR)
        /// </summary>
        public static string AssembleWhrSQLWithOR(string value, string sql)
        {
            value = PublicClass.ValueFilter(value);
            return (value != "") ? string.Format(" OR " + sql, value) : "";
        }
        public static string ToWhr(string strWhr)
        {
            if (strWhr.Length > 0)
                strWhr = " WHERE 1=1 " + strWhr;
            return strWhr;
        }
        //public string GetColumnValue(string columnName, string tableName, string sqlWhr)
        //{
        //    string sql = string.Format(@"
        //                SELECT {0} FROM {1} {2}", columnName, tableName, sqlWhr);
        //    object obj = Mei.DataAccess.GetSingle(sql, Mei.DataAccess.ConnectionType._CienveGSLConnection);
        //    return (obj != DBNull.Value && obj != null) ? obj.ToString() : string.Empty;
        //}
    }
}