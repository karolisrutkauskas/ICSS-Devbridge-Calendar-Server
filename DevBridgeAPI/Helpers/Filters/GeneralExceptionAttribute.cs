using DevBridgeAPI.Models.Misc;
using DevBridgeAPI.Resources;
using DevBridgeAPI.UseCases.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace DevBridgeAPI.Helpers.Filters
{
    public class GeneralExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpResponseMessage response;
            if (exceptionHandlerMap.TryGetValue(actionExecutedContext.Exception.GetType(),
                out var exceptionHandler))
            {
                response = exceptionHandler.Invoke(actionExecutedContext);
            } else
            {
                System.Diagnostics.Debug.WriteLine(actionExecutedContext.Exception.StackTrace);
                System.Diagnostics.Debug.WriteLine(actionExecutedContext.Exception.Message);
                System.Diagnostics.Debug.WriteLine(actionExecutedContext.Exception.Source);
                var singletonError = Enumerable.Repeat(Errors.GenericError(), 1);
                response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, singletonError);
            }

            throw new HttpResponseException(response);
        }


        // FYI, for including more exception filters just add another dictionary value
        private readonly Dictionary<Type, Func<HttpActionExecutedContext, HttpResponseMessage>> exceptionHandlerMap 
            = new Dictionary<Type, Func<HttpActionExecutedContext, HttpResponseMessage>>
        {
            [typeof(ApiException)] = (aec =>
            {
                var ex = aec.Exception as ApiException;
                return aec.Request.CreateResponse(ex.StatusCode, ex.Errors);
            }),

            [typeof(ValidationFailedException)] = (aec =>
            {
                var ex = aec.Exception as ValidationFailedException;
                var errors = ex.ValidationInfo.ErrorMessages;
                return aec.Request.CreateResponse(HttpStatusCode.BadRequest, errors);
            }),

            [typeof(EntityNotFoundException)] = (aec =>
            {
                var ex = aec.Exception as EntityNotFoundException;
                var singletonError = Enumerable.Repeat(Errors.EntityNotFound(ex.EntityType), 1);
                return aec.Request.CreateResponse(HttpStatusCode.NotFound, singletonError);
            }),

            [typeof(UniqueFieldException)] = (aec =>
            {
                var ex = aec.Exception as UniqueFieldException;
                var singletonError = Enumerable.Repeat(Errors.UniqueValueViolation(ex.FieldName), 1);
                return aec.Request.CreateResponse(HttpStatusCode.Conflict, singletonError);
            })
        };
    }
}