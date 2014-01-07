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
            seed, self.q = self._generate_q()
            n = divmod(bits - 1, 160)[0]
            counter, offset, v = 0, 2, {}
            b = self.q >> 5 & 15
            two_power_b = pow(2L, b)
            two_power_bits_minus_one = pow(2L, bits - 1)

            while counter < 4096:
                for k in range(0, n+1):
                    v[k] = bytes_to_long(SHA.new(
                        seed + str(offset) + str(k)).digest())

                w = v[n] % two_power_b

                for k in range(n - 1, -1, -1):
                    w = (w << 160L) + v[k]

                x = w + two_power_bits_minus_one
                c = x % (2 * self.q)
                self.p = x - (c - 1)

                if two_power_bits_minus_one <= self.p and isPrime(self.p):
                    break

                counter += 1
                offset += n + 1

            if counter < 4069:
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
        seed = os.urandom(20)
        hash1 = SHA.new(seed).digest()
        hash2 = SHA.new(long_to_bytes(bytes_to_long(seed)+1)).digest()
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
            return seed, q

        raise ValueError('Bad q value generated')

    def is_private(self):
        return self.x is not None

    def get_private_key(self):
        if not self.is_private():
            raise TypeError('Could not convert public key to private key')

        private_key = DSAKey()
        private_key.y = self.y
        private_key.g = self.g
        private_key.p = self.p
        private_key.q = self.q
        private_key.x = self.x

        return private_key

    def get_public_key(self):
        public_key = DSAKey()
        public_key.y = self.y
        public_key.g = self.g
        public_key.p = self.p
        public_key.q = self.q
        public_key.x = self.x

        return public_key

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
