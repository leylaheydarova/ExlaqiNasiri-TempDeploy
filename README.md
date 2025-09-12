## Adlandırma Standartları

### 1.1 Migrations
- Migrations adlandırmalarda hansı entity yaradılıbsa, onun adı ilə, lakin ilk hərf **kiçik** şəkildə yazılmalıdır (capitalize).
- **Nümunə:** `tedbirFileRelation023982103`

### 1.2 Exception-lar
- Bütün exception-larda ilk öncə **nəyə aid olduğu**, sonra isə **exception tipi** yazılmalıdır.
- **Nümunə:** `RegisterFailedException`

### 1.3 Entity-lərdə ID
- Entity-lərdə `ID` hissəsi **böyüklə** yazılmalıdır.
- **Nümunə:** `HədisID`

---

## Əlavə Standartlar

### 2.1 NotFoundException
- NotFoundException istifadə edilərkən mütləq `string message` verilməlidir.
- Message hissəsində yalnız **tapılmayan entity** adı qeyd olunmalıdır.
- Şablon artıq `base`-də hazırdır:
  ```csharp
  base($"Sorry, {message} cannot be found!") message kiçik hərflə yazılmalıdır, çünki cümlənin ortasında istifadə olunur.
### 2.2 Boş Data Qaydası

- Əgər database-də `GetSingle` və `GetAll` üçün data tapılmırsa:
  - **Exception atılmamalıdır.**
  - Əvəzində **default (boş) data** qaytarılmalıdır.
- Bu standart **qeyd bölməsində** də göstərilib, burada bir daha vurğulanır.

---

## Multi-Environment İstifadəsi

Miqrasiyanı silmək və ya database-i yeniləmək üçün **mütləq hansı environment-də** işlədiyimiz göstərilməlidir.  
Əks halda `appsettings.json` faylı oxunur və `connection string` tapılmadığı üçün **xəta baş verir**.

### 3.1 Package Manager Console üçün:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Azima"

3.2 Terminal (CLI) üçün:

dotnet ef migrations add InitialMigration --environment Azima
dotnet ef database update --environment Azima

Note: Artiq proyektde "Manage User Secret"-den istifade etdiyimize gore, environment-e ehtiyac yoxdur.
