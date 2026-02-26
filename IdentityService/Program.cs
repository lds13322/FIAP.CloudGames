using IdentityService.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os Controllers (ISSO AQUI É ESSENCIAL)
builder.Services.AddControllers();

// Injeção de Dependência do nosso serviço de Token
builder.Services.AddScoped<TokenService>();

// Configura o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();