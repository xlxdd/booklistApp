using booklistDomain.DomainService;
using booklistDomain.Entities.Identity;
using booklistDomain.Helpers;
using booklistDomain.Interfaces;
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
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped<IBookRepository,BookRepository>();
builder.Services.AddScoped<IFileService, CloudinaryFileService>();
builder.Services.AddScoped<BookDomainService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),new MySqlServerVersion(new Version(builder.Configuration["Database:Version"])));
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

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());//注册验证器
builder.Services.AddFluentValidationAutoValidation();//启用自动验证
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
