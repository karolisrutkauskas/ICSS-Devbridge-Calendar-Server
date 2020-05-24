using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Dao
{
    public class AssignmentsDao : IAssignmentsDao
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

        public IEnumerable<Assignment> SelectPlannedInFuture(int userId)
        {
            string sql = "SELECT * FROM Assignments " +
                         "WHERE UserId = @UserId " +
                         "AND Date > GETDATE()";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Assignment>(sql, new { UserId = userId });
            }
        }

        public Assignment SelectRow(int assignmentId)
        {
            string sql = "SELECT * FROM Assignments " +
                         "WHERE AsgnId = @Id";

            using (var db = new DbContext())
            {
                return db.Connection.Query<Assignment>(sql, new { Id = assignmentId }).First();
            }
        }

        public void UpdateRow(Assignment assignment)
        {
            using (var db = new DbContext())
            {
                db.Connection.Execute("UPDATE Assignments " +
                    "SET TopicId = @TopicId, Comments = @Comments, Date = @Date " +
                    "WHERE AsgnId = @Id",
                    new { Id = assignment.AsgnId, TopicId = assignment.TopicId, Comments = assignment.Comments, Date = assignment.Date }
                    );
            }
        }
    }
}