using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Patch
{
    public class UserManagerId
    {
        /// <summary>
        /// Unique identifier of this user's manager. If user has no manager, this field is null.
        /// </summary>
        [Required(ErrorMessage = "Manager ID must be provided")]
        public int? ManagerId { get; set; }
    }
}