use testdb;

db.restaurants.find({name: "Morris Park Bake Shop"}).pretty();

db.restaurants.update({name: "Morris Park Bake Shop"}, {$set: {test: "Hi there"}, $currentDate: {lastModified: true}});
