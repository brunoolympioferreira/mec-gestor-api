# ⚙️ MecGestor

**MecGestor** é um sistema **SaaS** voltado para **oficinas mecânicas**, desenvolvido para facilitar o gerenciamento completo das operações diárias, desde o cadastro de clientes e veículos até o controle de ordens de serviço e faturamento.  
O objetivo principal é oferecer uma solução moderna, intuitiva e acessível, permitindo que o mecânico ou gestor acompanhe todas as atividades da oficina em um único lugar.

---

## 🚀 Visão Geral

O **MecGestor** tem como proposta digitalizar e automatizar os principais processos de uma oficina mecânica.  
Através de um painel web, o usuário poderá gerenciar clientes, veículos, serviços realizados, peças utilizadas e ordens de serviço de forma simples e organizada.

---

## 🧩 Módulos Principais (Roadmap)

- **Cadastro de Clientes:** registro de informações básicas e histórico de serviços.  
- **Cadastro de Veículos:** vínculo direto entre o cliente e seus veículos.  
- **Gestão de Ordens de Serviço (OS):** emissão, acompanhamento e controle de status.  
- **Controle de Serviços e Peças:** registro detalhado de cada item utilizado na OS.  
- **Faturamento e Pagamentos:** acompanhamento de valores, formas de pagamento e status financeiro.  
- **Relatórios e Indicadores:** visualização de métricas e desempenho da oficina (em fases futuras).  

---

## 🧱 Sprint 1 — Módulo de Ordem de Serviço (MVP)

A **Sprint 1** tem como objetivo estruturar a base do sistema e desenvolver o módulo inicial de **Ordem de Serviço (OS)**.  
Nesta versão, serão entregues as seguintes funcionalidades:

- Modelagem de dados para entidades principais (Cliente, Veículo, OS, Itens da OS).  
- CRUD de Ordens de Serviço.  
- Cadastro e vínculo de clientes e veículos à OS.  
- Controle de status da OS (Aberta, Em andamento, Concluída, Cancelada).  
- Cálculo automático do valor total (serviços + peças).  
- Integração inicial com o módulo de autenticação (usuário administrador).  

---

## 🛠️ Stack Tecnológica

- **Backend:** .NET 8 (C#) — API RESTful  
- **Frontend:** Angular 17 + Angular Material  
- **Banco de Dados:** SQL Server  
- **Arquitetura:** Clean Architecture + Entity Framework Core  
- **Autenticação:** JWT (JSON Web Token)  
- **Versionamento:** Git / GitHub  

---

## ⚙️ Como Executar Localmente

### Pré-requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- [Node.js 20+](https://nodejs.org/)  
- [Angular CLI](https://angular.io/cli)  
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

### Passos

#### 🔹 Backend
```bash
cd MecGestor.Api
dotnet restore
dotnet run
