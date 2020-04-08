using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using System.Web.Http;
using System.Linq;


namespace DevBridgeAPI.Controllers
{
    public class TeamsController : ApiController
    {
        private readonly IModelSelector selector;

        public TeamsController(IModelSelector selector)
        {
            this.selector = selector;
        }

        // GET api/users
        public IHttpActionResult Get()
        {
            return Ok(selector.SelectAllRows().Cast<Team>());
        }
    }
}
