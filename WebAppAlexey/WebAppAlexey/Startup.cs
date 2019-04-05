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
using WebAppAlexey.DAL.Models;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using System.Collections.Generic;

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
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "You api title", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });

            });
          

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
                configuration.RootPath = "MaterialProject/dist";
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

            app.UseCors(MyAllowSpecificOrigins);
            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Northwind Service API V1");
            });

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
                

                spa.Options.SourcePath = "MaterialProject";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
