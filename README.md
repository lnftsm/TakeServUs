# TakeServUs - Field Service Management Platform

## 📱 Proje Hakkında
TakeServUs is a mobile-first platform that simplifies work management for field staff and technicians, enhances customer communication, and optimises business processes.

## 🚀 Teknoloji Stack
- **Backend:** .NET Core 8.0
- **Frontend:** Flutter
- **Database:** SQL Server
- **Authentication:** JWT
- **Architecture:** Service Design Pattern

## 📁 Project Structure
TakeServUs/
├── src/
│   ├── TakeServUs.Domain/        # Core business entities ve logic
│   ├── TakeServUs.Application/   # Business services ve DTOs
│   ├── TakeServUs.Infrastructure/# External services (JWT, Email, SMS)
│   ├── TakeServUs.Persistence/   # Database context ve repositories
│   └── TakeServUs.API/           # Web API endpoints

## 🔧 Installation
1. Clone the repository
2. Run the `dotnet restore` command
3. Configure the `appsettings.json` file
4. Create the database with `dotnet ef database update`
5. Start the API with `dotnet run --project src/TakeServUs.API`

## 📝 Features
- [ ] User management (Business Owner, Technician, Customer)
- [ ] Job/task assignment and tracking
- [ ] Real-time GPS tracking
- [ ] Invoice and quotation creation
- [ ] Material inventory tracking
- [ ] Multi-language support (TR/EN)
- [ ] Theme support (Light/Dark)

## 📄 Licence
This project is proprietary and confidential.
