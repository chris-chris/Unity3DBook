using System;
using System.Web.Mvc;
using unity3d.Models;

namespace unity3d.Controllers
{
    public class UpgradeController : Controller
    {
        /*
        URL     : /Upgrade/
        Method  : GET
        내용     : 업그레이드 정보를 조회하는 API
        */
        [HttpGet]
        public JsonResult Info()
        {
            UpgradeResult result = UpgradeModel.Info();

            result.ResultCode = 1;
            result.Message = "OK";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /*
        URL     : /Upgrade/Execute
        Method  : POST
        내용     : 업그레이드 요청
        */
        [HttpPost]
        public JsonResult Execute(Int64 UserID, string UpgradeType)
        {
            ResultBase result = UpgradeModel.Execute(UserID, UpgradeType);
            
            return Json(result);
        }
    }
}
