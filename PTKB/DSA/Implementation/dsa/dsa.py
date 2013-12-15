import hashlib
import os
from large_prime_generator.large_prime_generator import LargePrimeGenerator


class DSA:
    def __init__(self):
        self.g = 160
        self.large_prime_generator = LargePrimeGenerator()

    def generate_p_q(self):
        seed = os.urandom(20)

        print 'generated: ' + str(self.generate_q(seed))

    def generate_q(self, seed):

        u = hashlib.sha1(seed).digest() ^ (hashlib.sha1())
        q = u | 2**159 | 1

        if self.large_prime_generator.test_if_prime(q):
            return q

        return self.generate_q()



