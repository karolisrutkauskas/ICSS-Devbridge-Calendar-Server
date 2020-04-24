using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.UseCases.UserCasesN
{
    public class UserLogic : IUserLogic
    {
        private readonly IUsersDao usersDao;
        private readonly ITeamTreeNodeFactory tmTreeFactory;

        public UserLogic(IUsersDao usersDao, ITeamTreeNodeFactory tmTreeFactory)
        {
            this.usersDao = usersDao;
            this.tmTreeFactory = tmTreeFactory;
        }

        public void RegisterNewUser(int registeredById, Models.User newUser)
        {
            // TODO: Validate request objects (don't allow null for example)
            newUser.ManagerId = registeredById;
            // TODO: Hash passwords
            usersDao.InsertNewUser(newUser);
        }
        public TeamTreeNode GetTeamTree(int rootUserId)
        {
            var user = usersDao.SelectByID(rootUserId);
            return tmTreeFactory.ConstructFromRoot(user);
        }
    }
}