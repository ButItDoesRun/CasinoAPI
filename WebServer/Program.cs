﻿using DataLayer;
using DataLayer.DataServiceInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebServer.Services;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


// Add services to the container.
builder.Services.AddControllers();


//DATASERVICES
/* CASINO FRAMEWORK */
builder.Services.AddSingleton<IDataserviceMoneyPot, DataserviceMoneyPot>();
builder.Services.AddSingleton<IDataserviceBets, DataserviceBets>();
builder.Services.AddSingleton<IDataserviceGame, DataserviceGame>();
builder.Services.AddSingleton<IDataservicePlayer, DataservicePlayer>();
builder.Services.AddSingleton<IDataserviceBet, DataserviceBet>();

/*Auto Mapper*/
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/*Hashing*/
builder.Services.AddSingleton<Hashing>();

/*JWT Authentication*/
builder.Services.AddAuthentication(opt => 
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; 
})
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        //opt.SaveToken = true;
        opt.Authority = builder.Configuration.GetValue<string>("Jwt:Authority");
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = false,      
            ValidateIssuerSigningKey = true,               
            ClockSkew = TimeSpan.Zero
        };
        
    });

//builder.Services.AddAuthorization();
//builder.Services.AddAuthorization(auth =>
//{
//    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
//        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
//        .RequireAuthenticatedUser().Build());
//});

// Add configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();





builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


/*DateTime Converter */
builder.Services.AddControllers()
               .AddJsonOptions(options =>
               {
                   options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
               });

builder.WebHost.UseUrls("http://*:5001", "https://localhost:5001");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();
IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;

app.MapControllers();
app.Run();