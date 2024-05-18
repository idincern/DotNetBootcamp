# Middlewares

Her request geldiğinde ve her responsea dönüşürken arada çalışan processler. Hepsinin adı **"Use"** ile başlar

**Filterlardan farkı:**
- Filterlar controller'daki metodlarda çalışır, Middleware'ler her requestte çalışır.
- Filterlar endpointlerdeki metodlarda çalışır, Middleware'ler endpointe gelmeden/geldikten sonra çalışabilir.
- Bir requestin response'a dönme süresini middlewareler ile bulunabilir. Filterlar bazı businesslerde çalışan bazı businesslerda çalışmayan metodlar için kullanılır.

![middlewares](middlewares.png)
- UseExceptionHandler: Request response'a dönmeden hemen önce - Middleware stack'in en başına konur ki DTO'lar düzgünce gönderilebilsin.
- HSTS: UI ile ilgili middleware
- UseHttpsRedirection: http ile gele istekleri https'e çevirir.
- UseAuthorization: Filterlar ile aktif hale gelir [Authorize] attribute'u ile devreye girer.

## **Logging Levels:**
1) Trace
2) Debug
3) Information
4) Warning
5) Error - methodlara özgü
6) Critical - uygulamaya özgü system down(db size full, memory full)

![logging_levels](logging_levels.png)

**Serilog(NLog'dan daha iyi):**
Tüm logları(bilinçli atılan ve sistemin attığı) DB'ye kaydederler. Singleton olarak yaşam döngüleri vardır.

Global Exception Handler Middleware:
(Swagger'dan hemen sonra)
