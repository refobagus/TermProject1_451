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

CREATE TABLE Hours (
	busID VARCHAR(50),
	day VARCHAR(10),
	openclose VARCHAR(50),
	PRIMARY KEY (busID, day, openclose),
	FOREIGN KEY (busID) REFERENCES Business(busID),
);

CREATE TABLE Attribute (
	busID VARCHAR(50),
	attribute_name VARCHAR(50) NOT NULL,
	value VARCHAR(50),
	PRIMARY KEY (busID, attribute_name),
	FOREIGN KEY (busID) REFERENCES Business(busID),
);

CREATE TABLE [User] (
	avgStar INT,
	cool INT,
	fans INT,
	funny INT, 
	name VARCHAR(50),
	tipcount INT,
	useful INT,
	userID VARCHAR(50) NOT NULL,
	yelping_since VARCHAR(500),
	PRIMARY KEY (userID)
);

CREATE TABLE Tip (
	busID VARCHAR(500) NOT NULL,
	date VARCHAR(500),
	likes INT,
	text VARCHAR(MAX),
    userID VARCHAR(500) NOT NULL,
	FOREIGN KEY (busID) REFERENCES Business(busID)
);


CREATE TABLE CheckIn (
	busID VARCHAR(50),
	date VARCHAR(50),
	PRIMARY KEY (busID),
	FOREIGN KEY (busID) REFERENCES Business(busID)
);