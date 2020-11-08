using TeaDB.Entities;
using TeaDB.Models;
using System.Collections.Generic;
namespace TeaDB.IMappers
{
    /// <summary>
    /// Mapping between Product Model and Entities
    /// </summary>
    public interface IProductMapper
    {
        Products ParseProduct(ProductModel product);
        ICollection<Products> ParseProduct(List<ProductModel> product);
        ProductModel ParseProduct(Products products);
        List<ProductModel> ParseProduct(ICollection<Products> products);
    }
}