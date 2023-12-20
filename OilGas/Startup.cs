using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(OilGas.Startup))]

namespace OilGas
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Dou.Context.Init(new Dou.DouConfig
            {
                DefaultPassword = "3922",
                PasswordEncode = (p) =>
                {
                    //return (int.Parse(p) * 4 + 13579) + "";
                    return System.Web.Helpers.Crypto.HashPassword(p);
                },
                VerifyPassword = (ep, vp) =>
                {
                    //int pint = 0;
                    //bool tp = int.TryParse(vp, out pint);
                    //if (!tp)
                    //    return false;
                    //else
                    //{
                    //    return ep == (pint * 4 + 13579) + "";
                    //}
                    return System.Web.Helpers.Crypto.VerifyHashedPassword(ep, vp);
                },
                SqlDebugLog = true,
                LoginPage = new System.Web.Mvc.UrlHelper(System.Web.HttpContext.Current.Request.RequestContext).Action("Index", "Home")
            });
        }

    }
}
