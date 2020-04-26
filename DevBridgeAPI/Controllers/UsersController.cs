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

namespace DevBridgeAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IModelSelector selector;
        private readonly IUserLogic userLogic;

        public UsersController(IModelSelector selector, IUserLogic userLogic)
        {
            this.selector = selector;
            this.userLogic = userLogic;
        }

        [Authorize]
        [Route("api/users")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(selector.SelectAllRows().Cast<User>());
        }

        [Route("api/users")]
        [HttpPost]
        public HttpResponseMessage RegisterUser([FromBody] User newUser)
        {
            try
            {
                if (newUser == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, value: "Request body is empty");
                }
                if (newUser.ManagerId == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, value: "Manager ID should be provided");
                }
                userLogic.RegisterNewUser(newUser.ManagerId.Value, newUser);
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            catch(UniqueFieldException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, value: ex.Message);
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }

        /// <summary>
        /// Gets a tree represantation of team hierarchy
        /// starting from the specified root user
        /// </summary>
        /// <param name="rootUserId">Root user's ID that will be at the top of team hierarchy</param>
        /// <returns></returns>
        [Route("api/users/teamTree/{rootUserId}")]
        [HttpGet]
        [ResponseType(typeof(TeamTreeNode))]
        public IHttpActionResult GetTeamTree(int rootUserId)
        {
            try
            {
                return Ok(userLogic.GetTeamTree(rootUserId));
            }
            catch(SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }
    }
}
