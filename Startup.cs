using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OPS_API.Domain.Repositories;
using OPS_API.Domain.Services;
using OPS_API.Persistence.Contexts;
using OPS_API.Persistence.Repositories;
using OPS_API.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace OPS_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddEntityFrameworkNpgsql();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<ApplicationContext>(o =>
                o.UseNpgsql(
                    Configuration.GetConnectionString("DatabaseUri"),
                    b => b.UseNetTopologySuite()
                )
            );

            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOperationRepository, OperationRepository>();
            services.AddScoped<IEquipmentRepository, EquipmentRepository>();
            services.AddScoped<IRescuerRepository, RescuerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IRescuerService, RescuerService>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Otsingu- ja Päästetööde Süsteem",
                    Version = "1.0.0"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(
                c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OPS API v1")
            );

            using (
                var serviceScope = app.ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope()
            )
            {
                var db = serviceScope.ServiceProvider.GetService<ApplicationContext>().Database;

//                db.EnsureDeleted();
                db.EnsureCreated();
            }
        }
    }
}