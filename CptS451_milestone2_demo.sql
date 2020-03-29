
--GLOSSARY
--table names
/*
businesstable
usertable
tipstable
friendstable
checkin
businesscategory
businessattribute
businesshours

--some attribute names
zipcode
business_id
city  (business city)
name   (business name)
user_id
friend_id
numtips
numCheckins

user_id
tipcount  (user)
totallikes (user)

tipdate
tiptext
likes  (tip)

checkinyear
checkinmonth
checkinday
checkintime

*/
--1.
SELECT COUNT(*) 
FROM  business;
SELECT COUNT(*) 
FROM  users;
SELECT COUNT(*) 
FROM  tip;
/*
SELECT COUNT(*) 
FROM  friendstable;
*/
SELECT COUNT(*) 
FROM  checkin;
SELECT COUNT(*) 
FROM  categories;
--SELECT COUNT(*) 
--FROM  businessattribute;
SELECT COUNT(*) 
FROM  businesshours;



--2. Run the following queries on your business table, checkin table and review table. Make sure to change the attribute names based on your schema. 

SELECT postal_code, count(busID) 
FROM business
GROUP BY postal_code
HAVING count(busID) > 500
ORDER BY postal_code;

SELECT postal_code, COUNT(distinct C.category)
FROM business as B, categories as C
WHERE B.busID = C.busID
GROUP BY postal_code
HAVING count(distinct C.category)>300
ORDER BY postal_code;
/*
SELECT postal_code, COUNT(distinct A.attribute)
FROM business as B, businessattribute as A
WHERE B.business_id = A.business_id
GROUP BY zipcode
HAVING count(distinct A.attribute)>65;
*/

--3. Run the following queries on your business table, checkin table and tips table. Make sure to change the attribute names based on your schema. 
/*
SELECT users.userID, count(friend_id)
FROM users, friendstable
WHERE usertable.userID = friendstable.userID AND 
      usertable.userID = 'zvQ7B3KZuFOX7pYLsOxhpA'
GROUP BY usertable.userID;
*/

SELECT busID, name, city, review_count, numCheckins 
FROM business 
WHERE busID ='UvF68aNDfzCWQbxO6-647g' ;

SELECT userID, name, tipcount, numLikes
FROM users
WHERE userID = 'i3bLA4sEdFk8j3Pq6tx8wQ';

-----------

SELECT COUNT(*) 
FROM checkin
WHERE busID ='UvF68aNDfzCWQbxO6-647g';

SELECT count(*)
FROM tip
WHERE  busID = 'UvF68aNDfzCWQbxO6-647g';



--4. 
--Type the following statements. Make sure to change the attribute names based on your schema. 

SELECT COUNT(*) 
FROM checkin
WHERE busID ='M007_bAIM34x1yd138zhSQ';

SELECT busID,name, city, numCheckins, review_count
FROM business 
WHERE busID ='M007_bAIM34x1yd138zhSQ';

INSERT INTO checkin (busID, date)
VALUES ('M007_bAIM34x1yd138zhSQ','2020-03-27 15:20');


--5.
--Type the following statements. Make sure to change the attribute names based on your schema.  

SELECT busID ,name, city, numCheckins, review_count
FROM business
WHERE busID ='M007_bAIM34x1yd138zhSQ';

SELECT userID, name, tipcount, numlikes
FROM users
WHERE userID = 'rRrFcSEZOTw6iZagsIwTFQ';


INSERT INTO tip (userID, busID, madeOn, tip,likes)  
VALUES ('rRrFcSEZOTw6iZagsIwTFQ','M007_bAIM34x1yd138zhSQ', '2020-03-27 13:20','EVERYTHING IS AWESOME',0);

UPDATE tip
SET likes = likes+1
WHERE userID = 'rRrFcSEZOTw6iZagsIwTFQ' AND 
      busID = 'M007_bAIM34x1yd138zhSQ' AND 
      madeOn ='2020-03-27 13:20';
      