using Dapper;
using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DevBridgeAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IModelSelector selector;

        public UsersController(IModelSelector selector)
        {
            this.selector = selector;
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
        [ResponseType(typeof(IEnumerable<Assignment>))]
        public IHttpActionResult RegisterUser()
        {
            throw new NotImplementedException();
        }
    }
}
