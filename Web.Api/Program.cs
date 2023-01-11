using Business.IoC;
using Core.Utilities.JWT;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBLLDependencies();
builder.Services.AddControllers().AddFluentValidation(config=>config.RegisterValidatorsFromAssemblyContaining(typeof(Program)));
//Adding CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin",builder=>builder.WithOrigins("https://localhost:7031/"));
});
//JWT registration
var token = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer= true,
        ValidateAudience= true,
        ValidateLifetime= false,

        ValidAudience=token.Audience,
        ValidIssuer=token.Issuer,
        ValidateIssuerSigningKey=true,
        IssuerSigningKey=SecurityKeyHelper.CreateSecurityKey(token.SecurityKey)
    };
});






// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder=>builder.WithOrigins("https://localhost:7031/").AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
