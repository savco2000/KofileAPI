using System;
using System.Collections.Generic;

namespace KofileAPI.DomainModels
{
    public class Order
    {
        public DateTime OrderDate { get; set; }        
        public string OrderNumber { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string Type { get; set; }
        public int Pages { get; set; }
    }
}
