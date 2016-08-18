mongoimport --drop -d school -c students students.json

after changes
- db.students.count() - 200

- db.students.find( { _id : 137 } ).pretty( )

{
    "_id" : 137,
    "name" : "Tamika Schildgen",
    "scores" : [
        {
            "type" : "exam",
            "score" : 4.433956226109692
        },
        {
            "type" : "quiz",
            "score" : 65.50313785402548
        },
        {
            "type" : "homework",
            "score" : 89.5950384993947
        }
    ]
}



db.students.aggregate( [
  { '$unwind': '$scores' },
  {
    '$group':
    {
      '_id': '$_id',
      'average': { $avg: '$scores.score' }
    }
  },
  { '$sort': { 'average' : -1 } },
  { '$limit': 1 } ] )