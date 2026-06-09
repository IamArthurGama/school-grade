# Quickstart: Sistema de Notas Escolar

## Prerequisites

- .NET SDK installed.
- SQL Server available locally or through a configured connection string.

## Create Project Structure

```powershell
dotnet new mvc -n SistemaNotasEscolar
dotnet new xunit -n Tests
dotnet new sln -n SistemaNotasEscolar
dotnet sln add SistemaNotasEscolar/SistemaNotasEscolar.csproj
dotnet sln add Tests/Tests.csproj
dotnet add Tests/Tests.csproj reference SistemaNotasEscolar/SistemaNotasEscolar.csproj
```

## Add Required Packages

```powershell
dotnet add SistemaNotasEscolar/SistemaNotasEscolar.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add SistemaNotasEscolar/SistemaNotasEscolar.csproj package Microsoft.EntityFrameworkCore.Tools
```

## Configure Database

Set a SQL Server connection string in `SistemaNotasEscolar/appsettings.json`, then register the application DbContext in `Program.cs`.

## Run Migrations

```powershell
dotnet ef migrations add InitialCreate --project SistemaNotasEscolar
dotnet ef database update --project SistemaNotasEscolar
```

## TDD Workflow

1. Write xUnit tests for the next service or query rule.
2. Run tests and confirm they fail for the expected reason.
3. Implement the minimal model, repository, service, controller, or view change.
4. Run tests again.
5. Refactor while keeping tests green.

## Run Tests

```powershell
dotnet test
```

## Run Application

```powershell
dotnet run --project SistemaNotasEscolar
```

Open the printed localhost URL and verify Alunos, Disciplinas, Notas, and Consultas.
