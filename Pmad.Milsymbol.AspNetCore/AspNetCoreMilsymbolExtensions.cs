using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Pmad.Milsymbol.AspNetCore.Services;

namespace Pmad.Milsymbol.AspNetCore
{
    public static class AspNetCoreMilsymbolExtensions
    {
        private static Assembly MilsymbolAspNetCoreAssembly 
            => typeof(AspNetCoreMilsymbolExtensions).Assembly;

        public static ManifestEmbeddedFileProvider MilsymbolStaticFiles
            => new ManifestEmbeddedFileProvider(MilsymbolAspNetCoreAssembly, "wwwroot");

        public static void AddMilsymbolGenerator(this IServiceCollection services)
        {
            services.TryAddSingleton<IApp6dSymbolGenerator, SharedApp6dSymbolGenerator>();
        }

        public static IMvcBuilder AddMilsymbolMvcComponents(this IMvcBuilder builder, int boostrapVersion = 5)
        {
            builder.Services.AddLocalization();
            builder.Services.AddMilsymbolGenerator();
            builder.ConfigureApplicationPartManager(apm =>
            {
                // Allows Views to be found in the Pmad.Milsymbol.AspNetCore assembly
                apm.ApplicationParts.Add(new CompiledRazorAssemblyPart(MilsymbolAspNetCoreAssembly));
            });
            builder.AddViewLocalization();
            if (boostrapVersion == 5)
            {
                builder.Services.TryAddSingleton<IDesignSystemClasses, Boostrap5>();
            }
            else
            {
                builder.Services.TryAddSingleton<IDesignSystemClasses, Boostrap4>();
            }
            return builder;
        }

        public static void UseMilsymbolStaticFiles(this IApplicationBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = MilsymbolStaticFiles
            });
        }
    }
}
