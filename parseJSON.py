import json

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")


def parseBusinessData():
    #read the JSON file
    with open('yelp_business.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('business.txt', 'w')
        line = f.readline()
        count_line = 0
        outfile.write('HEADER: (business_id, name; address; state; state; city; postal_code; latitude; longitude; stars; is_open)\n')
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
            outfile.write(str(count_line+1)+'-\t')
            outfile.write(cleanStr4SQL(data['business_id'])+' ; ') #business id
            outfile.write(cleanStr4SQL(data['name'])+' ; ') #name
            outfile.write(cleanStr4SQL(data['address'])+' ; ') #full_address
            outfile.write(cleanStr4SQL(data['state'])+' ; ') #state
            outfile.write(cleanStr4SQL(data['city'])+' ; ') #city
            outfile.write(cleanStr4SQL(data['postal_code']) + ' ; ')  #zipcode
            outfile.write(str(data['latitude'])+' ; ') #latitude
            outfile.write(str(data['longitude'])+' ; ') #longitude
            outfile.write(str(data['stars'])+' ; ') #stars
            outfile.write(str(data['review_count'])+' ; ') #reviewcount
            outfile.write(str(data['is_open'])+'\n\t') #openstatus

            categories = data["categories"].split(', ')
            outfile.write(str(categories)+'\n\t[')  #category list
            
            attributes = data['attributes']
            for attr, val in attributes.items():
                if type(val) == str or type(val) == bool:
                    outfile.write('(\'' + attr + '\', \'' + val + '\')')
                if type(val) == dict:
                    for nested_attr, nested_val in val.items():
                        outfile.write('(\'' + nested_attr + '\', \'' + nested_val + '\')')
            outfile.write(']\n\t[')

            hours = data['hours']
            for day,hour in hours.items():
                x= hour.split('-')
                outfile.write('(\''+day+'\', \''+x[0]+'\', \''+x[1]+'\')')
            outfile.write(']\n')

            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()

def parseCheckinData():
    with open('yelp_checkin.JSON', 'r') as f:
        outfile = open('checkins.txt', 'w')
        line = f.readline() 
        count_line = 0
        outfile.write('HEADER: (business_id : (year month,day,time))\n')
        while line:
            outfile.write(str(count_line+1)+'- ')
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+':\n')
            date = data["date"].split(',')
            outfile.write(str(date)+'\t') 
            outfile.write('\n')
            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()
    pass


def parseTipData():
    with open('yelp_tip.JSON', 'r') as f:
        outfile = open('tip.txt', 'w')
        line = f.readline() 
        count_line = 0
        outfile.write('HEADER: (business_id; date; likes; text; user_id)\n')
        while line:
            outfile.write(str(count_line+1)+'- \'')
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+'\' ; \'')
            outfile.write(str(data['date'])+'\' ; ') 
            outfile.write(str(data['likes'])+' ; \'') 
            outfile.write(cleanStr4SQL(data['text'])+'\' ; \'')
            outfile.write(cleanStr4SQL(data['user_id'])+'\'\n') 

            line = f.readline()
            count_line +=1
    print(count_line)

    outfile.close()
    f.close()
    pass

def parseUserData():
    with open('yelp_user.JSON','r') as f:
        outfile = open('user.txt','w')
        line = f.readline()
        count_line = 0
        outfile.write('HEADER: (user_id; name; yelping_since; tipcount; fans; average_stars; (funny,useful,cool))\n')
        while line:
            outfile.write(str(count_line)+'- ')
            data = json.loads(line)
            outfile.write(str(data['average_stars']) + ' ; ')
            outfile.write(str(data['cool']) + ' ; ')
            outfile.write(str(data['fans']) + ' ; ')
            outfile.write(str(data['friends']) + ' ; ')
            outfile.write(str(data['funny']) + ' ; ')
            outfile.write(cleanStr4SQL(data['name']) + ' ; ')
            outfile.write(str(data['tipcount']) + ' ; ')
            outfile.write(str(data['useful']) + ' ; ')
            outfile.write(cleanStr4SQL(data['user_id']) + ' ; ')
            outfile.write(str(data['yelping_since']) + '\n')
            outfile.write('\n')
            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()


parseBusinessData()
parseUserData()
parseCheckinData()
parseTipData()
