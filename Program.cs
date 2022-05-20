using System.Text;
using CarrefourApi.Repository;
using CarrefourApi.Repository.SqliteRepository;
using CarrefourApi.Security;
using CarrefourApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var key = Encoding.ASCII.GetBytes(TokenService.Secret);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(config =>
    {
        config.AllowAnyOrigin();
        config.AllowAnyMethod();
        config.AllowAnyHeader();
    });
});

//informando para a aplicação que quero trabalhar com autenticação, autorização e que o formato do Token é o JWT.
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSendGrid(options =>
{
    options.ApiKey = builder.Configuration
    .GetSection("SendGridEmailSettings").GetValue<string>("APIKey");
});

builder.Services.AddSingleton<TokenService, TokenService>();
builder.Services.AddSingleton<IInscricaoRepository, SqliteInscricaoRepository>();
builder.Services.AddSingleton<IUsuarioRepository, SqliteUsuarioRepository>();
builder.Services.AddSingleton<IProgramaRepository, SqliteProgramaRepository>();
builder.Services.AddSingleton<IEmail, SendGridService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// app.UseHttpsRedirection();
app.UseStaticFiles();

//informo que de fato estou usando autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors();

app.Run();
