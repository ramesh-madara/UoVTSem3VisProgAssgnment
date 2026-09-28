DELETE FROM ReportResults;
DELETE FROM TemplateFields;
DELETE FROM ReportTemplates;

-- Template 1: CBC
INSERT INTO ReportTemplates (TemplateName, IsActive) VALUES ('Complete Blood Count (CBC)', 1);
DECLARE @Template1 INT = SCOPE_IDENTITY();
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template1, 'White Blood Cell (WBC)', 'Numeric', 4.5, 11.0, 'K/uL');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template1, 'Red Blood Cell (RBC)', 'Numeric', 4.2, 5.9, 'M/uL');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template1, 'Hemoglobin (HGB)', 'Numeric', 12.0, 17.5, 'g/dL');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template1, 'Hematocrit (HCT)', 'Numeric', 36.0, 53.0, '%');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template1, 'Platelet Count', 'Numeric', 150, 450, 'K/uL');

-- Template 2: Lipid Panel
INSERT INTO ReportTemplates (TemplateName, IsActive) VALUES ('Lipid Panel', 1);
DECLARE @Template2 INT = SCOPE_IDENTITY();
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template2, 'Total Cholesterol', 'Numeric', 0, 200, 'mg/dL');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template2, 'LDL Cholesterol', 'Numeric', 0, 100, 'mg/dL');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template2, 'HDL Cholesterol', 'Numeric', 40, 60, 'mg/dL');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template2, 'Triglycerides', 'Numeric', 0, 150, 'mg/dL');

-- Template 3: COVID-19 RT-PCR
INSERT INTO ReportTemplates (TemplateName, IsActive) VALUES ('COVID-19 RT-PCR', 1);
DECLARE @Template3 INT = SCOPE_IDENTITY();
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, NormalBoolean) VALUES (@Template3, 'SARS-CoV-2 Result', 'Boolean', 'Negative');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, Unit) VALUES (@Template3, 'Physician Notes', 'Text', '');

-- Template 4: Basic Metabolic Panel (BMP)
INSERT INTO ReportTemplates (TemplateName, IsActive) VALUES ('Basic Metabolic Panel (BMP)', 1);
DECLARE @Template4 INT = SCOPE_IDENTITY();
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template4, 'Glucose', 'Numeric', 70, 100, 'mg/dL');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template4, 'Calcium', 'Numeric', 8.5, 10.2, 'mg/dL');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template4, 'Sodium', 'Numeric', 135, 145, 'mEq/L');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template4, 'Potassium', 'Numeric', 3.5, 5.0, 'mEq/L');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template4, 'Carbon Dioxide (CO2)', 'Numeric', 23, 29, 'mEq/L');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template4, 'Chloride', 'Numeric', 96, 106, 'mEq/L');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template4, 'Blood Urea Nitrogen (BUN)', 'Numeric', 6, 20, 'mg/dL');
INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit) VALUES (@Template4, 'Creatinine', 'Numeric', 0.6, 1.3, 'mg/dL');
