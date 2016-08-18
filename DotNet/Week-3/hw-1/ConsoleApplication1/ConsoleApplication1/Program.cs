using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.IO;

using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

            Console.WriteLine();
            Console.WriteLine("Press enter");
            Console.ReadLine();
        }

        static async Task MainAsync(string[] args)
        {
            var settings = new MongoClientSettings
            {
            };

            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);

            var db = client.GetDatabase("school");        
            var col = db.GetCollection<BsonDocument>("students");

            await col.Find(new BsonDocument())
                .ForEachAsync(x =>
                {
                    var name = x["name"].ToString();
                    var scores = x["scores"].AsBsonArray;

                    foreach (BsonDocument score in scores)
                    {
                        string s = score["type"].ToString();
                    }

                    Console.WriteLine(name);
                });
        }
    }
}
