using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using System.Collections.Generic;

namespace DevBridgeAPI.UseCases
{
    public interface ITopicLogic
    {
        IEnumerable<LearntTopicsPerUser> GetSubordinatesLearntTopics(int managerId);
        IEnumerable<Topic> GetAll();
    }
}