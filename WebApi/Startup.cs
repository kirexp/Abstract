using System;
using System.IO;
using System.Threading.Tasks;
using DAL.Entities.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using WebApi.Auth;
using WebApi.Auth.Models;
using WebApi.Controllers;
using WebApi.Hub;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var policy = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy();
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            
            policy.SupportsCredentials = true;

            services.AddCors(x => x.AddPolicy("corsGlobalPolicy", policy));
            services.AddAuthentication(IdentityConstants.ApplicationScheme).AddCookie(IdentityConstants.ApplicationScheme).AddJwtBearer(cfg => {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                //asdasd
                cfg.TokenValidationParameters = new TokenValidationParameters() {
                    IssuerSigningKey = TokenAuthOption.Key,
                    ValidAudience = TokenAuthOption.Audience,
                    ValidIssuer = TokenAuthOption.Issuer,
                    // When receiving a token, check that we've signed it.
                    ValidateIssuerSigningKey = true,
                    // When receiving a token, check that it is still valid.
                    ValidateLifetime = true,
                    // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time 
                    // when validating the lifetime. As we're creating the tokens locally and validating them on the same 
                    // machines which should have synchronised time, this can be set to zero. and default value will be 5minutes
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
                cfg.Events = new JwtBearerEvents {
                    OnMessageReceived = context => {
                        if (context.Request.Path.Value.StartsWith("/loo") &&
                            context.Request.Query.TryGetValue("token", out StringValues token)
                        ) {
                            context.Token = token;
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context => {
                        var te = context.Exception;
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddIdentityCore<User>(options => {

            })
                .AddUserManager<ApplicationUserManager<User>>().AddUserStore<ApplicationUserStore>().AddSignInManager<ApplicationSignInManger<User>>()
                .AddDefaultTokenProviders();
            services.AddTransient<SignInManager<User>, ApplicationSignInManger<User>>();
            services.AddTransient<ApplicationSignInManger<User>, ApplicationSignInManger<User>>();
            services.AddTransient<Microsoft.AspNetCore.Identity.UserManager<User>, ApplicationUserManager<User>>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<SignInRepository, SignInRepository>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("corsGlobalPolicy");
            app.UseAuthentication();
            app.UseSignalR(routes => {
                routes.MapHub<LoopyHub>("loopy");
            });
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
