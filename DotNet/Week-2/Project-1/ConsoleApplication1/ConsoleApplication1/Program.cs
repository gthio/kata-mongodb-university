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

            //database and collection are thread-safe
            var db = client.GetDatabase("test");        
            var col = db.GetCollection<BsonDocument>("people");

            var doc = new BsonDocument
            {
                {"name", "jones"}
            };

            doc.Add("age", 30);
            doc["profession"] = "hacker";

            var array = new BsonArray();
            array.Add(new BsonDocument("color", "red"));
            array.Add(new BsonDocument("shape", "circle"));

            doc.Add("array", array);

            Console.WriteLine("name: " + doc["name"]);
            Console.WriteLine("color: " + doc["array"][0]["color"]);

            Console.WriteLine(doc);

            var conventionPack = new ConventionPack();
            conventionPack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            //BsonClassMap.RegisterClassMap<Person>(x =>
            //{
            //    x.AutoMap();
            //    x.MapMember(y => y.Name).SetElementName("name");
            //});

            var person = new Person
            {
                Name = "jones",
                Age = 30,
                Colors = new List<string>() { "red", "blue" },
                Pets = new List<Pet>() { new Pet { Name = "Fluffy", Type = "Pig" } },
                ExtraElements = new BsonDocument("another name", "another value")
            };

            using (var writer = new JsonWriter(Console.Out))
            {
                BsonSerializer.Serialize(writer, person);
            }
        }
    }

    class Person
    {
        public ObjectId Id { get; set; }

        //[BsonElement("name")]
        public string Name { get; set; }

        //[BsonRepresentation(BsonType.String)]
        public int Age { get; set; }
        public List<string> Colors { get; set; }
        public List<Pet> Pets { get; set; }
        public BsonDocument ExtraElements { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
