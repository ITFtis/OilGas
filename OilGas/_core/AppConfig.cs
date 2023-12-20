using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OilGas
{
    public class AppConfig
    {
        #region 私有變數

        private static string _rootPath;
        private static bool _isDev;

        #endregion

        #region 建構子

        static AppConfig()
        {
            _rootPath = ConfigurationManager.AppSettings["RootPath"].ToString();
            //實體路徑(解決開發者專案於不同目錄)
            _rootPath = _rootPath.Replace("~\\", HttpContext.Current.Server.MapPath("~\\"));

            _isDev = ConfigurationManager.AppSettings["IsDev"].ToString() == "true";
        }

        #endregion

        #region 公用屬性      

        /// <summary>
        /// 檔案存放跟目錄
        /// </summary>
        public static string RootPath
        {
            get { return _rootPath; }
        }

        /// <summary>
        /// (true/false)開發階段 true:不cache..等
        /// </summary>
        public static bool IsDev
        {
            get { return _isDev; }
        }

        #endregion
    }
}