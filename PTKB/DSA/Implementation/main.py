from dsa.dsa_key import DSAKey


def main():
    key = DSAKey.generate_instance(2048)

    print 'y = ' + str(key.y)
    print 'g = ' + str(key.g)
    print 'p = ' + str(key.p)
    print 'q = ' + str(key.q)
    print 'x = ' + str(key.x)

if __name__ == '__main__':
    main()
