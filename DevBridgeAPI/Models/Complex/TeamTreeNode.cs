using Microsoft.Ajax.Utilities;
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

        public IEnumerable<TeamTreeNode> BreadthFirstSearch(Predicate<TeamTreeNode> condition)
        {
            return BreadthFirstSearch(condition, new Queue<TeamTreeNode>());
        }

        private IEnumerable<TeamTreeNode> BreadthFirstSearch(Predicate<TeamTreeNode> condition, Queue<TeamTreeNode> bfsQueue)
        {
            var results = new LinkedList<TeamTreeNode>();
            Children.ForEach(x => bfsQueue.Enqueue(x));
            if (condition.Invoke(this) == true)
            {
                results.AddLast(this);
            }
            if (bfsQueue.Count == 0)
            {
                return results;
            } else
            {
                bfsQueue.Dequeue().BreadthFirstSearch(condition, bfsQueue).ForEach(x => results.AddLast(x));
                return results;
            }
        }
    }
}