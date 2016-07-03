use testdb;

//find all, override cursor config
var result = db.restaurants.find();

//result;

//while(result.hasNext()){
//  printjson(result.next());
//}

//result.forEach(printjson);

//var resultArray = result.toArray();

//var resultArrayFirstItem = result[1];
//printjson(resultArrayFirstItem);

//var myFirstDocument = result.hasNext() ? result.next() : null;
//result.objsLeftInBatch();

result.next();