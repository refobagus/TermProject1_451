
DECLARE @JSON VARCHAR(MAX)
SELECT @JSON = BulkColumn

FROM OPENROWSET
(BULK 'C:\Users\derem\OneDrive\Documents\CS451\yelp_business.JSON',SINGLE_CLOB)
AS j

SELECT business_id, name, address, city, state, postal_code,
latitude, longitude, stars, review_count, is_open, [attributes],
[categories], [hours]
INTO Businesses
    FROM OPENJSON (@JSON, '$')
    WITH (business_id VARCHAR(20),
    name VARCHAR(60),
    address VARCHAR(40),
    city VARCHAR(50),
    state VARCHAR(2),
    postal_code VARCHAR(10),
    latitude FLOAT,
    longitude FLOAT,
    stars INT,
    review_count INT,
    is_open BOOLEAN,
    [attributes] NVARCHAR(MAX) AS JSON,
    [categories] VARCHAR(MAX),
    [hours] VARCHAR(MAX) AS JSON);


