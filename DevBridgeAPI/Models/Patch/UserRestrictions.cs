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
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Consecutive days limit cannot be lower than 0")]
        public int? ConsecLimit { get; set; }
        /// <summary>
        /// Constraint on user that limits how many assignments they can take per month
        /// </summary>
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Monthly limit cannot be lower than 0")]
        public int? MonthlyLimit { get; set; }
        /// <summary>
        /// Constraint on user that limits how many assignments they can take per year
        /// </summary>
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Yearly limit cannot be lower than 0")]
        public int? YearlyLimit { get; set; }
        /// <summary>
        /// Constraint on user that limits how many assignments they can take per quarter
        /// </summary>
        [Range(minimum: 0, maximum: int.MaxValue, ErrorMessage = "Quarter limit cannot be lower than 0")]
        public int? QuarterLimit { get; set; }
    }
}