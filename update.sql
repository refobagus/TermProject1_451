/* numCheckins*/
UPDATE business
SET numCheckins = (SELECT COUNT(busid) FROM checkin WHERE bus_id = busid GROUP BY bus_id);

/* numTips */
UPDATE business
SET review_count = (SELECT COUNT(busID) FROM tip WHERE busID = bus_id GROUP BY bus_id);

/* tipcount */
(SELECT users.userID,COUNT(tip.userID) FROM tip,users 
WHERE users.userID = tip.userID GROUP BY users.userID);

/* numLikes */
(SELECT users.userID,SUM(tip.likes) FROM 
tip,users WHERE tip.userID = users.userID GROUP BY users.userID);