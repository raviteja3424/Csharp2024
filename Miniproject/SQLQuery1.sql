create database RaiwayReservation2


CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    FullName NVARCHAR(100),
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(15)
);

INSERT INTO Users (UserName, Password, FullName, Email, PhoneNumber)
VALUES 
('ravi', 'ravi3424', 'ravi teja', 'ravi3424teja.com', '9059473099'),
('john_smith', 'securepass', 'Jane Smith', 'jane.smith@example.com', '0987654321'),
('Rin_son', 'alicepass', 'Alice Johnson', 'alice.johnson@example.com', '5678901234'),
('bob_brown', 'bobpass', 'Bob brown', 'bob.brown@example.com', '4321098765');



CREATE TABLE Trains (
    TrainID INT IDENTITY(1,1) PRIMARY KEY,
    TrainNo NVARCHAR(20) NOT NULL,
    TrainName NVARCHAR(100) NOT NULL,
    FromStation NVARCHAR(100) NOT NULL,
    ToStation NVARCHAR(100) NOT NULL,
    Date DATETIME NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    NoOfSeats INT NOT NULL,
    Status NVARCHAR(50)
);



INSERT INTO Trains(TrainNo, TrainName, FromStation, ToStation, Date, Price, Status, NoOfSeats) VALUES
('12345', 'Express A', 'Station X', 'Station Y', '2024-08-10', 49.99, 'On Time', 100),
('67890', 'Express B', 'Station Y', 'Station Z', '2024-08-11', 59.99, 'Delayed', 120),
('11223', 'Express C', 'Station Z', 'Station X', '2024-08-12', 69.99, 'Cancelled', 150),
('44556', 'Express D', 'Station X', 'Station Z', '2024-08-13', 39.99, 'On Time', 110),
('78901', 'Express E', 'Station Y', 'Station X', '2024-08-14', 89.99, 'On Time', 130);





CREATE TABLE Booking (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    TrainID INT FOREIGN KEY REFERENCES Trains(TrainID),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    NumberOfSeats INT NOT NULL,
    Status NVARCHAR(50) NOT NULL
);


select* from Trains

select* from Users

select* from Booking 







