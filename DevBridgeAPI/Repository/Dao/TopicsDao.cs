using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.Repository.Dao
{
    public class TopicsDao : ITopicsDao
    {
        public IEnumerable<Topic> SelectAllRows()
        {
            string sql = "SELECT * FROM Topics";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Topic>(sql);
            }
        }

        public IEnumerable<Topic> SelectLearnt(int userId)
        {
            string sql = "SELECT * FROM Topics t " +
                         "JOIN LearntTopics lt " +
                         "ON t.TopicId = lt.TopicId " +
                         "WHERE lt.UserId = @UserId";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Topic>(sql, new { UserId = userId });
            }
        }
    }
}