using DevBridgeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostTopic = DevBridgeAPI.Models.Post.Topic;

namespace DevBridgeAPI.Repository.Dao
{
    public interface ITopicsDao
    {
        IEnumerable<Topic> SelectAllRows();
        Topic SelectById(int topicId);
        Topic InsertTopic(PostTopic topic);
        void UpdateTopic(Topic topic);
        IEnumerable<Topic> SelectLearnt(int userId);
        IEnumerable<Topic> SelectHistory(int topicId, int count);
    }
}
