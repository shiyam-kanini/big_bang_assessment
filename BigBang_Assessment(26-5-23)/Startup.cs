using BigBang_Assessment_26_5_23_.Models;
using BigBang_Assessment_26_5_23_.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BigBang_Assessment_26_5_23_
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepoAddress, RepoAddress>();
            services.AddScoped<IRepoXYZ, RepoXYZ>();
            services.AddScoped<IRepoRooms, RepoRooms>();
            services.AddScoped<IRepoRoles, RepoRoles>();
            services.AddScoped<IRepoHotel, RepoHotel>();
            services.AddScoped<IRepoEmployee, RepoEmployee>(); 
            services.AddScoped<IRepoEmployeeXYZRole, RepoEmployeeXYZRole>();
            services.AddScoped<IRepoUser, RepoUser>();



            services.AddDbContext<XYZHotelDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("connection")));

            
            services.AddControllers().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSetting:Token").Value)),

                ValidateIssuer = false,
                ValidateAudience = false,
            });

            /* services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
             })
             .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = Configuration["JWT:ValidAudience"],
                     ValidIssuer = Configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                 };
             });
 */
            services.AddControllers();

            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
