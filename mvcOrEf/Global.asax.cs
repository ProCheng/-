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

            //2020��10��21�� 22��19�� ��д��ע��������ˣ������Լ�


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

                //����û�δ��¼����actionδ��ȷ��ʶ��������¼��Ȩ������ת����¼ҳ��
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
