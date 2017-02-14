using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using unity3d.Models;
using unity3d.Models.unity3d.Models;

namespace unity3d.Controllers
{
    public class UserController : Controller
    {
        /*
        URL     : /User/Info?UserID=1
        Method  : GET
        내용     : 유저 정보 조회 API
        */
        [HttpGet]
        public JsonResult Info(Int64 UserID)
        {

            UserResult result = UserModel.Select(UserID);

            result.ResultCode = 1;
            result.Message = "OK";

            return Json(result, JsonRequestBehavior.AllowGet);

        }

    }
}
