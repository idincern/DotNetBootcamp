**Best Practice Örneği**
- Modeldeki Servis katmanında Repository katmanından alınan veriler bir ImmutableList içerisine record constructorunda deep copy yapılarak Controller katmanından Repository katmanına erişim kısıtlanır, DTO sayesinde veri iletilir. Bu sayede DB'deki veri bütünlüğü korunmuş olur ve client sidedan gelebilecek veri tehditlerine karşı korunulur. Controller'dan bu ImmutableList'i alarak istemcilere sunmak, veri bütünlüğünü korumaya yardımcı olur. İstemciler, bu liste üzerinde sadece okuma işlemleri yapabilir ve verilerin değiştirilmesi sadece uygulama içinde tanımlanan yollarla mümkün olur.
- Side effect'i olmayan metod(pure function) örneği - Functional Programming C# Vascaran Sarcar:
```cs
private decimal CalculateTax(decimal price, decimal tax) => (price * tax); // Bu sayede tax değeri her fonksiyonda tekrardan gönderilir. Statik/global değişkende oluşacak tax değişikliği metod çalışma mantığını etkilemez.
```
- Servisteki Add metodu geriye eklenen ürünün "Id"sini döndürür.
### Ödev:
1) ReadOnly List - Immutable List
2) Delegate / Event
3) Generic Method / Class


## Route Tanımlama:
- Model Binding: ![Model Binding](model_binding.png)
- **URL'de gizliliği önemli data taşınmamalı. Çünkü SSL dahi kullanılsa bu data şifrelenmez. Güvenli data taşıma yolu: Request/Response'ın Bodysi/Header'ı.**
- .NET'de bulunan IDataProtected() interface'indeki metodları şifreleme konusunda kullanılabilir(Tokenlarda değinilecek).

```cs
[HttpGet]
public IActionResult GetAll(int id) {...}

[HttpGet({"id"})]
public IActionResult GetById(int id) {...}

[HttpDelete]
public IActionResult Delete(int id) {...}
```

## Repository'e Update ve Add(Create) Metodlarının Eklenmesi:
![update_add](update_add.png)

### Bu metodları karşılayabilmek için ProductRequestDto ve ProductUpdateRequest "data class/recordu" oluşturulur(sadece bize gerekli olan Name ve Price bilgisini taşımak için)
![dtos](dtos.png)
![dto1](dto1.png)
![dto2](dto2.png)

## Service'e Update ve Add(Create) Metodlarının Eklenmesi:
![update](update.png)
![update_wId](update_wId.png)
![add](add.png)

## Controller'a Update ve Add(Create) Metodlarının Eklenmesi:

### Update:
![route_update](route_update.png)

### Create:
![route_create](route_create.png)
![create_postman](create_postman.png)


# Cyclomatic Complexity:
Bir programın karmaşıklığını ölçmek için kullanılan bir metrik veya ölçüdür. Bu ölçüm, bir programın içinde kaç tane farklı yol veya dolaşım olduğunu belirlemeye çalışır. Programlar, genellikle farklı kararlar, döngüler ve dallanmalar içerir. Cyclomatic complexity, bu yapıları analiz ederek bir programın ne kadar karmaşık olduğunu ölçmeye çalışır.

Cyclomatic complexity değeri, bir programdaki bağımsız yol sayısını belirtir. Genellikle, bu ölçüm sayesinde bir programın test edilebilirliği ve bakımı hakkında bilgi edinilebilir. Cyclomatic complexity'nin hesaplanmasında genellikle kontrol akışı grafiği kullanılır. Bu grafik, programdaki farklı kararlar, döngüler ve dallanmalar arasındaki ilişkileri görsel olarak temsil eder.

Cyclomatic complexity yüksek olan bir program, daha karmaşık ve genellikle daha fazla test gerektiren bir yapıya sahip olabilir. Bu nedenle, yazılım geliştirme sürecinde bu metriği kullanarak programların karmaşıklığını değerlendirmek ve optimize etmek önemli bir rol oynar.

In Visual Studio: [Analyze] -> [Calculate Code Metrics] -> [For Solution]

## Her route'da if() kontrolü yapmak yerine bir tane base controller oluşturup onu miras alarak bu davranışı gerçekleştirmek best practice'dir.
### **Not best practice:**

![response_checks](response_checks.png)

### **Response Model Edit:**
- **Kendi iç methodlarımızda kullanacağız IsSuccess gibi. O yüzden [JsonIgnore]**

![status_code_addition](status_code_addition.png)

## Static Factory Methods: Success
- **Default Ok**

![static_factory_success](static_factory_success.png)

## Static Factory Methods: Fail
- **Badrequest 400 default dönmesi: Client error**

![static_factory_fail](static_factory_fail.png)

**Example: New Return Type for Create() Service method:**
- Created gönderiliyor
![create_servicev2](create_servicev2.png)

**Delete ve Update => HttpStatusCode.NoContent**

**Object => Json: Serialization**

**Json => Object: Deserialization**

# BaseController Methods:
ObjectResult: İçerisine ne tip status code parametre olarak verilirse ona bürünür. Response bodysine de data Json'a serialize edilerek eklenir.
ObjectResult içerisine ne verilirse o tipi döner. Generic bir sınıftır. Örneğin, StatusCode olarak 200 verirsek geriye 200 döner.
Aşağıdaki CreateActionResult metodu cyclomatic complexityi düşürmek için yazılmıştır. Mesaj tipine göre response tipini ya da hatalı gelmişse hata mesajını clienta gönderir.

```cs
[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    // GET, PUT, DELETE
    public IActionResult CreateActionResult<T>(ResponseModelDto<T> response)
    {
        if (response.StatusCodes == HttpStatusCode.NoContent)
        {
            return new ObjectResult(null) { StatusCode = 204 }; // PUT, DELETE: Response bodysi boş
        }
        return new ObjectResult(response) { StatusCode = (int)response.StatusCodes }; // GET
    }

    // POST
    public IActionResult CreateActionResult<T>(ResponseModelDto<T> response, string methodName,
        object routeValues)
    {
        if (response.StatusCodes == HttpStatusCode.Created)
        {
            return CreatedAtAction(methodName, routeValues, response); // POST
        }
        return new ObjectResult(response) { StatusCode = (int)response.StatusCodes };
    }
}
```

Cyclomatic Complexityi düşürmek için if sayısını azaltacağız. Bu yüzden **CreateActionResult** içerisinde data yoksa null (içindeki response'ın bodysindeki dataya karşılık gelir) ve StatusCode = 204 (no content) döner. Create dışındaki tüm metodları bu fonksiyon karşılayabilir.
- Bu sayede diğer endpointlerdeki kodumuz aşağıdaki gibi sadeleşebilir:
![controller_new](controller_new.png)
**Hatta böyle tek bir satırda lambda fonksiyonu bile olabilir:**
![lambda_controller](lambda_controller.png)

![http_put_new](http_put_new.png)

- **Controllerdaki Update endpointlerini sadeleştirmek için aşağıdaki gibi basecontroller içerisinde bir CreateActionResult overloadı yazılması gerekir.**
Response'ın headerında oluşturulan dataya erişmek ile ilgili bir data taşınabilir. 201 dışındaki hiçbir durumda bu geçerli değildir. Örneğin; Headers -> Location'da Id bulunabilir.
methodName: "Controller adı", routeValues: "Route'da taşınan değer", response: "Body içindeki mesaj".
- **CustomBaseController'da:**

![created_custombasecontroller](created_custombasecontroller.png)
- **Onu inherit eden Controllerımızda:**

    nameof(GetId) dendiği zaman GetId metodunu çağırır.✅

![created_custombasecontroller2](created_custombasecontroller2.png)

## Kategori Bazlı Klasörleme

![kategori_bazlı_klasörleme](kategori_bazlı_klasörleme.png)


## Function Bazlı Klasörleme
![functional_bazlı_klasörleme](functional_bazlı_klasörleme.png)

# ModelBindingExample
- Alttaki gibi ise requestin bodysinde UserDto kompleks tipi taşınır.
![modelbinding](modelbinding.png)

Class, Method, Property'e eklenen attributeler ile bunların davranışı değiştirilebilir.

**1) [FromBody]:** Complex data types(class, record, struct) için default. (
Üstteki gibi)

**2) [FromHeader]:** Header'dan veri alır.(altta işlendi)

**3) [FromQuery]:** QueryString'ten veri alır. Simple data types(bool, int, string, DateTime) için default.

**4) [FromRoute]:**
![alt text](image.png)

- **Bunun için DTO'ya ve Endpoint'e eklenmesi gerekir.**

![modelbindingcontrollerexampleuserdto](modelbindingcontrollerexampleuserdto.png)

![userdto](userdto.png)

![postmanuserdto](postmanuserdto.png)

### **NOT:** Querystring'te taşınan veri **SSL** sertifikası olsa dahi şifrelenmez.
## Şifrelenen yerler:

**1) Request.Header** : Headera eklenip güvenli taşınabilir

**Simple Data Type:**
![headerda_taşıma](headerda_taşıma.png)
![postman_headercarry](postman_headercarry.png)

**Complex Data Type:**
![postman_complex_datatype](postman_complex_datatype.png)

**2) Request.Body**
Body'den alabilmek için **"Get"** harici isteklerde bulunmalıyız.

**3) Response.Header**

**4) Response.Body**


## Prensipler bir hedef gösterir, patternlar hedefe gidilen yolu gösterir.
# Dependency Injection Containers
- IoC + Dependency Inversion birbirini destekler.
Dependency injection sayesinde soyut konsepte(interface:+++ soyutlama için daha uygun)/abstract class:ortak metod-property tanımlarken) bağımlı olduğumuz için somut bağımlılığımız azalır. Örneğin:
ProductsController, ctor metodunda IProductService'i implemente ederse datanın nereden geldiği önemli olmaz. Bu sayede bağımlılık azalır.
- Repository Design Pattern + Unit of Work birbirini destekler.
- Event Sourcing + CQRS birbirini destekler.

## DI Container Types:
Nesneler framework tarafından initialize edilir/dispose edilir.

1. **AddSingleton:** Uygulama ilk ayağa kalktığında veya uygulamanın herhangi bir ctorunda görüldüğü anda bir nesne ilk defa oluşturulur ve bu çağırmalarda programın ömrü boyunca memorydeki aynı nesne döner. Genellikle helper metodlarda kullanılır. Örn: KDV hesaplayıcı, SEO ile ilgili TR karakterleri temizleyen bir metod. Redis'deki ConnectionMultiplexer singletondur.
2. **AddScoped (*):** Her requeste bir nesne oluşturur, ömrü response dönene kadardır. Controller, Service, Repository, DbContext hep scoped yaşam döngüsüne sahiptir. Aynı request içindeyken aynı servis birden fazla kez çağrılsa bile her seferinde aynı nesne döner.
3. **AddTransient:** Her bir classın ctorunda görüldüğü her anda memorye nesne örneği eklenir(birden çok olabilir). Request response'a döndüğü anda hepsi silinir.

```cs
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<PriceCalculator>();
```
Static yerine Singleton yazmamızın sebebi: İlerde soyutlanabilir hale getirebilmek için. Bu ayrıca testlerinin yazılmasını da kolaylaştırır.


```cs
//baseUrl/api/products
[HttpGet]
public IActionResult GetAll([FromServices] PriceCalculator priceCalculator)
{
    return Ok(_productService.GetAllWithCalculatedTax(priceCalculator));
}
```
**Burada [FromServices] ile Singleton nesne çekilip işlem yapılabiliyor. CTOR'da initialize etmedik çünkü diğer endpointler bu metodu kullanmayabilir. Yani metod initializer kullandık.**
Bu sayede static metod kullanmak yerine bu yapılanmayı kullanabiliyoruz. Controllerda 1 kere çağırmamız yeterli. Singleton olduğu için diğer katmanlarda da bu nesne kullanılır.

