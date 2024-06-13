

using LoanManagementSystem_V3_API.Model;
using LoanManagementSystem_V3_API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LoanManagementSystem_V3_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //For The connection string and the Db Context 
            builder.Services.AddDbContext<LmsV4DbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LMS_ConnectionString")));


            //Then we need to add the Scoped For The Repository 
            builder.Services.AddScoped<ILoginRepository,LoginRepositiory>();
            builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOfficerRepository, OfficerRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();


            //Then we need to Add middleware to AllowAllOrgins so that we will not Encouneter with Cors Error when integrating Angular 
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowAllOrgin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            //-----------------------------------------------------------------------------------------------------------

            // we need to register the JWT authndication Scheema 
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };

            });


            var app = builder.Build();
            app.UseCors("AllowAllOrgin");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
