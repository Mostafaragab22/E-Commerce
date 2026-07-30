# E-Commerce API

A RESTful backend for a general e-commerce platform, built with **ASP.NET Core 9 Web API** and **Entity Framework Core**.

## Overview

This API covers the full customer and admin lifecycle for an online store: product catalog with brands and categories, cart and wishlist, checkout with addresses/shipment/payment, order tracking, reviews, notifications, and inventory management — all secured with JWT authentication.

## Tech Stack

- **Framework:** ASP.NET Core 9 Web API
- **Database:** SQL Server + Entity Framework Core 9
- **Auth:** ASP.NET Core Identity + JWT Bearer authentication
- **Docs:** Swagger / Swashbuckle
- **Resilience:** Built-in ASP.NET Core Rate Limiting

## Project Structure

```
E-Commerce/
├── Controllers/    # API endpoints
├── DTOs/            # Request/response data contracts
├── Models/           # Entities and EF Core DbContext
├── Repository/       # Data access layer
├── Extensions/        # DI / service registration
└── Migrations/         # EF Core migrations
```

## Features

- **Authentication & Accounts** — registration, login, password change/reset, forgot password
- **Product Catalog** — products, brands, categories with full CRUD
- **Cart** — add, update, and remove cart items
- **Wishlist** — add/remove items, view personal wishlist
- **Addresses** — manage shipping addresses per user
- **Orders** — create, cancel, view order details/history, admin status updates
- **Payments** — record and retrieve payments per order
- **Shipment** — create and update shipment records
- **Reviews** — add and fetch product reviews
- **Inventory** — stock tracking with adjustment and movement history
- **Notifications** — per-user notifications with read status
- **User Management** — profile view/update, admin user type management
- **API Protection** — built-in rate limiting
- **API Docs** — interactive Swagger UI with JWT bearer support

## Core Entities

`Product`, `ProductVariant`, `Brand`, `Category`, `Cart`, `CartItem`, `Wishlist`, `WishlistItem`, `Order`, `OrderItem`, `OrderStatusHistory`, `Address`, `Payment`, `Refund`, `Shipment`, `Review`, `Inventory`, `InventoryMovement`, `Notification`, `Coupon`, `Promotion`, `ActivityLog`, `User`, `Role`

## API Endpoints

| Controller | Sample Endpoints |
|---|---|
| **Account** | `POST /Register`, `POST /Login`, `POST /change-password`, `POST /forgot-password`, `POST /reset-password` |
| **Product** | `GET`, `GET /{id}`, `POST`, `PUT /{id}`, `DELETE /{id}` |
| **Brand** | `GET`, `GET /{id}`, `POST`, `PUT /{id}`, `DELETE /{id}` |
| **Category** | `GET`, `GET /{id}`, `POST`, `PUT /{id}`, `DELETE /{id}` |
| **Cart** | `GET`, `POST`, `PUT /{id}`, `DELETE /{id}` |
| **Wishlist** | `POST`, `GET /my`, `DELETE /items/{itemId}` |
| **Address** | `GET /{id}`, `POST`, `PUT /{id}` |
| **Order** | `POST`, `GET /{id}`, `GET /OrderDetails/{id}`, `PUT /CancelOrder/{id}`, `GET`, `PUT /AdminUpdateStuts/{id}` |
| **Payment** | `GET`, `POST` |
| **Shipment** | `GET /{id}`, `POST`, `PUT /{id}` |
| **Review** | `POST`, `GET /{id}` |
| **Inventory** | `GET`, `POST /Adjust`, `GET /Movements/{itemId}` |
| **Notifications** | `GET`, `PUT /{id}/read` |
| **User** | `GET /MyProfile`, `PUT /UpdateMyProfile`, `GET`, `PUT /{id}/type` |

Full request/response contracts are available via Swagger UI once the API is running.

## Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full instance)

### Setup

1. **Clone the repository**
   ```bash
   git clone git@github.com:Mostafaragab22/E-Commerce.git
   cd E-Commerce
   ```

2. **Configure `appsettings.json`**

   Add your connection string and JWT settings:
   ```json
   {
     "ConnectionStrings": {
       "cs": "Server=YOUR_SERVER;Database=ECommerceDb;Trusted_Connection=True;TrustServerCertificate=True"
     },
     "JWT": {
       "Issuer": "your-issuer",
       "Audience": "your-audience",
       "SecretKey": "your-secret-key"
     }
   }
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the API**
   ```bash
   dotnet run
   ```

5. **Explore the API**
   Open `https://localhost:{port}/swagger` to view and test all endpoints. Use the **Authorize** button with a `Bearer {token}` from `/Login` to access protected routes.

## Author

**Mostafa Ragab**
Full Stack .NET Developer
[GitHub](https://github.com/Mostafaragab22) • [LinkedIn](https://www.linkedin.com/in/mostafa-ragab-846a09386)

