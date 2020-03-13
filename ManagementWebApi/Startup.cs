using System.IdentityModel.Tokens.Jwt;
using System.IO;
using ManagementWebApi.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;

namespace ManagementWebApi
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
            var authOptSection = Configuration.GetSection("AuthOptions");
            services.Configure<AuthOptions>(authOptSection);
            var authOptions = authOptSection.Get<AuthOptions>();
            authOptions.Init();

            services.AddDbContext<ManagementDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("db_manage"));
                options.UseLoggerFactory(new NLogLoggerFactory());
            });
            services.AddControllers().AddNewtonsoftJson(c => c.SerializerSettings.Converters.Add(
                new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() }));

            services
                .AddAuthentication(co =>
                {
                    co.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    co.RequireAuthenticatedSignIn = true;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.JwtIssuer,
                        ValidateAudience = false,
                        ValidAudience = authOptions.JwtAudience,
                        ValidateIssuerSigningKey = false,
                        IssuerSigningKey = authOptions.PublicKey,
                        ValidateLifetime = false, //WARNING
                        ValidateTokenReplay = false,
                        ValidateActor = false,
                        SignatureValidator = delegate (string token, TokenValidationParameters parameters)
                        {
                            var jwt = new JwtSecurityToken(token);

                            return jwt;
                        },
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            var webRoot = Path.Combine(env.ContentRootPath, "DataFolder");
            env.WebRootPath = webRoot;
            env.WebRootFileProvider = new PhysicalFileProvider(webRoot);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //app.UseHttpsRedirection();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
