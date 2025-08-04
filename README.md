# 🎮 FIAP.CloudGames API - Fase 2

Projeto desenvolvido individualmente para o **Tech Challenge de Arquitetura de Sistemas - FIAP Pós Tech**.

## 📌 Descrição

API REST para um sistema de jogos digitais, onde usuários podem se cadastrar, gerenciar sua biblioteca de jogos e administradores podem gerenciar o catálogo. Esta segunda fase do projeto focou em tornar a aplicação escalável, confiável e monitorável através de práticas DevOps e tecnologias de nuvem.

---

## 🏛️ Arquitetura e Fluxo DevOps

O projeto utiliza uma arquitetura moderna baseada em contêineres e um fluxo de CI/CD para automação de builds e deploys.

1.  **Código-fonte:** Versionado no GitHub.
2.  **CI (Integração Contínua):** O GitHub Actions é acionado a cada `push`. Ele constrói a imagem Docker da aplicação e a envia para o Docker Hub.
3.  **CD (Entrega Contínua):** Após a conclusão da CI, uma segunda pipeline de GitHub Actions é acionada. Ela se conecta ao Microsoft Azure e realiza o deploy da nova imagem no App Service.
4.  **Infraestrutura na Nuvem:** A aplicação roda em um **Azure App Service**, garantindo escalabilidade. Os dados são persistidos em um **Azure SQL Database**, um banco de dados gerenciado e robusto.
5.  **Monitoramento:** O **Azure Application Insights** coleta métricas de performance e falhas da aplicação em tempo real.

---

## 🛠️ Tecnologias Utilizadas

-   **.NET 8** e ASP.NET Core
-   **Entity Framework Core**
-   **Docker:** Para containerização da aplicação.
-   **GitHub Actions:** Para automação de CI/CD.
-   **Microsoft Azure:**
    -   **Azure App Service:** Para hospedar a aplicação em contêiner.
    -   **Azure SQL Database:** Para persistência dos dados em produção.
    -   **Application Insights:** Para monitoramento e telemetria.
-   **Docker Hub:** Como registro para as imagens Docker.
-   **JWT (JSON Web Token):** Para autenticação.
-   **Swagger:** Para documentação da API.

---

## 🚀 Aplicação na Nuvem

A API está publicada e disponível no Azure. Você pode acessar a documentação interativa (Swagger UI) através do seguinte link:

**[https://fiap-cloudgames-api-lucas-fubmcvh9e7b7dmc0.brazilsouth-01.azurewebsites.net/swagger/index.html](https://fiap-cloudgames-api-lucas-fubmcvh9e7b7dmc0.brazilsouth-01.azurewebsites.net/swagger/index.html)**

---

## ⚙️ Como Executar Localmente

*(Pode manter as instruções que você já tinha para rodar localmente com `dotnet run` ou adicionar as de Docker)*

1.  **Requisitos**:
    -   .NET SDK 8 instalado
    -   Docker Desktop instalado
2.  **Construir a imagem Docker**:
    ```bash
    docker build -t fiap-cloudgames-api .
    ```
3.  **Rodar o contêiner**:
    ```bash
    # (Atenção: A conexão com o banco de dados precisará ser ajustada para o ambiente local)
    docker run -p 5000:8080 fiap-cloudgames-api
    ```

---

## 👤 Autor

**Lucas dos Santos**
Discord: `lds133`

---
