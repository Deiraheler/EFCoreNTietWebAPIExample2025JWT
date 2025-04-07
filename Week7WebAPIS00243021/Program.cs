
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductWebAPI2025.DataLayer;
using System.Text;
using Tracker.WebAPIClient;
using Week7SupplierDataModelS00243021;
using Week7WebAPIS00243021.DataLayer;

namespace Week7WebAPIS00243021
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var connectionString = builder.Configuration.GetConnectionString("IdentityModelConnection")
                ?? throw new InvalidOperationException("Connection string 'IdentityModelConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<BusinessContext>(options =>
                options.UseSqlServer(connectionString));

            ActivityAPIClient.Track(StudentID: "S00243021",
                StudentName: "Dmytro Severin",
                activityName: "Rad302 Week 7 Lab 1",
                Task: "Setting up Web API");

            // adding user identity class  
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                cfg => cfg.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //add Authentication to the Web App
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie().AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Tokens:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Tokens:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Tokens:Key"]))
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product Web API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization Header using Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    }, new String[] {}
                }
                });
            });

            var app = builder.Build();

            // Retrieve the user context from the services container
            using (var scope = app.Services.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetRequiredService<BusinessContext>();
                // Retrieve the IWebHostEnvironment for the Content Root even though we are not using the file system here
                var hostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                _ctx.Database.Migrate();
                DbSeeder dbSeeder = new DbSeeder(_ctx, hostEnvironment);
                dbSeeder.SeedSuppliers();
            }

            // Scope the user context for the application
            using (var scope = app.Services.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                _ctx.Database.Migrate();
                // Create a new instance of the ApplicationDbSeeder class and call the Seed method for users and roles
                ApplicationDbSeeder dbSeeder = new ApplicationDbSeeder(_ctx, userManager, roleManager);
                dbSeeder.Seed().Wait(); // seed method is in the dbseeder class
            }

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
