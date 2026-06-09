# Feature Specification: Sistema de Notas Escolar

**Feature Branch**: `002-sistema-notas-escolar`

**Created**: 04/06/2026

**Status**: Draft

**Input**: User description: "Sistema de Notas Escolar para cadastrar alunos, disciplinas e notas; consultar notas; calcular medias por disciplina; demonstrar consultas com associacao, agrupamento e filtros; centralizar informacoes antes controladas manualmente ou por planilhas."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Gerenciar cadastros academicos (Priority: P1)

Como usuario responsavel pelo controle escolar, quero cadastrar, editar, excluir e listar alunos e disciplinas para manter a base academica organizada antes do lancamento de notas.

**Why this priority**: Alunos e disciplinas sao os dados fundamentais para qualquer registro de nota; sem eles, o sistema nao entrega valor operacional.

**Independent Test**: Pode ser testado criando, alterando, listando e removendo alunos e disciplinas sem depender do lancamento de notas.

**Acceptance Scenarios**:

1. **Given** que nao existe um aluno cadastrado, **When** o usuario informa nome e matricula validos, **Then** o aluno fica disponivel na listagem de alunos.
2. **Given** que existe uma disciplina cadastrada, **When** o usuario altera seu nome, **Then** a listagem apresenta a disciplina com os dados atualizados.
3. **Given** que existe um aluno sem notas vinculadas, **When** o usuario solicita sua exclusao, **Then** o aluno deixa de aparecer na listagem.

---

### User Story 2 - Registrar e consultar notas (Priority: P2)

Como usuario responsavel pelo controle escolar, quero registrar, editar, excluir e listar notas vinculadas a alunos e disciplinas para consultar o desempenho individual de cada estudante.

**Why this priority**: O registro de notas e o nucleo do sistema e substitui o controle manual ou por planilhas.

**Independent Test**: Pode ser testado criando ao menos um aluno e uma disciplina, registrando uma nota vinculada a ambos e verificando a listagem de notas.

**Acceptance Scenarios**:

1. **Given** que existem um aluno e uma disciplina cadastrados, **When** o usuario registra uma nota valida para essa combinacao, **Then** a nota aparece associada ao aluno e a disciplina na consulta.
2. **Given** que existe uma nota registrada, **When** o usuario edita seu valor, **Then** as consultas exibem o novo valor.
3. **Given** que existe uma nota registrada, **When** o usuario exclui essa nota, **Then** ela nao aparece mais nas consultas.

---

### User Story 3 - Calcular medias e consultas analiticas (Priority: P3)

Como usuario responsavel pelo acompanhamento escolar, quero consultar medias por disciplina e visualizar resultados agrupados e filtrados para identificar desempenho por aluno e por disciplina.

**Why this priority**: As consultas analiticas demonstram o ganho sobre planilhas, facilitando interpretacao e manutencao dos dados.

**Independent Test**: Pode ser testado com um conjunto pequeno de alunos, disciplinas e notas, verificando se as medias, agrupamentos e filtros retornam resultados esperados.

**Acceptance Scenarios**:

1. **Given** que uma disciplina possui varias notas registradas, **When** o usuario consulta a media da disciplina, **Then** o sistema apresenta a media calculada com base nas notas existentes.
2. **Given** que existem notas para diferentes alunos e disciplinas, **When** o usuario solicita a consulta combinada, **Then** o resultado mostra aluno, disciplina e valor da nota em uma unica visao.
3. **Given** que existem medias calculadas por disciplina, **When** o usuario aplica filtros de desempenho, **Then** o sistema exibe apenas os grupos que atendem aos criterios informados.

---

### Edge Cases

- Quando o usuario tenta cadastrar aluno sem nome ou sem matricula, o sistema deve impedir o cadastro e indicar os campos obrigatorios.
- Quando o usuario tenta cadastrar disciplina sem nome, o sistema deve impedir o cadastro e indicar o campo obrigatorio.
- Quando o usuario tenta registrar nota sem aluno ou sem disciplina, o sistema deve impedir o registro.
- Quando o usuario informa valor de nota fora da escala aceita, o sistema deve rejeitar o valor e informar o intervalo permitido.
- Quando uma disciplina nao possui notas registradas, a consulta de media deve indicar ausencia de dados em vez de calcular um resultado enganoso.
- Quando o usuario tenta excluir aluno ou disciplina com notas vinculadas, o sistema deve impedir a exclusao direta ou exigir a remocao previa das notas vinculadas.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST allow users to create, edit, delete, and list students.
- **FR-002**: System MUST require each student to have a name and a unique enrollment identifier.
- **FR-003**: System MUST allow users to create, edit, delete, and list subjects.
- **FR-004**: System MUST require each subject to have a name.
- **FR-005**: System MUST allow users to create, edit, delete, and list grades.
- **FR-006**: System MUST associate each grade with exactly one student and exactly one subject.
- **FR-007**: System MUST require each grade value to be numeric and within the accepted grade scale.
- **FR-008**: Users MUST be able to consult grades with student and subject information in the same result.
- **FR-009**: System MUST calculate the average grade for each subject using all grades registered for that subject.
- **FR-010**: System MUST provide grouped grade summaries by subject.
- **FR-011**: System MUST provide filtered grade consultations equivalent to filtering individual records and grouped summaries.
- **FR-012**: System MUST prevent deletion of students or subjects when existing grades would become orphaned, unless those linked grades are removed first.
- **FR-013**: System MUST present clear feedback when required data is missing, invalid, duplicated, or blocked by existing relationships.
- **FR-014**: System MUST persist student, subject, and grade data so information remains available after the user leaves and returns.

### Key Entities *(include if feature involves data)*

- **Aluno**: Represents a student. Key attributes are identifier, name, and enrollment number. A student can have multiple grades.
- **Disciplina**: Represents a school subject. Key attributes are identifier and name. A subject can have multiple grades.
- **Nota**: Represents a grade value assigned to one student in one subject. Key attributes are identifier, numeric value, student reference, and subject reference.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Users can complete the full registration flow for one student, one subject, and one grade in under 3 minutes.
- **SC-002**: Users can find a registered student's grades in under 30 seconds using the available list or consultation views.
- **SC-003**: For a prepared set of grade records, subject averages match manual calculation in 100% of validation cases.
- **SC-004**: The system prevents 100% of grade records that are missing a student, missing a subject, or contain an invalid grade value.
- **SC-005**: Users can view at least one combined student-subject-grade consultation, one grouped subject summary, and one filtered grouped result during acceptance testing.

## Assumptions

- The accepted grade scale is 0 to 10 inclusive, using decimal values when needed.
- Each student enrollment number is unique.
- A student or subject with linked grades cannot be deleted until the related grades are removed.
- Authentication and different permission levels are outside the scope of this feature.
- The technical stack and implementation practices provided by the user are planning constraints for the next phase, not functional behavior of the specification.
