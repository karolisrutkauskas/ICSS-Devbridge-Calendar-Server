using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Post;
using System.Collections.Generic;
using User = DevBridgeAPI.Models.User;
using PostUser = DevBridgeAPI.Models.Post.User;
using DevBridgeAPI.Models.Patch;
using System.Data.SqlClient;

namespace DevBridgeAPI.Repository.Dao
{
    public interface IUsersDao
    {
        IEnumerable<IModel> SelectAllRows();
        User SelectByID(int userId);
        User SelectByEmail(string email);
        IModel SelectOneRow(string username, string password);
        IEnumerable<User> SelectSubordinates(int managerId);
        User InsertAndReturnNewUser(PostUser user);
        void UpdateUser(User updatedUser);
        void UpdateGlobalRestrictions(UserRestrictions restrictions);
        void UpdateTeamRestrictions(UserRestrictions restrictions, int managerId);
        bool IsAncestorOf(int ancestor, int descendant);
        void UpdatePasswordClearToken(string hashedPassword, int userId);
        IEnumerable<User> SelectByPastTopicAssignment(int topicId, int ancestorId);
    }
}