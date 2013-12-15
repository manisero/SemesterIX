import random
from dsa.dsa import DSA


def main():
    dsa = DSA()
    print 'q = ' + str(dsa.generate_q(random.getrandbits(160)))

if __name__ == '__main__':
    main()
