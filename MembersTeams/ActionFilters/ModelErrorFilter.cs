using MembersTeams.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace MembersTeams.API.ActionFilters
{
    public class ModelErrorFilter : IAsyncResultFilter
    {
        private readonly IModelErrorsContext _modelErrorContext;

        public ModelErrorFilter(IModelErrorsContext modelErrorContext)
        {
            _modelErrorContext = modelErrorContext;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            ObjectResult objectResult = (ObjectResult)context.Result;
            if (_modelErrorContext.HasErrors())
            {
                objectResult.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            await next();
        }
    }
}
