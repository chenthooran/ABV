using ABV.Context;
using ABV.Context.Extensions;
using ABV.Core.Accounts.Commands.CreateAccount;
using ABV.Core.Accounts.Commands.CreateAccount.Factory;
using ABV.Core.Accounts.Commands.CreateAccountBalance;
using ABV.Core.Accounts.Commands.CreateAccountBalance.Factory;
using ABV.Core.Accounts.Commands.CreatePeriod;
using ABV.Core.Accounts.Commands.CreatePeriod.Factory;
using ABV.Core.Accounts.Queries.GetAccountBalances;
using ABV.Core.Accounts.Queries.GetAccounts;
using ABV.Core.Accounts.Queries.GetPeriods;
using ABV.Core.Contracts;
using ABV.Domain.Entities;
using ABV.Web.Accounts.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ABV.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddCookie()
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                                                       (Configuration["Token:Key"]))
                };
            });

            services.AddMvc();

            // Add application services.
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAccountFactory, AccountFactory>();
            services.AddTransient<IAccountBalanceFactory, AccountBalanceFactory>();
            services.AddTransient<IPeriodFactory, PeriodFactory>();
            services.AddTransient<IGetAccountsListQuery, GetAccountsListQuery>();
            services.AddTransient<IGetPeriodsListQuery, GetPeriodsListQuery>();
            services.AddTransient<IGetAccountBalancesListQuery, GetAccountBalancesListQuery>();
            services.AddTransient<ICreateAccountCommand, CreateAccountCommand>();
            services.AddTransient<ICreatePeriodCommand, CreatePeriodCommand>();
            services.AddTransient<ICreateAccountBalanceCommand, CreateAccountBalanceCommand>();
            services.AddTransient<IDbContextService, ApplicationDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.ServiceProvider.GetService<ApplicationDbContext>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<ApplicationDbContext>().SeedInitialData();
                }
            }
        }
    }
}
