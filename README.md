## Sheila Severino (2024-1693)

# Floristeria-Pedidos-API

API REST para la gestión de productos florales, clientes, pedidos y entregas, desarrollada con **ASP.NET Core Web API y .NET 10**.

## Descripción del proyecto

Este proyecto consiste en el desarrollo de una API REST para administrar los procesos principales de una floristería. Permite gestionar productos, registrar clientes, crear pedidos con sus respectivos detalles y administrar las entregas asociadas.

Se desarrolla como parte de la asignatura **Programación II**, aplicando arquitectura por capas, Entity Framework Core, SQL Server, inyección de dependencias y control de versiones con Git y GitHub.

## Tecnologías utilizadas

- C# y .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger
- Visual Studio 2026
- Git y GitHub

## Arquitectura del proyecto

La solución está organizada en cuatro proyectos independientes:

### Floristeria.API

Capa de presentación y exposición de los endpoints HTTP.

- **Controllers:** productos, clientes, pedidos y entregas.
- **Migrations:** migraciones de Entity Framework Core.
- **Program.cs:** configuración de servicios e inyección de dependencias.

### Floristeria.Application

Capa encargada de implementar los servicios y la lógica de negocio del sistema.

- **Contract:** interfaces de los servicios.
- **Core:** BaseService, IBaseService y ServiceResult.
- **Dtos:** modelos de transferencia de datos organizados por módulo.
- **Service:** implementación de ProductService, CustomerService, OrderService y DeliveryService.

Los servicios gestionan las operaciones y validaciones del negocio, evitando que estas responsabilidades permanezcan directamente en los controladores.

### Floristeria.Domain

Capa que define las entidades del negocio y los contratos de los repositorios.

- **Entities:** Product, Customer, Order, OrderDetail y Delivery.
- **Interfaces:** IProductRepository, ICustomerRepository, IOrderRepository e IDeliveryRepository.

### Floristeria.Infrastructure

Capa responsable del acceso a datos y la persistencia.

- **Context:** ApplicationDbContext.
- **Repositories:** implementación de los repositorios.
- **Interfaces:** contratos específicos para consultas de infraestructura.
- **Models:** modelos de consulta, incluyendo OrderSummaryModel.

Esta separación permite organizar las responsabilidades del sistema y facilitar su mantenimiento.

## Funcionalidades implementadas

- **Productos:** operaciones CRUD para administrar productos florales, precios, existencias y disponibilidad.
- **Clientes:** registro, consulta, actualización y eliminación de clientes.
- **Pedidos:** creación de pedidos, validación de productos y existencias, cálculo de totales y actualización de estados.
- **Entregas:** registro y administración de entregas, validación de fechas y actualización de estados.
- **Resúmenes de pedidos:** consulta mediante `GET /api/Orders/summaries`.
- **Capa de servicios:** implementación de contratos y servicios para los cuatro módulos.
- **Persistencia:** acceso a SQL Server mediante Entity Framework Core y repositorios.
- **Validaciones:** comprobación de datos recibidos y aplicación de reglas de negocio.

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

Las entidades principales son Product, Customer, Order, OrderDetail y Delivery.

El contexto `ApplicationDbContext` se encuentra en `Floristeria.Infrastructure`, mientras que las migraciones se mantienen en `Floristeria.API`.

La cadena de conexión se configura mediante `appsettings.json`, utilizando la configuración correspondiente al entorno local.

## Organización del repositorio

El proyecto utiliza una metodología de trabajo basada en Git Flow:

- **main:** rama reservada para la versión estable del producto.
- **develop:** rama que contiene la entrega de la Tarea 3.
- **application:** rama que incorpora la capa de servicios correspondiente a la Tarea 4.
- **feature/***: ramas utilizadas para implementar funcionalidades específicas.

Las ramas `develop` y `application` se mantienen independientes para facilitar la revisión de ambas asignaciones.

### Pull Requests de funcionalidades

- [PR #5 — Productos](../../pull/5)
- [PR #6 — Clientes](../../pull/6)
- [PR #7 — Pedidos](../../pull/7)
- [PR #8 — Entregas](../../pull/8)
- [PR #9 — Implementación de Domain e Infrastructure](../../pull/9)

## Verificaciones

- Recompilación exitosa de los cuatro proyectos, sin errores.
- Pruebas de consultas mediante Swagger.
- Validación de creación de pedidos con cliente inexistente.
- Registro y actualización de una entrega.
- Verificación de la integración entre API, Application, Domain e Infrastructure.

## Datos de revisión

### Tarea 3 — Domain e Infrastructure

**Rama:** `develop`

Implementación de las capas Domain e Infrastructure, incluyendo entidades, interfaces, repositorios, modelos y contexto de base de datos, integradas con una API REST funcional.

### Tarea 4 — Capa de servicios

**Rama:** `application`

Implementación de la capa Application con las carpetas Contract, Core, Dtos y Service.

Se desarrollaron los servicios de productos, clientes, pedidos y entregas, integrándolos con los controladores mediante inyección de dependencias.

La solución fue compilada correctamente y se realizaron pruebas funcionales mediante Swagger.
