using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Patch
{
    public class UpdatedAssignment
    {
        public int TopicId { get; set; }
        public string Comments { get; set; }
        public DateTime Date { get; set; }
    }
}