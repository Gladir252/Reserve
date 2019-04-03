using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAppAlexey.DAL.DataBaseContext;
using WebAppAlexey.BLL.Services;
using WebAppAlexey.DAL.Extension;
using WebAppAlexey.BLL.ViewModels;
using WebAppAlexey.DAL.Models;

namespace WebAppAlexey
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            ///

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:50133",
                                        "http://localhost:4200")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod(); 
                });
            });
            ///
            services.AddDefaultIdentity<User>().AddRoles<Role>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;////
                options.SaveToken = true;/////
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,//
                    ValidateAudience = false,//
                    ValidateLifetime = true,//
                    ValidateIssuerSigningKey = true,
                    

                    ValidIssuer = "http://localhost:50133",
                    ValidAudience = "http://localhost:50133",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
                };
            
            });

            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<WebAppDataBaseContext>(options => options.UseSqlServer(connection));

            services.RegisterRepositories();
            services.RegisterServices();



            

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
               
                app.UseHsts();
            }

            ///
            app.UseCors(MyAllowSpecificOrigins);
            ///

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
