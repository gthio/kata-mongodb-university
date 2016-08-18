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

            var db = client.GetDatabase("students");        
            var col = db.GetCollection<BsonDocument>("grades");
            var cnt = col.Count(new BsonDocument());

            var previous = "";
            var deletionList = new List<ObjectId>();
            
            await col.Find(new BsonDocument("type", "homework")).Sort("{student_id: 1, score:1 }")
                .ForEachAsync(x => 
                {
                    var id = x["_id"].AsObjectId;
                    var studentID = x["student_id"].ToString();

                    if (previous != studentID)
                    {
                        deletionList.Add(id);
                        previous = studentID;
                    }
                });

            foreach (var theid in deletionList)
            {
                col.DeleteOne(new BsonDocument("_id", theid));
            }
        }
    }
}
