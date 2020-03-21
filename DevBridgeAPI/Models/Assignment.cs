using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models
{
    public class Assignment : IModel
    {
        public int AsgnId { get; set; }
        public int UserId { get; set; }
        public int TopicId { get; set; }
        public string State { get; set; }
        public string Comments { get; set; }
        public DateTime Date { get; set; }
    }
}