# Estágio 1: Build - Usa a imagem do .NET SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia os arquivos de projeto (.csproj) e restaura as dependências primeiro
# Isso aproveita o cache do Docker, acelerando builds futuros
COPY *.csproj .
RUN dotnet restore

# Copia todo o resto do código fonte e publica a aplicação
COPY . .
RUN dotnet publish -c Release -o /app/publish --no-restore

# Estágio 2: Final - Usa a imagem do ASP.NET Runtime, que é muito menor
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia apenas a aplicação compilada do estágio de build
COPY --from=build /app/publish .

# Define a porta que a aplicação vai escutar dentro do contêiner
# É uma boa prática usar a porta 80 ou 8080 dentro do contêiner
# e mapear para a porta 5000 do seu computador quando for executar.
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Ponto de entrada: Comando para iniciar a aplicação
# IMPORTANTE: Verifique o nome exato do seu arquivo .dll na pasta de publicação
ENTRYPOINT ["dotnet", "FIAP.CloudGames.WebApi.dll"]