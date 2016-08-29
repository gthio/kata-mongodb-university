# The mongo Shell

- A component part of the MongoDB distributions.
- Start MongoDB Shell   
    - Default run, connect to localhost port 27017
        > mongo.exe

    - Connet to localhost, non default port
        > mongo.exe --port 28015

    - Connect to remote host
        > mongo --username {user} --password {password} --host {host} --port {port}

- Exit MongoDB Shell

    - quit()
    - <ctrl - c>

- Print result

    - Print without formatting
        > print()

    - Print with JSON formatting
        > print(tojson(<obj>))
        > printjson(<obj>)

- External editor

    - Before starting MongoDB Shell
        > SET EDITOR = Notepad

        > export EDITOR = vim

    - Open MongoDB Shell

    - Define variable or function
        > function myFunction(){}
        
    - Edit the function in external editor, save and Exit
        > edit myFunction

- Change MongoDB Shell batch size

    - To change the number of documents returned, from the default value 20

        > DBQuery.shellBatchSize = 5
        
- Customize MongoDB Shell prompt

    - To display prompt as <database>$
        > prompt = function() {
            return db + " > ";
        }

- Help

    - Command line Help

        > mongo --help
        
    - MongoDB Shell

        > show dbs

        > db.help()

        > db.<methodName>

        > show collections

        > db.<collection>.help()

        > db.<collection>.<methodName>

        > db.<collection>.find().help()

        > db.<collection>.find().toArray
