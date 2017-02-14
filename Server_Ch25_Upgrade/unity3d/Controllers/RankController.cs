using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using unity3d.Models;

namespace unity3d.Controllers
{
    public class RankController : Controller
    {
        /*
        URL     : /Rank/Total?Start=1&Count=50
        Method  : GET
        내용     : 전체 랭킹을 조회하는 API
        */
        [HttpGet]
        public JsonResult Total(int Start, int Count)
        {

            RankResult result = RankModel.Total(Start, Count);

            result.Message = "OK";
            result.ResultCode = 1;

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        /*
        URL     : /Rank/Friend
        Method  : POST
        내용     : 친구 랭킹을 조회하는 API
        */
        [HttpPost]
        public JsonResult Friend(Int64 UserID, List<string> FriendList)
        {

            RankResult result = RankModel.Friend(UserID, FriendList);

            result.Message = "OK";
            result.ResultCode = 1;

            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}
