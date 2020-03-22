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
        // GET api/users
        public IHttpActionResult Get()
        {
            IModelSelector selector = new UsersSelector(); // Gal kokį Dependency Injection panaudoti
            return Ok(selector.SelectAllRows().Cast<User>());
        }
    }
}
