using System.Collections.Generic;

namespace KofileAPI.DTOs
{
    public class DistributionDTO
    {
        public string OrderNumber { get; set; }
        public IList<DistributionItemDTO> DistributionItems { get; set; }
    }

    public class DistributionItemDTO
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
