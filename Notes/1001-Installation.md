# Installation

## Versions
* MongoDB Community Edition
* MongoDB Enterprise Edition

## OS
* Linux
* Windows
* OSX

## Windows - MongoDB Community Edition
* Create data directory
    - Default data directory is \data\db

* Start MongoDB
    - Run mongod.exe
    - With alternate path, 
        > mongod.exe --dbpath d:\test\mongod\data

        > mongod.exe --dbpath "d:\my test\mongod\data"

    - With non default port (default port is 27017)
        > mongod.exe --port 28000

    - With storageEngine 
        > mongod.exe --storageEngine mmapv1  

* Connect to MongoDB (via shell)
    > Mongo.exe

    > Mongo.exe --port 28000



## Configuration file
* Contains settings that are equivalent to the command line options
* Configuration file format is YAML format.
* To start MongoDB with configuration file,
    > Mongod.exe --config /etc/mongod.cfg
* Core options
    - systemLog.path

        The path of the log file for all the diagnostic logging information. 

    - systemLog.append

        true, it appends new entries to the existing log file. The default is MongoDB will back up existing file and create a new file.

    - storage.dbPath

        The path of the data storage. The default is \data\db

    - storage.engine

        The storage engine for MongoDB

        - mmapv1 (only option for 32-bit)
        - wiredTiger (default version 3.2+)
        - inMemory (Enterprise edition 3.2+)
