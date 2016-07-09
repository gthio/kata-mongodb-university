use testdb;

db.restaurants.find({name: "Laquana King"}).pretty();

//Remove all documents
//db.restaurants.remove({name: "Morris Park Bake Shop"});

//Remove just one document
db.restaurants.remove({name: "Laquana King"}, {justOne: true});

