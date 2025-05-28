📄 Available in: [English](README.en.md) | [Português](README.md)

# 🎮 Tech Challenge - FIAP Postgraduate (Educational Games Platform)

This project was developed as part of the Tech Challenge for the postgraduate program in Software Architecture at FIAP. The goal is to build a modern RESTful API using .NET 8 for managing educational games, including user management, purchased games, administration, and a virtual library.

---

## 🚀 Technologies Used

- ASP.NET Core 8 (Web API)
- C# 12
- Entity Framework Core
- Swagger / Swashbuckle
- SQL Server (with EF Migrations)
- Clean Architecture (layered separation)
- Docker (planned)
- GitHub Projects + Kanban

---

## 🧱 Project Structure

```
FiapPosTechChallenge/
├── src/
│   ├── Fiap.Games.Api/
│   │   ├── Controllers/
│   │   └── Program.cs / Swagger config
│   ├── Fiap.Games.Domain/
│   ├── Fiap.Games.Infrastructure/
│   └── Fiap.Games.Tests/
├── README.md
```

---

## 📌 Expected Features (based on challenge)

- User registration and listing
- Educational game registration
- Assign purchased games to users
- Admin and game CRUD
- Endpoint to list user game libraries

---

## ▶️ How to Run

1. Clone the repository:
```bash
git clone https://github.com/your-user/FiapPosTechChallenge.git
```

2. Restore packages:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run --project src/Fiap.Games.Api
```

4. Open Swagger UI:
```
http://localhost:5000/swagger
```

---

## 🤝 Contributors

- Marcelo Morais dos Santos – [LinkedIn](https://www.linkedin.com/in/marcelo-morais-61584146/) 
- Laerte Patrocínio – [LinkedIn](https://www.linkedin.com/in/laertepatrocinio)

---

## 🎓 Context

This is the **Phase 1** delivery of the FIAP Tech Challenge, built iteratively throughout the course to apply modern software architecture and development practices.

## User Registration and Authentication

To register a user, use the `/api/v1/User` endpoint with a POST request. Provide the following JSON payload:

```json
{
  "email": "example@example.com",
  "nickName": "nickname123",
  "password": "StrongPassword123!",
  "confirmPassword": "StrongPassword123!"
}
```

### Authentication

Use the `/api/v1/Authentication/Login` endpoint to authenticate. Provide the following JSON payload:

```json
{
  "email": "example@example.com",
  "password": "StrongPassword123!"
}
```

A valid JWT token will be returned and must be used in the Authorization header for protected endpoints.
