using System;
using EnglishLearning.Dictionary.Client;
using EnglishLearning.Utilities.Identity.DelegationHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.TaskService.Host.Configuration
{
    public static class HttpClientSettings
    {
        public static IServiceCollection AddEnglishLearningHttp(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var dictionaryAddress = configuration
                .GetValue<Uri>("ExternalServices:Dictionary");

            services
                .AddHttpClient<WordMetadataClient>(c =>
                {
                    c.BaseAddress = dictionaryAddress;
                });

            return services;
        }
    }
}