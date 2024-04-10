<div id="top"></div>

<div align="center">
  <a href="https://github.com/OxyProgrammer/aspnet-plus">
    <img src="ASPNetPlus.png" alt="Logo" width="790">
  </a>
  <br/>
  <h1 align="center"><u>ASPNet Plus</u></h1>

  <p align="center">
    Welcome to ASPNETPlus!
    <br/>
    <a href="https://github.com/OxyProgrammer/aspnet-plus/issues">Report Bug</a>
    ·
    <a href="https://github.com/OxyProgrammer/aspnet-plus/issues">Request Feature</a>
  </p>
  </div>

[![GitHub issues](https://img.shields.io/github/issues/OxyProgrammer/aspnet-plus?style=for-the-badge)](https://github.com/OxyProgrammer/aspnet-plus/issues)
[![GitHub forks](https://img.shields.io/github/forks/OxyProgrammer/aspnet-plus?style=for-the-badge)](https://github.com/OxyProgrammer/aspnet-plus/network)
[![GitHub stars](https://img.shields.io/github/stars/OxyProgrammer/aspnet-plus?style=for-the-badge)](https://github.com/OxyProgrammer/aspnet-plus/stargazers)
[![GitHub license](https://img.shields.io/github/license/OxyProgrammer/aspnet-plus?style=for-the-badge)](https://github.com/OxyProgrammer/aspnet-plus)

<!-- ABOUT THE PROJECT -->

## About The Project

This repository contains a comprehensive ASP.NET Core Web API project showcasing various features of ASP.NET Core with .NET 8.

- **Onion Architecture**: Organized using Onion architecture principles to maintain modularity and separation of concerns.
- **Logging using NLog**: Detailed setup and configuration of NLog for logging in ASP.NET Core.
- **Filters**: Implementation of filters for request/response processing.
- **Error Handling Best Practices**: Demonstrates best practices for error handling in ASP.NET Core.
- **Content Negotiation**: Supports content negotiation for JSON, XML, and custom formats.
- **Bulk Creation of Resources**: Implementation of bulk creation endpoints.
- **Validation**: Request input validation using built-in ASP.NET Core features.
- **Handling Parent-Child Relations**: Strategies for handling entities with parent-child relations.
- **HTTP Actions**: Implementation of standard HTTP actions like GET, POST, PUT, DELETE, OPTIONS, HEAD.
- **Advanced Data Handling**: Features filtering, paging, searching, sorting, and data shaping to minimize network bandwidth.
- **Rate Limiting and Throttling**: Implementation of rate limiting and throttling for API endpoints.
- **Versioning**: API versioning to support backward compatibility.
- **Documentation using Swagger**: Integration with Swagger for API documentation.

<p align="right">(<a href="#top">back to top</a>)</p>

## Setup

### Docker Desktop

To run this project using Docker Desktop, follow these steps:

+ Clone the repository to your local machine:
   ```bash
   git clone https://github.com/your-username/ASPNETPlus.git

+ Navigate to the project directory:
   ```bash
   cd ASPNETPlus
+ Run the Docker container:
    ```bash
    docker run -d -p 8080:80 aspnetplus
+ Access the API using your web browser or a tool like Postman:
    ```bash
    http://localhost:8080/swagger

<p align="right">(<a href="#top">back to top</a>)</p>

### Visual Studio and MS SQL Database

To run this project using Visual Studio and MS SQL Database, follow these steps:

+ Clone the repository to your local machine:
    ```bash
    git clone https://github.com/your-username/ASPNETPlus.git

+ Open the solution in Visual Studio.

+ Update the database connection string in appsettings.json to point to your MS SQL Database.

+ Build and run the project using Visual Studio.

+ Access the API using your web browser or a tool like Postman:
    ```bash
    http://localhost:5000/swagger

<p align="right">(<a href="#top">back to top</a>)</p>

### Usage
Once the project is up and running, you can interact with the API endpoints to test the features showcased in this project. Here are some example requests:

<!-- Provide examples of API requests and responses -->

## Contributing
Contributions are welcome! If you'd like to contribute to this project, please follow these steps:

+ Fork the repository.
+ Create a new branch for your feature or bug fix.
+ Make your changes.
+ Test your changes thoroughly.
+ Commit your changes with descriptive commit messages.
+ Push your changes to your fork.
+ Submit a pull request to the main repository's main branch.
+ License
+ This project is licensed under the MIT License.

<p align="right">(<a href="#top">back to top</a>)</p>

## Acknowledgements

+ ASP.NET Core
+ NLog
+ Swagger

<p align="right">(<a href="#top">back to top</a>)</p>

## Contact

If you have any questions, suggestions, or issues, please feel free to contact the project maintainer:

OxyProgrammer  - oxyprogrammer@gmail.com

Project Link: [https://github.com/OxyProgrammer/aspnet-plus](https://github.com/OxyProgrammer/aspnet-plus)

<p align="right">(<a href="#top">back to top</a>)</p>