using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace AzureADB2CWeb
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            //Applicaation/ClientId -3fa6a6ad-c3bf-4e06-b4c2-0e7f9b4e1abd
            //Auth end point  : https://login.microsoftonline.com/8a5705ad-421e-43c6-87a1-c8a714fe132a/oauth2/v2.0/authorize
            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = "https://winsomebasketadb2cprogram.b2clogin.com/WinsomeBasketADB2CProgram.onmicrosoft.com/B2C_1_SignIn_Up/v2.0/";
                options.ClientId = "eda947c9-5dac-4cc8-a474-c8dc05b5e284";
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.Scope.Add(options.ClientId);
                options.ClientSecret = "s2G8Q~MKA6JzpzkGz4IvWCEsSjfTk0CZbEQszdio";
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    NameClaimType = "name"
                };
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}