using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using FluentAssertions;

using MongoDB.Bson;
using MongoDB.Driver;

namespace Course
{
    class Program
    {
        static void Main(string[] args)
        {
            SetConnectionAsync().Wait();
            //InsertOneAsync().Wait();
            //FindAsync().Wait();
        }

        public static async Task<int> SetConnectionAsync()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);

            var connectionSettings = new MongoClientSettings()
            {
                Server = new MongoServerAddress("localhost", 27017),
                MaxConnectionPoolSize = 1000,
                MinConnectionPoolSize = 300
            };
            var client2 = new MongoClient(connectionSettings);

            return 1;
        }

        public static async Task<int> InsertOneAsync()
        {
            var _client = new MongoClient();
            var _database = _client.GetDatabase("test");

            var document = new BsonDocument
            {
                { "id", "41704620" },
                { "name", "Vella" },
                { "borough", "Manhattan" },
                { "cuisine", "Italian" },
                { "address" , new BsonDocument
                    {
                        { "street", "2 Avenue" },
                        { "coord", new BsonArray { 73.9557413, 40.7720266 } }
                    }
                },
            };

            var collection = _database.GetCollection<BsonDocument>("my_restaurants");
            await collection.InsertOneAsync(document);

            return 1;
        }

        public static async Task<int> FindAsync()
        {
            var _client = new MongoClient();
            var _database = _client.GetDatabase("test");

            var collection = _database.GetCollection<BsonDocument>("restaurants");

            var filter = new BsonDocument();
            var count = collection.Find(filter).Count();

            var filter2 = Builders<BsonDocument>.Filter.Eq("borough", "Manhattan");
            var count2 = collection.Find(filter2).Count();

            var filter3 = Builders<BsonDocument>.Filter.Eq("address.street", "2 Avenue");
            var count3 = collection.Find(filter3).Count();

            var filter4 = Builders<BsonDocument>.Filter.Eq("grades.grade", "B");
            var count4 = collection.Find(filter4).Count();

            var filter5 = Builders<BsonDocument>.Filter.Gt("grades.score", 30);
            var count5 = collection.Find(filter5).Count();

            var builder6 = Builders<BsonDocument>.Filter;
            var filter6 = builder6.Eq("borough", "Manhattan") &
                builder6.Gt("grades.score", 30);
            var count6 = collection.Find(filter6).Count();

            var builder7 = Builders<BsonDocument>.Filter;
            var filter7 = builder6.Eq("borough", "Manhattan") |
                builder7.Gt("grades.score", 30);
            var count7 = collection.Find(filter7).Count();

            var filter8 = new BsonDocument();
            var sort8 = Builders<BsonDocument>.Sort.Ascending("name");
            var result = collection.Find(filter8).Sort(sort8).ToList();

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            return 1;
        }
    }
}
