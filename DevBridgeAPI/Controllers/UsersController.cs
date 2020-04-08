using Dapper;
using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DevBridgeAPI.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IModelSelector selector;

        public UsersController(IModelSelector selector)
        {
            this.selector = selector;
        }

        // GET api/users
        public IHttpActionResult Get()
        {
            return Ok(selector.SelectAllRows().Cast<User>());
        }
    }
}
