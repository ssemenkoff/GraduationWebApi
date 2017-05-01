using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Core_Server.Models.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Core_Server.Models.Data;
using Core_Server.Helpers;

namespace Core_Server
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //string connection = "Server=tcp:ssemenkoff.database.windows.net,1433;Initial Catalog=ssemenkoff-graduationDB;Persist Security Info=False;User ID=ssemenkoff;Password=Simple1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";            
            string sqlite = "Data Source=AppData.db";
            string users = "Data Source=Users.db";


            //services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<UserContext>(options => options.UseSqlite(users));
            services.AddDbContext<DataContext>(options => options.UseSqlite(sqlite));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials() );
            });

            services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, UserContext userContext, DataContext dataContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                ExpireTimeSpan = TimeSpan.FromHours(1),
                SlidingExpiration = true
            });

            app.UseGoogleAuthentication(new GoogleOptions
            {
                ClientId = "1083905455612-b7t0pkss8akqf3fttf79jf2eceb5v4vi.apps.googleusercontent.com",
                ClientSecret = "Mhh0OpJvbFVlBcZZzqmk_vGv",
                Scope = { "email", "openid" }
            });

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Helpers.AuthenticationHelper.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = Helpers.AuthenticationHelper.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = Helpers.AuthenticationHelper.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true                    
                }
            });
            
            app.UseCors("CorsPolicy");

            DbInitializer.Initialize(userContext, dataContext);

            app.UseMvc();
        }
    }
}
