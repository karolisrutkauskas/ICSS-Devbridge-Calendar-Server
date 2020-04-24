using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Complex
{
    public class TeamTreeNode
    {
        public User This { get; set; }
        public IEnumerable<TeamTreeNode> Children { get; set; }
    }
}