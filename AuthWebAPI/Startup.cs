using AuthWebAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthWebAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string con = "Server=(localdb)\\mssqllocaldb;Database=personsdbstore;Trusted_Connection=True;";
  
            services.AddDbContext<PersonContext>(options => options.UseSqlServer(con));


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                       
                            ValidateIssuer = true,
                       
                            ValidIssuer = AuthOptions.ISSUER,

                            ValidateAudience = true,
                            ValidAudience = AuthOptions.AUDIENCE,
                            ValidateLifetime = true,

                    
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                 
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddAuthorization(opts => {
                opts.AddPolicy("OnlyForXanart", policy => {
                    policy.RequireClaim(ClaimTypes.Email, "xanart@gmail.com");
                });
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
