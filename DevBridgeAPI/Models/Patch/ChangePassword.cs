using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Patch
{
    /// <summary>
    /// Model for changing user's passwrd
    /// </summary>
    public class ChangePassword
    {
        /// <summary>
        /// User's old password required for determining validity (in plain text)
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "OldPassword field is required")]
        public string OldPassword { get; set; }
        /// <summary>
        /// User's new password (in plain text)
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "NewPassword field is required")]
        [RegularExpression(pattern: @"^(?=.*[0-9]+).{8,128}$", ErrorMessage = "Password must be at least 8 characters long" +
                                                                              "and contain at least 1 digit")]
        public string NewPassword { get; set; }
    }
}