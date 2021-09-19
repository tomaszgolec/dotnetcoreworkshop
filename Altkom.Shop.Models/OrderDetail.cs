﻿namespace Altkom.Shop.Models
{
    public class OrderDetail : BaseEntity
    {
        public virtual Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineAmount => Quantity * UnitPrice;
        public int? ItemId { get; set; }
    }
}
