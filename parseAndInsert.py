import json
import psycopg2

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def int2BoolStr (value):
    if value == 0:
        return 'False'
    else:
        return 'True'

def insert2BusinessTable():
    #reading the JSON file
    with open('./business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='yelpdb' user='postgres' host='localhost' password='wartech25'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str = "INSERT INTO businessTable (bus_id, name, address, state, city, zipcode, latitude, longitude, stars, numCheckins, numTips, openStatus) " \
                      "VALUES ('" + data['bus_id'] + "','" + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["address"]) + "','" + \
                      cleanStr4SQL(data["state"]) + "','" + cleanStr4SQL(data["city"]) + "','" + data["postal_code"] + "'," + str(data["latitude"]) + "," + \
                      str(data["longitude"]) + "," + str(data["stars"]) + ", 0 , 0 ,"  +  str(data["is_open"]) + ");"
            try:
                cur.execute(sql_str)
            except:
                print("Insert to businessTABLE failed!")
            conn.commit()
            # optionally you might write the INSERT statement to a file.
            # outfile.write(sql_str)

            line = f.readline()
            count_line +=1

        cur.close()
        conn.close()

    print(count_line)
    #outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2businessHours():
    #reading the JSON file
    with open('./yelp_business.JSON','r') as f:    #TODO: update path for the input file
        outfile =  open('./yelp_businesshours.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='wartech25'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            if data['hours']!={}:
                for x,y in data['hours'].items():
                    tem=(y.split("-"))
                    #print(data['business_id'],x,tem[0],tem[1])
                    sql_str = "INSERT INTO businessHours (Business_id, daysofWeek,openTime,closeTime) " \
                            "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + x+"','"+ tem[0]+"','"+tem[1]+"');"
                    try:
                        cur.execute(sql_str)
                    except:
                        print("Insert to businessTABLE failed! \n"+sql_str)
                    conn.commit()
                    # optionally you might write the INSERT statement to a file.
                    count_line +=1
                    outfile.write(sql_str)
            line = f.readline()
        cur.close()
        conn.close()
    print(count_line)
    outfile.close()  #uncomment this line if you are writing the INSERT statements to an output file.
    f.close()

def insert2Users():
    with open('./yelp_user.JSON','r') as f:
        outfile =  open('./yelp_user.SQL', 'w')
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='milestone2' user='postgres' host='localhost' password='teamsk'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            sql_str = 
            try:
                cur.execute(sql_str)
            except:
                print("Insert to usersTable failed! \n"+sql_str)
            conn.commit()
            outfile.write(sql_str)
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()

insert2BusinessTable()