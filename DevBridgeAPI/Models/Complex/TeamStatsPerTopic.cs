using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Complex
{
    public class TeamStatsPerTopic
    {
        public User TeamManager { get; set; }
        public int? MemberCount { get; set; }
    }
}