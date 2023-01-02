using Business.IoC;
using Core.Utilities.JWT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBLLDependencies();
builder.Services.AddControllers();
//Adding CORS
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin",builder=>builder.WithOrigins("https://localhost:4200"));
});
//JWT registration
var token = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication();






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
app.UseCors(builder=>builder.WithOrigins("https://localhost:4200").AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
