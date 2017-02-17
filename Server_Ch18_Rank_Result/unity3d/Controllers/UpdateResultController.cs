using System;
using System.Web.Mvc;
using unity3d.Models;

namespace unity3d.Controllers
{
    // 스테이지 정보를 업데이트하는 API 콘트롤러 클래스
    public class UpdateResultController : Controller
    {
        /*
        URL     : /UpdateResult/Post
        Method  : POST
        내용     : 스테이지 결과를 서버에 업로드해서 데이터베이스에 기록하는 API
        */
        public JsonResult Post(StageData Data)
        {
            StageResult result = StageModel.UpdateRecord(Data);

            result.Message = "OK";
            result.ResultCode = 1;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
