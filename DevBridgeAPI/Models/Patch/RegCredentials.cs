using System.ComponentModel.DataAnnotations;

namespace DevBridgeAPI.Models.Patch
{
    /// <summary>
    /// Credentials used to finalize user registration
    /// </summary>
    public class RegCredentials
    {
        /// <summary>
        /// Password created by user sent in plain text
        /// </summary>
        [Required]
        [RegularExpression(pattern: @"^(?=.*[0-9]+).{8,128}$", ErrorMessage = "Password must be at least 8 characters long" +
                                                                              "and contain at least 1 digit")]
        public string PlainPassword { get; set; }
        /// <summary>
        /// Token that was sent with URL in invitation email and is used 
        /// to check if valid user is trying to finish registration
        /// </summary>
        [Required]
        public string RegistrationToken { get; set; }
    }
}