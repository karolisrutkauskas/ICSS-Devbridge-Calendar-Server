using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DevBridgeAPI.Models
{
    /// <summary>
    /// Entity that has information about the main user of this system
    /// </summary>
    public class User : IModel
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Person's first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Person's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Persons email address. Must be unique among other users.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Person's role, for example: Junior Developer, Human Resources etc...
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Person's password
        /// </summary>
        [IgnoreDataMember]
        public string Password { get; set; }
        /// <summary>
        /// Unique identifier of this user's manager. If user has no manager, this field is null.
        /// </summary>
        public int? ManagerId { get; set; }
        /// <summary>
        /// Constraint on user that limits how many assignments they can take for consecutive days
        /// </summary>
        public int? ConsecLimit { get; set; }
        /// <summary>
        /// Constraint on user that limits how many assignments they can take per month
        /// </summary>
        public int? MonthlyLimit { get; set; }
        /// <summary>
        /// Constraint on user that limits how many assignments they can take per year
        /// </summary>
        public int? YearlyLimit { get; set; }
        /// <summary>
        /// A generated token to be used with new user invitation URL
        /// </summary>
        [IgnoreDataMember]
        public string RegistrationToken { get; set; }
    }
}