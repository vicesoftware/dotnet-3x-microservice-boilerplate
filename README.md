# .NET Microservice and Docker

This is a sample project for a microservice written in .NET Core 2.2 and C# that accesses a Postgres DB. The goal of this project is to establish a smooth process for test drive development (TDD) of the microservice leveraging the power of Docker container and the `docker-compose` tool.

Please read my blog post describing the details: http://gabrielschenker.com/index.php/2019/10/04/a-docker-workflow-for-net-developers/

# Setup
If this is the very first time you run an ASP.NET Core application, you should trust the HTTPS development certificate included in the .NET Core SDK. This task depends on your operating system. Please, take a look at the [official documentation](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-3.0&tabs=visual-studio#trust) to apply the proper procedure.