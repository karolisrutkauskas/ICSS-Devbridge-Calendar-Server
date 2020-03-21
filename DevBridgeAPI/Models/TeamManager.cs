using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models
{
    public class TeamManager : IModel
    {
        public int TeamId { get; set; }
        public int ManagerId { get; set; }
    }
}