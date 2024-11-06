# **Norebase Like Task**

## **A [C#](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/) API for liking articles built with [.NET 6.0](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-6)**


# Pre-requisites
* [Postgres](https://www.postgresql.org/download/)
* [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)


### Clone this repository
```
$ git clone https://github.com/Chibuoyimm/norebase.git
```

### Then go into main directory and install the dependencies
```
$ cd src/main/NorebaseTask.Api
```
```
$ dotnet restore
```

# Usage

### Run development server
```
$ dotnet run
```

## Testing with custom written tests

```
$ cd ../../test/NorebaseTask.Api.Tests
```
```
$ dotnet test
```

## Testing with postman

> Server must be running (see step to run development server)

### Register User

```
POST
http://localhost:5114/api/authentication/register
```
> JSON body with username, email and password is required

### Login

```
POST
http://localhost:5114/api/authentication/login
```
> JSON body with email and password is required

### Create Article

```
POST
http://localhost:5114/api/articles
```
> JSON body with title and body is required. Also, token from login is required for authentication

### Get all Articles

```
GET
http://localhost:5114/api/articles
```

### Like Article

```
POST
http://localhost:5114/api/articles/like/{articleId}
```
> Token from login is required for authentication

### UnLike Article

```
POST
http://localhost:5114/api/articles/unlike/{articleId}
```
> Token from login is required for authentication

### Get Article Likes

```
GET
http://localhost:5114/api/articles/like/{articleId}
```
> Token from login is required for authentication


## Check out API documentation

Go to `http://localhost:5114/swagger/v1/swagger.json`
> NOTE: local development server must still be running


## Database Diagram

![Diagram](/db-diagram.png)


# Design and Architecture of API

* **I built the Norebase Like Task API using .NET 6.0 and PostgreSQL**: 
.NET 6.0 is a super-fast, cross-platform framework that’s perfect for building modern web applications and APIs. It helps me create high-performance services that run efficiently. Specifically, I used ASP.NET Core, which is lightweight and designed to build RESTful APIs quickly.

* **For data storage, I chose PostgreSQL**: 
PostgreSQL is a powerful, open-source relational database that’s known for being reliable and scalable. It’s ideal for managing all the app's data, and its advanced features give me a lot of flexibility as the application grows.

* **I used Entity Framework Core (EF Core) to interact with the database**: 
EF Core is an ORM (Object-Relational Mapping) tool that makes it easier to work with databases. Instead of writing raw SQL queries, I can use .NET objects to interact with the data. This helps speed up development and keeps the code clean and simple.

* **The API has a set of endpoints to handle things like user registration, logging in, managing articles, and liking/unliking them**: 
I created models for Users and Articles and built services around them to manage everything. This makes it easy to add new features or modify the app as needed.

* **JWT (JSON Web Tokens) are used to secure the API**: 
I implemented JWT authentication to make sure that only logged-in users can access certain actions, like creating articles or liking them. When users log in, they get a token, which they need to include in their requests to prove they’re authorized. It’s a secure, efficient way to protect the app.

* **I included Swagger documentation for easy API exploration:** 
To make it easy for other developers (or anyone really) to interact with the API, I added Swagger. It’s a really handy tool that provides an interactive interface, showing all the available endpoints and letting users test them directly. It helps users understand how the API works and makes it a lot easier to get started.







