using Dapper;
using DevBridgeAPI.Models.Post;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Resources;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DevBridgeAPI.UseCases.UserLogicN;
using System.Net.Http;
using System.Net;
using DevBridgeAPI.UseCases.Exceptions;
using DevBridgeAPI.Helpers;
using DevBridgeAPI.Models.Patch;
using DevBridgeAPI.Models.Misc;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;

namespace DevBridgeAPI.Controllers
{
#pragma warning disable CA2000 // Dispose objects before losing scope

    public class UsersController : ApiController
    {
        private readonly IUserLogic userLogic;

        public UsersController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        /// <summary>
        /// Will register a new user with already assigned manager.
        /// </summary>
        /// <param name="newUser">New user to be inserted into database</param>
        /// <returns>Described at responses</returns>
        [Authorize]
        [Route("api/users")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Successful request, Return posted user", Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Request failed validations", Type = typeof(IEnumerable<ErrorMessage>))]
        [SwaggerResponse(HttpStatusCode.Conflict, Description = "Provided email already exists", Type = typeof(IEnumerable<ErrorMessage>))]
        [SwaggerResponseRemoveDefaults]
        [ValidateRequest]
        public IHttpActionResult RegisterUser([FromBody] User newUser)
        {
            return Ok(userLogic.RegisterNewUser(newUser));
        }

        /// <summary>
        /// Gets a tree represantation of team hierarchy
        /// starting from the specified root user
        /// </summary>
        /// <param name="rootUserId">Root user's ID that will be at the top of team hierarchy</param>
        /// <returns>A tree of users with subordinates as children starting from rootUser</returns>
        [Authorize]
        [Route("api/users/teamTree/{rootUserId}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Successful request, Return team tree", Type = typeof(TeamTreeNode))]

        public IHttpActionResult GetTeamTree(int rootUserId)
        {
            return Ok(userLogic.GetTeamTree(rootUserId));
        }

        [Route("api/users/restrictions/{userId}")]
        [HttpPatch]
        [ValidateRequest]
        public IHttpActionResult ChangeRestrictions([FromBody] UserRestrictions userRestrictions, int userId)
        {
            return Ok(userLogic.ChangeRestrictions(userRestrictions, userId));
        }

        [Route("api/users/restrictions/global")]
        [HttpPatch]
        [ValidateRequest]
        public IHttpActionResult ChangeGlobalRestrictions([FromBody] UserRestrictions userRestrictions)
        {
            userLogic.ChangeGlobalRestrictions(userRestrictions);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }

        [Route("api/users/restrictions/team/{managerId}")]
        [HttpPatch]
        [ValidateRequest]
        public IHttpActionResult ChangeTeamRestrictions([FromBody] UserRestrictions userRestrictions, int managerId)
        {
            userLogic.ChangeTeamRestrictions(userRestrictions, managerId);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }

        [Route("api/users/manager/{userId}")]
        [HttpPatch]
        [ValidateRequest]
        public IHttpActionResult ChangeTeamManager([FromBody] UserManagerId newManagerId, int userId)
        {
            return Ok(userLogic.ChangeTeamMember(newManagerId.ManagerId.Value, userId));  
        }
    }
#pragma warning restore CA2000 // Dispose objects before losing scope
}
