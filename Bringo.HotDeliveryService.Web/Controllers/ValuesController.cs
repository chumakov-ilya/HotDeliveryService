using Bringo.HotDeliveryService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Bringo.HotDeliveryService.Core.Configs;
using Ninject;

namespace Bringo.HotDeliveryService.Web.Controllers
{
    public class ValuesController : ApiController
    {
        [Inject]
        public IRepository Repository { get; set; }
        [Inject]
        public IAppSettings Settings { get; set; }

        public async Task<IHttpActionResult> Get([FromUri]Filter filter)
        {
            //if (id <=0) return Content(HttpStatusCode.BadRequest, new Error { ErrorText = $"empty subscription id." });

            var deliveries = await Repository.ReadAll();

            return Content(HttpStatusCode.OK, deliveries.ToArray());

            return Content(HttpStatusCode.BadRequest, new Error { ErrorText = $"incorrect child resource." });
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
