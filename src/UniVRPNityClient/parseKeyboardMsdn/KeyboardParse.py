f = open('FrenchKeyboard.txt', 'r')
for line in f:
    if(line):
        words = line.split()
        if(len(words) >= 2):
            print("KC_" + words[1].strip() + " 0x" + words[0])
            #for()
