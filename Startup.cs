using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using OAuthTest.Models.Configs;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;

namespace OAuthTest
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
            IConfiguration googleconfig = Configuration.GetSection("GoogleOauth");
            services.Configure<GoogleOAuthConfig>(googleconfig);
            services.AddMvc();
            services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("fully permissive", configurePolicy => configurePolicy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200").AllowCredentials()); //localhost:4200 is the default port an angular runs in dev mode with ng serve
            });

            services.AddDbContext<IdentityDbContext>(options =>
                        options.UseSqlite("Data Source=users.sqlite",
                        sqliteOptions => sqliteOptions.MigrationsAssembly("GoogleSpaWeb")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
            })
            .AddGoogle("Google", options =>
            {
                options.CallbackPath = new PathString("/callbackGoogle");
                options.ClientId = googleconfig.GetValue<string>("GoogleClientId");
                options.ClientSecret = googleconfig.GetValue<string>("GoogleClientSecret");
                options.Events = new OAuthEvents
                {
                    OnRemoteFailure = (RemoteFailureContext context) =>
                    {
                        context.Response.Redirect("/home/denied");
                        context.HandleResponse();
                        return Task.CompletedTask;
                    }
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("fully permissive");

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
