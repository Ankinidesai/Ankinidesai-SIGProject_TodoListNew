Pre-requirements: •	Visual Studio 2017 •	.NET Core SDK •	SQL Server •	Install Package using Nugget Manger • PM>Install-Package Microsoft.jQuery.Unobtrusive.Ajax -Version 3.2.6

Key components: Configuration Files:

web.config file: contains all the necessary configuration information of this web application.
global.asax: contains all the URL routing rules
Models:

TodoDetails: This model class contains all the properties need to link up with the database for a todo.
Identity Model: This is an automated model built at the time of the creating Project. This model deals with the user authentication. It is the model for identity. ApplicationdbContext is referring to data access layer and it is the database of all the user as it inherits IdentityDbContext. Also, I have added a table for todos that is a collection of todos named as TodoDetails.
Views (TodoDetails/Views):

_tabView: This is a partial view that is created for displaying the table of the TODOs for the current user.
Index: This view returns the partial view using ajax and jQuery.
allTodos: This will return the list of all the todos.
Controllers:

TodoDetailsController: contains navigation and crud logic of all todos.
HomeController class: contains the main application navigation logic(such as default page and about page).

Steps on How to Run:

Open the TodoDetails.sln in Visual Studio 2017.
Update-database-verbose / -force execute the command in package manager console  to look at the Database.
Install the Ajax by running the command in to Tools>NuGet Package Manager>Package Manager Console PM> Install-Package Microsoft.jQuery.Unobtrusive.Ajax -Version 3.2.6
Log in by entering username u1@gmail.com and password: password to login. Or Register yourself.
Create a todo and sub todo by entering the required details.
If the particular todo is done, mark the checkbox and it will get highlighted.
If the parent or child todo has pass the over date it will highlight in red.
You can edit/delete the todo. You can also see the details of the todo.
