using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ToDoList
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //GS.Data.Generic.SetConfiguration(configuration);
            
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            
            var prov = services.BuildServiceProvider();            

            //設定 Default Razor Page 
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/default", "");
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddControllers(config =>
            {
                // 注入錯誤處理
                //config.Filters.Add<ExceptionFilter>();
            });
            // Smart API 必要物件 (API 接收 JTOKEN 物件            
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //依 Property 輸出
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // JSON 的欄位編碼依 Camel 編碼輸出 
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                // Value = null 時,不輸出 
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }
            );
            // 允許存取等級
            services.AddCors();
            // 可使用 Controllers 
            services.AddControllers().AddJsonOptions(options =>
            {
                // 設定輸出的 JSON 字串 , 不會 content-type 而轉換
                //  JsonNamingPolicy => 輸出字串格式設定
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            int ExprieMinute = 20;
            string APP_NAME = "ToDoList";
            // 將 Session 存在 ASP.NET Core 記憶體中
            services.AddDistributedMemoryCache();
            TimeSpan ExpireMinute = TimeSpan.FromMinutes(ExprieMinute);
            services.AddSession(options =>
            {
                //限制 https 
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = APP_NAME;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.IsEssential = true;
                // session 有效時間
                options.IdleTimeout = ExpireMinute;
            });

            // Cookie 驗證
            services.AddAuthentication().AddCookie("Member", options =>
            {
                options.ExpireTimeSpan = ExpireMinute;
                options.Cookie.MaxAge = ExpireMinute;
                options.Cookie.Name = APP_NAME;
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            //services.AddAuthentication("Basic")
            //    .AddScheme<BasicAuthenticationOptions, MyAuthenticationHandler>("Basic", null);
            services.AddHttpClient();
            // DI 

            // AddConnection 
            var ConnectionSection = Configuration.GetSection("GeoSense").GetSection("ConnectionSetting");
            MyDbContextManager Manager = new MyDbContextManager();
            System.Type BuilderType = typeof(DbContextOptionsBuilder);
            foreach (var setting in ConnectionSection.GetChildren())
            {
                //Reflection Method 未完成!!先 HardCode 

                string Name = setting.GetValue<string>("Name");
                string InvokeMethod = setting.GetValue<string>("InvokeMethod");
                string ConnectionString = setting.GetValue<string>("ConnectionString");

                if (InvokeMethod == "UseSqlServer")
                {
                    var bu = new DbContextOptionsBuilder();
                    
                    Manager.Add(Name,
                        (new DbContextOptionsBuilder()).UseSqlServer(ConnectionString));                    
                }
   
            }


            //注入 身份驗證套件 => 主要給 JWTAuthenticationHandler 使用


            services.AddSingleton<IConfiguration>(provider => Configuration);

            //有用到 Authroize 才會去取得 UserData 
            //  千萬要記得來這裡實作<ClaimAuthorizationHandler> 使用者資料
            //services.AddScoped<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, ClaimAuthorizationHandler>();
//
            //services.AddTransient<RequestAuthentication>();
            //services.AddSingleton<AuthenticationManager, AuthenticationManager>();
            //IHttpContextAccessor -> Get HttpContext 
            services.AddHttpContextAccessor();
            //注入連線用物件 ; 目前缺點, 在同一個Request 無法共用 transaction 
            services.AddSingleton<MyDbContextManager>(Manager);
            //Html writer 中文會被編碼問題修正
            services.AddSingleton<System.Text.Encodings.Web.HtmlEncoder>(System.Text.Encodings.Web.HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //開發模式, 會傳出錯誤內容
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //導向錯誤頁面
                app.UseExceptionHandler("/Error");
            }

            //錯誤中介
            //app.UseMiddleware<ExceptionMiddleware>();

 
            //可以使用靜態頁面
            app.UseStaticFiles();
            //
           
            //啟用 Session
            app.UseSession();
            //強制轉到Https 
            app.UseHttpsRedirection();
            //使用路由
            app.UseRouting();
            //啟用授權
            app.UseAuthentication();
            app.UseAuthorization();
            //
            
            app.UseEndpoints(endpoints =>
            {           
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                //不要給預設頁面
                endpoints.MapControllerRoute("default2", "");
                // 預設 MVC 頁面
                // endpoints.MapControllerRoute("default2", "{controller:Home}/{action:IntoStart}/{id?}");
            });
        }
    }
}
