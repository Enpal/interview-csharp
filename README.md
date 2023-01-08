Backend Coding Challenge

Overview

The goal of the project is to implement two endpoints, which are:

    An endpoint that takes a long URL as input and returns a shortened version of that URL.
    An endpoint that can be accessed through the shortened URL and redirects the user to the original long URL.

Requirements

    The shortened URL should be a unique key that can be used to retrieve the original long URL.
    The shortened URL should be accessible through a web browser.
    The shortened URL should redirect the user to the original long URL.

System Requirements

    A functional installation of the .NET Core 7.0 SDK
    A code editor you're comfortable with.

Getting Started

    Clone the project repository
    Run the application with your IDE of choice or by running dotnet run --project ./src/Api/Api.csproj in the project root directory in a terminal.
    Implement the required endpoints. Basic scaffolding for them already exists in the <project_root>/src/Api/Endpoints/Url directory.
    Test your implementation using a web browser or a tool like curl
