using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Selector
{
    public class UsersSelector : IModelSelector, IUsersSelector
    {
        public IEnumerable<IModel> SelectAllRows()
        {
            string sql = "SELECT * FROM Users";
            using (var db = new DbContext())
            {
                return db.Connection.Query<User>(sql);
            }
        }

        public IModel SelectOneRow(string username, string password)
        {
            string sql = "SELECT * FROM Users WHERE Email = @Username AND Password = @Password";
            using (var db = new DbContext())
            {
                return db.Connection.Query<User>(sql, new { Username = username, Password = password }).FirstOrDefault();
            }
        }

        public User SelectByID(int userId)
        {
            string sql = "SELECT * FROM Users " +
                         "WHERE UserId = @UserId";
            using (var db = new DbContext())
            {
                return db.Connection.Query<User>(sql, new { UserId = userId }).FirstOrDefault();
            }
        }

        public IEnumerable<User> SelectSubordinates(int managerId)
        {
            string sql = "SELECT * FROM Users " +
                         "WHERE ManagerId = @ManagerId";
            using (var db = new DbContext())
            {
                return db.Connection.Query<User>(sql, new { ManagerId = managerId });
            }
        }
    }
}