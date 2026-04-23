# Michael Page Fullstack Challenge

## Overview
This project implements a **Task Management System** using **.NET Web API**, following **Clean Architecture**, CQRS with MediatR, and SQL Server.

It supports user and task management, including advanced filtering and JSON data handling in SQL Server.

---

## Architecture

### Solution structure

```
src/
 ├── MichaelPageChallenge.API
 ├── MichaelPageChallenge.Application
 ├── MichaelPageChallenge.Domain
 ├── MichaelPageChallenge.Infrastructure

tests/
 ├── MichaelPageChallenge.Tests
```

### Layers

- **API** → Controllers, middleware  
- **Application** → Commands, Queries, Validators  
- **Domain** → Entities, business rules  
- **Infrastructure** → EF Core, repositories  

---

## Getting Started

### Run the API

dotnet run --project src/MichaelPageChallenge.API

Swagger available at:

https://localhost:7105/swagger

---

## Database Setup

### Apply migrations

dotnet ef database update --project src/MichaelPageChallenge.Infrastructure --startup-project src/MichaelPageChallenge.API

---

## API Endpoints

### Users

- POST /api/users  
- GET /api/users  

---

### Tasks

- POST /api/tasks  
- GET /api/tasks  
- PUT /api/tasks/{id}/status  

---

### Filtering & Sorting

GET /api/tasks?userId={guid}&status={status}&order=asc|desc

Supports:
- Filter by user  
- Filter by status  
- Sort by creation date  

---

## Business Rules

- Task title is required  
- Task must have an assigned user  
- Task cannot transition directly from Pending → Done  

---

## Validations

Implemented using FluentValidation:
- Input validation at application layer  
- Domain invariants enforced in entities  

---

## JSON Support in SQL Server

Tasks include an AdditionalData column (NVARCHAR(MAX)) to store JSON.

### Example JSON

{
  "priority": "High",
  "estimatedDate": "2026-05-01",
  "tags": ["backend", "urgent"],
  "metadata": {
    "source": "api",
    "version": 1
  }
}

---

## JSON Validation (SQL)

ALTER TABLE Tasks  
ADD CONSTRAINT CK_Tasks_AdditionalData_IsJson  
CHECK (  
    AdditionalData IS NULL  
    OR AdditionalData = ''  
    OR ISJSON(AdditionalData) = 1  
);

---

## JSON Queries

### Get a JSON field

SELECT  
    Id,  
    Title,  
    JSON_VALUE(AdditionalData, '$.priority') AS Priority  
FROM Tasks;

---

### Filter by JSON value

SELECT *  
FROM Tasks  
WHERE JSON_VALUE(AdditionalData, '$.priority') = 'High';

---

### Extract JSON object

SELECT  
    JSON_QUERY(AdditionalData, '$.metadata') AS Metadata  
FROM Tasks;

---

### Work with arrays

SELECT *  
FROM Tasks  
CROSS APPLY OPENJSON(AdditionalData, '$.tags');

---

### Update JSON field

UPDATE Tasks  
SET AdditionalData = JSON_MODIFY(AdditionalData, '$.priority', 'Low')  
WHERE Id = 'YOUR_ID';

---

### Combined example

SELECT  
    Id,  
    Title,  
    JSON_VALUE(AdditionalData, '$.priority') AS Priority,  
    JSON_VALUE(AdditionalData, '$.estimatedDate') AS EstimatedDate  
FROM Tasks  
WHERE JSON_VALUE(AdditionalData, '$.priority') = 'High'  
ORDER BY CreatedAt DESC;

---

## Technical Highlights

- Clean Architecture  
- CQRS with MediatR  
- FluentValidation pipeline  
- Global exception handling  
- SQL Server JSON functions (ISJSON, JSON_VALUE, JSON_QUERY, OPENJSON)  