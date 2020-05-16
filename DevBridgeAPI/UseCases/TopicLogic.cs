using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.UseCases.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBridgeAPI.UseCases
{
    public class TopicLogic : ITopicLogic
    {
        private readonly IUsersDao usersDao;
        private readonly ITopicsDao topicsDao;

        public TopicLogic(IUsersDao usersDao, ITopicsDao topicsDao)
        {
            this.usersDao = usersDao;
            this.topicsDao = topicsDao;
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
                var topics = topicsDao.SelectLearnt(managerId);
                learntTopics.AddLast(new LearntTopicsPerUser { User = user, Topics = topics });
            }
            return learntTopics;
        }
    }
}