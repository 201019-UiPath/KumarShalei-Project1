using TeaDB.Entities;
using TeaDB.Models;
using System.Collections.Generic;

namespace TeaDB.IMappers
{
    /// <summary>
    /// Mapping between Customer Model and Entities
    /// </summary>
    public interface ICustomerMapper
    {
        Customers ParseCustomer(CustomerModel customer);
        ICollection<Customers> ParseCustomer(List<CustomerModel> customer);
        CustomerModel ParseCustomer(Customers customers);
        List<CustomerModel> ParseCustomer(ICollection<Customers> customers);
    }
}
