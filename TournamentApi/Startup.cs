using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TournamentApi.Data;
using TournamentApi.Data.Repositories;
using TournamentApi.Models;

namespace TournamentApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<TournamentContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("TournamentContext")));

            services.AddOpenApiDocument(c =>
            {
                c.DocumentName = "apidocs";
                c.Title = "Recipe API";
                c.Version = "v1";
                c.Description = "The Recipe API documentation description.";
            });

            services.AddScoped<TournamentAppDataInitializer>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<ITournamentRepository, TournamentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddIdentity<IdentityUser, IdentityRole>(cfg => cfg.User.RequireUniqueEmail = true).AddEntityFrameworkStores<TournamentContext>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TournamentAppDataInitializer tournamentAppDataInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwaggerUi3();
            app.UseSwagger();
            app.UseMvc();


            tournamentAppDataInitializer.initializeData().Wait();
        }
    }
}
