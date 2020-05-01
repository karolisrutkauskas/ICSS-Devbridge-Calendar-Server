using DevBridgeAPI.Models.Post;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.UseCases.Util;
using DevBridgeAPI.UseCases.Exceptions;
using System.Data.SqlClient;
using DevBridgeAPI.Models.Patch;
using System;
using User = DevBridgeAPI.Models.User;
using PostUser = DevBridgeAPI.Models.Post.User;

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

        /// <summary>
        /// Inserts a new user entity. Assumes that User's password is plain as it will be
        /// hashed in this method before insertion to database.
        /// </summary>
        /// <param name="newUser">New user to be inserted. Password property must not be hashed yet</param>
        public void RegisterNewUser(PostUser newUser)
        {
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

        /// <summary>
        /// Constructs a TeamTreeNode hierarchical structure starting at User with <paramref name="rootUserId"/>
        /// </summary>
        /// <param name="rootUserId">Id of root user</param>
        /// <returns>Constructed user team hierachy tree</returns>
        public TeamTreeNode GetTeamTree(int rootUserId)
        {
            var user = usersDao.SelectByID(rootUserId);
            return tmTreeFactory.ConstructFromRoot(user);
        }

        /// <summary>
        /// Calls data access to update user's restrictions
        /// </summary>
        /// <param name="userRestrictions">Modified user restrictions</param>
        /// <param name="userId">ID to lookup a user and modify</param>
        /// <returns>User instance with updated restrictions</returns>
        public User ChangeRestrictions(UserRestrictions userRestrictions, int userId)
        {
            User userToUpdate = usersDao.SelectByID(userId);
            if (userToUpdate == null)
            {
                throw new EntityNotFoundException($"User with ID {userId} was not found");
            }

            userToUpdate.ConsecLimit = userRestrictions.ConsecLimit;
            userToUpdate.MonthlyLimit = userRestrictions.MonthlyLimit;
            userToUpdate.YearlyLimit = userRestrictions.YearlyLimit;

            usersDao.UpdateUserAsync(userToUpdate);
            return userToUpdate;
        }

        public void ChangeGlobalRestrictions(UserRestrictions userRestrictions)
        {
            usersDao.UpdateGlobalRestrictions(userRestrictions);
        }

        public void ChangeTeamRestrictions(UserRestrictions userRestrictions, int managerId)
        {
            User teamManager = usersDao.SelectByID(managerId);
            if (teamManager == null)
            {
                throw new EntityNotFoundException($"User with ID {managerId} was not found");
            }

            usersDao.UpdateTeamRestrictions(userRestrictions, managerId);
        }
    }
}