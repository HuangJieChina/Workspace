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

namespace HH.API
{
    public class Startup
    {
        private static SymmetricSecurityKey _SymmetricKey = null;
        /// <summary>
        /// 获取JWT秘钥
        /// </summary>
        public static SymmetricSecurityKey SymmetricKey
        {
            get
            {
                if (_SymmetricKey == null)
                {
                    string key = "huangj@authine.com";
                    _SymmetricKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                }
                return _SymmetricKey;
            }
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                        ValidIssuer = "Authine",
                        ValidAudience = "api",
                        IssuerSigningKey = SymmetricKey
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

            services.AddMvc();
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

            // JWT认证
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
