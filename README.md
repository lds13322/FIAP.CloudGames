# 🌾 AgroSolutions - Plataforma IoT (MVP)

> **FIAP - Tech Challenge (Fase 5)**
> Projeto final focado em Arquitetura de Microsserviços, Mensageria, DevOps e Segurança (LGPD).

Este repositório contém o Produto Mínimo Viável (MVP) da plataforma IoT **AgroSolutions**, projetada para receber, processar e monitorar dados de sensores agrícolas em tempo real. O sistema visa ajudar produtores rurais a otimizar a irrigação e prevenir perdas na safra, enviando alertas de umidade e temperatura.

---

## 🏗️ Desenho da Arquitetura da Solução

A solução foi desenhada utilizando uma arquitetura orientada a eventos e baseada em microsserviços, garantindo escalabilidade, baixo acoplamento e processamento assíncrono.

### Componentes Principais:
1. **IoT Field Sensors (Simulação):** Dispositivos espalhados pelos talhões que enviam dados de umidade, temperatura e precipitação.
2. **IngestionService (API de Ingestão):** Porta de entrada dos dados. Recebe as medições e publica instantaneamente em uma fila de mensageria para não travar a recepção.
3. **Message Broker (RabbitMQ):** Fila de processamento assíncrono (`sensor_data_queue`). Garante que nenhum dado seja perdido mesmo em picos de tráfego.
4. **AlertService (Worker / Consumer):** Serviço em background que consome a fila do RabbitMQ, aplica as regras de negócio (ex: alerta se a umidade for < 30%) e notifica o produtor.
5. **IdentityService (Autenticação e LGPD):** Serviço de login responsável por autenticar o produtor e gerar Tokens JWT, assegurando os princípios de *Privacy by Design*.
6. **FarmService (Cadastro):** API responsável pelo registro da propriedade rural e demarcação dos talhões.

### Requisitos Técnicos Atendidos (Fase 5)
- ✔️ **Arquitetura em Microsserviços:** Divisão clara de responsabilidades (SOLID).
- ✔️ **Mensageria Obrigatória:** Uso do RabbitMQ para comunicação assíncrona.
- ✔️ **Segurança e LGPD:** Autenticação via JWT, minimização de dados no tráfego e acesso restrito.
- ✔️ **Orquestração de Contêineres:** Manifestos do Kubernetes (K8s) com LoadBalancer e escalabilidade (Replicas).
- ✔️ **DevOps & CI/CD:** Pipeline automatizado no GitHub Actions para Build e Testes Unitários.
- ✔️ **Testes Automatizados:** Implementação de testes unitários com xUnit.
- ✔️ **Infraestrutura via Docker:** Banco de dados e RabbitMQ orquestrados via `docker-compose`.

---

## 💻 Tecnologias Utilizadas

* **Linguagem/Framework:** .NET 8 (C#)
* **Mensageria:** RabbitMQ
* **Autenticação:** JSON Web Token (JWT)
* **Conteinerização e Orquestração:** Docker, Docker Compose, Kubernetes (K8s)
* **CI/CD:** GitHub Actions
* **Testes:** xUnit
* **Documentação de API:** Swagger (OpenAPI)

---

## 🚀 Como Executar o Projeto Localmente

### 1. Subir a Infraestrutura Base (Docker)
Antes de rodar as APIs, é necessário subir o RabbitMQ. Na raiz do projeto, execute:
```bash
docker-compose up -d

Acesse o painel do RabbitMQ em http://localhost:15672 (User: guest / Pass: guest).

2. Rodar os Microsserviços
Abra terminais separados para cada serviço e execute os comandos abaixo. O Swagger estará disponível na porta gerada pelo console (http://localhost:PORTA/swagger).

Serviço de Identidade (Login):

Bash
dotnet run --project IdentityService/IdentityService.csproj
Serviço de Fazendas (Cadastro):

Bash
dotnet run --project FarmService/FarmService.csproj
Serviço de Alertas (Worker em Background):

Bash
dotnet run --project AlertService/AlertService.csproj
Serviço de Ingestão (Recebimento de Dados IoT):

Bash
dotnet run --project IngestionService/IngestionService.csproj
3. Testando o Fluxo de Mensageria
Com o AlertService rodando em um terminal e o IngestionService em outro, abra o Swagger do Ingestion.

Envie um payload POST para /api/Sensor com umidade baixa (ex: 25%):

JSON
{
  "talhaoId": "Talhao-Sul-01",
  "temperatura": 28.5,
  "umidade": 25.0,
  "precipitacao": 0
}
Observe o terminal do AlertService disparar automaticamente o [ALERTA DE SECA] provando a comunicação assíncrona com sucesso.

🔒 Considerações sobre LGPD e Segurança
A arquitetura foi baseada no conceito de Privacy by Design. O IdentityService não trafega dados sensíveis abertos na rede. O login gera um Token JWT criptografado com ciclo de vida (expiração) definido, garantindo que os dados do produtor rural sejam manuseados segundo as diretrizes da Lei Geral de Proteção de Dados (LGPD).