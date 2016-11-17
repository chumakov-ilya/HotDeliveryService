using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bringo.HotDeliveryService.Core;

namespace Bringo.HotDeliveryService.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private CancellationTokenSource _cts;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StartJobs();
        }

        protected void Application_End()
        {
            StopJobs();
        }

        public void StartJobs()
        {
            Trace.WriteLine(MethodBase.GetCurrentMethod().Name);

            _cts = new CancellationTokenSource();

            var scheduler = DiRoot.Resolve<Scheduler>();
            var createJob = DiRoot.Resolve<CreateJob>();
            var expireJob = DiRoot.Resolve<ExpireJob>();

            Task.Run(() => scheduler.Run(_cts.Token, createJob, expireJob));
        }

        private void StopJobs()
        {
            Trace.WriteLine(MethodBase.GetCurrentMethod().Name);

            _cts.Cancel();
        }
    }
}
