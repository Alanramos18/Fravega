using Data.Entity;
using MongoDB.Driver;

namespace Fravega.Data
{
    public interface IFravegaContext
    {
        IMongoCollection<Promocion> DbSet { get; }
    }
}
