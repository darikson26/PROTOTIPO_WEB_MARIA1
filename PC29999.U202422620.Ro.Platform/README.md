# Ro Prescription API

## Description

This ASP.NET Core API creates a digital prescription for a patient. It validates prescription identifiers, patient identifiers, treatment dates, dosage rules, status values, and the patient identifier consistency between the URL and request body. Prescription data is configured for persistence in the MySQL `ro_wa` schema.

The API exposes one operation: `POST /api/v1/patients/{patientId}/prescriptions`. OpenAPI JSON is available at `/openapi/v1.json`.

## Author

Darikosn Salazar

## Requirements

- .NET 10 SDK
- MySQL server
- A MySQL schema named `ro_wa`

## Database configuration

Set the `DefaultConnection` value in `appsettings.Development.json` with your MySQL username and password. Do not commit real credentials.

Create the `ro_wa` schema in MySQL Workbench, then run the EF Core migration from the project directory:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.13
dotnet ef database update
```

The application does not create or update the database automatically.

## Run

From the solution directory:

```powershell
dotnet run --project .\PC29999.U202422620.Ro.Platform\PC29999.U202422620.Ro.Platform.csproj --urls http://localhost:5000
```

Open `PC29999.U202422620.Ro.Platform\PC29999.U202422620.Ro.Platform.http` in Rider and run the request using its gutter action. Set `Accept-Language` to `en`, `en-US`, `es`, or `es-PE` to select the error message language.
