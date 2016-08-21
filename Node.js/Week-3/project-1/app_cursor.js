var mongoClient = require('mongodb').MongoClient;
var assert = require('assert');

mongoClient.connect('mongodb://localhost:27017/crunchbase', function (err, db) {

    assert.equal(err, null);

    var query = {"category_code": "biotech"};

    var cursor = db.collection("companies").find(query);

    cursor.forEach(
        function (doc){
            console.log(doc.name);       
        },
        function (err){
            assert.equal(err, null);
            return db.close();     
        }
    );
})