using dal;
using Dal.Migrations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace mvcOrEf
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //2020年10月21日 22点19分 多写备注，方便别人，方便自己


           /* Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBcontent, Configuration>());
            var dbMigrator = new DbMigrator(new Configuration());
            dbMigrator.Update();*/


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public class AuthFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                //如果用户未登录，且action未明确标识可跳过登录授权，则跳转到登录页面
                if (false && !filterContext.ActionDescriptor.IsDefined(typeof(AuthEscape), false))
                {
                    const string loginUrl = "~/home/login";
                    filterContext.Result = new RedirectResult(loginUrl);
                }
                base.OnActionExecuting(filterContext);

            }
        }
        public class AuthEscape : ActionFilterAttribute { }



    }
}
