# Survivor Web API Projesi

Bu proje, bir Survivor konseptine sahip olan, yarışmacı ve kategorileri yöneten basit bir Web API uygulamasıdır. .NET 8.0, ASP.NET Core ve Entity Framework Core kullanılarak geliştirilmiştir.

## Proje Amacı

Uygulama, yarışmacılar ve kategoriler arasında bire-çok (one-to-many) ilişkisi kurarak, bu iki varlık üzerinde temel CRUD (Create, Read, Update, Delete) işlemlerini gerçekleştiren RESTful API endpoint'leri sunar.

## Özellikler

-   **Katmanlı Mimari:** Proje, daha düzenli ve yönetilebilir bir yapı için Entity ve Data katmanlarına ayrılmıştır.
-   **Entity Framework Core:** Veritabanı işlemleri için ORM (Object-Relational Mapper) olarak kullanılmıştır.
-   **Migration:** Veritabanı şeması, kod üzerinden `migration` komutları ile yönetilmektedir.
-   **Soft Delete:** Veriler, `IsDeleted` bayrağı ile yumuşak bir şekilde silinir, veritabanından kalıcı olarak kaldırılmaz.
-   **Swagger:** API endpoint'leri, Swagger (OpenAPI) arayüzü aracılığıyla otomatik olarak belgelenir ve canlı olarak test edilebilir.

## API Endpoint'leri

Projenin temel API endpoint'leri, Swagger arayüzünde detaylı bir şekilde belgelenmiştir. Başlıca endpoint'ler şunlardır:

### `CategoryController`
-   `GET /api/Category`: Tüm kategorileri listeler.
-   `POST /api/Category`: Yeni bir kategori oluşturur.
-   `PUT /api/Category/{id}`: Belirli bir kategoriyi günceller.
-   `DELETE /api/Category/{id}`: Belirli bir kategoriyi yumuşak olarak siler.

### `CompetitorController`
-   `GET /api/Competitor`: Tüm yarışmacıları listeler.
-   `GET /api/Competitor/category/{categoryId}`: Belirli bir kategoriye ait yarışmacıları listeler.
-   `POST /api/Competitor`: Yeni bir yarışmacı oluşturur.
-   `PUT /api/Competitor/{id}`: Belirli bir yarışmacıyı günceller.
-   `DELETE /api/Competitor/{id}`: Belirli bir yarışmacıyı yumuşak olarak siler.
