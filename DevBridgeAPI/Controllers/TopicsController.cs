using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using System.Web.Http;
using System.Linq;

namespace DevBridgeAPI.Controllers
{
    public class TopicsController : ApiController
    {
        // GET api/users
        public IHttpActionResult Get()
        {
            IModelSelector selector = new TopicsSelector(); // Gal kokį Dependency Injection panaudoti
            return Ok(selector.SelectAllRows().Cast<Topic>());
        }
    }
}
