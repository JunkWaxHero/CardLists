import os
import json
import argparse
from decimal import Decimal

def create_prefix(filepath, setname, prefix):
    try:
        with open(filepath, 'r', encoding='utf-8') as f:
            data = json.load(f)
    except Exception as e:
        print(f"Error reading {filepath}: {e}")
        return
    
    #Converts list to dict to make elements accessible by setname
    newData = {x['name']: x for x in data['sets']}
    directory = newData[setname]['cards']

    #Add Prefix
    for card in directory:
        card.update(number = prefix + "-" + card['number'])

    #Update version number
    major, minor = [int(x, 10) for x in data['version'].split('.')]
    minor += 1
    updatedVersion = str(major) + "." + str(minor)
    data.update(version = updatedVersion)

    splitName = data["name"].split(" ")
    uniqueId = data["uniqueId"]
    splitPath = filepath.split('/')

    #Returns Year of Product
    year = splitName[0]

    #Returns Name of Product
    productName = splitName[1]+" "+splitName[2]

    #Returns Product category
    sportCategory = splitPath[-3]

    #Injects sportCategory ,retrieves category JSON, and loads into script
    categoryPath = "../categories/"+sportCategory+".json"
    c = open(categoryPath, 'r', encoding='utf-8')
    category = json.load(c)

    #Sorts Dict by Year then by Release
    updatedCategory = {x['year']: x for x in category['category']['years']}
    findYear = updatedCategory[year]['releases']
    sortByName = {x['name']: x for x in findYear}

    #Retrieves product information by name
    getProduct = sortByName[productName]

    #Checks uniqueId to confirm product identity before updating version.
    if getProduct['uniqueId'] == uniqueId:
        getProduct.update(version = updatedVersion)

    #Convert back to JSON
    json_object = json.dumps(data, indent=2)
    json_object2 = json.dumps(category, indent=2)


    #Write updates to JSON files
    with open(filepath,'w') as outfile:
        outfile.write(json_object)

    with open(categoryPath,'w') as outfile:
        outfile.write(json_object2)


    #Success Message
    print(prefix + " Prefix Added to " + setname)
    print("Version updated to: " + updatedVersion)
    print(sportCategory+".json has been saved.")
    print(splitPath[-1]+" has been saved.")

def main(filepath, setname, prefix):
    if not os.path.isfile(filepath):
        print(f"Error: {filepath} is not a valid file.")
        return
    create_prefix(filepath, setname, prefix)

if __name__ == '__main__':
    parser=argparse.ArgumentParser()
    parser.add_argument('-j', '--json')
    parser.add_argument('-s', '--set')
    parser.add_argument('-p', '--prefix')
    args=parser.parse_args()

    main(args.json, args.set, args.prefix)