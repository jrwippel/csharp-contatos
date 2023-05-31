using ControleDeContatos.Data;
using ControleDeContatos.Helper;
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

                builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();                
                builder.Services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
                builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
                builder.Services.AddScoped<ISessao, Sessao>();
                builder.Services.AddScoped<IEmail, Email>();


                builder.Services.AddSession(o =>
                {
                    o.Cookie.HttpOnly = true;
                    o.Cookie.IsEssential = true;
                });
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
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}