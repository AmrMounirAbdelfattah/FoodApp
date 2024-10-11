using AutoMapper;
using DotNetEnv;
using FoodApp.API.Middlewares;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.Mappings;
using FoodApp.Application.CQRS.Users.Commands;
using FoodApp.Domain.Interface.Base;
using FoodApp.Infrastructure.Data;
using FoodApp.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace FoodApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            //builder.Services.AddDbContext<AppDbContext>();

            var connectionString = builder.Configuration.GetConnectionString("ConnectionStrings");

            // Configure DbContext with the connection string
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Enviroment
            Env.Load();
            //AUTOFAC
            //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            //builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            //    builder.RegisterModule(new AutofacModule()));
            //MEDIATR

            //builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
            //builder.Services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RegisterUserCommandHandler).Assembly));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddAutoMapper(typeof(UserProfile), typeof(RecipeProfile));
            builder.Services.AddScoped<IFluentEmailService, FluentEmailService>();

            var port = builder.Configuration.GetSection("EmailCredentials").GetValue<int>("Port");
            var from = builder.Configuration.GetSection("EmailCredentials").GetValue<string>("From");
            var password = builder.Configuration.GetSection("EmailCredentials").GetValue<string>("Password");
            var host = builder.Configuration.GetSection("EmailCredentials").GetValue<string>("Host");

            var sender = new SmtpClient
            {
                Host = host,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true,
                Port = port
            };
            builder.Services.AddFluentEmail(from, "Upskilling").AddSmtpSender(sender);

            var app = builder.Build();
            //AUTOMAPPER
            MapperHelper.Mapper = app.Services.GetService<IMapper>();
            //Middlewares
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
