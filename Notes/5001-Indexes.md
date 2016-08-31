# Indexes

Index data structures

- BTress (MMAP storage engine)
- B+Trees (Wired tiger storage engine)

Default Index

- Unique index on the _id field by default is created during creation of collection
- This _id index cant be removed

Index types

- Single index
    
    Index on single field of a document

    {score: 1}

- Compound index

    Index on multiple fields of a document

    {userid: 1, score: -1}

- Multikey index

    Index to content stored in an array. If a index field holds on array values, MongoDB create separates index entries for each elemnt of the array.
    MongoDB not allowe more than one array as part of Multikey index.

- Geospatical index

    Type of geospatial index

    - 2d --> uses planar geometry when returned
    - 2dsphere --> uses speherical geometry when returned

- Text index

    Supports searching for string content in a collection.


Index properties


- Unique index

    MongoDB to reject duplicate values for the indexed field

    db.members.createIndex({"user_id": 1, {unique: true}})

- Partial index

    MongoDB to index the documents that meet a specified filter expression

- Sparse index

    - MongoDB to ensure that the index only contain entries for documents that have the indexed fields.
    - Sparse and unique index options can be combined, to reject documents that have duplicate values for a field but ignore documenta that do not have the indexed key

- TTL index
    - MongoDB to use this special index, to automatically remove documents from a collection after certain amount of time.


Index creation

- Syntax

    db.collection.createIndex(keys, options)

    Keys: Field and value pairs. 

    Options:

    - name
    - background
    - unique
    - sparse
    - expireAfterSeconds

Examples
- Ascending index on a single field

    db.collection.createIndex({orderDate: 1});

- Compound index

    db.collection.createIndex({ orderDate: 1, zipcode: -1});


Behaviors

- Multiple createIndex() methods with the same specifications at the same time, only first operation will succeed.

- Non-background operation will block all toher operations on a database

- To add or change index options, must drop index and create another index with the new options. 

- If the same index fields exists, the method call will be ignored.


Others

- db.collection.getIndexes() to view specifications of existing Indexes
- db.collection.dropIndex({student_id: 1}), to drop index on the student_id field
- Use db.collection.createIndex() rather than db.collection.ensureIndex()


    
