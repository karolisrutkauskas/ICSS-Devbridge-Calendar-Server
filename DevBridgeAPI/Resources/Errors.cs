﻿#pragma warning disable CA1303 // Do not pass literals as localized parameters
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

        /// <summary>$"Invalid model state - {modelStateError}"</summary>
        public static ErrorMessage InvalidModelState(string modelStateError)
        {
            return new ErrorMessage { Code = 8, Message = $"Invalid model state - {modelStateError}" };
        }

        /// <summary>$"Provided registration token does not match"</summary>
        public static ErrorMessage InvalidRegistrationToken()
        {
            return new ErrorMessage { Code = 9, Message = $"Provided registration token does not match" };
        }

        /// <summary>"Provided registration token is expired"</summary>
        public static ErrorMessage ExpiredRegistrationToken()
        {
            return new ErrorMessage { Code = 10, Message = "Provided registration token is expired" };
        }

        /// <summary>"User is already registered"</summary>
        public static ErrorMessage UserAlreadyRegistered()
        {
            return new ErrorMessage { Code = 11, Message = "User is already registered" };
        }

        /// <summary>$"User has exceeded assignment limits: {limitType}"</summary>
        public static ErrorMessage ExceededAssignmentLimits(string limitType)
        {
            return new ErrorMessage { Code = 12, Message = $"User has exceeded assignment limits: {limitType}" };
        }
      
        /// <summary>$"Missing mandatory query parameter: {paramName}"</summary>
        public static ErrorMessage MissingMandatoryQueryParameter(string paramName)
        {
            return new ErrorMessage { Code = 13, Message = $"Missing mandatory query parameter: {paramName}" };
        }

        /// <summary>"Invalid password"</summary>
        public static ErrorMessage InvalidPassword()
        {
            return new ErrorMessage { Code = 14, Message = "Invalid password" };
        }
    }
}
#pragma warning restore CA1303 // Do not pass literals as localized parameters