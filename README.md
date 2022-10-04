# university

This is an example about **.NET 6.0 WebApi**. The application is made based on **MVC-model**. There is one Controller which is named StudentController and a model which is named Student.

The data is hard coded to the model, in order to make the application simple.

## ER model

<img src="er_model.png">

<a href="https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax">instructions for writing README file</a>

## Database connection

For development (local machine) I created environment variable called "DATABASE_URL" in Program.cs. And in Database.cs I use that variable. So in production environment (example Heroku), I set enviroment variable which is configured for the Heroku MySQL.

## JOIN Queries

I made a couple of examples about INNER JOIN queries. The controller is named **StudentdataController.cs** and the queries are in models: **Studendata_model.cs** and **Studentgrade_model.cs**. And note that I manipulate the dates with the **DATE_FORMAT** function in MySQL and then I have to use **string** type for that property in the model.

## Authentication

In the file BasicAuthentication I have this line

Database db = new Database(System.Environment.GetEnvironmentVariable("DATABASE_URL"));

It has a problem: after reboot, we have to call first login and then we can call protected route. But that is the way the application will be used any way. 