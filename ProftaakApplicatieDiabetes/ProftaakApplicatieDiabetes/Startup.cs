using Data.Contexts;
using Data.Interfaces;
using Logic;
using Logic.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProftaakApplicatieDiabetes
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Home/ErrorForbidden";
                    options.LoginPath = "/Home/ErrorNotLoggedIn";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", p => p.RequireAuthenticatedUser().RequireRole("Admin"));
                options.AddPolicy("CareRecipient", p => p.RequireAuthenticatedUser().RequireRole("CareRecipient"));
                options.AddPolicy("Doctor", p => p.RequireAuthenticatedUser().RequireRole("Doctor"));
            });
            services.AddScoped<IUserContext, UserContextSQL>();
            services.AddScoped<ICalculationContext, CalculationContext>();
            services.AddScoped<ICalculationLogic, CalculationLogic>();
            services.AddScoped<IMessageLogic, MessageLogic>();
            services.AddScoped<IMessageContext, MessageContext>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IDoctorContext, DoctorContext>();
            services.AddScoped<IDoctorLogic, DoctorLogic>();
            services.AddScoped<IAccountLogic, AccountLogic>();
            services.AddScoped<IAccountContext, AccountContext>();
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
