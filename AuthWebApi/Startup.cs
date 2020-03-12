using AuthWebApi.Database;
using AuthWebApi.DataModels;
using AuthWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace AuthWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var authOptSection = Configuration.GetSection("AuthOptions");
            services.Configure<AuthOptions>(authOptSection);
            var authOptions = authOptSection.Get<AuthOptions>();
            authOptions.Init();

            services.AddSingleton<IRngGeneratorService, RngGeneratorService>();
            services.AddSingleton<ITokenGeneratorService,JwtGeneratorService>();
            services.AddSingleton<IChallengeNotifierService, ChallengeNotifierService>();
            services.AddSingleton<PasswordHasher<UserRegistrationModels>>();

            services.AddDbContext<AuthDbContext>(options => { options.UseNpgsql(Configuration.GetConnectionString("db_auth")); });
            services.AddControllers().AddNewtonsoftJson();


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
                        ValidateAudience = true,
                        ValidAudience = authOptions.JwtAudience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = authOptions.PublicKey
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
