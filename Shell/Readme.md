# Getting Started

### Run MongoDB
1. Set up environment
2. Start MongoDB
```
mongod.exe --dbpath d:\test\mongodb\data
```
```
mongod.exe --dbpath "d:\test\mongodb\data folder with space"
```
3. Connect to MongoDB through shell
```
mongo.exe
```

### Import data into MongoDB collection
```
mongoimport --db testdb --collection restaurants --drop --file ../Datasets/primer-dataset.json
```