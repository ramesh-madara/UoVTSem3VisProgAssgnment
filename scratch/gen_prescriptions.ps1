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

$sql = "USE medicaldb;`n`n"
$sql += "DELETE FROM MedicalRecords;`n`n"

# Nuwan (1)
$sql += "INSERT INTO MedicalRecords (PatientId, Diagnosis, Prescription, CreatedDate) VALUES (1, '" + [Encryptor]::Encrypt("Acute Bronchitis") + "', '" + [Encryptor]::Encrypt("Azithromycin 500mg, 1 tablet daily for 3 days.`nAlbuterol inhaler 2 puffs every 4-6 hours as needed.") + "', GETDATE());`n"

# Sahan (2)
$sql += "INSERT INTO MedicalRecords (PatientId, Diagnosis, Prescription, CreatedDate) VALUES (2, '" + [Encryptor]::Encrypt("Hypertension (Stage 1)") + "', '" + [Encryptor]::Encrypt("Lisinopril 10mg, 1 tablet daily in the morning.`nAdvise on low sodium diet.") + "', GETDATE());`n"

# Madara (3)
$sql += "INSERT INTO MedicalRecords (PatientId, Diagnosis, Prescription, CreatedDate) VALUES (3, '" + [Encryptor]::Encrypt("Migraine with Aura") + "', '" + [Encryptor]::Encrypt("Sumatriptan 50mg, 1 tablet at onset of migraine, may repeat once after 2 hours if needed.`nIbuprofen 400mg as needed for pain.") + "', GETDATE());`n"

# Sandamali (4)
$sql += "INSERT INTO MedicalRecords (PatientId, Diagnosis, Prescription, CreatedDate) VALUES (4, '" + [Encryptor]::Encrypt("Type 2 Diabetes Mellitus") + "', '" + [Encryptor]::Encrypt("Metformin 500mg, 1 tablet twice daily with meals.`nCheck fasting blood glucose daily.") + "', GETDATE());`n"

$sql | Out-File -FilePath "insert_prescriptions.sql" -Encoding utf8
