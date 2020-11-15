using TeaDB.Entities;
using TeaDB.Models;
using System.Collections.Generic;

namespace TeaDB.IMappers
{

    /// <summary>
    /// Mapping between Location Model and Entities
    /// </summary>

    public interface ILocationMapper
    {
        Locations ParseLocation(LocationModel location);
        ICollection<Locations> ParseLocation(List<LocationModel> location);
        LocationModel ParseLocation(Locations locations);
        List<LocationModel> ParseLocation(ICollection<Locations> locations);
    }
}