using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HouseholdExpensesTrackerServer21.Infrastructure.Context;
using HouseholdExpensesTrackerServer21.Web.Common.Mvc;
using HouseholdExpensesTrackerServer21.Web.CompositionRoot;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static HouseholdExpensesTrackerServer21.Web.Core.Consts;

namespace HouseholdExpensesTrackerServer21.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "http://localhost:54707",
                        ValidAudience = "http://localhost:4000",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                    };
                });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddDefaultJsonOptions();

            services.AddDbContext<HouseholdDbContext>(
                options =>
                    options.UseSqlServer(
                        this.Configuration.GetConnectionString(ApplicationConfigurationKeys.HouseholdConnectionString))
            );
            services.AddScoped<IDbContext>(provider => provider.GetService<HouseholdDbContext>());
            services.AddLogging(builder => builder.AddSerilog(dispose: true));

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new AutofacDefaultModule { Configuration = this.Configuration });
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseErrorHandler();
            app.UseMvc();
            InitializeDb(app.ApplicationServices).Wait();
        }

        private static async Task InitializeDb(IServiceProvider service)
        {
            using (var serviceScope = service.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<IDbContext>();
                await db.Database.MigrateAsync();
            }
        }
    }
}
