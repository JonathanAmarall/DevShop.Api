# Diagrama de Classes - DevShop API

## Visão Geral das Classes e Relacionamentos

```mermaid
classDiagram
    %% Entidades de Domínio
    class Entity {
        <<abstract>>
        +string Id
        +DateTime CreateOnUtc
        +DateTime? UpdateOnUtc
        +Entity()
    }

    class Customer {
        +string FullName
        +string PhoneNumber
        +string Address
        +ICollection~Order~ Orders
        +Customer(fullName, phoneNumber, address)
    }

    class Product {
        +string Name
        +string Description
        +decimal Price
        +long Quantity
        +string Code
        +Product(name, description, price, quantity, code)
    }

    class Order {
        +string CustomerId
        +DateTime OrderDateOnUtc
        +decimal TotalAmount
        +OrderStatus Status
        +Customer Customer
        +ICollection~OrderItem~ Items
        +Order(customerId, totalAmount)
        +AddItem(item)
    }

    class OrderItem {
        +string OrderId
        +string ProductId
        +int Quantity
        +decimal UnitPrice
        +Product Product
        +Order Order
        +OrderItem(orderId, productId, quantity, unitPrice)
    }

    class OrderStatus {
        <<enumeration>>
        Pending
        Paid
        Shipped
        Delivered
        Cancelled
    }

    %% Views (DTOs)
    class ProductView {
        +string Id
        +string Name
        +string Description
        +decimal Price
        +long Quantity
        +string Code
        +DateTime CreateOnUtc
        +DateTime? UpdateOnUtc
    }

    class CreateProductView {
        +string Name
        +string Description
        +decimal Price
        +long Quantity
        +string Code
    }

    class UpdateProductView {
        +string Name
        +string Description
        +decimal Price
        +long Quantity
        +string Code
    }

    class CustomerView {
        +string Id
        +string FullName
        +string PhoneNumber
        +string Address
        +DateTime CreateOnUtc
        +DateTime? UpdateOnUtc
    }

    class CreateCustomerView {
        +string FullName
        +string PhoneNumber
        +string Address
    }

    class UpdateCustomerView {
        +string FullName
        +string PhoneNumber
        +string Address
    }

    class OrderView {
        +string Id
        +string CustomerId
        +DateTime OrderDateOnUtc
        +decimal TotalAmount
        +OrderStatus Status
        +DateTime CreateOnUtc
        +DateTime? UpdateOnUtc
        +CustomerView Customer
        +List~OrderItemView~ Items
    }

    class CreateOrderView {
        +string CustomerId
        +decimal TotalAmount
        +List~CreateOrderItemView~ Items
    }

    class UpdateOrderView {
        +decimal TotalAmount
        +OrderStatus Status
    }

    class OrderItemView {
        +string Id
        +string OrderId
        +string ProductId
        +int Quantity
        +decimal UnitPrice
        +DateTime CreateOnUtc
        +DateTime? UpdateOnUtc
        +ProductView Product
    }

    class CreateOrderItemView {
        +string ProductId
        +int Quantity
        +decimal UnitPrice
    }

    class UpdateOrderItemView {
        +int Quantity
        +decimal UnitPrice
    }

    %% Repositórios
    class IRepository~T~ {
        <<interface>>
        +GetAllAsync() Task~IEnumerable~T~~
        +GetByIdAsync(id) Task~T?~
        +FindAsync(predicate) Task~IEnumerable~T~~
        +FirstOrDefaultAsync(predicate) Task~T?~
        +AddAsync(entity) Task~T~
        +UpdateAsync(entity) Task
        +DeleteAsync(entity) Task
        +ExistsAsync(id) Task~bool~
        +ExistsAsync(predicate) Task~bool~
    }

    class Repository~T~ {
        -DevShopDbContext _context
        -DbSet~T~ _dbSet
        +Repository(context)
        +GetAllAsync() Task~IEnumerable~T~~
        +GetByIdAsync(id) Task~T?~
        +FindAsync(predicate) Task~IEnumerable~T~~
        +FirstOrDefaultAsync(predicate) Task~T?~
        +AddAsync(entity) Task~T~
        +UpdateAsync(entity) Task
        +DeleteAsync(entity) Task
        +ExistsAsync(id) Task~bool~
        +ExistsAsync(predicate) Task~bool~
    }

    class IProductRepository {
        <<interface>>
        +GetByCodeAsync(code) Task~Product?~
        +GetByNameAsync(name) Task~Product?~
        +GetByPriceRangeAsync(minPrice, maxPrice) Task~IEnumerable~Product~~
        +GetLowStockAsync(threshold) Task~IEnumerable~Product~~
    }

    class ProductRepository {
        +ProductRepository(context)
        +GetByCodeAsync(code) Task~Product?~
        +GetByNameAsync(name) Task~Product?~
        +GetByPriceRangeAsync(minPrice, maxPrice) Task~IEnumerable~Product~~
        +GetLowStockAsync(threshold) Task~IEnumerable~Product~~
    }

    class ICustomerRepository {
        <<interface>>
        +GetWithOrdersAsync(id) Task~Customer?~
        +GetByPhoneNumberAsync(phoneNumber) Task~IEnumerable~Customer~~
        +GetByAddressAsync(address) Task~IEnumerable~Customer~~
    }

    class CustomerRepository {
        +CustomerRepository(context)
        +GetWithOrdersAsync(id) Task~Customer?~
        +GetByPhoneNumberAsync(phoneNumber) Task~IEnumerable~Customer~~
        +GetByAddressAsync(address) Task~IEnumerable~Customer~~
    }

    class IOrderRepository {
        <<interface>>
        +GetWithDetailsAsync(id) Task~Order?~
        +GetAllWithDetailsAsync() Task~IEnumerable~Order~~
        +GetByCustomerIdAsync(customerId) Task~IEnumerable~Order~~
        +GetByStatusAsync(status) Task~IEnumerable~Order~~
        +GetByDateRangeAsync(startDate, endDate) Task~IEnumerable~Order~~
    }

    class OrderRepository {
        +OrderRepository(context)
        +GetWithDetailsAsync(id) Task~Order?~
        +GetAllWithDetailsAsync() Task~IEnumerable~Order~~
        +GetByCustomerIdAsync(customerId) Task~IEnumerable~Order~~
        +GetByStatusAsync(status) Task~IEnumerable~Order~~
        +GetByDateRangeAsync(startDate, endDate) Task~IEnumerable~Order~~
    }

    %% Controllers
    class ProductsController {
        -IProductRepository _productRepository
        -IMapper _mapper
        +ProductsController(repository, mapper)
        +GetProducts() Task~ActionResult~IEnumerable~ProductView~~~
        +GetProduct(id) Task~ActionResult~ProductView~~
        +GetProductByCode(code) Task~ActionResult~ProductView~~
        +GetProductByName(name) Task~ActionResult~ProductView~~
        +GetProductsByPriceRange(minPrice, maxPrice) Task~ActionResult~IEnumerable~ProductView~~~
        +GetLowStockProducts(threshold) Task~ActionResult~IEnumerable~ProductView~~~
        +CreateProduct(view) Task~ActionResult~ProductView~~
        +UpdateProduct(id, view) Task~IActionResult~
        +DeleteProduct(id) Task~IActionResult~
    }

    class CustomersController {
        -ICustomerRepository _customerRepository
        -IMapper _mapper
        +CustomersController(repository, mapper)
        +GetCustomers() Task~ActionResult~IEnumerable~CustomerView~~~
        +GetCustomer(id) Task~ActionResult~CustomerView~~
        +CreateCustomer(view) Task~ActionResult~CustomerView~~
        +UpdateCustomer(id, view) Task~IActionResult~
        +DeleteCustomer(id) Task~IActionResult~
    }

    class OrdersController {
        -IOrderRepository _orderRepository
        -ICustomerRepository _customerRepository
        -IProductRepository _productRepository
        -IMapper _mapper
        +OrdersController(orderRepo, customerRepo, productRepo, mapper)
        +GetOrders() Task~ActionResult~IEnumerable~OrderView~~~
        +GetOrder(id) Task~ActionResult~OrderView~~
        +GetOrdersByCustomer(customerId) Task~ActionResult~IEnumerable~OrderView~~~
        +GetOrdersByStatus(status) Task~ActionResult~IEnumerable~OrderView~~~
        +CreateOrder(view) Task~ActionResult~OrderView~~
        +UpdateOrder(id, view) Task~IActionResult~
        +DeleteOrder(id) Task~IActionResult~
    }

    %% Data Context
    class DevShopDbContext {
        +DbSet~Customer~ Customers
        +DbSet~Product~ Products
        +DbSet~Order~ Orders
        +DbSet~OrderItem~ OrderItems
        +DevShopDbContext(options)
        +OnModelCreating(modelBuilder)
        +SaveChanges()
        +SaveChangesAsync()
        -UpdateTimestamps()
    }

    %% Mappings
    class AutoMapperProfile {
        +AutoMapperProfile()
    }

    %% Relacionamentos - Herança
    Entity <|-- Customer
    Entity <|-- Product
    Entity <|-- Order
    Entity <|-- OrderItem

    %% Relacionamentos - Entidades
    Customer ||--o{ Order : has
    Order ||--o{ OrderItem : contains
    OrderItem }o--|| Product : references
    Order }o--|| Customer : belongs to

    %% Relacionamentos - Repositórios
    IRepository <|.. Repository
    IRepository <|-- IProductRepository
    IRepository <|-- ICustomerRepository
    IRepository <|-- IOrderRepository

    Repository <|-- ProductRepository
    Repository <|-- CustomerRepository
    Repository <|-- OrderRepository

    IProductRepository <|.. ProductRepository
    ICustomerRepository <|.. CustomerRepository
    IOrderRepository <|.. OrderRepository

    %% Relacionamentos - Controllers
    ProductsController --> IProductRepository : uses
    ProductsController --> AutoMapperProfile : uses
    CustomersController --> ICustomerRepository : uses
    CustomersController --> AutoMapperProfile : uses
    OrdersController --> IOrderRepository : uses
    OrdersController --> ICustomerRepository : uses
    OrdersController --> IProductRepository : uses
    OrdersController --> AutoMapperProfile : uses

    %% Relacionamentos - Data Context
    ProductRepository --> DevShopDbContext : uses
    CustomerRepository --> DevShopDbContext : uses
    OrderRepository --> DevShopDbContext : uses

    %% Relacionamentos - Mappings
    AutoMapperProfile --> ProductView : maps
    AutoMapperProfile --> CreateProductView : maps
    AutoMapperProfile --> UpdateProductView : maps
    AutoMapperProfile --> CustomerView : maps
    AutoMapperProfile --> CreateCustomerView : maps
    AutoMapperProfile --> UpdateCustomerView : maps
    AutoMapperProfile --> OrderView : maps
    AutoMapperProfile --> CreateOrderView : maps
    AutoMapperProfile --> UpdateOrderView : maps
    AutoMapperProfile --> OrderItemView : maps
    AutoMapperProfile --> CreateOrderItemView : maps
    AutoMapperProfile --> UpdateOrderItemView : maps
```

## Descrição dos Componentes

### Entidades de Domínio
- **Entity**: Classe base abstrata com propriedades comuns
- **Customer**: Representa um cliente do sistema
- **Product**: Representa um produto disponível para venda
- **Order**: Representa um pedido feito por um cliente
- **OrderItem**: Representa um item específico em um pedido
- **OrderStatus**: Enumeração dos possíveis status de um pedido

### Views (DTOs)
- **ProductView/CreateProductView/UpdateProductView**: DTOs para produtos
- **CustomerView/CreateCustomerView/UpdateCustomerView**: DTOs para clientes
- **OrderView/CreateOrderView/UpdateOrderView**: DTOs para pedidos
- **OrderItemView/CreateOrderItemView/UpdateOrderItemView**: DTOs para itens de pedido

### Repositórios
- **IRepository<T>**: Interface genérica para operações CRUD
- **Repository<T>**: Implementação base genérica
- **IProductRepository/ProductRepository**: Repositório específico para produtos
- **ICustomerRepository/CustomerRepository**: Repositório específico para clientes
- **IOrderRepository/OrderRepository**: Repositório específico para pedidos

### Controllers
- **ProductsController**: Gerencia operações de produtos
- **CustomersController**: Gerencia operações de clientes
- **OrdersController**: Gerencia operações de pedidos

### Infraestrutura
- **DevShopDbContext**: Contexto do Entity Framework
- **AutoMapperProfile**: Configurações de mapeamento entre entidades e Views 