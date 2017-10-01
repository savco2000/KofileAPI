using Xunit;
using System.Linq;

namespace KofileAPI.Tests
{
    public class OrderServiceTests
    {        
        private readonly IOrderService _orderService = new OrderService(new FeesAndDistributionService());      

        [Fact(DisplayName = "Correct fees should be applied to each order")]
        public void Correct_fees_should_be_applied_to_each_order()
        {
            const int expectedCount = 2;

            var order = _orderService.GetOrders().FirstOrDefault();

            var sut = _orderService;           

            var result = sut.GetPriceForSingleOrder(order);

            Assert.NotNull(result);
            Assert.Equal(result.OrderItems.Count(), expectedCount);

            Assert.Equal(result.OrderItems[0].Type, "Real Property Recording");
            Assert.Equal(result.OrderItems[1].Type, "Real Property Recording");

            Assert.Equal(result.OrderItems[0].Amount, 28.00m);
            Assert.Equal(result.OrderItems[1].Amount, 26.00m);

            Assert.Equal(result.TotalAmount, 54.00m);
        }

        [Fact(DisplayName = "Correct fund distributions should be applied for each order")]
        public void Correct_fund_distributions_should_be_applied_for_each_order()
        {
            const int expectedCount = 5;

            var order = _orderService.GetOrders().FirstOrDefault();            

            var sut = _orderService;

            var result = sut.GetDistributionsForSingleOrder(order);

            Assert.NotNull(result);
            Assert.Equal(result.DistributionItems.Count(), expectedCount);

            Assert.Equal(result.DistributionItems[0].Name, "Recording Fee");
            Assert.Equal(result.DistributionItems[1].Name, "Records Management and Preservation Fee");
            Assert.Equal(result.DistributionItems[2].Name, "Records Archive Fee");
            Assert.Equal(result.DistributionItems[3].Name, "Courthouse Security");
            Assert.Equal(result.DistributionItems[4].Name, "Other");

            Assert.Equal(result.DistributionItems[0].Amount, 10.00m);
            Assert.Equal(result.DistributionItems[1].Amount, 20.00m);
            Assert.Equal(result.DistributionItems[2].Amount, 20.00m);
            Assert.Equal(result.DistributionItems[3].Amount, 2.00m);
            Assert.Equal(result.DistributionItems[4].Amount, 2.00m);
        }
    }
}
