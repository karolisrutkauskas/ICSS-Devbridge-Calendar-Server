using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models
{
    [Table("LearntTopics")]
    public class LearntTopic
    {
        [ExplicitKey]
        public int TopicId { get; set; }
        [ExplicitKey]
        public int UserId { get; set; }
    }
}