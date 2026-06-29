# OOPExample
Учебный fullstack-проект для отработки принципов ООП, SOLID, Rest API, работы с базами данных (SSMS) через Dapper и FluentMigrator, и современного .NET/C# стека, ts и vue.js, 
UI-фреймворка element plus, реализации технологии tg bot

## 🚀 Технологии
**Backend:**
- C# / .NET
- Dapper (ORM)
- FluentMigrator
- TPL / async / await / Task
- REST API
- MSSQL 
- Dependency Injection
- Map
- Middleware

**Frontend:**
- Vue 3
- TypeScript
- Element plus
- Store, Cookie, LocalStorage

##Основные возможности

- Реализация CRUD-операций через REST API
- Асинхронная работа с базой данных (Dapper + SQL, FluentMifrator - дублирующая реализация)
- Примеры использования ООП: наследование, полиморфизм, инкапсуляция
- Применение принципов SOLID и REST
- Фронтенд на Vue для взаимодействия с API
- Покрытие логгированием и try/catch с отслеживанием ошибок на каждом этапе
- телеграмм бот для дополнительного информирования

## Запуск проекта

### Backend (UniversityProject)
1. Открыть решение UniversityProject.sln в Visual Studio / Rider
2. Настроить строку подключения в appsettings.json\
3. Запустить проект (миграции автоматически, запуск весь автоматический)

### Frontend
```bash
cd frontend-folder
npm install
npm run dev