import json

def cleanStr4SQL(s):
    return s.replace("'","`").replace("\n"," ")

def parseBusinessData():
    #read the JSON file
    with open('yelp_business.JSON','r') as f:  #Assumes that the data files are available in the current directory. If not, you should set the path for the yelp data files.
        outfile =  open('business.txt', 'w')
        line = f.readline()
        count_line = 0
        #read each JSON abject and extract data
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+'\t') #business id
            outfile.write(cleanStr4SQL(data['name'])+'\t') #name
            outfile.write(cleanStr4SQL(data['address'])+'\t') #full_address
            outfile.write(cleanStr4SQL(data['state'])+'\t') #state
            outfile.write(cleanStr4SQL(data['city'])+'\t') #city
            outfile.write(cleanStr4SQL(data['postal_code']) + '\t')  #zipcode
            outfile.write(str(data['latitude'])+'\t') #latitude
            outfile.write(str(data['longitude'])+'\t') #longitude
            outfile.write(str(data['stars'])+'\t') #stars
            outfile.write(str(data['review_count'])+'\t') #reviewcount
            outfile.write(str(data['is_open'])+'\t') #openstatus

            categories = data["categories"].split(', ')
            outfile.write(str(categories)+'\t')  #category list
            
            outfile.write(str([])) # write your own code to process attributes
            outfile.write(str([])) # write your own code to process hours
            outfile.write('\n')

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
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+'\t')
            outfile.write(cleanStr4SQL(data['date'])+'\t')
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
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+'\t')
            outfile.write(str(data['date'])+'\t') 
            outfile.write(str(data['likes'])+'\t') 
            outfile.write(cleanStr4SQL(data['text'])+'\t')
            outfile.write(cleanStr4SQL(data['user_id'])+'\t') 

            outfile.write('\n')

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
        while line:
            data = json.loads(line)
            outfile.write(str(data['average_stars']) + '\t')
            outfile.write(str(data['cool']) + '\t')
            outfile.write(str(data['fans']) + '\t')
            outfile.write(str(data['friends']) + '\t')
            outfile.write(str(data['funny']) + '\t')
            outfile.write(cleanStr4SQL(data['name']) + '\t')
            outfile.write(str(data['tipcount']) + '\t')
            outfile.write(str(data['useful']) + '\t')
            outfile.write(cleanStr4SQL(data['user_id']) + '\t')
            outfile.write(str(data['yelping_since']) + '\t')
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
