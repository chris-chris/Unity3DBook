using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCoreServer.Models;

namespace DotnetCoreServer.Controllers
{
    [Route("[controller]/[action]")]
    public class UpgradeController : Controller
    {

        IUserDao userDao;
        public UpgradeController(IUserDao userDao){
            this.userDao = userDao;
        }

        // GET Upgrade/Info
        [HttpGet]
        public string Info()
        {

            return "";
        }

        // POST Upgrade/Execute
        [HttpPost]
        public string Execute([FromBody] User requestUser)
        {

            return "";

        }

    }
}
