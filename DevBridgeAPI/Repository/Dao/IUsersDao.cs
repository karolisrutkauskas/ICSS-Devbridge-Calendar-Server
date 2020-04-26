using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Post;
using System.Collections.Generic;
using User = DevBridgeAPI.Models.User;
using PostUser = DevBridgeAPI.Models.Post.User;

namespace DevBridgeAPI.Repository.Dao
{
    public interface IUsersDao
    {
        IEnumerable<IModel> SelectAllRows();
        User SelectByID(int userId);
        IModel SelectOneRow(string username, string password);
        IEnumerable<User> SelectSubordinates(int managerId);
        void InsertNewUser(PostUser user);
    }
}