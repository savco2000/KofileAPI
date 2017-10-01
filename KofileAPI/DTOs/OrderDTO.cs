using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KofileAPI.DTOs
{
    public class OrderDTO
    {
        [JsonProperty(PropertyName = "order_date")]
        public DateTime OrderDate { get; set; }

        [JsonProperty(PropertyName = "order_number")]
        public string OrderNumber { get; set; }

        [JsonProperty(PropertyName = "order_items")]
        public List<OrderItemDTO> OrderItemDTOs { get; set; }
    }

    public class OrderItemDTO
    {
        [JsonProperty(PropertyName = "order_item_id")]
        public int OrderItemId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "pages")]
        public int Pages { get; set; }
    }
}
