using DevBridgeAPI.Models.Post;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevBridgeAPI.UseCases.Util;
using DevBridgeAPI.UseCases.Exceptions;
using System.Data.SqlClient;

namespace DevBridgeAPI.UseCases.UserLogicN
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
            try
            {
                usersDao.InsertNewUser(newUser);
            } catch (SqlException ex)
            {
                if (ex.Message.Contains("UQ_Users_Email") && ex.Number == 2627) // 2627 - violated unique constraint
                {
                    throw new UniqueFieldException("Email address is already taken!", ex);
                }
                throw;
            }
        }
        public TeamTreeNode GetTeamTree(int rootUserId)
        {
            var user = usersDao.SelectByID(rootUserId);
            return tmTreeFactory.ConstructFromRoot(user);
        }
    }
}