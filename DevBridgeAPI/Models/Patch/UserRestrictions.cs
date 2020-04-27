using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models.Patch
{
    public class UserRestrictions
    {
        /// <summary>
        /// Constraint on user that limits how many assignments they can take for consecutive days
        /// </summary>
        [Required]
        public int? ConsecLimit { get; set; }
        /// <summary>
        /// Constraint on user that limits how many assignments they can take per month
        /// </summary>
        [Required]
        public int? MonthlyLimit { get; set; }
        /// <summary>
        /// Constraint on user that limits how many assignments they can take per year
        /// </summary>
        [Required]
        public int? YearlyLimit { get; set; }
    }
}