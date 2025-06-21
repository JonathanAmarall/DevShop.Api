# Diagrama C4 Component - DevShop API

## Visão dos Componentes (C4 Model Level 2)

```mermaid
graph TB
    subgraph "DevShop API"
        subgraph "Controllers"
            products_controller[Products Controller<br/>ASP.NET Core Controller<br/>Gerencia operações de produtos]
            customers_controller[Customers Controller<br/>ASP.NET Core Controller<br/>Gerencia operações de clientes]
            orders_controller[Orders Controller<br/>ASP.NET Core Controller<br/>Gerencia operações de pedidos]
        end
        
        subgraph "Repositories"
            product_repo[Product Repository<br/>Repository Pattern<br/>Acesso a dados de produtos]
            customer_repo[Customer Repository<br/>Repository Pattern<br/>Acesso a dados de clientes]
            order_repo[Order Repository<br/>Repository Pattern<br/>Acesso a dados de pedidos]
        end
        
        subgraph "Infrastructure"
            db_context[DevShop DbContext<br/>Entity Framework Core<br/>Contexto do banco de dados]
            auto_mapper[AutoMapper<br/>Object Mapping<br/>Mapeamento entre entidades e Views]
        end
        
        subgraph "Views (DTOs)"
            product_view[Product Views<br/>DTOs<br/>Views para produtos]
            customer_view[Customer Views<br/>DTOs<br/>Views para clientes]
            order_view[Order Views<br/>DTOs<br/>Views para pedidos]
        end
    end
    
    db[(SQLite Database<br/>SQLite<br/>Armazena dados de produtos, clientes e pedidos)]
    client[Cliente<br/>Usuário da aplicação que consome a API]
    swagger[Swagger UI<br/>Documentação e teste da API]
    
    %% Relacionamentos externos
    client -->|HTTP GET/POST/PUT/DELETE<br/>REST API| products_controller
    client -->|HTTP GET/POST/PUT/DELETE<br/>REST API| customers_controller
    client -->|HTTP GET/POST/PUT/DELETE<br/>REST API| orders_controller
    client -->|Acessa documentação<br/>HTTP| swagger
    swagger -->|Testa endpoints<br/>HTTP/REST| products_controller
    swagger -->|Testa endpoints<br/>HTTP/REST| customers_controller
    swagger -->|Testa endpoints<br/>HTTP/REST| orders_controller
    
    %% Relacionamentos internos - Controllers
    products_controller -->|Usa<br/>Dependency Injection| product_repo
    products_controller -->|Usa<br/>Dependency Injection| auto_mapper
    products_controller -->|Retorna| product_view
    
    customers_controller -->|Usa<br/>Dependency Injection| customer_repo
    customers_controller -->|Usa<br/>Dependency Injection| auto_mapper
    customers_controller -->|Retorna| customer_view
    
    orders_controller -->|Usa<br/>Dependency Injection| order_repo
    orders_controller -->|Usa<br/>Dependency Injection| customer_repo
    orders_controller -->|Usa<br/>Dependency Injection| product_repo
    orders_controller -->|Usa<br/>Dependency Injection| auto_mapper
    orders_controller -->|Retorna| order_view
    
    %% Relacionamentos - Repositórios
    product_repo -->|Usa<br/>Entity Framework| db_context
    customer_repo -->|Usa<br/>Entity Framework| db_context
    order_repo -->|Usa<br/>Entity Framework| db_context
    
    %% Relacionamentos - Data Context
    db_context -->|Lê e escreve<br/>SQL| db
    
    %% Relacionamentos - AutoMapper
    auto_mapper -->|Mapeia<br/>Entidades ↔ Views| product_view
    auto_mapper -->|Mapeia<br/>Entidades ↔ Views| customer_view
    auto_mapper -->|Mapeia<br/>Entidades ↔ Views| order_view
    
    classDef person fill:#08427B,stroke:#073B6F,stroke-width:2px,color:#fff
    classDef component fill:#1168BD,stroke:#0E5DAD,stroke-width:2px,color:#fff
    classDef database fill:#438DD5,stroke:#3C7BC0,stroke-width:2px,color:#fff
    classDef external fill:#999999,stroke:#8A8A8A,stroke-width:2px,color:#fff
    classDef container fill:#85BBF0,stroke:#7AA9E0,stroke-width:2px,color:#000
    
    class client person
    class products_controller,customers_controller,orders_controller,product_repo,customer_repo,order_repo,db_context,auto_mapper,product_view,customer_view,order_view component
    class db database
    class swagger external
```

## Descrição dos Componentes

### Controllers
Responsáveis por receber requisições HTTP e coordenar as operações:

#### Products Controller
- **Responsabilidade**: Gerencia operações de produtos
- **Endpoints**:
  - `GET /api/products` - Lista todos os produtos
  - `GET /api/products/{id}` - Obtém produto por ID
  - `GET /api/products/code/{code}` - Obtém produto por código
  - `GET /api/products/name/{name}` - Obtém produto por nome
  - `GET /api/products/price-range` - Produtos por faixa de preço
  - `GET /api/products/low-stock` - Produtos com estoque baixo
  - `POST /api/products` - Cria novo produto
  - `PUT /api/products/{id}` - Atualiza produto
  - `DELETE /api/products/{id}` - Remove produto

#### Customers Controller
- **Responsabilidade**: Gerencia operações de clientes
- **Endpoints**:
  - `GET /api/customers` - Lista todos os clientes
  - `GET /api/customers/{id}` - Obtém cliente por ID
  - `POST /api/customers` - Cria novo cliente
  - `PUT /api/customers/{id}` - Atualiza cliente
  - `DELETE /api/customers/{id}` - Remove cliente

#### Orders Controller
- **Responsabilidade**: Gerencia operações de pedidos
- **Endpoints**:
  - `GET /api/orders` - Lista todos os pedidos
  - `GET /api/orders/{id}` - Obtém pedido por ID
  - `GET /api/orders/customer/{customerId}` - Pedidos por cliente
  - `GET /api/orders/status/{status}` - Pedidos por status
  - `POST /api/orders` - Cria novo pedido
  - `PUT /api/orders/{id}` - Atualiza pedido
  - `DELETE /api/orders/{id}` - Remove pedido

### Repositories
Implementam o padrão Repository para abstrair o acesso a dados:

#### Product Repository
- **Responsabilidade**: Acesso a dados de produtos
- **Métodos específicos**:
  - `GetByCodeAsync()` - Busca por código único
  - `GetByNameAsync()` - Busca por nome
  - `GetByPriceRangeAsync()` - Produtos por faixa de preço
  - `GetLowStockAsync()` - Produtos com estoque baixo

#### Customer Repository
- **Responsabilidade**: Acesso a dados de clientes
- **Métodos específicos**:
  - `GetWithOrdersAsync()` - Cliente com pedidos incluídos
  - `GetByPhoneNumberAsync()` - Clientes por telefone
  - `GetByAddressAsync()` - Clientes por endereço

#### Order Repository
- **Responsabilidade**: Acesso a dados de pedidos
- **Métodos específicos**:
  - `GetWithDetailsAsync()` - Pedido com detalhes completos
  - `GetAllWithDetailsAsync()` - Todos os pedidos com detalhes
  - `GetByCustomerIdAsync()` - Pedidos por cliente
  - `GetByStatusAsync()` - Pedidos por status
  - `GetByDateRangeAsync()` - Pedidos por período

### Infrastructure

#### DevShop DbContext
- **Responsabilidade**: Contexto do Entity Framework
- **Funcionalidades**:
  - Configuração de entidades
  - Relacionamentos
  - Migrações
  - Atualização automática de timestamps

#### AutoMapper
- **Responsabilidade**: Mapeamento entre entidades e Views
- **Funcionalidades**:
  - Conversão automática
  - Configurações centralizadas
  - Mapeamentos customizados

### Views (DTOs)
Objetos de transferência de dados para separar a camada de apresentação:

#### Product Views
- `ProductView` - Para retorno de dados
- `CreateProductView` - Para criação
- `UpdateProductView` - Para atualização

#### Customer Views
- `CustomerView` - Para retorno de dados
- `CreateCustomerView` - Para criação
- `UpdateCustomerView` - Para atualização

#### Order Views
- `OrderView` - Para retorno de dados
- `CreateOrderView` - Para criação
- `UpdateOrderView` - Para atualização
- `OrderItemView` - Para itens de pedido

## Fluxo de Dados

### Requisição Típica
1. **Cliente** envia requisição HTTP para **Controller**
2. **Controller** valida dados e chama **Repository**
3. **Repository** usa **DbContext** para acessar **Database**
4. **Database** retorna dados para **Repository**
5. **Repository** retorna entidades para **Controller**
6. **Controller** usa **AutoMapper** para converter entidades em **Views**
7. **Controller** retorna **Views** para **Cliente**

### Benefícios da Arquitetura
- **Separação de Responsabilidades**: Cada componente tem uma responsabilidade específica
- **Testabilidade**: Fácil criação de mocks para testes unitários
- **Manutenibilidade**: Código organizado e bem estruturado
- **Escalabilidade**: Fácil adição de novos recursos
- **Flexibilidade**: Troca fácil de implementações via Dependency Injection 