# DevShop API

API de e-commerce desenvolvida com ASP.NET Core 9.0 e Entity Framework Core com SQLite.

## 📊 Diagramas de Arquitetura

A documentação de arquitetura está disponível em [`Documentation/Architecture/`](DevShop.Api/Documentation/Architecture/):

### Diagramas Disponíveis

#### 1. **Diagrama de Classes UML** 
- **Arquivo**: [`ClassDiagram.puml`](DevShop.Api/Documentation/Architecture/ClassDiagram.puml) / [`ClassDiagram.md`](DevShop.Api/Documentation/Architecture/ClassDiagram.md)
- **Descrição**: Mostra as entidades, Views, Repositórios e suas relações
- **Nível**: Detalhado - Classes e métodos

#### 2. **Diagrama C4 Container (Nível 1)**
- **Arquivo**: [`C4-Container.puml`](DevShop.Api/Documentation/Architecture/C4-Container.puml) / [`C4-Container.md`](DevShop.Api/Documentation/Architecture/C4-Container.md)
- **Descrição**: Visão geral do sistema e suas dependências
- **Nível**: Alto - Containers e tecnologias

#### 3. **Diagrama C4 Component (Nível 2)**
- **Arquivo**: [`C4-Component.puml`](DevShop.Api/Documentation/Architecture/C4-Component.puml) / [`C4-Component.md`](DevShop.Api/Documentation/Architecture/C4-Component.md)
- **Descrição**: Mostra os componentes principais da aplicação
- **Nível**: Médio - Componentes e suas responsabilidades

#### 4. **Diagrama de Sequência**
- **Arquivo**: [`SequenceDiagram.puml`](DevShop.Api/Documentation/Architecture/SequenceDiagram.puml) / [`SequenceDiagram.md`](DevShop.Api/Documentation/Architecture/SequenceDiagram.md)
- **Descrição**: Fluxo de criação de um pedido
- **Nível**: Detalhado - Interações entre componentes

### Como Visualizar

#### PlantUML
1. Instale uma extensão PlantUML no seu editor (VS Code, IntelliJ, etc.)
2. Abra qualquer arquivo `.puml`
3. Use `Alt+Shift+D` (VS Code) para visualizar

#### PlantUML Online
1. Acesse: https://www.plantuml.com/plantuml/uml/
2. Cole o conteúdo do arquivo `.puml`
3. O diagrama será gerado automaticamente

#### Mermaid (GitHub)
Os diagramas também estão disponíveis em formato Mermaid para visualização direta no GitHub nos arquivos `.md`.

## Configuração do Banco de Dados

O projeto está configurado para usar SQLite como banco de dados. O arquivo do banco será criado automaticamente na primeira execução.

### Estrutura das Entidades

- **Customer**: Clientes com nome, telefone e endereço
- **Product**: Produtos com nome, descrição, preço, quantidade e código único
- **Order**: Pedidos com cliente, data, valor total e status
- **OrderItem**: Itens dos pedidos com produto, quantidade e preço unitário

### Configurações do Entity Framework

O `DevShopDbContext` está configurado com:

- Relacionamentos entre entidades
- Configurações de chaves primárias e estrangeiras
- Índices para otimização de consultas
- Atualização automática de timestamps (CreateOnUtc e UpdateOnUtc)

### Padrão Repository

O projeto implementa o padrão Repository para abstrair o acesso a dados:

#### Interfaces de Repositório:
- **IRepository<T>**: Interface genérica com operações básicas (CRUD)
- **IProductRepository**: Repositório específico para produtos com métodos customizados
- **ICustomerRepository**: Repositório específico para clientes com métodos customizados
- **IOrderRepository**: Repositório específico para pedidos com métodos customizados

#### Implementações:
- **Repository<T>**: Implementação base genérica
- **ProductRepository**: Implementação específica para produtos
- **CustomerRepository**: Implementação específica para clientes
- **OrderRepository**: Implementação específica para pedidos

#### Benefícios:
- Abstração do acesso a dados
- Facilita testes unitários
- Centraliza lógica de acesso a dados
- Permite troca fácil de implementação de banco

### Views e AutoMapper

O projeto utiliza Views (Data Transfer Objects) para separar a camada de apresentação das entidades de domínio:

#### Views Disponíveis:
- **ProductView**: Para retorno de dados de produtos
- **CreateProductView**: Para criação de produtos
- **UpdateProductView**: Para atualização de produtos
- **CustomerView**: Para retorno de dados de clientes
- **CreateCustomerView**: Para criação de clientes
- **UpdateCustomerView**: Para atualização de clientes
- **OrderView**: Para retorno de dados de pedidos
- **CreateOrderView**: Para criação de pedidos
- **UpdateOrderView**: Para atualização de pedidos
- **OrderItemView**: Para retorno de dados de itens de pedido
- **CreateOrderItemView**: Para criação de itens de pedido
- **UpdateOrderItemView**: Para atualização de itens de pedido

#### AutoMapper:
- Configurado automaticamente no `Program.cs`
- Mapeamentos definidos em `Mappings/AutoMapperProfile.cs`
- Conversão automática entre entidades e Views

### Migrações

O projeto inclui uma migração inicial que cria todas as tabelas necessárias. A migração é aplicada automaticamente na inicialização da aplicação.

### Dados Iniciais

O sistema inclui dados de exemplo para produtos que são inseridos automaticamente na primeira execução.

## Como Executar

1. Certifique-se de ter o .NET 9.0 SDK instalado
2. Execute o projeto:
   ```bash
   dotnet run
   ```
3. O banco de dados SQLite será criado automaticamente
4. Acesse a API em: `https://localhost:7000` (ou a porta configurada)
5. Acesse o Swagger UI em: `https://localhost:7000/swagger`

## Endpoints Disponíveis

### Produtos
- `GET /api/products` - Lista todos os produtos
- `GET /api/products/{id}` - Obtém um produto específico
- `GET /api/products/code/{code}` - Obtém produto por código
- `GET /api/products/price-range?minPrice=X&maxPrice=Y` - Produtos por faixa de preço
- `GET /api/products/low-stock?threshold=X` - Produtos com estoque baixo
- `POST /api/products` - Cria um novo produto
- `PUT /api/products/{id}` - Atualiza um produto
- `DELETE /api/products/{id}` - Remove um produto

### Clientes
- `GET /api/customers` - Lista todos os clientes
- `GET /api/customers/{id}` - Obtém um cliente específico com pedidos
- `GET /api/customers/phone/{phoneNumber}` - Clientes por telefone
- `GET /api/customers/address/{address}` - Clientes por endereço
- `POST /api/customers` - Cria um novo cliente
- `PUT /api/customers/{id}` - Atualiza um cliente
- `DELETE /api/customers/{id}` - Remove um cliente

### Pedidos
- `GET /api/orders` - Lista todos os pedidos com detalhes
- `GET /api/orders/{id}` - Obtém um pedido específico com detalhes
- `GET /api/orders/customer/{customerId}` - Pedidos por cliente
- `GET /api/orders/status/{status}` - Pedidos por status
- `GET /api/orders/date-range?startDate=X&endDate=Y` - Pedidos por período
- `POST /api/orders` - Cria um novo pedido
- `PUT /api/orders/{id}` - Atualiza um pedido
- `DELETE /api/orders/{id}` - Remove um pedido

## Estrutura do Projeto

```
DevShop.Api/
├── Controllers/          # Controllers da API
│   ├── ProductsController.cs
│   ├── CustomersController.cs
│   └── OrdersController.cs
├── Data/                 # Contexto do Entity Framework
│   ├── DevShopDbContext.cs
│   └── DatabaseInitializer.cs
├── Repositories/         # Padrão Repository
│   ├── IRepository.cs
│   ├── Repository.cs
│   ├── IProductRepository.cs
│   ├── ProductRepository.cs
│   ├── ICustomerRepository.cs
│   ├── CustomerRepository.cs
│   ├── IOrderRepository.cs
│   └── OrderRepository.cs
├── Views/                # Data Transfer Objects (Views)
│   ├── ProductView.cs
│   ├── CustomerView.cs
│   ├── OrderView.cs
│   └── OrderItemView.cs
├── Mappings/             # Configurações do AutoMapper
│   └── AutoMapperProfile.cs
├── Models/               # Entidades do domínio
│   ├── Customer.cs
│   ├── Entity.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── OrderStatus.cs
│   └── Product.cs
└── Program.cs           # Configuração da aplicação
```

## Tecnologias Utilizadas

- ASP.NET Core 9.0
- Entity Framework Core 9.0
- SQLite
- AutoMapper
- Padrão Repository
- OpenAPI/Swagger

## Exemplo de Uso

### Criar um Produto
```json
POST /api/products
{
  "name": "Novo Produto",
  "description": "Descrição do produto",
  "price": 99.99,
  "quantity": 10,
  "code": "PROD001"
}
```

### Criar um Cliente
```json
POST /api/customers
{
  "fullName": "João Silva",
  "phoneNumber": "(11) 99999-9999",
  "address": "Rua das Flores, 123"
}
```

### Criar um Pedido
```json
POST /api/orders
{
  "customerId": "customer-id-here",
  "totalAmount": 199.98,
  "items": [
    {
      "productId": "product-id-here",
      "quantity": 2,
      "unitPrice": 99.99
    }
  ]
}
```

### Consultas Avançadas

#### Produtos com estoque baixo
```
GET /api/products/low-stock?threshold=5
```

#### Pedidos por status
```
GET /api/orders/status/Pending
```

#### Pedidos por período
```
GET /api/orders/date-range?startDate=2024-01-01&endDate=2024-12-31
```
