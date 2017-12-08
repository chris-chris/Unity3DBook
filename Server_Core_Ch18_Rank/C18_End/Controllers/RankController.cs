using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    public class RankController : Controller
    {

        IUserDao userDao;
        public RankController(IUserDao userDao){
            this.userDao = userDao;
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            User user = userDao.GetUser(id);
            return user;
        }

        // POST Login/Facebook
        [HttpGet]
        public RankResult Total(int Start, int Limit)
        {

            RankResult result = new RankResult();
            
            return result;

        }

    }
}
