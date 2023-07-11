# simple-shop
Project creating the simple shop from the database-modelling [website](https://database-modelling.com/exercise/simple-shop-system).
The model has been implemented using a DDD approach with EF Core as ORM framework.
The project is implemented as a Minimal API, to support simple CRUD operations.

## DDD

Entity, Aggregate Root and ValueObject.
Repository pattern (Generic or Per AggregateRoot).
UnitOfWork (Transactions).
Readonly lists in entities.
Private setters in entities.
DomainEvents (Executed in transaction or executed asynchronously)


## Datamodel

```mermaid
---
title: Simple Shop System
---
erDiagram
ORDER {
    INT Id PK
    NVARCHAR(32) Name "NOT NULL"
    NVARCHAR(32) Organization "NULL"
    NVARCHAR(128) Address "NOT NULL"
    NVARCHAR(32) City "NOT NULL"
    NVARCHAR(32) State "NULL"
    INT ZipCode "NOT NULL"
    INT Phone "NOT NULL"
    INT PaymentMethod "NOT NULL"
    NVARCHAR(32) Email "NULL"
    INT DeliveryMethod "NOT NULL"
    DATETIME DeliveryAt "NOT NULL"
    TEXT Instructions "NULL"
}

ORDERITEM {
    INT Quantity "NOT NULL"
    ORDER Order "NOT NULL"
    ITEM Item "NOT NULL"
}

ITEM {
    INT Id PK
    NVARCHAR(32) Name "NOT NULL"
    DECIMAL Price "NOT NULL"
}

ITEM ||--o{ ORDERITEM : ""
ORDER ||--|{ ORDERITEM : ""
```
