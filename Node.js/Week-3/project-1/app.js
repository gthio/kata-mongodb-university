var mongoClient = require('mongodb').MongoClient;
var assert = require('assert');

mongoClient.connect('mongodb://localhost:27017/crunchbase', function(err, db) {

    assert.equal(err, null);

    var query = {"category_code": "biotech"};

    db.collection('companies').find(query).toArray(function(err, docs){

        assert.equal(err, null);

        docs.forEach(function(doc){
            console.log(doc.name);
        });
    })
})