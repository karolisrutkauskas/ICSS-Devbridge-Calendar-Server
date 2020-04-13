using DevBridgeAPI.Areas.HelpPage.ModelDescriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models
{
    /// <summary>
    /// Entity that shows a relationship between user topic that was assigned to them.
    /// It is also known as 'Learning Day' that may be represented at user's calendar.
    /// </summary>
    
    [ModelName("Assignment")]
    public class Assignment : IModel
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int AsgnId { get; set; }

        /// <summary>
        /// User ID that has this assignment
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Topic ID that is assigned
        /// </summary>
        public int TopicId { get; set; }
        /// <summary>
        /// State ID that shows current state of this assignment (for example: upcomming, completed...)
        /// Use GET call to access full list of possible assignments
        /// </summary>
        public int StateId { get; set; }
        /// <summary>
        /// Optional description provided by user that has this assignment. Is in free text
        /// and can additionally contain links to other resources.
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// Date of this assignment, may be used for calendar.
        /// </summary>
        public DateTime Date { get; set; }
    }
}