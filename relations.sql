-- Create this table first
CREATE TABLE Business (
	busID VARCHAR(500),
	name VARCHAR(500),
	address VARCHAR(500),
	city VARCHAR(500),
	state VARCHAR(500),
	postal_code VARCHAR(500),
	latitude VARCHAR(500),
	longitude VARCHAR(500),
	stars INT,
	numCheckins INT,
	isOpen INT,
	review_count INT,
	PRIMARY KEY (busID),
);

-- Create this table second
CREATE TABLE Users (
	userID VARCHAR(50) NOT NULL,
	avgStar INT,
	cool INT,
	fans INT,
	funny INT, 
	name VARCHAR(50),
	tipcount INT,
	numLikes INT,
	useful INT,
	yelping_since VARCHAR(500),
	latitude VARCHAR(50),
	longitude VARCHAR(50),
	PRIMARY KEY (userID)
);

CREATE TABLE businessHours (
	busID VARCHAR(50),
	dayofWeek VARCHAR(10),
	openclose VARCHAR(50),
	openTime TIME,
	closeTime TIME,
	PRIMARY KEY (busID, day),
	FOREIGN KEY (busID) REFERENCES Business(busID),
);

CREATE TABLE Attribute (
	busID VARCHAR(50),
	attribute_name VARCHAR(50) NOT NULL,
	value VARCHAR(50),
	PRIMARY KEY (busID, attribute_name),
	FOREIGN KEY (busID) REFERENCES Business(busID),
);

CREATE TABLE Tip (
	busID VARCHAR(500) NOT NULL,
	userID VARCHAR(500) NOT NULL,
	likes INT,
	tip VARCHAR(MAX),
	madeOn DATETIME,
	PRIMARY KEY(busID,userID,madeOn),
	FOREIGN KEY (busID) REFERENCES Business(busID),
	FOREIGN KEY(userID) REFERENCES Users(userID)
);


CREATE TABLE CheckIn (
	busID VARCHAR(50),
	date VARCHAR(50),
	PRIMARY KEY (busID),
	FOREIGN KEY (busID) REFERENCES Business(busID)
);