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

            //database and collection are thread-safe
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("people");

            var doc = new BsonDocument
            {
                {"Name", "Smith"},
                {"Age", 30},
                {"Profession", "hacker"}
            };

            var doc2 = new BsonDocument
            {
                {"Something else", true}
            };

            var doc3 = new Person
            {
                Name = "James",
                Age = 24,
                Profession = "Hacker"
            };

            await col.InsertOneAsync(doc);
            await col.InsertManyAsync(new BsonDocument[] { doc2 });

            var col2 = db.GetCollection<Person>("People");
            await col2.InsertOneAsync(doc3);

            //with cursor
            using (var cursor = await col.Find(new BsonDocument()).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var temp in cursor.Current)
                    {
                        Console.WriteLine(temp);
                    }
                }
            }

            //dump to list
            var list = await col.Find(new BsonDocument()).ToListAsync();
            foreach (var temp in list)
            {
                Console.WriteLine(temp);
            }

            //foreach
            await col.Find(new BsonDocument())
                .ForEachAsync(x => Console.WriteLine(x));


            var filter = new BsonDocument("Name", "Smith");
            var list2 = await col.Find(filter).ToListAsync();
            var list3 = await col.Find("{Name : 'Smith'}").ToListAsync();

            foreach (var temp in list2)
            {
                Console.WriteLine(temp);
            }


            var builder = Builders<BsonDocument>.Filter;
            var filterb = builder.Lt("Age", 30);
            var filterb3 = builder.And(builder.Lt("Age", 30), builder.Eq("Name", "Jones"));
            var list4 = await col.Find(filter).ToListAsync();

            foreach (var temp in list4)
            {
                Console.WriteLine(temp);
            }

            var builder2 = Builders<Person>.Filter;
            var filterb2 = builder2.Lt(x => x.Age, 30);
            var filterb32 = builder2.And(builder2.Lt(x => x.Age, 30), builder2.Eq(x => x.Name, "Jones"));
            var list42 = await col2.Find(filterb2).ToListAsync();

            foreach (var temp in list42)
            {
                Console.WriteLine(temp);
            }




            var list4a = await col.Find(new BsonDocument())
                .Project("{Name: 1, _id: 0}")
                .Project(new BsonDocument("Name", 1).Add("_id", 0))
                .Project(Builders<BsonDocument>.Projection.Include("Name").Exclude("_id"))
                .Limit(1)
                .Skip(5)
                .Sort("{Age: 1}")
                .Sort(new BsonDocument("Age", 1))
                .Sort(Builders<BsonDocument>.Sort.Ascending("Age").Descending("Name"))
                .ToListAsync();

            var list4b = await col2.Find<Person>(new BsonDocument())
                .Project(x => new {x.Name, CalcAge = x.Age + 20 })
                .SortBy(x => x.Age)
                .ThenBy(x => x.Name)
                .ToListAsync();
               
        }

    }

    class Person
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Profession { get; set; }
    }
}
