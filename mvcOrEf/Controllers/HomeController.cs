using dal;
using model;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace mvcOrEf.Controllers
{
    public class HomeController : Controller
    {
        UnitofWork unitof = new UnitofWork();
        User user = null;

        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            /*  string userinfo = Newtonsoft.Json.JsonConvert.SerializeObject("fsd");
              FormsAuthentication.SignOut();*/


            return View();
        }

        [HttpGet]
        //[AuthEscape]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        //[AuthEscape]
        //[Authorize]
        public ActionResult Login(string Account, string Pwd, string Code)
        {

            try
            {

                if (Account == null || Pwd == null || Code == null)
                {
                    return Json(new { state = false, msg = "非法请求" });
                }
            }
            catch (Exception)
            {

                return Json(new { state = false, msg = "非法请求" });
            }
            if (Session["Code"] == null)
            {
                return Json(new { state = false, msg = "验证码已经过期" });
            }
            if (Code.ToUpper() != Session["Code"].ToString().ToUpper())
            {
                return Json(new { state = false, msg = "验证码错误" });
            }
            try
            {
                user = unitof.CreateRespository<User>().GetEntityFirst(a => a.Account == Account && a.Pwd == Pwd);
            }
            catch (Exception)
            {

                return Json(new { state = false, msg = "发生了未知错误" });
            }
            if (user != null)
            {
                Session["User"] = user;
                Session.Timeout = 10;
                string userstr = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                //身份验证一天过期
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Account, DateTime.Now, DateTime.Now.AddDays(1)
                    , true, userstr, FormsAuthentication.FormsCookiePath);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                Response.Cookies.Add(cookie);
                return Json(new { state = true, msg = "登录成功" });
            }
            else
                return Json(new { state = false, msg = "账号或密码错误" });
            //return Json(new {Account=Account,Pwd=Pwd },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logoff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "home");

        }


    }
}