# 👨‍💼 FoodServiceAPI

## FoodServiceAPI Documentation

Welcome to the documentation of FoodServiceAPI, a Web API that provides all the logical layer of our food service.

### Introduction

FoodServiceAPI is a .NET application that utilizes .NET Core 8.0. It connects to a MySQL database for data storage, authentication is performed via JWT using the Microsoft.AspNetCore.Authentication.JwtBearer Version="8.0.4" package, and it utilizes Entity Framework Core for data access.

### Requirements

To use FoodServiceAPI, you need to have installed:

* .NET Core 8.0
* MySQL Server
* NuGet Packages: Microsoft.AspNetCore.Authentication.JwtBearer Version="8.0.4", EntityFrameworkCore Version="8.0.2"

### Installation

1. Clone the repository:

```bash
git clone https://github.com/Org-FoodService/FoodServiceAPI.git
```

2. Open the project in Visual Studio or your preferred code editor.
3. Configure the connection to the MySQL database in the `appsettings.json` file.
4. Build and run the application.

### Endpoints

FoodServiceAPI has the following endpoints:

1. **GET /api/products**
   * Returns a list of all available products.
2. **GET /api/products/{id}**
   * Returns details of a specific product with the provided ID.
3. **POST /api/products**
   * Creates a new product.
4. **PUT /api/products/{id}**
   * Updates details of an existing product with the provided ID.
5. **DELETE /api/products/{id}**
   * Removes a product with the provided ID.

### Authentication

Authentication in FoodServiceAPI is performed using the JWT (JSON Web Tokens) scheme. To authenticate, include the JWT token in the HTTP request header.

### Example Usage

1. Authenticating and obtaining a JWT token:

```http
POST /api/authenticate HTTP/1.1
Content-Type: application/json

{
    "username": "your_username",
    "password": "your_password"
}
```

2. Using the JWT token to access a protected endpoint:

```http
GET /api/products HTTP/1.1
Authorization: Bearer YOUR_JWT_TOKEN
```
