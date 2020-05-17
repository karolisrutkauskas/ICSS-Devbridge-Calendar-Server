using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Complex
{
    /// <summary>
    /// User's list of topics that are in user's 
    /// assignment that will happen sometime in the future
    /// </summary>
    public class PlannedTopicsPerUser
    {
        /// <summary>
        /// Info about user
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// A list of planned topics
        /// </summary>
        public IEnumerable<PlannedTopic> Topics { get; set; }

        /// <summary>
        /// Topic that has an assignment date for some user
        /// </summary>
        public class PlannedTopic
        {
            /// <summary>
            /// Info about the topic
            /// </summary>
            public Topic Topic { get; set; }
            /// <summary>
            /// Assignment date
            /// </summary>
            public DateTime AppointmentDate { get; set; }
        }
    }
}