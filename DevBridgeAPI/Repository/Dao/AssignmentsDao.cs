using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Dao
{
    public class AssignmentsDao : IModelSelector, IAssignmentsDao
    {
        public IEnumerable<IModel> SelectAllRows()
        {
            string sql = "SELECT * FROM Assignments";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Assignment>(sql);
            }
        }

        public IEnumerable<Assignment> SelectByUserId(int userId)
        {
            string sql = "SELECT * FROM Assignments " +
                         "WHERE UserId = @userId";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Assignment>(sql, new { userId });
            }
        }

        public void AddRow(Assignment assignment)
        {
            string sql = "INSERT INTO Assignments " +
                         "VALUES (@userId, @topicId, @stateId, @comments, @date)";
            using (var db = new DbContext())
            {
                db.Connection.Query<Assignment>(sql, new { assignment.UserId, assignment.TopicId, assignment.StateId, assignment.Comments, assignment.Date });
            }
        }
    }
}