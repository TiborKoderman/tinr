#start coords = (0,0) center of map

#end cords = random where they are n distance away from start cords

#map is a 2d array of 0s and 1s S is start E is end

#all 1s are connected

n = 10

def generateLevel(n):
    #start coords:
    start = (n//2, n//2)
    end = (random.randint(0,n), random.randint(0,n))
    level = [[0 for i in range(n)] for j in range(n)]
    level[start[0]][start[1]] = "S"
    level[end[0]][end[1]] = "E"
    return level
    

level = generateLevel(n)

print(level)