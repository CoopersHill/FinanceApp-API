using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using hwFinanceApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace hwFinanceApp
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; //name of default policy
        private string _FinanceConnectionString = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _FinanceConnectionString = Configuration["ConnectionStrings: HWFinanceConnectionStringProd"];
            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "hwFinanceApp", Version = "v1" });
            });
            // services.AddDbContext<FinanceContext>(opt => opt.UseInMemoryDatabase("TransactionList")); --only used when no db around
            //services.AddDbContext<FinanceContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<FinanceContext>(options => options.UseSqlServer(_FinanceConnectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
                                                                {
                                                                    builder.AllowAnyOrigin()
                                                                    .AllowAnyMethod()
                                                                    .AllowAnyHeader();
                                                                    //.AllowCredentials(); can't use credentials with allowany origin
                                                                });
            });
            services.AddAuthorization(options => 
            {
                options.AddPolicy("ApiScope", policy => 
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "hwFinanceApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins); //for more information, see https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-5.0

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                .RequireAuthorization("ApiScope");
            });
        }
    }
}
