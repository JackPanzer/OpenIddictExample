How to start up this proof of concept

------------------------------------------------

1.- SqlServer Express installed. You can use whatever database software might want, but the scripts provided for this solution have been tested only on it.

LOCAL
=========

This README assumes that you have VS2022 or equivalent installed for asp.net core development with net6 with a working installation of SQLServer Express.
	
Once installed go to the following folder, C:\Program Files\Microsoft SQL Server\<sql server version>\LocalDB\Bin

And open a new terminal in this path

Execute the following commands:

- SqlLocalDB.exe create OpenIddictExample
- SqlLocalDB.exe start OpenIddictExample

With this, you can access the database either with Sql Server Management Studio or Azure Data Studio using the following connection string
(localdb)\\OpenIddictExample
If any authentication is required, select windows authentication.

------------------------------------------------

2.- Create the example database

The database has been created with a simple create database statement. Feel free to use whatever helps you create the database.

Once created, basically dump all the scripts inside the Sql folder so there is no need to run the EF migrations.

------------------------------------------------

At the end of the process you should have a functional solution to start tinkering.
The connection string will end up being something like this:

- Data Source=(localdb)\\OpenIddictExample;Initial Catalog=OpenIddictExample;Integrated Security=True;

Happy coding