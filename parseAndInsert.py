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
    with open('./yelp_business.JSON','r') as f:    #TODO: update path for the input file
        #outfile =  open('./yelp_business.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to Milestone2 database on postgres server using psycopg2
        try:
            conn = psycopg2.connect("dbname='Milestone2' user='GuestUser' host='localhost' password='abcd123'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # Generate the INSERT statement for the cussent business
            # TODO: The below INSERT statement is based on a simple (and incomplete) businesstable schema. Update the statement based on your own table schema and
            # include values for all businessTable attributes
            sql_str = "INSERT INTO business (busid, name, address, state, city, postal_code, latitude, longitude, stars, numCheckins, review_count, isopen) " \
                      "VALUES ('" + data['business_id'] + "','" + cleanStr4SQL(data["name"]) + "','" + cleanStr4SQL(data["address"]) + "','" + \
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

def insert2Categories():
    #reading the JSON file
    with open('./yelp_business.JSON','r') as f:    #TODO: update path for the input file
        outfile =  open('./yelp_categories.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to yelpdb database on postgres server using psycopg2
        #TODO: update the database name, username, and password
        try:
            conn = psycopg2.connect("dbname='Milestone2' user='GuestUser' host='localhost' password='abcd123'")
        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            # for category in data['categories']:
            s = cleanStr4SQL(data['categories'])
            cat = s.split(", ")
            for x in cat:
                sql_str = "INSERT INTO Categories (busID, category) " \
                        "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + x +"');"
                try:
                        cur.execute(sql_str)
                except:
                        print("Insert to businessTABLE failed! \n" + sql_str)
                        return
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

def insert2businessHours():

    #reading the JSON file
    with open('./yelp_business.JSON','r') as f:    #TODO: update path for the input file
        outfile =  open('./yelp_businesshours.SQL', 'w')  #uncomment this line if you are writing the INSERT statements to an output file.
        line = f.readline()
        count_line = 0

        #connect to Milestone2 database on postgres server using psycopg2
        try:
            conn = psycopg2.connect("dbname='Milestone2' user='GuestUser' host='localhost' password='abcd123'")
        except Exception as inst:
            print('Unable to connect to the database!' )
            print(type(inst))
            print(inst.args)
            print(inst)
            return
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            if data['hours']!={}:
                for x,y in data['hours'].items():
                    tem=(y.split("-"))
                    #print(data['business_id'],x,tem[0],tem[1])
                    sql_str = "INSERT INTO businessHours (busID, dayofWeek,openTime,closeTime) " \
                            "VALUES ('" + cleanStr4SQL(data['business_id']) + "','" + x + "','" + tem[0]+"','"+tem[1]+"');"
                    try:
                        cur.execute(sql_str)
                    except:
                        print("Insert to businessTABLE failed! \n" + sql_str)
                        return
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
            conn = psycopg2.connect("dbname='Milestone2' user='GuestUser' host='localhost' password='abcd123'")

        except:
            print('Unable to connect to the database!')
        cur = conn.cursor()

        while line:
            data = json.loads(line)
            sql_str = "INSERT INTO Users(userID, avgStar, cool, fans, funny, name, tipCount, numLikes, Useful, Yelping_since, latitude, longitude) " \
                "Values ('" + cleanStr4SQL(data['user_id']) + "','" + str(data['average_stars']) + "','" + str(data['cool']) + "','" + str(data['fans']) + "','" + \
                str(data['funny']) + "','" + cleanStr4SQL(data['name']) + "','" + str(data['tipcount']) + "','" + "0" + "','" + str(data['useful']) + "','" + str(data['yelping_since']) + \
                "','" + "0.0.0' , '0.0.0" + "');"
            try:
                cur.execute(sql_str)
            except Exception as inst:
                print("Insert to usersTable failed! \n" + sql_str)
                print(type(inst))
                print(inst.args)
                print(inst)
                return
            conn.commit()
            outfile.write(sql_str)
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()

def insert2Tips():
    with open('./yelp_tip.JSON','r') as f:
        outfile =  open('./yelp_tip.SQL', 'w')
        line = f.readline()
        count_line = 0
        passedLastBreak = False
        try:
            conn = psycopg2.connect("dbname='Milestone2' user='GuestUser' host='localhost' password='abcd123'")
        except Exception as inst:
            print('Unable to connect to the database!' )
            print(type(inst))
            print(inst.args)
            print(inst)
            return
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            if len(cleanStr4SQL(data['text'])) >= 500:
                passedLastBreak = True
            sql_str = "INSERT INTO Tip(busID, userID, likes, tip, madeOn) " \
                "Values ('" + cleanStr4SQL(data['business_id']) + "','" + cleanStr4SQL(data['user_id']) + "','" + str(data['likes']) + "','" + \
                     cleanStr4SQL(data['text']) + "','" + str(data['date']) + "');"
            if passedLastBreak == True:
                try:
                    cur.execute(sql_str)
                except Exception as inst:
                    print("Insert to TipsTable failed! \n" + sql_str)
                    print(type(inst))
                    print(inst.args)
                    print(inst)
                    
                conn.commit()
                outfile.write(sql_str)
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()

def insert2Checkins():
    with open('./yelp_checkin.JSON','r') as f:
        outfile =  open('./yelp_checkin.SQL', 'w')
        line = f.readline()
        count_line = 0
        try:
            conn = psycopg2.connect("dbname='Milestone2' user='GuestUser' host='localhost' password='abcd123'")
        except Exception as inst:
            print('Unable to connect to the database!' )
            print(type(inst))
            print(inst.args)
            print(inst)
            return
        cur = conn.cursor()
        while line:
            data = json.loads(line)
            busID = cleanStr4SQL(data['business_id'])
            fullDatesString = data['date']
            splitDates = fullDatesString.split(',')
            for i in range(len(splitDates)):
                sql_str = "INSERT INTO Checkin(busID, date) " \
                    "Values ('" + busID + "','" + str(splitDates[i]) + "');"
                try:
                    cur.execute(sql_str)
                except Exception as inst:
                    print("Insert to Checins Table failed! \n" + sql_str)
                    print(type(inst))
                    print(inst.args)
                    print(inst)
                    
                conn.commit()
                outfile.write(sql_str)
            line = f.readline()
            count_line +=1
        cur.close()
        conn.close()
    print(count_line)
    f.close()


#insert2BusinessTable()
#insert2businessHours()
#insert2Users()
#insert2Tips()
#insert2Checkins()
insert2Categories()