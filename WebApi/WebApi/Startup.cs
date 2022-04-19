using Application.Common.Interfaces;
using Application.DishStatuses.Queries.GetDishStatusesList;
using Application.IngredientStatuses.Queries.GetIngredientStatusesList;
using AutoMapper;
using Domain.Entities.Authorization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using System;
using System.Reflection;
using Common.Dto.Ingredients;
using WebApi.Infrastructure.Extensions;
using WebApi.Mappings;

namespace WebApi
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
            var mapperConfig = new MapperConfiguration(x =>
            {
                x.AddProfile(new IngredientStatusMappingProfile());
                x.AddProfile(new IngredientMappingProfile());
                x.AddProfile(new DishStatusMappingProfile());
                x.AddProfile(new DishCategoryMappingProfile());
                x.AddProfile(new DishMappingProfile());
                x.AddProfile(new OrderStatusMappingProfile());
                x.AddProfile(new TableStatusMappingProfile());
                x.AddProfile(new OrderMappingProfile());
                x.AddProfile(new WaiterMappingProfile());
                x.AddProfile(new TableMappingProfile());
                x.AddProfile(new KitchenerMappingProfile());
                x.AddProfile(new RoleMappingProfile());
            });
            services.AddIdentity<User, Role>(options =>
                {
                    options.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<AppDbContext>();

            var authOptions = services.ConfigureAuthOptions(Configuration);
            services.AddJwtAuthentication(authOptions);

            services.AddMediatR(typeof(GetIngredientStatusesListQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetDishStatusesListQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(InsertedIngredientDto).GetTypeInfo().Assembly);

            services.AddSingleton(mapperConfig.CreateMapper());
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
                services.AddDbContext<AppDbContext>();
            /*services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    "Persist Security Info=False;Integrated Security=true;Initial Catalog=MyRestaurant;Server=DESKTOP-1P5LPV2"));
            */
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddControllers();
            
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" }); });

            ConfigureSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
            else
            {
                app.UseErrorHandling();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            var contact = new OpenApiContact()
            {
                Name = "Gusac Dmitri",
                Email = "gusac.dmitri212@gmail.com",
                Url = new Uri("http://www.example.com")
            };

            var license = new OpenApiLicense()
            {
                Name = "Api License",
                Url = new Uri("http://www.example.com")
            };

            var info = new OpenApiInfo()
            {
                Version = "v2",
                Title = "Swagger Elearning API",
                Description = "This API can be used for developing elearning platforms",
                TermsOfService = new Uri("http://www.example.com"),
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", info);
            });
        }
    }
}
