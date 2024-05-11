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

- Fast fail and Guard Clauses => önce olumsuz kısmı if ile kontrol et sonrasında doğru kısma geç işlemlere devam et.

### FluentValidation => .NET ile ilgili tüm projelerde kullanılmalı.
```cs
RuleFor(x=>x.Price).InclusiveBetween(1,1000).WithMessage("Fiyat 1 ile 1000 arasında olabilir");
```

```cs
RuleFor(x=>x.IdentityNo).Length(11).WithMessage("TC no 11 haneli olmalıdır").Must(CheckIdentityNo).WithMessage("TC numarası hatalıdır");

### Delegates
- Action => void
- Predicate => bool
- Func => dynamic

public bool CheckIdentityNo(string idedntityNo)
{
    /// ......

    return true;
}
```

### Not: Basit validationlar için FluentValidation kullanabiliriz. Karmaşık işlemler için servislerin içinde validation yapabiliriz.

### Tüm .NET Winforms ve WPF gibi projelerinde DIContainer olarak **Autofac** kullan 3rd party kütüphane.

### ADO.NET(old) => **EF Core** 👍
EF Core, Uygulamaları veritabanından bağımsız hale getirir.
```cs
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolDB;Trusted_Connection=True;");
    }
}
// OnConfiguring method is used to configure the database provider (e.g., SQL Server, SQLite, PostgreSQL) that EF Core will use to connect to your database. This method is also typically overridden in your DbContext class and is called by EF Core when the context is being configured.
```
```cs
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Student>()
        .HasKey(s => s.StudentId);

    modelBuilder.Entity<Student>()
        .Property(s => s.Name)
        .IsRequired();

    modelBuilder.Entity<Course>()
        .HasMany(c => c.Students)
        .WithMany(s => s.Courses)
        .UsingEntity(j => j.ToTable("StudentCourse"));
}
// OnModelCreating method is used to configure the model that EF Core will use to create the database schema. This method is typically overridden in your DbContext class to define the shape of your database entities, their relationships, and various constraints.
```

ef core tools nuget paketi yüklendikten sonra
add-migration [migration-name]

.... 4 komut ile entitymiz MSSQL'e yükleniyor.
Yapılan her değişiklik için migration eklenir.
Geri almak için
### **Dapper** for mini ORM 👍
Dapper, ham SQL cümleciğinden aldığı nesneyi otomatik olarak bizim classlarımıza mapler. Performanslıdır, join'in çok olduğu yerde kullanılabilir(EF Core memory kullanarak bunu yapar daha yavaştır). Bu yüzden hızın önemli olduğu yerde Dapper kullanılmalı.