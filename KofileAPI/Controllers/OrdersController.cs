using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KofileAPI.DTOs;

namespace KofileAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;        

        public OrdersController(IOrderService orderService) 
            => _orderService = orderService;

        [HttpPost("prices")]
        public IEnumerable<PriceDTO> Prices([FromBody]List<OrderDTO> orderDTOs)
        {
            var result = _orderService.GetPricesForOrders(orderDTOs);

            return result;
        }

        [HttpPost("distributions")]
        public IEnumerable<DistributionDTO> Distributions([FromBody]List<OrderDTO> orderDTOs)
        {
            var result = _orderService.GetDistributionsForOrders(orderDTOs);

            return result;            
        }        
    }
}
