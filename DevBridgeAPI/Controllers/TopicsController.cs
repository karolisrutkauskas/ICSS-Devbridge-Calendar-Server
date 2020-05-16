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
        /// Will request for learnt topics for each user under specified manager
        /// </summary>
        /// <remarks>
        /// Learnt topic - a topic that a user has completed learning at some point
        /// Error codes:<br/>
        /// 6: User with provided email not found<br/>
        /// </remarks>
        /// <param name="managerId">Manager ID whose subordinates learnt topics should be returned</param>
        /// <returns>A list of learnt topics</returns>
        [Route("api/topics/teamLearntTopics/{managerId}")]
        [HttpGet]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Description = "A list of learnt topics for each member in team", Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "User with provided id not found", Type = typeof(ErrorMessage))]
        public IHttpActionResult GetLearntTopicsByTeamManager(int managerId)
        {
            return Ok(topicLogic.GetSubordinatesLearntTopics(managerId));   
        }
    }
}
