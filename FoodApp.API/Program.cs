using AutoMapper;
using DotNetEnv;
using FoodApp.API.Middlewares;
using FoodApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FoodApp.Application.Common.Mappings;
using FoodApp.Application.Common.Helpers;
using Autofac.Core;
using FoodApp.Domain.Interface.Base;
using FoodApp.Infrastructure.Repositories.Base;
using FoodApp.Application.CQRS.Users.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace FoodApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>();

            builder.Services.AddControllers();
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

            builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            builder.Services.AddAutoMapper(typeof(UserProfile),typeof(RecipeProfile));
           
          
          
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
