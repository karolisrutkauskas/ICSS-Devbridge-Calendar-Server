using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.UseCases.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using PlannedTopic = DevBridgeAPI.Models.Complex.PlannedTopicsPerUser.PlannedTopic;
using PostTopic = DevBridgeAPI.Models.Post.Topic;

namespace DevBridgeAPI.UseCases
{
    public class TopicLogic : ITopicLogic
    {
        private readonly IUsersDao usersDao;
        private readonly ITopicsDao topicsDao;
        private readonly IAssignmentsDao asgnDao;

        public TopicLogic(IUsersDao usersDao, ITopicsDao topicsDao, IAssignmentsDao asgnDao)
        {
            this.usersDao = usersDao;
            this.topicsDao = topicsDao;
            this.asgnDao = asgnDao;
        }

        public IEnumerable<Topic> GetAll()
        {
            return topicsDao.SelectAllRows();
        }

        public IEnumerable<LearntTopicsPerUser> GetSubordinatesLearntTopics(int managerId)
        {
            if (usersDao.SelectByID(managerId) == null)
            {
                throw new EntityNotFoundException($"Manager with id {managerId} not found", typeof(User));
            }
            var learntTopics = new LinkedList<LearntTopicsPerUser>();
            var subordinates = usersDao.SelectSubordinates(managerId);
            foreach (var user in subordinates)
            {
                var topics = topicsDao.SelectLearnt(user.UserId);
                learntTopics.AddLast(new LearntTopicsPerUser { User = user, Topics = topics });
            }
            return learntTopics;
        }

        public IEnumerable<PlannedTopicsPerUser> GetSubordinatesPlannedTopics(int managerId)
        {
            if (usersDao.SelectByID(managerId) == null)
            {
                throw new EntityNotFoundException($"Manager with id {managerId} not found", typeof(User));
            }
            var plannedTopics = new LinkedList<PlannedTopicsPerUser>();
            var subordinates = usersDao.SelectSubordinates(managerId);
            foreach (var user in subordinates)
            {
                var topics = new PlannedTopicsPerUser { User = user, Topics = new LinkedList<PlannedTopic>() };
                foreach (var assignment in asgnDao.SelectPlannedInFuture(user.UserId))
                {
                    (topics.Topics as LinkedList<PlannedTopic>).AddLast(new PlannedTopic()
                    {
                        Topic = topicsDao.SelectById(assignment.TopicId),
                        AppointmentDate = assignment.Date
                    });
                }
                plannedTopics.AddLast(topics);
            }
            return plannedTopics;
        }
        public IEnumerable<User> GetUsersWithPastTopicAssignment(int topicId, int managerId)
        {
            if (topicsDao.SelectById(topicId) == null)
            {
                throw new EntityNotFoundException($"Topic with id {topicId} not found", typeof(Topic));
            }
            return usersDao.SelectByPastTopicAssignment(topicId, managerId);
        }

        public IEnumerable<TeamStatsPerTopic> GetTeamsWithPastTopicAssignment(int topicId, int managerId)
        {
            var users = GetUsersWithPastTopicAssignment(topicId, managerId);
            var teams = new Dictionary<int?, TeamStatsPerTopic>();

            foreach (var u in users)
            {
                if (u.ManagerId != null)
                {
                    if (teams.ContainsKey(u.ManagerId.Value))
                    {
                        teams[u.ManagerId].MemberCount++;
                    }
                    else
                    {
                        teams.Add(u.ManagerId, new TeamStatsPerTopic()
                        {
                            MemberCount = 1,
                            TeamManager = usersDao.SelectByID(u.ManagerId.Value),
                        });
                    }
                }
            }
            return teams.Values;
        }

        public Topic InsertOrUpdateTopic(PostTopic topic, int changeByUserId, int? topicId = null)
        {
            topic.ChangeByUserId = changeByUserId;
            if (topicId == null)
            {
                return topicsDao.InsertTopic(topic);
            }
            var topicToUpdate = topicsDao.SelectById(topicId.Value);
            if (topicToUpdate == null)
            {
                return topicsDao.InsertTopic(topic);
            } else
            {
                topicToUpdate.UpdateFields(topic);
                topicsDao.UpdateTopic(topicToUpdate);
                return topicToUpdate;
            }
        }

        public Topic GetById(int topicId)
        {
            var topic = topicsDao.SelectById(topicId);
            if (topic == null) throw new EntityNotFoundException($"Topic with ID {topicId} not found", typeof(Topic));
            return topic;
        }

        public IEnumerable<Topic> GetPrevVersions(int topicId, int maxCount)
        {
            if (topicsDao.SelectById(topicId) == null)
            {
                throw new EntityNotFoundException($"Topic with ID {topicId} not found", typeof(Topic));
            }
            return topicsDao.SelectHistory(topicId, maxCount);
        }

        public LearntTopicsPerUser GetLearntTopics(int userId)
        {
            var user = usersDao.SelectByID(userId);
            if (user == null)
            {
                throw new EntityNotFoundException($"User with ID {userId} not found", typeof(User));
            }
            return new LearntTopicsPerUser { User = user, Topics = topicsDao.SelectLearnt(userId) } ;
        }

        public void LearnTopic(int userId, int topicId)
        {
            try
            {
                topicsDao.AddLearntTopic(new LearntTopic { UserId = userId, TopicId = topicId });
            } catch (SqlException e) when(e.Number == 2627)
            {
                throw new UniqueFieldException($"Learnt topic with User ID {userId} and Topic ID {topicId} already exists",
                    nameof(LearntTopic.TopicId) + "," + nameof(LearntTopic.UserId));
            }
        }

        public void UnlearnTopic(int userId, int topicId)
        {
            topicsDao.RemoveLearntTopic(new LearntTopic { UserId = userId, TopicId = topicId });
        }
    }
}