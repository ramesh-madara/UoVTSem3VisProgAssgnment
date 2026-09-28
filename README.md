# Patient Management System

A secure, desktop-based Windows Forms application built with C# and .NET for managing patient clinical records. This system focuses on security, usability, and professional data management for medical clinics.

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
