# Online Secure Hospital System

A secure web-based hospital management system built with Blazor Server and MudBlazor.

## Features

- **Role-based access control** with 6 user types: Patient, Doctor, Chief Doctor, Curing Doctor, Receptionist, and System Administrator
- **Secure medical records** with DES encryption and hash functions
- **Appointment management** system with scheduling and status tracking
- **Hierarchical permissions**: Curing Doctors (full access), Chief Doctors (read-only), Patients (partial access to own records)

## Security

- DES encryption for patient medical records
- SHA-256 hashing
- Role-based access control with sensitive data masking
- Username/password authentication

## Technology Stack

- **Frontend**: Blazor Server, MudBlazor
- **Backend**: .NET, Entity Framework
- **Database**: SQLite

## Installation

1. Clone the repository
2. Run `dotnet ef database update`
3. Run `dotnet run`

## User Roles
- **System Admin**: User management
- **Receptionist**: Patient registration
- **Chief Doctor**: Doctor-patient assignments, read access to records
- **Curing Doctor**: Full medical record management
- **Consulting Doctor**: Specialized consultations
- **Patient**: View own records, book appointments

## A default user is created with username salahiadmin and with password 123456 to perform other operations and be able to use the system.
