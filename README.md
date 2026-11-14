## Sheila Severino (2024-1693)

# Floristeria-Pedidos-API

API REST para la gestión de productos florales, clientes, pedidos y entregas, desarrollada con **ASP.NET Core Web API y .NET 10**.

## Descripción del proyecto

Este proyecto consiste en el desarrollo de una API REST para administrar los procesos principales de una floristería. Permite gestionar productos, registrar clientes, crear pedidos con sus respectivos detalles y administrar las entregas asociadas.

El proyecto se desarrolla como parte de la asignatura **Programación II**, aplicando conceptos de desarrollo de APIs, arquitectura por capas, Entity Framework Core, SQL Server y control de versiones con Git y GitHub.

## Tecnologías utilizadas

- C# y .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger
- Visual Studio 2026
- Git y GitHub

## Arquitectura del proyecto

La solución está organizada en tres proyectos independientes:

### Floristeria.API

Capa de presentación y exposición de los endpoints HTTP.

- Controllers: productos, clientes, pedidos y entregas.
- DTOs: modelos para recibir y validar solicitudes.
- Migrations: migraciones de Entity Framework Core.
- Program.cs: configuración de servicios e inyección de dependencias.

### Floristeria.Domain

Capa que define las entidades del negocio y los contratos de los repositorios.

- Entities: Product, Customer, Order, OrderDetail y Delivery.
- Interfaces: IProductRepository, ICustomerRepository, IOrderRepository e IDeliveryRepository.

### Floristeria.Infrastructure

Capa responsable del acceso a datos y la persistencia.

- Context: ApplicationDbContext.
- Repositories: implementación de los repositorios de productos, clientes, pedidos y entregas.
- Interfaces: contratos específicos para consultas de infraestructura.
- Models: modelos de consulta, incluyendo OrderSummaryModel.

Esta separación permite organizar las responsabilidades del sistema y facilitar su mantenimiento.

## Funcionalidades implementadas

- **Productos:** operaciones CRUD para administrar productos florales, precios, existencias y disponibilidad.
- **Clientes:** registro, consulta, actualización y eliminación de clientes.
- **Pedidos:** creación de pedidos con productos, cantidades, validación de existencias y cálculo del total.
- **Entregas:** registro y administración de entregas asociadas a pedidos.
- **Resúmenes de pedidos:** consulta de información resumida mediante el endpoint `GET /api/Orders/summaries`.
- **Persistencia:** acceso a SQL Server mediante Entity Framework Core y repositorios.
- **Validaciones:** comprobación de datos recibidos y reglas básicas del sistema.

## Endpoints principales

| Módulo | Ruta | Operaciones |
|---|---|---|
| Productos | `/api/Products` | GET, POST, PUT, DELETE |
| Clientes | `/api/Customers` | GET, POST, PUT, DELETE |
| Pedidos | `/api/Orders` | GET, POST, PUT, DELETE |
| Entregas | `/api/Deliveries` | GET, POST, PUT, DELETE |
| Resúmenes | `/api/Orders/summaries` | GET |

Los endpoints pueden consultarse y probarse mediante Swagger.

## Base de datos

El proyecto utiliza **SQL Server** con Entity Framework Core y migraciones Code First.

Las entidades principales son:

- Product
- Customer
- Order
- OrderDetail
- Delivery

El contexto `ApplicationDbContext` se encuentra en `Floristeria.Infrastructure`, mientras que las migraciones se mantienen en `Floristeria.API`.

La cadena de conexión se configura mediante `appsettings.json`, utilizando la configuración correspondiente al entorno local.

## Organización del repositorio

El proyecto utiliza una metodología de trabajo basada en Git Flow:

- **main:** rama reservada para la versión estable del producto.
- **develop:** rama principal de integración y desarrollo.
- **feature/**: ramas utilizadas para implementar funcionalidades y asignaciones específicas.

Cada funcionalidad se desarrolla en su propia rama y posteriormente se integra en `develop` mediante Pull Requests.

### Pull Requests de funcionalidades

- [PR #5 — Productos](../../pull/5)
- [PR #6 — Clientes](../../pull/6)
- [PR #7 — Pedidos](../../pull/7)
- [PR #8 — Entregas](../../pull/8)
- [PR #9 — Implementación de Domain e Infrastructure](../../pull/9)

## Verificaciones

- Compilación exitosa de la solución.
- Pruebas funcionales de la API mediante Swagger.
- Validación de operaciones CRUD y respuestas HTTP.
- Verificación de la conexión con SQL Server y la integración de Domain e Infrastructure.

## Datos de revisión

**La rama `develop` contiene el código integrado del proyecto.**

Las ramas `feature/` se conservan como evidencia del proceso de desarrollo y de la implementación progresiva de las funcionalidades.

La **Tarea 3** incorpora las capas Domain e Infrastructure, con sus entidades, interfaces, repositorios, modelos y contexto de base de datos, integradas con una API REST funcional.
