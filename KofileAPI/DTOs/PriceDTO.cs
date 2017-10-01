using System.Collections.Generic;

namespace KofileAPI.DTOs
{
    public class PriceDTO
    {        
        public string OrderNumber { get; set; }
        public List<PriceItemDTO> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class PriceItemDTO
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
