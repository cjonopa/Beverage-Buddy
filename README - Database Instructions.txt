*** Getting the databaase up and running ***

when you first run this you will need to install dotnet Entity Framework to get the database.
Here are the steps needed to take:
1.	open a cmd line interface and go to the projects main folder (ex. C:\Projects\C#\APC460\beverage-buddy)
2.	in the cmd line enter the following - dotnet tool install dotnet-ef -g
	a. this will install the Entity Framework tool onto your computer globally so you can use it
3.	Once you have done this, you will need to go the Beverage-Buddy.Data folder - 
		(C:\Projects\C#\APC460\beverage-buddy\Beverage-Buddy.Data)
4.	In here you will then run the command - dotnet ef database update
	a. this will run all the migration code in the Migration folder
5.	Open SQL Server Object Explorer
6.	Go the localdb\MSSQLLocalDB server and see if the BeverageBuddy database is there

*** Making changes to the entities ***

IF you make any change to the entities, you will need to create a migration and push it up. here is
how to do that.
1.	you will need to go the Beverage-Buddy.Data folder - 
		(C:\Projects\C#\APC460\beverage-buddy\Beverage-Buddy.Data)
2.	in the cmd line run the following command - dotnet ef migrations add {descriptive name of change}
	a. this creates a migration code in the Migration folder
3.	after this run you can then check the migration in the folder and make sure it looks accurate
4.	next in the cmd line you will now run - dotnet ef database update
	a. this will then push your migration to the database and record the entry in the _EFMigrationsHistory 
	   table in the database.