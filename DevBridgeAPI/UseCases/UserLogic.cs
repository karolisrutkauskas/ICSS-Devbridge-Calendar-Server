using DevBridgeAPI.Models;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.UseCases
{
    public class UserLogic : IUserLogic
    {
        IUsersDao usersDao;
        public void RegisterNewUser(int registeredById, User newUser)
        {
            // TODO: Validate request objects (don't allow null for example)
            newUser.ManagerId = registeredById;
            // TODO: Hash passwords
            usersDao.InsertNewUser(newUser);
        }
    }
}