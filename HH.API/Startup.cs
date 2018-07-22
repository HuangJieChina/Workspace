using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using HH.API.Controllers;
using Autofac;
using System.Reflection;
using HH.API.Services;
using Autofac.Extensions.DependencyInjection;
using HH.API.IServices;
using HH.API.Entity;
using HH.API.Aop;
using HH.API.Events;

namespace HH.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region 使用 Autofac 之前 -------------------
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="services"></param>
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        //        .AddJwtBearer(o =>
        //        {
        //            o.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                NameClaimType = JwtClaimTypes.Name,
        //                RoleClaimType = JwtClaimTypes.Role,
        //                ValidIssuer = Config.API_Issuer,
        //                ValidAudience = Config.API_Audience,
        //                IssuerSigningKey = Config.SymmetricKey
        //                /***********************************TokenValidationParameters的参数默认值***********************************/
        //                // RequireSignedTokens = true,  
        //                // SaveSigninToken = false,  
        //                // ValidateActor = false,  
        //                // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。  
        //                // ValidateAudience = true,  
        //                // ValidateIssuer = true,   
        //                // ValidateIssuerSigningKey = false,  
        //                // 是否要求Token的Claims中必须包含Expires  
        //                // RequireExpirationTime = true,  
        //                // 允许的服务器时间偏移量  
        //                // ClockSkew = TimeSpan.FromSeconds(300),  
        //                // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比  
        //                // ValidateLifetime = true  
        //            };
        //        });

        //    // 注册接口和实现类的映射关系
        //    // services.AddScoped<IUserRepository, UserRepository>();

        //    services.AddMvc();
        //}
        #endregion

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                        ValidIssuer = Config.API_Issuer,
                        ValidAudience = Config.API_Audience,
                        IssuerSigningKey = Config.SymmetricKey
                        /***********************************TokenValidationParameters的参数默认值***********************************/
                        // RequireSignedTokens = true,  
                        // SaveSigninToken = false,  
                        // ValidateActor = false,  
                        // 将下面两个参数设置为false，可以不验证Issuer和Audience，但是不建议这样做。  
                        // ValidateAudience = true,  
                        // ValidateIssuer = true,   
                        // ValidateIssuerSigningKey = false,  
                        // 是否要求Token的Claims中必须包含Expires  
                        // RequireExpirationTime = true,  
                        // 允许的服务器时间偏移量  
                        // ClockSkew = TimeSpan.FromSeconds(300),  
                        // 是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比  
                        // ValidateLifetime = true  
                    };
                });

            // 注册接口和实现类的映射关系
            // services.AddScoped<IUserRepository, UserRepository>();
            // services.AddSingleton<IWorkflowPackageRepository, WorkflowPackageRepository>();

            // 配置跨域
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            // End

            services.AddMvc();
            return InitIoC(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // NLog Start
            loggerFactory.AddNLog();// 添加NLog 
            LogManager.LoadConfiguration("nlog.config");
            // End

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 白名单
            app.UseWhiteList();

            // JWT认证
            app.UseAuthentication();

            // 服务的依赖注入 Start
            RegisterEntityEvent registerEntityEvent = new RegisterEntityEvent();
            registerEntityEvent.RegisterUserEnvent();

            // End
            app.UseMvc();
        }

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceProvider InitIoC(IServiceCollection services)
        {
            IoCContainer.Register("HH.API.Services", "HH.API.IServices");//注册service
            return IoCContainer.Build(services);
        }
    }
}
