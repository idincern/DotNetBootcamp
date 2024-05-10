- SDK: Software Development Kit => Geliştirme için kullanılan ortam.
- Runtime: SDK ile geliştirilen uygulamaları çalıştırmamızı sağlayan ortam. Servera deploy edilirken serverda runtime kurulu olması yeterlidir.

- Önemli olan neyin nerede kullanılacağını bilmek. Örneğin Unit Testin nereye yazılmasını bilmek önemli, nasıl yazıldığını ezberlemek değil.

- Model-View-Controller(MVC) Design Pattern
Günümüzde web ve API tarafında çok yaygın kullanılır. Ancak daha modüler, performanslı ve sürdürülebilir projeler için bu patternı terk edeceğiz.

- RESTFul servisler HTTP protokolüyle çalışan bir mimari yaklaşımdır(Protokol = Kurallar bütünü). Bu sayede browser-server haberleşmesi gerçekleştirilebiliyor(ortak dil) ve web uygulamaları geliştirebiliyoruz. gRPC gibi bir mimari olsaydı browserlardan istek atılamazdı çünkü tarayıcılar bu protololü bilmiyor. REST mimarisinde AJAX üzerinden/SwaggerUI üzerinden endpointlerimize istek atıp haberleşebiliyoruz.

**Controller:** Arabulucudur, hiçbir zaman business ile ilgili bir görevi olmamalı. Request alır, Modele ya da View'e iletir ve geriye response döner. Unit testleri, integration testleri olmaz. Sadece end to end testleri vardır.

**Model:** Business ve Veritabanı ile ilgili işlemlerin yapıldığı yer.
- Repository(R) Katmanı = Data Access Layer(DAL) => DB işlemleri
- Service(S) Katmanı = Business Logic(BL) => Operasyon/metod/algoritma
Business tam manasıyla Database(repository)den aldığı ham veri üzerinde işlem yapan ve döndürendir.

**View:** HTML, CSS, JS - bu kursun içeriğine dahil değil.

### Araştır:
- MinimalAPI ve FastEndpoint libraryler > Must learn APIs
- Bir uygulamayı hızlandırmanın en iyi yöntemlerinden birisi:
    1) **Redis**
    2) Optimize etmektir
    3) Veritabanınde **index** yapısı kurulmalı.
Her API ve web uygulaması cachelenmeye uygundur.
- Önce Kestrel daha sonra nginx server

### API'lerin günümüzdeki modern teknolojilerde geliştirilmesi elzemdir.

- Türkiye'de bir web projesi canlıya alınırken hosting firmasından 1. API için 2. Web Uygulaması için ayrı ayrı hosting alınıyor. Web uygulaması API ile haberleşir. Ayrıca tek bir VM ile istenilen sayıda uygulama dış dünyaya açılabilir.

# ASP.NET Core Web API Projesi
Authentication, authorization, logging, vs hepsi merkezde toplanır ve güvenlik artar(Cros-cutting concern)
![CrossCuttingConcern](ccc.png)

- Connected Services: DB'ler için hazır servisler bulunur.
- Dependencies:
    - Analyzers: .NET core ile roslyn derleyicisine geçildi. Yazılan kodlarda statik olarak analiz yapılabiliyor ve compiler uyarı yapıyor. Bu kurallar Analyzers'ta bulunur.
    - Frameworks: Paket sepeti => içerisinde birçok paket bulunuyor. Sadece console app ise .NETCore.App, API ile ilgili paketler gelince AspNetCore.App eklenir.
    - Packages: Projeye eklenen paketler(eski adı library). **Swagger OpenAPI Integration** => Endpointlerimizin hazır dokümentasyonunu oluşturmayı sağlar. Ortak bir kural seti oluşturur. Java, Python ve .Net için de aynı sonuç gösterilir. Projedeki Packages içindeki Swashbuckle.AspNetCore paketi tarafından UI oluşturulur. Ekiplerarası testi kolaylaştırıyor.
- Properties
    - launchSettings.json: Development ortamı ile ilgili ayarları içeren dosya. .yaml dosyası olarak kullanılması daha okunaklı yapar(gelecekte geçilecek gibi duruyor). *Kestrel* sunucusu ile cross-platform, *IIS* ile sadece Windows serverlarda çalışır(eski). .NET Core ile Kestrel otomatik olarak gelir. Kubernetes, **Docker** hep Linux sunucu(bedava, güvenlik konfigürasyonları, paket desteği). https => Server-Client veri iletiminde SSL sertifikaları ile güvenliği sağlar. Canlıda 443 portunu kullanır. http'de 80 portu kullanılır(resmi olarak belirlenmiştir).

