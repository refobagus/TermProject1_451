-- Create this table first


CREATE TABLE Business (
	bus_id VARCHAR(500),
	name VARCHAR(500),
	address VARCHAR(500),
	state VARCHAR(500),
	city VARCHAR(500),
	zipcode VARCHAR(500),
	latitude VARCHAR(500),
	longitude VARCHAR(500),
	stars INT,
	numCheckins INT,
	review_count INT,
	isOpen INT,
	PRIMARY KEY (bus_id)
);

-- Create this table second
CREATE TABLE Users (
	userID VARCHAR(50) NOT NULL,
	avgStar FLOAT,
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
	openTime TIME,
	closeTime TIME,
	PRIMARY KEY (busID, dayofWeek),
	FOREIGN KEY (busID) REFERENCES Business(bus_id)
);

CREATE TABLE Categories (
	busID VARCHAR(50),
	category VARCHAR(100),
	FOREIGN KEY (busID) REFERENCES Business(bus_id)
);

CREATE TABLE Tip (
	busID VARCHAR(500) NOT NULL,
	userID VARCHAR(500) NOT NULL,
	likes INT,
	tip VARCHAR(1000),
	madeOn TIMESTAMP,
	PRIMARY KEY(busID,userID,madeOn),
	FOREIGN KEY (busID) REFERENCES Business(bus_id),
	FOREIGN KEY(userID) REFERENCES Users(userID)
);



CREATE TABLE CheckIn (
	busID VARCHAR(50),
	date TIMESTAMP,
	PRIMARY KEY (busID, date),
	FOREIGN KEY (busID) REFERENCES Business(bus_id)
);