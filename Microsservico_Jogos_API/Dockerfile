# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura as dependências
COPY ["FIAP.CloudGames.WebApi.csproj", "./"]
RUN dotnet restore "FIAP.CloudGames.WebApi.csproj"

# Copia o restante do código e compila
COPY . .
RUN dotnet publish "FIAP.CloudGames.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio Final (Runtime) - Imagem Otimizada
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Define a porta que a API expõe (padrão .NET 8 é 8080)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "FIAP.CloudGames.WebApi.dll"]