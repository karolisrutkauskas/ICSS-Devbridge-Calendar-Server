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
using DevBridgeAPI.UseCases.Integrations;
using DevBridgeAPI.UseCases.Integrations.EmailService;
using DevBridgeAPI.Resources;
using System.Configuration;

namespace DevBridgeAPI.UseCases.UserLogicN
{
    public class UserLogic : IUserLogic
    {
        private readonly IUsersDao usersDao;
        private readonly ITeamTreeNodeFactory tmTreeFactory;
        private readonly IUserValidator userValidator;
        private readonly IUserIntegrations userIntegrations;

        public UserLogic(IUsersDao usersDao, ITeamTreeNodeFactory tmTreeFactory, IUserValidator userValidator, IUserIntegrations userIntegrations)
        {
            this.usersDao = usersDao;
            this.tmTreeFactory = tmTreeFactory;
            this.userValidator = userValidator;
            this.userIntegrations = userIntegrations;
        }

        /// <summary>
        /// Inserts a new user entity to database. User entity will
        /// be marked as unregistered by assigning RegistrationToken
        /// </summary>
        /// <param name="newUser">New user to be inserted</param>
        public User RegisterNewUser(PostUser newUser)
        {
            try
            {
                newUser.RegistrationToken = HashingUtil.GenerateToken();
                var registeredUser = usersDao.InsertAndReturnNewUser(newUser);
                userIntegrations.CreateInvitation(newUser);
                return registeredUser;
            } catch (SqlException ex)
            {
                if (ex.Message.Contains("UQ_Users_Email") && ex.Number == 2627) // 2627 - violated unique constraint
                {
                    throw new UniqueFieldException(ex.Message, nameof(PostUser.Email));
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
        /// <exception cref="EntityNotFoundException">When user with ID <paramref name="userId"/> is not found</exception>
        public User ChangeRestrictions(UserRestrictions userRestrictions, int userId)
        {
            User userToUpdate = usersDao.SelectByID(userId);
            if (userToUpdate == null)
            {
                throw new EntityNotFoundException($"User with ID {userId} was not found", typeof(User));
            }

            userToUpdate.ConsecLimit = userRestrictions.ConsecLimit;
            userToUpdate.MonthlyLimit = userRestrictions.MonthlyLimit;
            userToUpdate.YearlyLimit = userRestrictions.YearlyLimit;

            usersDao.UpdateUserAsync(userToUpdate);
            return userToUpdate;
        }

        /// <summary>
        /// Changes restrictions for every user
        /// </summary>
        /// <param name="userRestrictions">New user restrictions</param>
        public void ChangeGlobalRestrictions(UserRestrictions userRestrictions)
        {
            usersDao.UpdateGlobalRestrictions(userRestrictions);
        }

        /// <summary>
        /// Changes restrictions for every member in team (excluding manager)
        /// </summary>
        /// <param name="userRestrictions">New user restrictions</param>
        /// <param name="managerId">ID of manager whose team will have restrictions changed</param>
        /// <exception cref="EntityNotFoundException">When user with <paramref name="managerId"/> ID is not found</exception>
        public void ChangeTeamRestrictions(UserRestrictions userRestrictions, int managerId)
        {
            User teamManager = usersDao.SelectByID(managerId);
            if (teamManager == null)
            {
                throw new EntityNotFoundException($"User with ID {managerId} was not found", typeof(User));
            }

            usersDao.UpdateTeamRestrictions(userRestrictions, managerId);
        }

        /// <summary>
        /// Changes manager for the specified user
        /// </summary>
        /// <param name="newManagerId">ID of new user's manager</param>
        /// <param name="userId">ID of user that will have their manager changed</param>
        /// <returns>User with reassigned manager</returns>
        /// <exception cref="ValidationFailedException">When manager reassignment does not meet business requirements</exception>
        public User ChangeTeamMember(int newManagerId, int userId)
        {
            var validationInfo = userValidator.ValidataManagerReassignment(newManagerId, userId);
            if (!validationInfo.IsValid)
            {
                throw new ValidationFailedException(validationInfo);
            }

            var userForUpdate = usersDao.SelectByID(userId);
            userForUpdate.ManagerId = newManagerId;

            return userForUpdate;
        }

    }
}