using Infrastructures.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Infrastructures.API.ActionFilters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationContext _notificationContext;

        public NotificationFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            ObjectResult objectResult = (ObjectResult)context.Result;
            if (_notificationContext.HasNotifications())
            {
                objectResult.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            await next();
        }
    }
}
