use testdb;

//count
db.restaurants.count();

//distinct
db.restaurants.distinct("borough");

//group count
db.restaurants.aggregate([{$match: {borough: "Manhattan"}}, {$group: {_id: "$cuisine", sum: {"$sum": 1}}}]);