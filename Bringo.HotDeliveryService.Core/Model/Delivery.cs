using System;
using System.Collections.Generic;
using SQLite;

namespace Bringo.HotDeliveryService.Core
{
    public class Delivery
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DeliveryStatusEnum Status { get; set; }
        public string Title { get; set; }
        public int? UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }

        public bool ShouldBeExpired(DateTime threshold)
        {
            if (UserId != null && Status != DeliveryStatusEnum.Taken)
                throw new NotSupportedException($"Delivery #{Id} is broken.");

            return Status == DeliveryStatusEnum.Available && CreationTime < threshold;
        }

        /// <summary>
        /// I prefer 'timestamp' column on database side. It is ridiculous to update time manually.
        /// </summary>
        public void MarkAsModified()
        {
            ModificationTime = DateTime.Now;
        }     
    }
}
