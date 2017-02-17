using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unity3d.Models
{
    public class UpgradeResult : ResultBase
    {
        public List<UpgradeData> Data { get; set; }
    }
}