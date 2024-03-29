﻿using JarvisNG.Data;
using JarvisNG.Data.Repositories;
using JarvisNG.Models.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.SwaggerGeneration.Processors.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using JarvisNG.Data;
//using JarvisNG.Data.Repositories;
//using JarvisNG.Models.IRepositories;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

namespace JarvisNG {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<DataContext>(Options =>
                Options.UseSqlServer(Configuration.GetConnectionString("DataContext")));

            services.AddScoped<DataInitializer>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddCors(options =>
                options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()));
            services.AddOpenApiDocument(c => {
                c.DocumentName = "apidocs";
                c.Title = "Jarvis API";
                c.Version = "v1";
                c.Description = "The Jarvis API documentation description.";
                c.DocumentProcessors
                    .Add(new SecurityDefinitionAppender("JWT Token",
                        new SwaggerSecurityScheme {
                            Type = SwaggerSecuritySchemeType.ApiKey,
                            Name = "Authorization",
                            In = SwaggerSecurityApiKeyLocation.Header,
                            Description = "Copy 'Bearer' + valid JWT token into field"
                        }));
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            }); //for OpenAPI 3.0 else AddSwaggerDocument();

            services.AddIdentity<IdentityUser, IdentityRole>(cfg => cfg.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<DataContext>();

            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters =
                    "azertyuiopqsdfghjklmwxcvbnAZERTYUIOPQSDFGHJKLMWXCVBN234567890";
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication(x => { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true //Ensure token hasn't expired
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, IHostingEnvironment env, DataInitializer initializer) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();


            app.UseCors("AllowAllOrigins");


            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwaggerUi3();

            app.UseSwagger();


            initializer.InitializeData().Wait();
        }
    }
}
