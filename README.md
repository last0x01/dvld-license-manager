# DVLD Management System

![Status](https://img.shields.io/badge/Status-Completed-brightgreen)
![Version](https://img.shields.io/badge/Version-1.0.0-blue)
![C#](https://img.shields.io/badge/language-C%23-178600?logo=csharp&logoColor=white)
![WinForms](https://img.shields.io/badge/UI-WinForms-512BD4)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![ADO.NET](https://img.shields.io/badge/ADO.NET-Data%20Access-orange)
![Architecture](https://img.shields.io/badge/Architecture-3--Tier-blue)

A complete desktop application for managing **driving-license requests — local and international** — built with C# WinForms and **ADO.NET** on **SQL Server**, using a clean **3-tier architecture** (Presentation, Business Logic, Data Access). Modular, maintainable, extensible, and reusable.

---

## Demo Credentials

| Username | Password |
| -------- | -------- |
| `User`   | `1111`   |

---

## Architecture

```
Presentation Layer (WinForms UI)
        │
        ▼
   Business Layer (clsUser, clsLicense, clsApplication, clsTest, ...)
        │
        ▼
   Data Access Layer (ADO.NET + SQL Server)
```

- **Presentation Layer** — `DVLD Presentaion-Layer` (Forms: People, Applications, Drivers, Licenses, Tests, Users, Login, MainMenu)
- **Business Logic Layer** — `DVLD Business-Layer`
- **Data Access Layer** — `DVLD DataAccess-Layer`
- **Database** — `Database/` (sample database backup included)

---

## Features

### People
- Add, update, delete, find, and list people
- Country selection and formatting helpers

### Applications
- **Local Driving License Applications**: apply, schedule tests, issue the license
- **International Driving License Applications**: issue international licenses
- Application types and application management
- Detained licenses management

### Licenses
- Issue local licenses per license class
- Issue international licenses
- **Detain / release** licenses (`clsDetainedLicensesData`)

### Tests
- Test appointment scheduling
- Written and practical test management per type (`clsTestTypesData`, `clsTestAppointment`)

### Drivers
- Driver records tied to license holders

### Users & Login
- User management with login screen
- "Remember me" for username/password
- Session/user context via `clsGlobal`

---

## Requirements

- Visual Studio 2022
- .NET Framework
- SQL Server (local or remote)
- ADO.NET

---

## Getting Started

1. Clone the repository:

   ```bash
   git clone https://github.com/last0x01/dvld-license-manager.git
   ```

2. Open `DVLD Project.sln` in Visual Studio.

3. **Restore the sample database** from `Database/P_DVLD` (this is a SQL Server database backup):

   ```sql
   RESTORE DATABASE P_DVLD
   FROM DISK = 'C:\path\to\P_DVLD'
   WITH REPLACE;
   ```

4. Set the connection string in the Data Access Layer (`clsDataAccessSettings.cs`).

5. Build and run the project.

6. Log in with the demo credentials (`User` / `1111`).

---

## License

This project is for educational/portfolio purposes.