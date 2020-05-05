using DevBridgeAPI.Models.Misc;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.Resources;
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
            var errorMessages = new List<ErrorMessage>();

            if (newManagerId == userId)
            {
                errorMessages.Add(Errors.UserIsTheirOwnManager());
            }

            if (_usersDao.SelectByID(userId) == null)
            {
                errorMessages.Add(Errors.UserNotFound(userId));
            }

            if (_usersDao.SelectByID(newManagerId) == null)
            {
                errorMessages.Add(Errors.ManagerNotFound(newManagerId));
            }

            if (IsAncestorOf(ancestor: userId, descendant: newManagerId))
            {
                errorMessages.Add(Errors.UserRelationshipCycle());
            }

            return new ValidationInfo(errorMessages);
        }

        private bool IsAncestorOf(int ancestor, int descendant)
        {
            return _usersDao.IsAncestorOf(ancestor, descendant);
        }
    }
}