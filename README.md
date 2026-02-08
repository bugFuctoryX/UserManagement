# UserManagement

> A Blazor Server (.NET 10) demo / proof-of-concept application showcasing modern user management with clean architecture.

---

## 🧭 Overview

**UserManagement** is a Blazor Server–based (**Interactive Server**) .NET 10 application that demonstrates  
**user authentication, listing, editing, and deletion** using a modern, maintainable architecture.

This project is intentionally a **pet project / training app**, built as a **technology reference and showcase**.

### 🎯 Focus areas
- **Telerik UI for Blazor** – professional UI components
- **CQRS + MediatR** – clear separation of commands and queries
- **FluentValidation** – centralized validation pipeline
- **FileDb (CSV)** – lightweight persistence with audit logging

- ### ✨ Recent updates
- **SignalR-based rendering** via **Interactive Server** render mode.
- **Authentication** handled through a **JavaScript HTTP endpoint** that manages user cookies.

```razor
<TelerikRootComponent>
    <Routes @rendermode="InteractiveServer" />
</TelerikRootComponent>
```
---

## 🧱 Technology stack

| Area | Technology |
|-----|-----------|
| Framework | **.NET 10** (`net10.0`) |
| UI | **Blazor Web App (Server / Interactive)** |
| Architecture | **CQRS + MediatR** |
| Validation | **FluentValidation** |
| UI Library | **Telerik UI for Blazor** |
| Persistence | **FileDb (CSV)** |

---

## 🗂️ Project structure

```text
UserManagement
├─ Core
│  ├─ UserManagement.Domain          # Domain entities (User, Profile, Credential, Audit)
│  └─ UserManagement.Application     # CQRS, use cases, validations, pipeline
├─ Infrastructure
│  └─ UserManagement.Infrastructure  # FileDb (CSV), audit, seeding
└─ Presentation
   └─ UserManagement.Web             # Blazor UI + Telerik components
├─ Tests
│  └─ UserManager.Tests.Integration  # Integration tests (ASP.NET Core hosting)
```

---

## ✅ Integration tests

This project includes an **integration test suite** in `Tests/UserManager.Tests.Integration`, built with  
**xUnit** and **Microsoft.AspNetCore.Mvc.Testing** to validate end-to-end behavior of the Blazor Server app.

**Run tests**
```bash
dotnet test Tests/UserManager.Tests.Integration
```
---

## 💡 Potential future additions
- **Role-based authorization (RBAC)** and policy-based access control
- **Refresh token rotation** and stronger session security practices
- **Audit trail UI** for browsing historical actions
- **Multi-language (i18n)** support for user-facing screens