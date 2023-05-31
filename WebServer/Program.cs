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
using Microsoft.Extensions.Logging.Console;


var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


// Add services to the container.
builder.Services.AddControllers();

//Logging for monitoring 

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});



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

/*user authentication*/
builder.Services.AddSingleton<Authentication>();

/*Whitelist*/
builder.Services.Configure<IPWhitelistOptions>(builder.Configuration.GetSection("IPWhitelistOptions"));


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
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value)),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = false,      
            ValidateIssuerSigningKey = true,               
            ClockSkew = TimeSpan.Zero
        };
        
    });


//CORS with named policy and middleware
/*The specified URL must not contain a trailing slash (/). 
 * If the URL terminates with /, the comparison returns false 
 * and no header is returned.*/
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5001",
                              "https://coinpusher.dk")
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

//Specifies URL the webhost listens on
builder.WebHost.UseUrls("https://*:5001", "https://localhost:5001");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseIPWhitelist();

app.UseAuthentication();
app.UseAuthorization();

app.Logger.LogInformation("Starting the app");


app.MapControllers();
app.Run();