﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.Shop.Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus Status { get; set; }
        public IEnumerable<OrderDetail> Details { get; set; }

        public Order()
        {
            Details = new List<OrderDetail>();
            Status = OrderStatus.Ordered;
        }

    }
}
