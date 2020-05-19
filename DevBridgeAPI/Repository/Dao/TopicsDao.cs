using DevBridgeAPI.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PostTopic = DevBridgeAPI.Models.Post.Topic;
using Dapper.Contrib.Extensions;

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

        public Topic SelectById(int topicId)
        {
            string sql = "SELECT * FROM Topics " +
                         "WHERE TopicId = @TopicId";
            using (var db = new DbContext())
            {
                return db.Connection.Query<Topic>(sql, new { TopicId = topicId } ).FirstOrDefault();
            }
        }

        public Topic InsertTopic(PostTopic topic)
        {
            using (var db = new DbContext())
            {
                var insertedId = (int)db.Connection.Insert(topic);
                return SelectById(insertedId);
            }
        }
        public void UpdateTopic(Topic topic)
        {
            using (var db = new DbContext())
            {
                db.Connection.Update(topic);
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