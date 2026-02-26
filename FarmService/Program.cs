var builder = WebApplication.CreateBuilder(args);

// Adiciona o suporte aos Controllers
builder.Services.AddControllers();

// Configura o Swagger para podermos testar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Habilita as rotas da nossa API
app.MapControllers();

app.Run();