USE medicaldb;

DECLARE @Rep INT;

INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (3, 9, 1, GETDATE());
SET @Rep = SCOPE_IDENTITY();
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 26, 'UwmWC6NDvW0IpZXPGg4kDA==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 27, 'zzB+IJAc6jGBDuPUlqMk/w==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 28, 'cNPpRO0kfMZZxwpNxmef6Q==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 29, 'pbld+ergLybmoImPVfwQLA==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 30, '8it28rgBjzPfVl5OjGlYyA==');

INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (1, 10, 1, GETDATE());
SET @Rep = SCOPE_IDENTITY();
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 31, '1seXBu58TqODV7+OudbKnQ==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 32, 'wfeWg8gmedscRLHtvvlTAA==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 33, 'rdXmT53OgG2bArCOlJ8pvA==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 34, '5GMv8bfltw730KUMboCFGw==');

INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (2, 11, 1, GETDATE());
SET @Rep = SCOPE_IDENTITY();
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 35, '6uA/Hn8wGzUS6rhMrVNTYQ==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 36, 'aeBTub8fd65VwJoMVh5lqMg3z9+hJDLh5NZjyuyWgExWRx1y0jJ2LNr1b+fK5h77');

INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (3, 12, 1, GETDATE());
SET @Rep = SCOPE_IDENTITY();
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 37, 'BT2nm59yzUkh9mSODQWeQA==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 38, 'KtHCF+mm6ztEt5p+C+dZEA==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 39, 'Cqy56dXNmRzToJCrL0aatw==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 40, 'ZNVRWGqGF2/RQWeg9U+XNQ==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 41, 'xJhpXzwoM9boZ5mMILTlpA==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 42, 'ZSpDQLTzvXiPH61OPbcl0Q==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 43, '78OEY6ydEo5rNwGKfAy7rA==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 44, 'LYz9KINyzoJfvNwEnSQqAg==');

INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (1, 9, 1, GETDATE());
SET @Rep = SCOPE_IDENTITY();
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 26, 'NCHzGoQgKBpBSxOt2r3Mjw==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 27, 'N07PbvJhZuPeG2wPFcT5qA==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 28, 'knVbQ/V0jqAXeiYuVaKbxQ==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 29, 'wFH8Fxima3CekOOfjIlNCg==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 30, 'B/jS5y8DJZj4bg0LHjrCJQ==');

INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) VALUES (2, 10, 1, GETDATE());
SET @Rep = SCOPE_IDENTITY();
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 31, 'Y8yzpXM/hEOGw5R5K4IAqw==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 32, 'ZraBA77fmZy1LhiZ0N4Psw==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 33, 'JvnQIMUvUzQoZpeZpg19GQ==');
INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@Rep, 34, 'WcUCyh8F1cI6eW7+EvRBZA==');


