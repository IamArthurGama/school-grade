# Tasks: Sistema de Notas Escolar

**Input**: Design documents from `/specs/002-sistema-notas-escolar/`

**Prerequisites**: plan.md, spec.md, research.md, data-model.md, contracts/web-ui.md, quickstart.md

**Tests**: TDD is explicitly required. Test tasks are listed before implementation tasks for each user story and should fail before implementation.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel because it touches different files and has no dependency on incomplete tasks.
- **[Story]**: Maps task to a user story from `spec.md`.
- Every task includes an exact target path.

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Create the solution, projects, dependencies, and base folders.

- [X] T001 Create ASP.NET Core MVC project in `SistemaNotasEscolar/SistemaNotasEscolar.csproj`
- [X] T002 Create xUnit test project in `Tests/SistemaNotasEscolar.Tests.csproj`
- [X] T003 Create solution file and add both projects in `SistemaNotasEscolar.sln`
- [X] T004 Add project reference from `Tests/SistemaNotasEscolar.Tests.csproj` to `SistemaNotasEscolar/SistemaNotasEscolar.csproj`
- [X] T005 Add Entity Framework Core SQL Server and Tools packages to `SistemaNotasEscolar/SistemaNotasEscolar.csproj`
- [X] T006 [P] Create planned MVC folders in `SistemaNotasEscolar/Controllers`, `SistemaNotasEscolar/Models`, `SistemaNotasEscolar/Data`, `SistemaNotasEscolar/Repositories`, `SistemaNotasEscolar/Services`, and `SistemaNotasEscolar/Views`
- [X] T007 [P] Create planned test folders in `Tests/Services` and `Tests/Repositories`
- [X] T008 Configure SQL Server connection string placeholder in `SistemaNotasEscolar/appsettings.json`

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Establish shared infrastructure required before user story implementation.

**CRITICAL**: No user story work should begin until this phase is complete.

- [X] T009 Create application DbContext skeleton in `SistemaNotasEscolar/Data/AppDbContext.cs`
- [X] T010 Register MVC, DbContext, repositories, and services placeholders in `SistemaNotasEscolar/Program.cs`
- [X] T011 [P] Create shared validation result helper in `SistemaNotasEscolar/Services/ServiceResult.cs`
- [X] T012 [P] Create shared layout navigation entries for Alunos, Disciplinas, Notas, and Consultas in `SistemaNotasEscolar/Views/Shared/_Layout.cshtml`
- [ ] T013 [P] Create database initializer placeholder in `SistemaNotasEscolar/Data/DbInitializer.cs`
- [X] T014 Configure test database or in-memory context factory in `Tests/Repositories/TestDbContextFactory.cs`

**Checkpoint**: Foundation ready. User story implementation can now begin.

---

## Phase 3: User Story 1 - Gerenciar cadastros academicos (Priority: P1) MVP

**Goal**: Users can create, edit, delete, and list students and subjects.

**Independent Test**: Create, update, list, and delete students and subjects without registering grades.

### Tests for User Story 1

- [X] T015 [P] [US1] Write failing tests for required and unique student enrollment validation in `Tests/Services/AlunoServiceTests.cs`
- [X] T016 [P] [US1] Write failing tests for required subject name validation in `Tests/Services/DisciplinaServiceTests.cs`
- [X] T017 [P] [US1] Write failing repository persistence tests for students and subjects in `Tests/Repositories/RepositoryPersistenceTests.cs`

### Implementation for User Story 1

- [X] T018 [P] [US1] Create Aluno model with Id, Nome, Matricula, and Notas navigation in `SistemaNotasEscolar/Models/Aluno.cs`
- [X] T019 [P] [US1] Create Disciplina model with Id, Nome, and Notas navigation in `SistemaNotasEscolar/Models/Disciplina.cs`
- [X] T020 [US1] Add Alunos and Disciplinas sets and uniqueness configuration to `SistemaNotasEscolar/Data/AppDbContext.cs`
- [X] T021 [P] [US1] Create student repository interface in `SistemaNotasEscolar/Repositories/IAlunoRepository.cs`
- [X] T022 [P] [US1] Create subject repository interface in `SistemaNotasEscolar/Repositories/IDisciplinaRepository.cs`
- [X] T023 [P] [US1] Implement student repository CRUD in `SistemaNotasEscolar/Repositories/AlunoRepository.cs`
- [X] T024 [P] [US1] Implement subject repository CRUD in `SistemaNotasEscolar/Repositories/DisciplinaRepository.cs`
- [X] T025 [P] [US1] Create student service interface in `SistemaNotasEscolar/Services/IAlunoService.cs`
- [X] T026 [P] [US1] Create subject service interface in `SistemaNotasEscolar/Services/IDisciplinaService.cs`
- [X] T027 [US1] Implement student validation and CRUD orchestration in `SistemaNotasEscolar/Services/AlunoService.cs`
- [X] T028 [US1] Implement subject validation and CRUD orchestration in `SistemaNotasEscolar/Services/DisciplinaService.cs`
- [X] T029 [US1] Register concrete student and subject repositories/services in `SistemaNotasEscolar/Program.cs`
- [X] T030 [P] [US1] Create AlunosController CRUD actions in `SistemaNotasEscolar/Controllers/AlunosController.cs`
- [X] T031 [P] [US1] Create DisciplinasController CRUD actions in `SistemaNotasEscolar/Controllers/DisciplinasController.cs`
- [X] T032 [P] [US1] Create Alunos Razor views Index/Create/Edit/Delete in `SistemaNotasEscolar/Views/Alunos`
- [X] T033 [P] [US1] Create Disciplinas Razor views Index/Create/Edit/Delete in `SistemaNotasEscolar/Views/Disciplinas`
- [X] T034 [US1] Create initial migration for students and subjects in `SistemaNotasEscolar/Migrations`
- [X] T035 [US1] Run all US1 tests with `dotnet test` from `SistemaNotasEscolar.sln`

**Checkpoint**: User Story 1 is functional and independently testable.

---

## Phase 4: User Story 2 - Registrar e consultar notas (Priority: P2)

**Goal**: Users can create, edit, delete, and list grades linked to one student and one subject.

**Independent Test**: Create a student and subject, register a grade for them, edit it, list it with names, and delete it.

### Tests for User Story 2

- [X] T036 [P] [US2] Write failing tests for grade range and required relationship validation in `Tests/Services/NotaServiceTests.cs`
- [X] T037 [P] [US2] Write failing repository persistence tests for grades with student and subject references in `Tests/Repositories/RepositoryPersistenceTests.cs`

### Implementation for User Story 2

- [X] T038 [US2] Create Nota model with Id, Valor, AlunoId, DisciplinaId, and navigation properties in `SistemaNotasEscolar/Models/Nota.cs`
- [X] T039 [US2] Configure Nota relationships and grade precision/range support in `SistemaNotasEscolar/Data/AppDbContext.cs`
- [X] T040 [P] [US2] Create grade repository interface in `SistemaNotasEscolar/Repositories/INotaRepository.cs`
- [X] T041 [US2] Implement grade repository CRUD with included Aluno and Disciplina data in `SistemaNotasEscolar/Repositories/NotaRepository.cs`
- [X] T042 [P] [US2] Create grade service interface in `SistemaNotasEscolar/Services/INotaService.cs`
- [X] T043 [US2] Implement grade validation and CRUD orchestration in `SistemaNotasEscolar/Services/NotaService.cs`
- [X] T044 [US2] Register grade repository and service in `SistemaNotasEscolar/Program.cs`
- [X] T045 [US2] Add migration for grades and relationships in `SistemaNotasEscolar/Migrations`
- [X] T046 [US2] Create NotasController CRUD actions in `SistemaNotasEscolar/Controllers/NotasController.cs`
- [X] T047 [US2] Create Notas Razor views Index/Create/Edit/Delete with student and subject selectors in `SistemaNotasEscolar/Views/Notas`
- [X] T048 [US2] Run US1 and US2 tests with `dotnet test` from `SistemaNotasEscolar.sln`

**Checkpoint**: User Stories 1 and 2 work independently.

---

## Phase 5: User Story 3 - Calcular medias e consultas analiticas (Priority: P3)

**Goal**: Users can view combined grade records, average grades by subject, and filtered LINQ-style reports.

**Independent Test**: Seed a small set of students, subjects, and grades; verify combined rows, subject averages, grades >= 6, and subject averages >= 7.

### Tests for User Story 3

- [X] T049 [P] [US3] Write failing tests for combined student-subject-grade query in `Tests/Services/ConsultaServiceTests.cs`
- [X] T050 [P] [US3] Write failing tests for average by subject grouping in `Tests/Services/ConsultaServiceTests.cs`
- [X] T051 [P] [US3] Write failing tests for grade >= 6 and grouped average >= 7 filters in `Tests/Services/ConsultaServiceTests.cs`

### Implementation for User Story 3

- [X] T052 [P] [US3] Create NotaDetalhada view model in `SistemaNotasEscolar/Models/NotaDetalhada.cs`
- [X] T053 [P] [US3] Create MediaPorDisciplina view model in `SistemaNotasEscolar/Models/MediaPorDisciplina.cs`
- [X] T054 [P] [US3] Create consultation service interface in `SistemaNotasEscolar/Services/IConsultaService.cs`
- [X] T055 [US3] Implement combined Aluno + Disciplina + Nota query in `SistemaNotasEscolar/Services/ConsultaService.cs`
- [X] T056 [US3] Implement GroupBy average by subject query in `SistemaNotasEscolar/Services/ConsultaService.cs`
- [X] T057 [US3] Implement WHERE-style grade filter and HAVING-style average filter in `SistemaNotasEscolar/Services/ConsultaService.cs`
- [X] T058 [US3] Register consultation service in `SistemaNotasEscolar/Program.cs`
- [X] T059 [US3] Create ConsultasController actions in `SistemaNotasEscolar/Controllers/ConsultasController.cs`
- [X] T060 [P] [US3] Create combined grade consultation Razor view in `SistemaNotasEscolar/Views/Consultas/NotasPorAlunoDisciplina.cshtml`
- [X] T061 [P] [US3] Create average by subject Razor view in `SistemaNotasEscolar/Views/Consultas/MediasPorDisciplina.cshtml`
- [X] T062 [P] [US3] Create filtered consultation Razor view in `SistemaNotasEscolar/Views/Consultas/Filtros.cshtml`
- [X] T063 [US3] Run all service and query tests with `dotnet test` from `SistemaNotasEscolar.sln`

**Checkpoint**: All user stories are independently functional.

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Documentation, validation, and final cleanup across all stories.

- [ ] T064 [P] Update project README with setup, migration, test, and run instructions in `README.md`
- [ ] T065 [P] Create prompts log document in `docs/PROMPTS.md`
- [ ] T066 [P] Create tokens summary document or cross-reference current usage log in `docs/TOKENS.md`
- [ ] T067 [P] Verify AI token records remain current in `docs/AI_USAGE.md`
- [ ] T068 Apply Bootstrap styling consistency across Razor layout and views in `SistemaNotasEscolar/Views`
- [ ] T069 Run database migration command from `specs/002-sistema-notas-escolar/quickstart.md`
- [ ] T070 Run full test suite with `dotnet test` from `SistemaNotasEscolar.sln`
- [ ] T071 Run application smoke test with `dotnet run --project SistemaNotasEscolar`
- [ ] T072 Review all tasks and acceptance scenarios against `specs/002-sistema-notas-escolar/spec.md`

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies.
- **Foundational (Phase 2)**: Depends on Setup completion and blocks all user stories.
- **User Story 1 (Phase 3)**: Depends on Foundational completion and is the MVP.
- **User Story 2 (Phase 4)**: Depends on Foundational completion and uses student/subject data from US1 for realistic verification.
- **User Story 3 (Phase 5)**: Depends on grade data from US2 for query verification.
- **Polish (Phase 6)**: Depends on the desired user stories being complete.

### User Story Dependencies

- **US1**: No dependency on other user stories after foundational setup.
- **US2**: Requires Aluno and Disciplina concepts from US1 to register grades.
- **US3**: Requires Nota records from US2 to calculate averages and filters.

### Within Each User Story

- Tests must be written first and fail before implementation.
- Models and repository interfaces precede repository implementations.
- Repositories precede services.
- Services precede controllers and views.
- Migrations follow model and DbContext changes.

## Parallel Opportunities

- T006, T007, and T008 can run after project creation because they touch different folders/files.
- T011, T012, and T013 can run in parallel during foundational setup.
- US1 tests T015, T016, and T017 can run in parallel.
- US1 models/interfaces T018, T019, T021, T022, T025, and T026 can run in parallel.
- US2 tests T036 and T037 can run in parallel.
- US3 tests T049, T050, and T051 can run in parallel.
- US3 view model and interface tasks T052, T053, and T054 can run in parallel.
- Polish documentation tasks T064, T065, T066, and T067 can run in parallel.

## Parallel Example: User Story 1

```text
Task: "T015 [P] [US1] Write failing tests for required and unique student enrollment validation in Tests/Services/AlunoServiceTests.cs"
Task: "T016 [P] [US1] Write failing tests for required subject name validation in Tests/Services/DisciplinaServiceTests.cs"
Task: "T017 [P] [US1] Write failing repository persistence tests for students and subjects in Tests/Repositories/RepositoryPersistenceTests.cs"
Task: "T018 [P] [US1] Create Aluno model with Id, Nome, Matricula, and Notas navigation in SistemaNotasEscolar/Models/Aluno.cs"
Task: "T019 [P] [US1] Create Disciplina model with Id, Nome, and Notas navigation in SistemaNotasEscolar/Models/Disciplina.cs"
```

## Parallel Example: User Story 3

```text
Task: "T049 [P] [US3] Write failing tests for combined student-subject-grade query in Tests/Services/ConsultaServiceTests.cs"
Task: "T050 [P] [US3] Write failing tests for average by subject grouping in Tests/Services/ConsultaServiceTests.cs"
Task: "T051 [P] [US3] Write failing tests for grade >= 6 and grouped average >= 7 filters in Tests/Services/ConsultaServiceTests.cs"
Task: "T060 [P] [US3] Create combined grade consultation Razor view in SistemaNotasEscolar/Views/Consultas/NotasPorAlunoDisciplina.cshtml"
Task: "T061 [P] [US3] Create average by subject Razor view in SistemaNotasEscolar/Views/Consultas/MediasPorDisciplina.cshtml"
Task: "T062 [P] [US3] Create filtered consultation Razor view in SistemaNotasEscolar/Views/Consultas/Filtros.cshtml"
```

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Complete Phase 1 setup.
2. Complete Phase 2 foundational infrastructure.
3. Complete Phase 3 for student and subject CRUD.
4. Stop and validate US1 through tests and manual navigation.

### Incremental Delivery

1. Deliver US1 for academic registration.
2. Add US2 for grade registration and consultation.
3. Add US3 for averages and filtered reports.
4. Finish polish documentation, migrations validation, and smoke testing.

### TDD Strategy

1. Write the tests listed at the start of each user story phase.
2. Run tests and confirm the new tests fail.
3. Implement the smallest code change needed for the story.
4. Run tests again and refactor while keeping them green.

## Notes

- `ConsultasController` covers the reporting requirements from the task request.
- `[P]` tasks are safe to run in parallel only when their listed dependencies are complete.
- Keep commits small after each task or logical group.
