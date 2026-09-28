# Patient Medical Report Management System - Master Documentation

## Overview
The Patient Medical Report Management System is a C# WinForms application designed for clinics and hospitals to securely manage patient records, dynamic medical reports, and prescriptions.

## User Roles & Logic
1. **Admin:**
   - Full access to all system features.
   - Can manage users, manage dynamic report templates, and view/edit all patients.
2. **Doctor:**
   - Access restricted primarily to patient care.
   - Can view existing report templates but cannot edit them (prompts user to contact Admin).
   - Can register patients, view histories, and add medical records.
3. **UI Adaptation:**
   - The main dashboard dynamically greys out and sorts restricted modules to the bottom of the sidebar based on the logged-in user's role.

## Feature Set

### 1. Patient Management
- **Registration:** Capture Name, NIC, Contact, Address, Email, Blood Group, Gender, and Age.
- **Search & Filter:** Real-time client-side filtering via `DataView` on the DataGridView. Users can search by Name/NIC and filter by Gender.
- **Sorting:** Clickable column headers (e.g., Age) for instant grid sorting.

### 2. Medical Records (Prescriptions)
- **Data Entry:** Capture standard Diagnosis and Prescription text.
- **Encryption:** All text is encrypted using AES-256 before being committed to the `MedicalRecords` table.
- **Decryption:** Data is transparently decrypted in memory when displayed in the History UI.

### 3. Dynamic Medical Reports
- **Template Builder (Admin):** Allows admins to define custom report types (e.g., "Blood Test") with arbitrary fields (e.g., "RBC Count", "WBC Count").
- **Dynamic Entry (Doctor):** When a doctor selects a template, the UI dynamically generates input fields matching the template.
- **EAV Model Data Storage:** Results are saved using an Entity-Attribute-Value (EAV) model across `PatientReports` (Header) and `ReportResults` (Data), with all data values being AES encrypted.

### 4. PDF Exporting
- **Sticky Note PDFs:** Uses `iTextSharp` to generate PDFs for both Prescriptions and Medical Reports.
- **Styling:** The PDFs are explicitly sized to 5x5 inches with a light yellow background (`#ffffcc`) to mimic physical sticky notes.
- **Preview:** Generates a temporary PDF file and opens it in an embedded `WebBrowser` control via a Modal form for previewing before finalizing.

## Technical Walkthrough

### UI/UX Engine
- **`UITheme.cs`**: A centralized static class that recursively traverses any form passed to it. It flattens buttons, styles grids, removes borders, and enforces the global font (`Segoe UI`) and color palette (`#2c3e50`, `#2980b9`, etc.), ensuring absolute consistency across the application.
- **Custom Tab Rendering**: Forms like `ViewPatientHistoryForm` and `AddMedicalRecordForm` hook into `DrawItem` to manually paint active tabs bright blue and inactive tabs gray, providing immediate visual feedback.

### Encryption Pipeline
- **Keys:** A hardcoded (for academic scope) 32-byte Key and 16-byte IV are used.
- **Process:** Data strings are fed through `Aes.Create()`, written to a `CryptoStream`, and converted to Base64 strings for storage. 
- **Retrieval:** The Base64 string is decoded to bytes and passed through an AES decryptor stream to recover the plaintext for the UI or PDF.

### Dynamic Rendering Logic
In `AddMedicalReportForm.cs`:
1. The app queries `TemplateFields` for the selected `TemplateId`.
2. A loop instantiates `Label` and `Control` (TextBox, NumericUpDown) pairs.
3. The controls are laid out vertically using a Y-offset accumulator.
4. On Save, the app loops through the dynamic controls, extracts the values, encrypts them, and inserts them into `ReportResults` using the generated `ReportId`.

## Conclusion
The system successfully bridges modern flat UI design with a robust, secure, and highly flexible backend architecture capable of supporting any clinical reporting need through its dynamic template engine.
