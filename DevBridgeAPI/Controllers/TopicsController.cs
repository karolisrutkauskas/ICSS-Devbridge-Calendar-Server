﻿using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using System.Web.Http;
using System.Linq;

namespace DevBridgeAPI.Controllers
{
    public class TopicsController : ApiController
    {
        private readonly IModelSelector selector;

        public TopicsController(IModelSelector selector)
        {
            this.selector = selector;
        }

        [Authorize]
        public IHttpActionResult Get()
        {

            return Ok(selector.SelectAllRows().Cast<Topic>());
        }
    }
}
