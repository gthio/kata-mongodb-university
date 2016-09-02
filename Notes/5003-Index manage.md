# Manage Indexes

To return a list of indexes on  a collection

> db.collection.getIndexes()

To remove an index

> db.collection.dropIndex()

> db.collection.dropIndex({ "tax-id": 1})

To remove all indexes (except _id index)

> db.collection.dropIndexes()

To rebuild indexes for a collection. 
This operation drops all indexes, including the _id index, and then rebuilds all indexes. 
_id index rebuild is in the foreground. Other indexes will be rebuild in the background if originally specified with the background option.

> db.collection.reIndex()





