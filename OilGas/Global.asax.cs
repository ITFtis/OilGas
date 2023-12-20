using RSW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OilGas
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //log4net
            log4net.Config.XmlConfigurator.Configure(); // must have this line
            var Logger = log4net.LogManager.GetLogger(typeof(MvcApplication));

            try
            {
                logger.Info("BkTask±Ò°Ê(Application_Start):" + DateFormat.ToDate6(DateTime.Now));

                var task = new BkTask();
                task.Run();
            }
            catch (Exception ex)
            {
                logger.Error("BkTask¿ù»~:" + ex.Message);
            }
        }
    }
}
