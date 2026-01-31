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
