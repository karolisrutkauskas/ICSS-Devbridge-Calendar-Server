﻿using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.UseCases.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PlannedTopic = DevBridgeAPI.Models.Complex.PlannedTopicsPerUser.PlannedTopic;

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
                var topics = topicsDao.SelectLearnt(managerId);
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
    }
}