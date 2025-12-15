using System.Text;
using FamilyApplication.CommonServices;
using FamilyApplication.DBContext;
using FamilyApplication.IRepos;
using FamilyApplication.IServices;
using FamilyApplication.Repos;
using FamilyApplication.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<JwtService>();
builder.Services.AddSingleton<PasswordHasedService>();
builder.Services.AddScoped<IRegisterRepo, RegisterRepo>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IAdminRepo, AdminRepo>();    
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();  
builder.Services.AddScoped<IAuthService, AuthService>();    
builder.Services.AddScoped<IMemberRepo, MemberRepo>();  
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IShowAllMembersRepo, ShowAllMembersRepo>();
builder.Services.AddScoped<IShowAllMembersService, ShowAllMembersService>();


builder.Services.AddControllers()
    .AddFluentValidation();
builder.Services.AddValidatorsFromAssemblyContaining<PasswordValidation>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Family Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat ="Jwt",
        In = ParameterLocation.Header,
        Description= "Enter 'Bearer' followed by your token",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<ApplicationDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        b => b.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowAngular");

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();    

app.UseAuthorization();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images")),
    RequestPath = "/Images"
});

app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
//    var passwordService = scope.ServiceProvider.GetRequiredService<PasswordHasedService>();
//    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

//    try
//    {
//        if (!db.RegisterModels.Any(u => u.Role == "Admin"))
//        {
//            var defaultAdminPassword = config["Admin:DefaultPassword"] ?? "ChangeMe@123";
//            var hashed = passwordService.PasswordHased(defaultAdminPassword);

//            var admin = new RegisterModels
//            {
//                Username = "Admin",
//                Fname = "Family",
//                Lname = "Admin",
//                Phone = "7708038216",
//                Email = "admin@gmail.com",
//                Password = hashed,
//                Cpassword = hashed,
//                Role = "Admin"
//            };

//            db.RegisterModels.Add(admin);
//            db.SaveChanges();
//            Console.WriteLine("Default Admin created successfully.");
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($" Error creating Admin: {ex.Message}");
//    }
//}
    app.Run();
