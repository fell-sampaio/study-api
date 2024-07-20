# StudyAPI

## Introduction
API project for study. Application written with .NET Core API and Entity Framework. Data layer connects to SQL Server.

## Getting Started

1. Clone this repository
2. Restore the packages by running: `dotnet restore`
3. Install the EF Core CLI by running: `dotnet tool install --global dotnet-ef`
4. Set the environment variable `ASPNETCORE_ENVIRONMENT` to `Local`
    - On Windows Command Prompt: `set ASPNETCORE_ENVIRONMENT=Local`
    - On Windows PowerShell: `$env:ASPNETCORE_ENVIRONMENT="Local"`
    - On Linux/MacOS: `export ASPNETCORE_ENVIRONMENT=Local`
5. Run the migrations on your local database
6. Run the API by navigating to the API Project and running `dotnet run`

## EF Migrations

### Create Migration

``` powershell
dotnet ef migrations add <migration-name>
```

### Run Migration

``` powershell
dotnet ef database update
```

### Generate Migration Script

``` powershell
dotnet ef migrations script --idempotent --output <path/file-name.sql>
```

### Remove Migration

``` powershell
dotnet ef migrations remove
```