CREATE OR REPLACE FUNCTION tipUpdate() RETURNS TRIGGER AS '
BEGIN
    UPDATE User
    SET tipCount = tipCount + 1
    WHERE User.userID = NEW.userID;
    RETURN NEW;
END
' LANGUAGE plpgsql;


CREATE TRIGGER UpdateTips
AFTER INSERT ON Tip
WHEN (NEW.userID IS NOT NULL)
EXECUTE PROCEDURE tipUpdate();


CREATE OR REPLACE FUNCTION checkInUpdate() RETURNS TRIGGER AS '
BEGIN
    UPDATE Business
    SET numCheckins = numCheckins + 1
    WHERE Business.busID = NEW.busID;
    RETURN NEW
END
' LANGUAGE plpgsql;

CREATE TRIGGER UpdateCheckins() 
AFTER INSERT ON CheckIn
WHEN (NEW.busID IS NOT NULL)
EXECUTE PROCEDURE checkInUpdate();

