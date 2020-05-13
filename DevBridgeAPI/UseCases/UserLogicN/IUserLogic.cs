using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Models.Patch;
using User = DevBridgeAPI.Models.User;
using PostUser = DevBridgeAPI.Models.Post.User;
using DevBridgeAPI.UseCases.Exceptions;

namespace DevBridgeAPI.UseCases.UserLogicN
{
    public interface IUserLogic
    {
        /// <summary>
        /// Inserts a new user entity. Assumes that User's password is plain as it will be
        /// hashed in this method before insertion to database.
        /// </summary>
        /// <param name="newUser">New user to be inserted. Password property must not be hashed yet</param>
        User RegisterNewUser(PostUser newUser);
        /// <summary>
        /// Constructs a TeamTreeNode hierarchical structure starting at User with <paramref name="rootUserId"/>
        /// </summary>
        /// <param name="rootUserId">Id of root user</param>
        /// <returns>Constructed user team hierachy tree</returns>
        TeamTreeNode GetTeamTree(int rootUserId);
        TeamTreeNode GetTeamTree(string rootEmail);
        /// <summary>
        /// Calls data access to update user's restrictions
        /// </summary>
        /// <param name="userRestrictions">Modified user restrictions</param>
        /// <param name="userId">ID to lookup a user and modify</param>
        /// <returns>User instance with updated restrictions</returns>
        /// <exception cref="EntityNotFoundException">When user with ID <paramref name="userId"/> is not found</exception>
        User ChangeRestrictions(UserRestrictions userRestrictions, int userId);
        /// <summary>
        /// Changes restrictions for every user
        /// </summary>
        /// <param name="userRestrictions">New user restrictions</param>
        void ChangeGlobalRestrictions(UserRestrictions userRestrictions);
        /// <summary>
        /// Changes restrictions for every member in team (excluding manager)
        /// </summary>
        /// <param name="userRestrictions">New user restrictions</param>
        /// <param name="managerId">ID of manager whose team will have restrictions changed</param>
        /// <exception cref="EntityNotFoundException">When user with <paramref name="managerId"/> ID is not found</exception>
        void ChangeTeamRestrictions(UserRestrictions userRestrictions, int managerId);
        /// <summary>
        /// Changes manager for the specified user
        /// </summary>
        /// <param name="newManagerId">ID of new user's manager</param>
        /// <param name="userId">ID of user that will have their manager changed</param>
        /// <returns>User with reassigned manager</returns>
        /// <exception cref="ValidationFailedException">When manager reassignment does not meet business requirements</exception>
        User ChangeTeamMember(int newManagerId, int userId);
        /// <summary>
        /// Finalizes registration for user with provided email.
        /// Will perform validations and test if user is not already registered,
        /// if provided token is not expired and if user exists at all
        /// </summary>
        /// <param name="regCredentials">Credentials used to finish registration, also used for validation</param>
        /// <returns>Updated user entity after successfulregistration</returns>
        /// <exception cref="ValidationFailedException">When user registration fails described validations</exception>
        /// <exception cref="EntityNotFoundException">When user with provided email was not found</exception>
        User FinishRegistration(RegCredentials regCredentials);
    }
}
