# Web UI Contracts: Sistema de Notas Escolar

## Navigation

The application exposes navigation entries for Alunos, Disciplinas, Notas, and Consultas.

## Alunos

### List Students

- **Route**: `GET /Alunos`
- **Displays**: nome, matricula, edit action, delete action.

### Create Student

- **Routes**: `GET /Alunos/Create`, `POST /Alunos/Create`
- **Inputs**: nome, matricula.
- **Validation**: required nome, required unique matricula.
- **Success**: redirects to student list with the new student visible.

### Edit Student

- **Routes**: `GET /Alunos/Edit/{id}`, `POST /Alunos/Edit/{id}`
- **Inputs**: nome, matricula.
- **Success**: redirects to student list with updated values.

### Delete Student

- **Routes**: `GET /Alunos/Delete/{id}`, `POST /Alunos/Delete/{id}`
- **Rule**: deletion is blocked when linked grades exist.

## Disciplinas

### List Subjects

- **Route**: `GET /Disciplinas`
- **Displays**: nome, edit action, delete action.

### Create Subject

- **Routes**: `GET /Disciplinas/Create`, `POST /Disciplinas/Create`
- **Inputs**: nome.
- **Validation**: required nome.

### Edit Subject

- **Routes**: `GET /Disciplinas/Edit/{id}`, `POST /Disciplinas/Edit/{id}`
- **Inputs**: nome.

### Delete Subject

- **Routes**: `GET /Disciplinas/Delete/{id}`, `POST /Disciplinas/Delete/{id}`
- **Rule**: deletion is blocked when linked grades exist.

## Notas

### List Grades

- **Route**: `GET /Notas`
- **Displays**: aluno, matricula, disciplina, valor, edit action, delete action.

### Create Grade

- **Routes**: `GET /Notas/Create`, `POST /Notas/Create`
- **Inputs**: aluno, disciplina, valor.
- **Validation**: aluno required, disciplina required, valor between 0 and 10.

### Edit Grade

- **Routes**: `GET /Notas/Edit/{id}`, `POST /Notas/Edit/{id}`
- **Inputs**: aluno, disciplina, valor.

### Delete Grade

- **Routes**: `GET /Notas/Delete/{id}`, `POST /Notas/Delete/{id}`
- **Success**: grade no longer appears in grade or consultation lists.

## Consultas

### Combined Student-Subject-Grade Query

- **Route**: `GET /Consultas/NotasPorAlunoDisciplina`
- **Displays**: nome do aluno, matricula, disciplina, nota.

### Average by Subject

- **Route**: `GET /Consultas/MediasPorDisciplina`
- **Displays**: disciplina, quantidade de notas, media.
- **Rule**: subjects without grades show no misleading average.

### Grade and Average Filters

- **Route**: `GET /Consultas/Filtros`
- **Displays**: notas with valor >= 6 and grouped subjects with media >= 7.
