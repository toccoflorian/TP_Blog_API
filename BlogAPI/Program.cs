using BlogAPI;
using BlogAPI.IRepository;
using BlogAPI.Models;
using BlogAPI.Repositories;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(dbContextBuilderOptions =>
    dbContextBuilderOptions.UseSqlServer(builder.Configuration.GetConnectionString("ContextCS"))
);

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Context>();

// Add services to the container.

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IThemeRepository, ThemeRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<InitializerUser>();

builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
    jsonOptions.JsonSerializerOptions.ReferenceHandler = 
    ReferenceHandler.IgnoreCycles
);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swaggerOptions =>
{
    //swaggerOptions.SwaggerDoc("V1", new OpenApiInfo { Title = "Blog API V1", Version = "V1" });
    //string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //swaggerOptions.IncludeXmlComments(xmlPath);

    swaggerOptions.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    swaggerOptions.OperationFilter<SecurityRequirementsOperationFilter>();
});



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<AppUser>();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

