📄 Disponível em: [Português](README.md) | [English](README.en.md)

# 🎮 Tech Challenge - Pós FIAP (Venda de Jogos Educacionais)

Este projeto foi desenvolvido como parte do Tech Challenge da pós-graduação em Arquitetura de Soluções na FIAP. O objetivo é construir uma API RESTful moderna utilizando .NET 8, com foco na gestão de jogos educacionais, incluindo funcionalidades como cadastro de usuários, jogos adquiridos, administração e biblioteca virtual.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8 (Web API)
- C# 12
- Entity Framework Core
- Swagger / Swashbuckle
- SQL Server (Migrations)
- Clean Architecture (separação em camadas)
- Docker (planejado)
- GitHub Projects + Kanban
- JWT Authentication
- AutoMapper
- xUnit (Testes Unitários)

---

## 📄 Documentos
- EventStorming e Domain Storytelling(print): https://miro.com/app/board/uXjVI-mHcaQ=/?share_link_id=658453906053

---

## 🧱 Estrutura do Projeto

```
FiapPosTechChallenge/
├── src/
│   ├── Fiap.Games.Api/
│   ├── Fiap.Games.Domain/
│   ├── Fiap.Games.Infrastructure/
│   └── Fiap.Games.Tests/
├── README.md
```

---

## 📌 Funcionalidades Implementadas

- Cadastro e listagem de usuários
- Cadastro de jogos educativos
- Atribuição de jogos comprados aos usuários
- CRUD de administradores e jogos
- Autenticação de usuários com JWT
- Cobertura de testes unitários com xUnit

---

## ▶️ Como Executar

1. Clonar o repositório:
```bash
git clone https://github.com/seu-usuario/FiapPosTechChallenge.git
```

2. Restaurar pacotes:
```bash
dotnet restore
```

3. Rodar aplicação:
```bash
dotnet run --project src/Fiap.Games.Api
```

4. Acessar documentação da API:
```
http://localhost:5000/swagger
```
O migration ira rodar automaticamente, cria as roles e o usuario admin 
```
email: admin@fiap.com
senha: Fiap2025@
---

## 🧪 Como cadastrar usuários

1. Endpoint:
```
POST /api/v1/User/Create
```

2. Payload de exemplo:
```json
{
  "password": "SenhaForte123",
  "email": "usuario@teste.com",
  "firstName": "Fiap",
  "lastName": "pós",
  "birthdate": "2025-05-31T17:17:11.672Z",
  "nickName": "apelidoDoUsuario"
}
```
 Testes automatizados
---

Testes Unitários (xUnit)
Os testes estão localizados em Core.PosTech8Nett.Test.

Para executá-los:
```bash
dotnet test Core.PosTech8Nett.Test
```

```
Testes BDD com SpecFlow
```

Os testes de comportamento estão localizados em Core.PosTech8Nett.BDD.

Executam cenários como:
Criaçäo de usuário.
Login
Listagem de usuários
Criaçäo de games
Listagem de games


## 🤝 Contribuidores

- Marcelo Morais dos Santos – [LinkedIn](https://www.linkedin.com/in/marcelo-morais-61584146/)
- Laerte Patrocínio – [LinkedIn](https://www.linkedin.com/in/laerte-patrocinio-19937295/)

---

## 🎓 Contexto

Este projeto representa a **Fase 1** do desafio técnico da FIAP, com entregas iterativas conforme evolução do curso e aplicação de princípios de arquitetura moderna e boas práticas de engenharia de software.
