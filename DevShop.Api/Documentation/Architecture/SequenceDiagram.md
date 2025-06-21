# Diagrama de Sequência - DevShop API

## Fluxo de Criação de Pedido

```mermaid
sequenceDiagram
    participant Client
    participant OrdersController
    participant CustomerRepo
    participant ProductRepo
    participant OrderRepo
    participant Mapper
    participant DbContext
    participant DB as SQLite Database

    Client->>OrdersController: POST /api/orders<br/>CreateOrderView
    activate OrdersController

    OrdersController->>CustomerRepo: GetByIdAsync(customerId)
    activate CustomerRepo
    CustomerRepo->>DbContext: FindAsync(customerId)
    activate DbContext
    DbContext->>DB: SELECT * FROM Customers WHERE Id = @customerId
    activate DB
    DB-->>DbContext: Customer data
    deactivate DB
    DbContext-->>CustomerRepo: Customer entity
    deactivate DbContext
    CustomerRepo-->>OrdersController: Customer (or null)
    deactivate CustomerRepo

    alt Customer not found
        OrdersController-->>Client: 400 Bad Request<br/>"Cliente não encontrado"
        deactivate OrdersController
    else Customer found
        OrdersController->>Mapper: Map<Order>(createOrderView)
        activate Mapper
        Mapper-->>OrdersController: Order entity
        deactivate Mapper
        
        loop For each item in createOrderView.Items
            OrdersController->>ProductRepo: GetByIdAsync(item.ProductId)
            activate ProductRepo
            ProductRepo->>DbContext: FindAsync(productId)
            activate DbContext
            DbContext->>DB: SELECT * FROM Products WHERE Id = @productId
            activate DB
            DB-->>DbContext: Product data
            deactivate DB
            DbContext-->>ProductRepo: Product entity
            deactivate DbContext
            ProductRepo-->>OrdersController: Product (or null)
            deactivate ProductRepo
            
            alt Product not found
                OrdersController-->>Client: 400 Bad Request<br/>"Produto com ID {productId} não encontrado"
                deactivate OrdersController
            else Product found
                OrdersController->>Mapper: Map<OrderItem>(itemView, context)
                activate Mapper
                Mapper-->>OrdersController: OrderItem entity
                deactivate Mapper
                
                OrdersController->>OrdersController: order.AddItem(orderItem)
            end
        end
        
        OrdersController->>OrderRepo: AddAsync(order)
        activate OrderRepo
        OrderRepo->>DbContext: Add(order)
        activate DbContext
        DbContext->>DB: INSERT INTO Orders (...)
        activate DB
        DB-->>DbContext: Order created
        deactivate DB
        DbContext->>DbContext: SaveChangesAsync()
        DbContext->>DB: INSERT INTO OrderItems (...)
        activate DB
        DB-->>DbContext: OrderItems created
        deactivate DB
        DbContext-->>OrderRepo: Order entity
        deactivate DbContext
        OrderRepo-->>OrdersController: Created order
        deactivate OrderRepo
        
        OrdersController->>OrderRepo: GetWithDetailsAsync(orderId)
        activate OrderRepo
        OrderRepo->>DbContext: Include(Customer).Include(Items).ThenInclude(Product)
        activate DbContext
        DbContext->>DB: SELECT * FROM Orders o<br/>LEFT JOIN Customers c ON o.CustomerId = c.Id<br/>LEFT JOIN OrderItems oi ON o.Id = oi.OrderId<br/>LEFT JOIN Products p ON oi.ProductId = p.Id<br/>WHERE o.Id = @orderId
        activate DB
        DB-->>DbContext: Order with details
        deactivate DB
        DbContext-->>OrderRepo: Order with relationships
        deactivate DbContext
        OrderRepo-->>OrdersController: Order with details
        deactivate OrderRepo
        
        OrdersController->>Mapper: Map<OrderView>(orderWithDetails)
        activate Mapper
        Mapper-->>OrdersController: OrderView
        deactivate Mapper
        
        OrdersController-->>Client: 201 Created<br/>OrderView
        deactivate OrdersController
    end
```

## Descrição do Fluxo

### 1. Recebimento da Requisição
- **Cliente** envia requisição `POST /api/orders` com `CreateOrderView`
- **OrdersController** recebe e processa a requisição

### 2. Validação do Cliente
- Controller chama `CustomerRepo.GetByIdAsync()` para verificar se o cliente existe
- Se cliente não existir, retorna erro 400 Bad Request
- Se cliente existir, continua o processo

### 3. Criação do Pedido
- **AutoMapper** converte `CreateOrderView` em entidade `Order`
- O pedido é criado com status "Pending" e data atual

### 4. Processamento dos Itens
- Para cada item no pedido:
  - Valida se o produto existe via `ProductRepo.GetByIdAsync()`
  - Se produto não existir, retorna erro 400 Bad Request
  - Se produto existir, cria `OrderItem` via AutoMapper
  - Adiciona o item ao pedido

### 5. Persistência no Banco
- **OrderRepo.AddAsync()** salva o pedido e seus itens
- **DbContext** executa as transações SQL necessárias
- **SQLite** confirma as inserções

### 6. Recuperação com Detalhes
- **OrderRepo.GetWithDetailsAsync()** busca o pedido com relacionamentos
- Inclui dados do cliente e produtos dos itens
- **DbContext** executa JOIN complexo para obter todos os dados

### 7. Conversão e Resposta
- **AutoMapper** converte entidade `Order` em `OrderView`
- Controller retorna `201 Created` com os dados do pedido criado

## Pontos Importantes

### Validações
- **Cliente**: Deve existir antes de criar o pedido
- **Produtos**: Cada produto referenciado deve existir
- **Dados**: Validação automática via Model Binding

### Transações
- **Entity Framework** gerencia transações automaticamente
- Se qualquer operação falhar, toda a transação é revertida
- Garante consistência dos dados

### Performance
- **Eager Loading**: Carrega relacionamentos necessários
- **Single Query**: JOIN otimizado para buscar dados completos
- **Async/Await**: Operações não-bloqueantes

### Tratamento de Erros
- **400 Bad Request**: Para dados inválidos ou entidades não encontradas
- **500 Internal Server Error**: Para erros inesperados do servidor
- **Logs**: Rastreamento de erros para debugging

## Benefícios do Fluxo

### Robustez
- Validações em múltiplas camadas
- Transações ACID
- Tratamento adequado de erros

### Performance
- Operações assíncronas
- Queries otimizadas
- Carregamento eficiente de relacionamentos

### Manutenibilidade
- Separação clara de responsabilidades
- Código bem estruturado
- Fácil debugging e testes 