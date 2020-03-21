using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models
{
    public class Team : IModel
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
    }
}