# Data Model: Sistema de Notas Escolar

## Aluno

Represents a student managed by the school-grade system.

### Fields

- `Id`: Unique identifier.
- `Nome`: Required student name.
- `Matricula`: Required unique enrollment number.

### Relationships

- Has many `Nota` records.

### Validation Rules

- `Nome` is required.
- `Matricula` is required.
- `Matricula` must be unique.
- An `Aluno` with linked `Nota` records cannot be deleted until those grades are removed.

## Disciplina

Represents a school subject.

### Fields

- `Id`: Unique identifier.
- `Nome`: Required subject name.

### Relationships

- Has many `Nota` records.

### Validation Rules

- `Nome` is required.
- A `Disciplina` with linked `Nota` records cannot be deleted until those grades are removed.

## Nota

Represents one grade value assigned to one student in one subject.

### Fields

- `Id`: Unique identifier.
- `Valor`: Numeric grade value.
- `AlunoId`: Required reference to `Aluno`.
- `DisciplinaId`: Required reference to `Disciplina`.

### Relationships

- Belongs to one `Aluno`.
- Belongs to one `Disciplina`.

### Validation Rules

- `Valor` is required.
- `Valor` must be between 0 and 10 inclusive.
- `AlunoId` must reference an existing student.
- `DisciplinaId` must reference an existing subject.

## Consultation Models

### NotaDetalhada

- `AlunoNome`
- `Matricula`
- `DisciplinaNome`
- `Valor`

Used for the combined student-subject-grade consultation.

### MediaPorDisciplina

- `DisciplinaNome`
- `QuantidadeNotas`
- `Media`

Used for grouped subject averages and filtered grouped results.
