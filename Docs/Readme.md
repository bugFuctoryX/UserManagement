# Projekt áttekintés

## Technológia
- **.NET 10**
- **C# 14**

## Architektúra és minták

### Clean Architecture
**Projekt-referenciák (dependencies):**
- **Presentation** → Application + Infrastructure  
- **Infrastructure** → Application + Domain  
- **Application** → Domain  
- **Domain** → *(nincs függőség)*

### Használt minták
- **Unit of Work**
  - `IUnitOfWork` az **Application/Abstractions** rétegben
  - implementáció az **Infrastructure** rétegben
- **Mediator pattern** (pl. MediatR)

## API
- **REST API** – **Minimal APIs**
- **EF Core Lazy Loading**

## Alkalmazás-szervezés
- **CQRS** (Command Query Responsibility Segregation)
  - **Commands / Queries**
  - **Request / Response** modellek

## Külső könyvtárak
- **FluentValidation**
- **FluentResult**
- **MediatR**
- **Swagger**

## Authentication és Authorization
- **JWT Bearer token alapú auth**
- **Role-based authorization**
- **Policy-based authorization**
- **Custom requirement-ek és handler-ek**
- **Refresh token mechanizmus**
- **Password hashing** (BCrypt)
- **Account lockout** (többszöri sikertelen bejelentkezés után)

## Command 
dotnet user-secrets set "Jwt:PrivateKeyPem" "-----BEGIN PRIVATE KEY-----\ncf577f21-49e0-4a37-900d-360f9aa7ca79\n-----END PRIVATE KEY-----"

### Design pattern kategóriák (GoF vs. enterprise)
- **GoF (Gang of Four) patternök:** **Mediator** (Behavioral)
- **Architekturális / enterprise minták:** **Clean Architecture**, **CQRS**, **Unit of Work**

### Common/Behaviors
MediatR pipeline behavior-ok (MediatR “middleware”). Cross-cutting logika ide kerül, ami minden request előtt/után lefut, pl. validáció, logging, tranzakciókezelés.
