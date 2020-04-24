using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models
{
    public class User : IModel
    {
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public int? ManagerId { get; set; }
        public int? ConsecLimit { get; set; }
        public int? MonthlyLimit { get; set; }
        public int? YearlyLimit { get; set; }
    }
}