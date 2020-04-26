using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using System.Linq;
using System.Web.Http;

namespace DevBridgeAPI.Controllers
{
    public class GoalsController : ApiController
    {
        private readonly IModelSelector selector;

        public GoalsController(IModelSelector selector)
        {
            this.selector = selector;
        }

        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(selector.SelectAllRows().Cast<Goal>());
        }
    }
}
