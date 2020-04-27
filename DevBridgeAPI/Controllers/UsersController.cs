﻿using Dapper;
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

        /// <summary>
        /// Will register a new user with already assigned manager.
        /// FirstName, Las
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [Route("api/users")]
        [HttpPost]
        [ValidateRequest]
        public IHttpActionResult RegisterUser([FromBody] User newUser)
        {
            try
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                //if (newUser.FirstName == null)
                //{
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, value: "Email should be provided");
                //}
                //if (newUser.Email == null)
                //{
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, value: "Email should be provided");
                //}
                //if (newUser == null)
                //{
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, value: "Request body is empty");
                //}
                //if (newUser.ManagerId == null)
                //{
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, value: "Manager ID should be provided");
                //}
                //if (newUser.Email == null)
                //{
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, value: "Email should be provided");
                //}
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

        [Route("api/users/restrictions/{userId}")]
        [HttpPatch]
        [ValidateRequest]
        public IHttpActionResult ChangeRestrictions([FromBody] UserRestrictions userRestrictions, int userId)
        {
            try
            {
                return Ok(userLogic.ChangeRestrictions(userRestrictions, userId));
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
}
