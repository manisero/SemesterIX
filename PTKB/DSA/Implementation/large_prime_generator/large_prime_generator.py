import random
import basic_primes


class LargePrimeGenerator:
    def __init__(self):
        self.confidence_level = 128

    def generate_large_prime(self, bits_from, bits_to):
        if bits_from < 10:
            raise LargePrimeGeneratorException('Method generates primes with more than 10 bits only!')
        elif bits_from >= bits_to:
            raise LargePrimeGeneratorException('bits_to has to be greater than bits_from')

        while True:
            random_number = random.randrange(2**bits_from, 2**bits_to)

            if self.test_if_prime(random_number):
                return random_number

    def test_if_prime(self, number):
        if number >= 3:
            if (number & 1) != 0:
                if number not in basic_primes.BasicPrimes.Primes:
                    if len(filter(lambda x: number % x == 0, basic_primes.BasicPrimes.Primes)) == 0:
                        return self.rabin_miller_test(number)
                else:
                    return True

        return False

    def rabin_miller_test(self, number):
        reduced_even = number - 1
        reduced_even_two_exponent = 0

        while reduced_even & 1 == 0:
            reduced_even /= 2
            reduced_even_two_exponent += 1

        for current_confidence_level in range(0, self.confidence_level, 2):
            random_number = random.randrange(2, number - 1)
            value = pow(random_number, reduced_even, number)

            if value != 1:
                iteration = 0

                while value != number - 1:
                    if iteration == reduced_even_two_exponent - 1:
                        return False

                    iteration += 1
                    value = (value**2) % number

        return True


class LargePrimeGeneratorException(Exception):
    pass
