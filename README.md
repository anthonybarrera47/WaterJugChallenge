# Water Jug Challenge

## Description

The **Water Jug Challenge** is a web application that solves the classic water jug problem using the breadth-first search (BFS) algorithm. The application is built with .NET 8 and follows development best practices including SOLID, DRY, and KISS. This project also includes integration testing to ensure proper functionality of the components.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Clone the Repository

```bash
git clone https://github.com/anthonybarrera47/water-jug-challenge.git 
cd water-jug-challenge
```

### Building the Project
Open the solution in Visual Studio 2022.
Restore the NuGet packages.
Build the solution to ensure all dependencies are correctly installed.
### Usage
Run the application from Visual Studio by pressing F5 or using the dotnet run command from the terminal.
Use Swagger UI, which will be available at http://localhost:5000/swagger to interact with the API.
### Project Architecture
The project is structured as follows:

- Controllers: Contains the WaterJugController.cs which handles API requests.
- Models: Includes WaterJugRequest.cs and WaterJugState.cs which define the data structures.
- Services: Contains business logic for solving the water jug problem. The WaterJugSolver.cs implements the BFS algorithm.
- Program.cs: The entry point of the application.

### Technical Details
#### Water Jug Solver
The core logic is implemented in the WaterJugSolver class, which utilizes the BFS algorithm to find the solution to the water jug problem.

#### Dependency Injection
The project uses dependency injection to manage services and their lifetimes, ensuring loose coupling and better testability.

### Testing
The solution includes a separate test project, WaterJugChallengeTest, which contains integration tests for the application.

#### Testing Frameworks
- xUnit: Used for writing and running tests.
- Moq: Used for mocking dependencies in tests.
- Microsoft.AspNetCore.Mvc.Testing: Provides integration testing functionality.
#### Running Tests
- Open the Test Explorer in Visual Studio.
- Run all tests to ensure everything is working as expected.

## Example

### Request
POST /api/waterjug/GetSolution
#### Body
```json
{
  "xCapacity": 2,
  "yCapacity": 10,
  "zAmountWanted": 4
}
```
### Response
#### Success
```json
{
  "solution": [
    {
      "step": 1,
      "bucketX": 2,
      "bucketY": 0,
      "action": "Fill bucket X",
      "status": "Unsolved"
    },
    {
      "step": 2,
      "bucketX": 0,
      "bucketY": 2,
      "action": "Transfer from X to Y",
      "status": "Unsolved"
    },
    {
      "step": 3,
      "bucketX": 2,
      "bucketY": 2,
      "action": "Fill bucket X",
      "status": "Unsolved"
    },
    {
      "step": 4,
      "bucketX": 0,
      "bucketY": 4,
      "action": "Transfer from X to Y",
      "status": "Solved"
    }
  ]
}
```
#### Failure
```json
{
  "solution": [],
  "message": "No solution possible"
}
```