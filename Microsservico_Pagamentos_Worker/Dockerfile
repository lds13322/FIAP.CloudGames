# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura dependências
COPY ["FIAP.CloudGames.Pagamentos.Worker.csproj", "./"]
RUN dotnet restore "FIAP.CloudGames.Pagamentos.Worker.csproj"

# Copia o restante do código e compila
COPY . .
RUN dotnet publish "FIAP.CloudGames.Pagamentos.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final (Runtime) - Imagem Otimizada
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Workers geralmente não expõem portas HTTP, mas se tiver healthcheck, pode precisar.
# Não expomos porta explicitamente aqui pois é um processador de fila.

ENTRYPOINT ["dotnet", "FIAP.CloudGames.Pagamentos.Worker.dll"]