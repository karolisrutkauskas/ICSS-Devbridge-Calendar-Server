using Dapper;
using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Complex;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Resources;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DevBridgeAPI.UseCases.UserCasesN;

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
        public IHttpActionResult RegisterUser()
        {
            throw new NotImplementedException();
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
