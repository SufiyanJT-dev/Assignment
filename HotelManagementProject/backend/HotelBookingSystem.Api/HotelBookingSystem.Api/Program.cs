using HotelBookingSystem.Api.Middleware;
using HotelBookingSystem.Appilcation.Auth.Query;
using HotelBookingSystem.Appilcation.Common;
using HotelBookingSystem.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPasswordHasher<HotelBookingSystem.Domain.Entities.Customer>, PasswordHasher<HotelBookingSystem.Domain.Entities.Customer>>();
builder.Services.AddScoped<IPasswordHasher<HotelBookingSystem.Domain.Entities.Employee>, PasswordHasher<HotelBookingSystem.Domain.Entities.Employee>>();
builder.Services.AddSingleton<JwtTokenGenerator>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAuthentication(options =>
{ 
options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}
).AddJwtBearer(options => {
        options.RequireHttpsMetadata=false;
        options.SaveToken=true;
    var jwtSection = builder.Configuration.GetSection("JwtSettings");
        var key = jwtSection.GetValue<string>("Key");
        var issuer = jwtSection.GetValue<string>("Issuer");
        var audience = jwtSection.GetValue<string>("Audience");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });



builder.Services.AddDbContext<HotelDbContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // ✅ Your Angular app origin
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // ✅ This is critical
    });
});


builder.Services.AddMediatR(x =>
    x.RegisterServicesFromAssembly(Assembly.Load("HotelBookingSystem.Appilcation")));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();

app.Run();
 