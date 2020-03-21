using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Models
{
    public class Constraint : IModel
    {
        public int ConstrId { get; set; }
        public int? ConsecLimit { get; set; }
        public int? MonthLimit { get; set; }
        public int? YearLimit { get; set; }
        public int? TeamId { get; set; }
        public int? UserId { get; set; }
        public bool Global { get; set; }
    }
}