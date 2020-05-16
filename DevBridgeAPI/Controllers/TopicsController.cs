using DevBridgeAPI.Models;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using DevBridgeAPI.Models.Misc;
using DevBridgeAPI.UseCases;

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
        [SwaggerResponse(HttpStatusCode.OK, Description = "A list of learnt topics for each member in team", Type = typeof(User))]
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
        [SwaggerResponse(HttpStatusCode.OK, Description = "A list of planned topics for each member in team", Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Manager with provided id not found", Type = typeof(ErrorMessage))]
        public IHttpActionResult GetTeamPlannedTopics(int managerId)
        {
            return Ok(topicLogic.GetSubordinatesPlannedTopics(managerId));
        }
    }
}
