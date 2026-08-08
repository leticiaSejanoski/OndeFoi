using Microsoft.Extensions.Options;
using OndeFoi.Data;
using Microsoft.EntityFrameworkCore;
using OndeFoi.Repositories;
using OndeFoi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using OndeFoi.Controllers;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<GastoRepository>();
builder.Services.AddScoped<GastoService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<DashboardController>();
builder.Services.AddScoped<DashboardService>();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //define o método de autenticação como Bearer
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // faz a autenticação
}
)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
    };
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header

    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("React",
      policy =>
      {
          policy
          .WithOrigins("http://localhost:5173")
          .AllowAnyHeader()
          .AllowAnyMethod();
      });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("React");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
