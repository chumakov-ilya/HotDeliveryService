using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using SQLite;

namespace Bringo.HotDeliveryService.Core
{
    public class Delivery
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DeliveryStatusEnum Status { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }

        public bool IsExpiredByTime(DateTime expirationTime)
        {
            return Status == DeliveryStatusEnum.Available && CreationTime < expirationTime;
        }
    }
}
