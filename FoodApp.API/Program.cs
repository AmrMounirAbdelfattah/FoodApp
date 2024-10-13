using AutoMapper;
using DotNetEnv;
using FoodApp.API.Middlewares;
using FoodApp.Application.Common.Constants;
using FoodApp.Application.Common.Helpers;
using FoodApp.Application.Common.Helpers.CloudinaryHelper;
using FoodApp.Application.Common.Helpers.ImageHelper;
using FoodApp.Application.Common.Mappings;
using FoodApp.Application.CQRS.Users.Commands;
using FoodApp.Domain.Interface.Base;
using FoodApp.Infrastructure.Data;
using FoodApp.Infrastructure.Repositories.Base;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Text;

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
           // builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Food App Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. " +
                                    "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                                    "\r\n\r\nExample: \"Bearer abcdefghijklmnopqrstuvwxyz\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
            });

            builder.Services.AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = JwtSettings.Issuer,
                    ValidAudience = JwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero, // Reduce the default clock skew (allowable token time discrepancy)
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.SecretKey))
                };
            });

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
            builder.Services.AddAutoMapper(typeof(UserProfile), typeof(RecipeProfile), typeof(CategoryProfile), typeof(RecipeImagesProfile), typeof(RatingProfile));
            builder.Services.AddScoped<IFluentEmailService, FluentEmailService>();
            builder.Services.AddSingleton<ICloudinaryService, CloudinaryService>();
            builder.Services.AddSingleton<IImageService, ImageService>();

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
