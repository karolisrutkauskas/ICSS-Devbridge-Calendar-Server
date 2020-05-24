using DevBridgeAPI.Models;
using DevBridgeAPI.Models.Patch;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Dao;
using DevBridgeAPI.Resources;
using DevBridgeAPI.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace DevBridgeAPI.Controllers
{
    public class AssignmentsController : ApiController
    {
        private readonly IAssignmentLogic asignLogic;

        public AssignmentsController(IAssignmentLogic asignLogic)
        {
            this.asignLogic = asignLogic;
        }


        /// <summary>
        /// Gets a complete list of assignment data
        /// </summary>
        /// <returns>A full list of assignments</returns>
        [Authorize]
        [Route("api/assignments")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Assignment>))]
        public IHttpActionResult Get()
        {
            try {
                var identity = User.Identity;
                return Ok(asignLogic.SelectAllAssignmentsByUser(identity.Name)); 
            }
            catch (SystemException ex) 
            {
                System.Diagnostics.Trace.TraceError(ex.StackTrace);
                System.Diagnostics.Trace.TraceError(ex.Message);
                System.Diagnostics.Trace.TraceError(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }

        [Authorize]
        [Route("api/assignments/manager")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Assignment>))]

        public IHttpActionResult GetSubordinatesAssignments()
        {
            try {
                var identity = User.Identity;
                return Ok(asignLogic.FindSubordinatesAssignments(identity.Name)); 
            }
            catch (SystemException ex)
            {
                System.Diagnostics.Trace.TraceError(ex.StackTrace);
                System.Diagnostics.Trace.TraceError(ex.Message);
                System.Diagnostics.Trace.TraceError(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }

        [Authorize]
        [Route("api/assignments")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] Assignment assignment)
        {
            try { asignLogic.AddAssignment(assignment); }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
            return Ok();
        }

        [Authorize]
        [Route("api/assignments/{id}")]
        [HttpPatch]
        public IHttpActionResult Patch([FromBody] UpdatedAssignment assignmentUpdate, int id)
        {
            return Ok(asignLogic.UpdateAssignment(assignmentUpdate, id));
        }
    }
}
