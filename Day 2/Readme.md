**Best Practice Örneği**
- Modeldeki Servis katmanında Repository katmanından alınan veriler bir ImmutableList içerisine record constructorunda deep copy yapılarak Controller katmanından Repository katmanına erişim kısıtlanır. Bu sayede DB'deki veri bütünlüğü korunmuş olur ve client sidedan gelebilecek veri tehditlerine karşı koruma olur. Controller'dan bu ImmutableList'i alarak istemcilere sunmak, veri bütünlüğünü korumaya yardımcı olur. İstemciler, bu liste üzerinde sadece okuma işlemleri yapabilir ve verilerin değiştirilmesi sadece uygulama içinde tanımlanan yollarla mümkün olur.
- Side effect'i olmayan metod örneği:
```cs
private decimal CalculateTax(decimal price, decimal tax) => (price * tax); // Bu sayede tax değeri her fonksiyonda tekrardan gönderilir. Statik/global değişkende oluşacak tax değişikliği metod çalışma mantığını etkilemez
```