using DevBridgeAPI.Models;
using DevBridgeAPI.Repository;
using DevBridgeAPI.Repository.Selector;
using DevBridgeAPI.UseCases;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace DevBridgeAPI.Controllers
{
    public class AssignmentsController : ApiController
    {
        private const string genericError = "Unexpected error";
        private readonly IAssignmentLogic asignLogic;

        public AssignmentsController(IAssignmentLogic asignLogic)
        {
            this.asignLogic = asignLogic;
        }

        [Route("assignments")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try { return Ok(asignLogic.SelectAllAssignments()); }
            catch (SystemException ex) 
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: genericError);
            }
        }

        [Route("assignments/user/{userId}")]
        [HttpGet]
        public IHttpActionResult GetUsersAssignments(int userId)
        {
            try { return Ok(asignLogic.FindAssignments(userId)); }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: genericError);
            }
        }

        [Route("assignments/manager/{managerId}")]
        [HttpGet]
        public IHttpActionResult GetSubordinatesAssignments(int managerId)
        {
            try { return Ok(asignLogic.FindSubordinatesAssignments(managerId)); }
            catch (SystemException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.Source);
                throw new HttpException(httpCode: 500, message: genericError);
            }
        }
    }
}
