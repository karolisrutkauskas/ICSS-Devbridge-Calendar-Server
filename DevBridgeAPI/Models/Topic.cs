using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models
{
    public class Topic : IModel
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentTopicId { get; set; }
        public int ChangeByUserId { get; set; }
        public DateTime SysStart { get; set; }
        public DateTime SysEnd { get; set; }
    }
}