using System.Collections.Generic;

namespace KofileAPI.DomainModels
{
    public class FeesAndDistributions
    {
        public string OrderItemType { get; set; }
        public IList<Fee> Fees { get; set; }
        public IList<Distribution> Distributions { get; set; }
    }

    public class Fee
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }

    public class Distribution
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
