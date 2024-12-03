using Identity.Domain.Common.ModelErrors;
using Identity.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Identity.Extensions
{
    public class DomainResult<T>
    {
        private readonly INotificationContext _notificationContext;
        private readonly IModelErrorsContext _modelErrorsContext;

        public Dictionary<string, string> Notifications => _notificationContext.Notifications.ToDictionary(e => e.Key, e => e.Message);
        public List<ModelErrors> ModelErrors => _modelErrorsContext.ModelErrors.ToList();
        public bool Success => !_notificationContext.HasNotifications() && !_modelErrorsContext.HasErrors();
        public T Value { get; set; }

        public string RequestUrl { get; set; }

        private static string GetRequestUrlFromHttpContext(HttpContext context)
        {
            return context.Items.ContainsKey("RequestUrl") ? context.Items["RequestUrl"]?.ToString() ?? "" : "";
        }

        internal DomainResult(T value, INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, string requestUrl = "")
        {
            _notificationContext = notificationContext;
            _modelErrorsContext = modelErrorsContext;
            Value = value;
            RequestUrl = requestUrl;
        }

        public static IActionResult Ok(T value, INotificationContext notificationContext, IModelErrorsContext modelErrorsContext)
        {
            var context = new HttpContextAccessor().HttpContext;
            var requestUrl = context != null ? GetRequestUrlFromHttpContext(context) : "";

            return new ObjectResult(null)
            {
                StatusCode = (int)HttpStatusCode.OK,
                Value = new DomainResult<T>(value, notificationContext, modelErrorsContext, requestUrl)
            };
        }

        public static IActionResult Failure(T errorMessage, INotificationContext notificationContext, IModelErrorsContext modelErrorsContext, HttpStatusCode statusCode)
        {
            var context = new HttpContextAccessor().HttpContext;
            var requestUrl = context != null ? GetRequestUrlFromHttpContext(context) : "";
            DomainResult<T> result = new(errorMessage, notificationContext, modelErrorsContext, requestUrl);

            return new ObjectResult(null)
            {
                StatusCode = (int)statusCode,
                Value = result
            };
        }

    }
}
