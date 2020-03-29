/* numCheckins*/
UPDATE business

SET numCheckins = (SELECT COUNT(busid) FROM checkin WHERE checkin.busID = business.busid GROUP BY checkin.busID);

/* numTips */
UPDATE business
SET review_count = (SELECT COUNT(busID) FROM tip WHERE tip.busID = business.busID GROUP BY tip.busID);

/* tipcount */
-- (SELECT users.userID,COUNT(tip.userID) FROM tip,users 
-- WHERE users.userID = tip.userID GROUP BY users.userID);
UPDATE users
SET tipcount = (SELECT COUNT(tip.userID) FROM tip
WHERE users.userID = tip.userID GROUP BY users.userID);

/* numLikes */
-- (SELECT users.userID,SUM(tip.likes) FROM 
-- tip,users WHERE tip.userID = users.userID GROUP BY users.userID);
UPDATE users
SET numLikes = (SELECT SUM(tip.likes) FROM tip WHERE tip.userID = users.userID GROUP BY users.userID)