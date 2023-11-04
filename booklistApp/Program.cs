using booklistAPI;
using booklistDomain.DomainService;
using booklistDomain.Entities.Identity;
using booklistDomain.Helpers;
using booklistDomain.Interfaces;
using booklistDomain.Interfaces.Identity;
using booklistDomain.Services;
using booklistInfrastructure;
using booklistInfrastructure.Repositories;
using booklistInfrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});//禁用自动验证
{//配置读取
    builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));
}
{//注册仓储层
    builder.Services.AddScoped<IBookRepository, BookRepository>();
    builder.Services.AddScoped<IBookListRepository, BookListRepository>();
    builder.Services.AddScoped<IIdentityRepository, IdentityRepository>();
}
{//注册服务层
    builder.Services.AddScoped<BookDomainService>();
    builder.Services.AddScoped<BookListDomainService>();
    builder.Services.AddScoped<IdentityDomainService>();
}
{//注册三方服务
    builder.Services.AddScoped<IFileService, CloudinaryFileService>();
    builder.Services.AddScoped<ISMSService, SMSService>();
    builder.Services.AddScoped<ITokenService, TokenService>();
}
builder.Services.AddMemoryCache();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(builder.Configuration["Database:Version"])));
});//注册db

JWTOptions jwtOpt = builder.Configuration.GetSection("JWT").Get<JWTOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOpt.Issuer,
                    ValidAudience = jwtOpt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOpt.Key))
                };
            });

builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add<UOW>();
    }
    );
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());//注册验证器
builder.Services.AddFluentValidationAutoValidation();//启用自动验证
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookList API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT令牌",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT"
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
            new string[] { }
        }
    });
    c.EnableAnnotations();
});
var idBuilder = builder.Services.AddIdentityCore<AppUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    //不能设定RequireUniqueEmail，否则不允许邮箱为空
    //options.User.RequireUniqueEmail = true;
    //以下两行，把GenerateEmailConfirmationTokenAsync验证码缩短
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
}
);
idBuilder = new IdentityBuilder(idBuilder.UserType, typeof(Role), builder.Services);
idBuilder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders()
    .AddRoleManager<RoleManager<Role>>()
    .AddUserManager<UserManager<AppUser>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookList API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
