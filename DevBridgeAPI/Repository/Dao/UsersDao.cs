﻿using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.Contrib.Extensions;
using User = DevBridgeAPI.Models.User;
using PostUser = DevBridgeAPI.Models.Post.User;
using DevBridgeAPI.Models.Patch;

namespace DevBridgeAPI.Repository.Dao
{
    public class UsersDao : IModelSelector, IUsersDao
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

        public void InsertNewUser(PostUser user)
        {
            using (var db = new DbContext())
            {
                db.Connection.Insert(user);
            }
        }

        public void UpdateUser(User updatedUser)
        {
            using (var db = new DbContext())
            {
                db.Connection.Update(updatedUser);
            }
        }
        public void UpdateUserAsync(User updatedUser)
        {
            using (var db = new DbContext())
            {
                db.Connection.UpdateAsync(updatedUser);
            }
        }

        public void UpdateGlobalRestrictions(UserRestrictions restrictions)
        {
            using (var db = new DbContext())
            {
                db.Connection.Execute("UPDATE Users SET " +
                                      "ConsecLimit = @Consec, " +
                                      "MonthlyLimit = @Monthly, " +
                                      "YearlyLimit = @Yearly",
                                      new { Consec = restrictions.ConsecLimit,
                                            Monthly = restrictions.MonthlyLimit,
                                            Yearly = restrictions.YearlyLimit });
            }
        }

        public void UpdateTeamRestrictions(UserRestrictions restrictions, int managerId)
        {
            using (var db = new DbContext())
            {
                db.Connection.Execute("UPDATE Users SET " +
                                      "ConsecLimit = @Consec, " +
                                      "MonthlyLimit = @Monthly, " +
                                      "YearlyLimit = @Yearly " +
                                      "WHERE ManagerId = @ManagerId",
                                      new { Consec = restrictions.ConsecLimit,
                                            Monthly = restrictions.MonthlyLimit,
                                            Yearly = restrictions.YearlyLimit,
                                            ManagerId = managerId });
            }
        }

    }
}