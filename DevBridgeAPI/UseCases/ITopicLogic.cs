using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using System.Collections.Generic;
using PostTopic = DevBridgeAPI.Models.Post.Topic;

namespace DevBridgeAPI.UseCases
{
    public interface ITopicLogic
    {
        IEnumerable<LearntTopicsPerUser> GetSubordinatesLearntTopics(int managerId);
        IEnumerable<Topic> GetAll();
        Topic GetById(int topicId);
        IEnumerable<PlannedTopicsPerUser> GetSubordinatesPlannedTopics(int managerId);
        IEnumerable<User> GetUsersWithPastTopicAssignment(int topicId, int managerId);
        IEnumerable<TeamStatsPerTopic> GetTeamsWithPastTopicAssignment(int topicId, int managerId);
        Topic InsertOrUpdateTopic(PostTopic topic, int changeByUserId, int? topicId = null);
    }
}