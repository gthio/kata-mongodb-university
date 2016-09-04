# Aggregation

Aggregation pipeline is a framework for data aggregation modeled on the concept of data processing pipelines.
Documents enter a multi stage pipline that transform the documents into aggregated results.

    db.collection.aggregate([$ match stage, $ group stage])


