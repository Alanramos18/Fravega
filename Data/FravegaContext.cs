using Data.Entity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fravega.Data
{
    public class FravegaContext : IFravegaContext
    {
        public IMongoCollection<Promocion> DbSet { get; }

        public FravegaContext(IOptions<DbConfig> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            DbSet = database.GetCollection<Promocion>(config.Value.PromocionCollections);
        }
    }
}
