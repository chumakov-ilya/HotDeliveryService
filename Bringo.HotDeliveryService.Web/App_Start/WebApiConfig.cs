using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Bringo.HotDeliveryService.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
                .Add(new RequestHeaderMapping("Accept",
                                              "text/html",
                                              StringComparison.InvariantCultureIgnoreCase,
                                              true,
                                              "application/json"));

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }

    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            context.Result = new InternalServerErrorResult();
        }

        public class InternalServerErrorResult : IHttpActionResult
        {
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var message = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                message.Content = new StringContent(HttpStatusCode.InternalServerError.ToString());
                return Task.FromResult(message);
            }
        }
    }
}
