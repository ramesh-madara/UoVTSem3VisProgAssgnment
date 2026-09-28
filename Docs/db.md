# Database Documentation

## Connection Details
- **Server:** `.\SQLEXPRESS`
- **Database:** `medicaldb`
- **Authentication:** Windows Authentication (Integrated Security=True)

## Schema Overview

### 1. `Users`
Stores authentication and role information for system access.
- `UserId` (INT, PK, IDENTITY)
- `Username` (VARCHAR(50), UNIQUE, NOT NULL)
- `Password` (VARCHAR(50), NOT NULL) - *Plain text (for academic purposes)*
- `Role` (VARCHAR(20), NOT NULL) - *e.g., 'Admin', 'Doctor'*

### 2. `Patients`
Stores core demographic data for patients.
- `PatientId` (INT, PK, IDENTITY)
- `FullName` (NVARCHAR(100), NOT NULL)
- `NIC` (VARCHAR(20), UNIQUE, NOT NULL)
- `ContactNumber` (VARCHAR(20), NOT NULL)
- `Email` (VARCHAR(100), NOT NULL)
- `Address` (NVARCHAR(MAX), NOT NULL)
- `BloodGroup` (VARCHAR(5), NOT NULL)
- `Gender` (VARCHAR(10), NOT NULL)
- `Age` (INT, NOT NULL)
- `RegisteredDate` (DATETIME, DEFAULT GETDATE())

### 3. `MedicalRecords`
Stores standard prescription and diagnosis records for patients.
- `RecordId` (INT, PK, IDENTITY)
- `PatientId` (INT, FK -> Patients)
- `Diagnosis` (NVARCHAR(MAX), NOT NULL) - *Encrypted at rest (AES-256)*
- `Prescription` (NVARCHAR(MAX), NOT NULL) - *Encrypted at rest (AES-256)*
- `CreatedDate` (DATETIME, DEFAULT GETDATE())

### 4. `ReportTemplates`
Stores dynamic template structures for specialized medical reports.
- `TemplateId` (INT, PK, IDENTITY)
- `TemplateName` (VARCHAR(100), NOT NULL)
- `Description` (VARCHAR(MAX), NULL)

### 5. `TemplateFields`
Stores the individual fields belonging to a specific `ReportTemplate`.
- `FieldId` (INT, PK, IDENTITY)
- `TemplateId` (INT, FK -> ReportTemplates)
- `FieldName` (VARCHAR(100), NOT NULL)
- `FieldType` (VARCHAR(50), NOT NULL) - *e.g., 'Text', 'Number', 'Date'*
- `IsRequired` (BIT, DEFAULT 0)

### 6. `PatientReports` (Header)
Stores the metadata linking a patient to a generated dynamic report.
- `ReportId` (INT, PK, IDENTITY)
- `PatientId` (INT, FK -> Patients)
- `TemplateId` (INT, FK -> ReportTemplates)
- `DoctorId` (INT, FK -> Users)
- `ReportDate` (DATETIME, DEFAULT GETDATE())

### 7. `ReportResults` (Data)
Stores the actual values entered into the fields of a dynamic report.
- `ResultId` (INT, PK, IDENTITY)
- `ReportId` (INT, FK -> PatientReports)
- `FieldId` (INT, FK -> TemplateFields)
- `EncryptedValue` (NVARCHAR(MAX), NOT NULL) - *Encrypted at rest (AES-256)*
