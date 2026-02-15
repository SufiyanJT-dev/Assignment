-- Healthcare System Schema

CREATE TABLE Patients (
    PatientID INT PRIMARY KEY,
    PatientName VARCHAR(100),
    DateOfBirth DATE,
    Gender VARCHAR(10)
);

CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY,
    DoctorName VARCHAR(100),
    Specialty VARCHAR(100)
);

CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY,
    PatientID INT,
    DoctorID INT,
    AppointmentDate DATE,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (DoctorID) REFERENCES Doctors(DoctorID)
);

CREATE TABLE Medications (
    MedicationID INT PRIMARY KEY,
    MedicationName VARCHAR(200),
    DosageForm VARCHAR(50)
);

CREATE TABLE Prescriptions (
    PrescriptionID INT PRIMARY KEY,
    PatientID INT,
    MedicationID INT,
    PrescriptionDate DATE,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID),
    FOREIGN KEY (MedicationID) REFERENCES Medications(MedicationID)
);

-- Questions

-- 1. List all patients and their appointments, including patients who have never had an appointment.
SELECT * FROM [dbo].[Patients] AS P LEFT JOIN [dbo].[Appointments] AS A ON P.[PatientID]=A.[PatientID]
-- 2. Display all doctors and their scheduled appointments, including doctors without any appointments.
SELECT * FROM [dbo].[Doctors]AS D LEFT JOIN [dbo].[Appointments] AS A ON D.[DoctorID]=A.[DoctorID]
-- 3. Show all medications and the patients they've been prescribed to, including medications that haven't been prescribed.
SELECT M.*,P.[PatientName] from [dbo].[Medications] AS M  left join [dbo].[Prescriptions] as Pr on M.[MedicationID]=Pr.[MedicationID] LEFT join [dbo].[Patients] as P on Pr.[PatientID]=P.[PatientID]
-- 4. List all possible patient-doctor combinations, regardless of whether an appointment has occurred.
SELECT * FROM [dbo].[Patients]  CROSS JOIN [dbo].[Doctors]
Select * from [dbo].[Prescriptions] as Pr full join [dbo].[Patients] as P on Pr.[PatientID]=P.[PatientID]  full join [dbo].[Medications] as M on Pr.[MedicationID]=M.[MedicationID]
-- 6. Show all patients who have never been prescribed any medication, along with their appointment history.
SELECT 
    P.PatientID,
    P.PatientName,
    A.AppointmentID,
    A.AppointmentDate,
    A.DoctorID FROM [dbo].[Patients] AS P LEFT JOIN [dbo].[Appointments] AS A  ON P.PatientID = A.PatientID LEFT JOIN [dbo].[Prescriptions] AS Pr ON P.PatientID = Pr.PatientID WHERE Pr.PatientID IS NULL



-- 7. List all doctors who have appointments in the next week, along with the patients they're scheduled to see.
select * from [dbo].[Doctors] as D join [dbo].[Appointments] AS A on D.[DoctorID]=A.[DoctorID] join [dbo].[Patients] as P on A.[PatientID]=P.[PatientID] where datediff(day, A.[AppointmentDate],getdate())  between -7 and 0 
-- 8. Display all medications prescribed to patients over 60 years old, including medications not prescribed to this age group.
select * from [dbo].[Medications] as M left join [dbo].[Prescriptions] as P on M.MedicationID=P.MedicationID  left join  [dbo].[Patients] as Pa on P.PatientID=pa.PatientID  and datediff(year,Pa.[DateOfBirth],GETDATE())>=60

-- 9. Show all appointments from last year and any associated prescription information.
 select A.[AppointmentID],A.[PatientID],A.[DoctorID],A.[AppointmentDate],Pr.[PrescriptionID], Pr.[PatientID],Pr.[MedicationID],Pr.[PrescriptionDate] from [dbo].[Appointments] as A join  [dbo].[Prescriptions] as Pr on A.PatientID=Pr.PatientID where Year(A.AppointmentDate) = Year(GETDATE())-1

-- 10. List all possible specialty-medication combinations, regardless of whether a doctor of that specialty has prescribed that medication.
SELECT 
    S.Specialty,
    M.MedicationID,
    M.MedicationName
FROM [dbo].[Doctors] as S
  CROSS JOIN 
    [dbo].[Medications] AS M
