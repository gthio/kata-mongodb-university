use test;

//find all
db.restaurants.find().pretty();

//find, top level field
db.restaurants.find({"borough": "Manhattan"}).pretty();

//find, field in embedded document
db.restaurants.find({"address.zipcode": "10075"}).pretty();

//find, field in an array
db.restaurants.find({"grades.grade": "B"}).pretty();

//find, field in 
db.restaurants.find({"borough": { $in: ["Brooklyn", "Queens"] }}).pretty();

//find, greater than operator
db.restaurants.find({"grades.score": {$gt: 30}}).pretty();

//find, less than operator
db.restaurants.find({"grades.score": {$lt: 10}}).pretty();

//find, logical AND
db.restaurants.find({"borough": "Manhattan", "address.zipcode": "10075"}).pretty();

//find, logical OR
db.restaurants.find({$or: [{"borough": "Manhattan"}, {"address.zipcode": "10075"}]}).pretty();

//find, field AND and OR 
db.restaurants.find({"borough": "Queens", $or: [{ "grades.grade" : "P" }, {"grades.score": {$lt: 2}}]}).pretty();

//find, sort --> asc: 1, desc: -1
db.restaurants.find().sort({"name": -1, "address.grade": 1}).pretty();

//find, sort --> asc: 1, desc: -1
db.restaurants.find().sort({"name": -1, "address.grade": 1}).pretty();

//find, return specified fields only, _id by default is returned
db.restaurants.find({}, {name: 1}).pretty();

//find, return specified fields only
db.restaurants.find({}, {_id: 0, name: 1, "address.street": 1}).pretty();

//find, return specified fields only in embedded document
db.restaurants.find({}, {_id: 0, "address.coord": 1 }).pretty();

//find, suprress specified fields in embeded documents
db.restaurants.find({}, {_id: 0, "address.coord": 0, grades:0}).pretty();

//find, return last element in grades array
db.restaurants.find({}, {grades: {$slice: -1}}).pretty();

//find, return first element in grades array
db.restaurants.find({}, {grades: {$elemMatch: {}}}).pretty();

//find, return first element in grades array, where grade is B
db.restaurants.find({}, {name: 1, grades: {$elemMatch: { grade: 'B'}}}).pretty();

//find, null or missing fields or do not containe the name field
db.restaurants.find({name: null}).pretty();

//find, existance check
db.restaurants.find({"address.coord": {$exists: true}}).pretty();

