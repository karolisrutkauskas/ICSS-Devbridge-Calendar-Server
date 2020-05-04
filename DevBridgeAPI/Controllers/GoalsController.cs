using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.Resources;
using DevBridgeAPI.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace DevBridgeAPI.Controllers
{
    public class GoalsController : ApiController
    {
        private readonly IGoalsLogic goalsLogic;

        public GoalsController(IGoalsLogic goalsLogic)
        {
            this.goalsLogic = goalsLogic;
        }

        [Authorize]
        [Route("api/goals")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Goal>))]
        public IHttpActionResult Get()
        {
            try
            {
                var identity = User.Identity;
                return Ok(goalsLogic.SelectAllGoalsByUser(identity.Name));
            }

            catch (SystemException ex)
            {
                System.Diagnostics.Trace.TraceError(ex.StackTrace);
                System.Diagnostics.Trace.TraceError(ex.Message);
                System.Diagnostics.Trace.TraceError(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }
    }
}
