using System;
using System.Web.Mvc;
using unity3d.Models;

namespace unity3d.Controllers
{
    public class LoginController : Controller
    {
        /*
        URL     : /Login/Facebook
        Method  : POST
        Body    : 
        {
            "FacebookID":12323,
            "FacebookName":"Chris",
            "FacebookPhotoURL":"http://www.google.com"
        }
        내용     : 페이스북 정보를 활용해서 게임 서버에 로그인하는 API
        */
        [HttpPost]
        public JsonResult Facebook(UserData User)
        {
            LoginResult result = LoginModel.Login(User);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
