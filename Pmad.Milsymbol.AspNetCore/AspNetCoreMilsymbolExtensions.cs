using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        public static IMvcBuilder AddMilsymbolMvcComponents(this IMvcBuilder builder, DesignSystem designSystem = DesignSystem.Automatic)
        {
            builder.Services.AddLocalization();
            builder.Services.AddMilsymbolGenerator();
            builder.Services.AddDesignSystem(designSystem);

            builder.ConfigureApplicationPartManager(apm =>
            {
                // Allows Views to be found in the Pmad.Milsymbol.AspNetCore assembly
                apm.ApplicationParts.Add(new CompiledRazorAssemblyPart(MilsymbolAspNetCoreAssembly));
            });
            builder.AddViewLocalization();

            return builder;
        }

        /// <summary>
        /// Setup design system for Pmad components
        /// </summary>
        /// <param name="services"></param>
        /// <param name="designSystem"></param>
        public static void AddDesignSystem(this IServiceCollection services, DesignSystem designSystem)
        {
            if (designSystem == DesignSystem.Automatic)
            {
                services.TryAddSingleton<IDesignSystem>(provider =>
                    DetectDesignSystsem(provider.GetRequiredService<IWebHostEnvironment>()));
            }
            else if (designSystem == DesignSystem.Bootstrap5)
            {
                services.TryAddSingleton<IDesignSystem, Bootstrap5DesignSystem>();
            }
            else
            {
                services.TryAddSingleton<IDesignSystem, Bootstrap4DesignSystem>();
            }
        }

        private static IDesignSystem DetectDesignSystsem(IWebHostEnvironment host)
        {
            var file = host.WebRootFileProvider.GetFileInfo("lib/bootstrap/dist/css/bootstrap.css");
            if (file.Exists && !file.IsDirectory)
            {
                using var stream = file.CreateReadStream();
                var content = new StreamReader(stream).ReadToEnd();
                if (content.Contains("Bootstrap v5"))
                {
                    return new Bootstrap5DesignSystem();
                }
                if (content.Contains("Bootstrap v4"))
                {
                    return new Bootstrap4DesignSystem();
                }
            }
            return new Bootstrap5DesignSystem();
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
