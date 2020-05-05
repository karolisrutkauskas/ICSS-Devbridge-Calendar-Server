using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DevBridgeAPI.Models.Misc
{
    public class ValidationInfo
    {
        public IEnumerable<string> ErrorMessages { get; set; }
        public bool IsValid { get; }
        public ValidationInfo(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
            if (errorMessages == null || !errorMessages.Any())
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
        }
    }
}