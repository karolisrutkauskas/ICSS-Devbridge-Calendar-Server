using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Dao;
using System.Web.Http;
using System.Linq;

namespace DevBridgeAPI.Controllers
{
    public class TopicsController : ApiController
    {
        private readonly ITopicsDao topicsDao;

        public TopicsController(ITopicsDao topicsDao)
        {
            this.topicsDao = topicsDao;
        }

        [Authorize]
        public IHttpActionResult Get()
        {
            return Ok(topicsDao.SelectAllRows().Cast<Topic>());
        }
    }
}
