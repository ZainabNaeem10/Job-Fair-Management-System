use jobfair;


CREATE TABLE [User] (
    UserId INT  PRIMARY KEY,
    FirstName VARCHAR(100) not null,
    MiddleName VARCHAR(100),
	LastName VARCHAR(100) not null,
    Email NVARCHAR(100) not null unique check(email like '%_@_%._%'),
	Phone varchar(11) not null check (Phone like '03%' ),
	Password VARCHAR(8) NOT NULL UNIQUE CHECK (LEN(Password) > 0),
    Role VARCHAR(50) not null,
    IsActive BIT
);



CREATE TABLE Companies (
    CompanyID INT  PRIMARY KEY,
    Name VARCHAR(150)  not null,
    Sector VARCHAR(100)  not null,
    City VARCHAR(100) ,
    Street VARCHAR(100),
	Country VARCHAR(100) not null,
	ContactInfo VARCHAR(100)  not null CHECK (LEN(ContactInfo) >= 5)

);


CREATE TABLE Student (
   StudentId NVARCHAR(10) PRIMARY KEY CHECK (StudentId LIKE '[0-9][0-9]i-[0-9][0-9][0-9][0-9]'),
    DegreeProgram VARCHAR(100) NOT NULL CHECK (DegreeProgram IN( 'SE','CS','AI','DS','CYS')),
    CurrentSemester INT NOT NULL CHECK (CurrentSemester >= 1 AND CurrentSemester <= 8),
    cgpa FLOAT NOT NULL CHECK (cgpa >= 0.0 AND cgpa <= 4.0),
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

ALTER TABLE Student ADD Hire bit DEFAULT 0;
ALTER TABLE Student ADD is_approved bit DEFAULT 0;



CREATE TABLE Recruiter (
    RecruiterId INT PRIMARY KEY,
    UserId INT NOT NULL,
    is_approved BIT NOT NULL CHECK (is_approved IN (0, 1)),
    CompanyID INT NOT NULL CHECK (CompanyID > 0),
    FOREIGN KEY (UserId) REFERENCES [User](UserId),
    FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID)
);


CREATE TABLE Tpo (
    TpoId INT PRIMARY KEY,
    Office_location VARCHAR(100),
    UserId INT,
    FOREIGN KEY (UserId) REFERENCES [User](UserId),
   
);

CREATE TABLE Booth_Coordinator (
    BoothCoordinatorId INT PRIMARY KEY,
    UserId INT  not null,
    ShiftTimings VARCHAR(100) not null,
    FOREIGN KEY (UserId) REFERENCES [User](UserId)
);

ALTER TABLE Booth_Coordinator
ALTER COLUMN ShiftTimings VARCHAR(100) NULL;
ALTER TABLE Student
ALTER COLUMN  DegreeProgram VARCHAR(100);


CREATE TABLE JobFairEvents (
    EventId INT PRIMARY KEY,
    Title VARCHAR(150) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Venue VARCHAR(150) NOT NULL,
    BoothSlots INT NOT NULL
);

ALTER TABLE JobFairEvents
ADD CONSTRAINT chk_enddate CHECK (EndDate >= StartDate);


CREATE TABLE Jobpostings (
    JobID INT PRIMARY KEY,
    Title VARCHAR(500) NOT NULL,
    Salary VARCHAR(100) NOT NULL,
    Description VARCHAR(500) NOT NULL,
    Street VARCHAR(100) NOT NULL,
    City VARCHAR(100) NOT NULL,
    Country VARCHAR(100) NOT NULL,
    CompanyID INT NOT NULL,
    EventID INT,
    FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID),
    FOREIGN KEY (EventID) REFERENCES JobFairEvents(EventId),
    CHECK (LEN(Title) > 0), -- ensures Title is not empty.
    CHECK (LEN(Salary) > 0), -- ensures Salary is not empty.
    CHECK (LEN(Description) > 0) -- ensures Description is not empty.
);

CREATE TABLE StudentSkills (
    Userid NVARCHAR(10) NOT NULL,
    skills VARCHAR(255) NOT NULL CHECK (LEN(skills) > 0),
    FOREIGN KEY (Userid) REFERENCES Student(StudentId),
);

-- Make 'skills' column nullable
ALTER TABLE StudentSkills DROP CONSTRAINT IF EXISTS CK_StudentSkills_skills;
ALTER TABLE StudentSkills ALTER COLUMN skills VARCHAR(255) NULL;


------------------------------------------------------------------
CREATE TABLE Student_Certificates (
    CertificateName VARCHAR(150) NOT NULL CHECK (LEN(CertificateName) > 0),
     Userid NVARCHAR(10) NOT NULL,
    FOREIGN KEY (Userid) REFERENCES Student(StudentId),
);

-- Make 'CertificateName' column nullable
ALTER TABLE Student_Certificates DROP CONSTRAINT IF EXISTS CK_Student_Certificates_CertificateName;
ALTER TABLE Student_Certificates ALTER COLUMN CertificateName VARCHAR(150) NULL;

CREATE TABLE Jobpostings_Type (
    Job_id INT NOT NULL,
    Type VARCHAR(200) NOT NULL CHECK (LEN(Type) > 0),
    FOREIGN KEY (Job_id) REFERENCES Jobpostings(JobID)
);

CREATE TABLE Jobpostings_RequiredSkills (
    Job_id INT NOT NULL,
    RequiredSkills VARCHAR(500) NOT NULL CHECK (LEN(RequiredSkills) > 0),
    FOREIGN KEY (Job_id) REFERENCES Jobpostings(JobID)
);

CREATE TABLE Applications (
    ApplicationID INT PRIMARY KEY,
    Status VARCHAR(50) NOT NULL CHECK (LEN(Status) > 0),
    [Date Applied] DATE NOT NULL CHECK ([Date Applied] <= GETDATE()),
    Userid NVARCHAR(10) NOT NULL,
    JobID INT NOT NULL,
    FOREIGN KEY (Userid) REFERENCES Student(StudentId),
    FOREIGN KEY (JobID) REFERENCES jobPostings(JobID)
);


CREATE TABLE Interviews (
    InterviewID INT PRIMARY KEY,
    start_time DATETIME NOT NULL,
    end_time DATETIME NOT NULL,
    Status VARCHAR(50) NOT NULL CHECK (LEN(Status) > 0),
    Result VARCHAR(50) CHECK (LEN(Result) > 0),
    ApplicationID INT NOT NULL,
    Userid INT NOT NULL,
    EventID INT NOT NULL,
    FOREIGN KEY (ApplicationID) REFERENCES Applications(ApplicationID),
    FOREIGN KEY (Userid) REFERENCES Recruiter(RecruiterId),
    FOREIGN KEY (EventID) REFERENCES JobFairEvents(EventId)
);

ALTER TABLE Interviews
ADD CONSTRAINT chk_endtime CHECK (end_time >= start_time);


CREATE TABLE Booth (
    BoothID INT  PRIMARY KEY,
    check_intime datetime CHECK (check_intime >= 0),
    UserId INT NOT NULL,
    CompanyID INT NOT NULL CHECK (CompanyID > 0),
	EventId int not null,
    location VARCHAR(100) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES TPO(TPOID),
    FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID),
    FOREIGN KEY (EventID) REFERENCES JobFairEvents(EventId)
);

ALTER TABLE Booth ADD VisitorCount INT DEFAULT 0;
ALTER TABLE Booth Alter Column UserId int ;

CREATE TABLE Reviews (
    ReviewID INT PRIMARY KEY,
    Comments VARCHAR(200) NOT NULL CHECK (LEN(Comments) > 0),
    Ratings INT NOT NULL CHECK (Ratings >= 1 AND Ratings <= 5),
     Userid NVARCHAR(10) NOT NULL,
    FOREIGN KEY (Userid) REFERENCES Student(StudentId),
    Recruiterid INT NOT NULL,
    FOREIGN KEY (Recruiterid) REFERENCES Recruiter(RecruiterId)
);



CREATE TABLE Visits (
     Userid NVARCHAR(10) NOT NULL,
    booth_id INT NOT NULL,
    FOREIGN KEY (Userid) REFERENCES Student(StudentId),
    FOREIGN KEY (booth_id) REFERENCES Booth(BoothID)
);

CREATE TABLE Monitors (
    booth_id INT NOT NULL,
    Userid INT NOT NULL,
    FOREIGN KEY (Userid) REFERENCES Booth_Coordinator(BoothCoordinatorId),
    FOREIGN KEY (booth_id) REFERENCES Booth(BoothID)
);


--------------------------------------------tables-------------------------
INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(1, 'Ahmed', 'Ali', 'Khan', 'ahmed.khan1@example.com', '03001234567', 'pass1234', 'Student', 1),
(2, 'Fatima', NULL, 'Sheikh', 'fatima.sheikh@example.com', '03012345678', 'pass5600', 'Student', 1),
(3, 'Aisha', 'Bibi', 'Malik', 'aisha.malik@example.com', '03023456789', 'word9876', 'Student', 1),
(4, 'Usman', 'Yasir', 'Raja', 'usman.raja@example.com', '03034567890', 'abc56789', 'Student', 1),
(5, 'Zain', NULL, 'Chaudhry', 'zain.chaudhry@example.com', '03045678901', 'code6789', 'Student', 1),
(6, 'Hassan', 'Ali', 'Shah', 'hassan.shah@example.com', '03056789012', 'lock1234', 'Student', 1),
(7, 'Zara', 'Bano', 'Farooq', 'zara.farooq@example.com', '03067890123', 'key98765', 'Student', 1),
(8, 'Bilal', 'Ahmed', 'Bhatti', 'bilal.bhatti@example.com', '03078901234', 'myst5678', 'Student', 1),
(9, 'Maryam', NULL, 'Akhtar', 'maryam.akhtar@example.com', '03089012345', 'safe6767', 'Student', 1),
(10, 'Ali', 'Muhammad', 'Butt', 'ali.butt@example.com', '03090123456', 'pass4300', 'Student', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(11, 'Sara', NULL, 'Iqbal', 'sara.iqbal@example.com', '03011123456', 'star9090', 'Student', 1),
(12, 'Hamza', 'Kamal', 'Dar', 'hamza.dar@example.com', '03012123456', 'code5454', 'Student', 1),
(13, 'Amna', NULL, 'Zafar', 'amna.zafar@example.com', '03013123456', 'open4321', 'Student', 1),
(14, 'Khadija', 'Aliya', 'Tariq', 'khadija.tariq@example.com', '03014123456', 'safe4321', 'Student', 1),
(15, 'Shahzaib', NULL, 'Sadiq', 'shahzaib.sadiq@example.com', '03015123456', 'word6789', 'Student', 1),
(16, 'Tariq', 'Ahmed', 'Javed', 'tariq.javed@example.com', '03016123456', 'pass5678', 'Student', 1),
(17, 'Laiba', 'Rehan', 'Nawaz', 'laiba.nawaz@example.com', '03017123456', 'code9876', 'Student', 1),
(18, 'Noman', NULL, 'Yousaf', 'noman.yousaf@example.com', '03018123456', 'key56789', 'Student', 1),
(19, 'Hira', 'Ali', 'Zahid', 'hira.zahid@example.com', '03019123456', 'word4399', 'Student', 1),
(20, 'Abdullah', NULL, 'Rafiq', 'abdullah.rafiq@example.com', '03020123456', 'safe5411', 'Student', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(21, 'Rida', 'Sana', 'Qureshi', 'rida.qureshi@example.com', '03021123456', 'pass7890', 'Student', 1),
(22, 'Sana', NULL, 'Mumtaz', 'sana.mumtaz@example.com', '03022123456', 'lock9876', 'Student', 1),
(23, 'Hafsa', 'Amin', 'Sohail', 'hafsa.sohail@example.com', '03023123456', 'myst6789', 'Student', 1),
(24, 'Faizan', 'Rizwan', 'Anwar', 'faizan.anwar@example.com', '03024123456', 'key1234', 'Student', 1),
(25, 'Iqra', NULL, 'Mustafa', 'iqra.mustafa@example.com', '03025123456', 'star5432', 'Student', 1),
(26, 'Mohsin', 'Hafeez', 'Qazi', 'mohsin.qazi@example.com', '03026123456', 'lock4321', 'Student', 1),
(27, 'Samina', NULL, 'Rana', 'samina.rana@example.com', '03027123456', 'open9876', 'Student', 1),
(28, 'Raheel', 'Zaman', 'Chishti', 'raheel.chishti@example.com', '03028123456', 'safe5600', 'Student', 1),
(29, 'Aliya', NULL, 'Tahir', 'aliya.tahir@example.com', '03029123456', 'word7866', 'Student', 1),
(30, 'Sobia', 'Kiran', 'Aslam', 'sobia.aslam@example.com', '03030123456', 'lock7801', 'Student', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(31, 'Sadiq', 'Hussain', 'Mehdi', 'sadiq.mehdi@example.com', '03031123456', 'fast0987', 'Student', 1),
(32, 'Imran', NULL, 'Zaidi', 'imran.zaidi@example.com', '03032123456', 'unlock23', 'Student', 1),
(33, 'Rabia', 'Khan', 'Jaffar', 'rabia.jaffar@example.com', '03033123456', 'code7860', 'Student', 1),
(34, 'Hammad', 'Adeel', 'Shahid', 'hammad.shahid@example.com', '03034123456', 'pass9876', 'Student', 1),
(35, 'Zubair', 'Yusuf', 'Mehmood', 'zubair.mehmood@example.com', '03035123456', 'mysecret', 'Student', 1),
(36, 'Farah', 'Sobia', 'Ahmed', 'farah.ahmed@example.com', '03036123456', 'secure99', 'Student', 1),
(37, 'Junaid', 'Rasheed', 'Asif', 'junaid.asif@example.com', '03037123456', 'open786', 'Student', 1),
(38, 'Tariqa', 'Hammad', 'Ali', 'tariqa.ali@example.com', '03038123456', 'star4569', 'Student', 1),
(39, 'Maheen', NULL, 'Amjad', 'maheen.amjad@example.com', '03039123456', 'fast12', 'Student', 1),
(40, 'Shoaib', 'Amin', 'Hassan', 'shoaib.hassan@example.com', '03040123456', 'secure89', 'Student', 1);

select*from [User]

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(41, 'Anum', 'Rehan', 'Munir', 'anum.munir@example.com', '03041123456', 'safe200', 'Student', 1),
(42, 'Saad', NULL, 'Ibrahim', 'saad.ibrahim@example.com', '03042123456', 'pass45', 'Student', 1),
(43, 'Raza', 'Jamal', 'Hussain', 'raza.hussain@example.com', '03043123456', 'code001', 'Student', 1),
(44, 'Rameez', NULL, 'Latif', 'rameez.latif@example.com', '03044123456', 'my7860', 'Student', 1),
(45, 'Arif', 'Shah', 'Zaman', 'arif.zaman@example.com', '03045123456', 'secure78', 'Student', 1),
(46, 'Nabila', 'Sana', 'Khalid', 'nabila.khalid@example.com', '03046123456', 'unlocker', 'Student', 1),
(47, 'Faisal', NULL, 'Masood', 'faisal.masood@example.com', '03047123456', 'mypass', 'Student', 1),
(48, 'Zahra', NULL, 'Haque', 'zahra.haque@example.com', '03048123456', 'fastend', 'Student', 1),
(49, 'Basit', 'Hussain', 'Ali', 'basit.ali@example.com', '03049123456', 'locksafe', 'Student', 1),
(50, 'Amber', NULL, 'Tariq', 'amber.tariq@example.com', '03050123456', 'lock2000', 'Student', 1);


INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(51, 'Farhan', 'Ali', 'Riaz', 'farhan.riaz@example.com', '03051123456', 'safe5678', 'Student', 1),
(52, 'Habib', NULL, 'Ullah', 'habib.ullah@example.com', '03052123456', 'word4321', 'Student', 1),
(53, 'Nazish', 'Akhtar', 'Hussain', 'nazish.hussain@example.com', '03053123456', 'secure90', 'Student', 1),
(54, 'Adil', 'Ahmed', 'Malik', 'adil.malik@example.com', '03054123456', 'code1235', 'Student', 1),
(55, 'Shafaq', NULL, 'Iqbal', 'shafaq.iqbal@example.com', '03055123456', 'open5678', 'Student', 1),
(56, 'Rameesha', 'Saeed', 'Shah', 'rameesha.shah@example.com', '03056123456', 'fast2345', 'Student', 1),
(57, 'Zeeshan', 'Ali', 'Khan', 'zeeshan.khan@example.com', '03057123456', 'lock7890', 'Student', 1),
(58, 'Shan', 'Ahmed', 'Farooq', 'shan.farooq@example.com', '03058123456', 'star4567', 'Student', 1),
(59, 'Huma', NULL, 'Munir', 'huma.munir@example.com', '03059123456', 'code5400', 'Student', 1),
(60, 'Muzammil', 'Rehan', 'Latif', 'muzammil.latif@example.com', '03060123456', 'unlock76', 'Student', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(61, 'Aliya', NULL, 'Riaz', 'aliya.riaz@example.com', '03061123456', 'fast6543', 'Student', 1),
(62, 'Waleed', 'Kamal', 'Sultan', 'waleed.sultan@example.com', '03062123456', 'secure98', 'Student', 1),
(63, 'Shahnaz', 'Tariq', 'Bukhari', 'shahnaz.bukhari@example.com', '03063123456', 'safe0000', 'Student', 1),
(64, 'Hamid', NULL, 'Qureshi', 'hamid.qureshi@example.com', '03064123456', 'word5669', 'Student', 1),
(65, 'Sadia', 'Ali', 'Zaman', 'sadia.zaman@example.com', '03065123456', 'code1234', 'Student', 1),
(66, 'Taha', 'Rafiq', 'Jamil', 'taha.jamil@example.com', '03066123456', 'lock8000', 'Student', 1),
(67, 'Fatima', NULL, 'Ali', 'fatima.ali@example.com', '03067123456', 'star7654', 'Student', 1),
(68, 'Azhar', 'Rehman', 'Ansari', 'azhar.ansari@example.com', '03068123456', 'secure54', 'Student', 1),
(69, 'Nida', 'Tariq', 'Mir', 'nida.mir@example.com', '03069123456', 'fast8765', 'Student', 1),
(70, 'Zubair', 'Saeed', 'Chaudhry', 'zubair.chaudhry@example.com', '03070123456', 'safe9876', 'Student', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(71, 'Afshan', NULL, 'Shafiq', 'afshan.shafiq@example.com', '03071123456', 'word3456', 'Student', 1),
(72, 'Rafi', 'Ahmed', 'Javed', 'rafi.javed@example.com', '03072123456', 'code6543', 'Student', 1),
(73, 'Shaista', 'Ali', 'Hassan', 'shaista.hassan@example.com', '03073123456', 'lock5432', 'Student', 1),
(74, 'Imtiaz', 'Riaz', 'Butt', 'imtiaz.butt@example.com', '03074123456', 'star1234', 'Student', 1),
(75, 'Arslan', NULL, 'Iqbal', 'arslan.iqbal@example.com', '03075123456', 'secure00', 'Student', 1),
(76, 'Aneeqa', 'Tahir', 'Latif', 'aneeqa.latif@example.com', '03076123456', 'fast2300', 'Student', 1),
(77, 'Mubeen', 'Ali', 'Zahid', 'mubeen.zahid@example.com', '03077123456', 'word6543', 'Student', 1),
(78, 'Zainab', NULL, 'Ahmed', 'zainab.ahmed@example.com', '03078123456', 'safe0987', 'Student', 1),
(79, 'Javed', 'Shah', 'Malik', 'javed.malik@example.com', '03079123456', 'unlock78', 'Student', 1),
(80, 'Shaheen', 'Rehman', 'Asif', 'shaheen.asif@example.com', '03080123456', 'secure10', 'Student', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(81, 'Ahmad', NULL, 'Mumtaz', 'ahmad.mumtaz@example.com', '03081123456', 'fast7654', 'Student', 1),
(82, 'Rabia', 'Ali', 'Rizwan', 'rabia.rizwan@example.com', '03082123456', 'safe5432', 'Student', 1),
(83, 'Farah', NULL, 'Tariq', 'farah.tariq@example.com', '03083123456', 'star4321', 'Student', 1),
(84, 'Salman', 'Ahmed', 'Butt', 'salman.butt@example.com', '03084123456', 'secure12', 'Student', 1),
(85, 'Arisha', NULL, 'Zahid', 'arisha.zahid@example.com', '03085123456', 'lock8765', 'Student', 1),
(86, 'Shahid', 'Ali', 'Murtaza', 'shahid.murtaza@example.com', '03086123456', 'code5432', 'Student', 1),
(87, 'Amir', 'Tariq', 'Iqbal', 'amir.iqbal@example.com', '03087123456', 'word2345', 'Student', 1),
(88, 'Ayesha', NULL, 'Rehman', 'ayesha.rehman@example.com', '03088123456', 'safe6789', 'Student', 1),
(89, 'Nabeel', 'Shah', 'Ali', 'nabeel.ali@example.com', '03089123456', 'unlock00', 'Student', 1),
(90, 'Bushra', NULL, 'Ahmed', 'bushra.ahmed@example.com', '03090123456', 'secure07', 'Student', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(91, 'Rehan', 'Ali', 'Ahmed', 'rehan.ahmed@example.com', '03091123456', 'pass2323', 'Recruiter', 1),
(92, 'Zubair', NULL, 'Hassan', 'zubair.hassan@example.com', '03092123456', 'lock2222', 'Recruiter', 0),
(93, 'Anam', 'Tariq', 'Latif', 'anam.latif@example.com', '03093123456', 'star3333', 'Recruiter', 1),
(94, 'Kamran', 'Ali', 'Farooq', 'kamran.farooq@example.com', '03094123456', 'safe3333', 'Recruiter', 0),
(95, 'Nimra', NULL, 'Sheikh', 'nimra.sheikh@example.com', '03095123456', 'code5555', 'Recruiter', 1),
(96, 'Faisal', 'Rehman', 'Siddiqui', 'faisal.siddiqui@example.com', '03096123456', 'word6666', 'Recruiter', 1),
(97, 'Kiran', 'Ali', 'Riaz', 'kiran.riaz@example.com', '03097123456', 'fast7777', 'Recruiter', 0),
(98, 'Arsalan', 'Hussain', 'Malik', 'arsalan.malik@example.com', '03098123456', 'secure22', 'Recruiter', 1),
(99, 'Ayesha', NULL, 'Zahid', 'ayesha.zahid@example.com', '03099123456', 'pass9999', 'Recruiter', 0),
(100, 'Shahzaib', 'Ali', 'Tariq', 'shahzaib.tariq@example.com', '03100123456', 'lock0000', 'Recruiter', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(101, 'Saima', 'Kamal', 'Javed', 'saima.javed@example.com', '03101123456', 'safe1234', 'Recruiter', 1),
(102, 'Zain', NULL, 'Ahmed', 'zain.ahmed@example.com', '03102123456', 'word5678', 'Recruiter', 0),
(103, 'Rubina', 'Ali', 'Farhan', 'rubina.farhan@example.com', '03103123456', 'star9876', 'Recruiter', 1),
(104, 'Shafiq', 'Riaz', 'Butt', 'shafiq.butt@example.com', '03104123456', 'secure43', 'Recruiter', 0),
(105, 'Muzamil', 'Tariq', 'Hassan', 'muzamil.hassan@example.com', '03105123456', 'code3210', 'Recruiter', 1),
(106, 'Anila', NULL, 'Qureshi', 'anila.qureshi@example.com', '03106123456', 'fast4567', 'Recruiter', 1),
(107, 'Haider', 'Ali', 'Aslam', 'haider.aslam@example.com', '03107123456', 'lock8901', 'Recruiter', 0),
(108, 'Bushra', 'Tariq', 'Raza', 'bushra.raza@example.com', '03108123456', 'word3333', 'Recruiter', 1),
(109, 'Maham', NULL, 'Zaman', 'maham.zaman@example.com', '03109123456', 'safe4444', 'Recruiter', 0),
(110, 'Fahad', 'Ali', 'Shah', 'fahad.shah@example.com', '03110123456', 'star5555', 'Recruiter', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(111, 'Rahim', 'Ali', 'Iqbal', 'rahim.iqbal@example.com', '03111123456', 'pass1111', 'Booth_Coordinator', 1),
(112, 'Sara', NULL, 'Tariq', 'sara.tariq@example.com', '03112123456', 'lock*765', 'Booth_Coordinator', 1),
(113, 'Bilal', 'Hassan', 'Malik', 'bilal.malik@example.com', '03113123456', 'star&*99', 'Booth_Coordinator', 1),
(114, 'Kamran', NULL, 'Shah', 'kamran.shah@example.com', '03114123456', 'safeopi1', 'Booth_Coordinator', 1),
(115, 'Zainab', 'Ali', 'Rizwan', 'zainab.rizwan@example.com', '03115123456', 'codeplm0', 'Booth_Coordinator', 1),
(116, 'Adil', NULL, 'Farooq', 'adil.farooq@example.com', '03116123456', 'wordghj7', 'Booth_Coordinator', 1),
(117, 'Hira', 'Tariq', 'Latif', 'hira.latif@example.com', '03117123456', 'fast7fxz', 'Booth_Coordinator', 1),
(118, 'Saad', NULL, 'Ahmed', 'saad.ahmed@example.com', '03118123456', 'secure6q', 'Booth_Coordinator', 1),
(119, 'Reema', 'Ali', 'Riaz', 'reema.riaz@example.com', '03119123456', 'pass9asd', 'Booth_Coordinator', 1),
(120, 'Fahad', NULL, 'Tahir', 'fahad.tahir@example.com', '03120123456', 'lock0vvv', 'Booth_Coordinator', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(121, 'Bushra', 'Rehan', 'Iqbal', 'bushra.iqbal@example.com', '03121123456', 'safeqwe3', 'Booth_Coordinator', 1),
(122, 'Hamza', NULL, 'Shah', 'hamza.shah@example.com', '03122123456', 'wordfyn4', 'Booth_Coordinator', 1),
(123, 'Laiba', 'Ali', 'Farhan', 'laiba.farhan@example.com', '03123123456', 'star6kkk', 'Booth_Coordinator', 1),
(124, 'Noman', NULL, 'Hassan', 'noman.hassan@example.com', '03124123456', 'secure3x', 'Booth_Coordinator', 1),
(125, 'Arslan', 'Tariq', 'Butt', 'arslan.butt@example.com', '03125123456', 'code3ttt', 'Booth_Coordinator', 1),
(126, 'Rubina', NULL, 'Ahmed', 'rubina.ahmed@example.com', '03126123456', 'fast4qqq', 'Booth_Coordinator', 1),
(127, 'Tariq', 'Ali', 'Aslam', 'tariq.aslam@example.com', '03127123456', 'lock89io', 'Booth_Coordinator', 1),
(128, 'Shaheen', NULL, 'Latif', 'shaheen.latif@example.com', '03128123456', 'word33k3', 'Booth_Coordinator', 1),
(129, 'Muzammil', 'Rehan', 'Raza', 'muzammil.raza@example.com', '03129123456', 'safe4v44', 'Booth_Coordinator', 1),
(130, 'Aiman', NULL, 'Zaman', 'aiman.zaman@example.com', '03130123456', 'star5c55', 'Booth_Coordinator', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(131, 'Shafiq', 'Ali', 'Shah', 'shafiq.shah@example.com', '03131123456', 'securea9', 'Booth_Coordinator', 1),
(132, 'Sadia', NULL, 'Riaz', 'sadia.riaz@example.com', '03132123456', 'passb111', 'Booth_Coordinator', 1),
(133, 'Farah', 'Ahmed', 'Butt', 'farah.butt@example.com', '03133123456', 'lockc222', 'Booth_Coordinator', 1),
(134, 'Faisal', NULL, 'Tariq', 'faisal.tariq@example.com', '03134123456', 'stard333', 'Booth_Coordinator', 1),
(135, 'Irfan', 'Rehan', 'Iqbal', 'irfan.iqbal@example.com', '03135123456', 'safee444', 'Booth_Coordinator', 1),
(136, 'Sana', NULL, 'Shah', 'sana.shah@example.com', '03136123456', 'code55f5', 'Booth_Coordinator', 1),
(137, 'Maha', 'Ali', 'Riaz', 'maha.riaz@example.com', '03137123456', 'word6g66', 'Booth_Coordinator', 1),
(138, 'Shaista', NULL, 'Ahmed', 'shaista.ahmed@example.com', '03138123456', 'fast7h77', 'Booth_Coordinator', 1),
(139, 'Yasir', 'Rehan', 'Latif', 'yasir.latif@example.com', '03139123456', 'securei7', 'Booth_Coordinator', 1),
(140, 'Zehra', NULL, 'Tariq', 'zehra.tariq@example.com', '03140123456', 'pass9k99', 'Booth_Coordinator', 1);

INSERT INTO [User] (UserId, FirstName, MiddleName, LastName, Email, Phone, Password, Role, IsActive) VALUES
(141, 'Sami', 'Ali', 'Hassan', 'sami.hassan@example.com', '03141123456', 'fasta111', 'Booth_Coordinator', 1),
(142, 'Faiza', NULL, 'Khan', 'faiza.khan@example.com', '03142123456', 'safeb222', 'Booth_Coordinator', 1),
(143, 'Arham', 'Tariq', 'Latif', 'arham.latif@example.com', '03143123456', 'wordc333', 'Booth_Coordinator', 1),
(144, 'Maham', NULL, 'Rehman', 'maham.rehman@example.com', '03144123456', 'lock4d44', 'Booth_Coordinator', 1),
(145, 'Ali', 'Shah', 'Farhan', 'ali.farhan@example.com', '03145123456', 'code55e5', 'Booth_Coordinator', 1),
(146, 'Rida', NULL, 'Tariq', 'rida.tariq@example.com', '03146123456', 'star66f6', 'Booth_Coordinator', 1),
(147, 'Waqar', 'Ahmed', 'Siddiqui', 'waqar.siddiqui@example.com', '031471234g6', 'pass7777', 'Booth_Coordinator', 1),
(148, 'Hina', 'Ali', 'Zafar', 'hina.zafar@example.com', '03148123456', 'fast88h8', 'Booth_Coordinator', 1),
(149, 'Saba', NULL, 'Kamal', 'saba.kamal@example.com', '03149123456', 'safe99i9', 'Booth_Coordinator', 1),
(150, 'Adeel', 'Riaz', 'Ali', 'adeel.ali@example.com', '03150123456', 'lock00j0', 'Booth_Coordinator', 1),
(151, 'Nashit', NULL, 'Ahmed', 'nashit.ahmed@example.com', '03151123456', 'word12k4', 'Booth_Coordinator', 1),
(152, 'Zoya', 'Hassan', 'Qureshi', 'zoya.qureshi@example.com', '03152123456', 'code43l1', 'Booth_Coordinator', 1),
(153, 'Ahsan', NULL, 'Hameed', 'ahsan.hameed@example.com', '03153123456', 'star56m8', 'Booth_Coordinator', 1),
(154, 'Nazia', 'Tariq', 'Iqbal', 'nazia.iqbal@example.com', '03154123456', 'safe87n5', 'Booth_Coordinator', 1),
(155, 'Kashif', 'Ali', 'Raza', 'kashif.raza@example.com', '03155123456', 'fast65o3', 'Booth_Coordinator', 1),
(156, 'Mehwish', NULL, 'Hassan', 'mehwish.hassan@example.com', '03156123456', 'lock7p11', 'Booth_Coordinator', 1),
(157, 'Qasim', 'Rehan', 'Latif', 'qasim.latif@example.com', '03157123456', 'star33q3', 'Booth_Coordinator', 1),
(158, 'Asif', 'Ali', 'Shah', 'asif.shah@example.com', '03158123456', 'word66r6', 'Booth_Coordinator', 1),
(159, 'Rubab', NULL, 'Tariq', 'rubab.tariq@example.com', '03159123456', 'fast5s32', 'Booth_Coordinator', 1),
(160, 'Ibrahim', 'Riaz', 'Ahmed', 'ibrahim.ahmed@example.com', '03160123456', 'safe7t54', 'Booth_Coordinator', 1),
(161, 'Zain', 'Ali', 'Qureshi', 'zain.qureshi@example.com', '03161123456', 'passu111', 'Tpo', 1),
(162, 'Rabia', NULL, 'Tariq', 'rabia.tariq@example.com', '03162123456', 'lock2v22', 'Tpo', 1),
(163, 'Fahad', 'Hassan', 'Latif', 'fahad.latif@example.com', '03163123456', 'starw333', 'Tpo', 1),
(164, 'Ayesha', NULL, 'Farooq', 'ayesha.farooq@example.com', '03164123456', 'safex444', 'Tpo', 1),
(165, 'Bilal', 'Ali', 'Khan', 'bilal.khan@example.com', '03165123456', 'code5y55', 'Tpo', 1),
(166, 'Hina', NULL, 'Riaz', 'hina.riaz@example.com', '03166123456', 'word66z6', 'Tpo', 1),
(167, 'Ali', 'Shah', 'Ahmed', 'ali.ahmed@example.com', '03167123456', 'fast7!77', 'Tpo', 1),
(168, 'Arslan', NULL, 'Latif', 'arslan.latif@example.com', '03168123456', 'secure@8', 'Tpo', 1),
(169, 'Zara', 'Ali', 'Siddiqui', 'zara.siddiqui@example.com', '03169123456', 'pass#999', 'Tpo', 1),
(170, 'Saba', NULL, 'Sheikh', 'saba.sheikh@example.com', '03170123456', 'lock00%0', 'Tpo', 1),
(171, 'Rehan', 'Hassan', 'Malik', 'rehan.malik@example.com', '03171123456', 'saf$1234', 'Tpo', 1),
(172, 'Anum', NULL, 'Farhan', 'anum.farhan@example.com', '03172123456', 'word56&8', 'Tpo', 1),
(173, 'Hamza', 'Tariq', 'Aslam', 'hamza.aslam@example.com', '03173123456', 'star*876', 'Tpo', 1),
(174, 'Fatima', NULL, 'Zafar', 'fatima.zafar@example.com', '03174123456', 'secure(5', 'Tpo', 1),
(175, 'Shafiq', 'Ali', 'Hameed', 'shafiq.hameed@example.com', '03175123456', 'code)210', 'Tpo', 1);


INSERT INTO Companies (CompanyID, Name, Sector, City, Street, Country, ContactInfo) VALUES
(1, 'Tech Innovators', 'Information Technology', 'Karachi', 'Shahrah-e-Faisal', 'Pakistan', '021-3456789'),
(2, 'Green Energy Solutions', 'Renewable Energy', 'Islamabad', 'G-11/3 Street', 'Pakistan', '051-2345678'),
(3, 'Pak Agro Farms', 'Agriculture', 'Lahore', 'Mall Road', 'Pakistan', '042-4567890'),
(4, 'Smart Build Corp', 'Construction', 'Rawalpindi', 'Murree Road', 'Pakistan', '051-5678901'),
(5, 'Fast Motors', 'Automobile', 'Faisalabad', 'Industrial Area', 'Pakistan', '041-6789012'),
(6, 'HealthFirst', 'Healthcare', 'Multan', 'Cantt Road', 'Pakistan', '061-2345678'),
(7, 'EduLift Academy', 'Education', 'Peshawar', 'University Town', 'Pakistan', '091-3456789'),
(8, 'Pak Fashion Hub', 'Textile', 'Sialkot', 'Wazirabad Road', 'Pakistan', '052-4567890'),
(9, 'Digital Dreams', 'IT Services', 'Gujranwala', 'Model Town', 'Pakistan', '055-5678901'),
(10, 'Skyline Constructions', 'Real Estate', 'Quetta', 'Airport Road', 'Pakistan', '081-6789012'),
(11, 'Eco Travels', 'Tourism', 'Bahawalpur', 'Circular Road', 'Pakistan', '062-2345678'),
(12, 'MediCare Group', 'Pharmaceuticals', 'Karachi', 'DHA Phase 5', 'Pakistan', '021-3456789'),
(13, 'PakFood Industries', 'Food Processing', 'Lahore', 'Johar Town', 'Pakistan', '042-4567890'),
(14, 'Urban Designs', 'Architecture', 'Islamabad', 'Blue Area', 'Pakistan', '051-5678901'),
(15, 'Sky High Airlines', 'Aviation', 'Rawalpindi', 'Airport Road', 'Pakistan', '051-6789012'),
(16, 'SolarTech Systems', 'Energy Solutions', 'Hyderabad', 'Latifabad', 'Pakistan', '022-2345678'),
(17, 'WaterPurity Co.', 'Water Solutions', 'Karachi', 'Korangi Industrial Area', 'Pakistan', '021-3456789'),
(18, 'Excellence Consulting', 'Management', 'Faisalabad', 'Peoples Colony', 'Pakistan', '041-5678901'),
(19, 'Clean Pak Services', 'Waste Management', 'Lahore', 'Model Town', 'Pakistan', '042-6789012'),
(20, 'Visionary Media', 'Advertising', 'Islamabad', 'F-7 Markaz', 'Pakistan', '051-2345678'),
(21, 'SecureTech', 'Cybersecurity', 'Karachi', 'I.I. Chundrigar Road', 'Pakistan', '021-4567890'),
(22, 'Urban Life Developers', 'Property Management', 'Peshawar', 'Hayatabad', 'Pakistan', '091-3456789'),
(23, 'Agri Innovations', 'Agriculture Technology', 'Multan', 'Industrial Estate', 'Pakistan', '061-5678901'),
(24, 'Global Maritime', 'Shipping', 'Gawadar', 'Coastal Highway', 'Pakistan', '086-2345678'),
(25, 'NextGen Solutions', 'Software Development', 'Karachi', 'Clifton', 'Pakistan', '021-6789012'),
(26, 'Fast Logistics', 'Transport', 'Quetta', 'Sariab Road', 'Pakistan', '081-2345678'),
(27, 'Prime Textiles', 'Manufacturing', 'Sialkot', 'Defence Area', 'Pakistan', '052-3456789'),
(28, 'Bright Future Academy', 'Education', 'Gujranwala', 'Main Boulevard', 'Pakistan', '055-4567890'),
(29, 'Urban Essentials', 'Retail', 'Lahore', 'Cantt Area', 'Pakistan', '042-5678901'),
(30, 'Rapid Solutions', 'Startup Incubator', 'Islamabad', 'G-6 Sector', 'Pakistan', '051-6789012');


INSERT INTO Student (StudentId, DegreeProgram, CurrentSemester, cgpa, UserId) VALUES
('23i-0001', 'SE', 1, 3.2, 1),
('23i-0002', 'CS', 2, 3.8, 2),
('23i-0003', 'AI', 3, 2.9, 3),
('23i-0004', 'DS', 4, 3.5, 4),
('23i-0005', 'CYS', 5, 3.7, 5),
('23i-0006', 'SE', 6, 3.1, 6),
('23i-0007', 'CS', 7, 3.4, 7),
('23i-0008', 'AI', 8, 3.0, 8),
('23i-0009', 'DS', 1, 2.8, 9),
('23i-0010', 'CYS', 2, 3.6, 10),
('23i-0011', 'SE', 3, 3.3, 11),
('23i-0012', 'CS', 4, 3.9, 12),
('23i-0013', 'AI', 5, 2.7, 13),
('23i-0014', 'DS', 6, 3.0, 14),
('23i-0015', 'CYS', 7, 3.8, 15),
('23i-0016', 'SE', 8, 2.9, 16),
('23i-0017', 'CS', 1, 3.7, 17),
('23i-0018', 'AI', 2, 3.4, 18),
('23i-0019', 'DS', 3, 3.1, 19),
('23i-0020', 'CYS', 4, 3.5, 20),
('23i-0021', 'SE', 5, 3.6, 21),
('23i-0022', 'CS', 6, 3.8, 22),
('23i-0023', 'AI', 7, 2.8, 23),
('23i-0024', 'DS', 8, 3.2, 24),
('23i-0025', 'CYS', 1, 3.4, 25),
('23i-0026', 'SE', 2, 3.1, 26),
('23i-0027', 'CS', 3, 3.9, 27),
('23i-0028', 'AI', 4, 3.2, 28),
('23i-0029', 'DS', 5, 2.9, 29),
('23i-0030', 'CYS', 6, 3.6, 30),
('23i-0031', 'SE', 7, 3.5, 31),
('23i-0032', 'CS', 8, 3.1, 32),
('23i-0033', 'AI', 1, 3.0, 33),
('23i-0034', 'DS', 2, 3.8, 34),
('23i-0035', 'CYS', 3, 2.7, 35),
('23i-0036', 'SE', 4, 3.2, 36),
('23i-0037', 'CS', 5, 3.4, 37),
('23i-0038', 'AI', 6, 3.3, 38),
('23i-0039', 'DS', 7, 2.8, 39),
('23i-0040', 'CYS', 8, 3.5, 40),
('23i-0041', 'SE', 1, 3.6, 41),
('23i-0042', 'CS', 2, 3.9, 42),
('23i-0043', 'AI', 3, 3.0, 43),
('23i-0044', 'DS', 4, 2.9, 44),
('23i-0045', 'CYS', 5, 3.7, 45),
('23i-0046', 'SE', 6, 3.3, 46),
('23i-0047', 'CS', 7, 3.2, 47),
('23i-0048', 'AI', 8, 3.7, 48),
('23i-0049', 'DS', 1, 3.0, 49),
('23i-0050', 'CYS', 2, 2.9, 50),
('23i-0051', 'SE', 3, 3.5, 51),
('23i-0052', 'CS', 4, 3.8, 52),
('23i-0053', 'AI', 5, 3.2, 53),
('23i-0054', 'DS', 6, 3.0, 54),
('23i-0055', 'CYS', 7, 3.1, 55),
('23i-0056', 'SE', 8, 3.6, 56),
('23i-0057', 'CS', 1, 3.7, 57),
('23i-0058', 'AI', 2, 3.9, 58),
('23i-0059', 'DS', 3, 3.1, 59),
('23i-0060', 'CYS', 4, 2.8, 60),
('23i-0061', 'SE', 5, 3.5, 61),
('23i-0062', 'CS', 6, 3.3, 62),
('23i-0063', 'AI', 7, 3.0, 63),
('23i-0064', 'DS', 8, 2.9, 64),
('23i-0065', 'CYS', 1, 3.4, 65),
('23i-0066', 'SE', 2, 3.2, 66),
('23i-0067', 'CS', 3, 3.6, 67),
('23i-0068', 'AI', 4, 3.8, 68),
('23i-0069', 'DS', 5, 3.1, 69),
('23i-0070', 'CYS', 6, 3.0, 70),
('23i-0071', 'SE', 7, 3.3, 71),
('23i-0072', 'CS', 8, 3.5, 72),
('23i-0073', 'AI', 1, 3.2, 73),
('23i-0074', 'DS', 2, 3.0, 74),
('23i-0075', 'CYS', 3, 3.7, 75),
('23i-0076', 'SE', 4, 3.6, 76),
('23i-0077', 'CS', 5, 3.9, 77),
('23i-0078', 'AI', 6, 3.8, 78),
('23i-0079', 'DS', 7, 3.3, 79),
('23i-0080', 'CYS', 8, 3.2, 80),
('23i-0081', 'SE', 1, 3.0, 81),
('23i-0082', 'CS', 2, 3.1, 82),
('23i-0083', 'AI', 3, 2.9, 83),
('23i-0084', 'DS', 4, 3.4, 84),
('23i-0085', 'CYS', 5, 3.5, 85),
('23i-0086', 'SE', 6, 3.8, 86),
('23i-0087', 'CS', 7, 3.2, 87),
('23i-0088', 'AI', 8, 3.3, 88),
('23i-0089', 'DS', 1, 3.0, 89),
('23i-0090', 'CYS', 2, 3.7, 90);



INSERT INTO Recruiter (RecruiterId, UserId, is_approved, CompanyID) VALUES
(1, 91, 1, 1),
(2, 92, 0, 2),
(3, 93, 1, 3),
(4, 94, 0, 4),
(5, 95, 1, 5),
(6, 96, 1, 6),
(7, 97, 0, 7),
(8, 98, 1, 8),
(9, 99, 0, 9),
(10, 100, 1, 10),
(11, 101, 1, 11),
(12, 102, 0, 12),
(13, 103, 1, 13),
(14, 104, 0, 14),
(15, 105, 1, 15),
(16, 106, 1, 16),
(17, 107, 0, 17),
(18, 108, 1, 18),
(19, 109, 0, 19),
(20, 110, 1, 20);


INSERT INTO Tpo (TpoId, Office_location, UserId) VALUES
(1, 'Lahore Main Campus', 161),
(2, 'Islamabad Regional Office', 162),
(3, 'Karachi South Office', 163),
(4, 'Rawalpindi City Center', 164),
(5, 'Multan Trade Zone', 165),
(6, 'Faisalabad Industrial Area', 166),
(7, 'Peshawar Office', 167),
(8, 'Quetta Hub', 168),
(9, 'Sialkot Export Center', 169),
(10, 'Gujranwala Business District', 170),
(11, 'Hyderabad Office', 171),
(12, 'Bahawalpur Regional Office', 172),
(13, 'Gawadar Port Authority', 173),
(14, 'Sukkur Development Office', 174),
(15, 'Abbottabad Gateway Office', 175);


INSERT INTO Booth_Coordinator (BoothCoordinatorId, UserId, ShiftTimings) VALUES
(1, 111, '9:00 AM - 11:00 AM'),
(2, 112, '11:00 AM - 1:00 PM'),
(3, 113, '1:00 PM - 3:00 PM'),
(4, 114, '3:00 PM - 5:00 PM'),
(5, 115, '9:00 AM - 11:00 AM'),
(6, 116, '11:00 AM - 1:00 PM'),
(7, 117, '1:00 PM - 3:00 PM'),
(8, 118, '3:00 PM - 5:00 PM'),
(9, 119, '9:00 AM - 11:00 AM'),
(10, 120, '11:00 AM - 1:00 PM'),
(11, 121, '1:00 PM - 3:00 PM'),
(12, 122, '3:00 PM - 5:00 PM'),
(13, 123, '9:00 AM - 11:00 AM'),
(14, 124, '11:00 AM - 1:00 PM'),
(15, 125, '1:00 PM - 3:00 PM'),
(16, 126, '3:00 PM - 5:00 PM'),
(17, 127, '9:00 AM - 11:00 AM'),
(18, 128, '11:00 AM - 1:00 PM'),
(19, 129, '1:00 PM - 3:00 PM'),
(20, 130, '3:00 PM - 5:00 PM'),
(21, 131, '9:00 AM - 11:00 AM'),
(22, 132, '11:00 AM - 1:00 PM'),
(23, 133, '1:00 PM - 3:00 PM'),
(24, 134, '3:00 PM - 5:00 PM'),
(25, 135, '9:00 AM - 11:00 AM'),
(26, 136, '11:00 AM - 1:00 PM'),
(27, 137, '1:00 PM - 3:00 PM'),
(28, 138, '3:00 PM - 5:00 PM'),
(29, 139, '9:00 AM - 11:00 AM'),
(30, 140, '11:00 AM - 1:00 PM'),
(31, 141, '1:00 PM - 3:00 PM'),
(32, 142, '3:00 PM - 5:00 PM'),
(33, 143, '9:00 AM - 11:00 AM'),
(34, 144, '11:00 AM - 1:00 PM'),
(35, 145, '1:00 PM - 3:00 PM'),
(36, 146, '3:00 PM - 5:00 PM'),
(37, 147, '9:00 AM - 11:00 AM'),
(38, 148, '11:00 AM - 1:00 PM'),
(39, 149, '1:00 PM - 3:00 PM'),
(40, 150, '3:00 PM - 5:00 PM'),
(41, 151, '9:00 AM - 11:00 AM'),
(42, 152, '11:00 AM - 1:00 PM'),
(43, 153, '1:00 PM - 3:00 PM'),
(44, 154, '3:00 PM - 5:00 PM'),
(45, 155, '9:00 AM - 11:00 AM'),
(46, 156, '11:00 AM - 1:00 PM'),
(47, 157, '1:00 PM - 3:00 PM'),
(48, 158, '3:00 PM - 5:00 PM'),
(49, 159, '9:00 AM - 11:00 AM'),
(50, 160, '11:00 AM - 1:00 PM');


INSERT INTO JobFairEvents (EventId, Title, StartDate, EndDate, Venue, BoothSlots) VALUES
(1, 'Tech Careers Expo', '2025-05-01', '2025-05-02', 'Lahore Campus', 50),
(2, 'Future Innovators Fair', '2025-06-10', '2025-06-11', 'Karachi Campus', 40),
(3, 'AI Revolution Summit', '2025-07-15', '2025-07-16', 'Islamabad Campus', 30),
(4, 'Cyber Security Job Fest', '2025-08-05', '2025-08-06', 'Faisalabad Campus', 35),
(5, 'AgriTech Careers Expo', '2025-09-01', '2025-09-02', 'Peshawar Campus', 20),
(6, 'Health Careers Fair', '2025-10-10', '2025-10-11', 'Islamabad Campus', 25),
(7, 'Engineering Job Fair', '2025-11-01', '2025-11-02', 'Peshawar Campus', 45),
(8, 'Startup Success Fair', '2025-12-05', '2025-12-06', 'Karachi Campus', 50),
(9, 'Technology Futures Summit', '2026-01-15', '2026-01-16', 'Lahore Campus', 40),
(10, 'Industrial Innovation Forum', '2026-02-20', '2026-02-21', 'Faisalabad Campus', 35);



INSERT INTO Jobpostings (JobID, Title, Salary, Description, Street, City, Country, CompanyID, EventID) VALUES
(1, 'Software Engineer', 'PKR 120,000', 'Develop and maintain software systems.', 'Main Street', 'Lahore', 'Pakistan', 1, 1),
(2, 'Data Analyst', 'PKR 90,000', 'Analyze data trends to assist decision-making.', 'Mall Road', 'Karachi', 'Pakistan', 2, 2),
(3, 'AI Specialist', 'PKR 150,000', 'Design and implement AI solutions.', 'Tech Park', 'Islamabad', 'Pakistan', 3, 3),
(4, 'HR Manager', 'PKR 80,000', 'Manage human resources operations.', 'Civic Center', 'Rawalpindi', 'Pakistan', 4, 4),
(5, 'Marketing Executive', 'PKR 70,000', 'Create and execute marketing strategies.', 'Business Hub', 'Faisalabad', 'Pakistan', 5, 5),
(6, 'Cybersecurity Analyst', 'PKR 100,000', 'Protect systems from cyber threats.', 'Defence Road', 'Peshawar', 'Pakistan', 6, 6),
(7, 'Project Manager', 'PKR 110,000', 'Oversee and deliver projects on time.', 'Industrial Estate', 'Multan', 'Pakistan', 7, 7),
(8, 'Sales Executive', 'PKR 60,000', 'Build relationships with clients.', 'Airport Road', 'Quetta', 'Pakistan', 8, 8),
(9, 'Graphic Designer', 'PKR 50,000', 'Create visual content for campaigns.', 'Cultural Complex', 'Gujranwala', 'Pakistan', 9, 9),
(10, 'Backend Developer', 'PKR 130,000', 'Develop server-side logic.', 'IT Park', 'Sialkot', 'Pakistan', 10, 10),
(11, 'Frontend Developer', 'PKR 110,000', 'Create user-facing applications.', 'University Road', 'Bahawalpur', 'Pakistan', 11, 1),
(12, 'Content Writer', 'PKR 50,000', 'Produce engaging written content.', 'Media City', 'Lahore', 'Pakistan', 12, 2),
(13, 'Accountant', 'PKR 85,000', 'Manage financial records.', 'Finance Avenue', 'Karachi', 'Pakistan', 13, 3),
(14, 'UX Designer', 'PKR 120,000', 'Design user-centric digital interfaces.', 'Creative Hub', 'Islamabad', 'Pakistan', 14, 4),
(15, 'Operations Manager', 'PKR 130,000', 'Oversee daily business activities.', 'Trade Center', 'Rawalpindi', 'Pakistan', 15, 5),
(16, 'Database Administrator', 'PKR 140,000', 'Manage and maintain databases.', 'Main Square', 'Faisalabad', 'Pakistan', 16, 6),
(17, 'Digital Marketing Specialist', 'PKR 75,000', 'Plan and execute digital campaigns.', 'Marketing Street', 'Peshawar', 'Pakistan', 17, 7),
(18, 'Network Engineer', 'PKR 90,000', 'Design and implement network systems.', 'Tech Road', 'Multan', 'Pakistan', 18, 8),
(19, 'Business Analyst', 'PKR 95,000', 'Bridge the gap between business and IT.', 'Global Avenue', 'Quetta', 'Pakistan', 19, 9),
(20, 'Customer Support Specialist', 'PKR 55,000', 'Assist customers with inquiries.', 'Service Lane', 'Gujranwala', 'Pakistan', 20, 10),
(21, 'Mechanical Engineer', 'PKR 110,000', 'Design and build mechanical systems.', 'Engineering Block', 'Sialkot', 'Pakistan', 21, 1),
(22, 'Electrical Engineer', 'PKR 115,000', 'Develop and maintain electrical systems.', 'Innovation Boulevard', 'Bahawalpur', 'Pakistan', 22, 2),
(23, 'Product Manager', 'PKR 140,000', 'Lead product development.', 'Tech Square', 'Lahore', 'Pakistan', 23, 3),
(24, 'AI Researcher', 'PKR 160,000', 'Conduct AI research and experiments.', 'Research Hub', 'Karachi', 'Pakistan', 24, 4),
(25, 'Logistics Manager', 'PKR 90,000', 'Oversee supply chain operations.', 'Logistics Lane', 'Islamabad', 'Pakistan', 25, 5),
(26, 'Systems Analyst', 'PKR 100,000', 'Analyze and design IT solutions.', 'System Alley', 'Rawalpindi', 'Pakistan', 26, 6),
(27, 'Game Developer', 'PKR 145,000', 'Develop engaging video games.', 'Gaming Hub', 'Faisalabad', 'Pakistan', 27, 7),
(28, 'Bioinformatics Scientist', 'PKR 125,000', 'Analyze biological data.', 'Science Block', 'Peshawar', 'Pakistan', 28, 8),
(29, 'E-commerce Manager', 'PKR 85,000', 'Manage online retail operations.', 'E-commerce Center', 'Multan', 'Pakistan', 29, 9),
(30, 'Architect', 'PKR 150,000', 'Design and plan building structures.', 'Architecture Lane', 'Quetta', 'Pakistan', 30, 10);

INSERT INTO StudentSkills (Userid, skills) VALUES
('23i-0001', 'Software Engineering'), 
('23i-0001', 'Agile Methodology'),
('23i-0002', 'Data Structures'), 
('23i-0002', 'Operating Systems'),
('23i-0003', 'Machine Learning'), 
('23i-0003', 'Deep Learning'),
('23i-0004', 'Data Visualization'), 
('23i-0004', 'Statistical Analysis'),
('23i-0005', 'Network Security'), 
('23i-0005', 'Cybersecurity Fundamentals'),
('23i-0006', 'Software Testing'), 
('23i-0006', 'Scrum Methodology'),
('23i-0007', 'Database Systems'), 
('23i-0007', 'Algorithm Design'),
('23i-0008', 'Computer Vision'), 
('23i-0008', 'Neural Networks'),
('23i-0009', 'Big Data'), 
('23i-0009', 'Data Mining'),
('23i-0010', 'Malware Analysis'), 
('23i-0010', 'Digital Forensics'),
('23i-0011', 'Software Architecture'), 
('23i-0011', 'Project Management'),
('23i-0012', 'System Programming'), 
('23i-0012', 'Cloud Computing'),
('23i-0013', 'Natural Language Processing'), 
('23i-0013', 'AI Ethics'),
('23i-0014', 'Data Engineering'), 
('23i-0014', 'Data Analytics Tools'),
('23i-0015', 'Information Security'), 
('23i-0015', 'Risk Management'),
('23i-0016', 'Software Maintenance'), 
('23i-0016', 'Design Patterns'),
('23i-0017', 'Web Development'), 
('23i-0017', 'Mobile App Development'),
('23i-0018', 'Reinforcement Learning'), 
('23i-0018', 'Pattern Recognition'),
('23i-0019', 'Data Warehousing'), 
('23i-0019', 'ETL Processes'),
('23i-0020', 'Cryptography'), 
('23i-0020', 'Penetration Testing'),
('23i-0021', 'Software Lifecycle Management'), 
('23i-0021', 'Unit Testing'),
('23i-0022', 'Database Administration'), 
('23i-0022', 'Distributed Systems'),
('23i-0023', 'Expert Systems'), 
('23i-0023', 'AI Programming'),
('23i-0024', 'Visualization Tools'), 
('23i-0024', 'Big Data Analytics'),
('23i-0025', 'Ethical Hacking'), 
('23i-0025', 'Network Defense'),
('23i-0026', 'UML Modeling'), 
('23i-0026', 'Software Quality Assurance'),
('23i-0027', 'Computer Networks'), 
('23i-0027', 'Computer Architecture'),
('23i-0028', 'Cognitive Computing'), 
('23i-0028', 'AI Algorithms'),
('23i-0029', 'Data Preprocessing'), 
('23i-0029', 'Data Cleaning'),
('23i-0030', 'Cloud Security'), 
('23i-0030', 'Security Auditing'),
('23i-0031', 'Design Thinking'), 
('23i-0031', 'Agile Design'),
('23i-0032', 'Software Deployment'), 
('23i-0032', 'Concurrency'),
('23i-0033', 'Robotics'), 
('23i-0033', 'AI Simulation'),
('23i-0034', 'Data Scraping'), 
('23i-0034', 'Data Modeling'),
('23i-0035', 'Firewall Management'), 
('23i-0035', 'Incident Response'),
('23i-0036', 'Code Refactoring'), 
('23i-0036', 'Testing Frameworks'),
('23i-0037', 'Compiler Design'), 
('23i-0037', 'Operating Systems'),
('23i-0038', 'AI Research'), 
('23i-0038', 'Bayesian Networks'),
('23i-0039', 'SQL Optimization'), 
('23i-0039', 'R Programming'),
('23i-0040', 'Secure Programming'), 
('23i-0040', 'Access Control'),
('23i-0041', 'Clean Code'), 
('23i-0041', 'Software Metrics'),
('23i-0042', 'Data Structures & Algorithms'), 
('23i-0042', 'Software Engineering'),
('23i-0043', 'AI in Games'), 
('23i-0043', 'Knowledge Representation'),
('23i-0044', 'Data Pipelines'), 
('23i-0044', 'NoSQL Databases'),
('23i-0045', 'Cybersecurity Policies'), 
('23i-0045', 'Information Assurance'),
('23i-0046', 'DevOps Practices'), 
('23i-0046', 'Configuration Management'),
('23i-0047', 'Virtualization'), 
('23i-0047', 'Shell Scripting'),
('23i-0048', 'AI-driven Interfaces'), 
('23i-0048', 'AI Product Design'),
('23i-0049', 'Tableau'), 
('23i-0049', 'Data Integration'),
('23i-0050', 'Cyber Risk Analysis'), 
('23i-0050', 'Vulnerability Management'),
('23i-0051', 'Object Oriented Design'), 
('23i-0051', 'Version Control'),
('23i-0052', 'Mobile Computing'), 
('23i-0052', 'Computer Graphics'),
('23i-0053', 'Intelligent Systems'), 
('23i-0053', 'AI APIs'),
('23i-0054', 'Data Interpretation'), 
('23i-0054', 'Visualization Libraries'),
('23i-0055', 'SIEM Tools'), 
('23i-0055', 'Security Frameworks'),
('23i-0056', 'Software Integration'), 
('23i-0056', 'Release Management'),
('23i-0057', 'Parallel Computing'), 
('23i-0057', 'Scientific Computing'),
('23i-0058', 'AI Automation'), 
('23i-0058', 'AI Chatbots'),
('23i-0059', 'Data Querying'), 
('23i-0059', 'Data Aggregation'),
('23i-0060', 'Threat Analysis'), 
('23i-0060', 'Cyber Law'),
('23i-0061', 'Code Documentation'), 
('23i-0061', 'Process Modeling'),
('23i-0062', 'AI in Healthcare'), 
('23i-0062', 'Security in AI Systems'),
('23i-0063', 'AI Testing'), 
('23i-0063', 'Ethics in AI'),
('23i-0064', 'Scientific Data Analysis'), 
('23i-0064', 'ML Pipelines'),
('23i-0065', 'Cryptography Fundamentals'), 
('23i-0065', 'InfoSec Compliance'),
('23i-0066', 'API Development'), 
('23i-0066', 'Component Testing'),
('23i-0067', 'Software Modelling'), 
('23i-0067', 'Operating Systems'),
('23i-0068', 'Conversational AI'), 
('23i-0068', 'AI Frameworks'),
('23i-0069', 'Data Interpretation'), 
('23i-0069', 'Predictive Analysis'),
('23i-0070', 'System Hardening'), 
('23i-0070', 'Red Teaming'),
('23i-0071', 'Requirement Analysis'), 
('23i-0071', 'Design Documents'),
('23i-0072', 'Software Scaling'), 
('23i-0072', 'Performance Tuning'),
('23i-0073', 'AI Toolkits'), 
('23i-0073', 'Voice Recognition'),
('23i-0074', 'Data Modeling Tools'), 
('23i-0074', 'AI Analytics'),
('23i-0075', 'Cybersecurity Engineering'), 
('23i-0075', 'IT Governance'),
('23i-0076', 'Microservices Architecture'), 
('23i-0076', 'CI/CD Pipelines'),
('23i-0077', 'Parallel Algorithms'), 
('23i-0077', 'Data Science'),
('23i-0078', 'Deep Neural Networks'), 
('23i-0078', 'AI Optimization'),
('23i-0079', 'Dashboard Design'), 
('23i-0079', 'Data Communication'),
('23i-0080', 'Web Security'), 
('23i-0080', 'Threat Hunting'),
('23i-0081', 'Agile Testing'), 
('23i-0081', 'Code Reviews'),
('23i-0082', 'Scripting Languages'), 
('23i-0082', 'OS Internals'),
('23i-0083', 'AI Assistants'), 
('23i-0083', 'AI Content Creation'),
('23i-0084', 'Data Reporting'), 
('23i-0084', 'Scientific Computing'),
('23i-0085', 'Risk Control'), 
('23i-0085', 'Authentication Systems'),
('23i-0086', 'Software Delivery'), 
('23i-0086', 'System Design'),
('23i-0087', 'Compiler Construction'), 
('23i-0087', 'Hardware Interfaces'),
('23i-0088', 'AI Systems Integration'), 
('23i-0088', 'Speech Recognition'),
('23i-0089', 'BI Tools'), 
('23i-0089', 'Time Series Analysis'),
('23i-0090', 'Cloud Security Architecture'), 
('23i-0090', 'Log Management');



INSERT INTO Student_Certificates (Userid, CertificateName) 
VALUES 
('23i-0001', 'Certified Python Developer'),
('23i-0001', 'Certified Data Analyst'),
('23i-0002', 'Certified Data Analyst'),
('23i-0002', 'Agile Methodology Workshop'),
('23i-0003', 'Machine Learning Specialist'),
('23i-0003', 'AI & Deep Learning Certificate'),
('23i-0004', 'Full Stack Web Development'),
('23i-0004', 'UI/UX Design Certification'),
('23i-0005', 'Cybersecurity Fundamentals'),
('23i-0005', 'Certified Ethical Hacker (CEH)'),
('23i-0006', 'Team Management Skills'),
('23i-0006', 'UI/UX Design Certification'),
('23i-0007', 'Database Management Certificate'),
('23i-0007', 'Certified Ethical Hacker (CEH)'),
('23i-0008', 'AWS Certified Cloud Practitioner'),
('23i-0008', 'Cloud Security Architecture'),
('23i-0009', 'Mobile App Developer Certification'),
('23i-0009', 'Data Mining Specialist'),
('23i-0010', 'ISTQB Certified Tester'),
('23i-0010', 'Digital Forensics'),
('23i-0011', 'Machine Learning with Python'),
('23i-0011', 'AI & Deep Learning Certificate'),
('23i-0012', 'Digital Marketing Certificate'),
('23i-0012', 'Cloud Security Architecture'),
('23i-0013', 'AI & Deep Learning Certificate'),
('23i-0013', 'Machine Learning Specialist'),
('23i-0014', 'Technical Writing Certificate'),
('23i-0014', 'Embedded Systems Certification'),
('23i-0015', 'Frontend Engineering Certificate'),
('23i-0015', 'Certified Data Analyst'),
('23i-0016', 'Video Editing Masterclass'),
('23i-0016', 'Digital Marketing Certificate'),
('23i-0017', 'Unity Game Development Certificate'),
('23i-0017', 'Game Engines Developer Certificate'),
('23i-0018', 'Data Mining Specialist'),
('23i-0018', 'Data Analytics Tools'),
('23i-0019', 'SEO Optimization Workshop'),
('23i-0019', 'UI/UX Design Certification'),
('23i-0020', 'Creative Writing Program'),
('23i-0020', 'Digital Marketing Certificate'),
('23i-0021', 'Blockchain Developer Certificate'),
('23i-0021', 'AI Programming'),
('23i-0022', 'Embedded Systems Certification'),
('23i-0022', 'C++ Advanced Programming Certificate'),
('23i-0023', '3D Modeling Certificate'),
('23i-0023', 'Certified Ethical Hacker (CEH)'),
('23i-0024', 'Graphic Design Masterclass'),
('23i-0024', 'Digital Illustration Masterclass'),
('23i-0025', 'C++ Advanced Programming Certificate'),
('23i-0025', 'Data Mining Specialist'),
('23i-0026', 'Java Developer Certificate'),
('23i-0026', 'Certified Ethical Hacker (CEH)'),
('23i-0027', 'MATLAB for Engineers'),
('23i-0027', 'Certified Python Developer'),
('23i-0028', 'Robotics Fundamentals Certificate'),
('23i-0028', 'AI & Deep Learning Certificate'),
('23i-0029', 'Physics Simulations Workshop'),
('23i-0029', 'Machine Learning Specialist'),
('23i-0030', 'Linux Admin Certification'),
('23i-0030', 'Cloud Security Architecture'),
('23i-0031', 'Agile Methodology Workshop'),
('23i-0031', 'Certified Data Analyst'),
('23i-0032', 'Business Analysis Certificate'),
('23i-0032', 'AI Programming'),
('23i-0033', 'Natural Language Processing Certificate'),
('23i-0033', 'Machine Learning with Python'),
('23i-0034', 'E-commerce Management Workshop'),
('23i-0034', 'Certified Ethical Hacker (CEH)'),
('23i-0035', 'CRM Strategies Certificate'),
('23i-0035', 'AI & Deep Learning Certificate'),
('23i-0036', 'UI/UX Design Certification'),
('23i-0036', 'Video Editing Masterclass'),
('23i-0037', 'Network Security Essentials'),
('23i-0037', 'Digital Forensics'),
('23i-0038', 'IoT Developer Certification'),
('23i-0038', 'Cloud Security Architecture'),
('23i-0039', 'Supply Chain Analytics'),
('23i-0039', 'Data Mining Specialist'),
('23i-0040', 'Electrical Circuit Design Certificate'),
('23i-0040', 'Certified Ethical Hacker (CEH)'),
('23i-0041', 'Certified Ethical Hacker (CEH)'),
('23i-0041', 'Certified Python Developer'),
('23i-0042', 'Content Creation Specialist'),
('23i-0042', 'Business Intelligence Developer'),
('23i-0043', 'Digital Illustration Masterclass'),
('23i-0043', 'AI Programming'),
('23i-0044', 'Data Visualization with Tableau'),
('23i-0044', 'AI & Deep Learning Certificate'),
('23i-0045', 'Product Design Fundamentals'),
('23i-0045', 'Data Analytics Tools'),
('23i-0046', 'Research Analysis Training'),
('23i-0046', 'C++ Advanced Programming Certificate'),
('23i-0047', 'Business Intelligence Developer'),
('23i-0047', 'Cloud Security Architecture'),
('23i-0048', '3D Animation Certificate'),
('23i-0048', 'Video Editing Masterclass'),
('23i-0049', 'Mechanical Design Workshop'),
('23i-0049', 'Certified Data Analyst'),
('23i-0050', 'Structural Analysis Certificate'),
('23i-0050', 'Certified Ethical Hacker (CEH)'),
('23i-0051', 'Energy Management Program'),
('23i-0051', 'Certified Ethical Hacker (CEH)'),
('23i-0052', 'Video Game Design Certificate'),
('23i-0052', 'Frontend Engineering Certificate'),
('23i-0053', 'JavaScript Web Development'),
('23i-0053', 'UI/UX Design Certification'),
('23i-0054', 'Frontend Engineering Certificate'),
('23i-0054', 'Certified Ethical Hacker (CEH)'),
('23i-0055', 'Backend Integration Workshop'),
('23i-0055', 'Machine Learning Specialist'),
('23i-0056', 'Data Pipeline Engineering'),
('23i-0056', 'Business Intelligence Developer'),
('23i-0057', 'Statistical Analysis in SPSS'),
('23i-0057', 'Data Analytics Tools'),
('23i-0058', 'Mobile UX Design Bootcamp'),
('23i-0058', 'Game Engines Developer Certificate'),
('23i-0059', 'Cross-platform App Development'),
('23i-0059', 'Machine Learning Specialist'),
('23i-0060', 'R Programming Certificate'),
('23i-0060', 'Certified Ethical Hacker (CEH)'),
('23i-0061', 'Bioinformatics and Genomics'),
('23i-0061', 'Certified Data Analyst'),
('23i-0062', 'Photoshop Expert Certificate'),
('23i-0062', 'UI/UX Design Certification'),
('23i-0063', 'Risk Management Certification'),
('23i-0063', 'Cloud Security Architecture'),
('23i-0064', 'Innovation Management Training'),
('23i-0064', 'Data Analytics Tools'),
('23i-0065', 'Financial Analysis Certificate'),
('23i-0065', 'Certified Ethical Hacker (CEH)'),
('23i-0066', 'Business Strategy Fundamentals'),
('23i-0066', 'AI Programming'),
('23i-0067', 'Public Speaking Mastery'),
('23i-0067', 'Certified Data Analyst'),
('23i-0068', 'Consulting Essentials'),
('23i-0068', 'Cloud Security Architecture'),
('23i-0069', 'System Architecture Certificate'),
('23i-0069', 'Certified Ethical Hacker (CEH)'),
('23i-0070', 'Application Deployment Workshop'),
('23i-0070', 'Machine Learning Specialist'),
('23i-0071', 'Hardware Programming Lab'),
('23i-0071', 'Certified Ethical Hacker (CEH)'),
('23i-0072', 'Operational Research Certificate'),
('23i-0072', 'Data Analytics Tools'),
('23i-0073', 'Natural Language Processing Certificate'),
('23i-0073', 'AI Programming'),
('23i-0074', 'CI/CD Pipeline Engineering'),
('23i-0074', 'Cloud Security Architecture'),
('23i-0075', 'Data Mining Specialist'),
('23i-0075', 'Certified Python Developer'),
('23i-0076', 'Predictive Modeling Certificate'),
('23i-0076', 'Certified Ethical Hacker (CEH)'),
('23i-0077', 'Java Frameworks Mastery'),
('23i-0077', 'Frontend Engineering Certificate'),
('23i-0078', 'Customer Insights Certificate'),
('23i-0078', 'Mobile App Developer Certification'),
('23i-0079', 'Presentation Design Certificate'),
('23i-0079', 'Machine Learning Specialist'),
('23i-0080', 'Virtual Reality Developer Certification'),
('23i-0080', 'Cybersecurity Fundamentals'),
('23i-0081', 'Augmented Reality Developer Certification'),
('23i-0081', 'Game Engines Developer Certificate'),
('23i-0082', 'Security Protocols Certificate'),
('23i-0082', 'UI/UX Design Certification'),
('23i-0083', 'Social Media Management Training'),
('23i-0083', 'Certified Ethical Hacker (CEH)'),
('23i-0084', 'Video Production Mastery'),
('23i-0084', 'Certified Python Developer'),
('23i-0085', 'Advanced Circuit Design'),
('23i-0085', 'Data Analytics Tools'),
('23i-0086', 'Creative Problem Solving Workshop'),
('23i-0086', 'Machine Learning Specialist'),
('23i-0087', 'Team Management Skills'),
('23i-0087', 'Certified Data Analyst'),
('23i-0088', 'Machine Translation Specialist'),
('23i-0088', 'Artificial Intelligence APIs'),
('23i-0089', 'Game Engines Developer Certificate'),
('23i-0089', 'Customer Engagement Analytics'),
('23i-0090', 'Customer Engagement Analytics'),
('23i-0090', 'Log Management');



INSERT INTO Jobpostings_Type (Job_id, Type) VALUES
(1, 'Full-time'),
(1, 'On-site'),
(2, 'Full-time'),
(2, 'Remote'),
(3, 'Full-time'),
(4, 'Part-time'),
(5, 'Contract'),
(6, 'Full-time'),
(6, 'Remote'),
(7, 'Full-time'),
(8, 'Part-time'),
(9, 'Contract'),
(9, 'Remote'),
(10, 'Full-time'),
(11, 'Full-time'),
(11, 'Remote'),
(12, 'Part-time'),
(13, 'Full-time'),
(14, 'Full-time'),
(15, 'Full-time'),
(16, 'Full-time'),
(16, 'On-site'),
(17, 'Part-time'),
(17, 'Remote'),
(18, 'Full-time'),
(19, 'Contract'),
(20, 'Part-time'),
(20, 'Remote'),
(21, 'Full-time'),
(22, 'Full-time'),
(23, 'Full-time'),
(24, 'Full-time'),
(25, 'Full-time'),
(26, 'Full-time'),
(26, 'Remote'),
(27, 'Full-time'),
(28, 'Full-time'),
(29, 'Full-time'),
(29, 'Remote'),
(30, 'Contract');


INSERT INTO Jobpostings_RequiredSkills (Job_id, RequiredSkills)
VALUES
(1, 'C++'),
(1, 'Data Structures'),
(2, 'SQL'),
(2, 'Data Visualization'),
(3, 'Python'),
(3, 'TensorFlow'),
(4, 'HR Management'),
(4, 'Conflict Resolution'),
(5, 'Marketing Strategies'),
(5, 'SEO'),
(6, 'Network Security'),
(6, 'Penetration Testing'),
(7, 'Project Planning'),
(7, 'Agile Methodology'),
(8, 'Sales Pitching'),
(8, 'CRM Tools'),
(9, 'Adobe Illustrator'),
(9, 'Graphic Design Principles'),
(10, 'Node.js'),
(10, 'API Integration'),
(11, 'React.js'),
(11, 'HTML/CSS'),
(12, 'Content Writing'),
(12, 'SEO Writing'),
(13, 'Accounting'),
(13, 'QuickBooks'),
(14, 'Wireframing'),
(14, 'User Research'),
(15, 'Team Leadership'),
(15, 'Operations Strategy'),
(16, 'SQL Server'),
(16, 'Database Tuning'),
(17, 'Social Media Ads'),
(17, 'Google Analytics'),
(18, 'Cisco Networking'),
(18, 'LAN/WAN Setup'),
(19, 'Business Intelligence'),
(19, 'Stakeholder Analysis'),
(20, 'Customer Service'),
(20, 'Issue Resolution'),
(21, 'SolidWorks'),
(21, 'AutoCAD'),
(22, 'Circuit Design'),
(22, 'MATLAB'),
(23, 'Product Roadmapping'),
(23, 'User Feedback Analysis'),
(24, 'Research Methodology'),
(24, 'AI Modeling'),
(25, 'Supply Chain Optimization'),
(25, 'Logistics Software'),
(26, 'System Analysis'),
(26, 'UML Diagrams'),
(27, 'Unity'),
(27, 'C#'),
(28, 'Python'),
(28, 'Genomics'),
(29, 'Shopify'),
(29, 'Digital Payments'),
(30, 'AutoCAD'),
(30, 'Structural Design');

INSERT INTO Applications (ApplicationID, Status, [Date Applied], Userid, JobID)
VALUES
(1, 'Applied', '2025-03-10', '23I-0001', 1),
(2, 'Applied', '2025-03-11', '23I-0002', 2),
(3, 'Applied', '2025-03-12', '23I-0003', 3),
(4, 'Applied', '2025-03-13', '23I-0004', 4),
(5, 'Applied', '2025-03-14', '23I-0005', 5),
(6, 'Applied', '2025-03-15', '23I-0006', 6),
(7, 'Interview Scheduled', '2025-03-16', '23I-0007', 7),
(8, 'Applied', '2025-03-17', '23I-0008', 8),
(9, 'Applied', '2025-03-18', '23I-0009', 9),
(10, 'Applied', '2025-03-19', '23I-0010', 10),
(11, 'Applied', '2025-03-20', '23I-0011', 11),
(12, 'Interview Scheduled', '2025-03-21', '23I-0012', 12),
(13, 'Applied', '2025-03-22', '23I-0013', 13),
(14, 'Applied', '2025-03-23', '23I-0014', 14),
(15, 'Interview Scheduled', '2025-03-24', '23I-0015', 15),
(16, 'Applied', '2025-03-25', '23I-0016', 16),
(17, 'Applied', '2025-03-26', '23I-0017', 17),
(18, 'Applied', '2025-03-27', '23I-0018', 18),
(19, 'Interview Scheduled', '2025-03-28', '23I-0019', 19),
(20, 'Interview Scheduled', '2025-03-29', '23I-0020', 20),
(21, 'Applied', '2025-03-30', '23I-0021', 21),
(22, 'Applied', '2025-03-31', '23I-0022', 22),
(23, 'Applied', '2025-03-10', '23I-0023', 23),
(24, 'Interview Scheduled', '2025-03-11', '23I-0024', 24),
(25, 'Interview Scheduled', '2025-03-12', '23I-0025', 25),
(26, 'Applied', '2025-03-13', '23I-0026', 26),
(27, 'Applied', '2025-03-14', '23I-0027', 27),
(28, 'Interview Scheduled', '2025-03-15', '23I-0028', 28),
(29, 'Interview Scheduled', '2025-03-16', '23I-0029', 29),
(30, 'Applied', '2025-03-17', '23I-0030', 30),
(31, 'Applied', '2025-03-18', '23I-0031', 1),
(32, 'Applied', '2025-03-19', '23I-0032', 2),
(33, 'Applied', '2025-03-20', '23I-0033', 3),
(34, 'Applied', '2025-03-21', '23I-0034', 4),
(35, 'Applied', '2025-03-22', '23I-0035', 5),
(36, 'Applied', '2025-03-23', '23I-0036', 6),
(37, 'Applied', '2025-03-24', '23I-0037', 7),
(38, 'Interview Scheduled', '2025-03-25', '23I-0038', 8),
(39, 'Applied', '2025-03-26', '23I-0039', 9),
(40, 'Applied', '2025-03-27', '23I-0040', 10),
(41, 'Applied', '2025-03-28', '23I-0041', 11),
(42, 'Applied', '2025-03-29', '23I-0042', 12),
(43, 'Applied', '2025-03-30', '23I-0043', 13),
(44, 'Interview Scheduled', '2025-03-31', '23I-0044', 14),
(45, 'Applied', '2025-03-10', '23I-0045', 15),
(46, 'Applied', '2025-03-11', '23I-0046', 16),
(47, 'Applied', '2025-03-12', '23I-0047', 17),
(48, 'Applied', '2025-03-13', '23I-0048', 18),
(49, 'Applied', '2025-03-14', '23I-0049', 19),
(50, 'Applied', '2025-03-15', '23I-0050', 20),
(51, 'Applied', '2025-03-16', '23I-0051', 21),
(52, 'Applied', '2025-03-17', '23I-0052', 22),
(53, 'Interview Scheduled', '2025-03-18', '23I-0053', 23),
(54, 'Applied', '2025-03-19', '23I-0054', 24),
(55, 'Applied', '2025-03-20', '23I-0055', 25),
(56, 'Applied', '2025-03-21', '23I-0056', 26),
(57, 'Applied', '2025-03-22', '23I-0057', 27),
(58, 'Applied', '2025-03-23', '23I-0058', 28),
(59, 'Applied', '2025-03-24', '23I-0059', 29),
(60, 'Interview Scheduled', '2025-03-25', '23I-0060', 30),
(61, 'Applied', '2025-03-26', '23I-0061', 1),
(62, 'Applied', '2025-03-27', '23I-0062', 2),
(63, 'Applied', '2025-03-28', '23I-0063', 3),
(64, 'Applied', '2025-03-29', '23I-0064', 4),
(65, 'Applied', '2025-03-30', '23I-0065', 5),
(66, 'Applied', '2025-03-31', '23I-0066', 6),
(67, 'Applied', '2025-03-10', '23I-0067', 7),
(68, 'Applied', '2025-03-11', '23I-0068', 8),
(69, 'Applied', '2025-03-12', '23I-0069', 9),
(70, 'Applied', '2025-03-13', '23I-0070', 10),
(71, 'Applied', '2025-03-14', '23I-0071', 11),
(72, 'Applied', '2025-03-15', '23I-0072', 12),
(73, 'Applied', '2025-03-16', '23I-0073', 13),
(74, 'Applied', '2025-03-17', '23I-0074', 14),
(75, 'Applied', '2025-03-18', '23I-0075', 15),
(76, 'Interview Scheduled', '2025-03-19', '23I-0076', 16),
(77, 'Applied', '2025-03-20', '23I-0077', 17),
(78, 'Applied', '2025-03-21', '23I-0078', 18),
(79, 'Interview Scheduled', '2025-03-22', '23I-0079', 19),
(80, 'Applied', '2025-03-23', '23I-0080', 20),
(81, 'Applied', '2025-03-24', '23I-0081', 21),
(82, 'Applied', '2025-03-25', '23I-0082', 22),
(83, 'Applied', '2025-03-26', '23I-0083', 23),
(84, 'Applied', '2025-03-27', '23I-0084', 24),
(85, 'Applied', '2025-03-28', '23I-0085', 25),
(86, 'Applied', '2025-03-29', '23I-0086', 26),
(87, 'Applied', '2025-03-30', '23I-0087', 27),
(88, 'Applied', '2025-03-31', '23I-0088', 28),
(89, 'Applied', '2025-03-10', '23I-0089', 29),
(90, 'Applied', '2025-03-11', '23I-0090', 30),
(91, 'Applied', '2025-03-12', '23I-0011', 1),
(92, 'Interview Scheduled', '2025-03-13', '23I-0012', 2),
(93, 'Applied', '2025-03-14', '23I-0013', 3),
(94, 'Applied', '2025-03-15', '23I-0014', 4),
(95, 'Applied', '2025-03-16', '23I-0015', 5),
(96, 'Applied', '2025-03-17', '23I-0016', 6),
(97, 'Applied', '2025-03-18', '23I-0017', 7),
(98, 'Applied', '2025-03-19', '23I-0018', 8),
(99, 'Applied', '2025-03-20', '23I-0019', 9),
(100, 'Applied', '2025-03-21', '23I-0020', 10),
(101, 'Interview Scheduled', '2025-03-11', '23I-0001', 2),
(102, 'Applied', '2025-03-12', '23I-0002', 3),
(103, 'Interview Scheduled', '2025-03-13', '23I-0003', 4),
(104, 'Applied', '2025-03-14', '23I-0004', 5),
(105, 'Interview Scheduled', '2025-03-15', '23I-0005', 6),
(106, 'Applied', '2025-03-16', '23I-0006', 7),
(107, 'Interview Scheduled', '2025-03-17', '23I-0007', 8),
(108, 'Applied', '2025-03-18', '23I-0008', 9),
(109, 'Interview Scheduled', '2025-03-19', '23I-0009', 10),
(110, 'Applied', '2025-03-20', '23I-0010', 11),
(111, 'Interview Scheduled', '2025-03-21', '23I-0011', 12),
(112, 'Applied', '2025-03-22', '23I-0012', 13),
(113, 'Interview Scheduled', '2025-03-23', '23I-0013', 14),
(114, 'Applied', '2025-03-24', '23I-0014', 15),
(115, 'Interview Scheduled', '2025-03-25', '23I-0015', 16),
(116, 'Applied', '2025-03-26', '23I-0016', 17),
(117, 'Interview Scheduled', '2025-03-27', '23I-0017', 18),
(118, 'Applied', '2025-03-28', '23I-0018', 19),
(119, 'Interview Scheduled', '2025-03-29', '23I-0019', 20),
(120, 'Applied', '2025-03-30', '23I-0020', 21),
(121, 'Interview Scheduled', '2025-03-31', '23I-0021', 22),
(122, 'Applied', '2025-03-10', '23I-0022', 23),
(123, 'Interview Scheduled', '2025-03-11', '23I-0023', 24),
(124, 'Applied', '2025-03-12', '23I-0024', 25),
(125, 'Interview Scheduled', '2025-03-13', '23I-0025', 26),
(126, 'Applied', '2025-03-14', '23I-0026', 27),
(127, 'Interview Scheduled', '2025-03-15', '23I-0027', 28),
(128, 'Applied', '2025-03-16', '23I-0028', 29),
(129, 'Interview Scheduled', '2025-03-17', '23I-0029', 30),
(130, 'Applied', '2025-03-18', '23I-0030', 1),
(131, 'Interview Scheduled', '2025-03-19', '23I-0031', 2),
(132, 'Applied', '2025-03-20', '23I-0032', 3),
(133, 'Interview Scheduled', '2025-03-21', '23I-0033', 4),
(134, 'Applied', '2025-03-22', '23I-0034', 5),
(135, 'Interview Scheduled', '2025-03-23', '23I-0035', 6),
(136, 'Applied', '2025-03-24', '23I-0036', 7),
(137, 'Interview Scheduled', '2025-03-25', '23I-0037', 8),
(138, 'Applied', '2025-03-26', '23I-0038', 9),
(139, 'Interview Scheduled', '2025-03-27', '23I-0039', 10),
(140, 'Applied', '2025-03-28', '23I-0040', 11),
(141, 'Interview Scheduled', '2025-03-29', '23I-0041', 12),
(142, 'Applied', '2025-03-30', '23I-0042', 13),
(143, 'Interview Scheduled', '2025-03-31', '23I-0043', 14),
(144, 'Applied', '2025-03-10', '23I-0044', 15),
(145, 'Interview Scheduled', '2025-03-11', '23I-0045', 16),
(146, 'Applied', '2025-03-12', '23I-0046', 17),
(147, 'Interview Scheduled', '2025-03-13', '23I-0047', 18),
(148, 'Applied', '2025-03-14', '23I-0048', 19),
(149, 'Interview Scheduled', '2025-03-15', '23I-0049', 20),
(150, 'Applied', '2025-03-16', '23I-0050', 21),
(151, 'Interview Scheduled', '2025-03-17', '23I-0051', 22),
(152, 'Applied', '2025-03-18', '23I-0052', 23),
(153, 'Interview Scheduled', '2025-03-19', '23I-0053', 24),
(154, 'Applied', '2025-03-20', '23I-0054', 25),
(155, 'Interview Scheduled', '2025-03-21', '23I-0055', 26),
(156, 'Applied', '2025-03-22', '23I-0056', 27),
(157, 'Interview Scheduled', '2025-03-23', '23I-0057', 28),
(158, 'Applied', '2025-03-24', '23I-0058', 29),
(159, 'Interview Scheduled', '2025-03-25', '23I-0059', 30),
(160, 'Applied', '2025-03-26', '23I-0060', 1),
(161, 'Interview Scheduled', '2025-03-27', '23I-0061', 2),
(162, 'Applied', '2025-03-28', '23I-0062', 3),
(163, 'Interview Scheduled', '2025-03-29', '23I-0063', 4),
(164, 'Applied', '2025-03-30', '23I-0064', 5),
(165, 'Interview Scheduled', '2025-03-31', '23I-0065', 6),
(166, 'Applied', '2025-03-10', '23I-0066', 7),
(167, 'Interview Scheduled', '2025-03-11', '23I-0067', 8),
(168, 'Applied', '2025-03-12', '23I-0068', 9),
(169, 'Interview Scheduled', '2025-03-13', '23I-0069', 10),
(170, 'Applied', '2025-03-14', '23I-0070', 11),
(171, 'Interview Scheduled', '2025-03-15', '23I-0071', 12),
(172, 'Applied', '2025-03-16', '23I-0072', 13),
(173, 'Interview Scheduled', '2025-03-17', '23I-0073', 14),
(174, 'Applied', '2025-03-18', '23I-0074', 15),
(175, 'Interview Scheduled', '2025-03-19', '23I-0075', 16),
(176, 'Applied', '2025-03-20', '23I-0076', 17),
(177, 'Interview Scheduled', '2025-03-21', '23I-0077', 18),
(178, 'Applied', '2025-03-22', '23I-0078', 19),
(179, 'Interview Scheduled', '2025-03-23', '23I-0079', 20),
(180, 'Applied', '2025-03-24', '23I-0080', 21),
(181, 'Interview Scheduled', '2025-03-25', '23I-0081', 22),
(182, 'Applied', '2025-03-26', '23I-0082', 23),
(183, 'Interview Scheduled', '2025-03-27', '23I-0083', 24),
(184, 'Applied', '2025-03-28', '23I-0084', 25),
(185, 'Interview Scheduled', '2025-03-29', '23I-0085', 26),
(186, 'Applied', '2025-03-30', '23I-0086', 27),
(187, 'Interview Scheduled', '2025-03-31', '23I-0087', 28),
(188, 'Applied', '2025-03-10', '23I-0088', 29),
(189, 'Interview Scheduled', '2025-03-11', '23I-0089', 30),
(190, 'Applied', '2025-03-12', '23I-0090', 1),
(191, 'Interview Scheduled', '2025-03-13', '23I-0081', 2),
(192, 'Applied', '2025-03-14', '23I-0082', 3),
(193, 'Interview Scheduled', '2025-03-15', '23I-0083', 4),
(194, 'Applied', '2025-03-16', '23I-0084', 5),
(195, 'Interview Scheduled', '2025-03-17', '23I-0085', 6),
(196, 'Applied', '2025-03-18', '23I-0086', 7),
(197, 'Interview Scheduled', '2025-03-19', '23I-0087', 8),
(198, 'Applied', '2025-03-20', '23I-0088', 9),
(199, 'Interview Scheduled', '2025-03-21', '23I-0090', 10),
(200, 'Applied', '2025-03-22', '23I-0040', 11),
(201, 'Interview Scheduled', '2025-03-23', '23I-0001', 12),
(202, 'Applied', '2025-03-24', '23I-0002', 13),
(203, 'Interview Scheduled', '2025-03-25', '23I-0003', 14),
(204, 'Applied', '2025-03-26', '23I-0004', 15);

INSERT INTO Interviews (InterviewID, start_time, end_time, Status, Result, ApplicationID, Userid, EventID)
VALUES
(1, '2025-03-01 09:00:00', '2025-03-01 09:30:00', 'Scheduled', 'Pending', 1, 1, 1),
(2, '2025-03-01 09:15:00', '2025-03-01 09:45:00', 'Scheduled', 'Passed', 2, 2, 2),
(3, '2025-03-01 09:30:00', '2025-03-01 10:00:00', 'Scheduled', 'Failed', 3, 3, 3),
(4, '2025-03-01 10:00:00', '2025-03-01 10:30:00', 'Scheduled', 'Pending', 4, 4, 4),
(5, '2025-03-01 10:15:00', '2025-03-01 10:45:00', 'Scheduled', 'Passed', 5, 5, 5),
(6, '2025-03-01 10:30:00', '2025-03-01 11:00:00', 'Scheduled', 'Failed', 6, 6, 6),
(7, '2025-03-01 11:00:00', '2025-03-01 11:30:00', 'Scheduled', 'Pending', 7, 7, 7),
(8, '2025-03-01 11:15:00', '2025-03-01 11:45:00', 'Scheduled', 'Passed', 8, 8, 8),
(9, '2025-03-01 11:30:00', '2025-03-01 12:00:00', 'Scheduled', 'Failed', 9, 9, 9),
(10, '2025-03-01 12:00:00', '2025-03-01 12:30:00', 'Scheduled', 'Pending', 10, 10, 10),
(11, '2025-03-02 09:00:00', '2025-03-02 09:30:00', 'Scheduled', 'Passed', 11, 11, 1),
(12, '2025-03-02 09:30:00', '2025-03-02 10:00:00', 'Scheduled', 'Failed', 12, 12, 2),
(13, '2025-03-02 10:00:00', '2025-03-02 10:30:00', 'Scheduled', 'Pending', 13, 13, 3),
(14, '2025-03-02 10:15:00', '2025-03-02 10:45:00', 'Scheduled', 'Passed', 14, 14, 4),
(15, '2025-03-02 10:45:00', '2025-03-02 11:15:00', 'Scheduled', 'Failed', 15, 15, 5),
(16, '2025-03-02 11:00:00', '2025-03-02 11:30:00', 'Scheduled', 'Pending', 16, 16, 6),
(17, '2025-03-02 11:30:00', '2025-03-02 12:00:00', 'Scheduled', 'Passed', 17, 17, 7),
(18, '2025-03-02 12:00:00', '2025-03-02 12:30:00', 'Scheduled', 'Failed', 18, 18, 8),
(19, '2025-03-02 12:30:00', '2025-03-02 13:00:00', 'Scheduled', 'Pending', 19, 19, 9),
(20, '2025-03-02 13:00:00', '2025-03-02 13:30:00', 'Scheduled', 'Passed', 20, 20, 10),
(21, '2025-03-03 09:00:00', '2025-03-03 09:30:00', 'Scheduled', 'Failed', 21, 1, 1),
(22, '2025-03-03 09:30:00', '2025-03-03 10:00:00', 'Scheduled', 'Pending', 22, 2, 2),
(23, '2025-03-03 10:00:00', '2025-03-03 10:30:00', 'Scheduled', 'Passed', 23, 3, 3),
(24, '2025-03-03 10:15:00', '2025-03-03 10:45:00', 'Scheduled', 'Failed', 24, 4, 4),
(25, '2025-03-03 10:45:00', '2025-03-03 11:15:00', 'Scheduled', 'Pending', 25, 5, 5),
(26, '2025-03-03 11:00:00', '2025-03-03 11:30:00', 'Scheduled', 'Passed', 26, 6, 6),
(27, '2025-03-03 11:30:00', '2025-03-03 12:00:00', 'Scheduled', 'Failed', 27, 7, 7),
(28, '2025-03-03 12:00:00', '2025-03-03 12:30:00', 'Scheduled', 'Pending', 28, 8, 8),
(29, '2025-03-03 12:30:00', '2025-03-03 13:00:00', 'Scheduled', 'Passed', 29, 9, 9),
(30, '2025-03-03 13:00:00', '2025-03-03 13:30:00', 'Scheduled', 'Failed', 30, 10, 10),
(31, '2025-03-04 09:00:00', '2025-03-04 09:30:00', 'Scheduled', 'Pending', 31, 1, 1),
(32, '2025-03-04 09:30:00', '2025-03-04 10:00:00', 'Scheduled', 'Passed', 32, 2, 2),
(33, '2025-03-04 10:00:00', '2025-03-04 10:30:00', 'Scheduled', 'Failed', 33, 3, 3),
(34, '2025-03-04 10:30:00', '2025-03-04 11:00:00', 'Scheduled', 'Pending', 34, 4, 4),
(35, '2025-03-04 11:00:00', '2025-03-04 11:30:00', 'Scheduled', 'Passed', 35, 5, 5),
(36, '2025-03-04 11:30:00', '2025-03-04 12:00:00', 'Scheduled', 'Failed', 36, 6, 6),
(37, '2025-03-04 12:00:00', '2025-03-04 12:30:00', 'Scheduled', 'Pending', 37, 7, 7),
(38, '2025-03-04 12:30:00', '2025-03-04 13:00:00', 'Scheduled', 'Passed', 38, 8, 8),
(39, '2025-03-04 13:00:00', '2025-03-04 13:30:00', 'Scheduled', 'Failed', 39, 9, 9),
(40, '2025-03-04 13:30:00', '2025-03-04 14:00:00', 'Scheduled', 'Pending', 40, 10, 10),
(41, '2025-03-05 09:00:00', '2025-03-05 09:30:00', 'Scheduled', 'Passed', 41, 11, 1),
(42, '2025-03-05 09:30:00', '2025-03-05 10:00:00', 'Scheduled', 'Failed', 42, 12, 2),
(43, '2025-03-05 10:00:00', '2025-03-05 10:30:00', 'Scheduled', 'Pending', 43, 13, 3),
(44, '2025-03-05 10:30:00', '2025-03-05 11:00:00', 'Scheduled', 'Passed', 44, 14, 4),
(45, '2025-03-05 11:00:00', '2025-03-05 11:30:00', 'Scheduled', 'Failed', 45, 15, 5),
(46, '2025-03-05 11:30:00', '2025-03-05 12:00:00', 'Scheduled', 'Pending', 46, 16, 6),
(47, '2025-03-05 12:00:00', '2025-03-05 12:30:00', 'Scheduled', 'Passed', 47, 17, 7),
(48, '2025-03-05 12:30:00', '2025-03-05 13:00:00', 'Scheduled', 'Failed', 48, 18, 8),
(49, '2025-03-05 13:00:00', '2025-03-05 13:30:00', 'Scheduled', 'Pending', 49, 19, 9),
(50, '2025-03-05 13:30:00', '2025-03-05 14:00:00', 'Scheduled', 'Passed', 50, 20, 10),
(51, '2025-03-06 09:00:00', '2025-03-06 09:30:00', 'Scheduled', 'Failed', 51, 11, 1),
(52, '2025-03-06 09:30:00', '2025-03-06 10:00:00', 'Scheduled', 'Pending', 52, 12, 2),
(53, '2025-03-06 10:00:00', '2025-03-06 10:30:00', 'Scheduled', 'Passed', 53, 13, 3),
(54, '2025-03-06 10:30:00', '2025-03-06 11:00:00', 'Scheduled', 'Failed', 54, 14, 4),
(55, '2025-03-06 11:00:00', '2025-03-06 11:30:00', 'Scheduled', 'Pending', 55, 15, 5),
(56, '2025-03-06 11:30:00', '2025-03-06 12:00:00', 'Scheduled', 'Passed', 56, 16, 6),
(57, '2025-03-06 12:00:00', '2025-03-06 12:30:00', 'Scheduled', 'Failed', 57, 17, 7),
(58, '2025-03-06 12:30:00', '2025-03-06 13:00:00', 'Scheduled', 'Pending', 58, 18, 8),
(59, '2025-03-06 13:00:00', '2025-03-06 13:30:00', 'Scheduled', 'Passed', 59, 19, 9),
(60, '2025-03-06 13:30:00', '2025-03-06 14:00:00', 'Scheduled', 'Failed', 60, 20, 10),
(61, '2025-03-07 09:00:00', '2025-03-07 09:30:00', 'Scheduled', 'Pending', 61, 1, 1),
(62, '2025-03-07 09:30:00', '2025-03-07 10:00:00', 'Scheduled', 'Passed', 62, 2, 2),
(63, '2025-03-07 10:00:00', '2025-03-07 10:30:00', 'Scheduled', 'Failed', 63, 3, 3),
(64, '2025-03-07 10:30:00', '2025-03-07 11:00:00', 'Scheduled', 'Pending', 64, 4, 4),
(65, '2025-03-07 11:00:00', '2025-03-07 11:30:00', 'Scheduled', 'Passed', 65, 5, 5),
(66, '2025-03-07 11:30:00', '2025-03-07 12:00:00', 'Scheduled', 'Failed', 66, 6, 6),
(67, '2025-03-07 12:00:00', '2025-03-07 12:30:00', 'Scheduled', 'Pending', 67, 7, 7),
(68, '2025-03-07 12:30:00', '2025-03-07 13:00:00', 'Scheduled', 'Passed', 68, 8, 8),
(69, '2025-03-07 13:00:00', '2025-03-07 13:30:00', 'Scheduled', 'Failed', 69, 9, 9),
(70, '2025-03-07 13:30:00', '2025-03-07 14:00:00', 'Scheduled', 'Pending', 70, 10, 10),
(71, '2025-03-08 09:00:00', '2025-03-08 09:30:00', 'Scheduled', 'Passed', 71, 11, 1),
(72, '2025-03-08 09:30:00', '2025-03-08 10:00:00', 'Scheduled', 'Failed', 72, 12, 2),
(73, '2025-03-08 10:00:00', '2025-03-08 10:30:00', 'Scheduled', 'Pending', 73, 13, 3),
(74, '2025-03-08 10:30:00', '2025-03-08 11:00:00', 'Scheduled', 'Passed', 74, 14, 4),
(75, '2025-03-08 11:00:00', '2025-03-08 11:30:00', 'Scheduled', 'Failed', 75, 15, 5),
(76, '2025-03-08 11:30:00', '2025-03-08 12:00:00', 'Scheduled', 'Pending', 76, 16, 6),
(77, '2025-03-08 12:00:00', '2025-03-08 12:30:00', 'Scheduled', 'Passed', 77, 17, 7),
(78, '2025-03-08 12:30:00', '2025-03-08 13:00:00', 'Scheduled', 'Failed', 78, 18, 8),
(79, '2025-03-08 13:00:00', '2025-03-08 13:30:00', 'Scheduled', 'Pending', 79, 19, 9),
(80, '2025-03-08 13:30:00', '2025-03-08 14:00:00', 'Scheduled', 'Passed', 80, 20, 10),
(81, '2025-02-20 09:00:00', '2025-02-20 09:30:00', 'Scheduled', 'Failed', 81, 1, 1),
(82, '2025-02-20 09:30:00', '2025-02-20 10:00:00', 'Scheduled', 'Pending', 82, 2, 2),
(83, '2025-02-20 10:00:00', '2025-02-20 10:30:00', 'Scheduled', 'Passed', 83, 3, 3),
(84, '2025-02-20 10:30:00', '2025-02-20 11:00:00', 'Scheduled', 'Failed', 84, 4, 4),
(85, '2025-02-20 11:00:00', '2025-02-20 11:30:00', 'Scheduled', 'Pending', 85, 5, 5),
(86, '2025-02-20 11:30:00', '2025-02-20 12:00:00', 'Scheduled', 'Passed', 86, 6, 6),
(87, '2025-02-20 12:00:00', '2025-02-20 12:30:00', 'Scheduled', 'Failed', 87, 7, 7),
(88, '2025-02-20 12:30:00', '2025-02-20 13:00:00', 'Scheduled', 'Pending', 88, 8, 8),
(89, '2025-02-20 13:00:00', '2025-02-20 13:30:00', 'Scheduled', 'Passed', 89, 9, 9),
(90, '2025-02-20 13:30:00', '2025-02-20 14:00:00', 'Scheduled', 'Failed', 90, 10, 10),
(91, '2025-02-21 09:00:00', '2025-02-21 09:30:00', 'Scheduled', 'Pending', 91, 1, 1),
(92, '2025-02-21 09:30:00', '2025-02-21 10:00:00', 'Scheduled', 'Passed', 92, 2, 2),
(93, '2025-02-21 10:00:00', '2025-02-21 10:30:00', 'Scheduled', 'Failed', 93, 3, 3),
(94, '2025-02-21 10:30:00', '2025-02-21 11:00:00', 'Scheduled', 'Pending', 94, 4, 4),
(95, '2025-02-21 11:00:00', '2025-02-21 11:30:00', 'Scheduled', 'Passed', 95, 5, 5),
(96, '2025-02-21 11:30:00', '2025-02-21 12:00:00', 'Scheduled', 'Failed', 96, 6, 6),
(97, '2025-02-21 12:00:00', '2025-02-21 12:30:00', 'Scheduled', 'Pending', 97, 7, 7),
(98, '2025-02-21 12:30:00', '2025-02-21 13:00:00', 'Scheduled', 'Passed', 98, 8, 8),
(99, '2025-02-21 13:00:00', '2025-02-21 13:30:00', 'Scheduled', 'Failed', 99, 9, 9),
(100, '2025-02-21 13:30:00', '2025-02-21 14:00:00', 'Scheduled', 'Pending', 100, 10, 10),
(101, '2025-02-22 09:00:00', '2025-02-22 09:30:00', 'Scheduled', 'Passed', 101, 11, 1),
(102, '2025-02-22 09:30:00', '2025-02-22 10:00:00', 'Scheduled', 'Failed', 102, 12, 2),
(103, '2025-02-22 10:00:00', '2025-02-22 10:30:00', 'Scheduled', 'Pending', 103, 13, 3),
(104, '2025-02-22 10:30:00', '2025-02-22 11:00:00', 'Scheduled', 'Passed', 104, 14, 4),
(105, '2025-02-22 11:00:00', '2025-02-22 11:30:00', 'Scheduled', 'Failed', 105, 15, 5),
(106, '2025-02-22 11:30:00', '2025-02-22 12:00:00', 'Scheduled', 'Pending', 106, 16, 6),
(107, '2025-02-22 12:00:00', '2025-02-22 12:30:00', 'Scheduled', 'Passed', 107, 17, 7),
(108, '2025-02-22 12:30:00', '2025-02-22 13:00:00', 'Scheduled', 'Failed', 108, 18, 8),
(109, '2025-02-22 13:00:00', '2025-02-22 13:30:00', 'Scheduled', 'Pending', 109, 19, 9),
(110, '2025-02-22 13:30:00', '2025-02-22 14:00:00', 'Scheduled', 'Passed', 110, 20, 10),
(111, '2025-02-23 09:00:00', '2025-02-23 09:30:00', 'Scheduled', 'Failed', 111, 11, 1),
(112, '2025-02-23 09:30:00', '2025-02-23 10:00:00', 'Scheduled', 'Pending', 112, 12, 2),
(113, '2025-02-23 10:00:00', '2025-02-23 10:30:00', 'Scheduled', 'Passed', 113, 13, 3),
(114, '2025-02-23 10:30:00', '2025-02-23 11:00:00', 'Scheduled', 'Failed', 114, 14, 4),
(115, '2025-02-23 11:00:00', '2025-02-23 11:30:00', 'Scheduled', 'Pending', 115, 15, 5),
(116, '2025-02-23 11:30:00', '2025-02-23 12:00:00', 'Scheduled', 'Passed', 116, 16, 6),
(117, '2025-02-23 12:00:00', '2025-02-23 12:30:00', 'Scheduled', 'Failed', 117, 17, 7),
(118, '2025-02-23 12:30:00', '2025-02-23 13:00:00', 'Scheduled', 'Pending', 118, 18, 8),
(119, '2025-02-23 13:00:00', '2025-02-23 13:30:00', 'Scheduled', 'Passed', 119, 19, 9),
(120, '2025-02-23 13:30:00', '2025-02-23 14:00:00', 'Scheduled', 'Failed', 120, 20, 10),
(121, '2025-01-10 09:00:00', '2025-01-10 09:30:00', 'Scheduled', 'Pending', 121, 1, 1),
(122, '2025-01-10 09:35:00', '2025-01-10 10:05:00', 'Scheduled', 'Passed', 122, 2, 2),
(123, '2025-01-10 10:10:00', '2025-01-10 10:40:00', 'Scheduled', 'Failed', 123, 3, 3),
(124, '2025-01-10 10:45:00', '2025-01-10 11:15:00', 'Scheduled', 'Pending', 124, 4, 4),
(125, '2025-01-10 11:20:00', '2025-01-10 11:50:00', 'Scheduled', 'Passed', 125, 5, 5),
(126, '2025-01-10 11:55:00', '2025-01-10 12:25:00', 'Scheduled', 'Failed', 126, 6, 6),
(127, '2025-01-10 12:30:00', '2025-01-10 13:00:00', 'Scheduled', 'Pending', 127, 7, 7),
(128, '2025-01-10 13:05:00', '2025-01-10 13:35:00', 'Scheduled', 'Passed', 128, 8, 8),
(129, '2025-01-10 13:40:00', '2025-01-10 14:10:00', 'Scheduled', 'Failed', 129, 9, 9),
(130, '2025-01-10 14:15:00', '2025-01-10 14:45:00', 'Scheduled', 'Pending', 130, 10, 10),
(131, '2025-01-11 09:00:00', '2025-01-11 09:30:00', 'Scheduled', 'Passed', 131, 11, 1),
(132, '2025-01-11 09:35:00', '2025-01-11 10:05:00', 'Scheduled', 'Failed', 132, 12, 2),
(133, '2025-01-11 10:10:00', '2025-01-11 10:40:00', 'Scheduled', 'Pending', 133, 13, 3),
(134, '2025-01-11 10:45:00', '2025-01-11 11:15:00', 'Scheduled', 'Passed', 134, 14, 4),
(135, '2025-01-11 11:20:00', '2025-01-11 11:50:00', 'Scheduled', 'Failed', 135, 15, 5),
(136, '2025-01-11 11:55:00', '2025-01-11 12:25:00', 'Scheduled', 'Pending', 136, 16, 6),
(137, '2025-01-11 12:30:00', '2025-01-11 13:00:00', 'Scheduled', 'Passed', 137, 17, 7),
(138, '2025-01-11 13:05:00', '2025-01-11 13:35:00', 'Scheduled', 'Failed', 138, 18, 8),
(139, '2025-01-11 13:40:00', '2025-01-11 14:10:00', 'Scheduled', 'Pending', 139, 19, 9),
(140, '2025-01-11 14:15:00', '2025-01-11 14:45:00', 'Scheduled', 'Passed', 140, 20, 10),
(141, '2025-01-12 09:00:00', '2025-01-12 09:30:00', 'Scheduled', 'Failed', 141, 11, 1),
(142, '2025-01-12 09:35:00', '2025-01-12 10:05:00', 'Scheduled', 'Pending', 142, 12, 2),
(143, '2025-01-12 10:10:00', '2025-01-12 10:40:00', 'Scheduled', 'Passed', 143, 13, 3),
(144, '2025-01-12 10:45:00', '2025-01-12 11:15:00', 'Scheduled', 'Failed', 144, 14, 4),
(145, '2025-01-12 11:20:00', '2025-01-12 11:50:00', 'Scheduled', 'Pending', 145, 15, 5),
(146, '2025-01-12 11:55:00', '2025-01-12 12:25:00', 'Scheduled', 'Passed', 146, 16, 6),
(147, '2025-01-12 12:30:00', '2025-01-12 13:00:00', 'Scheduled', 'Failed', 147, 17, 7),
(148, '2025-01-12 13:05:00', '2025-01-12 13:35:00', 'Scheduled', 'Pending', 148, 18, 8),
(149, '2025-01-12 13:40:00', '2025-01-12 14:10:00', 'Scheduled', 'Passed', 149, 19, 9),
(150, '2025-01-12 14:15:00', '2025-01-12 14:45:00', 'Scheduled', 'Failed', 150, 20, 10),
(151, '2025-01-13 09:00:00', '2025-01-13 09:30:00', 'Scheduled', 'Pending', 151, 1, 1),
(152, '2025-01-13 09:35:00', '2025-01-13 10:05:00', 'Scheduled', 'Passed', 152, 2, 2),
(153, '2025-01-13 10:10:00', '2025-01-13 10:40:00', 'Scheduled', 'Failed', 153, 3, 3),
(154, '2025-01-13 10:45:00', '2025-01-13 11:15:00', 'Scheduled', 'Pending', 154, 4, 4),
(155, '2025-01-13 11:20:00', '2025-01-13 11:50:00', 'Scheduled', 'Passed', 155, 5, 5),
(156, '2025-01-13 11:55:00', '2025-01-13 12:25:00', 'Scheduled', 'Failed', 156, 6, 6),
(157, '2025-01-13 12:30:00', '2025-01-13 13:00:00', 'Scheduled', 'Pending', 157, 7, 7),
(158, '2025-01-13 13:05:00', '2025-01-13 13:35:00', 'Scheduled', 'Passed', 158, 8, 8),
(159, '2025-01-13 13:40:00', '2025-01-13 14:10:00', 'Scheduled', 'Failed', 159, 9, 9),
(160, '2025-01-13 14:15:00', '2025-01-13 14:45:00', 'Scheduled', 'Pending', 160, 10, 10),
(161, '2025-02-01 09:00:00', '2025-02-01 09:30:00', 'Scheduled', 'Pending', 161, 1, 1),
(162, '2025-02-01 09:35:00', '2025-02-01 10:05:00', 'Scheduled', 'Passed', 162, 2, 2),
(163, '2025-02-01 10:10:00', '2025-02-01 10:40:00', 'Scheduled', 'Failed', 163, 3, 3),
(164, '2025-02-01 10:45:00', '2025-02-01 11:15:00', 'Scheduled', 'Pending', 164, 4, 4),
(165, '2025-02-01 11:20:00', '2025-02-01 11:50:00', 'Scheduled', 'Passed', 165, 5, 5),
(166, '2025-02-01 11:55:00', '2025-02-01 12:25:00', 'Scheduled', 'Failed', 166, 6, 6),
(167, '2025-02-01 12:30:00', '2025-02-01 13:00:00', 'Scheduled', 'Pending', 167, 7, 7),
(168, '2025-02-01 13:05:00', '2025-02-01 13:35:00', 'Scheduled', 'Passed', 168, 8, 8),
(169, '2025-02-01 13:40:00', '2025-02-01 14:10:00', 'Scheduled', 'Failed', 169, 9, 9),
(170, '2025-02-01 14:15:00', '2025-02-01 14:45:00', 'Scheduled', 'Pending', 170, 10, 10),
(171, '2025-02-02 09:00:00', '2025-02-02 09:30:00', 'Scheduled', 'Passed', 171, 11, 1),
(172, '2025-02-02 09:35:00', '2025-02-02 10:05:00', 'Scheduled', 'Failed', 172, 12, 2),
(173, '2025-02-02 10:10:00', '2025-02-02 10:40:00', 'Scheduled', 'Pending', 173, 13, 3),
(174, '2025-02-02 10:45:00', '2025-02-02 11:15:00', 'Scheduled', 'Passed', 174, 14, 4),
(175, '2025-02-02 11:20:00', '2025-02-02 11:50:00', 'Scheduled', 'Failed', 175, 15, 5),
(176, '2025-02-02 11:55:00', '2025-02-02 12:25:00', 'Scheduled', 'Pending', 176, 16, 6),
(177, '2025-02-02 12:30:00', '2025-02-02 13:00:00', 'Scheduled', 'Passed', 177, 17, 7),
(178, '2025-02-02 13:05:00', '2025-02-02 13:35:00', 'Scheduled', 'Failed', 178, 18, 8),
(179, '2025-02-02 13:40:00', '2025-02-02 14:10:00', 'Scheduled', 'Pending', 179, 19, 9),
(180, '2025-02-02 14:15:00', '2025-02-02 14:45:00', 'Scheduled', 'Passed', 180, 20, 10),
(181, '2025-02-03 09:00:00', '2025-02-03 09:30:00', 'Scheduled', 'Failed', 181, 11, 1),
(182, '2025-02-03 09:35:00', '2025-02-03 10:05:00', 'Scheduled', 'Pending', 182, 12, 2),
(183, '2025-02-03 10:10:00', '2025-02-03 10:40:00', 'Scheduled', 'Passed', 183, 13, 3),
(184, '2025-02-03 10:45:00', '2025-02-03 11:15:00', 'Scheduled', 'Failed', 184, 14, 4),
(185, '2025-02-03 11:20:00', '2025-02-03 11:50:00', 'Scheduled', 'Pending', 185, 15, 5),
(186, '2025-02-03 11:55:00', '2025-02-03 12:25:00', 'Scheduled', 'Passed', 186, 16, 6),
(187, '2025-02-03 12:30:00', '2025-02-03 13:00:00', 'Scheduled', 'Failed', 187, 17, 7),
(188, '2025-02-03 13:05:00', '2025-02-03 13:35:00', 'Scheduled', 'Pending', 188, 18, 8),
(189, '2025-02-03 13:40:00', '2025-02-03 14:10:00', 'Scheduled', 'Passed', 189, 19, 9),
(190, '2025-02-03 14:15:00', '2025-02-03 14:45:00', 'Scheduled', 'Failed', 190, 20, 10),
(191, '2025-02-04 09:00:00', '2025-02-04 09:30:00', 'Scheduled', 'Pending', 191, 1, 1),
(192, '2025-02-04 09:35:00', '2025-02-04 10:05:00', 'Scheduled', 'Passed', 192, 2, 2),
(193, '2025-02-04 10:10:00', '2025-02-04 10:40:00', 'Scheduled', 'Failed', 193, 3, 3),
(194, '2025-02-04 10:45:00', '2025-02-04 11:15:00', 'Scheduled', 'Pending', 194, 4, 4),
(195, '2025-02-04 11:20:00', '2025-02-04 11:50:00', 'Scheduled', 'Passed', 195, 5, 5),
(196, '2025-02-04 11:55:00', '2025-02-04 12:25:00', 'Scheduled', 'Failed', 196, 6, 6),
(197, '2025-02-04 12:30:00', '2025-02-04 13:00:00', 'Scheduled', 'Pending', 197, 7, 7),
(198, '2025-02-04 13:05:00', '2025-02-04 13:35:00', 'Scheduled', 'Passed', 198, 8, 8),
(199, '2025-02-04 13:40:00', '2025-02-04 14:10:00', 'Scheduled', 'Failed', 199, 9, 9),
(200, '2025-02-04 14:15:00', '2025-02-04 14:45:00', 'Scheduled', 'Pending', 200, 10, 10);


INSERT INTO Booth (BoothID, check_intime, UserId, CompanyID, EventId, location)
VALUES
(1, '2025-04-01 09:00:00', 1, 1, 1, 'C Block - Floor 1'),
(2, '2025-04-01 09:15:00', 2, 2, 2, 'B Block - Floor 1'),
(3, '2025-04-01 09:30:00', 3, 3, 3, 'A Block - Floor 1'),
(4, '2025-04-01 09:45:00', 4, 4, 4, 'C Block - Floor 2'),
(5, '2025-04-01 10:00:00', 5, 5, 5, 'C Block - Floor 3'),
(6, '2025-04-01 10:15:00', 6, 6, 6, 'A Block - Floor 2'),
(7, '2025-04-01 10:30:00', 7, 7, 7, 'B Block - Floor 2'),
(8, '2025-04-01 10:45:00', 8, 8, 8, 'C Block - Floor 4'),
(9, '2025-04-01 11:00:00', 9, 9, 9, 'C Block - Floor 5'),
(10, '2025-04-01 11:15:00', 10, 10, 10, 'A Block - Floor 3'),
(11, '2025-04-01 11:30:00', 11, 11, 1, 'C Block - Floor 6'),
(12, '2025-04-01 11:45:00', 12, 12, 2, 'C Block - Floor 1'),
(13, '2025-04-01 12:00:00', 13, 13, 3, 'B Block - Floor 1'),
(14, '2025-04-01 12:15:00', 14, 14, 4, 'A Block - Floor 1'),
(15, '2025-04-01 12:30:00', 15, 15, 5, 'C Block - Floor 2'),
(16, '2025-04-01 12:45:00', 1, 16, 6, 'C Block - Floor 3'),
(17, '2025-04-01 13:00:00', 2, 17, 7, 'A Block - Floor 2'),
(18, '2025-04-01 13:15:00', 3, 18, 8, 'B Block - Floor 2'),
(19, '2025-04-01 13:30:00', 4, 19, 9, 'C Block - Floor 4'),
(20, '2025-04-01 13:45:00', 5, 20, 10, 'C Block - Floor 5'),
(21, '2025-04-01 14:00:00', 6, 21, 1, 'A Block - Floor 3'),
(22, '2025-04-01 14:15:00', 7, 22, 2, 'C Block - Floor 6'),
(23, '2025-04-01 14:30:00', 8, 23, 3, 'C Block - Floor 1'),
(24, '2025-04-01 14:45:00', 9, 24, 4, 'B Block - Floor 1'),
(25, '2025-04-01 15:00:00', 10, 25, 5, 'A Block - Floor 1'),
(26, '2025-04-01 15:15:00', 11, 26, 6, 'C Block - Floor 2'),
(27, '2025-04-01 15:30:00', 12, 27, 7, 'C Block - Floor 3'),
(28, '2025-04-01 15:45:00', 13, 28, 8, 'A Block - Floor 2'),
(29, '2025-04-01 16:00:00', 14, 29, 9, 'B Block - Floor 2'),
(30, '2025-04-01 16:15:00', 15, 30, 10, 'C Block - Floor 4'),
(31, '2025-04-01 16:30:00', 1, 1, 1, 'C Block - Floor 5'),
(32, '2025-04-01 16:45:00', 2, 2, 2, 'A Block - Floor 3'),
(33, '2025-04-01 17:00:00', 3, 3, 3, 'C Block - Floor 6'),
(34, '2025-04-01 17:15:00', 4, 4, 4, 'C Block - Floor 1'),
(35, '2025-04-01 17:30:00', 5, 5, 5, 'B Block - Floor 1'),
(36, '2025-04-01 17:45:00', 6, 6, 6, 'A Block - Floor 1'),
(37, '2025-04-01 18:00:00', 7, 7, 7, 'C Block - Floor 2'),
(38, '2025-04-01 18:15:00', 8, 8, 8, 'C Block - Floor 3'),
(39, '2025-04-01 18:30:00', 9, 9, 9, 'A Block - Floor 2'),
(40, '2025-04-01 18:45:00', 10, 10, 10, 'B Block - Floor 2');

INSERT INTO Reviews (ReviewID, Comments, Ratings, Userid, Recruiterid)
VALUES
(1, 'Great experience, very helpful!', 5, '23i-0001', 1),
(2, 'The interview was fair and organized.', 4, '23i-0002', 2),
(3, 'Not satisfied with the process.', 2, '23i-0003', 3),
(4, 'Recruiter was friendly and professional.', 5, '23i-0004', 4),
(5, 'Could have been better prepared.', 3, '23i-0005', 5),
(6, 'Very informative discussion.', 4, '23i-0006', 6),
(7, 'I had to wait too long.', 2, '23i-0007', 7),
(8, 'Excellent guidance provided.', 5, '23i-0008', 8),
(9, 'The recruiter rushed the interview.', 2, '23i-0009', 9),
(10, 'Smooth and efficient process.', 4, '23i-0010', 10),
(11, 'It was okay, not great.', 3, '23i-0011', 11),
(12, 'Polite and clear communication.', 5, '23i-0012', 12),
(13, 'Did not receive timely feedback.', 2, '23i-0013', 13),
(14, 'Recruiter gave good insights.', 4, '23i-0014', 14),
(15, 'Decent experience overall.', 3, '23i-0015', 15),
(16, 'They were genuinely interested.', 5, '23i-0016', 16),
(17, 'Expected more questions.', 3, '23i-0017', 17),
(18, 'Good structure and format.', 4, '23i-0018', 18),
(19, 'Felt a bit disorganized.', 2, '23i-0019', 19),
(20, 'Helpful suggestions provided.', 5, '23i-0020', 20),
(21, 'The panel was professional.', 4, '23i-0021', 1),
(22, 'Poor communication from recruiter.', 2, '23i-0022', 2),
(23, 'Very engaging session.', 5, '23i-0023', 3),
(24, 'Unclear instructions given.', 2, '23i-0024', 4),
(25, 'Happy with the process.', 4, '23i-0025', 5),
(26, 'It felt rushed.', 2, '23i-0026', 6),
(27, 'Helpful and respectful.', 5, '23i-0027', 7),
(28, 'Good overall feedback.', 4, '23i-0028', 8),
(29, 'Could have been more transparent.', 3, '23i-0029', 9),
(30, 'Clear and concise.', 5, '23i-0030', 10),
(31, 'Too many delays.', 2, '23i-0031', 11),
(32, 'I liked the approach.', 4, '23i-0032', 12),
(33, 'Not enough time given.', 3, '23i-0033', 13),
(34, 'Motivating interaction.', 5, '23i-0034', 14),
(35, 'I didn’t feel heard.', 2, '23i-0035', 15),
(36, 'They explained everything well.', 4, '23i-0036', 16),
(37, 'Quite an average experience.', 3, '23i-0037', 17),
(38, 'Good tips and advice.', 5, '23i-0038', 18),
(39, 'Interview felt impersonal.', 2, '23i-0039', 19),
(40, 'Very professional behavior.', 4, '23i-0040', 20),
(41, 'Had a positive impression.', 5, '23i-0041', 1),
(42, 'The process was unclear.', 2, '23i-0042', 2),
(43, 'Recruiter was helpful and polite.', 5, '23i-0043', 3),
(44, 'Average experience.', 3, '23i-0044', 4),
(45, 'Loved the conversation.', 5, '23i-0045', 5),
(46, 'Too short to assess.', 2, '23i-0046', 6),
(47, 'Gave valuable career tips.', 4, '23i-0047', 7),
(48, 'Felt a bit awkward.', 3, '23i-0048', 8),
(49, 'The questions were challenging and good.', 5, '23i-0049', 9),
(50, 'Unprepared and late.', 1, '23i-0050', 10),
(51, 'Engaged and thorough.', 5, '23i-0051', 11),
(52, 'Just okay.', 3, '23i-0052', 12),
(53, 'Could have answered more questions.', 2, '23i-0053', 13),
(54, 'Very encouraging tone.', 5, '23i-0054', 14),
(55, 'Interview lacked direction.', 2, '23i-0055', 15),
(56, 'Was impressed with the knowledge.', 5, '23i-0056', 16),
(57, 'Neutral feelings.', 3, '23i-0057', 17),
(58, 'Loved the environment.', 4, '23i-0058', 18),
(59, 'Wasted my time.', 1, '23i-0059', 19),
(60, 'Fantastic recruiter.', 5, '23i-0060', 20),
(61, 'I felt nervous but they helped.', 4, '23i-0061', 1),
(62, 'Very robotic process.', 2, '23i-0062', 2),
(63, 'Great follow-up after interview.', 5, '23i-0063', 3),
(64, 'Expected better support.', 3, '23i-0064', 4),
(65, 'Excellent and friendly.', 5, '23i-0065', 5),
(66, 'Interview was disappointing.', 2, '23i-0066', 6),
(67, 'They really listened.', 4, '23i-0067', 7),
(68, 'Standard process.', 3, '23i-0068', 8),
(69, 'Highly recommend them.', 5, '23i-0069', 9),
(70, 'Unprepared panel.', 1, '23i-0070', 10),
(71, 'Comfortable and smooth.', 5, '23i-0071', 11),
(72, 'Not worth the wait.', 2, '23i-0072', 12),
(73, 'Asked good questions.', 4, '23i-0073', 13),
(74, 'Very basic interview.', 3, '23i-0074', 14),
(75, 'Impressive clarity.', 5, '23i-0075', 15),
(76, 'They didn’t seem interested.', 2, '23i-0076', 16),
(77, 'Helpful throughout.', 4, '23i-0077', 17),
(78, 'I learned a lot.', 5, '23i-0078', 18),
(79, 'Felt disrespected.', 1, '23i-0079', 19),
(80, 'Polite and courteous.', 5, '23i-0080', 20),
(81, 'Quick and easy process.', 4, '23i-0081', 1),
(82, 'Wasn’t professional.', 2, '23i-0082', 2),
(83, 'Nice and chill vibe.', 4, '23i-0083', 3),
(84, 'Nothing special.', 3, '23i-0084', 4),
(85, 'Great prep questions.', 5, '23i-0085', 5),
(86, 'Did not explain roles well.', 2, '23i-0086', 6),
(87, 'Great conversation flow.', 4, '23i-0087', 7),
(88, 'Felt like a formality.', 3, '23i-0088', 8),
(89, 'Nice feedback.', 5, '23i-0089', 9),
(90, 'Felt ignored.', 1, '23i-0090', 10);

INSERT INTO Visits (Userid, booth_id)
VALUES
('23i-0001', 1),
('23i-0002', 2),
('23i-0003', 3),
('23i-0004', 4),
('23i-0005', 5),
('23i-0006', 6),
('23i-0007', 7),
('23i-0008', 8),
('23i-0009', 9),
('23i-0010', 10),
('23i-0011', 11),
('23i-0012', 12),
('23i-0013', 13),
('23i-0014', 14),
('23i-0015', 15),
('23i-0016', 16),
('23i-0017', 17),
('23i-0018', 18),
('23i-0019', 19),
('23i-0020', 20),
('23i-0021', 21),
('23i-0022', 22),
('23i-0023', 23),
('23i-0024', 24),
('23i-0025', 25),
('23i-0026', 26),
('23i-0027', 27),
('23i-0028', 28),
('23i-0029', 29),
('23i-0030', 30),
('23i-0031', 31),
('23i-0032', 32),
('23i-0033', 33),
('23i-0034', 34),
('23i-0035', 35),
('23i-0036', 36),
('23i-0037', 37),
('23i-0038', 38),
('23i-0039', 39),
('23i-0040', 40),
('23i-0041', 1),
('23i-0042', 2),
('23i-0043', 3),
('23i-0044', 4),
('23i-0045', 5),
('23i-0046', 6),
('23i-0047', 7),
('23i-0048', 8),
('23i-0049', 9),
('23i-0050', 10),
('23i-0051', 11),
('23i-0052', 12),
('23i-0053', 13),
('23i-0054', 14),
('23i-0055', 15),
('23i-0056', 16),
('23i-0057', 17),
('23i-0058', 18),
('23i-0059', 19),
('23i-0060', 20),
('23i-0061', 21),
('23i-0062', 22),
('23i-0063', 23),
('23i-0064', 24),
('23i-0065', 25),
('23i-0066', 26),
('23i-0067', 27),
('23i-0068', 28),
('23i-0069', 29),
('23i-0070', 30),
('23i-0071', 31),
('23i-0072', 32),
('23i-0073', 33),
('23i-0074', 34),
('23i-0075', 35),
('23i-0076', 36),
('23i-0077', 37),
('23i-0078', 38),
('23i-0079', 39),
('23i-0080', 40),
('23i-0081', 1),
('23i-0082', 2),
('23i-0083', 3),
('23i-0084', 4),
('23i-0085', 5),
('23i-0086', 6),
('23i-0087', 7),
('23i-0088', 8),
('23i-0089', 9),
('23i-0090', 10),
('23i-0001', 20),
('23i-0002', 21),
('23i-0003', 22),
('23i-0004', 23),
('23i-0005', 24),
('23i-0006', 25),
('23i-0007', 26),
('23i-0008', 27),
('23i-0009', 28),
('23i-0010', 29);

INSERT INTO Monitors (booth_id, Userid)
VALUES
(1, 1), 
(2, 2), 
(3, 3), 
(4, 4), 
(5, 5),
(6, 6), 
(7, 7), 
(8, 8), 
(9, 9), 
(10, 10),
(11, 11), 
(12, 12), 
(13, 13), 
(14, 14), 
(15, 15),
(16, 16), 
(17, 17), 
(18, 18), 
(19, 19), 
(20, 20),
(21, 21), 
(22, 22), 
(23, 23), 
(24, 24), 
(25, 25),
(26, 26), 
(27, 27), 
(28, 28), 
(29, 29), 
(30, 30),
(31, 31), 
(32, 32), 
(33, 33), 
(34, 34), 
(35, 35),
(36, 36), 
(37, 37), 
(38, 38), 
(39, 39), 
(40, 40),
(1, 41), 
(2, 42), 
(3, 43), 
(4, 44), 
(5, 45),
(6, 46), 
(7, 47), 
(8, 48), 
(9, 49), 
(10, 50),
(11, 1), 
(12, 2), 
(13, 3), 
(14, 4), 
(15, 5),
(16, 6), 
(17, 7), 
(18, 8), 
(19, 9), 
(20, 10),
(21, 11), 
(22, 12), 
(23, 13), 
(24, 14), 
(25, 15),
(26, 16), 
(27, 17), 
(28, 18), 
(29, 19), 
(30, 20),
(31, 21), 
(32, 22), 
(33, 23), 
(34, 24), 
(35, 25),
(36, 26), 
(37, 27), 
(38, 28), 
(39, 29), 
(40, 30);

select*from [User]
select*from Student
select*from Recruiter
select*from Reviews
select*from TPO
select*from Booth_Coordinator
select*from Student_Certificates
select*from StudentSkills
select*from Interviews
select*from Applications

select*from Jobpostings_RequiredSkills
select*from Jobpostings_Type
select*from Visits
select*from Monitors
select*from Companies
select*from JobFairEvents


select*from Jobpostings
select*from Booth

UPDATE Booth 
                SET VisitorCount = 
                    CASE 
                        WHEN VisitorCount IS NULL THEN 1 
                        ELSE VisitorCount + 1 
                    END
                WHERE BoothID = 1

	CREATE TABLE CompanyInterviewSummary (
    CompanyID INT ,
    CompanyName VARCHAR(150) NOT NULL,
    TotalInterviews INT NOT NULL CHECK (TotalInterviews >= 0),
    FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID)
);


INSERT INTO CompanyInterviewSummary (CompanyID, CompanyName, TotalInterviews)
SELECT 
    c.CompanyID,
    c.Name,
    COUNT(i.InterviewID) AS TotalInterviews
FROM 
    Interviews i
JOIN 
    Applications a ON i.ApplicationID = a.ApplicationID
JOIN 
    JobPostings jp ON a.JobID = jp.JobID
JOIN 
    Companies c ON jp.CompanyID = c.CompanyID
GROUP BY 
    c.CompanyID, c.Name;

	CREATE TABLE CompanyOfferApplicationRatios (
    CompanyID INT ,
    CompanyName VARCHAR(150) NOT NULL,
    TotalApplications INT NOT NULL CHECK (TotalApplications >= 0),
    OffersMade INT NOT NULL CHECK (OffersMade >= 0),
    OfferToApplicationRatioPercent FLOAT NOT NULL CHECK (OfferToApplicationRatioPercent >= 0),
    FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID)
);

select*from CompanyOfferApplicationRatios

INSERT INTO CompanyOfferApplicationRatios (
    CompanyID,
    CompanyName,
    TotalApplications,
    OffersMade,
    OfferToApplicationRatioPercent
)
	SELECT 
    c.CompanyID,
    c.Name AS CompanyName,
    COUNT(DISTINCT a.ApplicationID) AS TotalApplications,
    SUM(CASE WHEN i.Result = 'Passed' THEN 1 ELSE 0 END) AS OffersMade,
    ROUND(
        CAST(SUM(CASE WHEN i.Result = 'Passed' THEN 1 ELSE 0 END) AS FLOAT) 
        / NULLIF(COUNT(DISTINCT a.ApplicationID), 0) * 100, 2
    ) AS OfferToApplicationRatioPercent
FROM 
    Applications a
JOIN 
    JobPostings jp ON a.JobID = jp.JobID
JOIN 
    Companies c ON jp.CompanyID = c.CompanyID
LEFT JOIN 
    Interviews i ON a.ApplicationID = i.ApplicationID
GROUP BY 
    c.CompanyID, c.Name
ORDER BY 
    OfferToApplicationRatioPercent DESC;

	CREATE TABLE RecruiterRating (
    RecruiterId INT NOT NULL,
	RecruiterName varchar(100) ,
    RatingValue INT CHECK (RatingValue BETWEEN 1 AND 5),
    Comment NVARCHAR(500),
	StudentId NVARCHAR(10) CHECK (StudentId LIKE '[0-9][0-9]i-[0-9][0-9][0-9][0-9]'),
);

INSERT INTO RecruiterRating (RecruiterId, RecruiterName, RatingValue, Comment, StudentId)
SELECT 
    DISTINCT 
    r.RecruiterId,
    u.FirstName + ' '+ u.LastName AS RecruiterName,
    rr.Ratings AS RatingValue,
    rr.Comments AS Comment,
    s.StudentId 
FROM 
    [User] u
    INNER JOIN Recruiter r ON u.UserId = r.UserId
    INNER JOIN Reviews rr ON r.RecruiterId = rr.RecruiterId
    INNER JOIN Student s ON rr.UserId = s.StudentId;

	CREATE TABLE PlacementSummary (
    TotalParticipants INT NOT NULL,
    StudentsHired INT NOT NULL,
    PlacementPercentage FLOAT NOT NULL CHECK (PlacementPercentage >= 0 AND PlacementPercentage <= 100)
);

INSERT INTO PlacementSummary (TotalParticipants, StudentsHired, PlacementPercentage)
SELECT 
    COUNT(DISTINCT a.Userid) AS TotalParticipants,
    COUNT(CASE WHEN s.Hire = 1 THEN 1 END) AS StudentsHired,
    CAST(COUNT(CASE WHEN s.Hire = 1 THEN 1 END) AS FLOAT) / 
    NULLIF(COUNT(DISTINCT a.Userid), 0) * 100 AS PlacementPercentage
FROM 
    Applications a
JOIN 
    Student s ON a.Userid = s.StudentId;

	CREATE TABLE DepartmentPlacementSummary (
    DegreeProgram VARCHAR(10) PRIMARY KEY,
    TotalParticipants INT NOT NULL,
    StudentsHired INT NOT NULL,
    PlacementPercentage FLOAT NOT NULL CHECK (PlacementPercentage >= 0 AND PlacementPercentage <= 100)
);

INSERT INTO DepartmentPlacementSummary (DegreeProgram, TotalParticipants, StudentsHired, PlacementPercentage)
SELECT 
    s.DegreeProgram,
    COUNT(DISTINCT a.Userid),
    COUNT(CASE WHEN s.Hire = 1 THEN 1 END),
    CAST(COUNT(CASE WHEN s.Hire = 1 THEN 1 END) AS FLOAT) / 
    NULLIF(COUNT(DISTINCT a.Userid), 0) * 100
FROM 
    Applications a
JOIN 
    Student s ON a.Userid = s.StudentId
GROUP BY 
    s.DegreeProgram;


	CREATE TABLE AverageSalaryByDegree (
    DegreeProgram VARCHAR(10) PRIMARY KEY,
    AverageSalary FLOAT NOT NULL CHECK (AverageSalary >= 0)
);

drop table AverageSalaryByDegree

WITH SalaryData AS (
    SELECT 
        s.DegreeProgram,
       TRY_CAST(REPLACE(REPLACE(REPLACE(Salary, 'PKR', ''), ',', ''), ' ', '') AS FLOAT) AS CleanedSalary
    FROM 
        Student s
    JOIN 
        Applications a ON s.StudentId = a.Userid
    JOIN 
        JobPostings jp ON a.JobID = jp.JobID
    JOIN 
        Interviews i ON i.ApplicationID = a.ApplicationID
    WHERE 
        s.Hire = 1 AND i.Result = 'Passed'
)

INSERT INTO AverageSalaryByDegree (DegreeProgram, AverageSalary)
SELECT 
    DegreeProgram,
    AVG(CleanedSalary)
FROM 
    SalaryData
WHERE 
    CleanedSalary IS NOT NULL
GROUP BY 
    DegreeProgram;

	--1

	SELECT 
    DegreeProgram, 
    COUNT(*) AS RegistrationCount
INTO 
    DepartmentWiseRegistration
FROM 
    Student
	where Student.DegreeProgram is not null
GROUP BY 
    DegreeProgram;
--2
SELECT 
    CASE 
        WHEN cgpa BETWEEN 0 AND 1.0 THEN '0.0 - 1.0'
        WHEN cgpa BETWEEN 1.1 AND 2.0 THEN '1.1 - 2.0'
        WHEN cgpa BETWEEN 2.1 AND 3.0 THEN '2.1 - 3.0'
        WHEN cgpa BETWEEN 3.1 AND 4.0 THEN '3.1 - 4.0'
       -- ELSE 'Unknown'
    END AS GpaRange,
    COUNT(*) AS NumberOfStudents
INTO 
    GpaDistribution
FROM 
    Student
GROUP BY 
    CASE 
        WHEN cgpa BETWEEN 0 AND 1.0 THEN '0.0 - 1.0'
        WHEN cgpa BETWEEN 1.1 AND 2.0 THEN '1.1 - 2.0'
        WHEN cgpa BETWEEN 2.1 AND 3.0 THEN '2.1 - 3.0'
        WHEN cgpa BETWEEN 3.1 AND 4.0 THEN '3.1 - 4.0'
     --   ELSE 'Unknown'
    END;
--3
SELECT 
    skills, 
    COUNT(*) AS SkillCount
INTO 
    TopSkills
FROM 
    StudentSkills
WHERE 
    StudentSkills.skills IS NOT NULL
GROUP BY 
    skills
ORDER BY 
    SkillCount DESC;

	--10
	SELECT 
    BoothID,
    location,
    VisitorCount
INTO 
    BoothTrafficStats
FROM 
    Booth
WHERE 
    VisitorCount IS NOT NULL;

	--11
	SELECT 
    DATEPART(HOUR, check_intime) AS CheckInHour,
    COUNT(*) AS TotalCheckIns
INTO 
    PeakParticipationHours
FROM 
    Booth
WHERE 
    check_intime IS NOT NULL
GROUP BY 
    DATEPART(HOUR, check_intime)
ORDER BY 
    TotalCheckIns DESC;

	--12
	SELECT 
    BoothCoordinatorId,
    ShiftTimings,
    DATEDIFF(HOUR,
        TRY_CAST(SUBSTRING(ShiftTimings, 1, CHARINDEX('-', ShiftTimings) - 1) AS TIME),
        TRY_CAST(SUBSTRING(ShiftTimings, CHARINDEX('-', ShiftTimings) + 1, LEN(ShiftTimings)) AS TIME)
    ) AS ShiftHours
INTO 
    CoordinatorTimeUsage
FROM 
    Booth_Coordinator
WHERE 
    ShiftTimings IS NOT NULL;


	select*from CoordinatorTimeUsage
	drop table CoordinatorTimeUsage
	select*from Booth_Coordinator

	




























