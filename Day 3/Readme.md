[Github Repo:](https://github.com/Fcakiroglu16/NetBootcamp.git) Branch lesson/3day

[Routing Constraints](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-8.0)

[Model Validation](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-8.0)
NOT:
```cs
    public record ProductCreateRequestDto([Required]string Name, decimal Price);
```
**Önemli Not:** String gibi referans tipli değişkenler için [Required] keywordü kullanılabilir ancak decimal value type bir değişkendir, default değer atar. Bunu aşmak için decimal? yapıp **nullable** hale getirmemiz gerekir. Ya da Range validation ile değer beklemeliyiz.
```cs
    /// 1. Required
    public record ProductCreateRequestDto([Required]string Name, [Required] decimal? Price);
    /// 2. Range
    public record ProductCreateRequestDto([Required]string Name, [Range(1,Int32.MaxValue)] decimal Price);
    /// 3. ErrorMessage
    public record ProductCreateRequestDto([Required(ErrorMessage = "Ürün adı bulunamadı.")]string Name, [Required] decimal? Price);
```


### Entitylerde asla UI validasyonu yapılmayacak! DTO'lar için yazılabilir(basit controllerlar için)