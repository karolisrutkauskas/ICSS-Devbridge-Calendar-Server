using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DevBridgeAPI.Models.Misc
{
    public class ValidationInfo
    {
        /// <summary>
        /// A list of messages that caused the validation to fail
        /// </summary>
        public IEnumerable<ErrorMessage> ErrorMessages { get; }

        /// <summary>
        /// Will be true if at least one error exists, otherwise false
        /// </summary>
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