using DevBridgeAPI.Models;
using System.Collections.Generic;

namespace DevBridgeAPI.Repository.Dao
{
    public interface IUsersDao
    {
        IEnumerable<IModel> SelectAllRows();
        User SelectByID(int userId);
        IModel SelectOneRow(string username, string password);
        IEnumerable<User> SelectSubordinates(int managerId);
        void InsertNewUser(User user);
    }
}