# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

# Estágio 2: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Define a porta que a aplicação vai escutar
ENV ASPNETCORE_URLS=http://+:8181

# Ponto de entrada ajustado
ENTRYPOINT ["dotnet", "FIAP.CloudGames.WebApi.dll"]