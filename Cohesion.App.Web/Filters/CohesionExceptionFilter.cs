using Cohesion.Core.ServiceRequest.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Cohesion.App.Web.Filters
{
    public class CohesionExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is KeyNotFoundException)
            {
                context.Result = new NoContentResult();
            }
            
            if(context.Exception is ServiceRequestNotFoundException)
            {
                context.Result = new NotFoundResult();
            }
        }
    }
}
