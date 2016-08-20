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

            var list = new Dictionary<int, Tuple<string, double>>();

            await col.Find(new BsonDocument())
                .ForEachAsync(x =>
                {
                    var id = x["_id"].ToInt32();
                    var scores = x["scores"].AsBsonArray;

                    foreach (BsonDocument score in scores)
                    {
                        string s = score["type"].ToString();
                        var test = score["score"].ToDouble();

                        if (s != "homework")
                        {
                            continue;
                        }

                        if (!list.ContainsKey(id))
                        {
                            list.Add(id, new Tuple<string, double>(s, test));
                        }
                        else
                        {
                            if (list[id].Item2 > test)
                            {
                                list[id] = new Tuple<string, double>(s, test);
                            }
                        }
                    }
                });

            foreach (var item in list)
            {
                var z = Builders<BsonDocument>.Filter.Eq("_id", item.Key);
                var zz = Builders<BsonDocument>.Update.PullFilter("scores",
                            Builders<BsonDocument>.Filter.Eq("type", item.Value.Item1) &
                            Builders<BsonDocument>.Filter.Eq("score", item.Value.Item2));

                var result = col.FindOneAndUpdateAsync(z, zz).Result;
            }
        }
    }
}
