using DevBridgeAPI.Models.Misc;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.UseCases.UserLogicN
{
    public class UserValidator : IUserValidator
    {
        private readonly IUsersDao _usersDao;

        public UserValidator(IUsersDao usersDao)
        {
            _usersDao = usersDao;
        }
        public ValidationInfo ValidataManagerReassignment(int newManagerId, int userId)
        {
            var errorMessages = new List<string>();

            if (newManagerId == userId)
            {
                errorMessages.Add("User can't be their own manager");
            }

            if (_usersDao.SelectByID(userId) == null)
            {
                errorMessages.Add($"User with ID {userId} is not found");
            }

            if (_usersDao.SelectByID(newManagerId) == null)
            {
                errorMessages.Add($"Manager with ID {newManagerId} is not found");
            }

            if (IsAncestorOf(ancestor: userId, descendant: newManagerId))
            {
                errorMessages.Add("Cannot reassign user to their descendant subordinate. Cycles in relationships not allowed");
            }

            return new ValidationInfo(errorMessages);
        }

        private bool IsAncestorOf(int ancestor, int descendant)
        {
            return _usersDao.IsAncestorOf(ancestor, descendant);
        }
    }
}