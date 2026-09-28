USE medicaldb;

-- Report 2: CBC for 982040639v
INSERT INTO ReportResults (ReportID, PatientNIC, TemplateID, FieldID, ResultValue, DateCreated) VALUES 
(2, '982040639v', 9, 26, '6.5', GETDATE()),
(2, '982040639v', 9, 27, '4.8', GETDATE()),
(2, '982040639v', 9, 28, '14.2', GETDATE()),
(2, '982040639v', 9, 29, '42.1', GETDATE()),
(2, '982040639v', 9, 30, '250', GETDATE());

-- Report 3: Lipid Panel for 199324100400
INSERT INTO ReportResults (ReportID, PatientNIC, TemplateID, FieldID, ResultValue, DateCreated) VALUES 
(3, '199324100400', 10, 31, '185', GETDATE()),
(3, '199324100400', 10, 32, '110', GETDATE()),
(3, '199324100400', 10, 33, '55', GETDATE()),
(3, '199324100400', 10, 34, '120', GETDATE());

-- Report 4: COVID for 199324398300
INSERT INTO ReportResults (ReportID, PatientNIC, TemplateID, FieldID, ResultValue, DateCreated) VALUES 
(4, '199324398300', 11, 35, 'Negative', GETDATE()),
(4, '199324398300', 11, 36, 'Patient asymptomatic. Cleared for travel.', GETDATE());

-- Report 5: BMP for 982040639v
INSERT INTO ReportResults (ReportID, PatientNIC, TemplateID, FieldID, ResultValue, DateCreated) VALUES 
(5, '982040639v', 12, 37, '92', GETDATE()),
(5, '982040639v', 12, 38, '9.5', GETDATE()),
(5, '982040639v', 12, 39, '140', GETDATE()),
(5, '982040639v', 12, 40, '4.2', GETDATE()),
(5, '982040639v', 12, 41, '25', GETDATE()),
(5, '982040639v', 12, 42, '102', GETDATE()),
(5, '982040639v', 12, 43, '15', GETDATE()),
(5, '982040639v', 12, 44, '0.9', GETDATE());

-- Report 6: CBC for 199324100400
INSERT INTO ReportResults (ReportID, PatientNIC, TemplateID, FieldID, ResultValue, DateCreated) VALUES 
(6, '199324100400', 9, 26, '7.2', GETDATE()),
(6, '199324100400', 9, 27, '5.1', GETDATE()),
(6, '199324100400', 9, 28, '15.0', GETDATE()),
(6, '199324100400', 9, 29, '45.0', GETDATE()),
(6, '199324100400', 9, 30, '310', GETDATE());

-- Report 7: Lipid Panel for 199324398300
INSERT INTO ReportResults (ReportID, PatientNIC, TemplateID, FieldID, ResultValue, DateCreated) VALUES 
(7, '199324398300', 10, 31, '210', GETDATE()),
(7, '199324398300', 10, 32, '135', GETDATE()),
(7, '199324398300', 10, 33, '45', GETDATE()),
(7, '199324398300', 10, 34, '160', GETDATE());
