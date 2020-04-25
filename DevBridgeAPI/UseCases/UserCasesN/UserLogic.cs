using DevBridgeAPI.Models.Post;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevBridgeAPI.UseCases.Util;

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

        public void RegisterNewUser(int registeredById, User newUser)
        {
            // TODO: Validate request objects (don't allow null for example)
            newUser.ManagerId = registeredById;
            newUser.Password = HashingUtil.HashPasswordWithSalt(newUser.Password);
            usersDao.InsertNewUser(newUser);
        }
        public TeamTreeNode GetTeamTree(int rootUserId)
        {
            var user = usersDao.SelectByID(rootUserId);
            return tmTreeFactory.ConstructFromRoot(user);
        }
    }
}