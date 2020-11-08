using TeaDB.Models;
using TeaDB.Entities;
using System.Collections.Generic;

namespace TeaDB.IMappers
{
    /// <summary>
    /// Mapping between Order Items Model and Entities
    /// </summary>
    public interface IOrderItemMapper
    {
        Orderitems ParseOrderItem(OrderItemModel orderItem);
        ICollection<Orderitems> ParseOrderItem(List<OrderItemModel> orderItem);
        OrderItemModel ParseOrderItem(Orderitems orderitems);
        List<OrderItemModel> ParseOrderItem(ICollection<Orderitems> orderitems);
    }
}
