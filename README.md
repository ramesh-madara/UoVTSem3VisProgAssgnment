# Patient Management System

A secure, desktop-based Windows Forms application built with C# and .NET for managing patient clinical records. This system focuses on security, usability, and professional data management for medical clinics.

---

## ⚙️ Setup Instructions

To run this project on your local machine, please follow these steps:

### 1. Prerequisites
- **Visual Studio 2022** (or 2019) with the `.NET desktop development` workload installed.
- **SQL Server Express** installed and running (`.\SQLEXPRESS`).
- **SQL Server Management Studio (SSMS)** (optional, but recommended for viewing the database).

### 2. Database Initialization
1. Open SQL Server Management Studio (SSMS) and connect to your local `.\SQLEXPRESS` instance.
2. Locate the `SQL/medicaldb.sql` file in this repository.
3. Open `medicaldb.sql` in SSMS and click **Execute** (or press `F5`).
4. This script will automatically create the `medicaldb` database, construct all required tables, and insert default user accounts:
   - **Admin:** Username: `admin` | Password: `admin123`
   - **Doctor:** Username: `doctor` | Password: `doctor123`
5. *(Optional)* You can execute the other `.sql` scripts inside the `SQL/` folder to populate the database with sample mock data.

### 3. Database Connection Configuration
1. The application is pre-configured to connect to a local SQL Server Express instance using Windows Authentication.
2. The connection string used across the application is:  
   `"Data Source=.\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;"`
3. **Important:** If your local SQL Server instance has a different name (e.g., `localhost`, `.\MSSQLSERVER`, or `(localdb)\MSSQLLocalDB`), you will need to open the solution in Visual Studio, press `Ctrl + Shift + F` (Find and Replace), and globally replace `.\SQLEXPRESS` with your specific server name across all `.cs` files before running the application.

### 4. Running the Application
1. Open the solution file `PatientManagementSystem.sln` in Visual Studio.
2. The project automatically references **iTextSharp** (ensure you restore NuGet packages if prompted).
3. Press **Start** (or `F5`) to compile and launch the application.
4. Log in using the default Admin or Doctor credentials!

---

## 🚀 Features & User Flow

### 1. Launch & Secure Login
The user starts the application and logs in securely. The system features differentiated access levels for **Admins** and **Doctors**. Passwords are encrypted for safety.

![Login Form](https://github.com/ramesh-madara/UoVTSem3VisProgAssgnment/blob/main/Screenshots/Screenshot%202026-09-28%20232232.png?raw=true)

### 2. Patients Dashboard
Once logged in, staff are greeted by the main dashboard. The **Patients Dashboard** allows for real-time search (by Name or NIC), sorting, and immediate access to patient profiles and histories. 

![Patients Dashboard](https://github.com/ramesh-madara/UoVTSem3VisProgAssgnment/blob/main/Screenshots/Screenshot%202026-09-28%20232307.png?raw=true)

### 3. Encrypted Medical Records
Doctors can securely add diagnoses and prescriptions.
**High Security:** All clinical data (Diagnosis, Prescription, and dynamic test results) is encrypted using AES-256 *before* being stored in the SQL Database to ensure maximum patient privacy.

![Secure Record Entry](https://github.com/ramesh-madara/UoVTSem3VisProgAssgnment/blob/main/Screenshots/Screenshot%202026-09-28%20233615.png?raw=true)

### 4. Dynamic Medical Reports
Admins can build custom report templates (e.g., Blood Tests, RT-PCR) by defining specific fields. Doctors can then select a template, and the UI will automatically generate the corresponding input fields on-the-fly. All report data is stored using an Entity-Attribute-Value (EAV) model and encrypted.

![Dynamic Medical Report](https://github.com/ramesh-madara/UoVTSem3VisProgAssgnment/blob/main/Screenshots/Screenshot%202026-09-28%20233452.png?raw=true)

### 5. Professional PDF Export
Staff can click "View Record" to read decrypted details in a beautifully designed popup, and then click **Export PDF** to generate an official, printable document. The generated PDFs feature automated formatting, highlighting, timestamps, and signature lines in a signature sticky-note aesthetic.

![PDF Preview](https://github.com/ramesh-madara/UoVTSem3VisProgAssgnment/blob/main/Screenshots/Screenshot%202026-09-28%20233522.png?raw=true)

### 6. System Information & UI Polish
The application was built with a strict adherence to a modern flat UI design, doing away with outdated 3D borders for a crisp, responsive, and professional user experience. 

![About System](https://github.com/ramesh-madara/UoVTSem3VisProgAssgnment/blob/main/Screenshots/Screenshot%202026-09-28%20234951.png?raw=true)

---

## 🛠️ Tech Stack
*   **Frontend:** C# Windows Forms (WinForms)
*   **Backend:** .NET Framework 4.8
*   **Database:** Microsoft SQL Server (`medicaldb`)
*   **Security:** `System.Security.Cryptography.Aes` for data encryption.
*   **Reporting:** iTextSharp for robust native PDF generation.

---

## 👥 Group Members
**UoVT SOF 23/24 Semester 3**  
*Visual Programming II Group Assignment*

- **SOF/23/B2/29** - Nuwan Hasanka
- **SOF/23/B2/02** - Ramesh Madara
- **SOF/23/B2/23** - Gihan Lavnidu
- **SOF/23/B2/28** - Chiranthi Bhagya
- **SOF/23/B2/20** - Suresh Indika
- **SOF/23/B2/14** - Thisari Wijerathne
