# Technology Stack

## Core Technologies
- **Language:** C#
- **Framework:** .NET Framework 4.8
- **UI Framework:** Windows Forms (WinForms)

## Database Layer
- **RDBMS:** Microsoft SQL Server Express (`.\SQLEXPRESS`)
- **Data Access:** ADO.NET (`System.Data.SqlClient`)
  - Used for parameterized SQL queries to prevent SQL Injection.
  - `SqlDataReader` and `SqlDataAdapter` for reading and mapping data to grids.

## Security
- **Cryptography:** `System.Security.Cryptography.Aes`
  - AES-256 symmetric encryption is used for encrypting sensitive medical data at rest (Diagnoses, Prescriptions, and dynamic Report Results).

## Libraries & Dependencies
- **iTextSharp (v5.5.13.3)**
  - A popular open-source PDF generation library for .NET.
  - Used to export Prescriptions and Medical Reports into stylized "Sticky Note" format PDF documents.

## Iconography & Assets
- **Unicode Emoji Icons:** 
  - The application uses OS-native emojis (e.g., 🚪, ℹ️) as lightweight vector icons on buttons and labels, avoiding the need to ship heavy external icon packs or image assets.

## Architecture Pattern
- **Event-Driven UI:** Standard WinForms event handlers (Clicks, TextChanged) mapped directly to data access layer code (Code-Behind architecture).
- **Dynamic UI Generation:** The report system relies heavily on dynamic control instantiation, where WinForms controls (`TextBox`, `DateTimePicker`, `NumericUpDown`) are generated at runtime based on the database schema inside `TemplateFields`.
