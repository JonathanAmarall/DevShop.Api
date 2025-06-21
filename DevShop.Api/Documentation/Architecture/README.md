# Documentação de Arquitetura - DevShop API

Este diretório contém os diagramas de arquitetura da aplicação DevShop API, incluindo diagramas UML e C4 Model.

## Diagramas Disponíveis

### 1. Diagrama de Classes UML
- **Arquivo**: `ClassDiagram.puml`
- **Descrição**: Mostra as entidades, Views, Repositórios e suas relações
- **Nível**: Detalhado - Classes e métodos

### 2. Diagrama de Componentes C4 Model - Nível 2
- **Arquivo**: `C4-Component.puml`
- **Descrição**: Mostra os componentes principais da aplicação
- **Nível**: Médio - Componentes e suas responsabilidades

### 3. Diagrama de Containers C4 Model - Nível 1
- **Arquivo**: `C4-Container.puml`
- **Descrição**: Visão geral do sistema e suas dependências
- **Nível**: Alto - Containers e tecnologias

### 4. Diagrama de Sequência
- **Arquivo**: `SequenceDiagram.puml`
- **Descrição**: Fluxo de criação de um pedido
- **Nível**: Detalhado - Interações entre componentes

## Como Visualizar

### Usando PlantUML
1. Instale uma extensão PlantUML no seu editor (VS Code, IntelliJ, etc.)
2. Abra qualquer arquivo `.puml`
3. Use `Alt+Shift+D` (VS Code) para visualizar

### Usando PlantUML Online
1. Acesse: https://www.plantuml.com/plantuml/uml/
2. Cole o conteúdo do arquivo `.puml`
3. O diagrama será gerado automaticamente

### Usando Mermaid (GitHub)
Os diagramas também estão disponíveis em formato Mermaid para visualização no GitHub.

## Tecnologias Utilizadas

- **PlantUML**: Para diagramas UML e C4
- **Mermaid**: Para compatibilidade com GitHub
- **C4 Model**: Para documentação de arquitetura em diferentes níveis

## Estrutura dos Diagramas

### C4 Model
- **Nível 1**: Contexto do sistema
- **Nível 2**: Containers da aplicação
- **Nível 3**: Componentes dentro dos containers
- **Nível 4**: Código (classes, métodos)

### UML
- **Diagrama de Classes**: Estrutura estática
- **Diagrama de Sequência**: Comportamento dinâmico
- **Diagrama de Componentes**: Arquitetura de componentes 