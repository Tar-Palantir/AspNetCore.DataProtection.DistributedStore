# AspNetCore.DataProtection.DistributedStore

[![GitHub](https://img.shields.io/github/license/Tar-Palantir/AspNetCore.DataProtection.DistributedStore)](https://github.com/Tar-Palantir/AspNetCore.DataProtection.DistributedStore/blob/master/LICENSE)
![build](https://github.com/Tar-Palantir/AspNetCore.DataProtection.DistributedStore/workflows/build/badge.svg)
![publish](https://github.com/Tar-Palantir/AspNetCore.DataProtection.DistributedStore/workflows/publish/badge.svg)
[![AspNetCore.DataProtection.DistributedStore](https://img.shields.io/nuget/vpre/AspNetCore.DataProtection.DistributedStore.svg)](https://www.nuget.org/packages/AspNetCore.DataProtection.DistributedStore/)

## Install

* Package Manager

```sh
PM> Install-Package AspNetCore.DataProtection.DistributedStore -Version 1.0.0
```

* .NET CLI

```sh
dotnet add package AspNetCore.DataProtection.DistributedStore --version 1.0.0
```

## How to use

Open Startup.cs

Configure DistributedStore in ConfigureServices:

```csharp
    //you can use MemoryCache
    //services.AddMemoryCache();
    
    //or use RedisCache
    services.AddDistributedRedisCache(options =>
    {
        options.Configuration = Configuration["Redis:ConnectionString"]; //redis连接字符串
        options.InstanceName = Configuration["Redis:Instance"]; //Redis实例名称
    });
```

Then configure DataProtection use PersistKeysToDistributedStore

```csharp
    services.AddDataProtection(options => options.ApplicationDiscriminator = "SSO")
        .PersistKeysToDistributedStore();
}
```
