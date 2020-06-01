using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Post
{
    /// <summary>
    /// Wrapper object for RegistrationToken string used
    /// for passing RegistrationToken through body
    /// </summary>
    public class RegistrationTokenWrapper
    {
        /// <summary>
        /// Token that was sent with URL in invitation email and is used 
        /// to check if valid user is trying to finish registration
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Registration Token must be provided")]
        public string RegistrationToken { get; set; }
    }
}