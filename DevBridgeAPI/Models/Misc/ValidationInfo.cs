using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DevBridgeAPI.Models.Misc
{
    public class ValidationInfo
    {
        public IEnumerable<ErrorMessage> ErrorMessages { get; }
        public bool IsValid { get; }
        public ValidationInfo(IEnumerable<ErrorMessage> errorMessages)
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