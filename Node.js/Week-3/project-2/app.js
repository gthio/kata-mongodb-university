var mongoClient = require('mongodb').MongoClient;
var assert = require('assert');

mongoClient.connect('mongodb://localhost:27017/crunchbase', function (err, db) {

    assert.equal(err, null);

    var query = {"category_code": "biotech"};
    var projection = {"name": 1, "category_code": 1, "_id": 0};

    var cursor = db.collection("companies").find(query);
    cursor.project(projection);

    cursor.forEach(
        function (doc){
            console.log(doc);       
        },
        function (err){
            assert.equal(err, null);
            return db.close();     
        }
    );
})