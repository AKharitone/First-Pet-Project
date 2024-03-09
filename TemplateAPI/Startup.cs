using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TemplateAPI.Seeds;
using Swashbuckle.AspNetCore.Swagger;
using TemplateAPIServices.Interfaces;
using TemplateAPIServices.Services;
using TemplateAPIModel;

namespace TemplateAPI
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Template API", Version = "v1" });
            });

            //Helper interfaces
            var configuration = new ApplicationConfiguration(Configuration);
            services.AddSingleton<IApplicationConfiguration>(configuration);

            //Set up authorization
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = LoginService.Issuer,
                        ValidAudience = LoginService.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JwtSecurityKey))
                    };
                });

            //Set up swagger to surface the authorization
            services.AddSwaggerGen(c =>
            {
                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
        {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });

            services.AddMvc();

            //Add database context
            services.AddDbContext<TemplateContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SystemDatabase"), o => o.MigrationsAssembly("TemplateAPI")));

            //Add services mapping
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IAddressService, AddressService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IHealthCardService, HealthCardService>();
            services.AddTransient<IIllnessService, IllnessService>();
            services.AddTransient<ISymptomService, SymptomService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TemplateContext context, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Warning()
             .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "Logs/Template.txt"))
             .CreateLogger();

            loggerFactory.AddSerilog();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:59633");
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowCredentials();
            });

            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template API");

            });

            context.Database.Migrate();
            Seed.initSeed(context);
        }
    }
}
