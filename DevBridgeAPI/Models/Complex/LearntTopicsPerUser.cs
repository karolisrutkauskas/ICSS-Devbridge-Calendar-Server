using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Complex
{
    /// <summary>
    /// Topics that a user has completed learning at some point
    /// </summary>
    public class LearntTopicsPerUser
    {
        /// <summary>
        /// Info about a user
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// A list of learnt topics
        /// </summary>
        public IEnumerable<Topic> Topics { get; set; }
    }
}