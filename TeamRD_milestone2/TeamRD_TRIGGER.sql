CREATE OR REPLACE FUNCTION tipUpdate() RETURNS TRIGGER AS '
BEGIN
    UPDATE Users
    SET tipCount = tipCount + 1
    WHERE Users.userID = NEW.userID;
    RETURN NEW;
END
' LANGUAGE plpgsql;


CREATE TRIGGER UpdateTips
AFTER INSERT ON Tip
FOR EACH ROW
WHEN (NEW.userID IS NOT NULL)
EXECUTE PROCEDURE tipUpdate();


CREATE OR REPLACE FUNCTION checkInUpdate() RETURNS TRIGGER AS '
BEGIN
    UPDATE Business
    SET numCheckins = numCheckins + 1
    WHERE Business.busID = NEW.busID;
    RETURN NEW;
END
' LANGUAGE plpgsql;

CREATE TRIGGER UpdateCheckins
AFTER INSERT ON CheckIn
FOR EACH ROW
WHEN (NEW.busID IS NOT NULL)
EXECUTE PROCEDURE checkInUpdate();

CREATE OR REPLACE FUNCTION UpdateLikes() RETURNS TRIGGER AS '
BEGIN
    UPDATE Users
    SET numLikes = numLikes + 1
    WHERE Users.userID = NEW.userID;
    RETURN NEW;
END
'LANGUAGE plpgsql;

CREATE TRIGGER UpdateLikes
AFTER UPDATE ON Tip
FOR EACH ROW
WHEN(NEW.userID IS NOT NULL)
EXECUTE PROCEDURE UpdateLikes();

CREATE OR REPLACE FUNCTIOn UpdateTipCount() RETURNS TRIGGER AS '
BEGIN
    UPDATE business
    SET review_count = review_count + 1
    WHERE business.busID = NEW.busID;
    RETURN NEW;
END
'LANGUAGE plpgsql;
CREATE TRIGGER UpdateTipsCount
AFTER INSERT ON Tip
FOR EACH ROW
WHEN(NEW.busID IS NOT NULL)
EXECUTE PROCEDURE UpdateTipCount();


