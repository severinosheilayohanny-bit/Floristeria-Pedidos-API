# Floristeria-Pedidos-API

API REST para la gestión de productos florales, clientes, pedidos y entregas, desarrollada en ASP.NET Core.

## Descripción del proyecto

Este proyecto consiste en el desarrollo de una API REST para administrar los procesos principales de una floristería. Permite gestionar productos, registrar clientes, crear pedidos con sus respectivos detalles y administrar las entregas asociadas.

El proyecto se desarrolla como parte de la asignatura Programación II, aplicando conceptos de desarrollo de APIs, Entity Framework Core, SQL Server, control de versiones y arquitectura de software.

## Tecnologías utilizadas

- C# y ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger
- Visual Studio
- Git y GitHub

## Funcionalidades implementadas

- **Productos:** operaciones CRUD para administrar los productos florales.
- **Clientes:** registro, consulta, actualización y eliminación de clientes.
- **Pedidos:** creación de pedidos con productos, cantidades y cálculo del total.
- **Entregas:** registro y administración de entregas asociadas a los pedidos.

## Organización del repositorio

El proyecto utiliza una metodología de trabajo basada en Git Flow:

- **main:** rama reservada para la versión estable del producto.
- **develop:** rama principal de integración y desarrollo.
- **feature/**: ramas utilizadas para implementar funcionalidades y asignaciones específicas.

Cada funcionalidad se desarrolla de forma independiente y posteriormente se integra en `develop` mediante Pull Requests.

### Pull Requests de funcionalidades

- [PR #5 — Productos](../../pull/5)
- [PR #6 — Clientes](../../pull/6)
- [PR #7 — Pedidos](../../pull/7)
- [PR #8 — Entregas](../../pull/8)

## Base de datos

El proyecto utiliza SQL Server con Entity Framework Core y migraciones Code First.

Las entidades principales son:

- Product
- Customer
- Order
- OrderDetail
- Delivery

## Datos de revisión 

**La rama `develop` contiene el código integrado del proyecto.**

Las ramas `feature/` se conservan como evidencia del proceso de desarrollo y de la implementación progresiva de las funcionalidades.

La arquitectura por capas se incorporará como parte de las siguientes asignaciones de Programación II.
