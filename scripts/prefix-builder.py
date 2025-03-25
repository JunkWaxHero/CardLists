import os
import sys
import json
import uuid


#Looks for Argument/ Path to JSON. 
if len(sys.argv) > 1:
    workingJson = sys.argv[1]
else:
    #Drop JSON in Terminal or Copy/Paste Path file
    workingJson=str(input("Insert Path to JSON Here: "))

#Initial script setup
f = open(workingJson)
data = json.load(f)
category = data['sets']

#Assigns index to set and displays list
x = 0
for set in category:
    print(str(x)+ " " + str(set['name']))
    x+=1

#User Prompts
setChoice = int(input('Enter index of Set to edit '))
selectOption = int(input('Select Option: \n1.Add Prefix \n2.Remove Prefix\n'))
prefix = input('Enter prefix for set: ')

#Assigns index for set.
category = data['sets'][setChoice]['cards']

#Add Prefix
if selectOption == 1:
    for card in category:
        card.update(number = prefix + "-" + card['number'])
        print(card)

#Remove Prefix 
if selectOption == 2:
    for card in category:
        card.update(number = card['number'].replace(prefix + "-",""))
        print(card)

json_object = json.dumps(data, indent=2)

with open(workingJson,'w') as outfile:
    outfile.write(json_object)
