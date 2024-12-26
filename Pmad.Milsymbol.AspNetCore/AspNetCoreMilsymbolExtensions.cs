using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Pmad.Milsymbol.AspNetCore
{
    public static class AspNetCoreMilsymbolExtensions
    {
        private static Assembly MilsymbolAspNetCoreAssembly 
            => typeof(AspNetCoreMilsymbolExtensions).Assembly;

        public static ManifestEmbeddedFileProvider MilsymbolStaticFiles
            => new ManifestEmbeddedFileProvider(MilsymbolAspNetCoreAssembly, "wwwroot");

        public static void AddMilsymbolMvcComponents(this IServiceCollection services)
        {
            services.AddMvc().ConfigureApplicationPartManager(apm =>
            {
                // Allows Views to be found in the Pmad.Milsymbol.AspNetCore assembly
                apm.ApplicationParts.Add(new CompiledRazorAssemblyPart(MilsymbolAspNetCoreAssembly));
            });
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
