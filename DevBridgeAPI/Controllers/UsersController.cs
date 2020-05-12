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

        //TODO: add ModelStateDictionary as swagger return type
        /// <summary>
        /// Will register a new user with already assigned manager.
        /// </summary>
        /// <param name="newUser">New user to be inserted into database</param>
        /// <returns>Described at responses</returns>
        [Authorize]
        [Route("api/users")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Successful request, no body content in response")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "User was not posted, request failed validations", Type = typeof(string))]
        [SwaggerResponseRemoveDefaults]
        [ValidateRequest]
        public IHttpActionResult RegisterUser([FromBody] User newUser)
        {
            try
            {
                userLogic.RegisterNewUser(newUser);
                return Ok();
            }
            catch(UniqueFieldException ex)
            {
                return BadRequest(ex.Message);
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
        /// <returns>A tree of users with subordinates as children starting from rootUser</returns>
        [Authorize]
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

        [Authorize]
        [Route("api/users/teamTree")]
        [HttpGet]
        [ResponseType(typeof(TeamTreeNode))]
        public IHttpActionResult GetTeamTree()
        {
            var identity = User.Identity;
            return Ok(userLogic.GetTeamTree(identity.Name));
        }

        [Route("api/users/restrictions/{userId}")]
        [HttpPatch]
        [ValidateRequest]
        public IHttpActionResult ChangeRestrictions([FromBody] UserRestrictions userRestrictions, int userId)
        {
            try
            {
                return Ok(userLogic.ChangeRestrictions(userRestrictions, userId));
            }
            catch (EntityNotFoundException ex)
            {
                using (var response = Request.CreateResponse((HttpStatusCode.NotFound, ex.Message)))
                    return ResponseMessage(response);
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }

        [Route("api/users/restrictions/global")]
        [HttpPatch]
        [ValidateRequest]
        public IHttpActionResult ChangeGlobalRestrictions([FromBody] UserRestrictions userRestrictions)
        {
            try
            {
                userLogic.ChangeGlobalRestrictions(userRestrictions);
                return Ok();
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }

        [Route("api/users/restrictions/team/{managerId}")]
        [HttpPatch]
        [ValidateRequest]
        public IHttpActionResult ChangeTeamRestrictions([FromBody] UserRestrictions userRestrictions, int managerId)
        {
            try
            {
                userLogic.ChangeTeamRestrictions(userRestrictions, managerId);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, ex.Message));
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }

        [Route("api/users/manager/{userId}")]
        [HttpPatch]
        [ValidateRequest]
        public IHttpActionResult ChangeTeamManager([FromBody] UserManagerId newManagerId, int userId)
        {
            try
            {
                return Ok(userLogic.ChangeTeamMember(newManagerId.ManagerId.Value, userId));
            }
            catch (ValidationFailedException ex)
            {
                int i = 1;
                ModelState.Clear();
                foreach (var errorMessage in ex.ValidationInfo.ErrorMessages)
                {
                    ModelState.AddModelError($"error {i++}", errorMessage);
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ex.ValidationInfo));
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }
    }
#pragma warning restore CA2000 // Dispose objects before losing scope
}
