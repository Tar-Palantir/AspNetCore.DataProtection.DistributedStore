using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AspNetCore.DataProtection.DistributedStore
{
    public static class PersistKeysToDistributedStoreExtensions
    {
        public static IDataProtectionBuilder PersistKeysToDistributedStore(
            this IDataProtectionBuilder builder, string key = "DataProtection-Keys")
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            
            builder.Services.AddSingleton(
                services =>
                {
                    var cache = services.GetService<IDistributedCache>();
                    return (IConfigureOptions<KeyManagementOptions>) new ConfigureOptions<KeyManagementOptions>(
                        options =>
                            options.XmlRepository =
                                (IXmlRepository) new DistributedStoreXmlRepository(cache, key));
                });
            return builder;
        }
    }
}