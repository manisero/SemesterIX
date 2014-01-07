import os
from Crypto.Hash import SHA
from Crypto.Util.number import long_to_bytes, bytes_to_long, isPrime, inverse
from random import randint


class DSAKey:
    def __init__(self):
        self.y = None
        self.g = None
        self.p = None
        self.q = None
        self.x = None

    def _generate(self, bits):
        if bits < 160:
            raise ValueError('Key is too short!')

        while True:
            s, self.q = self._generate_q()
            n = divmod(bits - 1, 160)[0]
            c, N, v = 0, 2, {}
            b = self.q >> 5 & 15
            powb = pow(2L, b)
            powL1 = pow(2L, bits - 1)

            while c < 4096:
                for k in range(0, n+1):
                    v[k] = bytes_to_long(SHA.new(s + str(N) + str(k)).digest())

                w = v[n] % powb

                for k in range(n - 1, -1, -1):
                    w = (w << 160L) + v[k]

                x = w + powL1
                self.p = x - (x % (2 * self.q) - 1)

                if powL1 <= self.p and isPrime(self.p):
                    break

                c, N = c + 1, N + n + 1

            if c < 4069:
                break

        power = divmod(self.p - 1, self.q)[0]

        while True:
            h = bytes_to_long(os.urandom(bits)) % (self.p - 1)
            self.g = pow(h, power, self.p)

            if 1 < h < self.p - 1 and self.g > 1:
                break

        while True:
            self.x = bytes_to_long(os.urandom(20))

            if 0 < self.x < self.q:
                break

        self.y = pow(self.g, self.x, self.p)

    def _generate_q(self):
        s = os.urandom(20)
        hash1 = SHA.new(s).digest()
        hash2 = SHA.new(long_to_bytes(bytes_to_long(s)+1)).digest()
        q = 0L

        for i in range(0, 20):
            c = ord(hash1[i]) ^ ord(hash2[i])

            if i == 0:
                c |= 128

            if i == 19:
                c |= 1

            q = q * 256 + c

        while not isPrime(q):
            q += 2

        if pow(2, 159L) < q < pow(2, 160L):
            return s, q

        raise ValueError('Bad q value generated')

    def is_private(self):
        return self.x is not None

    def sign(self, message):
        if not self.is_private():
            raise TypeError('Could not sign message with public key')

        m = bytes_to_long(SHA.new(message).digest())
        k = randint(1, self.q - 1)
        inverse_k = inverse(k, self.q)
        r = pow(self.g, k, self.p) % self.q
        s = (inverse_k * (m + self.x * r)) % self.q

        return r, s

    def verify(self, message, r, s):
        m = bytes_to_long(SHA.new(message).digest())

        if not (0 < r < self.q) or not (0 < s < self.q):
            return False

        w = inverse(s, self.q)
        u1 = (m * w) % self.q
        u2 = (r * w) % self.q
        v = (pow(self.g, u1, self.p) * pow(self.y, u2, self.p) % self.p) % self.q

        return v == r

    @staticmethod
    def generate_instance(bits):
        dsa_key = DSAKey()
        dsa_key._generate(bits)
        return dsa_key
