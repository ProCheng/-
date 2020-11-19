using common;
using System.Web.Mvc;

namespace mvcOrEf.Controllers
{
    public class GetCommonController : Controller
    {
        // GET: GetCommon
        //[AuthEscape]
        public ActionResult checkingCodeLogin()
        {
            var code = ValidateCode.CreateRandomCode(4);
            Session["Code"] = code;
            Session.Timeout = 3;            //两分钟过期
            return File(ValidateCode.CreateImage(code), @"img/jpeg");
        }
        //[AuthEscape]
        public ActionResult error404()
        {
            return View();
        }
    }
}