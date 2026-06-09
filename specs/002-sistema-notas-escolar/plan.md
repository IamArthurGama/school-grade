# Implementation Plan: Sistema de Notas Escolar

**Branch**: `002-sistema-notas-escolar` | **Date**: 04/06/2026 | **Spec**: [spec.md](spec.md)

**Input**: Feature specification from `/specs/002-sistema-notas-escolar/spec.md`

## Summary

Build a web application for school grade management where users can maintain students, subjects, and grades, then consult combined records and subject averages. The implementation will use ASP.NET Core MVC with Razor Views, Bootstrap, SQL Server persistence through Entity Framework Core, repositories for data access boundaries, services for business rules, and xUnit tests following a TDD workflow.

## Technical Context

**Language/Version**: C# with current stable .NET SDK suitable for ASP.NET Core MVC

**Primary Dependencies**: ASP.NET Core MVC, Razor Views, Bootstrap, Entity Framework Core, SQL Server provider, xUnit

**Storage**: SQL Server database managed through Entity Framework Core migrations

**Testing**: xUnit unit tests for services/repositories and integration-style tests for core MVC flows where practical

**Target Platform**: Web application running locally or on a Windows/Linux server capable of hosting ASP.NET Core and reaching SQL Server

**Project Type**: Server-rendered web application

**Performance Goals**: CRUD and consultation pages should render within 2 seconds for normal classroom-sized datasets; average calculations should complete immediately for acceptance-test data

**Constraints**: Keep grade validation on the 0 to 10 scale; preserve referential integrity between students, subjects, and grades; avoid orphaned grades

**Scale/Scope**: Single school-grade management app with three primary entities, CRUD screens, and three required query demonstrations

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

The constitution file currently contains only template placeholders and no enforceable project principles. No gate violations are present.

Post-design re-check: PASS. The design keeps implementation scope aligned with the specification, preserves testability through services and repositories, and does not introduce extra systems beyond the requested web app.

## Project Structure

### Documentation (this feature)

```text
specs/002-sistema-notas-escolar/
|-- plan.md
|-- research.md
|-- data-model.md
|-- quickstart.md
|-- contracts/
|   `-- web-ui.md
|-- checklists/
|   `-- requirements.md
`-- tasks.md
```

### Source Code (repository root)

```text
SistemaNotasEscolar/
|-- Controllers/
|   |-- AlunosController.cs
|   |-- DisciplinasController.cs
|   |-- NotasController.cs
|   `-- ConsultasController.cs
|-- Data/
|   |-- AppDbContext.cs
|   `-- DbInitializer.cs
|-- Models/
|   |-- Aluno.cs
|   |-- Disciplina.cs
|   `-- Nota.cs
|-- Repositories/
|   |-- IAlunoRepository.cs
|   |-- IDisciplinaRepository.cs
|   |-- INotaRepository.cs
|   |-- AlunoRepository.cs
|   |-- DisciplinaRepository.cs
|   `-- NotaRepository.cs
|-- Services/
|   |-- IAlunoService.cs
|   |-- IDisciplinaService.cs
|   |-- INotaService.cs
|   |-- IConsultaService.cs
|   |-- AlunoService.cs
|   |-- DisciplinaService.cs
|   |-- NotaService.cs
|   `-- ConsultaService.cs
|-- Views/
|   |-- Alunos/
|   |-- Disciplinas/
|   |-- Notas/
|   |-- Consultas/
|   `-- Shared/
|-- Migrations/
|-- appsettings.json
|-- Program.cs
`-- SistemaNotasEscolar.csproj

Tests/
|-- SistemaNotasEscolar.Tests.csproj
|-- Services/
|   |-- AlunoServiceTests.cs
|   |-- DisciplinaServiceTests.cs
|   |-- NotaServiceTests.cs
|   `-- ConsultaServiceTests.cs
`-- Repositories/
    `-- RepositoryPersistenceTests.cs
```

**Structure Decision**: Use a single ASP.NET Core MVC web project plus a separate xUnit test project. MVC controllers handle request flow, services enforce business rules, repositories isolate Entity Framework Core access, and Razor Views provide the user interface.

## Complexity Tracking

No constitution violations require justification.
