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
    public class DeliveriesController : ApiController
    {
        [Inject]
        public IRepository Repository { get; set; }

        public async Task<IHttpActionResult> Get([FromUri]Filter filter)
        {
            //if (id <=0) return Content(HttpStatusCode.BadRequest, new Error { ErrorText = $"empty subscription id." });

            var deliveries = await Repository.GetAll();

            return Content(HttpStatusCode.OK, deliveries.ToArray());

            return Content(HttpStatusCode.BadRequest, new Error { ErrorText = $"incorrect child resource." });
        }

        [Route("~/api/deliveries/{deliveryId}/actions/take")]
        public async Task<IHttpActionResult> Post([FromUri]int deliveryId, [FromBody]TakeRequestBody body)
        {
            Delivery delivery = await Repository.GetById(deliveryId);

            if (delivery == null)
                return Content(HttpStatusCode.NotFound, new Error { ErrorText = $"Delivery #{deliveryId} not found." });

            if (delivery.Status == DeliveryStatusEnum.Expired || delivery.IsExpiredByTime(DateTime.Now))
                return Content((HttpStatusCode)422, new Error { ErrorText = $"Delivery #{deliveryId} is expired." });

            delivery.Status = DeliveryStatusEnum.Taken;


            return Content(HttpStatusCode.OK, new Error { ErrorText = $"OK" });
            //Repository.Update(delivery);
        }
    }
}
