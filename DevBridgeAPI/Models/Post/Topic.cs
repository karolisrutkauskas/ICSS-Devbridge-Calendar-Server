using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DevBridgeAPI.Models.Post
{
    /// <summary>
    /// Information about a subject that can be taken as assignment and used by users to
    /// track their learning progress.
    /// </summary>
    public class Topic : IModel
    {
        /// <summary>
        /// Topic name (ex. Javascript basics, Team management basics etc..)
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name must be provided")]
        public string Name { get; set; }
        /// <summary>
        /// A free text field used for detailed description of topic
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Used for making topic learning hierarchy. May be used to specify
        /// another topic that is recommended requirement before this topic.
        /// May be null if there is no such topic.
        /// </summary>
        public int? ParentTopicId { get; set; }
        /// <summary>
        /// ID of user that updated/created this version of topic
        /// </summary>
        [IgnoreDataMember]
        public int? ChangeByUserId { get; set; }
    }
}