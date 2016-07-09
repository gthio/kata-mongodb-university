use testdb;

db.restaurants.insert(
   {
      "borough" : "Manhattan",
      "cuisine" : "Italian",
      "name" : "Inserted",
      "restaurant_id" : "41704620"
   }
);

db.restaurants.insert(
   {
     "_id" : "SuppliedID2",
      "borough" : "Manhattan",
      "cuisine" : "Italian",
      "name" : "Inserted 2",
      "restaurant_id" : "41704620"
   }
);

db.restaurants.find({name: "Inserted 2"}).pretty();