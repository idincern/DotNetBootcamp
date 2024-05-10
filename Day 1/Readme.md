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
MinimalAPI ve FastEndpoint libraryler > APIs

### API'lerin günümüzdeki modern teknolojilerde geliştirilmesi elzemdir.

