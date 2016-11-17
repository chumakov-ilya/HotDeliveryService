using Bringo.HotDeliveryService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Ninject;

namespace Bringo.HotDeliveryService.Web.Controllers
{
    public class DeliveriesController : ApiController
    {
        [Inject]
        public DeliveryService Service { get; set; }

        /// <summary>
        /// Get all deliveries by status (Available, Expired, Taken).
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get([FromUri]Filter filter)
        {
            //if (id <=0) return Content(HttpStatusCode.BadRequest, new Error { ErrorText = $"empty subscription id." });

            var deliveries = await Service.Get(filter);

            return Content(HttpStatusCode.OK, deliveries.ToArray());
        }

        /// <summary>
        /// Take a delivery by ID.
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        [Route("~/api/deliveries/{deliveryId}/actions/take")]
        public async Task<IHttpActionResult> Post([FromUri]int deliveryId, [FromBody]TakeRequestBody body)
        {
            Delivery delivery = await Service.Take(deliveryId);

            if (delivery == null) return NotFound(deliveryId);

            if (delivery.IsExpired()) return IsExpired(deliveryId);

            return Content(HttpStatusCode.OK, delivery);
        }

        private NegotiatedContentResult<Error> NotFound(int deliveryId)
        {
            return Content(HttpStatusCode.NotFound, new Error { ErrorText = $"Delivery #{deliveryId} is not found." });
        }

        private NegotiatedContentResult<Error> IsExpired(int deliveryId)
        {
            return Content((HttpStatusCode)422, new Error { ErrorText = $"Delivery #{deliveryId} is expired." });
        }
    }
}
