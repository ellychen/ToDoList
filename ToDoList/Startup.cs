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

            //�]�w Default Razor Page 
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/default", "");
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddControllers(config =>
            {
                // �`�J���~�B�z
                //config.Filters.Add<ExceptionFilter>();
            });
            // Smart API ���n���� (API ���� JTOKEN ����            
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //�� Property ��X
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // JSON �����s�X�� Camel �s�X��X 
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                // Value = null ��,����X 
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }
            );
            // ���\�s������
            services.AddCors();
            // �i�ϥ� Controllers 
            services.AddControllers().AddJsonOptions(options =>
            {
                // �]�w��X�� JSON �r�� , ���| content-type ���ഫ
                //  JsonNamingPolicy => ��X�r��榡�]�w
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            int ExprieMinute = 20;
            string APP_NAME = "ToDoList";
            // �N Session �s�b ASP.NET Core �O���餤
            services.AddDistributedMemoryCache();
            TimeSpan ExpireMinute = TimeSpan.FromMinutes(ExprieMinute);
            services.AddSession(options =>
            {
                //���� https 
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.Name = APP_NAME;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.IsEssential = true;
                // session ���Įɶ�
                options.IdleTimeout = ExpireMinute;
            });

            // Cookie ����
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
                //Reflection Method ������!!�� HardCode 

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


            //�`�J �������ҮM�� => �D�n�� JWTAuthenticationHandler �ϥ�


            services.AddSingleton<IConfiguration>(provider => Configuration);

            //���Ψ� Authroize �~�|�h���o UserData 
            //  �d�U�n�O�o�ӳo�̹�@<ClaimAuthorizationHandler> �ϥΪ̸��
            //services.AddScoped<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, ClaimAuthorizationHandler>();
//
            //services.AddTransient<RequestAuthentication>();
            //services.AddSingleton<AuthenticationManager, AuthenticationManager>();
            //IHttpContextAccessor -> Get HttpContext 
            services.AddHttpContextAccessor();
            //�`�J�s�u�Ϊ��� ; �ثe���I, �b�P�@��Request �L�k�@�� transaction 
            services.AddSingleton<MyDbContextManager>(Manager);
            //Html writer ����|�Q�s�X���D�ץ�
            services.AddSingleton<System.Text.Encodings.Web.HtmlEncoder>(System.Text.Encodings.Web.HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //�}�o�Ҧ�, �|�ǥX���~���e
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //�ɦV���~����
                app.UseExceptionHandler("/Error");
            }

            //���~����
            //app.UseMiddleware<ExceptionMiddleware>();

 
            //�i�H�ϥ��R�A����
            app.UseStaticFiles();
            //
           
            //�ҥ� Session
            app.UseSession();
            //�j�����Https 
            app.UseHttpsRedirection();
            //�ϥθ���
            app.UseRouting();
            //�ҥα��v
            app.UseAuthentication();
            app.UseAuthorization();
            //
            
            app.UseEndpoints(endpoints =>
            {           
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                //���n���w�]����
                endpoints.MapControllerRoute("default2", "");
                // �w�] MVC ����
                // endpoints.MapControllerRoute("default2", "{controller:Home}/{action:IntoStart}/{id?}");
            });
        }
    }
}
