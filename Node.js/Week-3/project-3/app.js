var mongoClient = require('mongodb').MongoClient;
var assert = require('assert');
var commandLineArgs = require('command-line-args');

var options = commandLineOptions();

mongoClient.connect('mongodb://localhost:27017/crunchbase', function (err, db) {

    assert.equal(err, null);

    var query = queryDocument(options);
    var projection = {"name": 1, "category_code": 1, "_id": 1,
        "number_of_employees": 1, "crunchbase_url": 1};

    var cursor = db.collection("companies").find(query, projection);
    var numMatches = 0;

    cursor.forEach(
        function (doc){
            numMatches = numMatches + 1;
            console.log(doc);       
        },
        function (err){
            assert.equal(err, null);
            console.log("Query: " + JSON.stringify(query));
            console.log("Matching documents: " + numMatches);
            return db.close();     
        }
    );
});

function commandLineOptions(){

    var cli = commandLineArgs([
        {name: "firstYear", alias: "f", type: Number},
        {name: "lastYear", alias: "l", type: Number},
        {name: "employees", alias: "e", type: Number}
    ]);

    var options = cli.parse()
    if (!(("firstYear" in options) && ("lastYear" in options))) {
        console.log(cli.getUsage({
            title: "Usage",
            description: "The first two options below are required. The rest are optional."
        }));

        process.exit();
    }    

    return options;
}

function queryDocument(options){

    console.log(options);    

    var query = {
        "founded_year": {
            "$gte": options.firstYear,
            "$lte": options.lastYear
        }
    };

    if ("employees" in options){
        query.number_of_employees = {
            "$gte": options.employees
        }
    }

    return query;
}