using GamesApi.Configuration;
using GamesApi.Data;
using GamesApi.DataProviders;
using GamesApi.DataProviders.Abstractions;
using GamesApi.Services;
using GamesApi.Services.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GamesApi
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            AppConfiguration = builder.Build();
        }

        public IConfiguration AppConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();

            // accepts any access token issued by identity server
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://192.168.1.120:5000";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            // adds an authorization policy to make sure the token is for scope 'api1'
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "gamesapi.gamesapi");
                });
                options.AddPolicy("ApiScopeBff", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "gamesapi.gamesapibff");
                });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GamesApi", Version = "v1" });
            });
            services.Configure<Config>(AppConfiguration);
            var connectionString = AppConfiguration["GamesApi:ConnectionString"];
            services.AddDbContextFactory<GamesDbContext>(
                opts => opts.UseNpgsql(connectionString));
            services.AddTransient<IGameProvider, GameProvider>();
            services.AddTransient<IGameService, GameService>();
            services.AddScoped<IDbContextWrapper<GamesDbContext>, DbContextWrapper<GamesDbContext>>();
            services.AddTransient<IHttpClientService, HttpClientService>();
            services.AddTransient<IRateLimitService, RateLimitService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GamesApi v1"));
            }

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Strict });
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
