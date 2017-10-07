using KofileAPI.DomainModels;
using KofileAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KofileAPI
{
    public interface IOrderService
    {        
        PriceDTO GetPriceForSingleOrder(OrderDTO orderDTO);
        IEnumerable<PriceDTO> GetPricesForOrders(IEnumerable<OrderDTO> orderDTOs);
        DistributionDTO GetDistributionsForSingleOrder(OrderDTO orderDTO);
        IEnumerable<DistributionDTO> GetDistributionsForOrders(IEnumerable<OrderDTO> orderDTOs);
    }

    public class OrderService : IOrderService
    {
        private readonly IEnumerable<FeesAndDistributions> _feesAndDistributions;       

        public OrderService(IFeesAndDistributionsService feesAndDistributionService)
            => _feesAndDistributions = feesAndDistributionService.FeesAndDistributions;

        #region Orders

        
        public PriceDTO GetPriceForSingleOrder(OrderDTO orderDTO)
        {
            var order = new Order
            {
                OrderDate = orderDTO.OrderDate,
                OrderNumber = orderDTO.OrderNumber,
                OrderItems = orderDTO.OrderItemDTOs.Select(x => new OrderItem
                {
                    OrderItemId = x.OrderItemId,
                    Type = x.Type,
                    Pages = x.Pages
                }).ToList()
            };

            var orderItemPrices = order.OrderItems.Select(orderItem =>
            {
                var fees = _feesAndDistributions.SingleOrDefault(x => x.OrderItemType == orderItem.Type)?.Fees;

                if (fees == null) return new PriceItemDTO();

                var priceOfOrderItem = fees.Sum(fee => CalculateOrderItemPrice(fee, orderItem.Pages));

                return new PriceItemDTO { Type = orderItem.Type, Amount = priceOfOrderItem };
            }).ToList();

            var totalAmount = orderItemPrices
                .Aggregate((total, next) => new PriceItemDTO { Amount = total.Amount + next.Amount })
                .Amount;

            return new PriceDTO { OrderNumber = order.OrderNumber, OrderItems = orderItemPrices, TotalAmount = totalAmount };
        }

        public IEnumerable<PriceDTO> GetPricesForOrders(IEnumerable<OrderDTO> orderDTOs) 
            => orderDTOs.Select(orderDTO => GetPriceForSingleOrder(orderDTO));

        #endregion

        #region Order distributions

        public DistributionDTO GetDistributionsForSingleOrder(OrderDTO orderDTO)
        {
            var order = new Order
            {
                OrderDate = orderDTO.OrderDate,
                OrderNumber = orderDTO.OrderNumber,
                OrderItems = orderDTO.OrderItemDTOs.Select(x => new OrderItem
                {
                    OrderItemId = x.OrderItemId,
                    Type = x.Type,
                    Pages = x.Pages
                }).ToList()
            };

            var unorderedDistributions = order.OrderItems.SelectMany(orderItem =>
            {
                var fees = _feesAndDistributions.SingleOrDefault(x => x.OrderItemType == orderItem.Type)?.Fees;

                var distributions = _feesAndDistributions.SingleOrDefault(x => x.OrderItemType == orderItem.Type)?.Distributions;

                if (fees == null || distributions == null) return new List<Distribution>();

                var priceOfOrderItem = fees.Sum(fee => CalculateOrderItemPrice(fee, orderItem.Pages));

                var difference = priceOfOrderItem - distributions.Sum(x => x.Amount);

                if (difference > 0)
                {
                    var tempDistributions = new List<Distribution>();
                    tempDistributions.AddRange(distributions);
                    tempDistributions.Add(new Distribution { Name = "Other", Amount = difference });
                    return tempDistributions;
                }

                return distributions;
            });

            var groupedAndSummedDistributions = unorderedDistributions.GroupBy(x => x.Name)
                .Select(y => new Distribution
                {
                    Name = y.Key,
                    Amount = y.Sum(z => z.Amount)
                });

            return new DistributionDTO
            {
                OrderNumber = order.OrderNumber,
                DistributionItems = groupedAndSummedDistributions
                    .Select(distribution => new DistributionItemDTO
                    {
                        Name = distribution.Name,
                        Amount = distribution.Amount
                    })
                    .ToList()
            };
        }

        public IEnumerable<DistributionDTO> GetDistributionsForOrders(IEnumerable<OrderDTO> orderDTOs) 
            => orderDTOs.Select(orderDTO => GetDistributionsForSingleOrder(orderDTO));

        #endregion

        #region Helper functions        

        private decimal CalculateOrderItemPrice(Fee fee, int numberOfPages)
            => (fee.Type == "per-page") ? fee.Amount * (numberOfPages - 1) : fee.Amount;        

        #endregion
    }
}
