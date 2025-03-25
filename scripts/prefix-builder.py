import os
import sys
import json
import uuid
import argparse

def create_prefix(filepath, setname, prefix):
    try:
        with open(filepath, 'r', encoding='utf-8') as f:
            data = json.load(f)
    except Exception as e:
        print(f"Error reading {filepath}: {e}")
        return
    
    #Converts list to dict to make elements accessible by setname
    newData = {x['name']: x for x in data['sets']}
    category = newData[setname]['cards']

    #Add Prefix
    for card in category:
        card.update(number = prefix + "-" + card['number'])

    #Convert back to JSON
    json_object = json.dumps(data, indent=2)

    with open(filepath,'w') as outfile:
        outfile.write(json_object)

    #Success Message
    print(prefix + " Added to " + setname)

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