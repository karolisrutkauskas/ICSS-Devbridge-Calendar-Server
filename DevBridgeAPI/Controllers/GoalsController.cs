using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using System.Linq;
using System.Web.Http;

namespace DevBridgeAPI.Controllers
{
    public class GoalsController : ApiController
    {
        // GET api/users
        public IHttpActionResult Get()
        {
            IModelSelector selector = new GoalsSelector(); // Gal kokį Dependency Injection panaudoti
            return Ok(selector.SelectAllRows().Cast<Goal>());
        }
    }
}
