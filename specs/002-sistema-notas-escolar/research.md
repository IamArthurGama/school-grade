# Research: Sistema de Notas Escolar

## Decision: ASP.NET Core MVC with Razor Views

**Rationale**: The requested application is a server-rendered web app centered on CRUD screens and consultation pages. MVC maps cleanly to controllers, models, views, and form-based workflows.

**Alternatives considered**: A separate SPA frontend was rejected because the scope does not require client-side complexity. A minimal API-only backend was rejected because the requested UI uses Razor Views.

## Decision: Entity Framework Core with SQL Server and migrations

**Rationale**: The project requires persistent relational storage, controlled schema evolution, and relationships between students, subjects, and grades. EF Core migrations provide repeatable database setup and update steps.

**Alternatives considered**: Direct SQL access was rejected because it would increase boilerplate and weaken testability. File-based storage was rejected because it does not meet the SQL Server persistence requirement.

## Decision: Repository Pattern plus Services

**Rationale**: Repositories abstract data operations for Aluno, Disciplina, and Nota, while services concentrate validation rules such as unique matricula, grade range, and blocked deletion when grades exist.

**Alternatives considered**: Controllers accessing EF Core directly was rejected because it mixes request handling with persistence and business rules. A generic-only repository was rejected because required queries are clearer with entity-specific repositories.

## Decision: xUnit with TDD workflow

**Rationale**: Service and query rules can be specified with focused tests before implementation. This supports the required red-green-refactor workflow and makes grade validation and average calculations repeatable.

**Alternatives considered**: Manual testing only was rejected because it does not satisfy TDD. UI-only tests were rejected as the first layer because they are slower and less focused than service-level tests.

## Decision: Query demonstrations live in a consultation service

**Rationale**: The required combined query, grouped averages, and filtered grouped results are business-facing consultations. Keeping them in `ConsultaService` makes them testable and reusable by `ConsultasController`.

**Alternatives considered**: Embedding LINQ directly in Razor Views was rejected because it would hide business behavior inside presentation code. Embedding all queries in controllers was rejected for testability and separation of concerns.
