use testdb;

//find all
db.restaurants.find().pretty();

//find, top level field
db.restaurants.find({"borough": "Manhattan"}).pretty();

//find, field in embedded document
db.restaurants.find({"address.zipcode": "10075"}).pretty();

//find, field in an array
db.restaurants.find({"grades.grade": "B"}).pretty();

//find, greater than operator
db.restaurants.find({"grades.score": {$gt: 30}}).pretty();

//find, less than operator
db.restaurants.find({"grades.score": {$lt: 10}}).pretty();

//find, logical AND
db.restaurants.find({"borough": "Manhattan", "address.zipcode": "10075"}).pretty();

//find, logical OR
db.restaurants.find({$or: [{"borough": "Manhattan"}, {"address.zipcode": "10075"}]}).pretty();

//find, sort --> asc: 1, desc: -1
db.restaurants.find().sort({"name": -1, "address.grade": 1}).pretty();
