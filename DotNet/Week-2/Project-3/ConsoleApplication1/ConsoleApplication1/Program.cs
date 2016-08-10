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
            var connectionString = "mongodb://localhost:27017";

            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");

            if (false)
            {
                var col = db.GetCollection<BsonDocument>("widgets");
                await db.DropCollectionAsync("widgets");
                var docs = Enumerable.Range(0, 10)
                    .Select(x => new BsonDocument("_id", x).Add("x", x));
                await col.InsertManyAsync(docs);

                var result = await col.ReplaceOneAsync(new BsonDocument("_id", 5),
                    new BsonDocument("_id", 5).Add("x", 30));

                var result2 = await col.UpdateOneAsync(
                    Builders<BsonDocument>.Filter.Eq("_id", 6),
                    new BsonDocument("$inc", new BsonDocument("x", 100)));

                var result3 = await col.UpdateOneAsync(
                    Builders<BsonDocument>.Filter.Eq("_id", 7),
                    Builders<BsonDocument>.Update.Inc("x", 1000));

                await col.Find(new BsonDocument())
                    .ForEachAsync(x => Console.WriteLine(x));
            }

            if (false)
            {
                var col = db.GetCollection<Widget>("widgets");
                await db.DropCollectionAsync("widgets");
                var docs = Enumerable.Range(0, 10)
                    .Select(x => new Widget { Id = x, X = x });
                await col.InsertManyAsync(docs);

                var result = await col.UpdateManyAsync(
                    Builders<Widget>.Filter.Gt(x => x.X, 5),
                    Builders<Widget>.Update.Inc("x", 10));

                var result2 = await col.UpdateManyAsync(
                    x => x.X == 4,
                    Builders<Widget>.Update.Inc("x", 10));

                await col.Find(new BsonDocument())
                    .ForEachAsync(x => Console.WriteLine(x));
            }

            if (false)
            {
                var col = db.GetCollection<Widget>("widgets");
                await db.DropCollectionAsync("widgets");
                var docs = Enumerable.Range(0, 10)
                    .Select(x => new Widget { Id = x, X = x });
                await col.InsertManyAsync(docs);

                var result1 = await col.DeleteOneAsync(x => x.X < 5);
                var result2 = await col.DeleteManyAsync(x => x.X > 5);

                await col.Find(new BsonDocument())
                    .ForEachAsync(x => Console.WriteLine(x));
            }

            if (false)
            {
                var col = db.GetCollection<Widget>("widgets");
                await db.DropCollectionAsync("widgets");
                var docs = Enumerable.Range(0, 10)
                    .Select(x => new Widget { Id = x, X = x });
                await col.InsertManyAsync(docs);

                var result = await col.FindOneAndUpdateAsync<Widget>(
                    x => x.X > 5,
                    Builders<Widget>.Update.Inc(x => x.X, 1),
                    new FindOneAndUpdateOptions<Widget, Widget> 
                    {
                        ReturnDocument = ReturnDocument.After,
                        Sort = Builders<Widget>.Sort.Descending(x => x.X)
                    });

                Console.WriteLine(result);
                Console.WriteLine();

                await col.Find(new BsonDocument())
                    .ForEachAsync(x => Console.WriteLine(x));
            }

            if (false)
            {
                var col = db.GetCollection<Widget>("widgets");
                await db.DropCollectionAsync("widgets");
                var docs = Enumerable.Range(0, 10)
                    .Select(x => new Widget { Id = x, X = x });
                await col.InsertManyAsync(docs);

                var result = await col.FindOneAndDeleteAsync<Widget>(
                    x => x.X > 5,
                    new FindOneAndDeleteOptions<Widget, Widget>
                    {
                        Sort = Builders<Widget>.Sort.Descending(x => x.X)
                    });

                await col.Find(new BsonDocument())
                    .ForEachAsync(x => Console.WriteLine(x));
            }

            if (true)
            {
                var col = db.GetCollection<BsonDocument>("widgets");
                await db.DropCollectionAsync("widgets");
                var docs = Enumerable.Range(0, 10)
                    .Select(x => new BsonDocument("_id", x).Add("x", x));
                await col.InsertManyAsync(docs);

                var result = col.BulkWriteAsync(new WriteModel<BsonDocument>[]
                    {
                        new DeleteOneModel<BsonDocument>("{x: 5}"),
                        new DeleteOneModel<BsonDocument>("{x: 7}"),
                        new UpdateManyModel<BsonDocument>("{x: {$lt: 7}}", "{$inc: {x: 1}}")
                    });

                await col.Find(new BsonDocument())
                    .ForEachAsync(x => Console.WriteLine(x));
            }
        }
    }

    [BsonIgnoreExtraElements]
    class Widget
    {
        public int Id { get; set; }

        [BsonElement("x")]
        public int X { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, X: {1}", Id, X);
        }
    }
}
