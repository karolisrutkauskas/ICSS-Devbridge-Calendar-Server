using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using System.Linq;
using System.Web.Http;

namespace DevBridgeAPI.Controllers
{
    public class AssignmentsController : ApiController
    {
        private readonly IModelSelector selector;

        public AssignmentsController(IModelSelector selector)
        {
            this.selector = selector;
        }

        // GET api/users
        public IHttpActionResult Get()
        {
            return Ok(selector.SelectAllRows().Cast<Assignment>());
        }
    }
}
