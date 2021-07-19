using System;
using System.Collections.Generic;
using System.Linq;
using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order:Entity,IAggregateRoot
    {
        //EF core features
        //--Owned Types
        //--Shadow Property
        //--Backing Field

        public DateTime CreatedDate { get; set; }

        public Address Address { get; set; }
        public string BuyerId { get; set; }
        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {

        }
        public Order (string buyerId,Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }
        public void AddOrderItem(string productId,string productName,decimal price,string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if(!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }
        public decimal GetTotalPrice => _orderItems.Sum(x=>x.Price);

    }
}
