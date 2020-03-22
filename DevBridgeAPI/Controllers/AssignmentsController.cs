using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using System.Linq;
using System.Web.Http;

namespace DevBridgeAPI.Controllers
{
    public class AssignmentsController : ApiController
    {
        // GET api/users
        public IHttpActionResult Get()
        {
            IModelSelector selector = new AssignmentsSelector(); // Gal kokį Dependency Injection panaudoti
            return Ok(selector.SelectAllRows().Cast<Assignment>());
        }
    }
}
