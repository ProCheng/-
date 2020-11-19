using System.Web.Mvc;

namespace mvcOrEf
{
    public class FilterConfig
    {
        /// <summary>
        /// 注册全局过滤器
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            //登录授权
            //filters.Add(new AuthFilter());
            filters.Add(new HandleErrorAttribute());    //注册错误

        }
    }
}
