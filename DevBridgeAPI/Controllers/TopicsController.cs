using DevBridgeAPI.Models;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using DevBridgeAPI.Models.Misc;
using DevBridgeAPI.UseCases;
using DevBridgeAPI.Models.Complex;
using System.Security.Claims;
using DevBridgeAPI.Helpers;

namespace DevBridgeAPI.Controllers
{
    public class TopicsController : ApiController
    {
        private readonly ITopicLogic topicLogic;

        public TopicsController(ITopicLogic topicLogic)
        {
            this.topicLogic = topicLogic;
        }

        [Route("api/topics")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(topicLogic.GetAll());
        }

        /// <summary>
        /// Will request for learnt topics for each direct subordinate under specified manager
        /// </summary>
        /// <remarks>
        /// Learnt topic - a topic that a user has completed learning at some point
        /// Error codes:<br/>
        /// 6: User with provided ID not found<br/>
        /// </remarks>
        /// <param name="managerId">Manager ID whose subordinates learnt topics should be returned</param>
        /// <returns>A list of learnt topics</returns>
        [Route("api/topics/teamLearnt/{managerId}")]
        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "A list of learnt topics for each member in team", Type = typeof(LearntTopicsPerUser))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Manager with provided id not found", Type = typeof(ErrorMessage))]
        public IHttpActionResult GetTeamLearntTopics(int managerId)
        {
            return Ok(topicLogic.GetSubordinatesLearntTopics(managerId));   
        }

        /// <summary>
        /// Will request for planned topics for each direct subordinate under specified manager
        /// </summary>
        /// <remarks>
        /// Planned topic - a topic where user has appointment of the topic sometime in the future
        /// Error codes:<br/>
        /// 6: User with provided ID not found<br/>
        /// </remarks>
        /// <param name="managerId">Manager ID whose subordinates planned topics should be returned</param>
        /// <returns>A list of learnt topics</returns>
        [Route("api/topics/teamPlanned/{managerId}")]
        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "A list of planned topics for each member in team", Type = typeof(PlannedTopicsPerUser))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Manager with provided id not found", Type = typeof(ErrorMessage))]
        public IHttpActionResult GetTeamPlannedTopics(int managerId)
        {
            return Ok(topicLogic.GetSubordinatesPlannedTopics(managerId));
        }

        /// <summary>
        /// Will request for users that have had an appointment for the 
        /// specified topic sometime in the past. Only lower than requesting 
        /// user's rank users will be returned.
        /// </summary>
        /// <remarks>
        /// Error codes:<br/>
        /// 6: Topic with provided ID not found<br/>
        /// </remarks>
        /// <param name="topicId">ID of topic that will be used for checking if user had past assignments</param>
        /// <returns>A list of users</returns>
        [Route("api/topics/{topicId}/usersWithPastAssignments")]
        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "A list of users that meet the criteria", Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Topic with provided id not found", Type = typeof(ErrorMessage))]
        public IHttpActionResult GetUsersByPastAssignments(int topicId)
        {
            int managerId = User.Identity.GetId();
            return Ok(topicLogic.GetUsersWithPastTopicAssignment(topicId, managerId));
        }

        /// <summary>
        /// Will request for teams with counts of users who have had an appointment for the 
        /// specified topic sometime in the past. Only lower than requesting 
        /// team's rank users will be returned.
        /// </summary>
        /// <remarks>
        /// Error codes:<br/>
        /// 6: Topic with provided ID not found<br/>
        /// </remarks>
        /// <param name="topicId">ID of topic that will be used for checking if team had past assignments</param>
        /// <returns>A list of teams with user counts</returns>
        [Route("api/topics/{topicId}/teamsWithPastAssignments")]
        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "A list of teams with user counts that meet the criteria", Type = typeof(TeamStatsPerTopic))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Topic with provided id not found", Type = typeof(ErrorMessage))]
        public IHttpActionResult GetTeamsByPastAssignments(int topicId)
        {
            int managerId = User.Identity.GetId();
            return Ok(topicLogic.GetTeamsWithPastTopicAssignment(topicId, managerId));
        }
    }
}
