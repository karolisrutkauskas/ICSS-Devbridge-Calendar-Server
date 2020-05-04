#pragma warning disable CA1303 // Do not pass literals as localized parameters
using DevBridgeAPI.Models.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Resources
{
    public static class Errors
    {
        /// <summary>"Unexpected Error"</summary>
        public static ErrorMessage GenericError()
        {
            return new ErrorMessage { Code = 0, Message = "Unexpected Error" };
        }
        /// <summary>"User can't be their own manager"</summary>
        public static ErrorMessage UserIsTheirOwnManager()
        {
            return new ErrorMessage { Code = 1, Message = "User can't be their own manager" };
        }
        /// <summary>$"User with ID {userId} is not found"</summary>
        public static ErrorMessage UserNotFound(int userId)
        {
            return new ErrorMessage { Code = 2, Message = $"User with ID {userId} is not found" };
        }
        /// <summary>$"Manager with ID {managerId} is not found"</summary>
        public static ErrorMessage ManagerNotFound(int managerId)
        {
            return new ErrorMessage { Code = 3, Message = $"Manager with ID {managerId} is not found" };
        }
        /// <summary>"Cannot reassign user to their descendant subordinate. Cycles in relationships not allowed"</summary>
        public static ErrorMessage UserRelationshipCycle()
        {
            return new ErrorMessage { Code = 4, Message = "Cannot reassign user to their descendant subordinate. Cycles in relationships not allowed" };
        }
        /// <summary>$"{value}: Violated unique value"</summary>
        public static ErrorMessage UniqueValueViolation(string value)
        {
            return new ErrorMessage { Code = 5, Message = $"{value}: Violated unique constraint" };
        }
        /// <summary>$"{entityType.Name}: Entity not found"</summary>
        public static ErrorMessage EntityNotFound(Type entityType)
        {
            return new ErrorMessage { Code = 6, Message = $"{entityType.Name}: Entity not found" };
        }
        /// <summary>"Request body cannot be empty"</summary>
        public static ErrorMessage EmptyRequestBody()
        {
            return new ErrorMessage { Code = 7, Message = "Request body cannot be empty" };
        }

        /// <summary>"Request body cannot be empty"</summary>
        public static ErrorMessage InvalidModelState(string modelStateError)
        {
            return new ErrorMessage { Code = 8, Message = $"Invalid model state - {modelStateError}" };
        }
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters