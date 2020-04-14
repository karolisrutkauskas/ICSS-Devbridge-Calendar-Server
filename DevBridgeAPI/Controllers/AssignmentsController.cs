using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
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
        [Route("api/assignments")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Assignment>))]
        public IHttpActionResult Get()
        {
            try { return Ok(asignLogic.SelectAllAssignments()); }
            catch (SystemException ex) 
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }

        [Route("api/assignments/user/{userId}")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Assignment>))]

        public IHttpActionResult GetUsersAssignments(int userId)
        {
            try { return Ok(asignLogic.FindAssignments(userId)); }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: Strings.GenericHttpError);
            }
        }

        [Route("api/assignments/manager/{managerId}")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Assignment>))]

        public IHttpActionResult GetSubordinatesAssignments(int managerId)
        {
            try { return Ok(asignLogic.FindSubordinatesAssignments(managerId)); }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
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
    }
}
