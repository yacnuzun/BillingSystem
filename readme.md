# BillingSystem - Mikroservis Tabanlı Faturalandırma Sistemi

Bu proje, **.NET 8** ve **Angular** kullanılarak geliştirilmiş mikroservis mimarisine sahip bir faturalandırma ve müşteri yönetim sistemidir.

## 🧱 Genel Yapı

Proje aşağıdaki alt sistemlerden oluşmaktadır:

### 📦 Backend

BillingSystem.Backend
├── BillingSystemOperational
│ ├── CustomerService
│ └── InvoiceService
└── BillingSystem.Backend (Auth, User işlemleri)

#### 🔹 BillingSystem.Backend
- `Account`, `Auth`, `JWT`, `User` yönetimi gibi temel işlemleri içerir.
- `JWT` tabanlı kimlik doğrulama sistemi içerir.
- Kullanıcı kayıt ve login işlemleri buradan yönetilir.

#### 🔹 BillingSystemOperational.CustomerService
- Müşteri (Customer) ile ilgili işlemler bu serviste yer alır.
- Her müşteri bir kullanıcıya (User) bağlıdır.
- CRUD işlemleri ve kimlik doğrulama entegrelidir.

#### 🔹 BillingSystemOperational.InvoiceService
- Fatura (Invoice) işlemlerini yönetir.
- Tarih aralığına göre fatura filtreleme, oluşturma, silme gibi işlemler desteklenir.
- Her fatura bir müşteriye bağlıdır.

---

### 💻 Frontend (Angular)


- Angular 17 (standalone component mimarisi) kullanılmıştır.
- Login, Register, Fatura ve Müşteri ekranları yer alır.
- `AuthGuard`, `Interceptor`, `Service` yapılarıyla frontend güvenliği sağlanmıştır.
- `localStorage` üzerinden oturum yönetimi yapılır.

---

## 🧩 Mimaride Kullanılan Teknolojiler

| Katman/Servis         | Teknoloji                          |
|-----------------------|-------------------------------------|
| Backend API           | ASP.NET Core 8                     |
| Authentication        | JWT + FluentValidation             |
| ORM                   | Entity Framework Core              |
| Veritabanı            | PostgreSQL                         |
| Frontend              | Angular                            |
| API İletişimi         | HTTPClient + Interceptor yapısı    |
| DTO Mapping           | Automapper                         |
| Logging (opsiyonel)   | Serilog (eklenebilir)              |

---
