using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.Contrib.Extensions;
using User = DevBridgeAPI.Models.User;
using PostUser = DevBridgeAPI.Models.Post.User;
using DevBridgeAPI.Models.Patch;
using System.Data.SqlClient;

namespace DevBridgeAPI.Repository.Dao
{
    public class UsersDao : IUsersDao
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

        public User SelectByEmail(string email)
        {
            string sql = "SELECT * FROM Users " +
                         "WHERE LOWER(Email) = LOWER(@Email)";
            using (var db = new DbContext())
            {
                return db.Connection.Query<User>(sql, new { Email = email }).FirstOrDefault();
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

        public User InsertAndReturnNewUser(PostUser user)
        {
            using (var db = new DbContext())
            {
                var insertedUserId = (int)db.Connection.Insert(user);
                return SelectByID(insertedUserId);
            }
        }

        public void UpdateUser(User updatedUser)
        {
            using (var db = new DbContext())
            {
                db.Connection.Update(updatedUser);
            }
        }

        public void UpdateGlobalRestrictions(UserRestrictions restrictions)
        {
            using (var db = new DbContext())
            {
                db.Connection.Execute("UPDATE Users SET " +
                                      "ConsecLimit = @Consec, " +
                                      "MonthlyLimit = @Monthly, " +
                                      "YearlyLimit = @Yearly, " +
                                      "QuarterLimit = @Quarter",
                                      new { Consec = restrictions.ConsecLimit,
                                            Monthly = restrictions.MonthlyLimit,
                                            Yearly = restrictions.YearlyLimit,
                                            Quarter = restrictions.QuarterLimit });
            }
        }

        public void UpdateTeamRestrictions(UserRestrictions restrictions, int managerId)
        {
            using (var db = new DbContext())
            {
                db.Connection.Execute("UPDATE Users SET " +
                                      "ConsecLimit = @Consec, " +
                                      "MonthlyLimit = @Monthly, " +
                                      "YearlyLimit = @Yearly, " +
                                      "QuarterLimit = @Quarter " +
                                      "WHERE ManagerId = @ManagerId",
                                      new { Consec = restrictions.ConsecLimit,
                                            Monthly = restrictions.MonthlyLimit,
                                            Yearly = restrictions.YearlyLimit,
                                            Quarter = restrictions.QuarterLimit,
                                            ManagerId = managerId });
            }
        }

        public bool IsAncestorOf(int ancestor, int descendant)
        {
            using (var db = new DbContext())
            {
                return db.Connection.ExecuteScalar<bool>("SELECT dbo.UserIsDescendantOf(@Ancestor, @Descendant) as isDesc",
                                                         new { Ancestor = ancestor, Descendant = descendant });
            }
        }

        public void UpdatePasswordClearToken(string hashedPassword, int userId)
        {
            using (var db = new DbContext())
            {
                db.Connection.Execute("UPDATE dbo.Users SET " +
                                      "Password = @Password, " +
                                      "RegistrationToken = NULL " +
                                      "WHERE UserId = @UserId",
                                      new { Password = hashedPassword,
                                            UserId = userId });
            }
        }

        public IEnumerable<User> SelectByPastTopicAssignment(int topicId, int ancestorId)
        {
            string sql = "SELECT * FROM Users u " +
                         "JOIN Assignments a " +
                         "ON a.UserId = u.UserId " +
                         "WHERE a.Date < GETDATE() " +
                         "AND a.TopicId = @TopicId " +
                         "AND (dbo.UserIsDescendantOf(@AncestorId, u.UserId) = 1 " +
                              "OR u.UserId = @AncestorId)";
            using (var db = new DbContext())
            {
                return db.Connection.Query<User>(sql, new { TopicId = topicId, AncestorId = ancestorId });
            }
        }

        public User SelectByRegToken(string token)
        {
            string sql = "SELECT * FROM Users " +
                         "WHERE RegistrationToken = @RegToken";
            using (var db = new DbContext())
            {
                return db.Connection.Query<User>(sql, new { RegToken = token }).FirstOrDefault();
            }
        }
    }
}