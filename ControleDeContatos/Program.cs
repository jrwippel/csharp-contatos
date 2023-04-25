using ControleDeContatos.Data;
using ControleDeContatos.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = String.Empty;
            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
                connection = builder.Configuration.GetConnectionString("Database");
                builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            }
            else
            {
                connection = Environment.GetEnvironmentVariable("Database");
            }       

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<BancoContext>(o => o.UseSqlServer(connection));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}