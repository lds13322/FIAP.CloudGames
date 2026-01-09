# 🎮 FIAP CloudGames - Tech Challenge 4
**Aluno:** Lucas
**Curso:** Arquitetura de Sistemas .NET com Azure

## 📋 Objetivo do Projeto
Refatoração de uma aplicação monolítica de gestão de jogos para uma arquitetura de **microsserviços**, utilizando containerização com **Docker** e orquestração em nuvem com **Kubernetes (AKS)**.

## 🏗️ Arquitetura da Solução

O sistema foi desacoplado em componentes independentes para garantir escalabilidade:

1.  **API (Producer):** Recebe requisições HTTP e envia mensagens para a fila.
2.  **RabbitMQ (Broker):** Gerencia a fila de processamento, garantindo comunicação assíncrona.
3.  **Worker (Consumer):** Processa as mensagens da fila em background.
4.  **Azure AKS:** Orquestra todos os containers garantindo alta disponibilidade.

### 🔄 Fluxo de Dados
`Cliente` ➤ `LoadBalancer` ➤ `API` ➤ `RabbitMQ` ➤ `Worker`

---

## 🚀 Guia de Execução

### Pré-requisitos
* Cluster AKS ativo no Azure.
* `kubectl` configurado localmente.

### Como rodar (Infraestrutura)
Os manifestos Kubernetes estão na pasta `k8s/`.

1. **Conectar ao Cluster:**
   ```bash
   az aks get-credentials --resource-group RG_FIAP_TechChallenge4 --name ClusterFiapGames

---

## 📜 Histórico de Criação (Comandos Utilizados)

Para fins de documentação, estes foram os comandos utilizados na CLI do Azure e Docker para provisionar a infraestrutura do zero:

### 1. Criação das Imagens Docker
```bash
# Build e Tag
docker build -t fiap-games-api:latest -f Microsservico_Jogos_API/Dockerfile .
docker build -t fiap-games-worker:latest -f Microsservico_Pagamentos_Worker/Dockerfile .

# Login e Push para o Azure Container Registry (ACR)
az acr login --name acrfiaplucas
docker push acrfiaplucas.azurecr.io/fiap-games-api:latest
docker push acrfiaplucas.azurecr.io/fiap-games-worker:latest

2. # Criação do Cluster gerenciado
az aks create --resource-group RG_FIAP_TechChallenge4 --name ClusterFiapGames --node-count 1 --generate-ssh-keys

# Vínculo entre AKS e ACR (Permissão de pull)
az aks update --name ClusterFiapGames --resource-group RG_FIAP_TechChallenge4 --attach-acr acrfiaplucas



🛠️ Tecnologias Utilizadas
.NET 8: Framework principal.

Docker: Criação das imagens.

Kubernetes (AKS): Orquestração.

RabbitMQ: Mensageria.

Azure Container Registry (ACR): Repositório de imagens privado.