ovire = [ (1,3,6), (2,4,3), (4,6,7), (3,4,9), (6,9,5), (9,10,2), (9,10,8)]
x = 6
hit = False
for i in range(1,11):
    for (x1,x2,y) in ovire:
        if y == i and x<=x2 and x>=x1:
            print(i)
            hit = True
            break
    if hit:
        break