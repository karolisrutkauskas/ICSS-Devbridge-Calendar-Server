using DevBridgeAPI.Models;
using System.Collections.Generic;

namespace DevBridgeAPI.Repository.Selector
{
    public interface IUsersSelector
    {
        IEnumerable<IModel> SelectAllRows();
        User SelectByID(int userId);
        IModel SelectOneRow(string username, string password);
        IEnumerable<User> SelectSubordinates(int managerId);
    }
}