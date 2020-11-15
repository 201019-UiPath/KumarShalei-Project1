using TeaDB.Entities;
using TeaDB.Models;
using System.Collections.Generic;

namespace TeaDB.IMappers
{
    /// <summary>
    /// Mapping between Order Model and Entities
    /// </summary>

    public interface IOrderMapper
    {
        Orders ParseOrder(OrderModel order);
        ICollection<Orders> ParseOrder(List<OrderModel> order);
        OrderModel ParseOrder(Orders orders);
        List<OrderModel> ParseOrder(ICollection<Orders> orders);
    }
}