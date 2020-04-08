using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using System.Linq;
using System.Web.Http;

namespace DevBridgeAPI.Controllers
{
    public class ConstraintsController : ApiController
    {
        private readonly IModelSelector selector;

        public ConstraintsController(IModelSelector selector)
        {
            this.selector = selector;
        }

        // GET api/users
        public IHttpActionResult Get()
        {
            return Ok(selector.SelectAllRows().Cast<Constraint>());
        }
    }
}
