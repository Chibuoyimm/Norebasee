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



# Design and Architecture of API

* The Norebase Like Task API was built using the .NET 6.0 Framework and PostgreSQL as the data storage mechanism.
* .NET 6.0 is a high-performance, cross-platform framework designed for building modern web applications, APIs, and microservices. The API is built using ASP.NET Core, which is a modular, lightweight, and fast framework optimized for high-performance RESTful services.
* The PostgreSQL database is used for persistent storage of data. It’s a powerful, open-source relational database that is known for its robustness, scalability, and support for advanced data types.
* Entity Framework Core (EF Core) is used as the Object-Relational Mapping (ORM) layer for interacting with the PostgreSQL database. It provides an easy-to-use interface to work with relational databases through .NET objects.
* The application provides a set of endpoints for user authentication, article management, and liking/unliking articles. This is made possible by defining models for Users and Articles and implementing services for handling these entities.
* The JWT (JSON Web Token) authentication mechanism is used to secure the API endpoints, ensuring that only authenticated users can interact with certain resources like creating articles, liking, and unliking them.
* The Swagger documentation is provided for API consumers to easily explore the endpoints and understand the request/response formats.







