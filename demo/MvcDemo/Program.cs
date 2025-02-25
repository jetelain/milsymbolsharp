using MvcDemoBS5;
using Pmad.Milsymbol.AspNetCore;
using Pmad.Milsymbol.AspNetCore.SymbolSelector.Bookmarks;

namespace MvcDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services
                .AddControllersWithViews()
                    .AddMilsymbolMvcComponents();

            builder.Services.AddScoped<ISymbolBookmarksService, SampleSymbolBookmarksService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMilsymbolStaticFiles();
            app.UseRequestLocalization(options => options.AddSupportedUICultures("en"));

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
