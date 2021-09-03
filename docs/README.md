# <img src="https://github.com/kitpymes/template-netcore-entities/raw/master/docs/images/logo.png" height="30px"> Kitpymes.Core.Entities

**Entidades base y de sesión, ValueObjects, Shadow Properties y Enumeraciones estaticas**
 
[![Build Status](https://github.com/kitpymes/template-netcore-entities/workflows/Kitpymes.Core.Entities/badge.svg)](https://github.com/kitpymes/template-netcore-entities/actions)
[![NuGet Status](https://img.shields.io/nuget/v/Kitpymes.Core.Entities)](https://www.nuget.org/packages/Kitpymes.Core.Entities/)
[![NuGet Download](https://img.shields.io/nuget/dt/Kitpymes.Core.Entities)](https://www.nuget.org/stats/packages/Kitpymes.Core.Entities?groupby=Version)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://github.com/kitpymes/template-netcore-entities/blob/master/docs/LICENSE.txt)
[![Size Repo](https://img.shields.io/github/repo-size/kitpymes/template-netcore-entities)](https://github.com/kitpymes/template-netcore-entities/)
[![Last Commit](https://img.shields.io/github/last-commit/kitpymes/template-netcore-entities)](https://github.com/kitpymes/template-netcore-entities/)

## 📋 Requerimientos 

* Visual Studio >= 2019

* NET TargetFramework >= net5.0

* Net Core SDK >= 5.0.100

* C# >= 9.0

* Conocimientos sobre Inyección de Dependencias

## 🔧 Instalación 

_Se puede instalar usando el administrador de paquetes Nuget o CLI dotnet._

_Nuget_

```
Install-Package Kitpymes.Core.Entities
```

_CLI dotnet_

```
dotnet add package Kitpymes.Core.Entities
```

## ⌨️ Código

### Entities

```cs
public interface IEntityBase { }
```

```cs
public abstract class EntityBase<TKey> : IEquatable<TKey>, IEntityBase
{
    protected EntityBase(TKey key) {}

    public virtual TKey Id { get; private set; }

    public static bool operator ==(EntityBase<TKey> left, EntityBase<TKey> right) {}

    public static bool operator !=(EntityBase<TKey> left, EntityBase<TKey> right) {}

    public override bool Equals(object? obj) {}

    public bool Equals([AllowNull] TKey other) {}

    public override int GetHashCode() {}
}
```

```cs
public abstract class EntityBaseInt : EntityBase<int>
{
    protected EntityBaseInt(int id)
        : base(id) { }
}
```

```cs
public abstract class EntityBaseLong : EntityBase<long>
{
    protected EntityBaseLong(long id)
        : base(id) { }
}
```

```cs
public abstract class EntityBaseString : EntityBase<string>
{
    protected EntityBaseString() {}

    protected EntityBaseString(string? id) {}
}
```


### Enumerations

```cs
public abstract class EnumerationBase<TEnum, TValue> : INotMapped
        where TEnum : EnumerationBase<TEnum, TValue>
        where TValue : IEquatable<TValue>
    {
        protected EnumerationBase(TValue value, string name, string? shortName = null) {}

        public string Name { get; }

        public string? ShortName { get; }

        public TValue Value { get; }

        public static bool operator ==(EnumerationBase<TEnum, TValue> left, EnumerationBase<TEnum, TValue> right) {}

        public static bool operator !=(EnumerationBase<TEnum, TValue> left, EnumerationBase<TEnum, TValue> right) {}

        public override string ToString() {}

        public static List<TEnum> GetAll() {}

        public static TEnum? ToEnum(TValue value) {}

        public static TEnum? ToEnum(string? name) {}

        public override bool Equals(object? obj) {}

        public bool Equals([AllowNull] TValue value) {}

        public override int GetHashCode() {}
    }
```

```cs
public abstract class EnumerationBaseInt<TEnum> : EnumerationBase<TEnum, int>
    where TEnum : EnumerationBase<TEnum, int>
{
    protected EnumerationBaseInt(int value, string name, string? shortName = null) {}
}
```

```cs
public class CardTypeEnum : EnumerationBaseInt<CardTypeEnum>
{
    public static readonly CardTypeEnum Amex = new CardTypeEnum(1, "American Express", nameof(Amex));

    public static readonly CardTypeEnum Visa = new CardTypeEnum(2, nameof(Visa), nameof(Visa));

    public static readonly CardTypeEnum MasterCard = new CardTypeEnum(3, "Master Card", nameof(MasterCard));

    public bool IsAmex => Name == Amex.Name;

    public bool IsVisa => Name == Visa.Name;

    public bool IsMasterCard => Name == MasterCard.Name;
}
```

```cs
public class GenderTypeEnum : EnumerationBaseInt<GenderTypeEnum>
{
    public static readonly GenderTypeEnum Male = new GenderTypeEnum(1, nameof(Male), "M");

    public static readonly GenderTypeEnum Female = new GenderTypeEnum(2, nameof(Female), "F");

    public bool IsMale => Name == Male.Name;

    public bool IsFemale => Name == Female.Name;
}
```

```cs
public class LogTypeEnum : EnumerationBaseInt<LogTypeEnum>
{
    public static readonly LogTypeEnum None = new LogTypeEnum(1, nameof(None));

    public static readonly LogTypeEnum Created = new LogTypeEnum(2, nameof(Created));

    public static readonly LogTypeEnum Updated = new LogTypeEnum(3, nameof(Updated));

    public static readonly LogTypeEnum Deleted = new LogTypeEnum(4, nameof(Deleted));

    public static readonly LogTypeEnum Changed = new LogTypeEnum(5, nameof(Changed));
}
```

```cs
public class StatusEnum : EnumerationBaseInt<StatusEnum>
{
    public static readonly StatusEnum Inactive = new StatusEnum(0, nameof(Inactive));

    public static readonly StatusEnum Active = new StatusEnum(1, nameof(Active));

    public bool IsActive => Name == Active.Name;

    public bool IsInactive => Name == Inactive.Name;

    public static StatusEnum ToEnum(bool status) {}

    public static bool ToBool(StatusEnum status) {}

    public static bool ToBool(string? status) {}
}
```

```cs
public class SubscriptionEnum : EnumerationBaseInt<SubscriptionEnum>
{
    public static readonly SubscriptionEnum Free = new SubscriptionEnum(1, nameof(Free));

    public static readonly SubscriptionEnum Silver = new SubscriptionEnum(2, nameof(Silver));

    public static readonly SubscriptionEnum Gold = new SubscriptionEnum(3, nameof(Gold));

    public bool IsFree => Name == Free.Name;

    public bool IsSilver => Name == Silver.Name;

    public bool IsGold => Name == Gold.Name;
}
```

### Session

```cs
public static class AppSession
{
    public static string? Key { get; set; }

    public static TenantSession? Tenant { get; set; }

    public static UserSession? User { get; set; }

    public static bool? IsDevelopment { get; set; }
}
```

```cs
public class TenantSession
{
    public bool? Enabled { get; set; }

    public string? Id { get; set; }

    public string? Subdomain { get; set; }
}
```

```cs
public class UserSession
{
    public string? Id { get; set; }

    public string? Email { get; set; }

    public string? Role { get; set; }

    public IEnumerable<string>? Permissions { get; set; }
}
```

### ShadowProperties

```cs
public interface ICreationAudited
{
    public const string CreatedDate = nameof(CreatedDate);

    public const string CreatedUserId = nameof(CreatedUserId);
}
```

```cs
public interface IDeletionAudited
{
    public const string DeletedDate = nameof(DeletedDate);

    public const string DeletedUserId = nameof(DeletedUserId);
}
```

```cs
public interface IModificationAudited
{
    public const string ModifiedDate = nameof(ModifiedDate);

    public const string ModifiedUserId = nameof(ModifiedUserId);
}
```

```cs
public interface IEntityAudited : ICreationAudited, IModificationAudited {}
```

```cs
public interface IEntityFullAudited : ICreationAudited, IModificationAudited, IDeletionAudited { }
```

```cs
public interface INotMapped
{
    public const string NotMapped = nameof(NotMapped);
}
```

```cs
public interface IRowVersion
{
    public const string IsRowVersion = nameof(IsRowVersion);
}
```

```cs
public interface IStatus
{
    public const string Status = nameof(Status);
}
```

```cs
public interface ITenant
{
    public const string TenantId = nameof(TenantId);
}
```

```cs
public interface IDefault
{
    public const string IsDefault = nameof(IsDefault);
}
```

### ValueObjects

```cs
public abstract class ValueObjectBase : INotMapped
{
    public static bool operator ==(ValueObjectBase left, ValueObjectBase right) {}

    public static bool operator !=(ValueObjectBase left, ValueObjectBase right) {}

    public override bool Equals(object? obj) {}

    public override int GetHashCode() {}

    protected abstract IEnumerable<object?> GetEqualityComponents();
}
```

```cs
public sealed class Address : ValueObjectBase
{
    public static Address Null { get;}

    public bool IsNull { get;}

    public string? Street { get; }

    public int? Number { get; }

    public string? PostalCode { get; }

    public string? City { get; }

    public string? State { get; }

    public string? Country { get; }

    [return: NotNull]
    public static Address Create(string street, int number, string postalCode, string city, string state, string country) {}

    [return: NotNull]
    public Address ChangeStreet(string? street) {}

    [return: NotNull]
    public Address ChangeNumber(int number) {}

    [return: NotNull]
    public Address ChangePostalCode(string? postalCode) {}

    [return: NotNull]
    public Address ChangeCity(string? city) {}

    [return: NotNull]
    public Address ChangeState(string? state) {}

    [return: NotNull]
    public Address ChangeCountry(string? country) {}

    public override string ToString() {}
}
```

```cs
public sealed class Currency : ValueObjectBase
{
    public enum CodeName
    {
        AED = 784,
        AFN = 971,
        ALL = 008,
        AMD = 051,
        ANG = 532,
        AOA = 973,
        ARS = 032,
        AUD = 036,
        AWG = 533,
        AZN = 944,
        BAM = 977,
        BBD = 052,
        BDT = 050,
        BGN = 975,
        BHD = 048,
        BIF = 108,
        BMD = 060,
        BND = 096,
        BOB = 068,
        BOV = 984,
        BRL = 986,
        BSD = 044,
        BTN = 064,
        BWP = 072,
        BYR = 974,
        BZD = 084,
        CAD = 124,
        CDF = 976,
        CHE = 947,
        CHF = 756,
        CHW = 948,
        CLF = 990,
        CLP = 152,
        CNY = 156,
        COP = 170,
        COU = 970,
        CRC = 188,
        CUC = 931,
        CUP = 192,
        CVE = 132,
        CZK = 203,
        DJF = 262,
        DKK = 208,
        DOP = 214,
        DZD = 012,
        EGP = 818,
        ERN = 232,
        ETB = 230,
        EUR = 978,
        FJD = 242,
        FKP = 238,
        GBP = 826,
        GEL = 981,
        GHS = 936,
        GIP = 292,
        GMD = 270,
        GNF = 324,
        GTQ = 320,
        GYD = 328,
        HKD = 344,
        HNL = 340,
        HRK = 191,
        HTG = 332,
        HUF = 348,
        IDR = 360,
        ILS = 376,
        INR = 356,
        IQD = 368,
        IRR = 364,
        ISK = 352,
        JMD = 388,
        JOD = 400,
        JPY = 392,
        KES = 404,
        KGS = 417,
        KHR = 116,
        KMF = 174,
        KPW = 408,
        KRW = 410,
        KWD = 414,
        KYD = 136,
        KZT = 398,
        LAK = 418,
        LBP = 422,
        LKR = 144,
        LRD = 430,
        LSL = 426,
        LYD = 434,
        MAD = 504,
        MDL = 498,
        MGA = 969,
        MKD = 807,
        MMK = 104,
        MNT = 496,
        MOP = 446,
        MRO = 478,
        MUR = 480,
        MVR = 462,
        MWK = 454,
        MXN = 484,
        MXV = 979,
        MYR = 458,
        MZN = 943,
        NAD = 516,
        NGN = 566,
        NIO = 558,
        NOK = 578,
        NPR = 524,
        NZD = 554,
        OMR = 512,
        PAB = 590,
        PEN = 604,
        PGK = 598,
        PHP = 608,
        PKR = 586,
        PLN = 985,
        PYG = 600,
        QAR = 634,
        RON = 946,
        RSD = 941,
        RUB = 643,
        RWF = 646,
        SAR = 682,
        SBD = 090,
        SCR = 690,
        SDG = 938,
        SEK = 752,
        SGD = 702,
        SHP = 654,
        SLL = 694,
        SOS = 706,
        SRD = 968,
        SSP = 728,
        STD = 678,
        SVC = 222,
        SYP = 760,
        SZL = 748,
        THB = 764,
        TJS = 972,
        TMT = 934,
        TND = 788,
        TOP = 776,
        TRY = 949,
        TTD = 780,
        TWD = 901,
        TZS = 834,
        UAH = 980,
        UGX = 800,
        USD = 840,
        USN = 997,
        UYI = 940,
        UYU = 858,
        UZS = 860,
        VEF = 937,
        VND = 704,
        VUV = 548,
        WST = 882,
        XAF = 950,
        XAG = 961,
        XAU = 959,
        XBA = 955,
        XBB = 956,
        XBC = 957,
        XBD = 958,
        XCD = 951,
        XDR = 960,
        XOF = 952,
        XPD = 964,
        XPF = 953,
        XPT = 962,
        XSU = 994,
        XTS = 963,
        XUA = 965,
        XXX = 999,
        YER = 886,
        ZAR = 710,
        ZMW = 967,
        ZWL = 932
    }

    public static Currency Default => new Currency();

    public string? Symbol { get; }

    public string? Code { get; }

    public string? Name { get; }

    [return: NotNull]
    public static Currency Create(CodeName code) => new Currency(code);

    [return: NotNull]
    public Currency ChangeCurrency(CodeName code) {}

    public override string ToString() {}
}
```

```cs
public sealed class Email : ValueObjectBase
{
    public static Email Null => new Email();

    public bool IsNull => Value.ToIsNullOrEmpty();

    public string? Value { get; }

    [return: NotNull]
    public static Email Create(string? email) => new Email(email);

    public void Change(string? email) => Value = email.ToIsEmailThrow(nameof(email));

    public override string ToString() {}

    public string? ToNormalize() {}
}
```

```cs
public sealed class FullName : ValueObjectBase
{
    public static FullName Null => new FullName();

    public bool IsNull => FirstName.ToIsNullOrEmpty();

    public string? FirstName { get; }

    public string? MiddleName { get; }

    public string? LastName { get; }

    [return: NotNull]
    public static FullName Create(string? firstName, string lastName, string? middleName = null) {}

    [return: NotNull]
    public FullName ChangeFirstName(string? firstName) {}

    [return: NotNull]
    public FullName ChangeMiddleName(string? middleName) {}

    [return: NotNull]
    public FullName ChangeLastName(string? lastName) {}

    public override string ToString() {}

    public string ToFullString() {}
}
```

```cs
public sealed class Money : ValueObjectBase
{
    public static Money Default => new Money();

    public decimal Amount { get; }

    public Currency? Currency { get; }

    public int? NumbeOfDecimals { get; }

    [return: NotNull]
    public static Money Create(decimal amount, Currency currency, int numbeOfDecimals = DefaultNumbeOfDecimals) {}

    [return: NotNull]
    public Money ChangeAmount(decimal amount) {}

    [return: NotNull]
    public Money ChangeCurrency(Currency currency) {}

    [return: NotNull]
    public Money ChangeNumbeOfDecimals(int numbeOfDecimals) {}

    public override string ToString() {}
}
```

```cs
public sealed class Name : ValueObjectBase
{
    public static Name Null { get; }

    public bool IsNull { get; }

    public string? Value { get; }

    [return: NotNull]
    public static Name Create(string? name) {}

    public void Change(string? name) {}

    public override string ToString() {}

    public string? ToNormalize() {}
}
```

```cs
public sealed class StringId : ValueObjectBase
{
    public static StringId Null  { get; }

    public bool IsNull  { get; }

    public string? Value { get; }

    [return: NotNull]
    public static StringId Create() {}

    [return: NotNull]
    public static StringId Create(string? id) {}

    public override string ToString() {}
}
```

```cs
public sealed class Subdomain : ValueObjectBase
{
    public static Subdomain Null { get; }

    public bool IsNull { get; }

    public string? Value { get; }

    [return: NotNull]
    public static Subdomain Create(string? subdomain) {}

    public void Change(string? subdomain) {}

    public override string ToString() {}
}
```

```cs
public sealed class Telephone : ValueObjectBase
{
    public static Telephone Null { get; }

    public bool IsNull { get; }

    public string? Prefix { get; }

    public long? Number { get; }

    [return: NotNull]
    public static Telephone Create(string? prefix, long number) {}

    [return: NotNull]
    public Telephone ChangePrefix(string? prefix) {}

    [return: NotNull]
    public Telephone ChangeNumber(long number) {}

    public override string ToString() {}
}
```

```cs
public sealed class TenantName : ValueObjectBase
{
    public static TenantName Null { get; }

    public bool IsNull { get; }

    public string? Value { get; }

    [return: NotNull]
    public static TenantName Create(string? name) {}

    public void Change(string? name) {}

    public override string ToString() {}
}
```

## ⚙️ Pruebas Unitarias

_Cada proyecto tiene su respectivo test, se ejecutan desde el "Explorador de pruebas"_

![Tests](images/screenshot/pruebas_unitarias.png)


## 🛠️ Construido con 

* [NET Core](https://dotnet.microsoft.com/download) - Framework de trabajo
* [C#](https://docs.microsoft.com/es-es/dotnet/csharp/) - Lenguaje de programación
* [Inserción de dependencias](https://docs.microsoft.com/es-es/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0) - Patrón de diseño de software
* [MSTest](https://docs.microsoft.com/es-es/dotnet/core/testing/unit-testing-with-mstest) - Pruebas unitarias
* [Nuget](https://www.nuget.org/) - Manejador de dependencias
* [Visual Studio](https://visualstudio.microsoft.com/) - Entorno de programacion


## ✒️ Autores 

* **Sebastian R Ferrari** - *Trabajo Inicial* - [kitpymes](https://kitpymes.com)


## 📄 Licencia 

* Este proyecto está bajo la Licencia [LICENSE](LICENSE.txt)


## 🎁 Gratitud 

* Este proyecto fue diseñado para compartir, creemos que es la mejor forma de ayudar 📢
* Cada persona que contribuya sera invitada a tomar una 🍺 
* Gracias a todos! 🤓

---
[Kitpymes](https://github.com/kitpymes) 😊