# Patient Management System

A secure, desktop-based Windows Forms application built with C# and .NET for managing patient clinical records. This system focuses on security, usability, and professional data management for medical clinics.

## 🚀 Features

*   **Secure Authentication & Role-Based Access**
    *   Login system with encrypted passwords (AES-256).
    *   Differentiated access levels for **Admins** and **Doctors**.
*   **User Management**
    *   Admins can create, manage, and remove staff accounts.
*   **Patient Registration**
    *   Capture and store patient demographic data (Name, NIC, Contact, Address, Blood Group).
*   **Encrypted Medical Records**
    *   Doctors can add diagnoses and prescriptions.
    *   **High Security:** All clinical data (Diagnosis and Prescription) is encrypted using AES-256 *before* being stored in the SQL Database to ensure patient privacy.
*   **Advanced Clinical History Dashboard**
    *   Global chronological view of all medical records.
    *   **Real-Time Search:** Instantly filter records by typing a Patient Name or NIC.
    *   **Sorting:** Sort by Newest First, Oldest First, or Alphabetical.
    *   **Elegant Modal Views:** Click "View Record" to read decrypted details in a beautifully designed, scrollable popup.
*   **Professional PDF Export**
    *   Generate official, printable PDF reports of any clinical record with one click.
    *   Includes automated formatting, shading, timestamps, and signature lines.

---

## 🔄 User Flow

1.  **Launch & Login**
    *   The user starts the application and logs in via the `LoginForm`.
    *   Depending on their role (Admin/Doctor), they are granted access to specific modules on the `MainDashboard`.
2.  **Patient Intake**
    *   When a new patient arrives, staff navigates to **Register Patient** to create a profile (NIC is used as a unique identifier).
3.  **Consultation & Records**
    *   The Doctor navigates to **Add Record**.
    *   They write the diagnosis and prescription. Upon saving, the system encrypts the data and stores it securely in the database.
4.  **Reviewing History**
    *   Staff navigates to **View History**.
    *   They use the real-time search bar to find a specific patient by Name or NIC.
    *   They click **"View Record"** to open the details modal. The system securely decrypts the data on-the-fly for viewing.
5.  **Exporting Data**
    *   From the record modal, the user clicks **"Export PDF"** to generate a professional document to print or share with the patient.

---

## 🛠️ Tech Stack
*   **Frontend:** C# Windows Forms (WinForms)
*   **Backend:** .NET Framework 4.8
*   **Database:** Microsoft SQL Server (`medicaldb`)
*   **Security:** `System.Security.Cryptography.Aes` for data encryption.
*   **Reporting:** Built-in `System.Drawing.Printing.PrintDocument` for native PDF generation.
