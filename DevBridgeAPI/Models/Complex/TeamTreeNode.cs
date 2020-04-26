using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DevBridgeAPI.Models.Complex
{
    [DataContract(IsReference = true)]
    public class TeamTreeNode
    {
        /// <summary>
        /// Information about user in the current node
        /// </summary>
        [DataMember]
        public User This { get; set; }
        /// <summary>
        /// A list of this user's subordinates. Will be null if empty
        /// </summary>
        [DataMember]
        public IEnumerable<TeamTreeNode> Children { get; set; }
    }
}