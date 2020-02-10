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
            outfile.write('\n');

            line = f.readline()
            count_line +=1
    print(count_line)
    outfile.close()
    f.close()

def parseUserData():
    #write code to parse yelp_user.JSON
    pass

def parseCheckinData():
    with open('yelp_checkin.JSON', 'r') as f:
        outfile = open('checkins.txt', 'w')
        line = f.readline() 
         
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+'\t')
            outfile.write(cleanStr4SQL(data['date'])+'\t')
            outfile.write('\n');

            line = f.readline()

    outfile.close()
    f.close()
    pass


def parseTipData():
    with open('yelp_tip.JSON', 'r') as f:
        outfile = open('tip.txt', 'w')
        line = f.readline() 
        
        while line:
            data = json.loads(line)
            outfile.write(cleanStr4SQL(data['business_id'])+'\t')
            outfile.write(str(data['date'])+'\t') 
            outfile.write(str(data['likes'])+'\t') 
            outfile.write(cleanStr4SQL(data['text'])+'\t')
            outfile.write(cleanStr4SQL(data['user_id'])+'\t') 

            outfile.write('\n');

            line = f.readline()

    outfile.close()
    f.close()
    pass

parseBusinessData()
parseUserData()
parseCheckinData()
parseTipData()
