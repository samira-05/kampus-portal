# KampüsPortal — Kampüs Etkinlik ve Kulüp Portalı

Kampüs içi öğrenci kulüplerini keşfetmek, kulüp bilgilerini incelemek ve düzenlenen etkinlikleri takip etmek amacıyla **ASP.NET Core MVC (.NET 10)** ile geliştirilmiş ilişkisel veri tabanlı bir web uygulamasıdır.

## Proje Hakkında

KampüsPortal, üniversite öğrencilerinin ilgi alanlarına göre topluluklara ulaşmasını ve kampüs içi güncel etkinliklerden haberdar olmasını sağlar. Uygulama; kulüp ve etkinlik yönetimi (CRUD), kategori bazlı filtreleme, anlık arama ve tarih doğrulama gibi temel web geliştirme pratiklerini temiz bir MVC mimarisiyle birleştirir.

### Temel Özellikler

* **Kulüp Keşfi ve Yönetimi (CRUD):** Öğrenci kulüplerini listeler, kategoriye (`Teknoloji`, `Sanat`, `Spor`, `Sosyal`) ve kulüp adına göre arama/filtreleme imkânı sunar. Yeni kulüp ekleme, düzenleme, detay görüntüleme ve silme işlemlerini destekler.
* **Etkinlik Takibi ve Yönetimi (CRUD):** Kulüplerin düzenlediği atölye, turnuva ve buluşmaları listeler; yeni etkinlik oluşturma, düzenleme, detay inceleme ve silme akışlarını sunar.
* **Akıllı Tarih ve Doğrulama Kuralları:** Ana sayfada yalnızca yaklaşan etkinlikler tarihe göre sıralı şekilde listelenir (geçmiş etkinlikler gizlenir). Etkinlik ekleme/düzenleme formlarında geçmiş tarih seçimi ve eksik alan girişi doğrulama (validation) kurallarıyla engellenir.
* **İlişkisel Veri Modeli (One-to-Many):** Her kulübün birden fazla etkinliğe sahip olabildiği bire-çok ilişki yapısı sayesinde, kulüp detay sayfasında o kulübe ait tüm etkinlikler otomatik olarak listelenir.
* **Duyarlı (Responsive) Arayüz:** Masaüstü ve mobil ekranlara uyumlu Bootstrap 5 ve özel CSS bileşenleriyle modern, okunaklı bir kullanıcı deneyimi sunar.

## Kullanılan Teknolojiler

* **Backend:** C#, ASP.NET Core MVC (.NET 10)
* **ORM & Veritabanı:** Entity Framework Core, SQLite
* **Frontend:** Razor Views (`.cshtml`), Bootstrap 5, Bootstrap Icons, HTML5, CSS3
* **Sürüm Kontrolü:** Git & GitHub

## Veritabanı Mimarisi

Uygulama SQLite üzerinde iki ana tablo ve aralarındaki bire-çok (`1:N`) ilişki üzerine kuruludur:

| Tablo | Alanlar | Açıklama |
| :--- | :--- | :--- |
| **Clubs** | `Id`, `Name`, `Description`, `Category` | Öğrenci kulüplerini tutar. `Category` alanı `enum` (`Teknoloji`, `Sanat`, `Spor`, `Sosyal`) olarak tanımlanmıştır. |
| **Events** | `Id`, `Title`, `Description`, `EventDate`, `Location`, `ClubId` | Etkinlik bilgilerini tutar. `ClubId` yabancı anahtarı (*Foreign Key*) ile ilgili kulübe bağlanır. |

## Proje Klasör Yapısı

```text
KampusPortal/
├── Controllers/
│   ├── HomeController.cs        # Ana sayfa, öne çıkan kulüpler ve yaklaşan etkinlikler
│   ├── ClubsController.cs       # Kulüp listeleme, arama, filtreleme ve CRUD işlemleri
│   └── EventsController.cs      # Etkinlik yönetimi, kulüp seçimi ve tarih doğrulama
├── Data/
│   └── ApplicationDbContext.cs  # EF Core veritabanı bağlamı ve DbSet tanımları
├── Migrations/                  # Veritabanı şema geçmişi
├── Models/
│   ├── Club.cs                  # Kulüp modeli ve veri doğrulama kuralları
│   ├── Event.cs                 # Etkinlik modeli ve tarih kontrol kuralları
│   └── ClubCategory.cs          # Kulüp kategorileri enum tanımı
├── Views/
│   ├── Home/                    # Ana sayfa görünümü
│   ├── Clubs/                   # Kulüp dizini, detay, ekleme, düzenleme ve silme görünümleri
│   ├── Events/                  # Etkinlik kartları, detay ve form görünümleri
│   └── Shared/                  # Ortak yerleşim (_Layout.cshtml) ve navigasyon menüsü
└── Program.cs                   # Uygulama yapılandırması, servis kayıtları ve yönlendirme

## Ekran Görüntüleri

### 1. Ana Sayfa
<img width="1892" height="861" alt="Ana Sayfa" src="https://github.com/user-attachments/assets/0658d5a3-8881-4ddd-8d53-27881323cd9f" />

### 2. Kulüpler Sayfası
<img width="1882" height="855" alt="Külüpler Sayfası" src="https://github.com/user-attachments/assets/2a5dc2bf-4e44-46a3-89ad-591b07e8f7c6" />

### 3. Kampüs Etkinlikleri Sayfası
<img width="1890" height="857" alt="Etkinlikler Sayfası" src="https://github.com/user-attachments/assets/b60aef75-75c8-428e-b61f-2c9005ecf695" />
