Add-Type -TypeDefinition @"
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public class Encryptor {
    public static string Encrypt(string plainText) {
        byte[] keyBytes = Encoding.UTF8.GetBytes("PatientClinicKey1234567890123456");
        byte[] ivBytes = Encoding.UTF8.GetBytes("PatientClinicIV1");
        using (var aes = Aes.Create()) {
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using (var ms = new MemoryStream()) {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                    using (var sw = new StreamWriter(cs)) {
                        sw.Write(plainText);
                    }
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
"@

$sql = "USE medicaldb;`n`nDECLARE @Rep INT;`n`n"

# Report 1
$sql += "INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (3, 9, 1, GETDATE());`nSET @Rep = SCOPE_IDENTITY();`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 26, '" + [Encryptor]::Encrypt("6.5") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 27, '" + [Encryptor]::Encrypt("4.8") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 28, '" + [Encryptor]::Encrypt("14.2") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 29, '" + [Encryptor]::Encrypt("42.1") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 30, '" + [Encryptor]::Encrypt("250") + "');`n`n"

# Report 2
$sql += "INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (1, 10, 1, GETDATE());`nSET @Rep = SCOPE_IDENTITY();`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 31, '" + [Encryptor]::Encrypt("185") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 32, '" + [Encryptor]::Encrypt("110") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 33, '" + [Encryptor]::Encrypt("55") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 34, '" + [Encryptor]::Encrypt("120") + "');`n`n"

# Report 3
$sql += "INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (2, 11, 1, GETDATE());`nSET @Rep = SCOPE_IDENTITY();`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 35, '" + [Encryptor]::Encrypt("Negative") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 36, '" + [Encryptor]::Encrypt("Patient asymptomatic. Cleared for travel.") + "');`n`n"

# Report 4
$sql += "INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (3, 12, 1, GETDATE());`nSET @Rep = SCOPE_IDENTITY();`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 37, '" + [Encryptor]::Encrypt("92") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 38, '" + [Encryptor]::Encrypt("9.5") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 39, '" + [Encryptor]::Encrypt("140") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 40, '" + [Encryptor]::Encrypt("4.2") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 41, '" + [Encryptor]::Encrypt("25") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 42, '" + [Encryptor]::Encrypt("102") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 43, '" + [Encryptor]::Encrypt("15") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 44, '" + [Encryptor]::Encrypt("0.9") + "');`n`n"

# Report 5
$sql += "INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (1, 9, 1, GETDATE());`nSET @Rep = SCOPE_IDENTITY();`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 26, '" + [Encryptor]::Encrypt("7.2") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 27, '" + [Encryptor]::Encrypt("5.1") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 28, '" + [Encryptor]::Encrypt("15.0") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 29, '" + [Encryptor]::Encrypt("45.0") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 30, '" + [Encryptor]::Encrypt("310") + "');`n`n"

# Report 6
$sql += "INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (2, 10, 1, GETDATE());`nSET @Rep = SCOPE_IDENTITY();`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 31, '" + [Encryptor]::Encrypt("210") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 32, '" + [Encryptor]::Encrypt("135") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 33, '" + [Encryptor]::Encrypt("45") + "');`n"
$sql += "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 34, '" + [Encryptor]::Encrypt("160") + "');`n`n"

$sql | Out-File -FilePath "insert_reports_dynamic.sql" -Encoding utf8
