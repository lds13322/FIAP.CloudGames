# 🎮 FIAP.CloudGames API

Projeto desenvolvido individualmente para a **Fase 1 do Tech Challenge de Arquitetura de Sistemas - FIAP Pós Tech**.

## 📌 Descrição

API REST para um sistema de jogos digitais, onde usuários podem:

- Cadastrar-se e autenticar-se via JWT
- Consultar catálogo de jogos
- Adicionar jogos à própria biblioteca
- Consultar sua biblioteca de jogos
- Admins podem cadastrar novos jogos

---

## 🛠️ Tecnologias Utilizadas

- .NET 8
- ASP.NET Core
- Entity Framework Core (SQLite)
- JWT (JSON Web Token)
- Swagger (documentação e testes)
- Arquitetura em camadas (Models, Services, Controllers)

---

## 🚀 Como Executar Localmente

1. **Requisitos**:
   - .NET SDK 8 instalado
   - VS Code ou Visual Studio

2. **Restaurar pacotes e compilar**:
   ```bash
   dotnet restore
   dotnet build
   ```

3. **Rodar o projeto**:
   ```bash
   dotnet run
   ```

4. **Acessar o Swagger UI**:
   ```
   http://localhost:5000/swagger
   ```

---

## 🔐 Testando a Autenticação

### 1. Cadastrar um usuário:
```json
POST /api/users/register
{
  "name": "Lucas",
  "email": "lucas@email.com",
  "password": "123456",
  "role": "Admin"
}
```

### 2. Fazer login:
```json
POST /api/users/login
{
  "email": "lucas@email.com",
  "password": "123456"
}
```

🔁 Copie o token JWT retornado e clique em "Authorize" no Swagger para testar endpoints protegidos.

---

## 🧪 Endpoints Principais

- `POST /api/users/register` - Cadastrar usuário
- `POST /api/users/login` - Login com JWT
- `POST /api/game` - Adicionar jogo (Admin)
- `GET /api/game` - Listar jogos
- `POST /api/game/add/{gameId}` - Adicionar jogo à biblioteca
- `GET /api/game/library` - Ver biblioteca do usuário

---

## 👤 Autor

**Lucas dos Santos**  
Discord: `lds133`

---

## 🎓 FIAP Pós Tech  
Arquitetura de Sistemas · Tech Challenge Fase 1
