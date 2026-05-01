CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    UserRole NVARCHAR(20) NOT NULL DEFAULT 'Student'
);
CREATE TABLE LostItems (
    LostItemID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    ItemName NVARCHAR(100) NOT NULL,
    Category NVARCHAR(50),
    Description NVARCHAR(300),
    LostLocation NVARCHAR(100),
    LostDate DATE,
    Status NVARCHAR(20) DEFAULT 'Pending',

    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE FoundItems (
    FoundItemID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    ItemName NVARCHAR(100) NOT NULL,
    Category NVARCHAR(50),
    Description NVARCHAR(300),
    FoundLocation NVARCHAR(100),
    FoundDate DATE,
    Status NVARCHAR(20) DEFAULT 'Pending',

    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE Matches (
    MatchID INT PRIMARY KEY IDENTITY(1,1),
    LostItemID INT NOT NULL,
    FoundItemID INT NOT NULL,
    AdminID INT NOT NULL,
    MatchDate DATE DEFAULT GETDATE(),
    MatchStatus NVARCHAR(20) DEFAULT 'Matched',

    FOREIGN KEY (LostItemID) REFERENCES LostItems(LostItemID),
    FOREIGN KEY (FoundItemID) REFERENCES FoundItems(FoundItemID),
    FOREIGN KEY (AdminID) REFERENCES Users(UserID)
);
ALTER TABLE Users
ADD UniversityID NVARCHAR(50) UNIQUE;