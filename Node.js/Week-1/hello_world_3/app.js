var mongoClient = require('mongodb').MongoClient,
	assert = require('assert');

mongoClient.connect('mongodb://localhost:27017/movies', function(err, db){

	assert.equal(null, err);
	console.log("Successfully connected to server");

	db.collection('movies').find({}).toArray(function(err, docs){

		docs.forEach(function(doc){
			console.log(doc.title);
		});

		db.close();

	});

	console.log("Called find()");
});
