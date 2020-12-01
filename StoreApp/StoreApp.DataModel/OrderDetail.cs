using System;
using System.Collections.Generic;

#nullable disable

namespace StoreApp.DataModel
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public virtual CustomerTable Customer { get; set; }
        public virtual StoreTable Location { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
