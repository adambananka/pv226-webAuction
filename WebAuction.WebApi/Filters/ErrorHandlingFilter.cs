using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebAuction.WebApi.Filters
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("Exception occured.")
            };
            base.OnException(actionExecutedContext);
        }
    }
}