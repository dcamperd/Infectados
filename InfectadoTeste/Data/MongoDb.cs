using System;
using InfectadoTeste.Data.Collections;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace InfectadoTeste.Data {
    public class MongoDb {
        public IMongoDatabase Db { get; }

        public MongoDb(IConfiguration configuration) {
            try {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                Db = client.GetDatabase(configuration["NomeBanco"]);
                MapClasses();
            }
            catch (Exception ex) {
                throw new MongoException("Nao foi possivel conectar ao MongoDb", ex);
            }
        }
        
        private void MapClasses() {
            var convetionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", convetionPack, t => true);

            if(!BsonClassMap.IsClassMapRegistered(typeof(Infectado))) {
                BsonClassMap.RegisterClassMap<Infectado>(i => {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}