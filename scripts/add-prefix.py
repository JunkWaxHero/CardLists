import os
import sys
import json
import uuid

#Initial script setup
f = open('CardLists/categories/baseball/2022/2022-Panini-Prizm.json')
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

json_object = json.dumps(data, indent=4)

with open('CardLists/categories/baseball/2022/2022-Panini-Prizm.json','w') as outfile:
    outfile.write(json_object)

